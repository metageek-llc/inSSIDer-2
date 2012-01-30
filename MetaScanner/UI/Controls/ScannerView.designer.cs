namespace inSSIDer.UI.Controls
{
    sealed partial class ScannerView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scannerGrid = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.macColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ssidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rssiColumn = new inSSIDer.UI.Controls.SparkLineColumn();
            this.channelColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxrateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.networktypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstseenColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mACAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sSIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firstSeenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastSeenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.latitudeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.longitudeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sparkLineColumn1 = new inSSIDer.UI.Controls.SparkLineColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scannerGrid)).BeginInit();
            this.cmsColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // scannerGrid
            // 
            this.scannerGrid.AllowUserToAddRows = false;
            this.scannerGrid.AllowUserToDeleteRows = false;
            this.scannerGrid.AllowUserToOrderColumns = true;
            this.scannerGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle1.NullValue = "null!";
            this.scannerGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.scannerGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.scannerGrid.BackgroundColor = System.Drawing.Color.Black;
            this.scannerGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.scannerGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.scannerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.scannerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.checkColumn,
            this.macColumn,
            this.ssidColumn,
            this.rssiColumn,
            this.channelColumn,
            this.vendorColumn,
            this.securityColumn,
            this.maxrateColumn,
            this.networktypeColumn,
            this.firstseenColumn,
            this.ageColumn,
            this.latColumn,
            this.lonColumn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.scannerGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.scannerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scannerGrid.EnableHeadersVisualStyles = false;
            this.scannerGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scannerGrid.Location = new System.Drawing.Point(0, 0);
            this.scannerGrid.MultiSelect = false;
            this.scannerGrid.Name = "scannerGrid";
            this.scannerGrid.ReadOnly = true;
            this.scannerGrid.RowHeadersVisible = false;
            this.scannerGrid.RowHeadersWidth = 50;
            dataGridViewCellStyle5.NullValue = "null!";
            this.scannerGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.scannerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.scannerGrid.ShowEditingIcon = false;
            this.scannerGrid.Size = new System.Drawing.Size(972, 329);
            this.scannerGrid.TabIndex = 4;
            this.scannerGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.scannerView_CellMouseClick);
            this.scannerGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.scannerView_CellPainting);
            this.scannerGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ScannerGridColumnHeaderMouseClick);
            this.scannerGrid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.ScannerViewColumnWidthChanged);
            this.scannerGrid.SelectionChanged += new System.EventHandler(this.scannerView_SelectionChanged);
            this.scannerGrid.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.scannerView_SortCompare);
            this.scannerGrid.VisibleChanged += new System.EventHandler(this.ScannerViewVisibleChanged);
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Visible = false;
            // 
            // checkColumn
            // 
            this.checkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.checkColumn.FillWeight = 20F;
            this.checkColumn.HeaderText = "";
            this.checkColumn.MinimumWidth = 20;
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.checkColumn.Width = 20;
            // 
            // macColumn
            // 
            this.macColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.macColumn.FillWeight = 70F;
            this.macColumn.HeaderText = "MAC Address";
            this.macColumn.MinimumWidth = 130;
            this.macColumn.Name = "macColumn";
            this.macColumn.ReadOnly = true;
            this.macColumn.Width = 130;
            // 
            // ssidColumn
            // 
            this.ssidColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ssidColumn.FillWeight = 150F;
            this.ssidColumn.HeaderText = "SSID";
            this.ssidColumn.MaxInputLength = 32;
            this.ssidColumn.MinimumWidth = 70;
            this.ssidColumn.Name = "ssidColumn";
            this.ssidColumn.ReadOnly = true;
            // 
            // rssiColumn
            // 
            this.rssiColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.rssiColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.rssiColumn.FillWeight = 40F;
            this.rssiColumn.HeaderText = "RSSI";
            this.rssiColumn.MinimumWidth = 50;
            this.rssiColumn.Name = "rssiColumn";
            this.rssiColumn.ReadOnly = true;
            this.rssiColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.rssiColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.rssiColumn.Width = 82;
            // 
            // channelColumn
            // 
            this.channelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.channelColumn.FillWeight = 50F;
            this.channelColumn.HeaderText = "Channel";
            this.channelColumn.MinimumWidth = 50;
            this.channelColumn.Name = "channelColumn";
            this.channelColumn.ReadOnly = true;
            this.channelColumn.Width = 50;
            // 
            // vendorColumn
            // 
            this.vendorColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vendorColumn.FillWeight = 80F;
            this.vendorColumn.HeaderText = "Vendor";
            this.vendorColumn.MinimumWidth = 80;
            this.vendorColumn.Name = "vendorColumn";
            this.vendorColumn.ReadOnly = true;
            // 
            // securityColumn
            // 
            this.securityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.securityColumn.FillWeight = 80F;
            this.securityColumn.HeaderText = "Security";
            this.securityColumn.MinimumWidth = 80;
            this.securityColumn.Name = "securityColumn";
            this.securityColumn.ReadOnly = true;
            // 
            // maxrateColumn
            // 
            this.maxrateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.maxrateColumn.FillWeight = 60F;
            this.maxrateColumn.HeaderText = "Max Rate";
            this.maxrateColumn.MinimumWidth = 60;
            this.maxrateColumn.Name = "maxrateColumn";
            this.maxrateColumn.ReadOnly = true;
            // 
            // networktypeColumn
            // 
            this.networktypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.networktypeColumn.HeaderText = "Network Type";
            this.networktypeColumn.MinimumWidth = 100;
            this.networktypeColumn.Name = "networktypeColumn";
            this.networktypeColumn.ReadOnly = true;
            // 
            // firstseenColumn
            // 
            this.firstseenColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.firstseenColumn.FillWeight = 70F;
            this.firstseenColumn.HeaderText = "First Seen";
            this.firstseenColumn.MinimumWidth = 70;
            this.firstseenColumn.Name = "firstseenColumn";
            this.firstseenColumn.ReadOnly = true;
            // 
            // ageColumn
            // 
            this.ageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ageColumn.FillWeight = 70F;
            this.ageColumn.HeaderText = "Last Seen";
            this.ageColumn.MinimumWidth = 70;
            this.ageColumn.Name = "ageColumn";
            this.ageColumn.ReadOnly = true;
            // 
            // latColumn
            // 
            this.latColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.latColumn.FillWeight = 70F;
            this.latColumn.HeaderText = "Latitude";
            this.latColumn.MinimumWidth = 70;
            this.latColumn.Name = "latColumn";
            this.latColumn.ReadOnly = true;
            // 
            // lonColumn
            // 
            this.lonColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lonColumn.FillWeight = 70F;
            this.lonColumn.HeaderText = "Longitude";
            this.lonColumn.MinimumWidth = 70;
            this.lonColumn.Name = "lonColumn";
            this.lonColumn.ReadOnly = true;
            // 
            // cmsColumns
            // 
            this.cmsColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mACAddressToolStripMenuItem,
            this.sSIDToolStripMenuItem,
            this.rSSIToolStripMenuItem,
            this.channelToolStripMenuItem,
            this.vendorToolStripMenuItem,
            this.securityToolStripMenuItem,
            this.maxRateToolStripMenuItem,
            this.networkTypeToolStripMenuItem,
            this.firstSeenToolStripMenuItem,
            this.lastSeenToolStripMenuItem,
            this.latitudeToolStripMenuItem,
            this.longitudeToolStripMenuItem});
            this.cmsColumns.Name = "cmsColumns";
            this.cmsColumns.Size = new System.Drawing.Size(149, 268);
            // 
            // mACAddressToolStripMenuItem
            // 
            this.mACAddressToolStripMenuItem.CheckOnClick = true;
            this.mACAddressToolStripMenuItem.Name = "mACAddressToolStripMenuItem";
            this.mACAddressToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.mACAddressToolStripMenuItem.Text = "MAC Address";
            this.mACAddressToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // sSIDToolStripMenuItem
            // 
            this.sSIDToolStripMenuItem.CheckOnClick = true;
            this.sSIDToolStripMenuItem.Name = "sSIDToolStripMenuItem";
            this.sSIDToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.sSIDToolStripMenuItem.Text = "SSID";
            this.sSIDToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // rSSIToolStripMenuItem
            // 
            this.rSSIToolStripMenuItem.CheckOnClick = true;
            this.rSSIToolStripMenuItem.Name = "rSSIToolStripMenuItem";
            this.rSSIToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.rSSIToolStripMenuItem.Text = "RSSI";
            this.rSSIToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // channelToolStripMenuItem
            // 
            this.channelToolStripMenuItem.CheckOnClick = true;
            this.channelToolStripMenuItem.Name = "channelToolStripMenuItem";
            this.channelToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.channelToolStripMenuItem.Text = "Channel";
            this.channelToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // vendorToolStripMenuItem
            // 
            this.vendorToolStripMenuItem.CheckOnClick = true;
            this.vendorToolStripMenuItem.Name = "vendorToolStripMenuItem";
            this.vendorToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.vendorToolStripMenuItem.Text = "Vendor";
            this.vendorToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.CheckOnClick = true;
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.securityToolStripMenuItem.Text = "Security";
            this.securityToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // maxRateToolStripMenuItem
            // 
            this.maxRateToolStripMenuItem.CheckOnClick = true;
            this.maxRateToolStripMenuItem.Name = "maxRateToolStripMenuItem";
            this.maxRateToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.maxRateToolStripMenuItem.Text = "Max Rate";
            this.maxRateToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // networkTypeToolStripMenuItem
            // 
            this.networkTypeToolStripMenuItem.CheckOnClick = true;
            this.networkTypeToolStripMenuItem.Name = "networkTypeToolStripMenuItem";
            this.networkTypeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.networkTypeToolStripMenuItem.Text = "Network Type";
            this.networkTypeToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // firstSeenToolStripMenuItem
            // 
            this.firstSeenToolStripMenuItem.CheckOnClick = true;
            this.firstSeenToolStripMenuItem.Name = "firstSeenToolStripMenuItem";
            this.firstSeenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.firstSeenToolStripMenuItem.Text = "First Seen";
            this.firstSeenToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // lastSeenToolStripMenuItem
            // 
            this.lastSeenToolStripMenuItem.CheckOnClick = true;
            this.lastSeenToolStripMenuItem.Name = "lastSeenToolStripMenuItem";
            this.lastSeenToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.lastSeenToolStripMenuItem.Text = "Last Seen";
            this.lastSeenToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // latitudeToolStripMenuItem
            // 
            this.latitudeToolStripMenuItem.CheckOnClick = true;
            this.latitudeToolStripMenuItem.Name = "latitudeToolStripMenuItem";
            this.latitudeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.latitudeToolStripMenuItem.Text = "Latitude";
            this.latitudeToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // longitudeToolStripMenuItem
            // 
            this.longitudeToolStripMenuItem.CheckOnClick = true;
            this.longitudeToolStripMenuItem.Name = "longitudeToolStripMenuItem";
            this.longitudeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.longitudeToolStripMenuItem.Text = "Longitude";
            this.longitudeToolStripMenuItem.Click += new System.EventHandler(this.MacAddressToolStripMenuItemClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.FillWeight = 130F;
            this.dataGridViewTextBoxColumn2.HeaderText = "MAC Address";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 130;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.FillWeight = 150F;
            this.dataGridViewTextBoxColumn3.HeaderText = "SSID";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 32;
            this.dataGridViewTextBoxColumn3.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // sparkLineColumn1
            // 
            this.sparkLineColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sparkLineColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.sparkLineColumn1.FillWeight = 40F;
            this.sparkLineColumn1.HeaderText = "RSSI";
            this.sparkLineColumn1.MinimumWidth = 50;
            this.sparkLineColumn1.Name = "sparkLineColumn1";
            this.sparkLineColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sparkLineColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.sparkLineColumn1.Width = 82;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.dataGridViewTextBoxColumn4.FillWeight = 50F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Channel";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.FillWeight = 80F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Vendor";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.FillWeight = 80F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Security";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.FillWeight = 60F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Max Rate";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.dataGridViewTextBoxColumn8.HeaderText = "Network Type";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn9.FillWeight = 70F;
            this.dataGridViewTextBoxColumn9.HeaderText = "First Seen";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.FillWeight = 70F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Last Seen";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn11.FillWeight = 70F;
            this.dataGridViewTextBoxColumn11.HeaderText = "Latitude";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.FillWeight = 70F;
            this.dataGridViewTextBoxColumn12.HeaderText = "Longitude";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // ScannerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scannerGrid);
            this.Name = "ScannerView";
            this.Size = new System.Drawing.Size(972, 329);
            ((System.ComponentModel.ISupportInitialize)(this.scannerGrid)).EndInit();
            this.cmsColumns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView scannerGrid;
        private System.Windows.Forms.ContextMenuStrip cmsColumns;
        private System.Windows.Forms.ToolStripMenuItem mACAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sSIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rSSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maxRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firstSeenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastSeenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem latitudeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem longitudeToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn macColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ssidColumn;
        private SparkLineColumn rssiColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn channelColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxrateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn networktypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstseenColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lonColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private SparkLineColumn sparkLineColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}
