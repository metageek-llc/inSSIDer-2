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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace inSSIDer.HTML
{
    public static class LinkHelper
    {
        #region Public Methods

        /// <summary>
        /// Opens a link in external browser
        /// </summary>
        /// <param name="url">URL to open</param>
        /// <param name="medium">Medium (application name)</param>
        /// <param name="source">Source (AboutForm, StartPage, etc)</param>
        public static void OpenLink(string url, string medium, string source)
        {
            StringBuilder urlBuilder = new StringBuilder(url, 100);
            urlBuilder.Append(url.Contains('?') ? '&' : '?');
            urlBuilder.Append("utm_campaign=Software&utm_medium=");
            urlBuilder.Append(medium);
            urlBuilder.Append("&utm_source=");
            urlBuilder.Append(source);
            try
            {
                Process.Start(urlBuilder.ToString());
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception)
            {
                MessageBox.Show(@"Unable to open web browser.\nPlease visit " + url, @"Error");
            }
        }

        #endregion Public Methods
    }
}