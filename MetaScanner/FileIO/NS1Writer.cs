////////////////////////////////////////////////////////////////

#region Header

//
// Copyright (c) 2007-2010 MetaGeek, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion Header


////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MetaGeek.WiFi;

namespace inSSIDer.FileIO
{
    public class Ns1Writer
    {
        #region Public Methods

        /// <summary>
        /// Writes out a NS1 file for the supplied APs
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="points"></param>
        public static void Write(string filename, AccessPoint[] points)
        {
            List<byte> bData = new List<byte>();

            //NetS - signature
            bData.AddRange(new byte[] { 0x4E, 0x65, 0x74, 0x53 });
            //12 - version 12
            bData.AddRange(new byte[] { 0x0c, 0x00, 0x00, 0x00 });

            //How may APs follow
            bData.AddRange(BitConverter.GetBytes(points.Length));

            //Loop through all APs and write them to the list
            foreach (AccessPoint ap in points)
            {
                //Length of the SSID
                bData.Add((byte)ap.Ssid.Length);

                //The SSID
                bData.AddRange(Encoding.ASCII.GetBytes(ap.Ssid));

                //The MAC address
                bData.AddRange(ap.MacAddress.Bytes);

                //RSSI
                bData.AddRange(BitConverter.GetBytes(ap.LastData.Rssi));

                //Noise - not reported
                bData.AddRange(BitConverter.GetBytes(0).Reverse());

                //SNR - not reported
                bData.AddRange(BitConverter.GetBytes(0).Reverse());

                //802.11 capability flags. This just shows if the AP uses WEP and/or is AdHoc
                if(ap.Security.ToLower() != "open")
                {
                    if(ap.NetworkType != "Infrastructure") bData.AddRange(new byte[] { 0x12, 0x00, 0x00, 0x00 });
                    else bData.AddRange(new byte[] { 0x11, 0x00, 0x00, 0x00 });
                }
                else
                {
                    if (ap.NetworkType != "Infrastructure") bData.AddRange(new byte[] { 0x02, 0x00, 0x00, 0x00 });
                    else bData.AddRange(new byte[] { 0x01, 0x00, 0x00, 0x00 });
                }

                //Beacon interval - not reported, just use 100 msec.
                bData.AddRange(BitConverter.GetBytes((uint)100));

                //First seen time
                bData.AddRange(BitConverter.GetBytes(ap.FirstSeenTimestamp.ToFileTime()));

                //Last seen time
                bData.AddRange(BitConverter.GetBytes(ap.LastSeenTimestamp.ToFileTime()));

                //Latitude
                bData.AddRange(BitConverter.GetBytes(ap.GpsData.Latitude));

                //Longitude
                bData.AddRange(BitConverter.GetBytes(ap.GpsData.Longitude));

                //No APDATA entries.
                //TODO: add this
                bData.AddRange(BitConverter.GetBytes(0));

                //Length of name. Not used
                bData.Add(0);

                //No Name bytes

                //Bit field Channel activity. Not Used.
                bData.AddRange(BitConverter.GetBytes((long)0));

                //Channel
                bData.AddRange(BitConverter.GetBytes((int)ap.Channel));

                //IP address. Not used.
                bData.AddRange(BitConverter.GetBytes(0).Reverse());

                //Min. signal,dBm
                bData.AddRange(BitConverter.GetBytes(-100));

                //Max noise.
                bData.AddRange(BitConverter.GetBytes(0));

                //Speed
                bData.AddRange(BitConverter.GetBytes(((int)ap.MaxRate) * 10));

                //IP subnet address. Not used.
                bData.AddRange(BitConverter.GetBytes((uint)0));

                //IP netmask. Not used.
                bData.AddRange(BitConverter.GetBytes((uint)0));

                //Misc flags. Not used.
                bData.AddRange(BitConverter.GetBytes((uint)0));

                //IElength. Not used/Not needed.
                bData.AddRange(BitConverter.GetBytes((uint)0));
            }
            //Write bytes to file
            System.IO.File.WriteAllBytes(filename, bData.ToArray());
        }

        #endregion Public Methods
    }
}