using inSSIDer.UI.Controls;

namespace inSSIDer.UI.Forms
{
    partial class FormLogConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogConverter));
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.fbOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new inSSIDer.UI.Controls.GrayButton();
            this.btnExport = new inSSIDer.UI.Controls.GrayButton();
            this.grayGroupBox3 = new inSSIDer.UI.Controls.GrayGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numMaxSignal = new System.Windows.Forms.NumericUpDown();
            this.numMaxSpeed = new System.Windows.Forms.NumericUpDown();
            this.numSatCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.chMaxSignal = new System.Windows.Forms.CheckBox();
            this.lbldataq = new System.Windows.Forms.Label();
            this.chMaxSpeed = new System.Windows.Forms.CheckBox();
            this.chGPSsatCount = new System.Windows.Forms.CheckBox();
            this.chGPSFixLost = new System.Windows.Forms.CheckBox();
            this.chGPSLockup = new System.Windows.Forms.CheckBox();
            this.grayGroupBox2 = new inSSIDer.UI.Controls.GrayGroupBox();
            this.lblVis = new System.Windows.Forms.Label();
            this.chShowRssiMarkers = new System.Windows.Forms.CheckBox();
            this.grayGroupBox1 = new inSSIDer.UI.Controls.GrayGroupBox();
            this.cmbOrganize = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chExportComp = new System.Windows.Forms.CheckBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.chExportEachAp = new System.Windows.Forms.CheckBox();
            this.btnChangeInFiles = new inSSIDer.UI.Controls.GrayButton();
            this.chExportSummary = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeOutdir = new inSSIDer.UI.Controls.GrayButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutDir = new System.Windows.Forms.TextBox();
            this.txtInFiles = new System.Windows.Forms.TextBox();
            this.grayGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSatCount)).BeginInit();
            this.grayGroupBox2.SuspendLayout();
            this.grayGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "*.gpx";
            this.openFile.Filter = "GPX logs (*.gpx)|*.gpx|XML files (*.xml)|*.xml";
            this.openFile.Multiselect = true;
            this.openFile.SupportMultiDottedExtensions = true;
            this.openFile.Title = "Select log files to load";
            // 
            // fbOutput
            // 
            this.fbOutput.Description = "Select the folder to place the output KML files.";
            this.fbOutput.SelectedPath = ".";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(59, 430);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(50, 3, 3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Done";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(241, 430);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 3, 50, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportButtonClick);
            // 
            // grayGroupBox3
            // 
            this.grayGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grayGroupBox3.BorderColor = System.Drawing.Color.Gray;
            this.grayGroupBox3.Controls.Add(this.label6);
            this.grayGroupBox3.Controls.Add(this.label5);
            this.grayGroupBox3.Controls.Add(this.label4);
            this.grayGroupBox3.Controls.Add(this.numMaxSignal);
            this.grayGroupBox3.Controls.Add(this.numMaxSpeed);
            this.grayGroupBox3.Controls.Add(this.numSatCount);
            this.grayGroupBox3.Controls.Add(this.label3);
            this.grayGroupBox3.Controls.Add(this.chMaxSignal);
            this.grayGroupBox3.Controls.Add(this.lbldataq);
            this.grayGroupBox3.Controls.Add(this.chMaxSpeed);
            this.grayGroupBox3.Controls.Add(this.chGPSsatCount);
            this.grayGroupBox3.Controls.Add(this.chGPSFixLost);
            this.grayGroupBox3.Controls.Add(this.chGPSLockup);
            this.grayGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grayGroupBox3.ForeColor = System.Drawing.Color.White;
            this.grayGroupBox3.Location = new System.Drawing.Point(12, 248);
            this.grayGroupBox3.Name = "grayGroupBox3";
            this.grayGroupBox3.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.grayGroupBox3.Size = new System.Drawing.Size(351, 161);
            this.grayGroupBox3.TabIndex = 6;
            this.grayGroupBox3.TabStop = false;
            this.grayGroupBox3.Text = "DataQ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(241, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "dB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "km/h";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "satellite(s) were visible";
            // 
            // numMaxSignal
            // 
            this.numMaxSignal.BackColor = System.Drawing.Color.Black;
            this.numMaxSignal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numMaxSignal.ForeColor = System.Drawing.Color.White;
            this.numMaxSignal.Location = new System.Drawing.Point(174, 132);
            this.numMaxSignal.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.numMaxSignal.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMaxSignal.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numMaxSignal.Name = "numMaxSignal";
            this.numMaxSignal.Size = new System.Drawing.Size(60, 20);
            this.numMaxSignal.TabIndex = 9;
            this.numMaxSignal.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // numMaxSpeed
            // 
            this.numMaxSpeed.BackColor = System.Drawing.Color.Black;
            this.numMaxSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numMaxSpeed.ForeColor = System.Drawing.Color.White;
            this.numMaxSpeed.Location = new System.Drawing.Point(159, 106);
            this.numMaxSpeed.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.numMaxSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxSpeed.Name = "numMaxSpeed";
            this.numMaxSpeed.Size = new System.Drawing.Size(60, 20);
            this.numMaxSpeed.TabIndex = 9;
            // 
            // numSatCount
            // 
            this.numSatCount.BackColor = System.Drawing.Color.Black;
            this.numSatCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSatCount.ForeColor = System.Drawing.Color.White;
            this.numSatCount.Location = new System.Drawing.Point(84, 83);
            this.numSatCount.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.numSatCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSatCount.Name = "numSatCount";
            this.numSatCount.Size = new System.Drawing.Size(60, 20);
            this.numSatCount.TabIndex = 9;
            this.numSatCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ignore data points if...";
            // 
            // chMaxSignal
            // 
            this.chMaxSignal.AutoSize = true;
            this.chMaxSignal.Checked = true;
            this.chMaxSignal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chMaxSignal.Location = new System.Drawing.Point(9, 133);
            this.chMaxSignal.Name = "chMaxSignal";
            this.chMaxSignal.Size = new System.Drawing.Size(162, 17);
            this.chMaxSignal.TabIndex = 5;
            this.chMaxSignal.Text = "The signal was stronger than";
            this.chMaxSignal.UseVisualStyleBackColor = true;
            // 
            // lbldataq
            // 
            this.lbldataq.AutoSize = true;
            this.lbldataq.ForeColor = System.Drawing.Color.White;
            this.lbldataq.Location = new System.Drawing.Point(6, 0);
            this.lbldataq.Name = "lbldataq";
            this.lbldataq.Size = new System.Drawing.Size(65, 13);
            this.lbldataq.TabIndex = 0;
            this.lbldataq.Text = "Data Quality";
            // 
            // chMaxSpeed
            // 
            this.chMaxSpeed.AutoSize = true;
            this.chMaxSpeed.Location = new System.Drawing.Point(9, 107);
            this.chMaxSpeed.Name = "chMaxSpeed";
            this.chMaxSpeed.Size = new System.Drawing.Size(147, 17);
            this.chMaxSpeed.TabIndex = 5;
            this.chMaxSpeed.Text = "I was traveling faster than";
            this.chMaxSpeed.UseVisualStyleBackColor = true;
            // 
            // chGPSsatCount
            // 
            this.chGPSsatCount.AutoSize = true;
            this.chGPSsatCount.Checked = true;
            this.chGPSsatCount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chGPSsatCount.Location = new System.Drawing.Point(9, 84);
            this.chGPSsatCount.Name = "chGPSsatCount";
            this.chGPSsatCount.Size = new System.Drawing.Size(72, 17);
            this.chGPSsatCount.TabIndex = 5;
            this.chGPSsatCount.Text = "Less than";
            this.chGPSsatCount.UseVisualStyleBackColor = true;
            // 
            // chGPSFixLost
            // 
            this.chGPSFixLost.AutoSize = true;
            this.chGPSFixLost.Checked = true;
            this.chGPSFixLost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chGPSFixLost.Location = new System.Drawing.Point(9, 61);
            this.chGPSFixLost.Name = "chGPSFixLost";
            this.chGPSFixLost.Size = new System.Drawing.Size(162, 17);
            this.chGPSFixLost.TabIndex = 5;
            this.chGPSFixLost.Text = "The GPS satellite fix was lost";
            this.chGPSFixLost.UseVisualStyleBackColor = true;
            // 
            // chGPSLockup
            // 
            this.chGPSLockup.AutoSize = true;
            this.chGPSLockup.Checked = true;
            this.chGPSLockup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chGPSLockup.Location = new System.Drawing.Point(9, 38);
            this.chGPSLockup.Name = "chGPSLockup";
            this.chGPSLockup.Size = new System.Drawing.Size(235, 17);
            this.chGPSLockup.TabIndex = 5;
            this.chGPSLockup.Text = "The GPS device appears to have locked up";
            this.chGPSLockup.UseVisualStyleBackColor = true;
            // 
            // grayGroupBox2
            // 
            this.grayGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grayGroupBox2.BorderColor = System.Drawing.Color.Gray;
            this.grayGroupBox2.Controls.Add(this.lblVis);
            this.grayGroupBox2.Controls.Add(this.chShowRssiMarkers);
            this.grayGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grayGroupBox2.ForeColor = System.Drawing.Color.White;
            this.grayGroupBox2.Location = new System.Drawing.Point(12, 189);
            this.grayGroupBox2.Name = "grayGroupBox2";
            this.grayGroupBox2.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.grayGroupBox2.Size = new System.Drawing.Size(351, 53);
            this.grayGroupBox2.TabIndex = 2;
            this.grayGroupBox2.TabStop = false;
            this.grayGroupBox2.Text = "Files";
            // 
            // lblVis
            // 
            this.lblVis.AutoSize = true;
            this.lblVis.ForeColor = System.Drawing.Color.White;
            this.lblVis.Location = new System.Drawing.Point(6, 0);
            this.lblVis.Name = "lblVis";
            this.lblVis.Size = new System.Drawing.Size(65, 13);
            this.lblVis.TabIndex = 0;
            this.lblVis.Text = "Visualization";
            // 
            // chShowRssiMarkers
            // 
            this.chShowRssiMarkers.AutoSize = true;
            this.chShowRssiMarkers.Location = new System.Drawing.Point(9, 22);
            this.chShowRssiMarkers.Name = "chShowRssiMarkers";
            this.chShowRssiMarkers.Size = new System.Drawing.Size(186, 17);
            this.chShowRssiMarkers.TabIndex = 5;
            this.chShowRssiMarkers.Text = "Show RSSI labels next to markers";
            this.chShowRssiMarkers.UseVisualStyleBackColor = true;
            // 
            // grayGroupBox1
            // 
            this.grayGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grayGroupBox1.BorderColor = System.Drawing.Color.Gray;
            this.grayGroupBox1.Controls.Add(this.cmbOrganize);
            this.grayGroupBox1.Controls.Add(this.label7);
            this.grayGroupBox1.Controls.Add(this.chExportComp);
            this.grayGroupBox1.Controls.Add(this.lblFiles);
            this.grayGroupBox1.Controls.Add(this.chExportEachAp);
            this.grayGroupBox1.Controls.Add(this.btnChangeInFiles);
            this.grayGroupBox1.Controls.Add(this.chExportSummary);
            this.grayGroupBox1.Controls.Add(this.label1);
            this.grayGroupBox1.Controls.Add(this.btnChangeOutdir);
            this.grayGroupBox1.Controls.Add(this.label2);
            this.grayGroupBox1.Controls.Add(this.txtOutDir);
            this.grayGroupBox1.Controls.Add(this.txtInFiles);
            this.grayGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grayGroupBox1.ForeColor = System.Drawing.Color.White;
            this.grayGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.grayGroupBox1.Name = "grayGroupBox1";
            this.grayGroupBox1.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.grayGroupBox1.Size = new System.Drawing.Size(351, 171);
            this.grayGroupBox1.TabIndex = 2;
            this.grayGroupBox1.TabStop = false;
            this.grayGroupBox1.Text = "Files";
            // 
            // cmbOrganize
            // 
            this.cmbOrganize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrganize.FormattingEnabled = true;
            this.cmbOrganize.Items.AddRange(new object[] {
            "Encryption, Channel",
            "Channel, Encryption"});
            this.cmbOrganize.Location = new System.Drawing.Point(140, 134);
            this.cmbOrganize.Name = "cmbOrganize";
            this.cmbOrganize.Size = new System.Drawing.Size(121, 21);
            this.cmbOrganize.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 137);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Organize by";
            // 
            // chExportComp
            // 
            this.chExportComp.AutoSize = true;
            this.chExportComp.Location = new System.Drawing.Point(9, 111);
            this.chExportComp.Name = "chExportComp";
            this.chExportComp.Size = new System.Drawing.Size(168, 17);
            this.chExportComp.TabIndex = 5;
            this.chExportComp.Text = "Export Comprehensive Kml file";
            this.chExportComp.UseVisualStyleBackColor = true;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.ForeColor = System.Drawing.Color.White;
            this.lblFiles.Location = new System.Drawing.Point(6, 0);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(28, 13);
            this.lblFiles.TabIndex = 0;
            this.lblFiles.Text = "Files";
            // 
            // chExportEachAp
            // 
            this.chExportEachAp.AutoSize = true;
            this.chExportEachAp.ForeColor = System.Drawing.Color.White;
            this.chExportEachAp.Location = new System.Drawing.Point(9, 88);
            this.chExportEachAp.Name = "chExportEachAp";
            this.chExportEachAp.Size = new System.Drawing.Size(230, 17);
            this.chExportEachAp.TabIndex = 5;
            this.chExportEachAp.Text = "Export each access point to its own Kml file";
            this.chExportEachAp.UseVisualStyleBackColor = true;
            // 
            // btnChangeInFiles
            // 
            this.btnChangeInFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeInFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeInFiles.ForeColor = System.Drawing.Color.Black;
            this.btnChangeInFiles.Location = new System.Drawing.Point(315, 13);
            this.btnChangeInFiles.Margin = new System.Windows.Forms.Padding(3, 3, 4, 3);
            this.btnChangeInFiles.Name = "btnChangeInFiles";
            this.btnChangeInFiles.Size = new System.Drawing.Size(29, 20);
            this.btnChangeInFiles.TabIndex = 2;
            this.btnChangeInFiles.Text = "...";
            this.btnChangeInFiles.UseVisualStyleBackColor = true;
            this.btnChangeInFiles.Click += new System.EventHandler(this.ChangeInputFilesButtonClick);
            // 
            // chExportSummary
            // 
            this.chExportSummary.AutoSize = true;
            this.chExportSummary.Checked = true;
            this.chExportSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chExportSummary.Location = new System.Drawing.Point(9, 65);
            this.chExportSummary.Name = "chExportSummary";
            this.chExportSummary.Size = new System.Drawing.Size(121, 17);
            this.chExportSummary.TabIndex = 5;
            this.chExportSummary.Text = "Export Summary File";
            this.chExportSummary.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input files:";
            // 
            // btnChangeOutdir
            // 
            this.btnChangeOutdir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeOutdir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeOutdir.ForeColor = System.Drawing.Color.Black;
            this.btnChangeOutdir.Location = new System.Drawing.Point(315, 39);
            this.btnChangeOutdir.Margin = new System.Windows.Forms.Padding(3, 3, 4, 3);
            this.btnChangeOutdir.Name = "btnChangeOutdir";
            this.btnChangeOutdir.Size = new System.Drawing.Size(29, 20);
            this.btnChangeOutdir.TabIndex = 4;
            this.btnChangeOutdir.Text = "...";
            this.btnChangeOutdir.UseVisualStyleBackColor = true;
            this.btnChangeOutdir.Click += new System.EventHandler(this.ChangeOutputDirectoryButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output folder:";
            // 
            // txtOutDir
            // 
            this.txtOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtOutDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutDir.ForeColor = System.Drawing.Color.White;
            this.txtOutDir.Location = new System.Drawing.Point(83, 39);
            this.txtOutDir.Name = "txtOutDir";
            this.txtOutDir.Size = new System.Drawing.Size(226, 20);
            this.txtOutDir.TabIndex = 3;
            // 
            // txtInFiles
            // 
            this.txtInFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtInFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInFiles.ForeColor = System.Drawing.Color.White;
            this.txtInFiles.Location = new System.Drawing.Point(83, 13);
            this.txtInFiles.Name = "txtInFiles";
            this.txtInFiles.ReadOnly = true;
            this.txtInFiles.Size = new System.Drawing.Size(226, 20);
            this.txtInFiles.TabIndex = 2;
            // 
            // FormLogConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(375, 474);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.grayGroupBox3);
            this.Controls.Add(this.grayGroupBox2);
            this.Controls.Add(this.grayGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogConverter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GPX log converter";
            this.Load += new System.EventHandler(this.FormLogConverterLoad);
            this.Shown += new System.EventHandler(this.FormLogConverterShown);
            this.grayGroupBox3.ResumeLayout(false);
            this.grayGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSatCount)).EndInit();
            this.grayGroupBox2.ResumeLayout(false);
            this.grayGroupBox2.PerformLayout();
            this.grayGroupBox1.ResumeLayout(false);
            this.grayGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private GrayButton btnChangeOutdir;
        private System.Windows.Forms.TextBox txtOutDir;
        private GrayButton btnChangeInFiles;
        private System.Windows.Forms.TextBox txtInFiles;
        private System.Windows.Forms.CheckBox chExportComp;
        private System.Windows.Forms.CheckBox chExportEachAp;
        private System.Windows.Forms.CheckBox chExportSummary;
        private GrayGroupBox grayGroupBox1;
        private System.Windows.Forms.Label lblFiles;
        private GrayGroupBox grayGroupBox2;
        private System.Windows.Forms.Label lblVis;
        private System.Windows.Forms.CheckBox chShowRssiMarkers;
        private System.Windows.Forms.Label lbldataq;
        private GrayGroupBox grayGroupBox3;
        private System.Windows.Forms.CheckBox chGPSLockup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chGPSsatCount;
        private System.Windows.Forms.CheckBox chGPSFixLost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSatCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numMaxSpeed;
        private System.Windows.Forms.CheckBox chMaxSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numMaxSignal;
        private System.Windows.Forms.CheckBox chMaxSignal;
        private System.Windows.Forms.ComboBox cmbOrganize;
        private System.Windows.Forms.Label label7;
        private GrayButton btnExport;
        private GrayButton btnCancel;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.FolderBrowserDialog fbOutput;
    }
}