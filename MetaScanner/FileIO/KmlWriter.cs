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
using System.Linq;
using System.Globalization;
using System.Xml;
using System.IO;
using inSSIDer.Misc;

namespace inSSIDer.FileIO
{
    public static class KmlWriter
    {

        public static void WriteSummaryKml(IEnumerable<Waypoint> points, string filename, ApOrganization groupby, bool showLabels)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\"><Document><name>Summary</name></Document></kml>");

            switch(groupby)
            {
                case ApOrganization.EncryptionThenChannel:
                    //Group APs by encryption
                    var priGroup = points.GroupBy(wp => wp.Extensions.Privacy).OrderBy(gd => gd.Key);

                    //Loop through all security groups
                    foreach (IGrouping<string, Waypoint> priWp in priGroup)
                    {
                        //Create primary group folder
                        XmlElement xePriFolder = XmlHelper.CreateKmlFolderWithName(doc, priWp.Key);

                        //Group by Channel
                        var secGroup = priWp.GroupBy(wp => wp.Extensions.Channel).OrderBy(gd => gd.Key);

                        //Loop through all channel groups
                        foreach (IGrouping<uint, Waypoint> secWp in secGroup)
                        {
                            //Create the secondary sub-folder
                            XmlElement xeChannel = XmlHelper.CreateKmlFolderWithName(doc, "Channel " + secWp.Key);

                            //Group all APs by mac address
                            var apGroup = secWp.GroupBy(wp => wp.Extensions.MacAddress);

                            //Loop to find the strongest points for each access point in this group
                            foreach (IGrouping<string, Waypoint> apG in apGroup)
                            {
                                //Find the highest signal points
                                int maxRssi = apG.Max(wp => wp.Extensions.Rssi);
                                IEnumerable<Waypoint> wps = apG.Where(wp => wp.Extensions.Rssi == maxRssi);

                                double avgLat = wps.Average(wp => wp.Latitude);
                                double avgLon = wps.Average(wp => wp.Longitude);
                                double avgEle = wps.Average(wp => wp.Elevation);
                                xeChannel.AppendChild(XmlHelper.CreatePlacemark(doc, wps.First(), true, showLabels,
                                                                                false, true, avgLat, avgLon, avgEle));
                            }
                            xePriFolder.AppendChild(xeChannel);
                        }
                        doc.GetElementsByTagName("Document").Item(0).AppendChild(xePriFolder);
                    }

                    break;
                case ApOrganization.ChannelThenEncryption:
                    //Group APs by encryption
                    var priGroup2 = points.GroupBy(wp => wp.Extensions.Channel).OrderBy(gd => gd.Key);

                    //Loop through all security groups
                    foreach (IGrouping<uint, Waypoint> priWp in priGroup2)
                    {
                        //Create primary group folder
                        XmlElement xePriFolder = XmlHelper.CreateKmlFolderWithName(doc, "Channel " + priWp.Key);

                        //Group by Channel
                        var secGroup = priWp.GroupBy(wp => wp.Extensions.Privacy).OrderBy(gd => gd.Key);

                        //Loop through all channel groups
                        foreach (IGrouping<string, Waypoint> secWp in secGroup)
                        {
                            //Create the secondary sub-folder
                            XmlElement xeChannel = XmlHelper.CreateKmlFolderWithName(doc, secWp.Key);

                            //Group all APs by mac address
                            var apGroup = secWp.GroupBy(wp => wp.Extensions.MacAddress);

                            //Loop to find the strongest points for each access point in this group
                            foreach (IGrouping<string, Waypoint> apG in apGroup)
                            {
                                //Find the highest signal points
                                int maxRssi = apG.Max(wp => wp.Extensions.Rssi);
                                IEnumerable<Waypoint> wps = apG.Where(wp => wp.Extensions.Rssi == maxRssi);

                                double avgLat = wps.Average(wp => wp.Latitude);
                                double avgLon = wps.Average(wp => wp.Longitude);
                                double avgEle = wps.Average(wp => wp.Elevation);
                                xeChannel.AppendChild(XmlHelper.CreatePlacemark(doc, wps.First(), true, showLabels,
                                                                                false, true, avgLat, avgLon, avgEle));

                            }
                            xePriFolder.AppendChild(xeChannel);
                        }
                        doc.GetElementsByTagName("Document").Item(0).AppendChild(xePriFolder);
                    }
                    
                    break;
                default:
                    break;
            }
            doc.Save(filename);
        }

        public static void WriteAccessPointKml(IEnumerable<Waypoint> points, string parentDir,bool showLabels)
        {
            XmlDocument doc;
            doc = new XmlDocument();
            const string kmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml><Document></Document></kml>";
            //doc.LoadXml(kmlHeader);

            const string dir = "APs";

            //Create the APs directory
            Directory.CreateDirectory(parentDir + "\\" + dir);

            //Group the waypoints be each AP(the MAC address)
            var apG = points.GroupBy(wp => wp.Extensions.MacAddress);

            //TODO: detect duplicate SSIDs and add (number) to the filename
            foreach (IGrouping<string, Waypoint> waypoint in apG)
            {
                //Reload the file with the KML header
                doc.LoadXml(kmlHeader);

                //Create the RSSI folder
                XmlElement xeRssi = XmlHelper.CreateKmlFolderWithName(doc, "RSSI");

                //Sort the points by RSSI
                var orderRssi = waypoint.OrderByDescending(wp => wp.Extensions.Rssi);
                int maxRssi = orderRssi.Max(wp => wp.Extensions.Rssi);

                //Loop and add all points
                foreach (Waypoint point in orderRssi)
                {
                    xeRssi.AppendChild(XmlHelper.CreatePlacemark(doc, point, true, showLabels, point.Extensions.Rssi == maxRssi, false));
                }
                //Add the folder to the document element
                doc.GetElementsByTagName("Document").Item(0).AppendChild(xeRssi);

                Waypoint lastWp = waypoint.First();

                //Try to write the file
                try
                {
                    //String tempName = String.Join("", lastWp.Extensions.Ssid.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
                    //filename ex. "fuji-2.kml"
                    doc.Save(string.Format("{0}\\{1}\\{2}.kml", parentDir, dir,
                                           String.Join("", lastWp.Extensions.Ssid.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries))));
                }
                catch(XmlException)
                {
                    //TODO: do something with this?
                }
            }
        }

        public static void WriteComprehensiveKml(IEnumerable<Waypoint> points,string filename,ApOrganization groupby,bool showLabels)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" ?><kml><Document><name>Comprehensive Data</name></Document></kml>");
            XmlElement xe;
            //XmlElement xe2;
            XmlElement xe3;
            switch (groupby)
            {
                case ApOrganization.EncryptionThenChannel:
                    var grouped = points.GroupBy(wp => wp.Extensions.Privacy).OrderBy(gd => gd.Key);

                    foreach (IGrouping<string, Waypoint> waypoint in grouped)
                    {
                        //Create folder element
                        xe = XmlHelper.CreateKmlFolderWithName(doc, waypoint.Key);//doc.CreateElement("Folder");

                        var g2 = waypoint.GroupBy(wp => wp.Extensions.Channel).OrderBy(ch => ch.Key);
                        foreach (IGrouping<uint, Waypoint> grouping in g2)
                        {
                            //Create sub-folder
                            xe3 = XmlHelper.CreateKmlFolderWithName(doc, "Channel " + grouping.Key);//doc.CreateElement("Folder");

                            //Group all points by mac address
                            var apgroup = grouping.GroupBy(wp => wp.Extensions.MacAddress);

                            //Add points to parent element
                            WriteSubItems(doc, apgroup, xe3, showLabels);

                            //Add the channel folder to the parent security folder
                            xe.AppendChild(xe3);
                        }

                        //Add the security folder to the main document
                        doc.GetElementsByTagName("Document").Item(0).AppendChild(xe);
                    }
                   
                    //Save kml file
                    doc.Save(filename);
                    break;
                case ApOrganization.ChannelThenEncryption:
                    var groupedc = points.GroupBy(wp => wp.Extensions.Channel).OrderBy(gd => gd.Key);

                    foreach (IGrouping<uint, Waypoint> waypoint in groupedc)
                    {
                        //Create folder element
                        xe = XmlHelper.CreateKmlFolderWithName(doc,"Channel " + waypoint.Key);//doc.CreateElement("Folder");

                        var g2 = waypoint.GroupBy(wp => wp.Extensions.Privacy).OrderBy(ch => ch.Key);
                        foreach (IGrouping<string, Waypoint> grouping in g2)
                        {
                            //Create sub-folder
                            xe3 = XmlHelper.CreateKmlFolderWithName(doc, grouping.Key);//doc.CreateElement("Folder");

                            //Group all points by mac address
                            var apgroup = grouping.GroupBy(wp => wp.Extensions.MacAddress);

                            //Add points to parent element
                            WriteSubItems(doc, apgroup, xe3, showLabels);

                            //Add the channel folder to the parent security folder
                            xe.AppendChild(xe3);
                        }

                        //Add the security folder to the main document
                        doc.GetElementsByTagName("Document").Item(0).AppendChild(xe);
                    }

                    //Save kml file
                    doc.Save(filename);
                    break;
                default:
                    break;
            }
            
        }

        private static void WriteSubItems(XmlDocument doc, IEnumerable<IGrouping<string, Waypoint>> points, XmlElement xeParent,bool showLabels)
        {
            if(xeParent == null || points == null || doc == null) return;

            //Loop through all the groups
            foreach (IGrouping<string, Waypoint> apg in points)
            {
                XmlElement xeAp1 = XmlHelper.CreateKmlFolderWithName(doc, apg.First().Extensions.Ssid);

                //Sort by rssi ro have the max at the top
                IEnumerable<Waypoint> wps = apg.OrderByDescending(wp => wp.Extensions.Rssi);

                //The point with the highest signal strength goes directly in the folder while all the other points go in the RSSI folder
                xeAp1.AppendChild(XmlHelper.CreatePlacemark(doc, wps.First(), true, showLabels, true, true));

                XmlElement xeRssi = XmlHelper.CreateKmlFolderWithName(doc, "RSSI");

                //for skipping the first element
                bool first = true;
                foreach (Waypoint p in wps)
                {
                    //Skip the first element
                    if (first)
                    {
                        first = false;
                        continue;
                    }

                    if (p.Extensions.Rssi == wps.First().Extensions.Rssi)
                    {
                        //Show with paddle marker if the rssi is one of the highest
                        //Add waypoint
                        xeRssi.AppendChild(XmlHelper.CreatePlacemark(doc, p, true, showLabels, true, false));
                    }
                    else
                    {
                        //Add waypoint
                        xeRssi.AppendChild(XmlHelper.CreatePlacemark(doc, p, true, showLabels, false, false));
                    }
                }

                //Add the rssi folder to the AP folder
                xeAp1.AppendChild(xeRssi);

                //Add the element to the parent element
                xeParent.AppendChild(xeAp1);
            }
        }

        public static Waypoint[] FilterData(Waypoint[] points, WaypointFilterArgs args)
        {
            if(args == null) return points;

            var gp = points.GroupBy(wp => wp.Extensions.MacAddress);

            //string currentMac;

            foreach (IGrouping<string, Waypoint> waypoint in gp)
            {
                //Console.WriteLine(waypoint.Key);
                //currentMac = waypoint.Key;
                foreach (Waypoint wp in waypoint)
                {
                    //If the point is already ignored, skip it
                    if(wp.Ignore) continue;
                    //Ignore the point of the GPS seemed to have been locked up
                    if (args.GpsLockedUp)
                    {
                        if (wp != null)
                        {
                            Waypoint[] ps = waypoint.Where(wpt => wpt != wp && !wp.Ignore && wpt.Time.Equals(wp.Time)).ToArray();
                            if (ps.Count() > 0)
                            {
                                //I'm just using this to set ignore on all bad points
                                ps.All(tr => tr.Ignore = true);
                            }
                        }
                    }

                    //GPS fix filter
                    if(args.GpsFixLost)
                    {
                        if(wp.Fix != "2d" || wp.Fix != "3d" || wp.Fix != "dgps")
                        {
                            wp.Ignore = true;
                        }
                    }

                    //Minimum number of satellites
                    if(args.MinimumSatsVisible > -1)
                    {
                        if(wp.SatCount < args.MinimumSatsVisible)
                            wp.Ignore = true;
                    }

                    //Maximum speed filter
                    //Perhaps they're guilty about exceeding the speed limit, or perhaps the signal strength measurement losses accuracy?
                    if(args.MaximumSpeedKmh > -1)
                    {
                        //If the speed isn't avalible, ignore this point?
                        if(string.IsNullOrEmpty(wp.Cmt)) wp.Ignore = true;
                        else
                        {
                            try
                            {
                                double speed = double.Parse(wp.Cmt, CultureInfo.InvariantCulture.NumberFormat);
                                if (speed > args.MaximumSpeedKmh) wp.Ignore = true;
                            }
                            catch(Exception)
                            {
                                //If something went wrong, ignore the point.
                                wp.Ignore = true;
                            }
                        }
                    }

                    //Ignore high signal strengths
                    if(args.MaxSignal > -101)
                    {
                        if(wp.Extensions.Rssi > args.MaxSignal)
                            wp.Ignore = true;
                    }
                }
            }

            return points;
        }

        internal static string EncryptionColor(string encryption)
        {
            switch(EncryptionIndex(encryption))
            {
                case 1: //Green
                    return "ff00ff00";
                case 2: //Yellow
                    return "ff00ffff";
                case 3: //Orange
                case 4: //WPA-TKIP is orange
                    return "ff00aaff";
                case 5: //Red
                    return "ff0000ff";
                default:
                    return "ffffffff";
            }
        }

        private static int EncryptionIndex(string encryption)
        {
            if (encryption.Contains("No") || encryption.Contains("no"))
                return 1;
            if (encryption.Contains("WEP"))
                return 2;
            if (encryption.Contains("WPA-TKIP"))
                return 4;
            if (encryption.Contains("WPA2") || encryption.Contains("RSNA"))
                return 5;
            if (encryption.Contains("WPA"))
                return 3;
            //If its made it this far and still unsure
            return 0;
        }

        internal static double IconScale(int rssi)
        {
            return ((100 + rssi) / 15.0);
        }
    }

    public class WaypointFilterArgs
    {
        public bool GpsLockedUp;
        public bool GpsFixLost;
        public int MinimumSatsVisible = -1;
        public int MaximumSpeedKmh = -1;
        public int MaxSignal = -101;
    }

    public enum ApOrganization
    {
        EncryptionThenChannel, ChannelThenEncryption
    }
}
