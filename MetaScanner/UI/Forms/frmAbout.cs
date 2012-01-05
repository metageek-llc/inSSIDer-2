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
using System.ComponentModel;
using System.Windows.Forms;

using inSSIDer.HTML;
using inSSIDer.Localization;
using inSSIDer.Properties;

namespace inSSIDer.UI.Forms
{
    partial class FormAbout : Form
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        internal FormAbout()
        {
            InitializeComponent();

            versionLabel.Text = Localizer.GetString("Version") + " " + Application.ProductVersion;

            //aboutHeaderImage.Left = (Width - aboutHeaderImage.Width) / 2;
            //versionLabel.Left = (Width - versionLabel.Width) / 2;
            //copyrightLabel.Left = (Width - copyrightLabel.Width) / 2;
            //linkLabel.Left = (Width - linkLabel.Width) / 2;
            //okButton.Left = (Width - okButton.Width) / 2;
        }

        #endregion Constructors

        #region Private Methods

        private void AboutHeaderImageClick(object sender, EventArgs e)
        {
            LinkToWebsite();
        }

        private void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkToWebsite();
        }

        private void LinkToWebsite()
        {
            try
            {
                LinkHelper.OpenLink("http://www.metageek.net", Settings.Default.AnalyticsMedium, "AboutForm");
            }
            catch (Win32Exception)
            {
            }
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion Private Methods
    }
}