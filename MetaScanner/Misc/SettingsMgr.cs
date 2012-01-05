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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using inSSIDer.Properties;
using inSSIDer.UI.Controls;
using inSSIDer.UI.Mini;

using MetaGeek.Gps;
using MetaGeek.WiFi;

namespace inSSIDer
{
    public static class SettingsMgr
    {
        #region Properties

        public static bool LastMini
        {
            get { return Settings.Default.lastMini; }
            set { Settings.Default.lastMini = value; }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Applies stored settings to the GPS Controller
        /// </summary>
        /// <param name="gps">The GPS Controler to apply settings to</param>
        public static void ApplyGpsSettings(GpsController gps)
        {
            try
            {
                gps.PortName = Settings.Default.gpsSerialPort;
                gps.PortBaudrate = Settings.Default.gpsBaud;
                gps.PortParity = Settings.Default.gpsParity;
                gps.PortHandshake = Settings.Default.gpsHandshake;
                gps.PortDataBits = Settings.Default.gpsDataBits;
                gps.PortStopBits = Settings.Default.gpsStopBits;

                //Start GPS if it was last time
                if (Settings.Default.gpsEnabled && !string.IsNullOrEmpty(gps.PortName))
                {
                    gps.Start();
                }
            }
            catch (Exception)
            {
                //Something went wrong applying settings, ignore
            }
        }

        /// <summary>
        /// Applies saved settings to the main form
        /// </summary>
        /// <param name="form">The main form</param>
        public static void ApplyMainFormSettings(Form form)
        {
            if (form == null) return;

            // Window state, size and position
            form.WindowState = Settings.Default.formWindowState;
            form.StartPosition = FormStartPosition.Manual;

            Rectangle windowArea = new Rectangle(Settings.Default.formLocation, Settings.Default.formSize);
            Rectangle desktopArea = Screen.GetWorkingArea(windowArea);

            if (!desktopArea.Contains(windowArea))
            {
                if (windowArea.Height > desktopArea.Height)
                    windowArea.Height = desktopArea.Height;

                if (windowArea.Width > desktopArea.Width)
                    windowArea.Width = desktopArea.Width;

                if (windowArea.Left < desktopArea.Left || windowArea.Right > desktopArea.Right)
                    windowArea.X = desktopArea.Left + ((desktopArea.Width - windowArea.Width) / 2);

                if (windowArea.Top < desktopArea.Top || windowArea.Bottom > desktopArea.Bottom)
                    windowArea.Y = desktopArea.Top + ((desktopArea.Height - windowArea.Height) / 2);
            }
            form.DesktopBounds = windowArea;
        }

        /// <summary>
        /// Applies saved settings to the mini form
        /// </summary>
        /// <param name="form">The mini form</param>
        public static void ApplyMiniFormSettings(Form form)
        {
            if (form == null) return;

            // Window state, size and position
            form.WindowState = Settings.Default.miniWindowState;
            form.StartPosition = FormStartPosition.Manual;

            Rectangle windowArea = new Rectangle(Settings.Default.miniLocation, Settings.Default.miniSize);
            Rectangle desktopArea = Screen.GetWorkingArea(windowArea);

            if (!desktopArea.Contains(windowArea))
            {
                if (windowArea.Height > desktopArea.Height)
                    windowArea.Height = desktopArea.Height;

                if (windowArea.Width > desktopArea.Width)
                    windowArea.Width = desktopArea.Width;

                if (windowArea.Left < desktopArea.Left || windowArea.Right > desktopArea.Right)
                    windowArea.X = desktopArea.Left + ((desktopArea.Width - windowArea.Width) / 2);

                if (windowArea.Top < desktopArea.Top || windowArea.Bottom > desktopArea.Bottom)
                    windowArea.Y = desktopArea.Top + ((desktopArea.Height - windowArea.Height) / 2);
            }
            form.DesktopBounds = windowArea;
        }

        /// <summary>
        /// Applies saved settings to the ScannerViewMini
        /// </summary>
        /// <param name="view">The ScannerViewMini</param>
        public static void ApplyScannerViewMiniSettings(ScannerViewMini view)
        {
            if (string.IsNullOrEmpty(Settings.Default.miniGridOrder)) return;
            try
            {
                //The string is like this:
                //<ColumnName>,<index>,<visible>|<ColumnName>,<index>,<visible>|<ColumnName>,<index>,<visible>|etc.
                string[] parts;
                foreach (string piece in Settings.Default.miniGridOrder.Split('|'))
                {
                    parts = piece.Split(',');
                    view.scannerGrid.Columns[parts[0]].DisplayIndex = Convert.ToInt32(parts[1]);
                    view.scannerGrid.Columns[parts[0]].Visible = parts[2] == "True";
                }
            }
            catch (NullReferenceException)
            {
                //Something went wrong, ignore
            }
            catch (IndexOutOfRangeException)
            {
                //Something went wrong, ignore
            }
            catch (FormatException)
            {
                //Something went wrong, ignore
            }
            //refresh the context menu
            view.UpdateColumnList();
        }

        /// <summary>
        /// Applies saved settings to the ScannerView
        /// </summary>
        /// <param name="view">The ScannerView</param>
        public static void ApplyScannerViewSettings(ScannerView view)
        {
            if(string.IsNullOrEmpty(Settings.Default.gridOrder)) return;
            try
            {
                //The string is like this:
                //<ColumnName>,<index>,<visible>|<ColumnName>,<index>,<visible>|<ColumnName>,<index>,<visible>|etc.
                string[] parts;
                foreach (string piece in Settings.Default.gridOrder.Split('|'))
                {
                    parts = piece.Split(',');
                    if (view != null)
                    {
                        view.scannerGrid.Columns[parts[0]].DisplayIndex = Convert.ToInt32(parts[1]);
                        view.scannerGrid.Columns[parts[0]].Visible = parts[2] == "True";
                    }
                }
            }
            catch (NullReferenceException)
            {
                //Something went wrong, ignore
            }
            catch (IndexOutOfRangeException)
            {
                //Something went wrong, ignore
            }
            catch (FormatException)
            {
                //Something went wrong, ignore
            }
            //refresh the context menu
            if (view != null) view.UpdateColumnList();
        }

        public static bool CheckSettingsSystem()
        {
            try
            {
                //Try settings get
                string setting = Settings.Default.settingsTest;
                //Then set
                Settings.Default.settingsTest = "OK";
                //If we haven't exceptioned yet, return true
                return true;
            }
            catch //(ConfigurationErrorsException) <-- we can't access this type
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the list of filters from settings
        /// </summary>
        /// <returns>The list of last use filters</returns>
        public static Filter[] GetFilterList()
        {
            if(string.IsNullOrEmpty(Settings.Default.lastFilters)) return new Filter[0];
            //The format is:
            //<filterExpr>|<filterExpr>|<filterExpr>|etc.

            try
            {
                string[] parts = Settings.Default.lastFilters.Split('|');
                List<Filter> filters = new List<Filter>();
                foreach (string s in parts)
                {
                    filters.Add(new Filter(s));
                }
                return filters.ToArray();
            }
            catch
            {
                return new Filter[0];
            }
        }

        /// <summary>
        /// Saves a list of filters to the last filters list
        /// </summary>
        /// <param name="filters">The filters to save</param>
        public static void SaveFilterList(Filter[] filters)
        {
            if (filters != null && filters.Length > 0)
            {
                StringBuilder sbOut = new StringBuilder();
                //The format is:
                //<filterExpr>|<filterExpr>|<filterExpr>|etc.
                try
                {
                    string pipe = "";
                    foreach (Filter f in filters)
                    {
                        sbOut.Append(pipe);
                        sbOut.Append(f.ToString());
                        pipe = "|";
                    }
                }
                catch (Exception)
                {
                    //No nothing.
                }
                Settings.Default.lastFilters = sbOut.ToString();
            }
            else
            {
                //Clear the last filters
                Settings.Default.lastFilters = string.Empty;
            }
        }

        /// <summary>
        /// Saves settings from GPS Controller
        /// </summary>
        /// <param name="gps">The GPS Controler save settings for</param>
        public static void SaveGpsSettings(GpsController gps)
        {
            try
            {
                Settings.Default.gpsEnabled = gps.Enabled;
                if (!string.IsNullOrEmpty(gps.PortName))
                {
                    Settings.Default.gpsSerialPort = gps.PortName;
                }
                Settings.Default.gpsBaud = gps.PortBaudrate;
                Settings.Default.gpsParity = gps.PortParity;
                Settings.Default.gpsHandshake = gps.PortHandshake;
                Settings.Default.gpsDataBits = gps.PortDataBits;
                Settings.Default.gpsStopBits = gps.PortStopBits;
                Settings.Default.Save();
            }
            catch (Exception)
            {
                //Something went wrong saving settings, ignore
            }
        }

        /// <summary>
        /// Saves the settings of the ScannerViewMini
        /// </summary>
        /// <param name="view">The ScannerViewMini</param>
        public static void SaveScannerViewMiniSettings(ScannerViewMini view)
        {
            try
            {
                StringBuilder sbOut = new StringBuilder();
                string pipe = "";
                foreach (DataGridViewColumn col in view.scannerGrid.Columns)
                {
                    //Add the column name and order
                    sbOut.Append(pipe);
                    sbOut.Append(col.Name);
                    sbOut.Append(",");
                    sbOut.Append(col.DisplayIndex);
                    sbOut.Append(",");
                    sbOut.Append(col.Visible);
                    pipe = "|";
                }
                Settings.Default.miniGridOrder = sbOut.ToString();
            }
            catch (Exception)
            {
                //Something went wrong, ignore
            }
        }

        /// <summary>
        /// Saves the settings of the ScannerView
        /// </summary>
        /// <param name="view">The ScannerView</param>
        public static void SaveScannerViewSettings(ScannerView view)
        {
            try
            {
                StringBuilder sbOut = new StringBuilder();
                string pipe = "";
                foreach (DataGridViewColumn col in view.scannerGrid.Columns)
                {
                    //Add the column name and order
                    sbOut.Append(pipe);
                    sbOut.Append(col.Name);
                    sbOut.Append(",");
                    sbOut.Append(col.DisplayIndex);
                    sbOut.Append(",");
                    sbOut.Append(col.Visible);
                    pipe = "|";
                }
                Settings.Default.gridOrder = sbOut.ToString();
            }
            catch (Exception)
            {
                //Something went wrong, ignore
            }
        }

        #endregion Public Methods

        #region Private Methods

        //public static string AnalyticsMedium
        //{
        //    get{return Settings.Default.AnalyticsMedium}
        //}
        /// <summary>
        /// Saves the settings
        /// </summary>
        //public static void Save()
        //{
        //    Settings.Default.Save();
        //}

        #endregion Private Methods
    }
}