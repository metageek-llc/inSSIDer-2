namespace inSSIDer.UI.Forms
{
    /// <summary>
    /// 
    /// </summary>
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.okButton = new System.Windows.Forms.Button();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.aboutHeaderImage = new System.Windows.Forms.PictureBox();
            this.contributorsLabel = new System.Windows.Forms.Label();
            this.contributorsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.aboutHeaderImage)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // copyrightLabel
            // 
            resources.ApplyResources(this.copyrightLabel, "copyrightLabel");
            this.copyrightLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.copyrightLabel.Name = "copyrightLabel";
            // 
            // linkLabel
            // 
            resources.ApplyResources(this.linkLabel, "linkLabel");
            this.linkLabel.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.linkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.TabStop = true;
            this.linkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(51)))), ((int)(((byte)(0)))));
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLinkClicked);
            // 
            // versionLabel
            // 
            resources.ApplyResources(this.versionLabel, "versionLabel");
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.versionLabel.Name = "versionLabel";
            // 
            // aboutHeaderImage
            // 
            this.aboutHeaderImage.Image = global::inSSIDer.Properties.Resources.launch_inssider;
            resources.ApplyResources(this.aboutHeaderImage, "aboutHeaderImage");
            this.aboutHeaderImage.Name = "aboutHeaderImage";
            this.aboutHeaderImage.TabStop = false;
            this.aboutHeaderImage.Click += new System.EventHandler(this.AboutHeaderImageClick);
            // 
            // contributorsLabel
            // 
            resources.ApplyResources(this.contributorsLabel, "contributorsLabel");
            this.contributorsLabel.Name = "contributorsLabel";
            // 
            // contributorsTextBox
            // 
            resources.ApplyResources(this.contributorsTextBox, "contributorsTextBox");
            this.contributorsTextBox.Name = "contributorsTextBox";
            this.contributorsTextBox.ReadOnly = true;
            // 
            // formAbout
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.contributorsTextBox);
            this.Controls.Add(this.contributorsLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.aboutHeaderImage);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.aboutHeaderImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox aboutHeaderImage;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label contributorsLabel;
        private System.Windows.Forms.TextBox contributorsTextBox;
    }
}