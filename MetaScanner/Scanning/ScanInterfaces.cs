using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaGeek.WiFi;
using System.Net.NetworkInformation;

namespace inSSIDer.Scanning
{
    public interface IScanningInterface
    {
        void Init(NetworkInterface wlanInterface,out Exception error);

        /// <summary>
        /// Returns scanned network data
        /// </summary>
        /// <returns>A list of NetworkData objects scanned</returns>
        IEnumerable<NetworkData> GetNetworkData();

        /// <summary>
        /// Starts a scan
        /// </summary>
        void ScanNetworks();

        /// <summary>
        /// Fires when the scan is complete
        /// </summary>
        event EventHandler ScanComplete;

        /// <summary>
        /// Fires when an invalid interface is selected
        /// </summary>
        event EventHandler InterfaceError;
    }
}
