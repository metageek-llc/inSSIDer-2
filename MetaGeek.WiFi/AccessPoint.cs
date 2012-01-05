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
using System.ComponentModel;
using System.Drawing;
using System.Linq;

using ManagedWifi;

using MetaGeek.Gps;

namespace MetaGeek.WiFi
{
    public class AccessPoint
    {
        #region Fields

        public const int MaxDataPoints = 20;
        private static long _counter;
        private readonly List<NetworkData> _networkData;
        private NetworkData _orignalData;

        #endregion Fields

        #region Properties

        /// <summary>
        /// How many seconds it has been since the AP was last heard from
        /// </summary>
        public int Age
        {
            get { return (int)(Timestamp.Subtract(LastSeenTimestamp)).TotalSeconds; }
        }

        /// <summary>
        /// The alias of this AP. FOR FUTURE USE.
        /// </summary>
        public string Alias
        {
            get; set;
        }

        /// <summary>
        /// The channel used by this AP
        /// </summary>
        public uint Channel
        {
            get { return LastData.Channel; }
        }

        /// <summary>
        /// Gets the connection status of this AP
        /// </summary>
        public bool Connected
        {
            get { return LastData.Connected; }
        }

        /// <summary>
        /// The first time the AP was heard from
        /// </summary>
        public DateTime FirstSeenTimestamp
        {
            get { return _orignalData.MyTimestamp; }
        }

        /// <summary>
        /// The GPS data for this AP.
        /// </summary>
        public GpsData GpsData
        {
            get; set;
        }

        /// <summary>
        /// Determines if the AP shows up on any graphs
        /// </summary>
        public bool Graph
        {
            get; set;
        }

        /// <summary>
        /// Determines of the AP is highlighted on the graphs
        /// </summary>
        public bool Highlight
        {
            get; set;
        }

        /// <summary>
        /// The index of the AP
        /// </summary>
        public long Index
        {
            get; set;
        }

        /// <summary>
        /// Is this AP operating in 802.11N mode
        /// </summary>
        public bool IsN
        {
            get { return _orignalData.IsTypeN; }
        }

        /// <summary>
        /// The last data added for this AP
        /// </summary>
        public NetworkData LastData
        {
            get { return MyNetworkDataCollection.Last(); }
        }

        /// <summary>
        /// The last time the AP was heard from
        /// </summary>
        public DateTime LastSeenTimestamp
        {
            get; set;
        }

        /// <summary>
        /// The MAC address of the AP
        /// </summary>
        public MacAddress MacAddress
        {
            get { return _orignalData.MyMacAddress; }
        }

        /// <summary>
        /// The Maximum data rate supported by this AP
        /// </summary>
        public double MaxRate
        {
            get { return _orignalData.MaxRate; }
        }

        /// <summary>
        /// The color to draw the AP
        /// </summary>
        public Color MyColor
        {
            get; set;
        }

        /// <summary>
        /// Historic access point information
        /// </summary>
        public List<NetworkData> MyNetworkDataCollection
        {
            get { return _networkData; }
        }

        /// <summary>
        /// Gets the network type of this AP
        /// </summary>
        public string NetworkType
        {
            get { return _orignalData.NetworkType; }
        }

        /// <summary>
        /// Details about 802.11n features of this AP
        /// </summary>
        public IeParser.TypeNSettings NSettings
        {
            get {return LastData.NSettings; }
        }

        /// <summary>
        /// The security mode supported by this AP
        /// </summary>
        public string Privacy
        {
            get { return _orignalData.Privacy; }
        }

        /// <summary>
        /// The basic(non-11N) rates supported by this AP
        /// </summary>
        public List<double> Rates
        {
            get { return _orignalData.Rates; }
        }

        /// <summary>
        /// Gets the RSSI (receive signal strength indication).
        /// </summary>        
        [DisplayName("Spark")]
        public int[] Spark
        {
            get
            {
                //Locking just in case
                lock (MyNetworkDataCollection)
                {
                    // Grab up to the first MaxDataPoint number of RSSI readings
                    int[] sparks = new int[Math.Min(_networkData.Count, MaxDataPoints)];

                    //TODO: Could we invert the loop to make it go backwards? Shouldn't that remove the sparks.Length - 1?
                    for (int i = 0; i < sparks.Length; i++)
                    {
                        sparks[sparks.Length - 1 - i] = _networkData[_networkData.Count - i - 1].Rssi;
                    }

                    return sparks;
                }
            }
        }

        /// <summary>
        /// The SSID of the AP
        /// </summary>
        public string Ssid
        {
            get { return LastData.Ssid; }
        }

        /// <summary>
        /// A slash delimited list of supported data rates
        /// </summary>
        public string SupportedRates
        {
            get { return LastData.SupportedRates; }
        }

        /// <summary>
        /// The last time this AP was updated
        /// </summary>
        public DateTime Timestamp
        {
            get; set;
        }

