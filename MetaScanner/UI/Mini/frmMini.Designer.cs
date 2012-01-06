using inSSIDer.UI.Controls;

namespace inSSIDer.UI.Mini
{
    partial class FormMini
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMini));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.configreGPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.changeLogFilenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startStopLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertGPXLogToKMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miniModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchToFullModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inSSIDerForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutInSSIDerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpsStatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsTabControl = new System.Windows.Forms.TabControl();
            this.tabGrid = new System.Windows.Forms.TabPage();
            this.scannerViewMini1 = new inSSIDer.UI.Mini.ScannerViewMini();
            this.tabTimeGraph = new System.Windows.Forms.TabPage();
            this.timeGraph1 = new inSSIDer.UI.Controls.TimeGraph();
            this.tab24Chan = new System.Windows.Forms.TabPage();
            this.channelView24 = new inSSIDer.UI.Controls.ChannelView();
            this.tab58ChanLow = new System.Windows.Forms.TabPage();
            this.channelView5Low = new inSSIDer.UI.Controls.ChannelView();
            this.tab28ChanHigh = new System.Windows.Forms.TabPage();
            this.channelView5High = new inSSIDer.UI.Controls.ChannelView();
            this.tabGps = new System.Windows.Forms.TabPage();
            this.gpsMon1 = new inSSIDer.UI.Controls.GpsMon();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.apCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.locationToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.loggingToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sdlgLog = new System.Windows.Forms.SaveFileDialog();
            this.networkInterfaceSelector1 = new inSSIDer.UI.Controls.NetworkInterfaceSelector();
            this.menuStrip1.SuspendLayout();
            this.detailsTabControl.SuspendLayout();
            this.tabGrid.SuspendLayout();
            this.tabTimeGraph.SuspendLayout();
            this.tab24Chan.SuspendLayout();
            this.tab58ChanLow.SuspendLayout();
            this.tab28ChanHigh.SuspendLayout();
            this.tabGps.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.shortcutsToolStripMenuItem,
            this.gpsStatToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(534, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.configreGPSToolStripMenuItem,
            this.toolStripSeparator5,
            this.changeLogFilenameToolStripMenuItem,
            this.startStopLoggingToolStripMenuItem,
            this.convertGPXLogToKMLToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // configreGPSToolStripMenuItem
            // 
            this.configreGPSToolStripMenuItem.Name = "configreGPSToolStripMenuItem";
            this.configreGPSToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.configreGPSToolStripMenuItem.Text = "Configre GPS";
            this.configreGPSToolStripMenuItem.Click += new System.EventHandler(this.ConfigreGpsToolStripMenuItemClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(199, 6);
            // 
            // changeLogFilenameToolStripMenuItem
            // 
            this.changeLogFilenameToolStripMenuItem.Name = "changeLogFilenameToolStripMenuItem";
            this.changeLogFilenameToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.changeLogFilenameToolStripMenuItem.Text = "Change log filename";
            this.changeLogFilenameToolStripMenuItem.Click += new System.EventHandler(this.ChangeLogFilenameToolStripMenuItemClick);
            // 
            // startStopLoggingToolStripMenuItem
            // 
            this.startStopLoggingToolStripMenuItem.Name = "startStopLoggingToolStripMenuItem";
            this.startStopLoggingToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.startStopLoggingToolStripMenuItem.Text = "Start Logging";
            this.startStopLoggingToolStripMenuItem.Click += new System.EventHandler(this.StartStopGpxLoggingToolStripMenuItemClick);
            // 
            // convertGPXLogToKMLToolStripMenuItem
            // 
            this.convertGPXLogToKMLToolStripMenuItem.Name = "convertGPXLogToKMLToolStripMenuItem";
            this.convertGPXLogToKMLToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.convertGPXLogToKMLToolStripMenuItem.Text = "Convert GPX log to KML";
            this.convertGPXLogToKMLToolStripMenuItem.Click += new System.EventHandler(this.ConvertGpxLogToKmlToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miniModeToolStripMenuItem,
            this.switchToFullModeToolStripMenuItem,
            this.fullScreenToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // miniModeToolStripMenuItem
            // 
            this.miniModeToolStripMenuItem.Checked = true;
            this.miniModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miniModeToolStripMenuItem.Enabled = false;
            this.miniModeToolStripMenuItem.Name = "miniModeToolStripMenuItem";
            this.miniModeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.miniModeToolStripMenuItem.Text = "Mini Mode";
            // 
            // switchToFullModeToolStripMenuItem
            // 
            this.switchToFullModeToolStripMenuItem.Name = "switchToFullModeToolStripMenuItem";
            this.switchToFullModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.switchToFullModeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.switchToFullModeToolStripMenuItem.Text = "&Normal Mode";
            this.switchToFullModeToolStripMenuItem.Click += new System.EventHandler(this.SwitchToFullModeToolStripMenuItemClick);
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Enabled = false;
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.fullScreenToolStripMenuItem.Text = "Full Screen";
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
            this.helpToolStripMenuItem.Text = "&Help";
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
            // gpsStatToolStripMenuItem
            // 
            this.gpsStatToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.gpsStatToolStripMenuItem.Image = global::inSSIDer.Properties.Resources.wifiPlay;
            this.gpsStatToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 186, 0);
            this.gpsStatToolStripMenuItem.Name = "gpsStatToolStripMenuItem";
            this.gpsStatToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.gpsStatToolStripMenuItem.Text = "Start GPS";
            this.gpsStatToolStripMenuItem.Click += new System.EventHandler(this.GpsStatToolStripMenuItemClick);
            this.gpsStatToolStripMenuItem.LocationChanged += new System.EventHandler(this.GpsStatToolStripMenuItemLocationChanged);
            this.gpsStatToolStripMenuItem.TextChanged += new System.EventHandler(this.GpsStatToolStripMenuItemTextChanged);
            // 
            // detailsTabControl
            // 
            this.detailsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsTabControl.Controls.Add(this.tabGrid);
            this.detailsTabControl.Controls.Add(this.tabTimeGraph);
            this.detailsTabControl.Controls.Add(this.tab24Chan);
            this.detailsTabControl.Controls.Add(this.tab58ChanLow);
            this.detailsTabControl.Controls.Add(this.tab28ChanHigh);
            this.detailsTabControl.Controls.Add(this.tabGps);
            this.detailsTabControl.ItemSize = new System.Drawing.Size(41, 18);
            this.detailsTabControl.Location = new System.Drawing.Point(0, 24);
            this.detailsTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.detailsTabControl.Multiline = true;
            this.detailsTabControl.Name = "detailsTabControl";
            this.detailsTabControl.SelectedIndex = 0;
            this.detailsTabControl.Size = new System.Drawing.Size(535, 216);
            this.detailsTabControl.TabIndex = 1;
            // 
            // tabGrid
            // 
            this.tabGrid.Controls.Add(this.scannerViewMini1);
            this.tabGrid.Location = new System.Drawing.Point(4, 22);
            this.tabGrid.Name = "tabGrid";
            this.tabGrid.Size = new System.Drawing.Size(527, 190);
            this.tabGrid.TabIndex = 5;
            this.tabGrid.Text = "Grid";
            this.tabGrid.UseVisualStyleBackColor = true;
            // 
            // scannerViewMini1
            // 
            this.scannerViewMini1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scannerViewMini1.Location = new System.Drawing.Point(0, 0);
            this.scannerViewMini1.Name = "scannerViewMini1";
            this.scannerViewMini1.Size = new System.Drawing.Size(527, 190);
            this.scannerViewMini1.TabIndex = 0;
            // 
            // tabTimeGraph
            // 
            this.tabTimeGraph.BackColor = System.Drawing.Color.Black;
            this.tabTimeGraph.Controls.Add(this.timeGraph1);
            this.tabTimeGraph.ForeColor = System.Drawing.Color.Lime;
            this.tabTimeGraph.Location = new System.Drawing.Point(4, 22);
            this.tabTimeGraph.Name = "tabTimeGraph";
            this.tabTimeGraph.Size = new System.Drawing.Size(527, 190);
            this.tabTimeGraph.TabIndex = 0;
            this.tabTimeGraph.Text = "Time Graph";
            this.tabTimeGraph.UseVisualStyleBackColor = true;
            // 
            // timeGraph1
            // 
            this.timeGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeGraph1.Location = new System.Drawing.Point(0, 0);
            this.timeGraph1.Margin = new System.Windows.Forms.Padding(0);
            this.timeGraph1.MaxAmplitude = -10F;
            this.timeGraph1.MaxTime = new System.DateTime(2010, 9, 1, 16, 24, 33, 190);
            this.timeGraph1.MinAmplitude = -100F;
            this.timeGraph1.Name = "timeGraph1";
            this.timeGraph1.RightMargin = 20;
            this.timeGraph1.ShowSSIDs = true;
            this.timeGraph1.Size = new System.Drawing.Size(527, 190);
            this.timeGraph1.TabIndex = 0;
            this.timeGraph1.TimeSpan = System.TimeSpan.Parse("00:05:00");
            // 
            // tab24Chan
            // 
            this.tab24Chan.BackColor = System.Drawing.Color.Black;
            this.tab24Chan.Controls.Add(this.channelView24);
            this.tab24Chan.ForeColor = System.Drawing.Color.Lime;
            this.tab24Chan.Location = new System.Drawing.Point(4, 22);
            this.tab24Chan.Name = "tab24Chan";
            this.tab24Chan.Size = new System.Drawing.Size(527, 190);
            this.tab24Chan.TabIndex = 1;
            this.tab24Chan.Text = "2.4 GHz Channels";
            this.tab24Chan.UseVisualStyleBackColor = true;
            // 
            // channelView24
            // 
            this.channelView24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelView24.Location = new System.Drawing.Point(0, 0);
            this.channelView24.MaxAmplitude = -10F;
            this.channelView24.MaxFrequency = 2495F;
            this.channelView24.MinAmplitude = -100F;
            this.channelView24.MinFrequency = 2400F;
            this.channelView24.Name = "channelView24";
            this.channelView24.Size = new System.Drawing.Size(527, 190);
            this.channelView24.TabIndex = 0;
            // 
            // tab58ChanLow
            // 
            this.tab58ChanLow.BackColor = System.Drawing.Color.Black;
            this.tab58ChanLow.Controls.Add(this.channelView5Low);
            this.tab58ChanLow.ForeColor = System.Drawing.Color.Lime;
            this.tab58ChanLow.Location = new System.Drawing.Point(4, 22);
            this.tab58ChanLow.Name = "tab58ChanLow";
            this.tab58ChanLow.Size = new System.Drawing.Size(527, 190);
            this.tab58ChanLow.TabIndex = 2;
            this.tab58ChanLow.Text = "5 GHz Low Channels";
            this.tab58ChanLow.UseVisualStyleBackColor = true;
            // 
            // channelView5Low
            // 
            this.channelView5Low.Band = inSSIDer.UI.Controls.ChannelView.BandType.Band5000MHz;
            this.channelView5Low.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelView5Low.Location = new System.Drawing.Point(0, 0);
            this.channelView5Low.MaxAmplitude = -10F;
            this.channelView5Low.MaxFrequency = 5350F;
            this.channelView5Low.MinAmplitude = -100F;
            this.channelView5Low.MinFrequency = 5150F;
            this.channelView5Low.Name = "channelView5Low";
            this.channelView5Low.Size = new System.Drawing.Size(527, 190);
            this.channelView5Low.TabIndex = 1;
            // 
            // tab28ChanHigh
            // 
            this.tab28ChanHigh.BackColor = System.Drawing.Color.Black;
            this.tab28ChanHigh.Controls.Add(this.channelView5High);
            this.tab28ChanHigh.ForeColor = System.Drawing.Color.Lime;
            this.tab28ChanHigh.Location = new System.Drawing.Point(4, 22);
            this.tab28ChanHigh.Name = "tab28ChanHigh";
            this.tab28ChanHigh.Size = new System.Drawing.Size(527, 190);
            this.tab28ChanHigh.TabIndex = 4;
            this.tab28ChanHigh.Text = "5 GHz High Channels";
            this.tab28ChanHigh.UseVisualStyleBackColor = true;
            // 
            // channelView5High
            // 
            this.channelView5High.Band = inSSIDer.UI.Controls.ChannelView.BandType.Band5000MHz;
            this.channelView5High.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelView5High.Location = new System.Drawing.Point(0, 0);
            this.channelView5High.MaxAmplitude = -10F;
            this.channelView5High.MaxFrequency = 5850F;
            this.channelView5High.MinAmplitude = -100F;
            this.channelView5High.MinFrequency = 5480F;
            this.channelView5High.Name = "channelView5High";
            this.channelView5High.Size = new System.Drawing.Size(527, 190);
            this.channelView5High.TabIndex = 2;
            // 
            // tabGps
            // 
            this.tabGps.BackColor = System.Drawing.Color.Black;
            this.tabGps.Controls.Add(this.gpsMon1);
            this.tabGps.Location = new System.Drawing.Point(4, 22);
            this.tabGps.Name = "tabGps";
            this.tabGps.Size = new System.Drawing.Size(527, 190);
            this.tabGps.TabIndex = 3;
            this.tabGps.Text = "GPS";
            // 
            // gpsMon1
            // 
            this.gpsMon1.BackColor = System.Drawing.Color.Black;
            this.gpsMon1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpsMon1.ForeColor = System.Drawing.Color.Lime;
            this.gpsMon1.Location = new System.Drawing.Point(0, 0);
            this.gpsMon1.Name = "gpsMon1";
            this.gpsMon1.Size = new System.Drawing.Size(527, 190);
            this.gpsMon1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apCountLabel,
            this.gpsToolStripStatusLabel,
            this.locationToolStripStatusLabel,
            this.loggingToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(534, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // apCountLabel
            // 
            this.apCountLabel.AutoSize = false;
            this.apCountLabel.BackColor = System.Drawing.SystemColors.Control;
            this.apCountLabel.Name = "apCountLabel";
            this.apCountLabel.Size = new System.Drawing.Size(100, 17);
            this.apCountLabel.Text = "0 / 0 AP(s)";
            this.apCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gpsToolStripStatusLabel
            // 
            this.gpsToolStripStatusLabel.AutoSize = false;
            this.gpsToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.gpsToolStripStatusLabel.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
            this.gpsToolStripStatusLabel.Name = "gpsToolStripStatusLabel";
            this.gpsToolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.gpsToolStripStatusLabel.Text = "GPS Status";
            this.gpsToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // locationToolStripStatusLabel
            // 
            this.locationToolStripStatusLabel.AutoSize = false;
            this.locationToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.locationToolStripStatusLabel.Name = "locationToolStripStatusLabel";
            this.locationToolStripStatusLabel.Size = new System.Drawing.Size(320, 17);
            this.locationToolStripStatusLabel.Text = "Location";
            this.locationToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loggingToolStripStatusLabel
            // 
            this.loggingToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.loggingToolStripStatusLabel.Name = "loggingToolStripStatusLabel";
            this.loggingToolStripStatusLabel.Size = new System.Drawing.Size(86, 15);
            this.loggingToolStripStatusLabel.Text = "Logging Status";
            // 
            // sdlgLog
            // 
            this.sdlgLog.DefaultExt = "gpx";
            this.sdlgLog.Filter = "GPX log files {*.gpx)|*.gpx";
            this.sdlgLog.SupportMultiDottedExtensions = true;
            this.sdlgLog.Title = "Select where to place the log file";
            // 
            // networkInterfaceSelector1
            // 
            this.networkInterfaceSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.networkInterfaceSelector1.AutoSize = true;
            this.networkInterfaceSelector1.Location = new System.Drawing.Point(353, -1);
            this.networkInterfaceSelector1.MaxTextLength = 25;
            this.networkInterfaceSelector1.Name = "networkInterfaceSelector1";
            this.networkInterfaceSelector1.Size = new System.Drawing.Size(181, 25);
            this.networkInterfaceSelector1.TabIndex = 2;
            this.networkInterfaceSelector1.SizeChanged += new System.EventHandler(this.NetworkInterfaceSelector1SizeChanged);
            // 
            // FormMini
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(534, 262);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.networkInterfaceSelector1);
            this.Controls.Add(this.detailsTabControl);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(550, 300);
            this.Name = "FormMini";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "inSSIDer 2.0 - Mini";
            this.LocationChanged += new System.EventHandler(this.FormMiniLocationChanged);
            this.SizeChanged += new System.EventHandler(this.FormMiniSizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.detailsTabControl.ResumeLayout(false);
            this.tabGrid.ResumeLayout(false);
            this.tabTimeGraph.ResumeLayout(false);
            this.tab24Chan.ResumeLayout(false);
            this.tab58ChanLow.ResumeLayout(false);
            this.tab28ChanHigh.ResumeLayout(false);
            this.tabGps.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl detailsTabControl;
        private System.Windows.Forms.TabPage tabTimeGraph;
        private System.Windows.Forms.TabPage tab24Chan;
        private System.Windows.Forms.TabPage tab58ChanLow;
        private System.Windows.Forms.TabPage tabGps;
        private TimeGraph timeGraph1;
        private ChannelView channelView24;
        private ChannelView channelView5Low;
        private System.Windows.Forms.TabPage tab28ChanHigh;
        private ChannelView channelView5High;
        private System.Windows.Forms.TabPage tabGrid;
        private NetworkInterfaceSelector networkInterfaceSelector1;
        private ScannerViewMini scannerViewMini1;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prevTabToolStripMenuItem;
        private GpsMon gpsMon1;
        private System.Windows.Forms.ToolStripMenuItem gpsStatToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel gpsToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel locationToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel loggingToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inSSIDerForumsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem aboutInSSIDerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem configreGPSToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem changeLogFilenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startStopLoggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertGPXLogToKMLToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sdlgLog;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchToFullModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miniModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel apCountLabel;
    }
}