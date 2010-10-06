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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using inSSIDer.FileIO;
using inSSIDer.HTML;
using inSSIDer.Localization;
using inSSIDer.Misc;
using inSSIDer.Properties;
using inSSIDer.Scanning;
using inSSIDer.UI.Controls;
using inSSIDer.UI.Forms;
using inSSIDer.Version;

namespace inSSIDer.UI.Mini
{
    public partial class FormMini : Form, IScannerUi
    {
        private ScannerN _scanner;

        private delegate void DelVoidCall();

        private const string Title = "inSSIDer 2.0 - Mini";

        public FormMini()
        {
            InitializeComponent();

            //Set up Gray theme
            ToolStripManager.Renderer = new GrayToolStripRenderer(); 
        }

        public void Initalize(ref ScannerN scanner)
        {
            //Init here
            _scanner = scanner;
            _scanner.ScanComplete += ScannerScanComplete;

            timeGraph1.SetScanner(ref _scanner);
            scannerViewMini1.SetScanner(ref _scanner);
            scannerViewMini1.RequireRefresh += ScannerViewMini1RequireRefresh;

            channelView24.SetScanner(ref _scanner);
            channelView5Low.SetScanner(ref _scanner);
            channelView5High.SetScanner(ref _scanner);

            filterMgr1.SetScanner(ref _scanner);

            gpsMon1.SetScanner(ref _scanner);

            networkInterfaceSelector1.Initialize(ref _scanner);
            networkInterfaceSelector1.NetworkScanStartEvent += NetworkInterfaceSelector1NetworkScanStartEvent;
            networkInterfaceSelector1.NetworkScanStopEvent += NetworkInterfaceSelector1NetworkScanStopEvent;

            _scanner.GpsControl.GpsLocationUpdated += GpsControl_GpsLocationUpdated;

            UpdateButtonsStatus();

            //Load settings
            SettingsMgr.ApplyScannerViewMiniSettings(scannerViewMini1);
        }

        private void GpsControl_GpsLocationUpdated(object sender, EventArgs e)
        {
            try
            {
                if (!_scanner.GpsControl.HasTalked)
                {
                    locationToolStripStatusLabel.Text = Localizer.GetString("WaitingForData");
                }
                else
                {
                    locationToolStripStatusLabel.Text = string.Format("Location: {0}, {1} Speed(km/h): {2}",
                                                                    _scanner.GpsControl.MyGpsData.Latitude.ToString("F6"),
                                                                    _scanner.GpsControl.MyGpsData.Longitude.ToString("F6"),
                                                                    _scanner.GpsControl.MyGpsData.Speed.ToString("F2"));
                }
                UpdateButtonsStatus();

            }
            catch (Exception)
            {

            }
        }

        private void NetworkInterfaceSelector1NetworkScanStopEvent(object sender, EventArgs e)
        {
            _scanner.StopScanning();
        }

        private void NetworkInterfaceSelector1NetworkScanStartEvent(object sender, EventArgs e)
        {
            if(_scanner.WlanInterface == null) return;
            //Start scanning with a minimum scan interval of 1 second
            _scanner.StartScanning(1000);
        }

