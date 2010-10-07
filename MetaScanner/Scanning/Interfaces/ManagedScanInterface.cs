using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using inSSIDer.Localization;
using inSSIDer.Misc;
using ManagedWifi;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning.Interfaces
{
    public class ManagedScanInterface : IScanningInterface
    {
        //private WlanClient _wlanClient;
        private WlanClient.WlanInterface _interface;

        public void Init(NetworkInterface wlanInterface, out Exception error)
        {
            error = null;
            _interface = null;
            

            Guid interfaceId = new Guid(wlanInterface.Id);
            //Translate the NetworkInterface to a WlanInterface
            foreach (WlanClient.WlanInterface wlan in InterfaceManager.Instance.WlanClient.Interfaces)
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

        public IEnumerable<NetworkData> GetNetworkData()
        {
            List<NetworkData> list = new List<NetworkData>();

            //If the interface is null, return nothing
            if(_interface == null) return list;

            IEnumerable<Wlan.WlanBssEntryN> networkBssList = _interface.GetNetworkBssList();
            IEnumerable<Wlan.WlanAvailableNetwork> availableNetworkList =
                _interface.GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles);
            if ((networkBssList != null) && (availableNetworkList != null))
            {
                //Get connected to AP
                Wlan.WlanAssociationAttributes connectedAP = new Wlan.WlanAssociationAttributes();
                //try
                //{
                    if (_interface.CurrentConnection.isState == Wlan.WlanInterfaceState.Connected)
                    {
                        connectedAP = _interface.CurrentConnection.wlanAssociationAttributes;
                    }
                //}
                //catch (Win32Exception) { /*Not connected*/ }


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

        public void ScanNetworks()
        {
            _interface.Scan();
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

        #region Events and triggers

        public event EventHandler ScanComplete;

        private void OnScanComplete()
        {
            if(ScanComplete == null) return;
            ScanComplete(this, EventArgs.Empty);
        }

        public event EventHandler InterfaceError;

        private void OnInterfaceError()
        {
            if (InterfaceError == null) return;
            InterfaceError(this, EventArgs.Empty);
        }

        #endregion
    }
}
