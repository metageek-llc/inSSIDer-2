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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using ManagedWifi;

namespace MetaGeek.WiFi
{
    public class NetworkData : ICloneable
    {
        #region Fields

        private double _maxRate;
        private string _networkType = string.Empty;
        private string _privacy = string.Empty;
        private string _ssid;
        private string _supportedRates;

        #endregion Fields

        #region Properties

        public int Age
        {
            get; private set;
        }

        public uint Channel
        {
            get; set;
        }

        public bool Connected
        {
            get; set;
        }

        public bool IsTypeN
        {
            get; set;
        }

        [DisplayName("Max Rate")]
        public double MaxRate
        {
            get
            {
                if ((Rates != null) && (Rates.Count > 0))
                {
                    return Rates[Rates.Count - 1];
                }
                return _maxRate;
            }
            internal set
            {
                _maxRate = value;
            }
        }

        [DisplayName("MAC Address")]
        public MacAddress MyMacAddress
        {
            get; internal set;
        }

        public DateTime MyTimestamp
        {
            get; set;
        }

        public string NetworkType
        {
            get
            {
                return _networkType;
            }
            set
            {
                if (value != null)
                    _networkType = value;
            }
        }

        public IeParser.TypeNSettings NSettings
        {
            get; set;
        }

        public string Privacy
        {
            get
            {
                return _privacy;
            }
            set
            {
                if (value != null)
                    _privacy = value;
            }
        }

        public List<double> Rates
        {
            get; internal set;
        }

        [DisplayName("RSSI")]
        public int Rssi
        {
            get; set;
        }

        [DisplayName("Signal Quality")]
        public uint SignalQuality
        {
            get; set;
        }

        [DisplayName("SSID")]
        public string Ssid
        {
            get
            {
                return _ssid;
            }
            set
            {
                _ssid = value.Equals("\0") ? "" : value;
            }
        }

        [DisplayName("Supported Rates")]
        public string SupportedRates
        {
            get
            {
                return (_supportedRates ?? (_supportedRates = BuildRateString()));
            }
            internal set
            {
                _supportedRates = value;
            }
        }

        #endregion Properties

        #region Constructors

        public NetworkData()
        {
            Rates = new List<double>();
            MyTimestamp = DateTime.Now;
            Connected = false;
        }

        public NetworkData(byte[] macAddress)
            : this()
        {
            MyMacAddress = new MacAddress(macAddress);
        }

        public NetworkData(DateTime timestamp, byte[] macAddress, string privacy, string ssid, uint channel, int rssi, uint signalQuality, string supportedRates, string networkType)
        {
            Rates = new List<double>();
            MyTimestamp = timestamp;
            MyMacAddress = new MacAddress(macAddress);

            Privacy = privacy;
            Ssid = ssid;
            Channel = channel;
            Rssi = rssi;
            SignalQuality = signalQuality;
            try
            {
                Rates = supportedRates.Split(new char[] { '/' }).ToList<string>().ConvertAll<double>(Convert.ToDouble);
            }
            catch (FormatException)
            {
                //Something went wrong
                Rates.Add(-1.0);
            }
            NetworkType = networkType;
            Age = 0;
        }

        public NetworkData(DateTime timestamp, byte[] macAddress, string privacy, string ssid, uint channel, int rssi,
            uint signalQuality, string supportedRates, string networkType, int age)
            : this(timestamp, macAddress, privacy, ssid, channel, rssi, signalQuality, supportedRates, networkType)
        {
            Age = age;
        }

        #endregion Constructors

        #region Public Methods

        public object Clone()
        {
            NetworkData data = new NetworkData(MyMacAddress.Bytes)
            {
                NetworkType = NetworkType,
                Privacy = Privacy,
                Rssi = Rssi,
                SignalQuality = SignalQuality,
                Ssid = Ssid,
                Channel = Channel,
                MyTimestamp = MyTimestamp,
                NSettings = NSettings,
                IsTypeN = IsTypeN,
                Connected = Connected
            };
            foreach (double num in Rates)
            {
                data.Rates.Add(num);
            }
            return data;
        }

        public override bool Equals(object obj)
        {
            NetworkData data = obj as NetworkData;
            if (data == null)
            {
                return false;
            }
            return data.MyMacAddress.Equals(MyMacAddress);
        }

        public override int GetHashCode()
        {
            return (MyMacAddress.GetHashCode() + Ssid.GetHashCode());
        }

        #endregion Public Methods

        #region Private Methods

        private string BuildRateString()
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            foreach (double num in Rates)
            {
                builder.Append(str);
                builder.Append(num);
                str = "/";
            }
            return builder.ToString();
        }

        #endregion Private Methods
    }
}