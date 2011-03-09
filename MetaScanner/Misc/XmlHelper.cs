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

using System.Text;
using System.Xml;
using inSSIDer.FileIO;
using System.Globalization;

namespace inSSIDer.Misc
{
    public class XmlHelper
    {
        public static XmlElement CreateElementWithText(XmlDocument document, string name, string text)
        {
            XmlElement xe = document.CreateElement(name);
            xe.AppendChild(document.CreateTextNode(text));
            return xe;
        }

        public static XmlElement CreateKmlFolderWithName(XmlDocument document, string name)
        {
            XmlElement xe = document.CreateElement("Folder");
            XmlElement xe2 = document.CreateElement("name");
            xe2.AppendChild(document.CreateTextNode(name));
            xe.AppendChild(xe2);
            return xe;
        }

        public static XmlElement CreatePlacemark(XmlDocument document, Waypoint wp, bool visible, bool showLabel, bool paddleMarker, bool ssidLabel)
        {
            //Create the main placemark element
            XmlElement xeMain = document.CreateElement("Placemark");

            string color = KmlWriter.EncryptionColor(wp.Extensions.Privacy);

            //Visibility is default true
            xeMain.AppendChild(CreateElementWithText(document, "visibility", visible ? "1" : "0"));

            //placemark style
            XmlElement xeStyle = document.CreateElement("Style");
            xeStyle.SetAttribute("id", "sn_shaded_dot");

            XmlElement xeIconStyle = document.CreateElement("IconStyle");
            XmlElement xeIcon = document.CreateElement("Icon");
            xeIcon.AppendChild(CreateElementWithText(document, "href",
                                                     paddleMarker
                                                         ? "http://maps.google.com/mapfiles/kml/paddle/wht-blank.png"
                                                         : "http://maps.google.com/mapfiles/kml/shapes/shaded_dot.png"));
            //Add icon element to iconstyle element
            xeIconStyle.AppendChild(xeIcon);

            xeIconStyle.AppendChild(CreateElementWithText(document, "color", color));
            xeIconStyle.AppendChild(CreateElementWithText(document, "scale", KmlWriter.IconScale(wp.Extensions.Rssi).ToString()));

            //Add element
            xeStyle.AppendChild(xeIconStyle);

            //LabelStyle element
            XmlElement xeLabelStyle = document.CreateElement("LabelStyle");
            xeLabelStyle.AppendChild(CreateElementWithText(document, "color", color));
            xeLabelStyle.AppendChild(CreateElementWithText(document, "scale", showLabel ? "1" : "0"));
            
            //Add element
            xeStyle.AppendChild(xeLabelStyle);

            //Add Style to main placemark element
            xeMain.AppendChild(xeStyle);

            //Add name element
            xeMain.AppendChild(CreateElementWithText(document, "name", ssidLabel ? wp.Extensions.Ssid + ": " + wp.Extensions.Rssi : wp.Extensions.Rssi.ToString()));

            //Add description element
            xeMain.AppendChild(CreateElementWithText(document, "description", wp.BuildKmlDescription()));
            //Location
            //KML requires Lon,Lat,Alt. It's backwards!
            XmlElement xePoint = document.CreateElement("Point");
            xePoint.AppendChild(CreateElementWithText(document, "coordinates",
                                                      string.Format("{0},{1},{2}", 
                                                            wp.Longitude.ToString(CultureInfo.InvariantCulture.NumberFormat),
                                                            wp.Latitude.ToString(CultureInfo.InvariantCulture.NumberFormat),
                                                            wp.Elevation.ToString(CultureInfo.InvariantCulture.NumberFormat))));
            xeMain.AppendChild(xePoint);

            return xeMain;
        }

