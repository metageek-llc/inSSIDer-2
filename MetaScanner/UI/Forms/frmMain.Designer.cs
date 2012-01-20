using inSSIDer.HTML;
using inSSIDer.UI.Controls;

namespace inSSIDer.UI.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureGPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.changeLogFilenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startStopLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertLogToKMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToNS1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchToMiniModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inSSIDerForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutInSSIDerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpsStatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.developerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNullScanningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopNullScanningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sdlgLog = new System.Windows.Forms.SaveFileDialog();
            this.sdlgNs1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.apCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.locationToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.loggingToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gripContainer1 = new inSSIDer.UI.Controls.GripSplitContainer();
            this.scannerView = new inSSIDer.UI.Controls.ScannerView();
            this.filtersView = new MetaGeek.Filters.Views.FiltersView();
            this.detailsTabControl = new inSSIDer.UI.Controls.CustomTabControl();
            this.tabNews = new System.Windows.Forms.TabPage();
            this.htmlControl = new inSSIDer.HTML.HtmlControl();
            this.tabTimeGraph = new System.Windows.Forms.TabPage();
            this.timeGraph1 = new inSSIDer.UI.Controls.TimeGraph();
            this.tab24Chan = new System.Windows.Forms.TabPage();
            this.chanView24 = new inSSIDer.UI.Controls.ChannelView();
            this.tab58Chan = new System.Windows.Forms.TabPage();
            this.chanView58 = new inSSIDer.UI.Controls.ChannelView();
            this.tabGps = new System.Windows.Forms.TabPage();
            this.gpsMon1 = new inSSIDer.UI.Controls.GpsMon();
            this.networkInterfaceSelector1 = new inSSIDer.UI.Controls.NetworkInterfaceSelector();
            this.mainMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.gripContainer1.Panel1.SuspendLayout();
            this.gripContainer1.Panel2.SuspendLayout();
            this.gripContainer1.SuspendLayout();
            this.detailsTabControl.SuspendLayout();
            this.tabNews.SuspendLayout();
            this.tabTimeGraph.SuspendLayout();
            this.tab24Chan.SuspendLayout();
            this.tab58Chan.SuspendLayout();
            this.tabGps.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.GripMargin = new System.Windows.Forms.Padding(2);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.shortcutsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.gpsStatToolStripMenuItem,
            this.developerToolStripMenuItem});
            this.mainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1008, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "Main Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureGPSToolStripMenuItem,
            this.toolStripSeparator6,
            this.changeLogFilenameToolStripMenuItem,
            this.startStopLoggingToolStripMenuItem,
            this.convertLogToKMLToolStripMenuItem,
            this.toolStripSeparator5,
            this.exportToNS1ToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem,
            this.crashToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // configureGPSToolStripMenuItem
            // 
            this.configureGPSToolStripMenuItem.Name = "configureGPSToolStripMenuItem";
            this.configureGPSToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.configureGPSToolStripMenuItem.Text = "Configure GPS";
            this.configureGPSToolStripMenuItem.Click += new System.EventHandler(this.ConfigureGpsToolStripMenuItemClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
            // 
            // changeLogFilenameToolStripMenuItem
            // 
            this.changeLogFilenameToolStripMenuItem.Name = "changeLogFilenameToolStripMenuItem";
            this.changeLogFilenameToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.changeLogFilenameToolStripMenuItem.Text = "Change log filename";
            this.changeLogFilenameToolStripMenuItem.Click += new System.EventHandler(this.ChangeLogFilenameToolStripMenuItemClick);
            // 
            // startStopLoggingToolStripMenuItem
            // 
            this.startStopLoggingToolStripMenuItem.Name = "startStopLoggingToolStripMenuItem";
            this.startStopLoggingToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.startStopLoggingToolStripMenuItem.Text = "Start Logging";
            this.startStopLoggingToolStripMenuItem.Click += new System.EventHandler(this.StartStopLoggingToolStripMenuItemClick);
            // 
            // convertLogToKMLToolStripMenuItem
            // 
            this.convertLogToKMLToolStripMenuItem.Name = "convertLogToKMLToolStripMenuItem";
            this.convertLogToKMLToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.convertLogToKMLToolStripMenuItem.Text = "Convert GPX to KML";
            this.convertLogToKMLToolStripMenuItem.Click += new System.EventHandler(this.ConvertLogToKmlToolStripMenuItemClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
            // 
            // exportToNS1ToolStripMenuItem
            // 
            this.exportToNS1ToolStripMenuItem.Name = "exportToNS1ToolStripMenuItem";
            this.exportToNS1ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exportToNS1ToolStripMenuItem.Text = "Export to NS1";
            this.exportToNS1ToolStripMenuItem.Click += new System.EventHandler(this.ExportToNs1ToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // crashToolStripMenuItem
            // 
            this.crashToolStripMenuItem.Name = "crashToolStripMenuItem";
            this.crashToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.crashToolStripMenuItem.Text = "Crash";
            this.crashToolStripMenuItem.Visible = false;
            this.crashToolStripMenuItem.Click += new System.EventHandler(this.CrashToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchToMiniModeToolStripMenuItem,
            this.normalModeToolStripMenuItem,
            this.fullscreenToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // switchToMiniModeToolStripMenuItem
            // 
            this.switchToMiniModeToolStripMenuItem.Name = "switchToMiniModeToolStripMenuItem";
            this.switchToMiniModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.switchToMiniModeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.switchToMiniModeToolStripMenuItem.Text = "Mini Mode";
            this.switchToMiniModeToolStripMenuItem.Click += new System.EventHandler(this.SwitchToMiniModeToolStripMenuItemClick);
            // 
            // normalModeToolStripMenuItem
            // 
            this.normalModeToolStripMenuItem.Checked = true;
            this.normalModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.normalModeToolStripMenuItem.Enabled = false;
            this.normalModeToolStripMenuItem.Name = "normalModeToolStripMenuItem";
            this.normalModeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.normalModeToolStripMenuItem.Text = "Normal Mode";
            this.normalModeToolStripMenuItem.Click += new System.EventHandler(this.NormalModeToolStripMenuItemClick);
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            this.fullscreenToolStripMenuItem.Click += new System.EventHandler(this.FullscreenToolStripMenuItemClick);
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextTabToolStripMenuItem,
            this.prevTabToolStripMenuItem});
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.shortcutsToolStripMenuItem.Text = "Shortcuts";
            this.shortcutsToolStripMenuItem.Visible = false;
            // 
            // nextTabToolStripMenuItem
            // 
            this.nextTabToolStripMenuItem.Name = "nextTabToolStripMenuItem";
            this.nextTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)));
            this.nextTabToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.nextTabToolStripMenuItem.Text = "Next Tab";
            this.nextTabToolStripMenuItem.Click += new System.EventHandler(this.NextTabToolStripMenuItemClick);
            // 
            // prevTabToolStripMenuItem
            // 
            this.prevTabToolStripMenuItem.Name = "prevTabToolStripMenuItem";
            this.prevTabToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Tab)));
            this.prevTabToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.prevTabToolStripMenuItem.Text = "Prev Tab";
            this.prevTabToolStripMenuItem.Click += new System.EventHandler(this.PrevTabToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inSSIDerForumsToolStripMenuItem,
            this.toolStripSeparator3,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator4,
            this.aboutInSSIDerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // inSSIDerForumsToolStripMenuItem
            // 
            this.inSSIDerForumsToolStripMenuItem.Name = "inSSIDerForumsToolStripMenuItem";
            this.inSSIDerForumsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.inSSIDerForumsToolStripMenuItem.Text = "inSSIDer Forums";
            this.inSSIDerForumsToolStripMenuItem.Click += new System.EventHandler(this.InSsiDerForumsToolStripMenuItemClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(167, 6);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItemClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // aboutInSSIDerToolStripMenuItem
            // 
            this.aboutInSSIDerToolStripMenuItem.Name = "aboutInSSIDerToolStripMenuItem";
            this.aboutInSSIDerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutInSSIDerToolStripMenuItem.Text = "About inSSIDer";
            this.aboutInSSIDerToolStripMenuItem.Click += new System.EventHandler(this.AboutInSsiDerToolStripMenuItemClick);
            // 
            // gpsStatToolStripMenuItem
            // 
            this.gpsStatToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.gpsStatToolStripMenuItem.Image = global::inSSIDer.Properties.Resources.wifiPlay;
            this.gpsStatToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 186, 0);
            this.gpsStatToolStripMenuItem.Name = "gpsStatToolStripMenuItem";
            this.gpsStatToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.gpsStatToolStripMenuItem.Text = "Start GPS";
            this.gpsStatToolStripMenuItem.Click += new System.EventHandler(this.GpsStatToolStripMenuItemClick);
            // 
            // developerToolStripMenuItem
            // 
            this.developerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startNullScanningToolStripMenuItem,
            this.stopNullScanningToolStripMenuItem});
            this.developerToolStripMenuItem.Name = "developerToolStripMenuItem";
            this.developerToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.developerToolStripMenuItem.Text = "Developer";
            this.developerToolStripMenuItem.Visible = false;
            // 
            // startNullScanningToolStripMenuItem
            // 
            this.startNullScanningToolStripMenuItem.Name = "startNullScanningToolStripMenuItem";
            this.startNullScanningToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.startNullScanningToolStripMenuItem.Text = "Start Null Scanning";
            this.startNullScanningToolStripMenuItem.Click += new System.EventHandler(this.StartNullScanningToolStripMenuItemClick);
            // 
            // stopNullScanningToolStripMenuItem
            // 
            this.stopNullScanningToolStripMenuItem.Name = "stopNullScanningToolStripMenuItem";
            this.stopNullScanningToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.stopNullScanningToolStripMenuItem.Text = "Stop Null Scanning";
            this.stopNullScanningToolStripMenuItem.Click += new System.EventHandler(this.StopNullScanningToolStripMenuItemClick);
            // 
            // sdlgLog
            // 
            this.sdlgLog.DefaultExt = "gpx";
            this.sdlgLog.Filter = "GPX log files (*.gpx)|*.gpx";
            this.sdlgLog.SupportMultiDottedExtensions = true;
            this.sdlgLog.Title = "Select where to place the log file";
            // 
            // sdlgNs1
            // 
            this.sdlgNs1.DefaultExt = "ns1";
            this.sdlgNs1.Filter = "NetStumbler files (*.ns1)|*.ns1";
            this.sdlgNs1.SupportMultiDottedExtensions = true;
            this.sdlgNs1.Title = "Select where to place the output NS1 file";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apCountLabel,
            this.gpsToolStripStatusLabel,
            this.locationToolStripStatusLabel,
            this.loggingToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 538);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.Size = new System.Drawing.Size(1008, 24);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // apCountLabel
            // 
            this.apCountLabel.AutoSize = false;
            this.apCountLabel.Name = "apCountLabel";
            this.apCountLabel.Size = new System.Drawing.Size(100, 19);
            this.apCountLabel.Text = "0 / 0 AP(s)";
            this.apCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gpsToolStripStatusLabel
            // 
            this.gpsToolStripStatusLabel.AutoSize = false;
            this.gpsToolStripStatusLabel.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            this.gpsToolStripStatusLabel.Name = "gpsToolStripStatusLabel";
            this.gpsToolStripStatusLabel.Size = new System.Drawing.Size(118, 19);
            this.gpsToolStripStatusLabel.Text = "GPS Status";
            this.gpsToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // locationToolStripStatusLabel
            // 
            this.locationToolStripStatusLabel.AutoSize = false;
            this.locationToolStripStatusLabel.Name = "locationToolStripStatusLabel";
            this.locationToolStripStatusLabel.Size = new System.Drawing.Size(320, 19);
            this.locationToolStripStatusLabel.Text = "Location";
            this.locationToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loggingToolStripStatusLabel
            // 
            this.loggingToolStripStatusLabel.Name = "loggingToolStripStatusLabel";
            this.loggingToolStripStatusLabel.Size = new System.Drawing.Size(86, 19);
            this.loggingToolStripStatusLabel.Text = "Logging Status";
            // 
            // gripContainer1
            // 
            this.gripContainer1.BackColor = System.Drawing.Color.Black;
            this.gripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gripContainer1.Location = new System.Drawing.Point(0, 24);
            this.gripContainer1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.gripContainer1.Name = "gripContainer1";
            this.gripContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // gripContainer1.Panel1
            // 
            this.gripContainer1.Panel1.Controls.Add(this.scannerView);
            this.gripContainer1.Panel1.Controls.Add(this.filtersView);
            this.gripContainer1.Panel1MinSize = 69;
            // 
            // gripContainer1.Panel2
            // 
            this.gripContainer1.Panel2.Controls.Add(this.detailsTabControl);
            this.gripContainer1.Panel2MinSize = 150;
            this.gripContainer1.Size = new System.Drawing.Size(1008, 514);
            this.gripContainer1.SplitterDistance = 245;
            this.gripContainer1.SplitterWidth = 7;
            this.gripContainer1.TabIndex = 1;
            // 
            // scannerView
            // 
            this.scannerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scannerView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scannerView.Location = new System.Drawing.Point(0, 66);
            this.scannerView.Name = "scannerView";
            this.scannerView.Size = new System.Drawing.Size(1008, 179);
            this.scannerView.TabIndex = 0;
            // 
            // filtersView
            // 
            this.filtersView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.filtersView.Dock = System.Windows.Forms.DockStyle.Top;
            this.filtersView.Location = new System.Drawing.Point(0, 0);
            this.filtersView.MaximumSize = new System.Drawing.Size(0, 66);
            this.filtersView.Name = "filtersView";
            this.filtersView.Padding = new System.Windows.Forms.Padding(3);
            this.filtersView.Size = new System.Drawing.Size(1008, 66);
            this.filtersView.TabIndex = 1;
            // 
            // detailsTabControl
            // 
            this.detailsTabControl.Controls.Add(this.tabNews);
            this.detailsTabControl.Controls.Add(this.tabTimeGraph);
            this.detailsTabControl.Controls.Add(this.tab24Chan);
            this.detailsTabControl.Controls.Add(this.tab58Chan);
            this.detailsTabControl.Controls.Add(this.tabGps);
            this.detailsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsTabControl.ItemSize = new System.Drawing.Size(0, 25);
            this.detailsTabControl.Location = new System.Drawing.Point(0, 0);
            this.detailsTabControl.Name = "detailsTabControl";
            this.detailsTabControl.Padding = new System.Drawing.Point(0, 0);
            this.detailsTabControl.SelectedIndex = 0;
            this.detailsTabControl.Size = new System.Drawing.Size(1008, 262);
            this.detailsTabControl.TabIndex = 0;
            this.detailsTabControl.TabStop = false;
            this.detailsTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.detailsTabControl_MouseDown);
            // 
            // tabNews
            // 
            this.tabNews.Controls.Add(this.htmlControl);
            this.tabNews.Location = new System.Drawing.Point(4, 29);
            this.tabNews.Name = "tabNews";
            this.tabNews.Size = new System.Drawing.Size(1000, 229);
            this.tabNews.TabIndex = 5;
            this.tabNews.Text = "News";
            this.tabNews.UseVisualStyleBackColor = true;
            // 
            // htmlControl
            // 
            this.htmlControl.AnalyticsSource = "NewsTab";
            this.htmlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlControl.IsWebBrowserContextMenuEnabled = false;
            this.htmlControl.Location = new System.Drawing.Point(0, 0);
            this.htmlControl.MinimumSize = new System.Drawing.Size(20, 20);
            this.htmlControl.Name = "htmlControl";
            this.htmlControl.OpenWebLinks = false;
            this.htmlControl.Size = new System.Drawing.Size(1000, 229);
            this.htmlControl.TabIndex = 0;
            this.htmlControl.UpdateIntervalDays = 1F;
            this.htmlControl.UpdateUrl = "http://www.metageek.net/blog/feed";
            this.htmlControl.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            this.htmlControl.WebBrowserShortcutsEnabled = false;
            // 
            // tabTimeGraph
            // 
            this.tabTimeGraph.BackColor = System.Drawing.Color.Black;
            this.tabTimeGraph.Controls.Add(this.timeGraph1);
            this.tabTimeGraph.ForeColor = System.Drawing.Color.Lime;
            this.tabTimeGraph.Location = new System.Drawing.Point(4, 29);
            this.tabTimeGraph.Name = "tabTimeGraph";
            this.tabTimeGraph.Size = new System.Drawing.Size(1000, 229);
            this.tabTimeGraph.TabIndex = 0;
            this.tabTimeGraph.Text = "Time Graph";
            this.tabTimeGraph.UseVisualStyleBackColor = true;
            // 
            // timeGraph1
            // 
            this.timeGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeGraph1.ForeColor = System.Drawing.Color.DimGray;
            this.timeGraph1.Location = new System.Drawing.Point(0, 0);
            this.timeGraph1.MaxAmplitude = -10F;
            this.timeGraph1.MaxTime = new System.DateTime(2010, 7, 28, 12, 8, 7, 739);
            this.timeGraph1.MinAmplitude = -100F;
            this.timeGraph1.Name = "timeGraph1";
            this.timeGraph1.RightMargin = 32;
            this.timeGraph1.ShowSSIDs = true;
            this.timeGraph1.Size = new System.Drawing.Size(1000, 229);
            this.timeGraph1.TabIndex = 0;
            this.timeGraph1.TimeSpan = System.TimeSpan.Parse("00:05:00");
            // 
            // tab24Chan
            // 
            this.tab24Chan.BackColor = System.Drawing.Color.Black;
            this.tab24Chan.Controls.Add(this.chanView24);
            this.tab24Chan.ForeColor = System.Drawing.Color.Lime;
            this.tab24Chan.Location = new System.Drawing.Point(4, 29);
            this.tab24Chan.Name = "tab24Chan";
            this.tab24Chan.Size = new System.Drawing.Size(1000, 229);
            this.tab24Chan.TabIndex = 1;
            this.tab24Chan.Text = "2.4 GHz Channels";
            this.tab24Chan.UseVisualStyleBackColor = true;
            // 
            // chanView24
            // 
            this.chanView24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chanView24.ForeColor = System.Drawing.Color.DimGray;
            this.chanView24.Location = new System.Drawing.Point(0, 0);
            this.chanView24.MaxAmplitude = -10F;
            this.chanView24.MaxFrequency = 2495F;
            this.chanView24.MinAmplitude = -100F;
            this.chanView24.MinFrequency = 2400F;
            this.chanView24.Name = "chanView24";
            this.chanView24.RightMargin = 20;
            this.chanView24.Size = new System.Drawing.Size(1000, 229);
            this.chanView24.TabIndex = 0;
            // 
            // tab58Chan
            // 
            this.tab58Chan.BackColor = System.Drawing.Color.Black;
            this.tab58Chan.Controls.Add(this.chanView58);
            this.tab58Chan.ForeColor = System.Drawing.Color.Lime;
            this.tab58Chan.Location = new System.Drawing.Point(4, 29);
            this.tab58Chan.Name = "tab58Chan";
            this.tab58Chan.Size = new System.Drawing.Size(1000, 229);
            this.tab58Chan.TabIndex = 4;
            this.tab58Chan.Text = "5 GHz Channels";
            this.tab58Chan.UseVisualStyleBackColor = true;
            // 
            // chanView58
            // 
            this.chanView58.Band = inSSIDer.UI.Controls.ChannelView.BandType.Band5000MHz;
            this.chanView58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chanView58.ForeColor = System.Drawing.Color.DimGray;
            this.chanView58.Location = new System.Drawing.Point(0, 0);
            this.chanView58.MaxAmplitude = -10F;
            this.chanView58.MaxFrequency = 5850F;
            this.chanView58.MinAmplitude = -100F;
            this.chanView58.MinFrequency = 5150F;
            this.chanView58.Name = "chanView58";
            this.chanView58.RightMargin = 20;
            this.chanView58.Size = new System.Drawing.Size(1000, 229);
            this.chanView58.TabIndex = 1;
            // 
            // tabGps
            // 
            this.tabGps.BackColor = System.Drawing.Color.Black;
            this.tabGps.Controls.Add(this.gpsMon1);
            this.tabGps.ForeColor = System.Drawing.Color.Lime;
            this.tabGps.Location = new System.Drawing.Point(4, 29);
            this.tabGps.Name = "tabGps";
            this.tabGps.Size = new System.Drawing.Size(1000, 229);
            this.tabGps.TabIndex = 3;
            this.tabGps.Text = "GPS";
            this.tabGps.UseVisualStyleBackColor = true;
            // 
            // gpsMon1
            // 
            this.gpsMon1.BackColor = System.Drawing.Color.Black;
            this.gpsMon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpsMon1.ForeColor = System.Drawing.Color.Lime;
            this.gpsMon1.Location = new System.Drawing.Point(0, 0);
            this.gpsMon1.Name = "gpsMon1";
            this.gpsMon1.Size = new System.Drawing.Size(1000, 229);
            this.gpsMon1.TabIndex = 0;
            // 
            // networkInterfaceSelector1
            // 
            this.networkInterfaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.networkInterfaceSelector1.AutoSize = true;
            this.networkInterfaceSelector1.Location = new System.Drawing.Point(827, -1);
            this.networkInterfaceSelector1.Name = "networkInterfaceSelector1";
            this.networkInterfaceSelector1.Size = new System.Drawing.Size(181, 25);
            this.networkInterfaceSelector1.TabIndex = 2;
            this.networkInterfaceSelector1.SizeChanged += new System.EventHandler(this.NetworkInterfaceSelector1SizeChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.gripContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.networkInterfaceSelector1);
            this.Controls.Add(this.mainMenu);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "inSSIDer 2.0";
            this.LocationChanged += new System.EventHandler(this.FormMainLocationChanged);
            this.SizeChanged += new System.EventHandler(this.FormMainSizeChanged);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.gripContainer1.Panel1.ResumeLayout(false);
            this.gripContainer1.Panel2.ResumeLayout(false);
            this.gripContainer1.ResumeLayout(false);
            this.detailsTabControl.ResumeLayout(false);
            this.tabNews.ResumeLayout(false);
            this.tabTimeGraph.ResumeLayout(false);
            this.tab24Chan.ResumeLayout(false);
            this.tab58Chan.ResumeLayout(false);
            this.tabGps.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private GripSplitContainer gripContainer1;
        private ScannerView scannerView;
        private NetworkInterfaceSelector networkInterfaceSelector1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sdlgLog;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToNS1ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sdlgNs1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prevTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutInSSIDerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inSSIDerForumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem crashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gpsStatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureGPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem changeLogFilenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startStopLoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertLogToKMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel gpsToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel loggingToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel locationToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem normalModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchToMiniModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem developerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNullScanningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopNullScanningToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel apCountLabel;
        private MetaGeek.Filters.Views.FiltersView filtersView;
        private System.Windows.Forms.TabPage tabNews;
        private HtmlControl htmlControl;
        private System.Windows.Forms.TabPage tabTimeGraph;
        private TimeGraph timeGraph1;
        private System.Windows.Forms.TabPage tab24Chan;
        private ChannelView chanView24;
        private System.Windows.Forms.TabPage tab58Chan;
        private ChannelView chanView58;
        private System.Windows.Forms.TabPage tabGps;
        private GpsMon gpsMon1;
        private CustomTabControl detailsTabControl;
    }
}