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
using System.Text;
using System.Threading;
using inSSIDer.Misc;
using ManagedWifi;
using MetaGeek.IoctlNdis;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning
{
    public class NetworkScanner : IDisposable
    {
        // Fields
        private IoctlNdis _myNdis;
        private readonly AutoResetEvent _myScanCompleteEvent = new AutoResetEvent(true);
        private readonly ManualResetEvent _myTerminateEvent = new ManualResetEvent(false);

        // Events
        public event EventHandler<IncomingDataEventArgs<NetworkData>> NewNetworkDataEvent;

        // Methods
        public NetworkScanner()
        {
            MyWaitHandleArray = new WaitHandle[] { MyTerminateEvent, MyScanCompleteEvent };
            if (Utilities.IsXp())
            {
                MyGetNetworkDataDelegate = new GetNetworkDataDelegate(GetXpNetworkData);
                MyScanNetworksDelegate = new ScanNetworksDelegate(ScanXpNetworks);
            }
            else
            {
                MyGetNetworkDataDelegate = new GetNetworkDataDelegate(GetNetworkData);
                MyScanNetworksDelegate = new ScanNetworksDelegate(ScanNetworks);
            }
        }

        public void Dispose()
        {
            if (IsScanning)
            {
                Stop();
            }
        }

        private static bool FindNetwork(string ssid, IEnumerable<Wlan.WlanAvailableNetwork> networks, ref Wlan.WlanAvailableNetwork foundNetwork)
        {
            if (networks != null)
            {
                foreach (Wlan.WlanAvailableNetwork network in networks)
                {
                    string str = Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, (int)network.dot11Ssid.SSIDLength);
                    if (!string.IsNullOrEmpty(str) && str.Equals(ssid))
                    {
                        foundNetwork = network;
                        return true;
                    }
                }
            }
            return false;
        }

        private IEnumerable<NetworkData> GetNetworkData()
        {
            List<NetworkData> list = new List<NetworkData>();
            IEnumerable<Wlan.WlanBssEntryN> networkBssList = MyWlanInterface.GetNetworkBssList();
            IEnumerable<Wlan.WlanAvailableNetwork> availableNetworkList =
                MyWlanInterface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles);
            if ((networkBssList != null) && (availableNetworkList != null))
            {
                Wlan.WlanAvailableNetwork foundNetwork = new Wlan.WlanAvailableNetwork();
                foreach (Wlan.WlanBssEntryN entry in networkBssList)
                {
                    string ssid = Encoding.ASCII.GetString(entry.BaseEntry.dot11Ssid.SSID, 0,
                                                           (int) entry.BaseEntry.dot11Ssid.SSIDLength);
                    if (FindNetwork(ssid, availableNetworkList, ref foundNetwork))
                    {
                        NetworkData item = new NetworkData(entry.BaseEntry.dot11Bssid);
                        
                        Utilities.ConvertToMbs(entry.BaseEntry.wlanRateSet.Rates, item.Rates);
                        if (entry.NSettings != null)
                        {
                            /*item.Rates.Add(IEParser.MCSSet.GetSpeed(entry.nSettings.maxMCS,
                                                                    entry.nSettings.ShortGI20MHz,
                                                                    entry.nSettings.ShortGI40MHz,
                                                                    entry.nSettings.Is40MHz));*/
                            item.NSettings = new IeParser.TypeNSettings(entry.NSettings);

                            //Add the extended 802.11N rates
                            item.Rates.AddRange(item.NSettings.Rates.Where(f => !item.Rates.Contains(f)));
                            item.Rates.Sort();
                        }

                        item.IsTypeN = entry.BaseEntry.dot11BssPhyType == Wlan.Dot11PhyType.Ht;
                        int num = Utilities.ComputeRssi(entry.BaseEntry.linkQuality);
                        item.Rssi = (entry.BaseEntry.rssi > num) ? entry.BaseEntry.rssi : num;
                        item.Ssid = ssid;
                        item.Channel = Utilities.ConvertToChannel(entry.BaseEntry.chCenterFrequency);
                        item.NetworkType = Utilities.ConvertToString(entry.BaseEntry.dot11BssType);
                        item.Privacy = Utilities.CreatePrivacyString(foundNetwork.dot11DefaultAuthAlgorithm,
                                                                     foundNetwork.dot11DefaultCipherAlgorithm);
                        item.SignalQuality = foundNetwork.wlanSignalQuality;
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private IEnumerable<NetworkData> GetXpNetworkData()
        {
            List<NetworkData> list = new List<NetworkData>();
            IEnumerable<NdisWlanBssidEx> exArray = MyNdis.QueryBssidList(MyWlanInterface.NetworkInterface);
            if (exArray != null)
            {
                foreach (NdisWlanBssidEx ex in exArray)
                {
                    NetworkData data2 = new NetworkData(ex.MacAddress);
                    data2.Channel = Utilities.ConvertToChannel(ex.Configuration.DSConfig);
                    NetworkData item = data2;
                    if ((ex.IELength <= ex.IEs.Length) && (ex.IELength > 28))
                    {
                        bool foundNIes = false;
                        for (int i = 0; i < (ex.IELength/* - 29*/); i++)
                        {
                            if (((ex.IEs[i] == 0x2D) && (ex.IEs[i + 1] == 26)) && ((ex.IEs[i + 28] == 0x3D) && (ex.IEs[(i + 28) + 1] == 0x16)))
                            {
                                foundNIes = true;
                                break;
                            }
                        }
                        if(foundNIes)
                        {
                            item.IsTypeN = true;
                            item.NSettings = IeParser.Parse(ex.IEs);
                            //item.Rates.Add(IEParser.MCSSet.GetSpeed(item.NSettings.maxMCS, item.NSettings.ShortGI20MHz,
                            //                                        item.NSettings.ShortGI40MHz, item.NSettings.Is40MHz));

                            //Add the extended 802.11N rates
                            item.Rates.AddRange(item.NSettings.Rates.Where(f => !item.Rates.Contains(f)));
                            item.Rates.Sort();
                        }
                    }
                    Utilities.ConvertToMbs(ex.SupportedRates, item.Rates, item.IsTypeN);
                    item.Rssi = ex.Rssi;
                    item.SignalQuality = 0;
                    string str = Encoding.ASCII.GetString(ex.Ssid, 0, (int)ex.SsidLength);
                    if (str != null)
                    {
                        str = str.Trim();
                    }
                    item.Ssid = str;
                    item.Privacy = MyNdis.GetPrivacyString(ex);
                    item.NetworkType = Utilities.FindValueString(Utilities.NetworkTypeText, (int)ex.NetworkTypeInUse);
                    list.Add(item);
                }
            }
            return list;
        }

        private void InvokeNewNetworkDataEvent(IEnumerable<NetworkData> dataList)
        {
            if (NewNetworkDataEvent != null)
            {
                NewNetworkDataEvent(this, new IncomingDataEventArgs<NetworkData>(dataList));
            }
        }

        private IEnumerable<NetworkData> ReadData()
        {
            IEnumerable<NetworkData> enumerable = new List<NetworkData>();
            if ((MyWlanInterface != null) && (MyWlanInterface.NetworkInterface != null))
            {
                lock (MyWlanInterface)
                {
                    enumerable = MyGetNetworkDataDelegate();
                }
            }
            return enumerable;
        }

        private void ScanNetworks()
        {
            MyWlanInterface.Scan();
        }

        private void ScanThreadFunc()
        {
            while (true)
            {
                //Scan speed regulation
                if(ScanInterval > 0) Thread.Sleep(ScanInterval);
                //Wait for either the terminate signal or the scan complete signal
                int num = WaitHandle.WaitAny(MyWaitHandleArray, 3000);
                if ((num != 258) && (num != 1))
                {
                    //Stop the scanning loop
                    break;
                }
                try
                {
                    IEnumerable<NetworkData> dataList = ReadData();
                    InvokeNewNetworkDataEvent(dataList);
                    lock (MyWlanInterface)
                    {
                        MyScanNetworksDelegate();
                    }
                }
                catch (Win32Exception)
                {
                }
            }
        }

        private void ScanXpNetworks()
        {
            MyNdis.Scan(MyWlanInterface.NetworkInterface);
        }

        public void Start()
        {
            Start(0);
        }

        public void Start(int interval)
        {
            ScanInterval = interval;
            MyWlanInterface.WlanNotification += WlanApi_WlanNotification;
            MyScannerThread = new Thread(ScanThreadFunc);
            MyScannerThread.Start();
            IsScanning = true;
        }

        public void Stop()
        {
            if (IsScanning)
            {
                MyWlanInterface.WlanNotification -= WlanApi_WlanNotification;
                if (MyScannerThread != null)
                {
                    _myTerminateEvent.Set();
                    if (!MyScannerThread.Join(1000))
                    {
                        MyScannerThread.Abort();
                    }
                    _myTerminateEvent.Reset();
                    MyScannerThread = null;
                }
                IsScanning = false;
            }
        }

        private void WlanApi_WlanNotification(Wlan.WlanNotificationData notifyData)
        {
            lock (this)
            {
                if (notifyData.notificationSource == Wlan.WlanNotificationSource.Acm)
                {
                    switch (((Wlan.WlanNotificationCodeAcm)notifyData.NotificationCode))
                    {
                        case Wlan.WlanNotificationCodeAcm.ScanComplete:
                        case Wlan.WlanNotificationCodeAcm.ScanFail:
                            MyScanCompleteEvent.Set();
                            break;
                    }
                }
            }
        }

        // Properties
        public bool IsScanning { get; private set; }

        private GetNetworkDataDelegate MyGetNetworkDataDelegate { get; set; }

        private IoctlNdis MyNdis
        {
            get
            {
                return (_myNdis ?? (_myNdis = new IoctlNdis()));
            }
        }

        private AutoResetEvent MyScanCompleteEvent
        {
            get
            {
                return _myScanCompleteEvent;
            }
        }

        private Thread MyScannerThread { get; set; }

        private ScanNetworksDelegate MyScanNetworksDelegate { get; set; }

        private ManualResetEvent MyTerminateEvent
        {
            get
            {
                return _myTerminateEvent;
            }
        }

        private WaitHandle[] MyWaitHandleArray { get; set; }

        public WlanClient.WlanInterface MyWlanInterface { get; set; }

        private int ScanInterval { get; set; }

        // Nested Types
        private delegate IEnumerable<NetworkData> GetNetworkDataDelegate();

        private delegate void ScanNetworksDelegate();
    }

}
