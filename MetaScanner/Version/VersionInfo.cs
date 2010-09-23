////////////////////////////////////////////////////////////////
//
// Copyright (c) 2007-2010 MetaGeek, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//
//	http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
//
////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace inSSIDer.Version
{
	/// <summary>
	/// Summary description for Version.
	/// </summary>
	public static class VersionInfo
    {
        #region Private Data
        private static string _latestVersion = string.Empty;
        private static string _versionDescription = string.Empty;
        private static string _downloadUrl = string.Empty;
        #endregion

        #region Public Properties

        public static string LatestVersion
        {
            get { return _latestVersion; }
        }

        public static string VersionDescription
        {
            get { return _versionDescription; }
        }

        public static string DownloadUrl
        {
            get { return _downloadUrl; }
        }
        #endregion

        #region Public Methods

        public static bool CheckForAvailableUpdate(string versionUrl, string ignoreVersion, bool userInitiated)
        {
            try
            {
                GetVersionPageText(versionUrl, 2);

                if (!userInitiated && _latestVersion.Equals(ignoreVersion))
                {
                }
                else if (Application.ProductVersion.Length > 0 && _latestVersion.Length > 0 && CompareVersions(_latestVersion, Application.ProductVersion))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static DialogResult ShowUpdateDialog()
        {
            using (VersionUpdateForm versionForm = new VersionUpdateForm())
            {
                versionForm.InstalledVersion = Application.ProductVersion;
                versionForm.LatestVersion = LatestVersion;
                versionForm.VersionDescription = VersionDescription;
                DialogResult result = versionForm.ShowDialog();

                return result;
            }
        }

        #endregion

        #region Private Methods

        private static int[] GetVersionNumbers(string versionString)
		{
			int[] result = new int[4];

			for (int i = 0; i < result.Length; i++)
			{
				int periodPos = versionString.IndexOf('.');

				int currentVersion;

#if !DEBUG
				try
				{
#endif		
                    if (periodPos != -1)
					{
						currentVersion = int.Parse(versionString.Substring(0, periodPos));
					}
					else
					{
						currentVersion = int.Parse(versionString);
					}
#if !DEBUG			
                }
				catch (Exception)
				{
					currentVersion = 0;
				}
#endif

				result[i] = currentVersion;

				versionString = versionString.Substring(periodPos + 1);
			}

			return result;
		}

		/// <summary>
		/// Compare two version number strings.
		/// </summary>
		/// <param name="version1">version number string</param>
		/// <param name="version2">version number string</param>
		private static bool CompareVersions(string version1, string version2)
		{
            int result = 0;

            try
            {
                int[] version1Array = GetVersionNumbers(version1);
                int[] version2Array = GetVersionNumbers(version2);

                for (int i = 0; result == 0 && i < version1Array.Length; i++)
                {
                    result = version1Array[i] - version2Array[i];
                }
            }
            catch (FormatException)
            {
                return false;
            }

            if (result > 0)
            {
                return true;
            }
		    return false;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private static void GetVersionPageText(string url, int timeoutInSeconds)
        {
            String line;

            Uri baseUri = new Uri(url);

            WebRequest request = WebRequest.Create(baseUri);
            request.Timeout = 1000 * timeoutInSeconds;

            try
            {
                using (Stream stream = request.GetResponse().GetResponseStream())
                using (StreamReader streamreader = new StreamReader(stream))
                {
                    while (!streamreader.EndOfStream && ((line = streamreader.ReadLine()) != null))
                    {
                        char[] parms = { ':' };
                        String[] tokens = line.Split(parms, 2);

                        switch (tokens[0])
                        {
                            case "Version":
                                _latestVersion = tokens[1].Trim();
                                break;

                            case "URL":
                                _downloadUrl = tokens[1].Trim();
                                break;

                            case "Description":
                                _versionDescription = streamreader.ReadToEnd();
                                break;
                        }
                    }
                }
                // Close the response to free resources.
                request.GetResponse().Close();
            }
            catch (WebException)
            {
            }
        }
        #endregion
    }
}