        /// <summary>
        /// The AP vendor
        /// </summary>
        public string Vendor
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public AccessPoint(NetworkData dataItem)
        {
            Index = _counter++;
            Graph = true;
            Highlight = false;
            _networkData = new List<NetworkData>(MaxDataPoints);
            _networkData.Add(dataItem);

            _orignalData = (NetworkData)dataItem.Clone();
            Timestamp = dataItem.MyTimestamp;
            LastSeenTimestamp = dataItem.MyTimestamp;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Clones and then adds data to this AP
        /// </summary>
        /// <param name="data"></param>
        public void AddData(NetworkData data)
        {
            if (data == null) throw new ArgumentNullException("data");
            lock (MyNetworkDataCollection)
            {
                MyNetworkDataCollection.Add((NetworkData) data.Clone());
            }
            Timestamp = data.MyTimestamp;

            //Remove old data, if any
            DeleteOldData(Timestamp - TimeSpan.FromMinutes(8));
        }

        /// <summary>
        /// Clones and then adds data to this AP
        /// </summary>
        /// <param name="data">The data to add</param>
        /// <param name="gpsData">The GPS data to go with</param>
        public void AddData(NetworkData data, GpsData gpsData)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (gpsData == null) throw new ArgumentNullException("gpsData");

            NetworkData nd2 = (NetworkData)data.Clone();

            lock (MyNetworkDataCollection)
            {
                //Check if the RSSI had been consistently stronger and stronger than the strongest reading before updating the GPS data
                //This means if this reading and the last reading are both higher than 2 readings ago, update the GPS data
                if ((MyNetworkDataCollection.Count > 1 && nd2.Rssi > MyNetworkDataCollection.Max(nd => nd.Rssi) &&
                     (nd2.Rssi > MyNetworkDataCollection[MyNetworkDataCollection.Count - 2].Rssi &&
                      MyNetworkDataCollection[MyNetworkDataCollection.Count - 1].Rssi >
                      MyNetworkDataCollection[MyNetworkDataCollection.Count - 2].Rssi)) ||
                    (nd2.Rssi > -100 && GpsData.SatelliteTime == DateTime.MinValue))
                {
                    //Update GPS data
                    GpsData = gpsData;
                }
                //Add the data
                MyNetworkDataCollection.Add(nd2);
            }
            Timestamp = nd2.MyTimestamp;
            LastSeenTimestamp = nd2.MyTimestamp;

            //Remove old data, if any
            DeleteOldData(Timestamp - TimeSpan.FromMinutes(8));
        }

        /// <summary>
        /// Adds filler(-100 rssi) data at the specified time
        /// </summary>
        /// <param name="time">The timestamp to add this data at</param>
        public void AddFiller(DateTime time)
        {
            lock (MyNetworkDataCollection)
            {
                MyNetworkDataCollection.Add(new NetworkData(time,
                                                            MacAddress.Bytes,
                                                            Privacy,
                                                            Ssid,
                                                            Channel,
                                                            LastData.Rssi,
                                                            0,
                                                            _orignalData.SupportedRates,
                                                            _orignalData.NetworkType,
                                                            (int) (time - LastSeenTimestamp).TotalSeconds));
            }
            Timestamp = time;
        }

        /// <summary>
        /// Removed data older than the date specified
        /// </summary>
        /// <param name="date">The oldest data to keep</param>
        public void DeleteOldData(DateTime date)
        {
            lock (MyNetworkDataCollection)
            {
                MyNetworkDataCollection.RemoveAll(nd => nd.MyTimestamp < date);
            }
        }

        public override bool Equals(object obj)
        {
            AccessPoint point = obj as AccessPoint;
            return ((point != null) && point.MyNetworkDataCollection.Equals(MyNetworkDataCollection));
        }

        public object[] GetData()
        {
            //if(ap.Index > 20)
            //    Console.WriteLine("Hi");
            return new object[]
                       {
                           Index,
                           Graph,
                           /*null,*/ //The signal indicator Image
                           MacAddress.ToString(),
                           Ssid,
                           Spark,
                           IsN && NSettings != null && NSettings.Is40MHz
                               ? NSettings.SecondaryChannelLower
                                     ? Channel + " + " + (Channel - 4)
                                     : Channel + " + " + (Channel + 4)
                               : Channel.ToString(),
                           Vendor,
                           Privacy,
                           MaxRate + (IsN ? " (N)" : ""),
                           NetworkType,
                           FirstSeenTimestamp.ToLongTimeString(),
                           LastSeenTimestamp.ToLongTimeString(),
                           GpsData.Latitude.ToString("F6"),
                           GpsData.Longitude.ToString("F6")
                       };
        }

        /// <summary>
        /// Returns network data up until the specified time
        /// </summary>
        /// <param name="time">The earliest time to get data</param>
        /// <returns></returns>
        public NetworkData[] GetDataUntilTime(DateTime time)
        {
            lock (MyNetworkDataCollection)
            {
                return MyNetworkDataCollection.Where(nd => nd.MyTimestamp > time).ToArray();
            }
        }

        public override int GetHashCode()
        {
            return MyNetworkDataCollection.First().GetHashCode();
        }

        /// <summary>
        /// Checks if this AP passes the filter
        /// </summary>
        /// <param name="f">The filter to test against</param>
        /// <returns>truf is it passed, otherwise false</returns>
        public bool Pass(Filter f)
        {
            return f.Eval(this);
        }

        #endregion Public Methods
    }
}