using inSSIDer.UI.Controls;

namespace inSSIDer.UI.Forms
{
    partial class FormFilterBuilder
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
            this.textExpression = new System.Windows.Forms.TextBox();
            this.expressionLabel = new System.Windows.Forms.Label();
            this.textValue = new System.Windows.Forms.TextBox();
            this.textValueLabel = new System.Windows.Forms.Label();
            this.valuesLabel = new System.Windows.Forms.ListBox();
            this.numberValue = new System.Windows.Forms.NumericUpDown();
            this.numberValueLabel = new System.Windows.Forms.Label();
            this.fixedValueLabel = new System.Windows.Forms.Label();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.vendorButton = new GrayRadioButton();
            this.ageButton = new GrayRadioButton();
            this.ssidButton = new GrayRadioButton();
            this.maxRateButton = new GrayRadioButton();
            this.rssiButton = new GrayRadioButton();
            this.macButton = new GrayRadioButton();
            this.securityButton = new GrayRadioButton();
            this.is40MHzButton = new GrayRadioButton();
            this.networkTypeButton = new GrayRadioButton();
            this.isNButton = new GrayRadioButton();
            this.channelButton = new GrayRadioButton();
            this.operationsPanel = new System.Windows.Forms.Panel();
            this.EndsWithButton = new GrayRadioButton();
            this.StartsWithButton = new GrayRadioButton();
            this.lessThanOrEqualButton = new GrayRadioButton();
            this.greaterThanOrEqualButton = new GrayRadioButton();
            this.lessThanButton = new GrayRadioButton();
            this.greaterThanButton = new GrayRadioButton();
            this.notEqualButton = new GrayRadioButton();
            this.equalButton = new GrayRadioButton();
            this.addExpressionButton = new GrayButton();
            this.removeButton = new GrayButton();
            this.acceptButton = new GrayButton();
            ((System.ComponentModel.ISupportInitialize)(this.numberValue)).BeginInit();
            this.propertiesPanel.SuspendLayout();
            this.operationsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textExpression
            // 
            this.textExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textExpression.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.textExpression.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textExpression.ForeColor = System.Drawing.Color.Lime;
            this.textExpression.Location = new System.Drawing.Point(74, 14);
            this.textExpression.Name = "textExpression";
            this.textExpression.Size = new System.Drawing.Size(311, 20);
            this.textExpression.TabIndex = 0;
            this.textExpression.TextChanged += new System.EventHandler(this.TextExpressionTextChanged);
            // 
            // expressionLabel
            // 
            this.expressionLabel.AutoSize = true;
            this.expressionLabel.Location = new System.Drawing.Point(7, 17);
            this.expressionLabel.Name = "expressionLabel";
            this.expressionLabel.Size = new System.Drawing.Size(61, 13);
            this.expressionLabel.TabIndex = 1;
            this.expressionLabel.Text = "Expression:";
            // 
            // textValue
            // 
            this.textValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.textValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textValue.ForeColor = System.Drawing.Color.Lime;
            this.textValue.Location = new System.Drawing.Point(353, 60);
            this.textValue.Name = "textValue";
            this.textValue.Size = new System.Drawing.Size(120, 20);
            this.textValue.TabIndex = 5;
            this.textValue.Visible = false;
            // 
            // textValueLabel
            // 
            this.textValueLabel.AutoSize = true;
            this.textValueLabel.Location = new System.Drawing.Point(316, 63);
            this.textValueLabel.Name = "textValueLabel";
            this.textValueLabel.Size = new System.Drawing.Size(31, 13);
            this.textValueLabel.TabIndex = 6;
            this.textValueLabel.Text = "Text:";
            this.textValueLabel.Visible = false;
            // 
            // valuesLabel
            // 
            this.valuesLabel.BackColor = System.Drawing.Color.Black;
            this.valuesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valuesLabel.ForeColor = System.Drawing.Color.Lime;
            this.valuesLabel.FormattingEnabled = true;
            this.valuesLabel.Location = new System.Drawing.Point(353, 125);
            this.valuesLabel.Name = "valuesLabel";
            this.valuesLabel.Size = new System.Drawing.Size(120, 106);
            this.valuesLabel.TabIndex = 7;
            this.valuesLabel.Visible = false;
            // 
            // numberValue
            // 
            this.numberValue.BackColor = System.Drawing.Color.Black;
            this.numberValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberValue.ForeColor = System.Drawing.Color.Lime;
            this.numberValue.Location = new System.Drawing.Point(353, 89);
            this.numberValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numberValue.Name = "numberValue";
            this.numberValue.Size = new System.Drawing.Size(120, 20);
            this.numberValue.TabIndex = 8;
            this.numberValue.Visible = false;
            // 
            // numberValueLabel
            // 
            this.numberValueLabel.AutoSize = true;
            this.numberValueLabel.Location = new System.Drawing.Point(298, 91);
            this.numberValueLabel.Name = "numberValueLabel";
            this.numberValueLabel.Size = new System.Drawing.Size(49, 13);
            this.numberValueLabel.TabIndex = 6;
            this.numberValueLabel.Text = "Numeric:";
            this.numberValueLabel.Visible = false;
            // 
            // fixedValueLabel
            // 
            this.fixedValueLabel.AutoSize = true;
            this.fixedValueLabel.Location = new System.Drawing.Point(312, 125);
            this.fixedValueLabel.Name = "fixedValueLabel";
            this.fixedValueLabel.Size = new System.Drawing.Size(35, 13);
            this.fixedValueLabel.TabIndex = 9;
            this.fixedValueLabel.Text = "Fixed:";
            this.fixedValueLabel.Visible = false;
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.vendorButton);
            this.propertiesPanel.Controls.Add(this.ageButton);
            this.propertiesPanel.Controls.Add(this.ssidButton);
            this.propertiesPanel.Controls.Add(this.maxRateButton);
            this.propertiesPanel.Controls.Add(this.rssiButton);
            this.propertiesPanel.Controls.Add(this.macButton);
            this.propertiesPanel.Controls.Add(this.securityButton);
            this.propertiesPanel.Controls.Add(this.is40MHzButton);
            this.propertiesPanel.Controls.Add(this.networkTypeButton);
            this.propertiesPanel.Controls.Add(this.isNButton);
            this.propertiesPanel.Controls.Add(this.channelButton);
            this.propertiesPanel.Location = new System.Drawing.Point(12, 57);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(156, 175);
            this.propertiesPanel.TabIndex = 11;
            // 
            // vendorButton
            // 
            this.vendorButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.vendorButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.vendorButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.vendorButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.vendorButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.vendorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vendorButton.ForeColor = System.Drawing.Color.Black;
            this.vendorButton.HoverColor = System.Drawing.Color.LightGray;
            this.vendorButton.Location = new System.Drawing.Point(81, 58);
            this.vendorButton.Name = "vendorButton";
            this.vendorButton.Size = new System.Drawing.Size(75, 24);
            this.vendorButton.TabIndex = 10;
            this.vendorButton.TabStop = true;
            this.vendorButton.Text = "Vendor";
            this.vendorButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.vendorButton.UseVisualStyleBackColor = true;
            this.vendorButton.Click += new System.EventHandler(this.StringPropertyButtonClick);
            this.vendorButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // ageButton
            // 
            this.ageButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.ageButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.ageButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.ageButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.ageButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ageButton.ForeColor = System.Drawing.Color.Black;
            this.ageButton.HoverColor = System.Drawing.Color.LightGray;
            this.ageButton.Location = new System.Drawing.Point(0, 57);
            this.ageButton.Name = "ageButton";
            this.ageButton.Size = new System.Drawing.Size(75, 24);
            this.ageButton.TabIndex = 10;
            this.ageButton.TabStop = true;
            this.ageButton.Text = "Age";
            this.ageButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ageButton.UseVisualStyleBackColor = true;
            this.ageButton.Click += new System.EventHandler(this.NumberPropertyButtonClick);
            this.ageButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // ssidButton
            // 
            this.ssidButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.ssidButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.ssidButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.ssidButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.ssidButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ssidButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ssidButton.ForeColor = System.Drawing.Color.Black;
            this.ssidButton.HoverColor = System.Drawing.Color.LightGray;
            this.ssidButton.Location = new System.Drawing.Point(81, 116);
            this.ssidButton.Name = "ssidButton";
            this.ssidButton.Size = new System.Drawing.Size(75, 24);
            this.ssidButton.TabIndex = 10;
            this.ssidButton.TabStop = true;
            this.ssidButton.Text = "SSID";
            this.ssidButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ssidButton.UseVisualStyleBackColor = true;
            this.ssidButton.Click += new System.EventHandler(this.StringPropertyButtonClick);
            this.ssidButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // maxRateButton
            // 
            this.maxRateButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.maxRateButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.maxRateButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.maxRateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.maxRateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.maxRateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxRateButton.ForeColor = System.Drawing.Color.Black;
            this.maxRateButton.HoverColor = System.Drawing.Color.LightGray;
            this.maxRateButton.Location = new System.Drawing.Point(81, 0);
            this.maxRateButton.Name = "maxRateButton";
            this.maxRateButton.Size = new System.Drawing.Size(75, 24);
            this.maxRateButton.TabIndex = 10;
            this.maxRateButton.TabStop = true;
            this.maxRateButton.Text = "Max Rate";
            this.maxRateButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.maxRateButton.UseVisualStyleBackColor = true;
            this.maxRateButton.Click += new System.EventHandler(this.NumberPropertyButtonClick);
            this.maxRateButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // rssiButton
            // 
            this.rssiButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.rssiButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.rssiButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.rssiButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.rssiButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.rssiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rssiButton.ForeColor = System.Drawing.Color.Black;
            this.rssiButton.HoverColor = System.Drawing.Color.LightGray;
            this.rssiButton.Location = new System.Drawing.Point(0, 29);
            this.rssiButton.Name = "rssiButton";
            this.rssiButton.Size = new System.Drawing.Size(75, 24);
            this.rssiButton.TabIndex = 10;
            this.rssiButton.TabStop = true;
            this.rssiButton.Text = "RSSI";
            this.rssiButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rssiButton.UseVisualStyleBackColor = true;
            this.rssiButton.Click += new System.EventHandler(this.NumberPropertyButtonClick);
            this.rssiButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // macButton
            // 
            this.macButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.macButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.macButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.macButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.macButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.macButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.macButton.ForeColor = System.Drawing.Color.Black;
            this.macButton.HoverColor = System.Drawing.Color.LightGray;
            this.macButton.Location = new System.Drawing.Point(0, 145);
            this.macButton.Name = "macButton";
            this.macButton.Size = new System.Drawing.Size(75, 24);
            this.macButton.TabIndex = 10;
            this.macButton.TabStop = true;
            this.macButton.Text = "MAC Addr.";
            this.macButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.macButton.UseVisualStyleBackColor = true;
            this.macButton.Click += new System.EventHandler(this.StringPropertyButtonClick);
            this.macButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // securityButton
            // 
            this.securityButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.securityButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.securityButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.securityButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.securityButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.securityButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.securityButton.ForeColor = System.Drawing.Color.Black;
            this.securityButton.HoverColor = System.Drawing.Color.LightGray;
            this.securityButton.Location = new System.Drawing.Point(81, 29);
            this.securityButton.Name = "securityButton";
            this.securityButton.Size = new System.Drawing.Size(75, 24);
            this.securityButton.TabIndex = 10;
            this.securityButton.TabStop = true;
            this.securityButton.Text = "Security";
            this.securityButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.securityButton.UseVisualStyleBackColor = true;
            this.securityButton.Click += new System.EventHandler(this.SecurityPropertyButtonClick);
            this.securityButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // is40MHzButton
            // 
            this.is40MHzButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.is40MHzButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.is40MHzButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.is40MHzButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.is40MHzButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.is40MHzButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.is40MHzButton.ForeColor = System.Drawing.Color.Black;
            this.is40MHzButton.HoverColor = System.Drawing.Color.LightGray;
            this.is40MHzButton.Location = new System.Drawing.Point(0, 116);
            this.is40MHzButton.Name = "is40MHzButton";
            this.is40MHzButton.Size = new System.Drawing.Size(75, 24);
            this.is40MHzButton.TabIndex = 10;
            this.is40MHzButton.TabStop = true;
            this.is40MHzButton.Text = "Is 40MHz";
            this.is40MHzButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.is40MHzButton.UseVisualStyleBackColor = true;
            this.is40MHzButton.Click += new System.EventHandler(this.BooleanPropertyButtonClick);
            this.is40MHzButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // networkTypeButton
            // 
            this.networkTypeButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.networkTypeButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.networkTypeButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.networkTypeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.networkTypeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.networkTypeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.networkTypeButton.ForeColor = System.Drawing.Color.Black;
            this.networkTypeButton.HoverColor = System.Drawing.Color.LightGray;
            this.networkTypeButton.Location = new System.Drawing.Point(81, 87);
            this.networkTypeButton.Name = "networkTypeButton";
            this.networkTypeButton.Size = new System.Drawing.Size(75, 24);
            this.networkTypeButton.TabIndex = 10;
            this.networkTypeButton.TabStop = true;
            this.networkTypeButton.Text = "Type";
            this.networkTypeButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.networkTypeButton.UseVisualStyleBackColor = true;
            this.networkTypeButton.Click += new System.EventHandler(this.StringPropertyButtonClick);
            this.networkTypeButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // isNButton
            // 
            this.isNButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.isNButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.isNButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.isNButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.isNButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.isNButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.isNButton.ForeColor = System.Drawing.Color.Black;
            this.isNButton.HoverColor = System.Drawing.Color.LightGray;
            this.isNButton.Location = new System.Drawing.Point(0, 87);
            this.isNButton.Name = "isNButton";
            this.isNButton.Size = new System.Drawing.Size(75, 24);
            this.isNButton.TabIndex = 10;
            this.isNButton.TabStop = true;
            this.isNButton.Text = "Is 802.11n";
            this.isNButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.isNButton.UseVisualStyleBackColor = true;
            this.isNButton.Click += new System.EventHandler(this.BooleanPropertyButtonClick);
            this.isNButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // channelButton
            // 
            this.channelButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.channelButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.channelButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.channelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.channelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.channelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.channelButton.ForeColor = System.Drawing.Color.Black;
            this.channelButton.HoverColor = System.Drawing.Color.LightGray;
            this.channelButton.Location = new System.Drawing.Point(0, 0);
            this.channelButton.Name = "channelButton";
            this.channelButton.Size = new System.Drawing.Size(75, 24);
            this.channelButton.TabIndex = 10;
            this.channelButton.TabStop = true;
            this.channelButton.Text = "Channel";
            this.channelButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.channelButton.UseVisualStyleBackColor = true;
            this.channelButton.Click += new System.EventHandler(this.NumberPropertyButtonClick);
            this.channelButton.CheckedChanged += new System.EventHandler(this.PropertyButtonCheckedChanged);
            // 
            // operationsPanel
            // 
            this.operationsPanel.Controls.Add(this.EndsWithButton);
            this.operationsPanel.Controls.Add(this.StartsWithButton);
            this.operationsPanel.Controls.Add(this.lessThanOrEqualButton);
            this.operationsPanel.Controls.Add(this.greaterThanOrEqualButton);
            this.operationsPanel.Controls.Add(this.lessThanButton);
            this.operationsPanel.Controls.Add(this.greaterThanButton);
            this.operationsPanel.Controls.Add(this.notEqualButton);
            this.operationsPanel.Controls.Add(this.equalButton);
            this.operationsPanel.Location = new System.Drawing.Point(204, 57);
            this.operationsPanel.Name = "operationsPanel";
            this.operationsPanel.Size = new System.Drawing.Size(70, 175);
            this.operationsPanel.TabIndex = 12;
            // 
            // EndsWithButton
            // 
            this.EndsWithButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.EndsWithButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.EndsWithButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.EndsWithButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.EndsWithButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.EndsWithButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EndsWithButton.ForeColor = System.Drawing.Color.Black;
            this.EndsWithButton.HoverColor = System.Drawing.Color.LightGray;
            this.EndsWithButton.Location = new System.Drawing.Point(0, 150);
            this.EndsWithButton.Name = "EndsWithButton";
            this.EndsWithButton.Size = new System.Drawing.Size(70, 24);
            this.EndsWithButton.TabIndex = 10;
            this.EndsWithButton.TabStop = true;
            this.EndsWithButton.Text = "Ends With";
            this.EndsWithButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.EndsWithButton.UseVisualStyleBackColor = true;
            this.EndsWithButton.Visible = false;
            this.EndsWithButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // StartsWithButton
            // 
            this.StartsWithButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.StartsWithButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.StartsWithButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.StartsWithButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.StartsWithButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.StartsWithButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartsWithButton.ForeColor = System.Drawing.Color.Black;
            this.StartsWithButton.HoverColor = System.Drawing.Color.LightGray;
            this.StartsWithButton.Location = new System.Drawing.Point(0, 120);
            this.StartsWithButton.Name = "StartsWithButton";
            this.StartsWithButton.Size = new System.Drawing.Size(70, 24);
            this.StartsWithButton.TabIndex = 10;
            this.StartsWithButton.TabStop = true;
            this.StartsWithButton.Text = "Starts With";
            this.StartsWithButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StartsWithButton.UseVisualStyleBackColor = true;
            this.StartsWithButton.Visible = false;
            this.StartsWithButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // lessThanOrEqualButton
            // 
            this.lessThanOrEqualButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.lessThanOrEqualButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.lessThanOrEqualButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.lessThanOrEqualButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.lessThanOrEqualButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.lessThanOrEqualButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lessThanOrEqualButton.ForeColor = System.Drawing.Color.Black;
            this.lessThanOrEqualButton.HoverColor = System.Drawing.Color.LightGray;
            this.lessThanOrEqualButton.Location = new System.Drawing.Point(40, 90);
            this.lessThanOrEqualButton.Name = "lessThanOrEqualButton";
            this.lessThanOrEqualButton.Size = new System.Drawing.Size(30, 24);
            this.lessThanOrEqualButton.TabIndex = 10;
            this.lessThanOrEqualButton.TabStop = true;
            this.lessThanOrEqualButton.Text = "<=";
            this.lessThanOrEqualButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lessThanOrEqualButton.UseVisualStyleBackColor = true;
            this.lessThanOrEqualButton.Visible = false;
            this.lessThanOrEqualButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // greaterThanOrEqualButton
            // 
            this.greaterThanOrEqualButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.greaterThanOrEqualButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.greaterThanOrEqualButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.greaterThanOrEqualButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.greaterThanOrEqualButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.greaterThanOrEqualButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.greaterThanOrEqualButton.ForeColor = System.Drawing.Color.Black;
            this.greaterThanOrEqualButton.HoverColor = System.Drawing.Color.LightGray;
            this.greaterThanOrEqualButton.Location = new System.Drawing.Point(40, 60);
            this.greaterThanOrEqualButton.Name = "greaterThanOrEqualButton";
            this.greaterThanOrEqualButton.Size = new System.Drawing.Size(30, 24);
            this.greaterThanOrEqualButton.TabIndex = 10;
            this.greaterThanOrEqualButton.TabStop = true;
            this.greaterThanOrEqualButton.Text = ">=";
            this.greaterThanOrEqualButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.greaterThanOrEqualButton.UseVisualStyleBackColor = true;
            this.greaterThanOrEqualButton.Visible = false;
            this.greaterThanOrEqualButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // lessThanButton
            // 
            this.lessThanButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.lessThanButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.lessThanButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.lessThanButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.lessThanButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.lessThanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lessThanButton.ForeColor = System.Drawing.Color.Black;
            this.lessThanButton.HoverColor = System.Drawing.Color.LightGray;
            this.lessThanButton.Location = new System.Drawing.Point(0, 90);
            this.lessThanButton.Name = "lessThanButton";
            this.lessThanButton.Size = new System.Drawing.Size(30, 24);
            this.lessThanButton.TabIndex = 10;
            this.lessThanButton.TabStop = true;
            this.lessThanButton.Text = "<";
            this.lessThanButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lessThanButton.UseVisualStyleBackColor = true;
            this.lessThanButton.Visible = false;
            this.lessThanButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // greaterThanButton
            // 
            this.greaterThanButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.greaterThanButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.greaterThanButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.greaterThanButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.greaterThanButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.greaterThanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.greaterThanButton.ForeColor = System.Drawing.Color.Black;
            this.greaterThanButton.HoverColor = System.Drawing.Color.LightGray;
            this.greaterThanButton.Location = new System.Drawing.Point(0, 60);
            this.greaterThanButton.Name = "greaterThanButton";
            this.greaterThanButton.Size = new System.Drawing.Size(30, 24);
            this.greaterThanButton.TabIndex = 10;
            this.greaterThanButton.TabStop = true;
            this.greaterThanButton.Text = ">";
            this.greaterThanButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.greaterThanButton.UseVisualStyleBackColor = true;
            this.greaterThanButton.Visible = false;
            this.greaterThanButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // notEqualButton
            // 
            this.notEqualButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.notEqualButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.notEqualButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.notEqualButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.notEqualButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.notEqualButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.notEqualButton.ForeColor = System.Drawing.Color.Black;
            this.notEqualButton.HoverColor = System.Drawing.Color.LightGray;
            this.notEqualButton.Location = new System.Drawing.Point(0, 30);
            this.notEqualButton.Name = "notEqualButton";
            this.notEqualButton.Size = new System.Drawing.Size(70, 24);
            this.notEqualButton.TabIndex = 10;
            this.notEqualButton.TabStop = true;
            this.notEqualButton.Text = "Not Equals";
            this.notEqualButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.notEqualButton.UseVisualStyleBackColor = true;
            this.notEqualButton.Visible = false;
            this.notEqualButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // equalButton
            // 
            this.equalButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.equalButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.equalButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray;
            this.equalButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.equalButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.equalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.equalButton.ForeColor = System.Drawing.Color.Black;
            this.equalButton.HoverColor = System.Drawing.Color.LightGray;
            this.equalButton.Location = new System.Drawing.Point(0, 0);
            this.equalButton.Name = "equalButton";
            this.equalButton.Size = new System.Drawing.Size(70, 24);
            this.equalButton.TabIndex = 10;
            this.equalButton.TabStop = true;
            this.equalButton.Text = "Equals";
            this.equalButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.equalButton.UseVisualStyleBackColor = true;
            this.equalButton.Visible = false;
            this.equalButton.CheckedChanged += new System.EventHandler(this.OperationButtonCheckedChanged);
            // 
            // addExpressionButton
            // 
            this.addExpressionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addExpressionButton.ForeColor = System.Drawing.Color.Black;
            this.addExpressionButton.Location = new System.Drawing.Point(479, 57);
            this.addExpressionButton.Name = "addExpressionButton";
            this.addExpressionButton.Size = new System.Drawing.Size(37, 24);
            this.addExpressionButton.TabIndex = 13;
            this.addExpressionButton.Text = "Add";
            this.addExpressionButton.UseVisualStyleBackColor = true;
            this.addExpressionButton.Visible = false;
            this.addExpressionButton.Click += new System.EventHandler(this.AddExpressionButtonClick);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Enabled = false;
            this.removeButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.removeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.removeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.ForeColor = System.Drawing.Color.Black;
            this.removeButton.Location = new System.Drawing.Point(391, 12);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(63, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Undo";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.RemoveButtonClick);
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.acceptButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.acceptButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.acceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptButton.ForeColor = System.Drawing.Color.Black;
            this.acceptButton.Location = new System.Drawing.Point(460, 12);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(56, 23);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.AcceptButtonClick);
            // 
            // FormFilterBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(528, 244);
            this.Controls.Add(this.addExpressionButton);
            this.Controls.Add(this.operationsPanel);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.fixedValueLabel);
            this.Controls.Add(this.numberValue);
            this.Controls.Add(this.valuesLabel);
            this.Controls.Add(this.numberValueLabel);
            this.Controls.Add(this.textValueLabel);
            this.Controls.Add(this.textValue);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.expressionLabel);
            this.Controls.Add(this.textExpression);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(491, 272);
            this.Name = "FormFilterBuilder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter Builder";
            ((System.ComponentModel.ISupportInitialize)(this.numberValue)).EndInit();
            this.propertiesPanel.ResumeLayout(false);
            this.operationsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textExpression;
        private System.Windows.Forms.Label expressionLabel;
        private GrayButton acceptButton;
        private System.Windows.Forms.TextBox textValue;
        private System.Windows.Forms.Label textValueLabel;
        private System.Windows.Forms.ListBox valuesLabel;
        private System.Windows.Forms.NumericUpDown numberValue;
        private System.Windows.Forms.Label numberValueLabel;
        private System.Windows.Forms.Label fixedValueLabel;
        private GrayRadioButton channelButton;
        private System.Windows.Forms.Panel propertiesPanel;
        private GrayRadioButton ageButton;
        private GrayRadioButton maxRateButton;
        private GrayRadioButton macButton;
        private GrayRadioButton is40MHzButton;
        private GrayRadioButton isNButton;
        private GrayRadioButton vendorButton;
        private GrayRadioButton ssidButton;
        private GrayRadioButton rssiButton;
        private GrayRadioButton securityButton;
        private GrayRadioButton networkTypeButton;
        private System.Windows.Forms.Panel operationsPanel;
        private GrayRadioButton EndsWithButton;
        private GrayRadioButton StartsWithButton;
        private GrayRadioButton notEqualButton;
        private GrayRadioButton equalButton;
        private GrayRadioButton greaterThanButton;
        private GrayRadioButton lessThanOrEqualButton;
        private GrayRadioButton greaterThanOrEqualButton;
        private GrayRadioButton lessThanButton;
        private GrayButton addExpressionButton;
        private GrayButton removeButton;
    }
}