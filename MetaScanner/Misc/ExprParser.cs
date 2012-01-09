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
using System.Collections.Generic;
using System.Text;

namespace inSSIDer.Misc
{
    static class ExprParser
    {
        #region Fields

        private static readonly Dictionary<string, string> Prop = new Dictionary<string, string>
                                                                       {
                                                                  {"age", "Age"},
                                                                  {"alias", "Alias"},
                                                                  {"channel", "Channel"},
                                                                  {"istypen", "IsTypeN"},
                                                                  {"is40mhz", "Is40Mhz"},
                                                                  {"macaddress", "MacAddress"},
                                                                  {"maxrate", "MaxRate"},
                                                                  {"networktype", "NetworkType"},
                                                                  {"security", "Security"},
                                                                  {"rssi", "Rssi"},
                                                                  {"ssid", "Ssid"},
                                                                  {"vendor", "Vendor"}
                                                              };

        #endregion Fields

        #region Public Methods

        public static string Fix(string exp)
        {
            string lastSection = "Couldn't parse anything.";
            try
            {
                char[] chars = exp.ToCharArray();
                List<char> chrList = new List<char>();
                //&&|\s&&\s|\s&&|&&\s

                //char lastChar = '\0';
                //bool addchr;

                for (int i = 0; i < chars.Length; i++)
                {
                    //addchr = true;
                    if (chars[i] == '&' && chars[i + 1] == '&')
                    {
                        if (chars[i - 1] != ' ' && chars[i - 1] != '"')
                        {
                            chrList.Add(' ');
                            chrList.Add('&');
                            chrList.Add('&');
                            i += 2;
                            if (chars[i] != ' ')
                            {
                                chrList.Add(' ');
                            }
                        }
                        else if (chars[i + 2] != ' ')
                        {
                            chrList.Add('&');
                            chrList.Add('&');
                            chrList.Add(' ');
                            i += 2;
                            //addchr = false;
                        }
                    }
                    //if (!addchr) continue;
                    chrList.Add(chars[i]);
                }
                string output = new string(chrList.ToArray());

                //Fix names
                string[] p = output.Split(new[] { " && " }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
                string sep = "";
                foreach (string s in p)
                {
                    //Are we in a quote?
                    bool inQ = false;

                    //Replace all spaces inside of quotes with placeholders
                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (chars[i] == '"') inQ = !inQ;
                        else if (inQ && chars[i] == ' ')
                        {
                            chars[i] = '|';
                        }
                    }
                    string s2 = new string(chars);

                    string[] p2 = s2.Trim().Split(' ');
                    //Replace space placeholders with spaces
                    p2[2] = p2[2].Replace('|', ' ');

                    string oldp20;
                    p2[0] = p2[0].ToLower();
                    foreach (KeyValuePair<string, string> prop in Prop)
                    {
                        oldp20 = p2[0];
                        p2[0] = p2[0].Replace(prop.Key, prop.Value);
                        if (p2[0] != oldp20) break;
                    }
                    sb.Append(sep);
                    sb.Append(p2[0]);

                    lastSection = "MSG:Error near \"" + p2[0] + "\"";

                    sb.Append(' ');
                    sb.Append(p2[1]);

                    lastSection.TrimEnd('"');
                    lastSection += " " + p2[1] + "\"";

                    sb.Append(' ');
                    sb.Append(p2[2]);
                    sep = " && ";

                }

                return sb.ToString();
            }
            catch
            {
                return lastSection;
            }
        }

        #endregion Public Methods
    }
}