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

using MetaGeek.Gps;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning
{
    public class ScanCompleteEventArgs : EventArgs
    {
        #region Properties

        public NetworkData[] Data
        {
            get; private set;
        }

        public GpsData GpsData
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public ScanCompleteEventArgs(NetworkData[] data, GpsData gpsData)
        {
            Data = data;
            GpsData = gpsData;
        }

        #endregion Constructors
    }
}