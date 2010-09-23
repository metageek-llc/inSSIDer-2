namespace inSSIDer.Version
{
    partial class VersionUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionUpdateForm));
            this.downloadButton = new System.Windows.Forms.Button();
            this.installedVersionNameLabel = new System.Windows.Forms.Label();
            this.latestVersionLabel = new System.Windows.Forms.Label();
            this.latestVersionNameLabel = new System.Windows.Forms.Label();
            this.installedVersionLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.remindLaterButton = new System.Windows.Forms.Button();
            this.ignoreVersionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(293, 248);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(99, 23);
            this.downloadButton.TabIndex = 0;
            this.downloadButton.Text = "Download Now";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButtonClick);
            // 
            // installedVersionNameLabel
            // 
            this.installedVersionNameLabel.AutoSize = true;
            this.installedVersionNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.installedVersionNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.installedVersionNameLabel.Location = new System.Drawing.Point(15, 16);
            this.installedVersionNameLabel.Name = "installedVersionNameLabel";
            this.installedVersionNameLabel.Size = new System.Drawing.Size(87, 13);
            this.installedVersionNameLabel.TabIndex = 8;
            this.installedVersionNameLabel.Text = "Installed Version:";
            // 
            // latestVersionLabel
            // 
            this.latestVersionLabel.AutoSize = true;
            this.latestVersionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.latestVersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.latestVersionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.latestVersionLabel.Location = new System.Drawing.Point(108, 38);
            this.latestVersionLabel.Name = "latestVersionLabel";
            this.latestVersionLabel.Size = new System.Drawing.Size(58, 13);
            this.latestVersionLabel.TabIndex = 11;
            this.latestVersionLabel.Text = "2.1.4.345";
            // 
            // latestVersionNameLabel
            // 
            this.latestVersionNameLabel.AutoSize = true;
            this.latestVersionNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.latestVersionNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.latestVersionNameLabel.Location = new System.Drawing.Point(15, 38);
            this.latestVersionNameLabel.Name = "latestVersionNameLabel";
            this.latestVersionNameLabel.Size = new System.Drawing.Size(77, 13);
            this.latestVersionNameLabel.TabIndex = 9;
            this.latestVersionNameLabel.Text = "Latest Version:";
            // 
            // installedVersionLabel
            // 
            this.installedVersionLabel.AutoSize = true;
            this.installedVersionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.installedVersionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.installedVersionLabel.Location = new System.Drawing.Point(108, 16);
            this.installedVersionLabel.Name = "installedVersionLabel";
            this.installedVersionLabel.Size = new System.Drawing.Size(52, 13);
            this.installedVersionLabel.TabIndex = 10;
            this.installedVersionLabel.Text = "2.1.3.524";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(16, 68);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(376, 162);
            this.descriptionTextBox.TabIndex = 12;
            // 
            // remindLaterButton
            // 
            this.remindLaterButton.Location = new System.Drawing.Point(156, 248);
            this.remindLaterButton.Name = "remindLaterButton";
            this.remindLaterButton.Size = new System.Drawing.Size(110, 23);
            this.remindLaterButton.TabIndex = 13;
            this.remindLaterButton.Text = "Remind Me Later";
            this.remindLaterButton.UseVisualStyleBackColor = true;
            this.remindLaterButton.Click += new System.EventHandler(this.RemindLaterButtonClick);
            // 
            // ignoreVersionButton
            // 
            this.ignoreVersionButton.Location = new System.Drawing.Point(16, 248);
            this.ignoreVersionButton.Name = "ignoreVersionButton";
            this.ignoreVersionButton.Size = new System.Drawing.Size(113, 23);
            this.ignoreVersionButton.TabIndex = 14;
            this.ignoreVersionButton.Text = "Ignore This Version";
            this.ignoreVersionButton.UseVisualStyleBackColor = true;
            this.ignoreVersionButton.Click += new System.EventHandler(this.IgnoreVersionButtonClick);
            // 
            // VersionUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(419, 293);
            this.Controls.Add(this.ignoreVersionButton);
            this.Controls.Add(this.remindLaterButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.installedVersionNameLabel);
            this.Controls.Add(this.latestVersionLabel);
            this.Controls.Add(this.latestVersionNameLabel);
            this.Controls.Add(this.installedVersionLabel);
            this.Controls.Add(this.downloadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 321);
            this.Name = "VersionUpdateForm";
            this.Padding = new System.Windows.Forms.Padding(12, 12, 12, 50);
            this.Text = "UpdateAvailable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label installedVersionNameLabel;
        private System.Windows.Forms.Label latestVersionLabel;
        private System.Windows.Forms.Label latestVersionNameLabel;
        private System.Windows.Forms.Label installedVersionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Button remindLaterButton;
        private System.Windows.Forms.Button ignoreVersionButton;
    }
}