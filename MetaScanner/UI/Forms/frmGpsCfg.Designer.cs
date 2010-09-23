using inSSIDer.UI.Controls;

namespace inSSIDer.UI.Forms
{
    partial class FormGpsCfg
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
            this.btnClose = new GrayButton();
            this.btnSave = new GrayButton();
            this.gbSerialPort = new GrayGroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbHandshake = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.pSpacer = new System.Windows.Forms.Panel();
            this.cbPortname = new System.Windows.Forms.ComboBox();
            this.numBaudrate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbSerialPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaudrate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(59, 135);
            this.btnClose.Margin = new System.Windows.Forms.Padding(50, 10, 3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(207, 135);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 10, 50, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // gbSerialPort
            // 
            this.gbSerialPort.BorderColor = System.Drawing.Color.Gray;
            this.gbSerialPort.Controls.Add(this.label4);
            this.gbSerialPort.Controls.Add(this.label7);
            this.gbSerialPort.Controls.Add(this.label6);
            this.gbSerialPort.Controls.Add(this.label5);
            this.gbSerialPort.Controls.Add(this.cbHandshake);
            this.gbSerialPort.Controls.Add(this.cbParity);
            this.gbSerialPort.Controls.Add(this.cbDataBits);
            this.gbSerialPort.Controls.Add(this.cbStopBits);
            this.gbSerialPort.Controls.Add(this.pSpacer);
            this.gbSerialPort.Controls.Add(this.cbPortname);
            this.gbSerialPort.Controls.Add(this.numBaudrate);
            this.gbSerialPort.Controls.Add(this.label3);
            this.gbSerialPort.Controls.Add(this.label2);
            this.gbSerialPort.Controls.Add(this.label1);
            this.gbSerialPort.Location = new System.Drawing.Point(9, 9);
            this.gbSerialPort.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.gbSerialPort.Name = "gbSerialPort";
            this.gbSerialPort.Size = new System.Drawing.Size(324, 113);
            this.gbSerialPort.TabIndex = 2;
            this.gbSerialPort.TabStop = false;
            this.gbSerialPort.Text = "Serial Port Configuration";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Handshake:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(169, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 21);
            this.label7.TabIndex = 7;
            this.label7.Text = "Parity:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(166, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 21);
            this.label6.TabIndex = 7;
            this.label6.Text = "Data bits:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Stop bits:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbHandshake
            // 
            this.cbHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHandshake.FormattingEnabled = true;
            this.cbHandshake.Items.AddRange(new object[] {
            "None",
            "Software",
            "Hardware",
            "Both"});
            this.cbHandshake.Location = new System.Drawing.Point(78, 82);
            this.cbHandshake.Name = "cbHandshake";
            this.cbHandshake.Size = new System.Drawing.Size(84, 21);
            this.cbHandshake.TabIndex = 6;
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbParity.Location = new System.Drawing.Point(228, 82);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(84, 21);
            this.cbParity.TabIndex = 6;
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBits.Location = new System.Drawing.Point(228, 55);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(84, 21);
            this.cbDataBits.TabIndex = 6;
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Items.AddRange(new object[] {
            "None",
            "1",
            "2",
            "1.5"});
            this.cbStopBits.Location = new System.Drawing.Point(78, 55);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(84, 21);
            this.cbStopBits.TabIndex = 6;
            // 
            // pSpacer
            // 
            this.pSpacer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pSpacer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pSpacer.Location = new System.Drawing.Point(6, 48);
            this.pSpacer.Name = "pSpacer";
            this.pSpacer.Size = new System.Drawing.Size(312, 1);
            this.pSpacer.TabIndex = 4;
            // 
            // cbPortname
            // 
            this.cbPortname.FormattingEnabled = true;
            this.cbPortname.Location = new System.Drawing.Point(78, 23);
            this.cbPortname.Name = "cbPortname";
            this.cbPortname.Size = new System.Drawing.Size(84, 21);
            this.cbPortname.TabIndex = 3;
            // 
            // numBaudrate
            // 
            this.numBaudrate.Increment = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numBaudrate.Location = new System.Drawing.Point(228, 24);
            this.numBaudrate.Maximum = new decimal(new int[] {
            38400,
            0,
            0,
            0});
            this.numBaudrate.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numBaudrate.Name = "numBaudrate";
            this.numBaudrate.Size = new System.Drawing.Size(84, 20);
            this.numBaudrate.TabIndex = 2;
            this.numBaudrate.Value = new decimal(new int[] {
            4800,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(166, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Baud rate:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial port Configuration";
            // 
            // formGpsCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(341, 171);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbSerialPort);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGpsCfg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure GPS";
            this.Load += new System.EventHandler(this.FormGpsCfgLoad);
            this.gbSerialPort.ResumeLayout(false);
            this.gbSerialPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaudrate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GrayGroupBox gbSerialPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbHandshake;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Panel pSpacer;
        private System.Windows.Forms.ComboBox cbPortname;
        private System.Windows.Forms.NumericUpDown numBaudrate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbParity;
        private GrayButton btnSave;
        private GrayButton btnClose;
    }
}