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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using inSSIDer.FileIO;
using inSSIDer.HTML;
using inSSIDer.Misc;
using inSSIDer.Scanning;
using inSSIDer.UI.Controls;
using inSSIDer.Version;
using Microsoft.Win32;
using inSSIDer.Properties;
using inSSIDer.Localization;
using System.Threading;
using System.Globalization;
using Timer = System.Timers.Timer;

namespace inSSIDer.UI.Forms
{
    public partial class FormMain : Form, IScannerUi
    {
        private ScannerN _scanner = new ScannerN();
        private Timer _gpsStatTimer = new Timer(1000);
        //private GpxDataLogger _logger;

        private FormWindowState _lastState;

        private delegate void DelVoidCall();

        //Flag to signal interface switching
        //private bool switching = false;

        public FormMain()
        {
            // This is for testing localization...
            String culture = String.Empty;
            //culture = "es-es";
            //culture = "zh-TW";
            //culture = "ja-JP";
            //culture = "de-DE";
            //culture = "sv-SE";
            //culture = "ru-RU";

            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo ci = new CultureInfo(culture);
                // set culture for formatting
                Thread.CurrentThread.CurrentCulture = ci;
                // set culture for resources
                Thread.CurrentThread.CurrentUICulture = ci;
            }

            InitializeComponent();
            ToolStripManager.Renderer = new GrayToolStripRenderer();

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        public void Initalize(ref ScannerN scanner)
        {
            _scanner = scanner;
            _scanner.ScanComplete += ScannerScanComplete;

            //_sc.StartScanning(_sc.AvalibleWlanInterfaces[0], 1000);
            //_updateTimer.Start();

            timeGraph1.SetScanner(ref _scanner);
            scannerView.SetScanner(ref _scanner);
            scannerView.RequireRefresh += ScannerViewRequireRefresh;
            chanView24.SetScanner(ref _scanner);
            chanView58.SetScanner(ref _scanner);
            filterMgr1.SetScanner(ref _scanner);
            networkInterfaceSelector1.Initialize(ref _scanner);
            networkInterfaceSelector1.NetworkScanStartEvent += NetworkInterfaceSelectorNetworkScanStartEvent;
            networkInterfaceSelector1.NetworkScanStopEvent += NetworkInterfaceSelectorNetworkScanStopEvent;

            _gpsStatTimer = new Timer(1000);
            _gpsStatTimer.Elapsed += GpsStatTimerElapsed;

            gpsMon1.SetScanner(ref _scanner);
            UpdateButtonsStatus();

            //Load settings
            SettingsMgr.ApplyScannerViewSettings(scannerView);
        }

        private void ScannerViewRequireRefresh(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void GpsStatTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
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

        private void ScannerScanComplete(object sender, ScanCompleteEventArgs e)
        {
            //_sc.ScanComplete -= _sc_ScanComplete;
            RefreshAll();

            //Log it!
            if (_scanner.Logger != null && _scanner.Logger.Enabled)
            {
                //Console.WriteLine(_sc.GetLastScan().Length);
                _scanner.Logger.AppendEntry(e.Data, e.GpsData);
                UpdateButtonsStatus();
            }

            //The invoke is always required
            try
            {
                Invoke(new DelVoidCall(() => apCountLabel.Text = string.Format("{0} / {1} AP(s)", _scanner.Cache.Count, _scanner.Cache.TotalCount)));
            }
            catch (InvalidOperationException)
            {
                // Exception thrown if UI isn't fully initialized yet. Ignore for now and let the next ScannerScanComplete() call
                //update the UI.
            }
        }

        private void RefreshAll()
        {
            scannerView.UpdateGrid();
            timeGraph1.Invalidate();
            chanView24.Invalidate();
            chanView58.Invalidate();
        }

        private void NetworkInterfaceSelectorNetworkScanStopEvent(object sender, EventArgs e)
        {
            _scanner.StopScanning();
        }

        private void NetworkInterfaceSelectorNetworkScanStartEvent(object sender, EventArgs e)
        {
            if(_scanner.WlanInterface == null)
            {
                
            }
            _scanner.StartScanning(1000);
        }

        /// <summary>
        /// Called when machine suspends, shuts down, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    networkInterfaceSelector1.StopScan();
                    break;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CheckForUpdate(false);

            // Move news HTML to temp directory if it's not already there
            // This allows us to update the news file
            bool copied = CopyHtmlToTemp();

            // Force update if we just copied the file over
            bool newsDisplayedProperly = htmlControl.UpdateFile(copied);
            if (!newsDisplayedProperly)
            {
                //tabNews.Visible = false;
                detailsTabControl.Controls.Remove(tabNews);
            }

#if CRASH
            crashToolStripMenuItem.Enabled = true;
            crashToolStripMenuItem.Visible = true;
#endif

#if DEBUG
            developerToolStripMenuItem.Visible = true;
#endif
            detailsTabControl.SelectedIndex = Settings.Default.formTabIndex;

            RefreshAll();

            //Continue scanning if we just switched and were scanning
            if(Program.WasScanning) networkInterfaceSelector1.StartScan();

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
            SettingsMgr.SaveScannerViewSettings(scannerView);
            Settings.Default.formTabIndex = detailsTabControl.SelectedIndex;

            //Settings.Default.Save();

            ReleaseEvents();

            base.OnFormClosing(e);
        }

