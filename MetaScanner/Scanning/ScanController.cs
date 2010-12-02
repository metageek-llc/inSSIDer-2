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
using inSSIDer.FileIO;
using inSSIDer.Misc;
using MetaGeek.Gps;
using MetaGeek.WiFi;
using System.Timers;
using System.Net.NetworkInformation;
using inSSIDer.Properties;

namespace inSSIDer.Scanning
{
    /// <summary>
    /// This class scans for and stores WiFi AP data
    /// </summary>
    public class ScanController : IDisposable
    {
        internal NetworkScannerN NetworkScanner;
        internal NetworkDataCacheN Cache;
        internal GpsController GpsControl;
        internal GpxDataLogger Logger;

        private NetworkInterface _interface;


        public event EventHandler<ScanCompleteEventArgs> ScanComplete;

        public bool Initalize(out Exception error)
        {
            error = null;
            NetworkScanner = new NetworkScannerN();

            //Set new data handler
            NetworkScanner.NewNetworkDataEvent += NetworkScannerNewNetworkDataEvent;

            Cache = new NetworkDataCacheN();

            //GPS
            GpsControl = new GpsController();

            Logger = new GpxDataLogger { AutoSave = true, AutoSaveInterval = TimeSpan.FromSeconds(10) };

            //Null scanning
            _tNullScan.Elapsed += TNullScanElapsed;

            //Init the interface manager
            InterfaceManager.Instance.Init(out error);

            return true;
        }

        private void StartScanning(NetworkInterface intf)
        {
            if (intf == null) return;
            NetworkScanner.NetworkInterface = intf;
            NetworkScanner.Start();
        }

        public void StartScanning()
        {
            StartScanning(Interface);
        }

        /// <summary>
        /// Stop scanning
        /// </summary>
        public void StopScanning()
        {
            NetworkScanner.Stop();
        }

        /// <summary>
        /// The interface to scan with
        /// </summary>
        public NetworkInterface Interface
        {
            get { return _interface; }
            set
            {
                _interface = value;

                if (_interface == null)
                {
                    Settings.Default.scanLastInterfaceId = Guid.Empty;
                    return;
                }

                //Set the last used interface
                Settings.Default.scanLastInterfaceId = new Guid(_interface.Id);
            }
        }


        private void NetworkScannerNewNetworkDataEvent(object sender, IncomingDataEventArgs<NetworkData> e)
        {
            if (e.Data == null || Cache == null || GpsControl == null) return;

            //Add data to the cache
            Cache.AddData(e.Data.ToArray(), GpsControl.GetCurrentGpsData());

            //Fire scan complete event
            OnScanComplete(e.Data.ToArray(), GpsControl.GetCurrentGpsData());
        }

        public bool SetInterface(string interfaceName)
        {
            Log.WriteLine("SetInterface()", "ScanController.SetInterface()");
            bool status = false;
            Log.WriteLine("Loop through all interfaces:", "ScanController.SetInterface()");
            foreach (NetworkInterface intf in InterfaceManager.Instance.Interfaces)
            {
                Log.WriteLine(intf.Description, "ScanController.SetInterface()");
                if (intf.Description.Equals(interfaceName, StringComparison.InvariantCultureIgnoreCase)) continue;
                Log.WriteLine("    Found it", "ScanController.SetInterface()");
                //We've found the interface
                Interface = intf;
                status = true;
                break;
            }
            Log.WriteLine("Return status:", "ScanController.SetInterface()");
            Log.WriteLine(status, "ScanController.SetInterface()");
            return status;
        }

        public bool SetInterface(Guid interfaceId)
        {
            bool status = false;
            foreach (NetworkInterface intf in InterfaceManager.Instance.Interfaces)
            {
                if (new Guid(intf.Id) != interfaceId) continue;
                //We've found the interface
                Interface = intf;
                status = true;
            }
            return status;
        }

        private void OnScanComplete(NetworkData[] data,GpsData gpsData)
        {
            if (ScanComplete != null) ScanComplete(this, new ScanCompleteEventArgs(data, gpsData));
        }

        #region Null scanning

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

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //Un-hook the event 
            //NetworkScanner.NewNetworkDataEvent -= NetworkScannerNewNetworkDataEvent;

            Log.WriteLine("Stop _ns", "Scanner.Dispose()");
            NetworkScanner.Stop();
            Log.WriteLine("Dispose _ns", "Scanner.Dispose()");
            NetworkScanner.Dispose();
            Log.WriteLine("Null out Cache", "Scanner.Dispose()");
            Cache = null;
            Log.WriteLine("Stop GpsControl", "Scanner.Dispose()");
            GpsControl.Stop();
            Log.WriteLine("Null out GpsControl", "Scanner.Dispose()");
            GpsControl = null;
        }

        #endregion
    }
}
