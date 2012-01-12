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
using System.Net.NetworkInformation;
using System.Timers;
using inSSIDer.FileIO;
using inSSIDer.Misc;
using inSSIDer.Properties;
using MetaGeek.Filters.Controllers;
using MetaGeek.Gps;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning
{
    /// <summary>
    /// This class scans for and stores WiFi AP data
    /// </summary>
    public class ScanController : IDisposable
    {
        #region Fields

        internal NetworkDataCacheN Cache;
        internal GpsController GpsControl;
        internal GpxDataLogger Logger;
        internal NetworkScannerN NetworkScanner;
        private const int numNull = 100;
        private readonly Random random = new Random();
        private NetworkInterface _interface;
        private readonly Timer _tNullScan = new Timer(1000);
        private readonly List<NullNetData> _usedData = new List<NullNetData>();
        private IEnumerable<NetworkData> _networkData;

        #endregion Fields

        #region Properties

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

        #endregion Properties

        #region Events

        public event EventHandler<ScanCompleteEventArgs> ScanComplete;

        #endregion Events

        #region Dispose

        public void Dispose()
        {
            //Un-hook the event
            //NetworkScanner.NewNetworkDataEvent -= NetworkScannerNewNetworkDataEvent;

            Log.WriteLine("Stop _ns", "Scanner.Dispose()");
            NetworkScanner.Stop();
            Log.WriteLine("Dispose _ns", "Scanner.Dispose()");
            NetworkScanner.Dispose();
            Log.WriteLine("Null out Cache", "Scanner.Dispose()");
            Cache.Dispose();
            Cache = null;
            Log.WriteLine("Stop GpsControl", "Scanner.Dispose()");
            GpsControl.Stop();
            Log.WriteLine("Null out GpsControl", "Scanner.Dispose()");
            GpsControl = null;
        }

        #endregion Dispose

        #region Public Methods

        public bool Initialize(out Exception error)
        {
            error = null;
            NetworkScanner = new NetworkScannerN();

            //Set new data handler
            NetworkScanner.NewNetworkDataEvent += NetworkScannerNewNetworkDataEvent;


            //GPS
            GpsControl = new GpsController();

            Logger = new GpxDataLogger { AutoSave = true, AutoSaveInterval = TimeSpan.FromSeconds(10) };

            //Null scanning
            _tNullScan.Elapsed += TNullScanElapsed;

            //Init the interface manager
            InterfaceManager.Instance.Init(out error);

            return true;
        }

        public bool SetInterface(string interfaceName)
        {
            bool status = false;
            foreach (NetworkInterface intf in InterfaceManager.Instance.Interfaces)
            {
                if (!intf.Description.Equals(interfaceName, StringComparison.InvariantCultureIgnoreCase)) continue;
                //We've found the interface
                Interface = intf;
                status = true;
                break;
            }
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

        public void StartNullScanning()
        {
            _tNullScan.Start();
        }

        public void StartScanning()
        {
            StartScanning(Interface);
        }

        public void StopNullScanning()
        {
            _tNullScan.Stop();
        }

        /// <summary>
        /// Stop scanning
        /// </summary>
        public void StopScanning()
        {
            NetworkScanner.Stop();
        }

        #endregion Public Methods

        #region Private Methods

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

        private void NetworkScannerNewNetworkDataEvent(object sender, IncomingDataEventArgs<NetworkData> e)
        {
            if (e.Data == null || Cache == null || GpsControl == null) return;

            //Add data to the cache
            _networkData = e.Data;
            Cache.AddData(e.Data.ToArray(), GpsControl.GetCurrentGpsData());
            //Fire scan complete event
            OnScanComplete(_networkData, GpsControl.GetCurrentGpsData());
        }

        private class NullNetData
        {
            public byte[] Mac;
            public uint Channel;
            public string Ssid;
            public int Rssi;
        }

        private void OnScanComplete(IEnumerable<NetworkData> data, GpsData gpsData)
        {
            if (ScanComplete != null)
            {
                ScanComplete(this, new ScanCompleteEventArgs(data.ToArray(), gpsData));
            }
        }


        private void StartScanning(NetworkInterface intf)
        {
            if (intf == null) return;
            NetworkScanner.NetworkInterface = intf;
            NetworkScanner.Start();
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
                                                     "WPA2-Enterprise",
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
            OnScanComplete(networkDataList.ToArray(),GpsData.Empty);
        }

        #endregion Private Methods

        public void InitializeCache(FiltersViewController<AccessPoint> filtersViewController)
        {
            filtersViewController.ItsSsidPropertyString = "Ssid";
            filtersViewController.ItsPrivacyPropertyString = "Security";
            filtersViewController.ItsChannelPropertyString = "Channel";
            Cache = new NetworkDataCacheN {ItsFilterViewController = filtersViewController};
            Cache.Initialize();
        }
    }
}