        private void UpdateButtonsStatus()
        {
            if(InvokeRequired)
            {
                Invoke(new DelVoidCall(UpdateButtonsStatus));
                return;
            }

            if(_scanner.GpsControl.Enabled)
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
                    size = FormatSizeString(new FileInfo(_scanner.Logger.Filename).Length);
                }

                loggingToolStripStatusLabel.Text = Localizer.GetString("LoggingTo") + " " +
                                                   _scanner.Logger.Filename.Substring(_scanner.Logger.Filename.LastIndexOf('\\') + 1) +
                                                   (size != string.Empty
                                                       ? " (" + size + ")"
                                                       : "");
                loggingToolStripStatusLabel.ForeColor = Color.Black;

                startStopLoggingToolStripMenuItem.Text = Localizer.GetString("StopLogging");

                loggingToolStripStatusLabel.BackColor = SystemColors.Control;
            }
            else
            {
                // Highlight "Logging Off" if GPS is enabled
                loggingToolStripStatusLabel.BackColor = _scanner.GpsControl.Enabled ? Color.Yellow : SystemColors.Control;

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

        private static string FormatSizeString(long sizeInBytes)
        {
            string output = "0B";
            if(sizeInBytes >= 1024)//Greater than or equal to a kilobyte
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

        private void FullscreenToolStripMenuItemClick(object sender, EventArgs e)
        {
            //It's not fullscreen, make it fullscreen
            _lastState = WindowState;
            if (WindowState == FormWindowState.Maximized)
            {
                //If the window is already maximized, 
                //it must be set to normal before the fullscreen
                //Otherwise, the fullscreen messes up bigtime.
                WindowState = FormWindowState.Normal;
            }
            FormBorderStyle = FormBorderStyle.None;

            WindowState = FormWindowState.Maximized;

            normalModeToolStripMenuItem.Checked = false;
            fullscreenToolStripMenuItem.Checked = true;

            fullscreenToolStripMenuItem.Enabled = false;
            normalModeToolStripMenuItem.Enabled = true;

            // Only allow switch to mini mode from normal mode
            switchToMiniModeToolStripMenuItem.Enabled = false;
        }

        private void NormalModeToolStripMenuItemClick(object sender, EventArgs e)
        {
            //It's fullscreen, return it to normal
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = _lastState;

            normalModeToolStripMenuItem.Checked = true;
            fullscreenToolStripMenuItem.Checked = false;

            fullscreenToolStripMenuItem.Enabled = true;
            normalModeToolStripMenuItem.Enabled = false;
            switchToMiniModeToolStripMenuItem.Enabled = true;
        }

        private void SwitchToMiniModeToolStripMenuItemClick(object sender, EventArgs e)
        {
            //switch to the mini form and preserve the scanner
            Program.Switching = Utilities.SwitchMode.ToMini;
            Close();
        }

        private void ExportToNs1ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(sdlgNs1.ShowDialog(this) == DialogResult.OK)
            {
                //Write the file
                Ns1Writer.Write(sdlgNs1.FileName,_scanner.Cache.GetAccessPoints());
                MessageBox.Show(Localizer.GetString("ExportComplete"),Localizer.GetString("Finished"), MessageBoxButtons.OK);
            }
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

        private void AboutInSsiDerToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (FormAbout form = new FormAbout())
            {
                form.ShowDialog(this);
            }
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

        private void CheckForUpdatesToolStripMenuItemClick(object sender, EventArgs e)
        {
            CheckForUpdate(true);
        }

        private void CrashToolStripMenuItemClick(object sender, EventArgs e)
        {
#if CRASH
            throw new NullReferenceException("You crashed it!");
#endif
        }

        /// <summary>
        /// Copies HTML files to temp directory
        /// </summary>
        /// <returns>True if copied files, false otherwise</returns>
        private bool CopyHtmlToTemp()
        {
            bool copiedFile = false;

            string newsDirectory = Path.GetTempPath() + "MetaGeekNews" + Path.DirectorySeparatorChar;
            string newsFile = newsDirectory + "news.html";

            try
            {
                //// check whether the rss already exists
                if (!File.Exists(newsFile))
                {
                    Directory.CreateDirectory(newsDirectory);
                    File.Copy("HTML\\Content\\news.html", newsFile, true);
                    copiedFile = true;
                }
                if (!File.Exists(newsDirectory + "news.css"))
                {
                    File.Copy("HTML\\Content\\news.css", newsDirectory + "news.css", true);
                }
            }
            catch (Exception)
            {
            }

            return copiedFile;
        }

        /// <summary>
        /// Checks for update and displays update dialog if there is an update available
        /// </summary>
        /// <param name="userInitiated">Is the update check initiated by the user?</param>
        private static void CheckForUpdate(bool userInitiated)
        {
            ParameterizedThreadStart ps = CheckForUpdateThread;
            Thread updateThread = new Thread(ps);
            updateThread.Start(userInitiated);
        }

        private static void CheckForUpdateThread(object data)
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

        private void NetworkInterfaceSelector1SizeChanged(object sender, EventArgs e)
        {
            gpsStatToolStripMenuItem.Margin = new Padding(0, 0, networkInterfaceSelector1.Width + 5, 0);
        }

        private void GpsStatToolStripMenuItemClick(object sender, EventArgs e)
        {
            //If GPS has no port specified, configure it 
            //If cancel is clicked on the configure button, the GPS will use the last settings.
            //Also stop the GPX logger if GPS is disabled while logging
            if (string.IsNullOrEmpty(_scanner.GpsControl.PortName))
            {
                _scanner.GpsControl.Stop();
                _scanner.Logger.Stop();
                _gpsStatTimer.Stop();
                ConfigureGps();
            }
            if (_scanner.GpsControl.Enabled)
            {
                _scanner.GpsControl.Stop();
                _scanner.Logger.Stop();
                _gpsStatTimer.Stop();
            }
            else
            {
                _scanner.GpsControl.Start();
                _gpsStatTimer.Start();
            }
            UpdateButtonsStatus();
        }

        private void ChangeLogFilenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (sdlgLog.ShowDialog(this) == DialogResult.OK && _scanner.Logger != null) _scanner.Logger.Filename = sdlgLog.FileName;
        }

        private void StartStopLoggingToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_scanner.Logger == null)
            {
                _scanner.Logger = new GpxDataLogger() {AutoSave = true, AutoSaveInterval = TimeSpan.FromSeconds(10)};
            }

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

