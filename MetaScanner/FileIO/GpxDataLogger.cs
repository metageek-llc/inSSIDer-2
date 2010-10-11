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
using System.Xml;
using System.IO;
using MetaGeek.Gps;
using MetaGeek.WiFi;

namespace inSSIDer.FileIO
{
    public class GpxDataLogger
    {
        private string _filename = string.Empty;
        public bool AutoSave;
        public TimeSpan AutoSaveInterval = TimeSpan.FromSeconds(20);
        private DateTime _lastSaveTime = DateTime.MinValue;
        //private XmlDocument _doc;

        private readonly List<Waypoint> _data;

        private FileStream _fsOutput;

        public bool Enabled;

        //private string _xmlHeader = @"<?xml version=""1.0""?><gpx version=""1.0"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://www.topografix.com/GPX/1/0"" xmlns:topografix=""http://www.topografix.com/GPX/Private/TopoGrafix/0/1"" xsi:schemaLocation=""http://www.topografix.com/GPX/1/0 http://www.topografix.com/GPX/1/0/gpx.xsd http://www.topografix.com/GPX/Private/TopoGrafix/0/1 http://www.topografix.com/GPX/Private/TopoGrafix/0/1/topografix.xsd""></gpx>";

        
        public GpxDataLogger()
        {
            //_doc = new XmlDocument();
            //_doc.LoadXml(_xmlHeader);

            _data = new List<Waypoint>();
        }

/*
        public GpxDataLogger(string filename)
        {
            //_doc = new XmlDocument();
            Filename = filename;

            SetupLog(Filename);
        }
*/

        private void SetupLog(string filename)
        {
            //Set up the file stream
            _fsOutput = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            Reset();

            if (File.Exists(filename))
            {
                //Load last log for appending
                //_doc.Load(new XmlTextReader(filename));
                lock (_data)
                {
                    _data.AddRange(GpxIO.ReadGpx(_fsOutput));
                }
            }
        }

