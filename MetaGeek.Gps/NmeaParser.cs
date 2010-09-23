////////////////////////////////////////////////////////////////
//
// Copyright (c) 2007-2010 MetaGeek, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//
//	http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
//
////////////////////////////////////////////////////////////////

using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace MetaGeek.Gps
{
    public class Satellite
    {
        public int Id;
        public double Elevation;
        public double Azimuth;
        public double Snr;
        public bool IsUsed;
    }

    /// <summary>
    /// Processes NMEA GPS sentences
    /// </summary>
    public class NmeaParser
    {
        /// <summary>
        /// Enumerated type of GPS sentences
        /// </summary>
        public enum SentenceType
        {
            None = 0, Gprmc, Gpgsv, Gpgsa, Gpgga, Gpvtg
        }

        #region Private Data
        // Represents the EN-US culture, used for numbers in NMEA sentences
        private static readonly CultureInfo NmeaCultureInfo = CultureInfo.InvariantCulture;
        // Used to convert knots into km per hour
        private static readonly double KphPerKnot = 1.8519984;

        private readonly char[] _delimiters = { ',', '*' };
        private bool _allSatellitesLoaded;

        //GPGSA
        private const int MaxChannels = 12;
        private int[] SatIDs;

        //GPGGA

        public NmeaParser()
        {
            Satellites = new List<Satellite>();
            MagVar = double.NaN;
            SatelliteTime = DateTime.Now;
            Timestamp = DateTime.Now;
        }

        #endregion

        public double Longitude { get; private set; }

        public double Latitude { get; private set; }

        public double Speed { get; private set; }

        public DateTime Timestamp { get; private set; }

        public DateTime SatelliteTime { get; private set; }

        public double Course { get; private set; }

        public bool HasFix { get; private set; }

        public double MagVar { get; private set; }

        public List<Satellite> Satellites { get; private set; }

        public int SatelliteCount { get; private set; }

        public bool GetAllSatellitesLoaded()
        {
            return _allSatellitesLoaded;
        }

        //public int[] GetSatIDs()
        //{
        //    return _satIDs;
        //}

        private int FixMode { get; set; }

        private bool IsForced2D3D { get; set; }

        public double Pdop { get; private set; }

        public double Vdop { get; private set; }

        public double Hdop { get; private set; }

        public int SatellitesUsed { get; private set; }

        private int PositionFix { get; set; }

        public double Altitude { get; private set; }

        private double GeoIdSeperation { get; set; }

        private double DgpsAge { get; set; }

        private int Dgpsid { get; set; }

/*
        public string FixString
        {
            get
            {
                string fix = string.Empty;

                if (PositionFix <= 1)
                {
                    switch (FixMode)
                    {
                        case 1:
                            fix = "none";
                            break;

                        case 2:
                            fix = "2d";
                            break;

                        case 3:
                            fix = "3d";
                            break;

                        default:
                            fix = "none";
                            break;
                    }
                }
                else
                {
                    switch (PositionFix)
                    {
                        case 2:
                            fix = "dgps";
                            break;

                        case 3:
                            fix = "pps";
                            break;

                        default:
                            fix = "none";
                            break;
                    }
                }

                return fix;
            }
        }
*/

        public bool Validate { get; set; }

        public SentenceType Parse(string sentence)
        {
            SentenceType type = SentenceType.None;

            // Discard the sentence if its checksum does not match our calculated checksum
            if (!Validate || IsValidSentence(sentence))
            {
                bool result;

                // Look at the first word to decide where to go next
                switch (GetWords(sentence)[0])
                {
                    case "$GPRMC":
                        // A "Recommended Minimum" sentence was found!
                        result = ParseGprmc(sentence);
                        if (result)
                        {
                            type = SentenceType.Gprmc;
                        }
                        break;

                    case "$GPGSV":
                        // A "Satellites in View" sentence was received
                        result = ParseGpgsv(sentence);
                        if (result)
                        {
                            type = SentenceType.Gpgsv;
                        }
                        break;

                    case "$GPGSA":
                        result = ParseGPGSA(sentence);
                        if (result)
                        {
                            type = SentenceType.Gpgsa;
                        }
                        break;

                        // Fix Data
                    case "$GPGGA":
                        result = ParseGpgga(sentence);
                        if (result)
                        {
                            type = SentenceType.Gpgga;
                        }
                        break;

                    case "$GPVTG":
                        result = ParseGpvtg(sentence);
                        if (result)
                        {
                            type = SentenceType.Gpvtg;
                        }
                        break;

                    default:
                        // Indicate that the sentence was not recognized
                        break;
                }
            }

            return type;
        }

        // Divides a sentence into individual words
        private string[] GetWords(string sentence)
        {
            return sentence.Split(_delimiters);
        }

        // Interprets a $GPRMC message
        private bool ParseGprmc(string sentence)
        {
            bool result = false;

            try
            {
                string[] words = GetWords(sentence);

                string rawUtCtime = words[1];
                string rawStatus = words[2];
                string rawLatitude = words[3];
                string rawNSindicator = words[4];
                string rawLongitude = words[5];
                string rawEWindicator = words[6];
                string rawSpeedinKnots = words[7];
                string rawCourse = words[8];
                string rawUtCdate = words[9];
                string rawMagneticVariationDegrees = words[10];
                string rawMagneticVariationEw = words[11];
                //string rawChecksum = words[12];

                //GET OUR POSITION. LATITUDE & LONGITUDE
                //If we have all the necessary information
                if (rawLatitude != "" && rawNSindicator != "" && rawLongitude != "" && rawEWindicator != "")
                {
                    ParseCoordinates(rawLongitude, rawLatitude, rawNSindicator, rawEWindicator);
                }

                //DATE TIME
                //If we have the information
                if (rawUtCtime != "" && rawUtCdate != "")
                {
                    ParseTime(rawUtCtime, rawUtCdate);
                }

                // SPEED
                //If we have the information
                if (rawSpeedinKnots != "")
                {
                    // Convert to Kilometres per hour
                    Speed = double.Parse(rawSpeedinKnots, NmeaCultureInfo)*KphPerKnot;
                }

                // BEARING/COURSE
                //If we have the information
                if (rawCourse != "")
                {
                    // Indicate that the sentence was recognized
                    Course = double.Parse(rawCourse, NmeaCultureInfo);
                }

                // SATELLITE FIX
                //If we have the information
                if (rawStatus != "")
                {
                    switch (rawStatus)
                    {
                        case "A":
                            HasFix = true;
                            break;
                        case "V":
                            HasFix = false;
                            break;
                    }
                }

                //MAGNETIC VARIATION
                //if we have the information
                if (rawMagneticVariationDegrees != "" & rawMagneticVariationEw != "")
                {
                    MagVar = double.Parse(rawMagneticVariationDegrees, NmeaCultureInfo);

                    if (rawMagneticVariationEw == "W")
                    {
                        MagVar = -MagVar;
                    }
                }

                result = true;
            }
            catch
            {
            }
            return result;
        }

        // Interprets a "Satellites in View" NMEA sentence
        private bool ParseGpgsv(string sentence)
        {
            bool result = false;

            try
            {
                string[] words = GetWords(sentence);

                string rawNumberOfMessages = words[1];
                string rawSequenceNumber = words[2];
                string rawSatellitesInView = words[3];
                SatelliteCount = int.Parse(rawSatellitesInView, NmeaCultureInfo);

                if (rawSequenceNumber == "1")
                {
                    Satellites.Clear();
                    _allSatellitesLoaded = false;
                }

                if (rawSequenceNumber == rawNumberOfMessages)
                {
                    _allSatellitesLoaded = true;
                }

                int index = 4;

                if (words.Length < 16) { return false; }

                while (index <= 16 && words.Length > index + 4 && words[index] != "")
                {
                    Satellite tempSatellite = new Satellite();
                    string id = words[index];
                    if (id != "")
                    {
                        int.TryParse(id, NumberStyles.Integer, NmeaCultureInfo, out tempSatellite.Id);
                    }

                    string elevation = words[index + 1];
                    if (elevation != "")
                    {

                        tempSatellite.Elevation = double.Parse(elevation, NmeaCultureInfo);
                    }

                    string azimuth = words[index + 2];
                    if (azimuth != "")
                    {
                        tempSatellite.Azimuth = Convert.ToDouble(azimuth, CultureInfo.InvariantCulture);
                    }

                    string snr = words[index + 3];
                    tempSatellite.Snr = snr == "" ? 0 : Convert.ToDouble(snr, CultureInfo.InvariantCulture);

                    index = index + 4;

                    Satellites.Add(tempSatellite);
                }

                result = true;
            }
            catch 
            { }
            // Indicate that the sentence was recognized
            return result;
        }

        // Interprets a "Fixed Satellites and DOP" NMEA sentence
        public bool ParseGPGSA(string sentence)
        {
            bool result = false;
            try
            {
                string[] Words = GetWords(sentence);

                string rawMode1a = Words[1];
                string rawMode1b = Words[2];

                int[] satIDs_ = new int[MaxChannels];
                int idx = 3;
                int satCount = 0;

                for (int i = 0; i < MaxChannels; i++)
                {
                    try
                    {
                        if (Words[idx] != string.Empty)
                        {
                            satIDs_[i] = Convert.ToInt32(Words[idx]);
                            satCount++;
                        }
                        else
                        {
                            satIDs_[i] = int.MaxValue;
                        }
                    }
                    catch (FormatException)
                    {
                        satIDs_[i] = int.MaxValue;
                    }
                    idx++;
                }

                SatIDs = satIDs_.Where(i => i < int.MaxValue).ToArray(); //new int[satCount];

                if (Satellites != null)
                {
                    Satellites.ForEach(sat => sat.IsUsed = false);
                    Satellites.ForEach(sat => sat.IsUsed = SatIDs.Contains(sat.Id));

                }

                string rawPdop = Words[idx];
                string rawHdop = Words[idx + 1];
                string rawVdop = Words[idx + 2];

                if (rawMode1a == "M")
                {
                    IsForced2D3D = true;
                }
                if (rawMode1a == "A")
                {
                    IsForced2D3D = false;
                }

                if (rawMode1b != "")
                {
                    FixMode = Convert.ToInt32(rawMode1b);
                }
                if (rawPdop != "")
                    Pdop = Convert.ToDouble(rawPdop, CultureInfo.InvariantCulture);
                if (rawHdop != "")
                    Hdop = Convert.ToDouble(rawHdop, CultureInfo.InvariantCulture);
                if (rawVdop != "")
                    Vdop = Convert.ToDouble(rawVdop, CultureInfo.InvariantCulture);

                result = true;
            }
            catch
            {
            }
            return result;
        }

        //Interprets a "Location data" NMEA sentence
        private bool ParseGpgga(string sentence)
        {
            bool result = false;

            try
            {
                string[] words = GetWords(sentence);

                if (words.Length >= 15)
                {
                    string rawUtCtime = words[1];
                    string rawLatitude = words[2];
                    string rawNsIndicator = words[3];
                    string rawLongitude = words[4];
                    string rawEwIndicator = words[5];
                    string rawPositionFix = words[6];
                    string rawSatellitesUsed = words[7];
                    string rawHdop = words[8];
                    string rawAltitude = words[9];
                    //string rawAltitudeUnits = words[10];
                    string rawGeoidSeperation = words[11];
                    //string rawSeperationUnits = words[12];
                    string rawDgpsAge = words[13];
                    string rawDgpsStationId = words[14];

                    ParseTime(rawUtCtime, "");
                    ParseCoordinates(rawLongitude, rawLatitude, rawNsIndicator, rawEwIndicator);

                    if (!string.IsNullOrEmpty(rawSatellitesUsed))
                    {
                        SatellitesUsed = int.Parse(rawSatellitesUsed, NmeaCultureInfo);
                    }

                    if (!string.IsNullOrEmpty(rawAltitude))
                    {
                        Altitude = double.Parse(rawAltitude, NmeaCultureInfo);
                    }

                    if (!string.IsNullOrEmpty(rawGeoidSeperation))
                    {
                        GeoIdSeperation = double.Parse(rawGeoidSeperation, NmeaCultureInfo);
                    }

                    if (!string.IsNullOrEmpty(rawDgpsAge))
                    {
                        DgpsAge = double.Parse(rawDgpsAge, NmeaCultureInfo);
                    }

                    if (!string.IsNullOrEmpty(rawDgpsStationId))
                    {
                        Dgpsid = int.Parse(rawDgpsStationId, NmeaCultureInfo);
                    }

                    if (!string.IsNullOrEmpty(rawPositionFix))
                    {
                        PositionFix = int.Parse(rawPositionFix, NmeaCultureInfo);
                    }

                    result = true;
                }
            }
            catch (Exception)
            {
            }

            return result;
        }

        /// <summary>
        /// Parses a GPVTG NMEA sentence
        /// </summary>
        /// <param name="sentence">a GPVTG NMEA sentence</param>
        /// <returns>true if the parse was successful</returns>
        private bool ParseGpvtg(string sentence)
        {
            bool result = false;

            try
            {
                string[] words = GetWords(sentence);

                //string rawCourseTrue = words[1];
                //string rawReferenceTrue = words[2];
                //string rawCourseMag = words[3];
                //string rawReferenceMag = words[4];
                //string rawSpeedKnots = words[5];
                string rawSpeedKph = words[7];

                if (rawSpeedKph != "")
                {
                    Speed = double.Parse(rawSpeedKph, NmeaCultureInfo);
                }
                
                result = true;
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Checks if the supplied NMEA sentence is valid
        /// </summary>
        /// <param name="sentence">a NMEA sentence to validate</param>
        /// <returns>true if the calculated checksum matches the received checksum</returns>
        private bool IsValidSentence(string sentence)
        {
            string readChecksum = sentence.Substring(sentence.IndexOf("*") + 1).Trim();
            return readChecksum == GetChecksum(sentence);
        }

        /// <summary>
        /// Tries to parse coordinates. Sets Latitude and Longitude if possible.
        /// </summary>
        /// <param name="rawLongitude"></param>
        /// <param name="rawLatitude"></param>
        /// <param name="rawNSindicator"></param>
        /// <param name="rawEWindicator"></param>
        private void ParseCoordinates(string rawLongitude, string rawLatitude, string rawNSindicator, string rawEWindicator)
        {
            //Latitude
            if (rawLatitude != string.Empty)
            {
                try
                {
                    double latHours = double.Parse(rawLatitude.Substring(0, 2), NmeaCultureInfo);
                    double latMinutes = double.Parse(rawLatitude.Substring(2), NmeaCultureInfo);

                    Latitude = latHours + latMinutes/60;

                    if (rawNSindicator == "S")
                    {
                        Latitude = -Latitude;
                    }

                    //Longitude
                    double lonHours = double.Parse(rawLongitude.Substring(0, 3), NmeaCultureInfo);
                    double lonMinutes = double.Parse(rawLongitude.Substring(3), NmeaCultureInfo);

                    Longitude = lonHours + lonMinutes/60;

                    if (rawEWindicator == "W")
                    {
                        Longitude = -Longitude;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Tries to parse the time, sets SatelliteTime and Timestamp if possible
        /// </summary>
        /// <param name="rawUtCtime"></param>
        /// <param name="rawUtCdate"></param>
        private void ParseTime(string rawUtCtime, string rawUtCdate)
        {
            //two lines of code to save us from y2.1k
            DateTime todayTime = DateTime.Now;
            int y2K = todayTime.Year / 100;

            int utcHours = int.Parse(rawUtCtime.Substring(0, 2), NmeaCultureInfo);
            int utcMinutes = int.Parse(rawUtCtime.Substring(2, 2), NmeaCultureInfo);
            int utcSeconds = int.Parse(rawUtCtime.Substring(4, 2), NmeaCultureInfo);
            int utcMilliseconds = 0;

            // Extract milliseconds if it is available
            if (rawUtCtime.Length > 7)
            {
                utcMilliseconds = int.Parse(rawUtCtime.Substring(7), NmeaCultureInfo);
            }

            //Read the date from the satellite
            if (rawUtCdate != "")
            {
                int dd = int.Parse(rawUtCdate.Substring(0, 2), NmeaCultureInfo);
                int mm = int.Parse(rawUtCdate.Substring(2, 2), NmeaCultureInfo);
                int yy = int.Parse(rawUtCdate.Substring(4, 2), NmeaCultureInfo);
                SatelliteTime = new DateTime(y2K * 100 + yy, mm, dd, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
            }
            else
            {
                SatelliteTime = new DateTime(SatelliteTime.Year, SatelliteTime.Month, SatelliteTime.Day, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
            }
            TimeSpan deltaTime = TimeZone.CurrentTimeZone.GetUtcOffset(SatelliteTime);
            Timestamp = SatelliteTime + deltaTime;
        }

        /// <summary>
        /// Calculates the checksum for a sentence
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        private string GetChecksum(string sentence)
        {
            int checksum = 0;
            foreach (char character in sentence)
            {
                switch (character)
                {
                    case '$': // Ignore 
                        break;
                    case '*': // Stop processing before the asterisk
                        break;
                    default: // Is this the first value for the checksum?
                        // Yes. Set the checksum to the value
                        // No. XOR the checksum with this character's value
                        checksum = checksum == 0 ? Convert.ToByte(character) : checksum ^ Convert.ToByte(character);
                        break;
                }
            }

            // Return the checksum formatted as a two-character hexadecimal
            return checksum.ToString("X2");
        }
    }
}