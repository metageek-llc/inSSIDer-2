namespace inSSIDer.UI.Controls
{
    partial class GpsMon
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
            this.lblLat = new System.Windows.Forms.Label();
            this.lblLon = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblAlt = new System.Windows.Forms.Label();
            this.lblPdop = new System.Windows.Forms.Label();
            this.lblHdop = new System.Windows.Forms.Label();
            this.lblVdop = new System.Windows.Forms.Label();
            this.lblFixType = new System.Windows.Forms.Label();
            this.lblPortName = new System.Windows.Forms.Label();
            this.lblNoGps = new System.Windows.Forms.Label();
            this.lblSatCount = new System.Windows.Forms.Label();
            this.gpsGraph1 = new inSSIDer.UI.Controls.GpsGraph();
            this.SuspendLayout();
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.ForeColor = System.Drawing.Color.White;
            this.lblLat.Location = new System.Drawing.Point(33, 28);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(48, 13);
            this.lblLat.TabIndex = 0;
            this.lblLat.Text = "Latitude:";
            this.lblLat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLon
            // 
            this.lblLon.AutoSize = true;
            this.lblLon.ForeColor = System.Drawing.Color.White;
            this.lblLon.Location = new System.Drawing.Point(24, 41);
            this.lblLon.Name = "lblLon";
            this.lblLon.Size = new System.Drawing.Size(57, 13);
            this.lblLon.TabIndex = 0;
            this.lblLon.Text = "Longitude:";
            this.lblLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.ForeColor = System.Drawing.Color.White;
            this.lblSpeed.Location = new System.Drawing.Point(6, 54);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(75, 13);
            this.lblSpeed.TabIndex = 0;
            this.lblSpeed.Text = "Speed (km/h):";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAlt
            // 
            this.lblAlt.AutoSize = true;
            this.lblAlt.ForeColor = System.Drawing.Color.White;
            this.lblAlt.Location = new System.Drawing.Point(36, 67);
            this.lblAlt.Name = "lblAlt";
            this.lblAlt.Size = new System.Drawing.Size(45, 13);
            this.lblAlt.TabIndex = 0;
            this.lblAlt.Text = "Altitude:";
            this.lblAlt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPdop
            // 
            this.lblPdop.AutoSize = true;
            this.lblPdop.ForeColor = System.Drawing.Color.White;
            this.lblPdop.Location = new System.Drawing.Point(41, 80);
            this.lblPdop.Name = "lblPdop";
            this.lblPdop.Size = new System.Drawing.Size(40, 13);
            this.lblPdop.TabIndex = 0;
            this.lblPdop.Text = "PDOP:";
            this.lblPdop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHdop
            // 
            this.lblHdop.AutoSize = true;
            this.lblHdop.ForeColor = System.Drawing.Color.White;
            this.lblHdop.Location = new System.Drawing.Point(40, 93);
            this.lblHdop.Name = "lblHdop";
            this.lblHdop.Size = new System.Drawing.Size(41, 13);
            this.lblHdop.TabIndex = 0;
            this.lblHdop.Text = "HDOP:";
            this.lblHdop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVdop
            // 
            this.lblVdop.AutoSize = true;
            this.lblVdop.ForeColor = System.Drawing.Color.White;
            this.lblVdop.Location = new System.Drawing.Point(41, 106);
            this.lblVdop.Name = "lblVdop";
            this.lblVdop.Size = new System.Drawing.Size(40, 13);
            this.lblVdop.TabIndex = 0;
            this.lblVdop.Text = "VDOP:";
            this.lblVdop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFixType
            // 
            this.lblFixType.AutoSize = true;
            this.lblFixType.ForeColor = System.Drawing.Color.White;
            this.lblFixType.Location = new System.Drawing.Point(31, 119);
            this.lblFixType.Name = "lblFixType";
            this.lblFixType.Size = new System.Drawing.Size(50, 13);
            this.lblFixType.TabIndex = 0;
            this.lblFixType.Text = "Fix Type:";
            this.lblFixType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.ForeColor = System.Drawing.Color.White;
            this.lblPortName.Location = new System.Drawing.Point(5, 5);
            this.lblPortName.Margin = new System.Windows.Forms.Padding(5);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(77, 13);
            this.lblPortName.TabIndex = 1;
            this.lblPortName.Text = "GPS on COM1";
            // 
            // lblNoGps
            // 
            this.lblNoGps.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoGps.ForeColor = System.Drawing.Color.Red;
            this.lblNoGps.Location = new System.Drawing.Point(5, 144);
            this.lblNoGps.Name = "lblNoGps";
            this.lblNoGps.Size = new System.Drawing.Size(58, 25);
            this.lblNoGps.TabIndex = 2;
            this.lblNoGps.Text = "GPS must be enabled to use this tab.";
            this.lblNoGps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoGps.Visible = false;
            // 
            // lblSatCount
            // 
            this.lblSatCount.AutoSize = true;
            this.lblSatCount.ForeColor = System.Drawing.Color.White;
            this.lblSatCount.Location = new System.Drawing.Point(0, 132);
            this.lblSatCount.Name = "lblSatCount";
            this.lblSatCount.Size = new System.Drawing.Size(81, 13);
            this.lblSatCount.TabIndex = 0;
            this.lblSatCount.Text = "Satellites (U/V):";
            this.lblSatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gpsGraph1
            // 
            this.gpsGraph1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpsGraph1.ForeColor = System.Drawing.Color.White;
            this.gpsGraph1.Location = new System.Drawing.Point(192, 0);
            this.gpsGraph1.Name = "gpsGraph1";
            this.gpsGraph1.Size = new System.Drawing.Size(608, 196);
            this.gpsGraph1.TabIndex = 3;
            // 
            // GpsMon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblNoGps);
            this.Controls.Add(this.gpsGraph1);
            this.Controls.Add(this.lblPortName);
            this.Controls.Add(this.lblSatCount);
            this.Controls.Add(this.lblFixType);
            this.Controls.Add(this.lblVdop);
            this.Controls.Add(this.lblHdop);
            this.Controls.Add(this.lblPdop);
            this.Controls.Add(this.lblAlt);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblLon);
            this.Controls.Add(this.lblLat);
            this.ForeColor = System.Drawing.Color.Lime;
            this.Name = "GpsMon";
            this.Size = new System.Drawing.Size(800, 196);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Label lblLon;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblAlt;
        private System.Windows.Forms.Label lblPdop;
        private System.Windows.Forms.Label lblHdop;
        private System.Windows.Forms.Label lblVdop;
        private System.Windows.Forms.Label lblFixType;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.Label lblNoGps;
        private System.Windows.Forms.Label lblSatCount;
        private inSSIDer.UI.Controls.GpsGraph gpsGraph1;
    }
}
