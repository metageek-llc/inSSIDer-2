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
using System.Text;
using inSSIDer.Misc;
using ManagedWifi;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning.Interfaces
{
    public class ManagedScanInterface : IScanningInterface
    {
        #region Fields

        //private WlanClient _wlanClient;
        private WlanInterface _interface;

        #endregion Fields

        #region Events

        public event EventHandler InterfaceError;

        public event EventHandler ScanComplete;

        #endregion Events

        #region Public Methods

        public IEnumerable<NetworkData> GetNetworkData()
        {
            List<NetworkData> list = new List<NetworkData>();

            //If the interface is null, return nothing
            if(_interface == null) return list;

            IEnumerable<Wlan.WlanBssEntryN> networkBssList = null;
            IEnumerable<Wlan.WlanAvailableNetwork> availableNetworkList = null;

            try
            {
                networkBssList = _interface.GetNetworkBssList();
                availableNetworkList = _interface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles);
            }
            catch (NullReferenceException)
            {
                //Hopefully the call will succeed next time.
            }
            if ((networkBssList != null) && (availableNetworkList != null))
            {
                //Get connected to AP
                Wlan.WlanAssociationAttributes connectedAP = new Wlan.WlanAssociationAttributes();
                try
                {
                    if (_interface.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected)
                    {
                        connectedAP = _interface.CurrentConnection.wlanAssociationAttributes;
                    }
                }
                catch (NullReferenceException) { /*Hopefully it will work next time*/ }

                Wlan.WlanAvailableNetwork foundNetwork = new Wlan.WlanAvailableNetwork();
                foreach (Wlan.WlanBssEntryN entry in networkBssList)
                {
                    string ssid = Encoding.ASCII.GetString(entry.BaseEntry.dot11Ssid.SSID, 0,
                                                           (int)entry.BaseEntry.dot11Ssid.SSIDLength);
                    if (FindNetwork(ssid, availableNetworkList, ref foundNetwork))
                    {

                        NetworkData item = new NetworkData(entry.BaseEntry.dot11Bssid);

                        Utilities.ConvertToMbs(entry.BaseEntry.wlanRateSet.Rates, item.Rates);
                        if (entry.NSettings != null)
                        {
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

                        //Check to see if this AP is the connected one
                        item.Connected = item.MyMacAddress.CompareToPhysicalAddress(connectedAP.Dot11Bssid);

                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public void Init(NetworkInterface wlanInterface, out Exception error)
        {
            error = null;
            _interface = null;

            Guid interfaceId = new Guid(wlanInterface.Id);
            //Translate the NetworkInterface to a WlanInterface
            foreach (WlanInterface wlan in InterfaceManager.Instance.WlanClient.Interfaces)
            {
                if (!wlan.InterfaceGuid.Equals(interfaceId)) continue;

                _interface = wlan;
                break;
            }
            if(_interface == null)
            {
                error = new ArgumentException("Invalid wireless interface", "wlanInterface");
                return;
            }

            _interface.WlanNotification += WlanApi_WlanNotification;
        }

        public void ScanNetworks()
        {
            try
            {
                _interface.Scan();
            }
            catch (NullReferenceException)
            {
                // This shouldn't be throwing an exception, but it can sometimes.
                // _interface should always be initialized by the time this call is made
                // Init() is called before the scan loop is started and will not start scanning if the interface is null
            }
        }

        #endregion Public Methods

        #region Private Methods

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

        private void OnInterfaceError()
        {
            if (InterfaceError == null) return;
            InterfaceError(this, EventArgs.Empty);
        }

        private void OnScanComplete()
        {
            if(ScanComplete == null) return;
            ScanComplete(this, EventArgs.Empty);
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
                            OnScanComplete();
                            break;
                    }
                }
            }
        }

        #endregion Private Methods
    }
}