        private void ConvertLogToKmlToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (FormLogConverter form = new FormLogConverter())
            {
                form.ShowDialog(this);
            }
        }

        private void ConfigureGpsToolStripMenuItemClick(object sender, EventArgs e)
        {
            _scanner.GpsControl.Stop();
            _gpsStatTimer.Stop();
            ConfigureGps();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Rleases all hooked external events
        /// </summary>
        private void ReleaseEvents()
        {
            _scanner.ScanComplete -= ScannerScanComplete;
            scannerView.RequireRefresh -= ScannerViewRequireRefresh;
            _gpsStatTimer.Stop();

            networkInterfaceSelector1.NetworkScanStartEvent -= NetworkInterfaceSelectorNetworkScanStartEvent;
            networkInterfaceSelector1.NetworkScanStopEvent -= NetworkInterfaceSelectorNetworkScanStopEvent;

            //Release control events too
            filterMgr1.ReleaseEvents();

            gpsMon1.ReleaseEvents();
        }

        private void StartNullScanningToolStripMenuItemClick(object sender, EventArgs e)
        {
            _scanner.StartNullScanning();
        }

        private void StopNullScanningToolStripMenuItemClick(object sender, EventArgs e)
        {
            _scanner.StopNullScanning();
        }

        private void FormMainLocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Settings.Default.formLocation = Location;
            }
        }

        private void FormMainSizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Settings.Default.formSize = Size;
            }
            if (WindowState != FormWindowState.Minimized)
            {
                Settings.Default.formWindowState = WindowState;
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
