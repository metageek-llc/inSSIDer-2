////////////////////////////////////////////////////////////////

#region Header

//
// Copyright (c) 2007-2010 MetaGeek, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion Header


////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Windows.Forms;

using inSSIDer.Misc;

using MetaGeek.WiFi;

namespace inSSIDer.UI.Forms
{
    public partial class FormFilterBuilder : Form
    {
        #region Fields

        public string Expr = "";
        private readonly string[] _booleanStrings = new[] { "True", "False" };
        private readonly string[] _securityStrings = new[] { "None", "WEP", "WPA-TKIP", "WPA-CCMP", "WPA2-TKIP", "WPA2-CCMP", "RSNA-CCMP" };
        private string _tempAddOperation = "";
        private string _tempAddProperty = "";

        #endregion Fields

        #region Constructors

        public FormFilterBuilder()
        {
            InitializeComponent();
        }

        public FormFilterBuilder(string exp)
            : this()
        {
            textExpression.Text = exp;
        }

        #endregion Constructors

        #region Private Methods

        private void AcceptButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textExpression.Text))
            {
                Close();
                return;
            }
            Filter f = new Filter();
            string error = f.SetExpression(ExprParser.Fix(textExpression.Text));
            if (error != string.Empty)
            {
                //MSG: means the Parser is sending a message up the line
                if (error.StartsWith("MSG:"))
                {
                    MessageBox.Show(error.Replace("MSG:",""), "Error parsing expression", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Error near \"" + error + "\"","Error parsing expression", MessageBoxButtons.OK);
                }
                return;
            }
            Expr = f.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddExpressionButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_tempAddOperation) || string.IsNullOrEmpty(_tempAddProperty)) return;
            if (textExpression.Text != "")
            {
                textExpression.AppendText(" && ");
            }
            if(textValue.Visible)
                textExpression.AppendText(_tempAddProperty + " " + _tempAddOperation + " \"" + textValue.Text + "\"");
            else if(numberValue.Visible)
                textExpression.AppendText(_tempAddProperty + " " + _tempAddOperation + " " + numberValue.Value);
            else if(valuesLabel.Visible)
                textExpression.AppendText(_tempAddProperty + " " + _tempAddOperation + " " + valuesLabel.SelectedItem);

            Reset();
        }

        private void BooleanPropertyButtonClick(object sender, EventArgs e)
        {
            HideAllOperations();

            equalButton.Visible = true;
            notEqualButton.Visible = true;
            fixedValueLabel.Visible = true;
            valuesLabel.Visible = true;

            valuesLabel.Items.Clear();
            valuesLabel.Items.AddRange(_booleanStrings);
            valuesLabel.SelectedIndex = 0;
        }

        private void HideAllOperations()
        {
            equalButton.Visible = false;
            equalButton.Checked = false;

            notEqualButton.Visible = false;
            notEqualButton.Checked = false;

            greaterThanButton.Visible = false;
            greaterThanButton.Checked = false;

            lessThanButton.Visible = false;
            lessThanButton.Checked = false;

            greaterThanOrEqualButton.Visible = false;
            greaterThanOrEqualButton.Checked = false;

            lessThanOrEqualButton.Visible = false;
            lessThanOrEqualButton.Checked = false;

            EndsWithButton.Visible = false;
            EndsWithButton.Checked = false;

            StartsWithButton.Visible = false;
            StartsWithButton.Checked = false;

            //And value fields
            textValueLabel.Visible = false;
            textValue.Visible = false;
            textValue.Clear();

            numberValueLabel.Visible = false;
            numberValue.Visible = false;
            numberValue.Value = 0;

            fixedValueLabel.Visible = false;
            valuesLabel.Visible = false;

            addExpressionButton.Visible = false;
        }

        private void NumberPropertyButtonClick(object sender, EventArgs e)
        {
            HideAllOperations();

            equalButton.Visible = true;
            notEqualButton.Visible = true;
            greaterThanButton.Visible = true;
            lessThanButton.Visible = true;
            greaterThanOrEqualButton.Visible = true;
            lessThanOrEqualButton.Visible = true;
            numberValueLabel.Visible = true;
            numberValue.Visible = true;
        }

        private void OperationButtonCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbTemp = sender as RadioButton;
            if (rbTemp == null) return;

            if (rbTemp == equalButton)
                _tempAddOperation = "==";
            else if (rbTemp == notEqualButton)
                _tempAddOperation = "!=";
            else if (rbTemp == greaterThanButton)
                _tempAddOperation = ">";
            else if (rbTemp == lessThanButton)
                _tempAddOperation = "<";
            else if (rbTemp == greaterThanOrEqualButton)
                _tempAddOperation = ">=";
            else if (rbTemp == lessThanOrEqualButton)
                _tempAddOperation = "<=";
            else if (rbTemp == StartsWithButton)
                _tempAddOperation = "sw";
            else if (rbTemp == EndsWithButton)
                _tempAddOperation = "ew";

            addExpressionButton.Visible = true;
        }

        private void PropertyButtonCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbTemp = sender as RadioButton;
            if (rbTemp == null) return;

            if (rbTemp == ageButton)
                _tempAddProperty = "Age";
            else if (rbTemp == channelButton)
                _tempAddProperty = "Channel";
            else if (rbTemp == isNButton)
                _tempAddProperty = "IsTypeN";
            else if (rbTemp == is40MHzButton)
                _tempAddProperty = "Is40MHz";
            else if (rbTemp == macButton)
                _tempAddProperty = "MacAddress";
            else if (rbTemp == maxRateButton)
                _tempAddProperty = "MaxRate";
            else if (rbTemp == networkTypeButton)
                _tempAddProperty = "NetworkType";
            else if (rbTemp == securityButton)
                _tempAddProperty = "Security";
            else if (rbTemp == rssiButton)
                _tempAddProperty = "Rssi";
            else if (rbTemp == ssidButton)
                _tempAddProperty = "Ssid";
            else if (rbTemp == vendorButton)
                _tempAddProperty = "Vendor";
        }

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textExpression.Text)) return;
            int ind = textExpression.Text.LastIndexOf(" && ");
            textExpression.Text = textExpression.Text.Remove(ind == -1 ? 0 : ind);
        }

        private void Reset()
        {
            HideAllOperations();
            addExpressionButton.Visible = false;
            _tempAddOperation = "";
            _tempAddProperty = "";
            UnCheckProps();
        }

        private void SecurityPropertyButtonClick(object sender, EventArgs e)
        {
            HideAllOperations();

            equalButton.Visible = true;
            notEqualButton.Visible = true;
            greaterThanButton.Visible = true;
            lessThanButton.Visible = true;
            greaterThanOrEqualButton.Visible = true;
            lessThanOrEqualButton.Visible = true;

            fixedValueLabel.Visible = true;
            valuesLabel.Visible = true;

            valuesLabel.Items.Clear();
            valuesLabel.Items.AddRange(_securityStrings);
            valuesLabel.SelectedIndex = 0;
        }

        private void StringPropertyButtonClick(object sender, EventArgs e)
        {
            HideAllOperations();

            equalButton.Visible = true;
            notEqualButton.Visible = true;
            StartsWithButton.Visible = true;
            EndsWithButton.Visible = true;
            textValueLabel.Visible = true;
            textValue.Visible = true;
        }

        private void TextExpressionTextChanged(object sender, EventArgs e)
        {
            removeButton.Enabled = textExpression.Text.Length >= 1;
        }

        private void UnCheckProps()
        {
            //This sets all of the property radio buttons to unchecked
            propertiesPanel.Controls.OfType<RadioButton>().ToList().ForEach(rb => rb.Checked = false);
        }

        #endregion Private Methods
    }
}