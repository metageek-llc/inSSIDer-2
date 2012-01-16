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
using FilterFramework;
using MetaGeek.Filters.Controllers;
using inSSIDer.Misc;

using MetaGeek.Gps;
using MetaGeek.WiFi;

namespace inSSIDer.Scanning
{
    public class NetworkDataCacheN : IDisposable
    {
        #region Fields

        private readonly AdapterVendors _av;
        private FilterHandler<AccessPoint> _filterHandler { get; set; }

        //This is a list of AccessPointN2 objects
        private readonly Dictionary<MacAddress, AccessPoint> _cache = new Dictionary<MacAddress, AccessPoint>();
        private bool _refreshColors;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Get the number of APs that pass filtering
        /// </summary>
        public int Count
        {
            get { return GetAccessPoints().Length; }
        }


        /// <summary>
        /// The newest timestamp in the cache
        /// </summary>
        public DateTime NewestTimestamp
        {
            get
            {
                lock (_cache)
                {
                    return _cache.Count > 0
                               ? DateTime.FromFileTime(_cache.Values.Max(ap => ap.Timestamp.ToFileTime()))
                               : DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// Get the total number of APs in the cache
        /// </summary>
        public int TotalCount
        {
            get { return _cache.Count; }
        }

        #endregion Properties

        #region Events

        public event EventHandler DataReset;

        #endregion Events

        #region Constructors

        public NetworkDataCacheN()
        {
            _av = new AdapterVendors();
        }

        private AccessPoint[] GetFilteredNetworkData(IEnumerable<AccessPoint> data)
        {

            lock(_filterHandler)
            {
                if(_filterHandler == null || !_filterHandler.HasFilters())
                    return data.ToArray();

                var filteredData = _filterHandler.ApplyFilter(data).ToArray();
                if (filteredData.Length < 1)
                    return filteredData;

                if(_refreshColors)
                {
                    Utilities.ResetColor();
                    foreach (var accessPoint in filteredData)
                    {
                        accessPoint.MyColor = Utilities.GetColor();
                    }
                    _refreshColors = false;
                }
                return filteredData;
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Adds data to the cache, and filler(-100 rssi) data if the AP isn't in the data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="gpsData"></param>
        public void AddData(NetworkData[] data, GpsData gpsData)
        {
            if (data == null || data.Length < 1) return;

            //Keep a tally of the APs that weren't here.
            List<MacAddress> missing = new List<MacAddress>();
            missing.AddRange(_cache.Keys);

            lock (_cache)
            {
                //Loop through all supplied data and add or update accesspoints
                foreach (NetworkData n2 in data)
                {
                    //Check if the cache contains this AP already
                    if (_cache.ContainsKey(n2.MyMacAddress))
                    {
                        //It does, update it!
                        GetAccessPointByMacAddress(n2.MyMacAddress).AddData(n2, gpsData);
                        //The AP was here, remove it from the missing list
                        missing.Remove(n2.MyMacAddress);
                    }
                    else
                    {
                        //nope, never heard of it. Add it!
                        _cache.Add(n2.MyMacAddress, new AccessPoint(n2)
                                                        {
                                                            MyColor = Utilities.GetColor(),
                                                            Vendor = _av.GetVendor(n2.MyMacAddress),
                                                            GpsData = gpsData
                                                        });

                    }
                }
            }

            //Add filler data for all absent APs
            foreach (MacAddress mac in missing)
            {
                GetAccessPointByMacAddress(mac).AddFiller(
                    DateTime.FromFileTime(data.Max(nd => nd.MyTimestamp.ToFileTime())));
            }
        }

        public FiltersViewController<AccessPoint> ItsFilterViewController { get; set; }
        public void Initialize()
        {
            //OUI lookup
            _av.LoadFromOui();
            _filterHandler = ItsFilterViewController.ItsFilterHandler;
            HookUpEvents();
        }

        private void UnHookEvents()
        {
            ItsFilterViewController.FiltersUpdatedEvent.ItsEvent -= FiltersViewController_FiltersUpdatedEvent;
        }

        private void HookUpEvents()
        {
            ItsFilterViewController.FiltersUpdatedEvent.ItsEvent += FiltersViewController_FiltersUpdatedEvent;
        }
        /// <summary>
        /// Erases ALL data stored in the cache
        /// </summary>
        public void Clear()
        {
            lock (_cache)
            {
                _cache.Clear();
                OnDataReset();
            }
        }

        private void FiltersViewController_FiltersUpdatedEvent(object sender, EventArgs e)
        {
            lock(_cache)
            {
                _cache.Clear();
                _refreshColors = true;
            }
        }
        /// <summary>
        /// Gets an AP by its ID number
        /// </summary>
        /// <param name="id">The ID of the AP to look for</param>
        /// <returns></returns>
        public AccessPoint GetAccessPointById(long id)
        {
            lock (_cache)
            {
                return _cache.Values.FirstOrDefault(ap => ap.Index == id);
            }
        }

        /// <summary>
        /// Gets an AP by its MAC address in string format
        /// </summary>
        /// <param name="mac">The MAC address of the AP to look for</param>
        /// <returns></returns>
        public AccessPoint GetAccessPointByMacAddress(string mac)
        {
            lock (_cache)
            {
                return _cache.Values.FirstOrDefault(ap => ap.MacAddress.ToString().ToLower().Equals(mac.ToLower()));
            }
        }

        /// <summary>
        /// Get all of the APs stored in the cache that pass the filters
        /// </summary>
        /// <returns></returns>
        public AccessPoint[] GetAccessPoints()
        {
            lock (_cache)
            {
                var v = GetFilteredNetworkData(_cache.Values);
                return v;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Gets an AP by its MAC address
        /// </summary>
        /// <param name="mac">The MAC address of the AP to look for</param>
        /// <returns></returns>
        private AccessPoint GetAccessPointByMacAddress(MacAddress mac)
        {
            lock (_cache)
            {
                return _cache.Values.FirstOrDefault(ap => ap.MacAddress.Equals(mac));
            }
        }

        /// <summary>
        /// Fire the DataReset event if it's hooked
        /// </summary>
        private void OnDataReset()
        {
            if (DataReset != null) DataReset(this, EventArgs.Empty);
        }

        #endregion Private Methods

        public void Dispose()
        {
            UnHookEvents();
        }
    }
}