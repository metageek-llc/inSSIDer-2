namespace inSSIDer.UI.Controls
{
    partial class NetworkInterfaceSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkInterfaceSelector));
            this.MyToolStrip = new System.Windows.Forms.ToolStrip();
            this.ScanButton = new System.Windows.Forms.ToolStripButton();
            this.NetworkInterfaceDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.MyToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyToolStrip
            // 
            this.MyToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScanButton,
            this.NetworkInterfaceDropDown});
            this.MyToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MyToolStrip.Name = "MyToolStrip";
            this.MyToolStrip.Size = new System.Drawing.Size(212, 25);
            this.MyToolStrip.TabIndex = 0;
            this.MyToolStrip.Text = "toolStrip1";
            // 
            // ScanButton
            // 
            this.ScanButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ScanButton.Image = global::inSSIDer.Properties.Resources.wifiPlay;
            this.ScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(51, 22);
            this.ScanButton.Text = "Start";
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // NetworkInterfaceDropDown
            // 
            this.NetworkInterfaceDropDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.NetworkInterfaceDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NetworkInterfaceDropDown.Image = ((System.Drawing.Image)(resources.GetObject("NetworkInterfaceDropDown.Image")));
            this.NetworkInterfaceDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NetworkInterfaceDropDown.Name = "NetworkInterfaceDropDown";
            this.NetworkInterfaceDropDown.Size = new System.Drawing.Size(127, 22);
            this.NetworkInterfaceDropDown.Text = "Select Wi-Fi Adapter";
            this.NetworkInterfaceDropDown.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NetworkInterfaceDropDown_DropDownItemClicked);
            // 
            // NetworkInterfaceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MyToolStrip);
            this.Name = "NetworkInterfaceSelector";
            this.Size = new System.Drawing.Size(212, 25);
            this.MyToolStrip.ResumeLayout(false);
            this.MyToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MyToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton NetworkInterfaceDropDown;
        private System.Windows.Forms.ToolStripButton ScanButton;
    }
}