        private void ScannerViewMini1RequireRefresh(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void ScannerScanComplete(object sender, ScanCompleteEventArgs e)
        {
            RefreshAll();

            //Log it!
            if (_scanner.Logger != null && _scanner.Logger.Enabled)
            {
                //Console.WriteLine(_sc.GetLastScan().Length);
                _scanner.Logger.AppendEntry(e.Data, e.GpsData);
                UpdateButtonsStatus();
            }
        }

        private void RefreshAll()
        {
            scannerViewMini1.Go();
            timeGraph1.Invalidate();
            channelView24.Invalidate();
            channelView5High.Invalidate();
            channelView5Low.Invalidate();
        }

        private void SwitchToFullModeToolStripMenuItemClick(object sender, EventArgs e)
        {
            Program.Switching = Utilities.SwitchMode.ToMain;
            Close();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Program.Switching = Utilities.SwitchMode.None;
            Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Continue scanning if we just switched and were scanning
            if (Program.WasScanning)
            {
                networkInterfaceSelector1.StartScan();
            }
            RefreshAll();

            //Hook the interface error event
            _scanner.NetworkScanner.InterfaceError += NetworkScanner_InterfaceError;

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //Set the scanning flag
            Program.WasScanning = Program.Switching != Utilities.SwitchMode.None && _scanner.NetworkScanner.IsScanning;

            if (Program.Switching == Utilities.SwitchMode.None)
            {
                _scanner.StopScanning();
            }

            SettingsMgr.SaveScannerViewMiniSettings(scannerViewMini1);
            ReleaseEvents();

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Rleases all hooked external events
        /// </summary>
        private void ReleaseEvents()
        {
            _scanner.ScanComplete -= ScannerScanComplete;
            scannerViewMini1.RequireRefresh -= ScannerViewMini1RequireRefresh;

            _scanner.GpsControl.GpsLocationUpdated -= GpsControl_GpsLocationUpdated;

            networkInterfaceSelector1.NetworkScanStartEvent -= NetworkInterfaceSelector1NetworkScanStartEvent;
            networkInterfaceSelector1.NetworkScanStopEvent -= NetworkInterfaceSelector1NetworkScanStopEvent;

            filterMgr1.ReleaseEvents();
        }

        private void NextTabToolStripMenuItemClick(object sender, EventArgs e)
        {
            detailsTabControl.SelectedIndex = ((detailsTabControl.SelectedIndex + 1) % detailsTabControl.TabCount);
        }

        private void PrevTabToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (detailsTabControl.SelectedIndex == 0)
            {
                detailsTabControl.SelectedIndex = detailsTabControl.TabCount - 1;
            }
            else
            {
                detailsTabControl.SelectedIndex = detailsTabControl.SelectedIndex - 1;
            }

        }

        private void NetworkInterfaceSelector1SizeChanged(object sender, EventArgs e)
        {
            gpsStatToolStripMenuItem.Margin = new Padding(0, 0, networkInterfaceSelector1.Width + 5, 0);
        }

        private void GpsStatToolStripMenuItemClick(object sender, EventArgs e)
        {
            //If GPS has no port specified or shift was held down, configure it 
            //If cancel is clicked on the configure button, the GPS will use the last settings.
            if (string.IsNullOrEmpty(_scanner.GpsControl.PortName))
            {
                _scanner.GpsControl.Stop();
                //_gpsStatTimer.Stop();
                ConfigureGps();
            }
            if (_scanner.GpsControl.Enabled)
            {
                _scanner.GpsControl.Stop();
                //_gpsStatTimer.Stop();
            }
            else
            {
                _scanner.GpsControl.Start();
                //_gpsStatTimer.Start();
            }
            UpdateButtonsStatus();
        }

        private void UpdateButtonsStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new DelVoidCall(UpdateButtonsStatus));
                return;
            }

            if (_scanner.GpsControl.Enabled)
            {
                gpsStatToolStripMenuItem.Text = "Stop GPS";
                //locationToolStripStatusLabel.Visible = true;

                if (!_scanner.GpsControl.HasFix && _scanner.GpsControl.HasTalked)
                {
                    gpsToolStripStatusLabel.Text = Localizer.GetString("GPSFixLost");
                    //gPSStstusToolStripMenuItem.ForeColor = Color.Orange;
                }
                else if (!_scanner.GpsControl.HasFix && !_scanner.GpsControl.HasTalked && !_scanner.GpsControl.TimedOut)
                {
                    gpsToolStripStatusLabel.Text = "Waiting";
                }
                else if (!_scanner.GpsControl.HasTalked && _scanner.GpsControl.TimedOut)
                {
                    gpsToolStripStatusLabel.Text = "Check GPS on " + _scanner.GpsControl.PortName;
                }
                else
                {
                    gpsToolStripStatusLabel.Text = Localizer.GetString("GPSOn");
                    //gpsStatToolStripMenuItem.Text = "Stop GPS - GPS: Ok";
                    //gPSStstusToolStripMenuItem.ForeColor = Color.LimeGreen; 
                }
                gpsStatToolStripMenuItem.Image = Resources.wifiStop;
                //turnOnOffGPSToolStripMenuItem.Text = Localizer.GetString("TurnGPSOff");
            }
            else
            {
                //gpsStatToolStripMenuItem.Text = Localizer.GetString("GPSOff");
                gpsStatToolStripMenuItem.Text = "Start GPS";
                //locationToolStripStatusLabel.Visible = false;
                locationToolStripStatusLabel.Text = string.Empty;
                //gPSStstusToolStripMenuItem.ForeColor = Color.DarkRed;
                //turnOnOffGPSToolStripMenuItem.Text = Localizer.GetString("TurnGPSOn");
                gpsToolStripStatusLabel.Text = Localizer.GetString("GPSOff");
                gpsStatToolStripMenuItem.Image = Resources.wifiPlay;
            }

