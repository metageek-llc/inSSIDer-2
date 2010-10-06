using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;
using inSSIDer.Misc;
using ManagedWifi;
using MetaGeek.IoctlNdis;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning.Interfaces
{
    public class NdisScanInterface : IScanningInterface
    {
        private IoctlNdis Ndis;
        private NetworkInterface _interface;

        private Timer ScanCompleteTimer = new Timer(1000) {AutoReset = false};

        public NdisScanInterface()
        {
            Ndis = new IoctlNdis();
            ScanCompleteTimer.Elapsed += ScanCompleteTimer_Elapsed;
        }

        public void Init(NetworkInterface wlanInterface, out Exception error)
        {
            error = null;

            _interface = wlanInterface;

            if (_interface == null)
            {
                error = new ArgumentException("Invalid wireless interface", "wlanInterface");
                return;
            }
        }

        public IEnumerable<NetworkData> GetNetworkData()
        {
            List<NetworkData> list = new List<NetworkData>();
            IEnumerable<NdisWlanBssidEx> exArray = Ndis.QueryBssidList(_interface);
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
                        if (foundNIes)
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
                    item.Privacy = Ndis.GetPrivacyString(ex);
                    item.NetworkType = Utilities.FindValueString(Utilities.InfrastructureText, (int)ex.InfrastructureMode);
                    list.Add(item);
                }
            }
            return list;
        }

        public void ScanNetworks()
        {
            if(!Ndis.Scan(_interface))
            {
                //There was a problem
                OnInterfaceError();
            }

            //Signal complete after a delay
            ScanCompleteTimer.Start();
        }

        #region Events and triggers

        public event EventHandler ScanComplete;

        private void OnScanComplete()
        {
            if (ScanComplete == null) return;
            ScanComplete(this, EventArgs.Empty);
        }


        private void ScanCompleteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnScanComplete();
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
