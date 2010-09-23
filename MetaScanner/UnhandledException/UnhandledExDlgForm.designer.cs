namespace inSSIDer.UnhandledException
{
    partial class UnhandledExDlgForm
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
            if(disposing && (components != null))
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelDevider = new System.Windows.Forms.Panel();
            this.labelExceptionDate = new System.Windows.Forms.Label();
            this.labelCaption = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonNotSend = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelLinkTitle = new System.Windows.Forms.Label();
            this.linkLabelData = new System.Windows.Forms.LinkLabel();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Window;
            this.panelTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(396, 81);
            this.panelTop.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelTitle.Location = new System.Drawing.Point(18, 56);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(360, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "{0} encountered a problem and needed to close";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDevider
            // 
            this.panelDevider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDevider.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDevider.Location = new System.Drawing.Point(0, 81);
            this.panelDevider.Name = "panelDevider";
            this.panelDevider.Size = new System.Drawing.Size(396, 2);
            this.panelDevider.TabIndex = 1;
            // 
            // labelExceptionDate
            // 
            this.labelExceptionDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExceptionDate.Location = new System.Drawing.Point(12, 90);
            this.labelExceptionDate.Name = "labelExceptionDate";
            this.labelExceptionDate.Size = new System.Drawing.Size(384, 23);
            this.labelExceptionDate.TabIndex = 2;
            this.labelExceptionDate.Text = "This error occured on {0}";
            // 
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaption.Location = new System.Drawing.Point(13, 113);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(387, 23);
            this.labelCaption.TabIndex = 3;
            this.labelCaption.Text = "Please tell MetaGeek about this problem";
            // 
            // labelDescription
            // 
            this.labelDescription.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.labelDescription.Location = new System.Drawing.Point(12, 136);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(387, 29);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "We have created an error report that you can send us. We will treat this report a" +
                "s confidential.";
            // 
            // buttonNotSend
            // 
            this.buttonNotSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNotSend.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonNotSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonNotSend.Location = new System.Drawing.Point(166, 222);
            this.buttonNotSend.Name = "buttonNotSend";
            this.buttonNotSend.Size = new System.Drawing.Size(75, 23);
            this.buttonNotSend.TabIndex = 1;
            this.buttonNotSend.Text = "&Don\'t send";
            this.buttonNotSend.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonSend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(247, 222);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(131, 23);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "&Send Error Report";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // labelLinkTitle
            // 
            this.labelLinkTitle.AutoSize = true;
            this.labelLinkTitle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.labelLinkTitle.Location = new System.Drawing.Point(13, 173);
            this.labelLinkTitle.Name = "labelLinkTitle";
            this.labelLinkTitle.Size = new System.Drawing.Size(252, 13);
            this.labelLinkTitle.TabIndex = 7;
            this.labelLinkTitle.Text = "To see what data this error report contains, please";
            // 
            // linkLabelData
            // 
            this.linkLabelData.AutoSize = true;
            this.linkLabelData.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.linkLabelData.Location = new System.Drawing.Point(262, 173);
            this.linkLabelData.Name = "linkLabelData";
            this.linkLabelData.Size = new System.Drawing.Size(55, 13);
            this.linkLabelData.TabIndex = 8;
            this.linkLabelData.TabStop = true;
            this.linkLabelData.Text = "click here.";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopy.Location = new System.Drawing.Point(15, 222);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(107, 23);
            this.buttonCopy.TabIndex = 9;
            this.buttonCopy.Text = "&Copy to Clipboard";
            this.buttonCopy.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::inSSIDer.Properties.Resources.metageek_logo;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 46);
            this.label1.TabIndex = 1;
            // 
            // UnhandledExDlgForm
            // 
            this.AcceptButton = this.buttonSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonNotSend;
            this.ClientSize = new System.Drawing.Size(396, 257);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.linkLabelData);
            this.Controls.Add(this.labelLinkTitle);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonNotSend);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.labelExceptionDate);
            this.Controls.Add(this.panelDevider);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnhandledExDlgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UnhandledExDlgForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UnhandledExDlgForm_Load);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelDevider;
        private System.Windows.Forms.Label labelExceptionDate;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Label labelDescription;
        internal System.Windows.Forms.Label labelTitle;
        internal System.Windows.Forms.Label labelLinkTitle;
        internal System.Windows.Forms.LinkLabel linkLabelData;
        internal System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonNotSend;
        internal System.Windows.Forms.Button buttonCopy;
        internal System.Windows.Forms.Label label1;
    }
}