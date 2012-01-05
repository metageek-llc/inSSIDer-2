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
using System.Windows.Forms;

namespace inSSIDer.Version
{
    public partial class VersionUpdateForm : Form
    {
        #region Properties

        /// <summary>
        /// Version of Chanalyzer that is installed
        /// </summary>
        public String InstalledVersion
        {
            get { return installedVersionLabel.Text; }
            set { installedVersionLabel.Text = value; }
        }

        /// <summary>
        /// Latest version of Chanalyzer that is released
        /// </summary>
        public String LatestVersion
        {
            get { return latestVersionLabel.Text; }
            set { latestVersionLabel.Text = value; }
        }

        /// <summary>
        /// Description of the latest version, included fixes, etc.
        /// </summary>
        public String VersionDescription
        {
            get { return descriptionTextBox.Text; }
            set { descriptionTextBox.Text = value; }
        }

        #endregion Properties

        #region Constructors

        public VersionUpdateForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Private Methods

        private void DownloadButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void IgnoreVersionButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        private void RemindLaterButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Private Methods
    }
}