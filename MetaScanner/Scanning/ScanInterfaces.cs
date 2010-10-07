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
