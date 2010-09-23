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
using System.Windows.Forms;
using inSSIDer.Scanning;
using MetaGeek.Gps;

namespace inSSIDer.UI.Controls
{
    public partial class GpsMon : UserControl
    {
        private Scanner _scanner;

        private delegate void DelUpdateView();

        public GpsMon()
        {
            InitializeComponent();
            //Show GPS must be enabled message
            lblNoGps.Visible = true;
            lblNoGps.Dock = DockStyle.Fill;
        }

        public void SetScanner(ref Scanner scanner)
        {
            _scanner = scanner;
            _scanner.GpsControl.GpsStatUpdated += GpsControl_GpsStatUpdated;
            _scanner.GpsControl.GpsMessage += GpsControl_GpsMessage;

            gpsGraph1.SetScanner(ref scanner);
            UpdateView();
        }

        private void GpsControl_GpsMessage(object sender, StringEventArgs e)
        {
            UpdateView();
        }

        private void GpsControl_GpsStatUpdated(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new DelUpdateView(UpdateView));
                }
                // occurs during close...
                catch (InvalidOperationException)
                {
                }
            }
            else
            {
                try
                {
                    lblNoGps.Visible = !_scanner.GpsControl.Enabled;

                    lblPortName.Text = "GPS on " + _scanner.GpsControl.PortName;

                    lblLat.Text = "Latitude: " + _scanner.GpsControl.MyGpsData.Latitude.ToString("F6");
                    lblLon.Text = "Longitude: " + _scanner.GpsControl.MyGpsData.Longitude.ToString("F6");
                    lblAlt.Text = "Altitude: " + _scanner.GpsControl.MyGpsData.Altitude.ToString("F2");
                    lblSpeed.Text = "Speed (km/h): " + _scanner.GpsControl.MyGpsData.Speed.ToString("F2");
                    lblPdop.Text = "PDOP: " + _scanner.GpsControl.MyGpsData.Pdop;
                    lblHdop.Text = "HDOP: " + _scanner.GpsControl.MyGpsData.Hdop;
                    lblVdop.Text = "VDOP: " + _scanner.GpsControl.MyGpsData.Vdop;
                    lblFixType.Text = "Fix Type: " + _scanner.GpsControl.MyGpsData.FixType;
                    lblSatCount.Text = "Satellites (U/V): " + _scanner.GpsControl.MyGpsData.SatellitesUsed + "/" + _scanner.GpsControl.SatellitesVisible;

                    //Refresh the gps signal graph
                    gpsGraph1.Invalidate();
                }
                catch(Exception)
                {
                    
                }
            }
        }

        /// <summary>
        /// Rleases all hooked external events
        /// </summary>
        public void ReleaseEvents()
        {
            _scanner.GpsControl.GpsStatUpdated -= GpsControl_GpsStatUpdated;
            _scanner.GpsControl.GpsMessage -= GpsControl_GpsMessage;
        }
    }
}