            //Update logger button
            if (_scanner.Logger != null && _scanner.Logger.Enabled)
            {
                string size = string.Empty;
                if (File.Exists(_scanner.Logger.Filename))
                {
                    size = SizeString(new FileInfo(_scanner.Logger.Filename).Length);
                }

                loggingToolStripStatusLabel.Text = Localizer.GetString("LoggingTo") + " " +
                                                   _scanner.Logger.Filename.Substring(_scanner.Logger.Filename.LastIndexOf('\\') + 1) +
                                                   (size != string.Empty
                                                       ? " (" + size + ")"
                                                       : "");

                Text = Title + " - " + loggingToolStripStatusLabel.Text;
                loggingToolStripStatusLabel.ForeColor = Color.Black;

                startStopLoggingToolStripMenuItem.Text = Localizer.GetString("StopLogging");
            }
            else
            {
                Text = Title;
                loggingToolStripStatusLabel.Text = Localizer.GetString("LoggingOff");
                startStopLoggingToolStripMenuItem.Text = Localizer.GetString("StartLogging");
            }
        }

        private void ConfigureGps()
        {
            using (FormGpsCfg form = new FormGpsCfg(_scanner.GpsControl))
            {
                //Show the cfg form
                form.ShowDialog(this);
            }
        }

        private string SizeString(long sizeInBytes)
        {
            string output;
            if (sizeInBytes >= 1024)//Greater than or equal to a kilobyte
            {
                if (sizeInBytes >= 1048576)//Greater than or equal to a megabyte
                {
                    if (sizeInBytes >= 1073741824)//Greater than or equal to a gigabyte
                    {
                        output = (sizeInBytes / 1073741824d).ToString("F2") + "GB";
                    }
                    else
                    {
                        output = (sizeInBytes / 1048576d).ToString("F2") + "MB";
                    }
                }
                else
                {
                    output = (sizeInBytes / 1024d).ToString("F2") + "KB";
                }
            }
            else
            {
                output = sizeInBytes + "B";
            }
            return output;
        }

        private void GpsStatToolStripMenuItemTextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(gpsStatToolStripMenuItem.Text);
        }

        private void GpsStatToolStripMenuItemLocationChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine(gpsStatToolStripMenuItem.Margin);
        }

        private void InSsiDerForumsToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                LinkHelper.OpenLink("http://metageek.net/forums/forumdisplay.php?4615-inSSIDer", Settings.Default.AnalyticsMedium, "HelpMenuForum");
            }
            catch (Win32Exception)
            {
                //
                // Ignore exception. 
                // 
                // This exception will be thrown when Firefox unexpectedly 
                // shuts down, and asks the user to restore the session when it is started by 
                // Inssider. Apparently, Windoz doesn't like this, because it tosses a 
                // file not found exception. Weird!
                //
                // Anyway, the lesser evil right now is to silently ignore
                // this exception.
                //                
            }
        }

        /// <summary>
        /// Checks for update and displays update dialog if there is an update available
        /// </summary>
        /// <param name="userInitiated">Is the update check initiated by the user?</param>
        private static void CheckForUpdate(bool userInitiated)
        {
            ParameterizedThreadStart ps = CheckUpdate;
            Thread updateThread = new Thread(ps);
            //updateThread.IsBackground = true;
            updateThread.Start(userInitiated);
        }

        private static void CheckUpdate(object data)
        {
            bool userInitiated = (bool)data;

            // don't check if the user didn't ask us to and it isn't time to check yet
            if (Settings.Default.VersionNextUpdateCheck > DateTime.Now && !userInitiated)
                return;

            if (VersionInfo.CheckForAvailableUpdate(@"http://www.metageek.net/misc/versions/inssider.txt", Settings.Default.VersionIgnoreThisVersion, userInitiated))
            {
                switch (VersionInfo.ShowUpdateDialog())
                {
                    case DialogResult.OK:
                        try
                        {
                            LinkHelper.OpenLink(VersionInfo.DownloadUrl, Settings.Default.AnalyticsMedium, @"CheckUpdateForm");
                        }
                        catch (Win32Exception) { }
                        break;
                    case DialogResult.Cancel:
                        Settings.Default.VersionNextUpdateCheck = DateTime.Now + TimeSpan.FromDays(Settings.Default.VersionDaysBetweenUpdateReminders);
                        break;
                    case DialogResult.Ignore:
                        Settings.Default.VersionIgnoreThisVersion = VersionInfo.LatestVersion;
                        break;
                }
            }
            else
            {
                Settings.Default.VersionNextUpdateCheck = DateTime.Now + TimeSpan.FromDays(1);
                if (userInitiated)
                    MessageBox.Show(Localizer.GetString("ApplicationUpToDate"));
            }
        }

        private void CheckForUpdatesToolStripMenuItemClick(object sender, EventArgs e)
        {
            CheckForUpdate(true);
        }

        private void AboutInSsiDerToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (FormAbout form = new FormAbout())
            {
                form.ShowDialog(this);
            }
        }

        private void ConfigreGpsToolStripMenuItemClick(object sender, EventArgs e)
        {
            _scanner.GpsControl.Stop();
            ConfigureGps();
        }

        private void ChangeLogFilenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (sdlgLog.ShowDialog(this) == DialogResult.OK && _scanner.Logger != null) _scanner.Logger.Filename = sdlgLog.FileName;
        }

        private void StartStopGpxLoggingToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_scanner.Logger == null) _scanner.Logger = new GpxDataLogger { AutoSave = true, AutoSaveInterval = TimeSpan.FromSeconds(10) };

            if (!_scanner.Logger.Enabled) //If the logger is null, it can't be enabled
            {
                //Reset the logger
                //TODO: Make the autosave interval configurable
                //TODO: Use Settings!
                _scanner.Logger.Reset();
                //Ask user for filename if it isn't selected already
                if (string.IsNullOrEmpty(_scanner.Logger.Filename))
                {
                    if (sdlgLog.ShowDialog(this) == DialogResult.OK)
                    {
                        _scanner.Logger.Filename = sdlgLog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                _scanner.Logger.Start();
            }
            else if (_scanner.Logger != null)
            {
                _scanner.Logger.Stop();
            }
            UpdateButtonsStatus();
        }

        private void ConvertGpxLogToKmlToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (FormLogConverter form = new FormLogConverter())
            {
                form.ShowDialog(this);
            }
        }

        private void FormMiniLocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Settings.Default.miniLocation = Location;
            }
        }

        private void FormMiniSizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Settings.Default.miniSize = Size;
            }
            if (WindowState != FormWindowState.Minimized)
            {
                Settings.Default.miniWindowState = WindowState;
            }
            if (WindowState == FormWindowState.Maximized)
            {
            }
        }

        private void NetworkScanner_InterfaceError(object sender, EventArgs e)
        {
            MessageBox.Show(Localizer.GetString("InterfaceError"),
                    "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            networkInterfaceSelector1.StopScan();
        }
    }
}
