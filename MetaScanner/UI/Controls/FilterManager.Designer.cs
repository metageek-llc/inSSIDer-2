namespace inSSIDer.UI.Controls
{
    partial class FilterManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvFilters = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.exprColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel13ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channel14ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.is80211nToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uses40MHzChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.above90ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.above80ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.above70ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.above60ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.above50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.above40ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlyOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlySecuredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastFiltersUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFilters
            // 
            this.dgvFilters.AllowUserToAddRows = false;
            this.dgvFilters.AllowUserToDeleteRows = false;
            this.dgvFilters.AllowUserToOrderColumns = true;
            this.dgvFilters.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle9.NullValue = "null!";
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.DimGray;
            this.dgvFilters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvFilters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFilters.BackgroundColor = System.Drawing.Color.Black;
            this.dgvFilters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFilters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFilters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.checkColumn,
            this.exprColumn,
            this.matchColumn});
            this.dgvFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFilters.EnableHeadersVisualStyles = false;
            this.dgvFilters.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvFilters.Location = new System.Drawing.Point(0, 24);
            this.dgvFilters.Margin = new System.Windows.Forms.Padding(0);
            this.dgvFilters.MultiSelect = false;
            this.dgvFilters.Name = "dgvFilters";
            this.dgvFilters.ReadOnly = true;
            this.dgvFilters.RowHeadersVisible = false;
            this.dgvFilters.RowHeadersWidth = 50;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle12.NullValue = "null!";
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dgvFilters.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvFilters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilters.ShowEditingIcon = false;
            this.dgvFilters.Size = new System.Drawing.Size(606, 177);
            this.dgvFilters.TabIndex = 5;
            this.dgvFilters.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvFiltersCellMouseClick);
            this.dgvFilters.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DgvFiltersCellPainting);
            this.dgvFilters.SelectionChanged += new System.EventHandler(this.DgvFiltersSelectionChanged);
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
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.NullValue = false;
            this.checkColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.checkColumn.FillWeight = 20F;
            this.checkColumn.HeaderText = "Enabled";
            this.checkColumn.MinimumWidth = 55;
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.ReadOnly = true;
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.checkColumn.Width = 55;
            // 
            // exprColumn
            // 
            this.exprColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.exprColumn.HeaderText = "Expression";
            this.exprColumn.Name = "exprColumn";
            this.exprColumn.ReadOnly = true;
            // 
            // matchColumn
            // 
            this.matchColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.matchColumn.HeaderText = "Matches";
            this.matchColumn.MinimumWidth = 55;
            this.matchColumn.Name = "matchColumn";
            this.matchColumn.ReadOnly = true;
            this.matchColumn.Visible = false;
            this.matchColumn.Width = 55;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickFiltersToolStripMenuItem,
            this.createNewToolStripMenuItem,
            this.editSelectedToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.lastFiltersUsedToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(606, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.createNewToolStripMenuItem.Text = "Create New";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.CreateNewToolStripMenuItemClick);
            // 
            // editSelectedToolStripMenuItem
            // 
            this.editSelectedToolStripMenuItem.Enabled = false;
            this.editSelectedToolStripMenuItem.Name = "editSelectedToolStripMenuItem";
            this.editSelectedToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.editSelectedToolStripMenuItem.Text = "Edit Selected";
            this.editSelectedToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedToolStripMenuItemClick);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Enabled = false;
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.deleteSelectedToolStripMenuItem.Text = "Delete Selected";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItemClick);
            // 
            // quickFiltersToolStripMenuItem
            // 
            this.quickFiltersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelsToolStripMenuItem,
            this.nToolStripMenuItem,
            this.rSSIToolStripMenuItem,
            this.securityToolStripMenuItem});
            this.quickFiltersToolStripMenuItem.Name = "quickFiltersToolStripMenuItem";
            this.quickFiltersToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.quickFiltersToolStripMenuItem.Text = "Quick Filters";
            // 
            // channelsToolStripMenuItem
            // 
            this.channelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channel1ToolStripMenuItem,
            this.channel2ToolStripMenuItem,
            this.channel3ToolStripMenuItem,
            this.channel4ToolStripMenuItem,
            this.channel5ToolStripMenuItem,
            this.channel6ToolStripMenuItem,
            this.channel7ToolStripMenuItem,
            this.channel8ToolStripMenuItem,
            this.channel9ToolStripMenuItem,
            this.channel10ToolStripMenuItem,
            this.channel11ToolStripMenuItem,
            this.channel12ToolStripMenuItem,
            this.channel13ToolStripMenuItem,
            this.channel14ToolStripMenuItem});
            this.channelsToolStripMenuItem.Name = "channelsToolStripMenuItem";
            this.channelsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.channelsToolStripMenuItem.Text = "Channels";
            this.channelsToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ChannelsToolStripMenuItemDropDownItemClicked);
            // 
            // channel1ToolStripMenuItem
            // 
            this.channel1ToolStripMenuItem.Name = "channel1ToolStripMenuItem";
            this.channel1ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel1ToolStripMenuItem.Text = "Channel 1";
            // 
            // channel2ToolStripMenuItem
            // 
            this.channel2ToolStripMenuItem.Name = "channel2ToolStripMenuItem";
            this.channel2ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel2ToolStripMenuItem.Text = "Channel 2";
            // 
            // channel3ToolStripMenuItem
            // 
            this.channel3ToolStripMenuItem.Name = "channel3ToolStripMenuItem";
            this.channel3ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel3ToolStripMenuItem.Text = "Channel 3";
            // 
            // channel4ToolStripMenuItem
            // 
            this.channel4ToolStripMenuItem.Name = "channel4ToolStripMenuItem";
            this.channel4ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel4ToolStripMenuItem.Text = "Channel 4";
            // 
            // channel5ToolStripMenuItem
            // 
            this.channel5ToolStripMenuItem.Name = "channel5ToolStripMenuItem";
            this.channel5ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel5ToolStripMenuItem.Text = "Channel 5";
            // 
            // channel6ToolStripMenuItem
            // 
            this.channel6ToolStripMenuItem.Name = "channel6ToolStripMenuItem";
            this.channel6ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel6ToolStripMenuItem.Text = "Channel 6";
            // 
            // channel7ToolStripMenuItem
            // 
            this.channel7ToolStripMenuItem.Name = "channel7ToolStripMenuItem";
            this.channel7ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel7ToolStripMenuItem.Text = "Channel 7";
            // 
            // channel8ToolStripMenuItem
            // 
            this.channel8ToolStripMenuItem.Name = "channel8ToolStripMenuItem";
            this.channel8ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel8ToolStripMenuItem.Text = "Channel 8";
            // 
            // channel9ToolStripMenuItem
            // 
            this.channel9ToolStripMenuItem.Name = "channel9ToolStripMenuItem";
            this.channel9ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel9ToolStripMenuItem.Text = "Channel 9";
            // 
            // channel10ToolStripMenuItem
            // 
            this.channel10ToolStripMenuItem.Name = "channel10ToolStripMenuItem";
            this.channel10ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel10ToolStripMenuItem.Text = "Channel 10";
            // 
            // channel11ToolStripMenuItem
            // 
            this.channel11ToolStripMenuItem.Name = "channel11ToolStripMenuItem";
            this.channel11ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel11ToolStripMenuItem.Text = "Channel 11";
            // 
            // channel12ToolStripMenuItem
            // 
            this.channel12ToolStripMenuItem.Name = "channel12ToolStripMenuItem";
            this.channel12ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel12ToolStripMenuItem.Text = "Channel 12";
            // 
            // channel13ToolStripMenuItem
            // 
            this.channel13ToolStripMenuItem.Name = "channel13ToolStripMenuItem";
            this.channel13ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel13ToolStripMenuItem.Text = "Channel 13";
            // 
            // channel14ToolStripMenuItem
            // 
            this.channel14ToolStripMenuItem.Name = "channel14ToolStripMenuItem";
            this.channel14ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.channel14ToolStripMenuItem.Text = "Channel 14";
            // 
            // nToolStripMenuItem
            // 
            this.nToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.is80211nToolStripMenuItem,
            this.uses40MHzChannelToolStripMenuItem});
            this.nToolStripMenuItem.Name = "nToolStripMenuItem";
            this.nToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nToolStripMenuItem.Text = "802.11n";
            // 
            // is80211nToolStripMenuItem
            // 
            this.is80211nToolStripMenuItem.Name = "is80211nToolStripMenuItem";
            this.is80211nToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.is80211nToolStripMenuItem.Text = "Is 802.11n";
            this.is80211nToolStripMenuItem.Click += new System.EventHandler(this.Is80211NToolStripMenuItemClick);
            // 
            // uses40MHzChannelToolStripMenuItem
            // 
            this.uses40MHzChannelToolStripMenuItem.Name = "uses40MHzChannelToolStripMenuItem";
            this.uses40MHzChannelToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.uses40MHzChannelToolStripMenuItem.Text = "Uses 40MHz channel";
            this.uses40MHzChannelToolStripMenuItem.Click += new System.EventHandler(this.Uses40MHzChannelToolStripMenuItemClick);
            // 
            // rSSIToolStripMenuItem
            // 
            this.rSSIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.above90ToolStripMenuItem,
            this.above80ToolStripMenuItem,
            this.above70ToolStripMenuItem,
            this.above60ToolStripMenuItem,
            this.above50ToolStripMenuItem,
            this.above40ToolStripMenuItem});
            this.rSSIToolStripMenuItem.Name = "rSSIToolStripMenuItem";
            this.rSSIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rSSIToolStripMenuItem.Text = "RSSI";
            this.rSSIToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.RSsiToolStripMenuItemDropDownItemClicked);
            // 
            // above90ToolStripMenuItem
            // 
            this.above90ToolStripMenuItem.Name = "above90ToolStripMenuItem";
            this.above90ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.above90ToolStripMenuItem.Text = "Above -90";
            // 
            // above80ToolStripMenuItem
            // 
            this.above80ToolStripMenuItem.Name = "above80ToolStripMenuItem";
            this.above80ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.above80ToolStripMenuItem.Text = "Above -80";
            // 
            // above70ToolStripMenuItem
            // 
            this.above70ToolStripMenuItem.Name = "above70ToolStripMenuItem";
            this.above70ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.above70ToolStripMenuItem.Text = "Above -70";
            // 
            // above60ToolStripMenuItem
            // 
            this.above60ToolStripMenuItem.Name = "above60ToolStripMenuItem";
            this.above60ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.above60ToolStripMenuItem.Text = "Above -60";
            // 
            // above50ToolStripMenuItem
            // 
            this.above50ToolStripMenuItem.Name = "above50ToolStripMenuItem";
            this.above50ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.above50ToolStripMenuItem.Text = "Above -50";
            // 
            // above40ToolStripMenuItem
            // 
            this.above40ToolStripMenuItem.Name = "above40ToolStripMenuItem";
            this.above40ToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.above40ToolStripMenuItem.Text = "Above -40";
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlyOpenToolStripMenuItem,
            this.onlySecuredToolStripMenuItem});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.securityToolStripMenuItem.Text = "Security";
            // 
            // onlyOpenToolStripMenuItem
            // 
            this.onlyOpenToolStripMenuItem.Name = "onlyOpenToolStripMenuItem";
            this.onlyOpenToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.onlyOpenToolStripMenuItem.Text = "Only Open";
            this.onlyOpenToolStripMenuItem.Click += new System.EventHandler(this.OnlyOpenToolStripMenuItemClick);
            // 
            // onlySecuredToolStripMenuItem
            // 
            this.onlySecuredToolStripMenuItem.Name = "onlySecuredToolStripMenuItem";
            this.onlySecuredToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.onlySecuredToolStripMenuItem.Text = "Only Secured";
            this.onlySecuredToolStripMenuItem.Click += new System.EventHandler(this.OnlySecuredToolStripMenuItemClick);
            // 
            // lastFiltersUsedToolStripMenuItem
            // 
            this.lastFiltersUsedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.addAllToolStripMenuItem});
            this.lastFiltersUsedToolStripMenuItem.Name = "lastFiltersUsedToolStripMenuItem";
            this.lastFiltersUsedToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.lastFiltersUsedToolStripMenuItem.Text = "Last filters used";
            this.lastFiltersUsedToolStripMenuItem.Visible = false;
            this.lastFiltersUsedToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.LastFiltersUsedToolStripMenuItemDropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
            // 
            // addAllToolStripMenuItem
            // 
            this.addAllToolStripMenuItem.Name = "addAllToolStripMenuItem";
            this.addAllToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.addAllToolStripMenuItem.Text = "Add All";
            this.addAllToolStripMenuItem.Click += new System.EventHandler(this.AddAllToolStripMenuItemClick);
            // 
            // FilterMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.dgvFilters);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FilterManager";
            this.Size = new System.Drawing.Size(606, 201);
            this.Load += new System.EventHandler(this.FilterMgr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilters)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFilters;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exprColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn matchColumn;
        private System.Windows.Forms.ToolStripMenuItem quickFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel11ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel12ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel13ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem channel14ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem is80211nToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uses40MHzChannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rSSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem above90ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem above80ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem above70ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem above60ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem above50ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem above40ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlyOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlySecuredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastFiltersUsedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem addAllToolStripMenuItem;
    }
}
