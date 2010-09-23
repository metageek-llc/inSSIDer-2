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
using System.ComponentModel;
using System.Linq;
using inSSIDer.FileIO;
using inSSIDer.Misc;
using ManagedWifi;
using MetaGeek.Gps;
using MetaGeek.WiFi;
using inSSIDer.Localization;
using System.Timers;

namespace inSSIDer.Scanning
{
    /// <summary>
    /// This class scans for and stores WiFi AP data
    /// </summary>
    public class Scanner : IDisposable
    {
        internal NetworkScanner NetworkScanner;
        internal NetworkDataCacheN Cache;
        internal GpsController GpsControl;
        internal GpxDataLogger Logger;

        //private WlanClient _wc;

        //public event EventHandler ScanStartEvent;
        //public event EventHandler ScanStopEvent;

        //public event EventHandler GpsLocationUpdated

        public event EventHandler<ScanCompleteEventArgs> ScanComplete;

        public bool Initalize(out Exception error)
        {
            error = null;
            NetworkScanner = new NetworkScanner();

            //Set new data handler
            NetworkScanner.NewNetworkDataEvent += NetworkScannerNewNetworkDataEvent;

            Cache = new NetworkDataCacheN();

            //GPS
            GpsControl = new GpsController();
            GpsControl.GpsUpdated += GpsControl_GpsUpdated;
            GpsControl.GpsTimeout += GpsControl_GpsTimeout;
            GpsControl.GpsError += GpsControl_GpsError;
            GpsControl.GpsLocationUpdated += GpsControl_GpsLocationUpdated;

            Logger = new GpxDataLogger { AutoSave = true, AutoSaveInterval = TimeSpan.FromSeconds(10) };

            //Null scanning
            _tNullScan.Elapsed += TNullScanElapsed;

            try
            {
                WlanClient = new WlanClient();
            }
            catch (Win32Exception exception)
            {
                error = exception;
                return false;
                //MessageBox.Show("Error Initializing Wlan Client: " + exception.Message + "\n\nWi-Fi data will not be displayed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (DllNotFoundException)
            {
                error = new Exception(Localizer.GetString("WlanapiNotFound"));
                return false;
                //MessageBox.Show("Error: wlanapi.dll could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            return true;
        }

        private void GpsControl_GpsLocationUpdated(object sender, EventArgs e)
        {
            //Console.WriteLine("GPS updated! - {0}, {1}", GpsControl.Latitude.ToString("F6"), GpsControl.Longitude.ToString("F6"));
        }

        private void GpsControl_GpsError(object sender, StringEventArgs e)
        {
            Console.WriteLine("GPS Error - {0}",e.Message);
        }

        private void GpsControl_GpsTimeout(object sender, EventArgs e)
        {
            Console.WriteLine("GPS timeout!");
        }

        private void GpsControl_GpsUpdated(object sender, EventArgs e)
        {
            //Console.WriteLine("GPS updated! - {0}, {1}", GpsControl.Latitude.ToString("F6"), GpsControl.Longitude.ToString("F6"));
            
        }

        private void StartScanning(WlanClient.WlanInterface intf, int scaninterval)
        {
            if (intf == null) return;
            NetworkScanner.MyWlanInterface = intf;
            NetworkScanner.Start(scaninterval);
            return;
        }

        public void StartScanning(int scaninterval)
        {
            StartScanning(WlanInterface, scaninterval);
            return;
        }

        /// <summary>
        /// Stop scanning
        /// </summary>
        public void StopScanning()
        {
            NetworkScanner.Stop();
        }

        /// <summary>
        /// The WiFi interface to scan with
        /// </summary>
        public WlanClient.WlanInterface WlanInterface { get; set; }

        /// <summary>
        /// Returns a list of avalible WiFi interfaces
        /// </summary>
        public WlanClient.WlanInterface[] AvalibleWlanInterfaces
        {
            get { return WlanClient.Interfaces; }
        }


        private void NetworkScannerNewNetworkDataEvent(object sender, IncomingDataEventArgs<NetworkData> e)
        {
            //Add data to the cache
            Cache.AddData(e.Data.ToArray(), GpsControl.GetCurrentGpsData());

            //Fire scan complete event
            OnScanComplete(e.Data.ToArray(), GpsControl.GetCurrentGpsData());
        }

        public WlanClient WlanClient { get; private set; }

        public bool SetInterface(string interfaceName)
        {
            bool status = false;
            foreach (WlanClient.WlanInterface intf in AvalibleWlanInterfaces)
            {
                if (intf.InterfaceDescription != interfaceName) continue;
                //We've found the interface
                WlanInterface = intf;
                status = true;
            }
            return status;
        }

        public bool SetInterface(Guid interfaceId)
        {
            bool status = false;
            foreach (WlanClient.WlanInterface intf in AvalibleWlanInterfaces)
            {
                if (intf.InterfaceGuid != interfaceId) continue;
                //We've found the interface
                WlanInterface = intf;
                status = true;
            }
            return status;
        }

        private void OnScanComplete(NetworkData[] data,GpsData gpsData)
        {
            if (ScanComplete != null) ScanComplete(this, new ScanCompleteEventArgs(data, gpsData));
        }

        private readonly Timer _tNullScan = new Timer(1000);
        private readonly List<NullNetData> _usedData = new List<NullNetData>();
        private const int numNull = 100;

        private readonly Random random = new Random();

        public void StartNullScanning()
        {
            _tNullScan.Start();
        }

        public void StopNullScanning()
        {
            _tNullScan.Stop();
        }

        private void TNullScanElapsed(object sender, ElapsedEventArgs e)
        {
           // _tNullScan.Interval = 750;
            List<NetworkData> networkDataList = new List<NetworkData>();

            if(_usedData.Count == 0)
            {
                for (int i = 0; i < numNull; i++)
                {
                    _usedData.Add(new NullNetData
                                      {
                                          Mac = GenerateFakeMacAddress(),
                                          Channel = (uint) random.Next(1, 14),
                                          Ssid = "Fake SSID",
                                          Rssi = random.Next(-90, -20)
                                      });
                }
                return;
            }

            for (int i = 0; i < numNull; i++)
            {
                NetworkData ndTemp = new NetworkData(DateTime.Now,
                                                     _usedData[i].Mac,
                                                     "RSNA-CCMP", 
                                                     _usedData[i].Ssid,
                                                     _usedData[i].Channel,
                                                     _usedData[i].Rssi,
                                                     50,
                                                     "1/2/5.5/6/12/24/36/48/54",
                                                     "Fake");
                //generate new rssi
                _usedData[i].Rssi = GenerateFakeRssi(_usedData[i].Rssi);
                networkDataList.Add(ndTemp);
            }

            //Add to cache
            Cache.AddData(networkDataList.ToArray(), GpsData.Empty);

            //Fire ScanComplete
            OnScanComplete(networkDataList.ToArray(), GpsData.Empty);
        }

        private byte[] GenerateFakeMacAddress()
        {
            byte[] outp = new byte[6];
            

            random.NextBytes(outp);
            outp[0] = 0x00;

            return outp;

        }

        private int GenerateFakeRssi(int lastRssi)
        {
            int rssi = lastRssi + random.Next(-4, 5);

            if (rssi < -95) rssi = -95;
            if (rssi > -20) rssi = -20;

            return rssi;
        }

        private class NullNetData
        {
            public byte[] Mac;
            public uint Channel;
            public string Ssid;
            public int Rssi;
        }

        #region GPX logging control

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Log.WriteLine("Stop _ns", "Scanner.Dispose()");
            NetworkScanner.Stop();
            Log.WriteLine("Dispose _ns", "Scanner.Dispose()");
            NetworkScanner.Dispose();
            Log.WriteLine("Null out WlanClient", "Scanner.Dispose()");
            WlanClient = null;
            Log.WriteLine("Null out Cache", "Scanner.Dispose()");
            Cache = null;
            Log.WriteLine("Stop GpsControl", "Scanner.Dispose()");
            GpsControl.Stop();
            Log.WriteLine("Null out GpsControl", "Scanner.Dispose()");
            GpsControl = null;
        }

        #endregion
    }

    public class ScanCompleteEventArgs : EventArgs
    {
        public ScanCompleteEventArgs(NetworkData[] data, GpsData gpsData)
        {
            Data = data;
            GpsData = gpsData;
        }

        public NetworkData[] Data { get; private set; }

        public GpsData GpsData { get; private set; }
    }
}
