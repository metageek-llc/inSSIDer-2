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
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using inSSIDer.Misc;
using MetaGeek.Gps;
using MetaGeek.WiFi;
using System.IO;

namespace inSSIDer.FileIO
{
    public static class GpxIO
    {
        public static List<Waypoint> ReadGpxFiles(IEnumerable<string> filenames)
        {
            List<Waypoint> points = new List<Waypoint>();
            List<Waypoint> tempPoints;
            foreach (string filename in filenames)
            {
                tempPoints = ReadGpx(filename);
                if(tempPoints == null) continue;
                points.AddRange(tempPoints);
                tempPoints.Clear();
            }
            return points;
        }

        private static List<Waypoint> ReadGpx(string filename)
        {
            if(!File.Exists(filename)) return null;

            List<Waypoint> points = new List<Waypoint>();

            XmlTextReader xtr = new XmlTextReader(filename);

            string subNodeName;

            Waypoint wp;

            try
            {
                //Loop through all elements
                while (xtr.Read())
                {
                    if (xtr.NodeType != XmlNodeType.Element && xtr.NodeType != XmlNodeType.EndElement &&
                        xtr.NodeType != XmlNodeType.Text)
                        continue;

                    //We're looking for waypoints
                    if (xtr.Name != "wpt") continue;

                    wp = new Waypoint();
                    if (xtr.HasAttributes) // Found latitude and longitude
                    {
                        string tempVal = xtr.GetAttribute("lat");
                        wp.Latitude = Convert.ToDouble(string.IsNullOrEmpty(tempVal) ? "0" : tempVal, CultureInfo.InvariantCulture.NumberFormat);

                        tempVal = xtr.GetAttribute("lon");
                        wp.Longitude = Convert.ToDouble(string.IsNullOrEmpty(tempVal) ? "0" : tempVal, CultureInfo.InvariantCulture.NumberFormat);
                    }

                    //Move to next node
                    xtr.Read();

                    //Loop through sub items
                    do
                    {
                        subNodeName = xtr.Name;
                        switch (subNodeName)
                        {
                            case "ele":
                                wp.Elevation = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                xtr.Read();
                                break;
                            case "time":
                                wp.Time = xtr.ReadString();
                                xtr.Read();
                                break;
                            case "name":
                                wp.Name = xtr.ReadString();
                                xtr.Read();
                                break;
                            case "cmt":
                                wp.Cmt = xtr.ReadString();
                                xtr.Read();
                                break;
                                //Description is not read from GPX. It is remade by the LogViewer.
                            case "desc":
                                xtr.Read();
                                xtr.Read();
                                break;
                            case "fix":
                                wp.Fix = xtr.ReadString();
                                xtr.Read();
                                break;
                            case "sat":
                                wp.SatCount = Convert.ToInt32(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                xtr.Read();
                                break;
                            case "vdop":
                                wp.Vdop = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                xtr.Read();
                                break;
                            case "hdop":
                                wp.Hdop = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                xtr.Read();
                                break;
                            case "pdop":
                                wp.Pdop = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                xtr.Read();
                                break;
                            case "":
                                //Blank tags should be ignored
                                break;
                            case "wpt":
                                //Closing tag of this element.
                                break;
                            case "extensions":
                                //The extensions element specifies information about our access point at this spot.
                                xtr.Read();
                                string subSubNodeName;

                                do
                                {
                                    subSubNodeName = xtr.Name;
                                    switch (subSubNodeName)
                                    {
                                        case "MAC":
                                            wp.Extensions.MacAddress = xtr.ReadString();
                                            xtr.Read();
                                            break;
                                        case "SSID":
                                            wp.Extensions.Ssid = xtr.ReadString();
                                            xtr.Read();
                                            break;
                                        case "RSSI":
                                            wp.Extensions.Rssi = Convert.ToInt32(xtr.ReadString());
                                            xtr.Read();
                                            break;
                                        case "ChannelID":
                                            wp.Extensions.Channel = Convert.ToUInt32(xtr.ReadString());
                                            xtr.Read();
                                            break;
                                        case "privacy":
                                            wp.Extensions.Privacy = xtr.ReadString();
                                            xtr.Read();
                                            break;
                                        case "signalQuality":
                                            wp.Extensions.SignalQuality = Convert.ToUInt32(xtr.ReadString());
                                            xtr.Read();
                                            break;
                                        case "networkType":
                                            wp.Extensions.NetworkType = xtr.ReadString();
                                            xtr.Read();
                                            break;
                                        case "rates":
                                            wp.Extensions.Rates = xtr.ReadString();
                                            xtr.Read();
                                            break;
                                            //Blank tags should be ignored.
                                        case "":
                                            break;
                                            //Closing tag should be skipped over also.
                                        case "extensions":
                                            break;
                                        default:
                                            xtr.Read();
                                            xtr.Read();
                                            break;
                                    }

                                    xtr.Read();
                                } while (subSubNodeName != "extensions");

                                break;
                                //Any other elements of GPX not implemented here but possibilty present in the log.
                                //e.g. magvar, geoidheight, src, link, sym, type, ageofdgpsdata, dgpsid
                            default:
                                xtr.Read();
                                xtr.Read();
                                break;
                        }
                        xtr.Read();

                    } while (subNodeName != "wpt");

                    //Add the Waypoint to the list
                    points.Add(wp);
                }
            }
            catch(XmlException)
            {
                
            }

            //doc.

            xtr.Close();

            return points;
        }

        public static IEnumerable<Waypoint> ReadGpx(Stream filename)
        {
            //if (!System.IO.File.Exists(filename)) return null;

            List<Waypoint> points = new List<Waypoint>();

            XmlTextReader xtr = new XmlTextReader(filename);

            Waypoint wp;

            try
            {
                //Loop through all elements
                while (xtr.Read())
                {
                    if (xtr.NodeType != XmlNodeType.Element && xtr.NodeType != XmlNodeType.EndElement &&
                        xtr.NodeType != XmlNodeType.Text)
                        continue;

                    if (xtr.Name == "wpt") //Found a waypoint node
                    {
                        wp = new Waypoint();
                        if (xtr.HasAttributes) // Found latitude and longitude
                        {
                            string tempVal = xtr.GetAttribute("lat");
                            wp.Latitude = Convert.ToDouble(string.IsNullOrEmpty(tempVal) ? "0" : tempVal, CultureInfo.InvariantCulture.NumberFormat);

                            tempVal = xtr.GetAttribute("lon");
                            wp.Longitude = Convert.ToDouble(string.IsNullOrEmpty(tempVal) ? "0" : tempVal, CultureInfo.InvariantCulture.NumberFormat);
                        }

                        //Move to next node
                        xtr.Read();

                        //Loop through sub items
                        string subNodeName;
                        do
                        {
                            subNodeName = xtr.Name;
                            switch (subNodeName)
                            {
                                case "ele":
                                    wp.Elevation = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                    xtr.Read();
                                    break;
                                case "time":
                                    wp.Time = xtr.ReadString();
                                    xtr.Read();
                                    break;
                                case "name":
                                    wp.Name = xtr.ReadString();
                                    xtr.Read();
                                    break;
                                case "cmt":
                                    wp.Cmt = xtr.ReadString();
                                    xtr.Read();
                                    break;
                                //Description is not read from GPX. It is regenerated on save
                                case "desc":
                                    xtr.Read();
                                    xtr.Read();
                                    break;
                                case "fix":
                                    wp.Fix = xtr.ReadString();
                                    xtr.Read();
                                    break;
                                case "sat":

                                    wp.SatCount = Convert.ToInt32(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                    xtr.Read();
                                    break;
                                case "vdop":
                                    wp.Vdop = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                    xtr.Read();
                                    break;
                                case "hdop":
                                    wp.Hdop = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                    xtr.Read();
                                    break;
                                case "pdop":
                                    wp.Pdop = Convert.ToDouble(xtr.ReadString(), CultureInfo.InvariantCulture.NumberFormat);
                                    xtr.Read();
                                    break;
                                case "":
                                    //Blank tags should be ignored
                                    break;
                                case "wpt":
                                    //Closing tag of this element.
                                    break;
                                case "extensions":
                                    //The extensions element specifies information about our access point at this spot.
                                    xtr.Read();
                                    string subSubNodeName;

                                    do
                                    {
                                        subSubNodeName = xtr.Name;
                                        switch (subSubNodeName)
                                        {
                                            case "MAC":
                                                wp.Extensions.MacAddress = xtr.ReadString();
                                                xtr.Read();
                                                break;
                                            case "SSID":
                                                wp.Extensions.Ssid = xtr.ReadString();
                                                xtr.Read();
                                                break;
                                            case "RSSI":
                                                wp.Extensions.Rssi = Convert.ToInt32(xtr.ReadString());
                                                xtr.Read();
                                                break;
                                            case "ChannelID":
                                                wp.Extensions.Channel = Convert.ToUInt32(xtr.ReadString());
                                                xtr.Read();
                                                break;
                                            case "privacy":
                                                wp.Extensions.Privacy = xtr.ReadString();
                                                xtr.Read();
                                                break;
                                            case "signalQuality":
                                                wp.Extensions.SignalQuality = Convert.ToUInt32(xtr.ReadString());
                                                xtr.Read();
                                                break;
                                            case "networkType":
                                                wp.Extensions.NetworkType = xtr.ReadString();
                                                xtr.Read();
                                                break;
                                            case "rates":
                                                wp.Extensions.Rates = xtr.ReadString();
                                                xtr.Read();
                                                break;
                                            //Blank tags should be ignored.
                                            case "":
                                                break;
                                            //Closing tag should be skipped over also.
                                            case "extensions":
                                                break;
                                            default:
                                                xtr.Read();
                                                xtr.Read();
                                                break;
                                        }

                                        xtr.Read();
                                    } while (subSubNodeName != "extensions");

                                    break;
                                //Any other elements of GPX not implemented here but possibilty present in the log.
                                //e.g. magvar, geoidheight, src, link, sym, type, ageofdgpsdata, dgpsid
                                default:
                                    xtr.Read();
                                    xtr.Read();
                                    break;
                            }
                            xtr.Read();

                        } while (subNodeName != "wpt");

                        //Add the Waypoint to the list
                        points.Add(wp);

                    }

                }
            }
            catch (XmlException)
            {

            }

            //doc.

            return points;
        }

        public static void WriteGpx(Stream filename, IEnumerable<Waypoint> waypoints)
        {
            //Reset the stream
            filename.SetLength(0);
            filename.Position = 0;

            //            XmlTextWriter xtw = new XmlTextWriter(filename, null);
            XmlWriter xtw = XmlWriter.Create(filename, new XmlWriterSettings {Indent = true});

            //Start the file
            if (xtw != null)
            {
                xtw.WriteStartDocument();

                //start gpx
                //xtw.WriteRaw(@"<gpx version=""1.0"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://www.topografix.com/GPX/1/0"" xmlns:topografix=""http://www.topografix.com/GPX/Private/TopoGrafix/0/1"" xsi:schemaLocation=""http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd http://www.topografix.com/GPX/Private/TopoGrafix/0/1 http://www.topografix.com/GPX/Private/TopoGrafix/0/1/topografix.xsd"">");
                xtw.WriteStartElement("gpx");
                try
                {
                    foreach (Waypoint wp in waypoints)
                        //for (int i = 0; i < waypoints.Count(); i++)
                    {
                        //wp = waypoints.ToArray()[i];
                        xtw.WriteStartElement("wpt");
                        //Latitude attribute
                        xtw.WriteStartAttribute("lat");
                        xtw.WriteString(wp.Latitude.ToString("###.######",CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndAttribute();

                        //Longitude attribute
                        xtw.WriteStartAttribute("lon");
                        xtw.WriteString(wp.Longitude.ToString("###.######", CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndAttribute();

                        //Elevation
                        xtw.WriteStartElement("ele");
                        xtw.WriteString(wp.Elevation.ToString(CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndElement();

                        //Time
                        xtw.WriteStartElement("time");
                        xtw.WriteString(wp.Time);
                        xtw.WriteEndElement();

                        //Geoidheight
                        xtw.WriteStartElement("geoidheight");
                        xtw.WriteString(wp.GeoidHeight.ToString(CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndElement();

                        //Name
                        xtw.WriteStartElement("name");
                        xtw.WriteString(wp.Name);
                        xtw.WriteEndElement();

                        //Speed (cmt)
                        xtw.WriteStartElement("cmt");
                        xtw.WriteString(wp.Cmt);
                        xtw.WriteEndElement();

                        //Description
                        xtw.WriteStartElement("desc");
                        xtw.WriteString(wp.Description);
                        xtw.WriteEndElement();

                        //GPS Fix
                        xtw.WriteStartElement("fix");
                        xtw.WriteString(wp.Fix);
                        xtw.WriteEndElement();

                        //Satellite count
                        xtw.WriteStartElement("sat");
                        xtw.WriteString(wp.SatCount.ToString());
                        xtw.WriteEndElement();

                        //hdop
                        xtw.WriteStartElement("hdop");
                        xtw.WriteString(wp.Hdop.ToString(CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndElement();

                        //vdop
                        xtw.WriteStartElement("vdop");
                        xtw.WriteString(wp.Vdop.ToString(CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndElement();

                        //pdop
                        xtw.WriteStartElement("pdop");
                        xtw.WriteString(wp.Pdop.ToString(CultureInfo.InvariantCulture.NumberFormat));
                        xtw.WriteEndElement();

                        //Extensions
                        xtw.WriteStartElement("extensions");

                        //MAC
                        xtw.WriteStartElement("MAC");
                        xtw.WriteString(wp.Extensions.MacAddress);
                        xtw.WriteEndElement();

                        //SSID
                        xtw.WriteStartElement("SSID");
                        xtw.WriteString(wp.Extensions.Ssid);
                        xtw.WriteEndElement();

                        //RSSI
                        xtw.WriteStartElement("RSSI");
                        xtw.WriteString(wp.Extensions.Rssi.ToString());
                        xtw.WriteEndElement();

                        //Channel
                        xtw.WriteStartElement("ChannelID");
                        xtw.WriteString(wp.Extensions.Channel.ToString());
                        xtw.WriteEndElement();

                        //Privacy
                        xtw.WriteStartElement("privacy");
                        xtw.WriteString(wp.Extensions.Privacy);
                        xtw.WriteEndElement();

                        //Signal Quality
                        xtw.WriteStartElement("signalQuality");
                        xtw.WriteString(wp.Extensions.SignalQuality.ToString());
                        xtw.WriteEndElement();

                        //Network Type
                        xtw.WriteStartElement("networkType");
                        xtw.WriteString(wp.Extensions.NetworkType);
                        xtw.WriteEndElement();

                        //Rates
                        xtw.WriteStartElement("rates");
                        xtw.WriteString(wp.Extensions.Rates);
                        xtw.WriteEndElement();

                        //Close extensions
                        xtw.WriteEndElement();

                        //Close wpt
                        xtw.WriteEndElement();
                    }
                }
                catch (InvalidOperationException)
                {

                }

                //Close gpx
                xtw.WriteEndElement();

                //Close the file
                xtw.WriteEndDocument();

                xtw.Flush();
            }
            //xtw.Close();
        }

        public static Waypoint ConvertNetworkDataToWaypoint(NetworkData data, GpsData gpsData)
        {
            Waypoint outpoint = new Waypoint();

            outpoint.Latitude = gpsData.Latitude;
            outpoint.Longitude = gpsData.Longitude;

            outpoint.Elevation = gpsData.Altitude;
            outpoint.Time = string.Format("{0}-{1}-{2}T{3}:{4}:{5}.{6}Z",
                                          new object[]
                                              {
                                                  gpsData.SatelliteTime.Year,
                                                  gpsData.SatelliteTime.Month.ToString("D2"),
                                                  gpsData.SatelliteTime.Day.ToString("D2"),
                                                  gpsData.SatelliteTime.Hour.ToString("D2"),
                                                  gpsData.SatelliteTime.Minute.ToString("D2"),
                                                  gpsData.SatelliteTime.Second,
                                                  gpsData.SatelliteTime.Millisecond
                                              });

            outpoint.GeoidHeight = gpsData.GeoidSeperation;
            //The SSID must be cleaned
            outpoint.Name = XmlHelper.CleanString(data.Ssid) + " [" + data.MyMacAddress + "]";

            outpoint.Cmt = gpsData.Speed.ToString(CultureInfo.InvariantCulture.NumberFormat);

            //outpoint.Description = string.Format(
            //    "{0}\r\n[{1}]\r\nRSSI: {2} dB\r\nQuality: {3}%\r\nChannel {4}\r\nSpeed (kph): {5}\r\n{6}",
            //    new object[]
            //        {
            //            XmlHelper.CleanString(data.Ssid), data.MyMacAddress.ToString(), data.Rssi, data.SignalQuality,
            //            data.Channel, gpsData.Speed,
            //            gpsData.SatelliteTime.ToString()
            //        });

            outpoint.Fix = gpsData.FixType;
            outpoint.SatCount = gpsData.SatellitesUsed;
            outpoint.Hdop = gpsData.Hdop;
            outpoint.Vdop = gpsData.Vdop;
            outpoint.Pdop = gpsData.Pdop;

            outpoint.Extensions.MacAddress = data.MyMacAddress.ToString();
            outpoint.Extensions.Ssid = XmlHelper.CleanString(data.Ssid);
            outpoint.Extensions.Rssi = data.Rssi;
            outpoint.Extensions.Channel = data.Channel;
            outpoint.Extensions.Privacy = data.Privacy;
            outpoint.Extensions.SignalQuality = data.SignalQuality;
            outpoint.Extensions.NetworkType = data.NetworkType;
            outpoint.Extensions.Rates = data.SupportedRates;

            return outpoint;
        }
    }

    public class Waypoint
    {
        public double Latitude;
        public double Longitude;
        public double Elevation;
        public double GeoidHeight;
        public double Vdop;
        public double Hdop;
        public double Pdop;
        public string Cmt = string.Empty; // Speed
        public string Time = string.Empty;
        public string Name = string.Empty;
        public string Description
        {
            get
            {
                return string.Format(
                    "{0}\r\n[{1}]\r\nRSSI: {2} dB\r\nQuality: {3}%\r\nChannel {4}\r\nSpeed (kph): {5}\r\n{6}",
                    new object[]
                    {
                        Extensions.Ssid, Extensions.MacAddress, Extensions.Rssi, Extensions.SignalQuality,
                        Extensions.Channel, Cmt, Time
                    });
            }
        }
        public string Fix = string.Empty;
        public int SatCount;
        public readonly Extension Extensions = new Extension();
        public bool Ignore;

        public string BuildKmlDescription()
        {
            //Generates the description string
            return Extensions.Ssid + " [" + Extensions.MacAddress + "]" + Environment.NewLine
             + Extensions.Privacy + Environment.NewLine
             + Extensions.Rssi + "dBm" +Environment.NewLine
             + "Channel " + Extensions.Channel + Environment.NewLine
             + "GPS" + Environment.NewLine
             + "     Lat,Lon,Alt  " + Latitude + "," + Longitude + "," + Elevation + Environment.NewLine
             + "     Speed (km/h) " + Cmt + Environment.NewLine
             + "     Time (UTC)   " + Time + Environment.NewLine
             + "     Precision    " + Environment.NewLine
             + "          Satellite Count " + SatCount + Environment.NewLine
             + "          Fix Mode        " + Fix + Environment.NewLine
             + "          VDOP            " + Vdop + Environment.NewLine
             + "          HDOP            " + Hdop + Environment.NewLine
             + "          PDOP            " + Pdop + Environment.NewLine;
        }

        public class Extension
        {
            public string MacAddress = string.Empty;
            public string Ssid = string.Empty;
            public int Rssi = -100;
            public uint Channel;
            public string Privacy = string.Empty;
            public uint SignalQuality;
            public string NetworkType = string.Empty;
            public string Rates = string.Empty;
        }
        
    }
}
