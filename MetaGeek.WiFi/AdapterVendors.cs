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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;


namespace MetaGeek.WiFi
{
    public class AdapterVendors
    {
        #region Members and Properties

        // vendor dictonary - maps mac address to vendor names
        private Dictionary<string, string> _vendors = new Dictionary<string, string>();
        
        #endregion Members and Properties

        #region Methods

        public string GetVendor(MacAddress mac) {
            // format the key name
            string key = mac[0].ToString("X2") + "-" +
                         mac[1].ToString("X2") + "-" +
                         mac[2].ToString("X2");
            try
            {
                return _vendors[key];
            }
            catch (KeyNotFoundException)
            {
                return String.Empty;
            }
        }

        public void LoadFromOui() {

            _vendors = new Dictionary<string, string>();

            // create a regular expression that will match the vendor's mac address
            Regex matcher = new Regex(
                @"(?<mac>^[0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F]-[0-9a-fA-F][0-9a-fA-F])\s+\(hex\)",
                RegexOptions.Compiled);

            // open the file and iterate through each line
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string msg = @"MetaGeek.WiFi.oui.txt";
            if (assembly != null)
            {
                StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(msg));

                try {
                    string textLine;
                    do {
                        textLine = reader.ReadLine();
                        if (!String.IsNullOrEmpty(textLine)) {
                            Match match = matcher.Match(textLine);
                            if (match.Success) {
                                // find the start of the vendor name
                                int index = textLine.IndexOf("(hex)") + "(hex)".Length;
                                // trim off leading and trailing whitespace
                                string vendor = textLine.Substring(index).Trim();
                                // use the mac string as the key
                                string mac = match.Groups["mac"].ToString();
                                // the OUI file contains some duplicate entries, so
                                // just use the first one we find.
                                if (!_vendors.ContainsKey(mac)) {
                                    _vendors.Add(mac, vendor);
                                }
                            }
                        }
                    }
                    while (textLine != null);
                }
                finally {
                    reader.Close();
                }
            }
        }

        #endregion Methods
    }
}