        /*
        public void AppendEntry(NetworkData[] aps, GpsData gpsData)
        {
            if(_doc == null) return;
            if(aps.Length < 1) return;

            XmlElement newE = null;
            XmlElement ne;

            foreach (NetworkData ap in aps)
            {
                
                //Ignore anything -100 (or below?)
                if(ap.Rssi <= -100) continue;

                newE = _doc.CreateElement("wpt");

                //Set lat and lon as attributes
                newE.SetAttribute("lat", gpsData.Latitude.ToString(CultureInfo.InvariantCulture.NumberFormat));
                newE.SetAttribute("lon", gpsData.Longitude.ToString(CultureInfo.InvariantCulture.NumberFormat));

                ne = _doc.CreateElement("ele");
                ne.AppendChild(_doc.CreateTextNode(gpsData.Altitude.ToString(CultureInfo.InvariantCulture.NumberFormat)));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("time");
                ne.AppendChild(
                    _doc.CreateTextNode(string.Format("{0}-{1}-{2}T{3}:{4}:{5}.{6}Z",
                                                      new object[]
                                                          {
                                                              gpsData.SatelliteTime.Year,
                                                              gpsData.SatelliteTime.Month.ToString("D2"),
                                                              gpsData.SatelliteTime.Day.ToString("D2"),
                                                              gpsData.SatelliteTime.Hour.ToString("D2"),
                                                              gpsData.SatelliteTime.Minute.ToString("D2"),
                                                              gpsData.SatelliteTime.Second,
                                                              gpsData.SatelliteTime.Millisecond
                                                          })));
                newE.AppendChild(ne);

                //ne = _doc.CreateElement("time");
                //ne.AppendChild(_doc.CreateTextNode(""));
                //newE.AppendChild(ne);

                ne = _doc.CreateElement("geoidheight");
                ne.AppendChild(_doc.CreateTextNode(gpsData.GeoidSeperation.ToString(CultureInfo.InvariantCulture.NumberFormat)));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("name");
                ne.AppendChild(_doc.CreateTextNode(XmlHelper.CleanString(ap.Ssid) + " [" + ap.MyMacAddress + "]"));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("cmt");
                ne.AppendChild(_doc.CreateTextNode(gpsData.Speed.ToString(CultureInfo.InvariantCulture.NumberFormat)));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("desc");
                ne.AppendChild(_doc.CreateTextNode(
                                   string.Format(
                                       "{0}\r\n[{1}]\r\nRSSI: {2} dB\r\nQuality: {3}%\r\nChannel {4}\r\nSpeed (kph): {5}\r\n{6}",
                                       new object[]
                                           {
                                               XmlHelper.CleanString(ap.Ssid), ap.MyMacAddress.ToString(), ap.Rssi, ap.SignalQuality,
                                               ap.Channel, gpsData.Speed,
                                               gpsData.SatelliteTime.ToString()
                                           })));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("fix");
                ne.AppendChild(_doc.CreateTextNode(gpsData.FixType));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("sat");
                ne.AppendChild(_doc.CreateTextNode(gpsData.SatCount.ToString()));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("hdop");
                ne.AppendChild(_doc.CreateTextNode(gpsData.HDOP.ToString(CultureInfo.InvariantCulture.NumberFormat)));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("vdop");
                ne.AppendChild(_doc.CreateTextNode(gpsData.VDOP.ToString(CultureInfo.InvariantCulture.NumberFormat)));
                newE.AppendChild(ne);

                ne = _doc.CreateElement("pdop");
                ne.AppendChild(_doc.CreateTextNode(gpsData.PDOP.ToString(CultureInfo.InvariantCulture.NumberFormat)));
                newE.AppendChild(ne);

                //Extensions = ap data
                ne = _doc.CreateElement("extensions");

                XmlElement ext = _doc.CreateElement("MAC");
                ext.AppendChild(_doc.CreateTextNode(ap.MyMacAddress.ToString()));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("SSID");
                ext.AppendChild(_doc.CreateTextNode(XmlHelper.CleanString(ap.Ssid)));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("RSSI");
                ext.AppendChild(_doc.CreateTextNode(ap.Rssi.ToString()));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("ChannelID");
                ext.AppendChild(_doc.CreateTextNode(ap.Channel.ToString()));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("privacy");
                ext.AppendChild(_doc.CreateTextNode(ap.Privacy));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("signalQuality");
                ext.AppendChild(_doc.CreateTextNode(ap.SignalQuality.ToString()));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("networkType");
                ext.AppendChild(_doc.CreateTextNode(ap.NetworkType));
                ne.AppendChild(ext);

                ext = _doc.CreateElement("rates");
                ext.AppendChild(_doc.CreateTextNode(ap.SupportedRates));
                ne.AppendChild(ext);

                newE.AppendChild(ne);

                _doc.GetElementsByTagName("gpx").Item(0).AppendChild(newE);
            }

            

            //Auto save if it's enabled and had been long enough
            if(AutoSave && (DateTime.Now - _lastSaveTime >= AutoSaveInterval || _lastSaveTime == DateTime.MinValue))
            {
                SaveLogFile();
                _lastSaveTime = DateTime.Now;
            }
        }
        */

        public void AppendEntry(NetworkData[] data, GpsData gpsData)
        {
            if (data.Length < 1) return;

            lock (_data)
            {
                foreach (NetworkData nd in data)
                {
                    _data.Add(GpxIO.ConvertNetworkDataToWaypoint(nd, gpsData));
                }
            }

            //Auto save if it's enabled and had been long enough
            if (AutoSave && (DateTime.Now - _lastSaveTime >= AutoSaveInterval || _lastSaveTime == DateTime.MinValue))
            {
                SaveLogFile();
                _lastSaveTime = DateTime.Now;
            }
        }

        private void SaveLogFile()
        {
            //if(_doc == null || string.IsNullOrEmpty(Filename)) return;
            if (_data.Count == 0 || _fsOutput == null) return;
            try
            {
                lock (_data)
                {
                    try
                    {
                        //_doc.Save(Filename);
                        GpxIO.WriteGpx(_fsOutput, _data);
                    }
                    catch (IOException ex) //The file couldn't be saved
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
            catch(XmlException ex)
            {
                //TODO: handle this properly. Use something like Debug.WriteLine()
                Console.WriteLine(ex.Message);
            }
        }

        public void Start()
        {
            try
            {
                //Set the file back up
                SetupLog(Filename);
                Enabled = true;
            }
            catch(Exception)
            {
                
            }
        }

        public void Stop()
        {
            if(!Enabled || _fsOutput == null) return;
            SaveLogFile();
            try
            {
                //Close the stream
                _fsOutput.Close();
                _fsOutput.Dispose();
                Enabled = false;
            }
            catch (Exception)
            {

            }
        }

        public void Reset()
        {
            lock (_data)
            {
                _data.Clear();
            }
        }

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; SetupLog(_filename);}
        }
    }
}