        public static XmlElement CreatePlacemark(XmlDocument document, Waypoint wp, bool visible, bool showLabel, bool paddleMarker, bool ssidLabel,double latitude,double longitude,double elevation)
        {
            //Create the main placemark element
            XmlElement xeMain = document.CreateElement("Placemark");

            string color = KmlWriter.EncryptionColor(wp.Extensions.Privacy);

            //Visibility is default true
            xeMain.AppendChild(CreateElementWithText(document, "visibility", visible ? "1" : "0"));

            //Placemark style
            XmlElement xeStyle = document.CreateElement("Style");
            xeStyle.SetAttribute("id", "sn_shaded_dot");

            XmlElement xeIconStyle = document.CreateElement("IconStyle");
            XmlElement xeIcon = document.CreateElement("Icon");
            xeIcon.AppendChild(CreateElementWithText(document, "href",
                                                     paddleMarker
                                                         ? "http://maps.google.com/mapfiles/kml/paddle/wht-blank.png"
                                                         : "http://maps.google.com/mapfiles/kml/shapes/shaded_dot.png"));
            //Add icon element to iconstyle element
            xeIconStyle.AppendChild(xeIcon);

            xeIconStyle.AppendChild(CreateElementWithText(document, "color", color));
            xeIconStyle.AppendChild(CreateElementWithText(document, "scale", KmlWriter.IconScale(wp.Extensions.Rssi).ToString()));

            //Add element
            xeStyle.AppendChild(xeIconStyle);

            //LabelStyle element
            XmlElement xeLabelStyle = document.CreateElement("LabelStyle");
            xeLabelStyle.AppendChild(CreateElementWithText(document, "color", color));
            xeLabelStyle.AppendChild(CreateElementWithText(document, "scale", showLabel || ssidLabel ? "1" : "0"));

            //Add element
            xeStyle.AppendChild(xeLabelStyle);

            //Add Style to main placemark element
            xeMain.AppendChild(xeStyle);

            //Add name element
            if(ssidLabel)
            {
                //(ssidLabel ? wp.Extensions.Ssid : "")
                //(showLabel ? wp.Extensions.Rssi.ToString() : "")
            }

            string tempName = (ssidLabel ? wp.Extensions.Ssid : "");
            if(showLabel && tempName != "")
            {
                tempName = tempName + ": " + wp.Extensions.Rssi;
            }
            else if (showLabel)
            {
                tempName = wp.Extensions.Rssi.ToString();
            }
            else if(ssidLabel)
            {
                tempName = wp.Extensions.Ssid;
            }


            xeMain.AppendChild(CreateElementWithText(document, "name", tempName));

            //Add description element
            xeMain.AppendChild(CreateElementWithText(document, "description", wp.BuildKmlDescription()));
            //Location
            //KML requires Lon,Lat,Alt. It's backwards!
            XmlElement xePoint = document.CreateElement("Point");
            xePoint.AppendChild(CreateElementWithText(document, "coordinates",
                                                      string.Format("{0},{1},{2}",
                                                              longitude.ToString(CultureInfo.InvariantCulture.NumberFormat),
                                                              latitude.ToString(CultureInfo.InvariantCulture.NumberFormat),
                                                              elevation.ToString(CultureInfo.InvariantCulture.NumberFormat))));
            xeMain.AppendChild(xePoint);

            return xeMain;
        }

        /// <summary>
        /// Replaces non-ASCII or forbidden XML characters with escape sequences or unicode
        /// </summary>
        /// <param name="input">String to be reformatted. Usually an SSID.</param>
        public static string CleanString(string input)
        {
            for (int j = 0; j < input.Length; j++)
            {
                byte charByte = (byte)input[j];
                // If it's not a standard ascii character or if its the & or < symbols
                if (!(charByte >= 32 && charByte <= 126 && charByte != 38 && charByte != 60))
                {
                    switch (charByte)
                    {
                        // no ampersands
                        case 38:
                            input = input.Substring(0, j) + "&amp;" + input.Substring(j + 1);
                            j += 4;
                            break;
                        // no less thans
                        case 60:
                            input = input.Substring(0, j) + "&lt;" + input.Substring(j + 1);
                            j += 3;
                            break;
                        // all other weird characters change directly to unicode
                        default:
                            byte[] tempByte = Encoding.Unicode.GetBytes(new char[] { input[j] });
                            string unicodeValue = System.Convert.ToString(tempByte[0] + (tempByte[1] << 8));
                            string output = "&amp;#" + unicodeValue + ";";
                            input = input.Substring(0, j) + output + input.Substring(j + 1);
                            j += output.Length - 1;
                            break;
                    }
                }
            }
            return input;
        }
    }
}
