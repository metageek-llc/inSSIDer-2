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
using System.Linq;
using System.Reflection;
using System.Text;

namespace MetaGeek.WiFi
{
    #region Enumerations

    public enum Bool
    {
        True, False, Nil
    }

    /// <summary>
    /// Operator
    /// </summary>
    public enum Op
    {
        /// <summary>
        /// Not Set
        /// </summary>
        NotSet,
        /// <summary>
        /// Equal (any)
        /// </summary>
        Equal,
        /// <summary>
        /// Not Equal (any)
        /// </summary>
        NotEqual,
        /// <summary>
        /// Greater Than (int)
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Less Than (int)
        /// </summary>
        LessThan,
        /// <summary>
        /// Greater Or Equal (int)
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// Less Or Equal (int)
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// Starts With (string)
        /// </summary>
        StartsWith,
        /// <summary>
        /// Ends With (string)
        /// </summary>
        EndsWith,
        /// <summary>
        /// Does not start with
        /// </summary>
        NotStartsWith,
        /// <summary>
        /// Does not end with
        /// </summary>
        NotEndsWith
    }

    public enum SecurityType
    {
        None = 0,
        Wep = 1,
        WpaTkip = 2,
        WpaCcmp = 3,
        Wpa2Tkip = 4,
        Wpa2Ccmp = 5
    }

    #endregion Enumerations

    /// <summary>
    /// Specifies Parameters for filtering AccessPoints
    /// </summary>
    public class Filter
    {
        #region Fields

        public int Age = -1;
        public string Alias = String.Empty;
        public int Channel = -1;
        public static Filter Empty = new Filter();
        public bool Enabled = true;

        //Unique Id
        public Guid Id;
        public Bool Is40MHz = Bool.Nil;
        public Bool IsTypeN = Bool.Nil;
        public string MacAddress = String.Empty;
        public int MaxRate = -1;
        public string NetworkType = String.Empty;
        public Op OpAge = Op.NotSet;
        public Op OpAlias = Op.NotSet;
        public Op OpChannel = Op.NotSet;
        public Op OpIs40MHz = Op.NotSet;
        public Op OpIsTypeN = Op.NotSet;
        public Op OpMacAddress = Op.NotSet;
        public Op OpMaxRate = Op.NotSet;
        public Op OpNetworkType = Op.NotSet;
        public Op OpRssi = Op.NotSet;
        public Op OpSecurity = Op.NotSet;
        public Op OpSsid = Op.NotSet;
        public Op OpVendor = Op.NotSet;
        public int Rssi = -101;
        public string Security = String.Empty;
        public string Ssid = String.Empty;
        public string Vendor = String.Empty;

        #endregion Fields

        #region Constructors

        public Filter()
        {
            Id = Guid.NewGuid();
        }

        public Filter(string expr)
            : this()
        {
            SetExpression(expr);
        }

        #endregion Constructors

        #region Public Methods

        public bool Eval(AccessPoint ap)
        {
            bool status = true;

            //Age
            if (Age > -1 && OpAge != Op.NotSet) status &= CompInt(ap.Age, OpAge, Age);

            //Alias
            if (!string.IsNullOrEmpty(Alias) && OpAlias != Op.NotSet) status &= CompString(ap.Alias, OpAlias, Alias, true);

            //Channel
            if (Channel > -1 && OpChannel != Op.NotSet) status &= CompInt((int)ap.Channel, OpChannel, Channel);

            //IsTypeN
            if (IsTypeN != Bool.Nil && OpIsTypeN != Op.NotSet) status &= CompBool(ap.IsN, OpIsTypeN, IsTypeN);

            //Is40MHz
            if (Is40MHz != Bool.Nil && OpIs40MHz != Op.NotSet)
            {
                status &= ap.NSettings == null
                         ? OpIs40MHz != Op.Equal
                         : CompBool(ap.NSettings.Is40MHz, OpIs40MHz, Is40MHz);
            }

            //MacAddress
            if (!string.IsNullOrEmpty(MacAddress) && OpMacAddress != Op.NotSet) status &= CompString(ap.MacAddress.ToString(), OpMacAddress, MacAddress, true);

            //MaxRate
            if (MaxRate > -1 && OpMaxRate != Op.NotSet) status &= CompInt((int)ap.MaxRate, OpMaxRate, MaxRate);

            //Security filtering ranking, filter as int
            if (!string.IsNullOrEmpty(Security) && OpSecurity != Op.NotSet && SecurityRanking(Security) != -1)
                status &= CompInt(SecurityRanking(ap.Privacy), OpSecurity, SecurityRanking(Security));

            //Rssi
            if (Rssi > -101 && OpRssi != Op.NotSet)
                status &= CompInt(ap.LastData.Rssi, OpRssi, Rssi);

            //Ssid
            if (!string.IsNullOrEmpty(Ssid) && OpSsid != Op.NotSet) status &= CompString(ap.Ssid, OpSsid, Ssid, true);

            //Network Type
            if (!string.IsNullOrEmpty(NetworkType) && OpNetworkType != Op.NotSet) status &= CompString(ap.NetworkType, OpNetworkType, NetworkType, true);

            //Vendor
            if (!string.IsNullOrEmpty(Vendor) && OpVendor != Op.NotSet) status &= CompString(ap.Vendor, OpVendor, Vendor, true);

            return status;
        }

        public object[] GetData()
        {
            return new object[] {Id, Enabled, GetExpression(), 0};
        }

        /// <summary>
        /// Configures this filter according to the expression
        /// </summary>
        /// <param name="expression">The expression to use</param>
        /// <returns>What section the parse failed on, if any</returns>
        public string SetExpression(string expression)
        {
            string[] parts = expression.Split(new[] { " && " }, StringSplitOptions.RemoveEmptyEntries);
            string[] p2;
            Type type;
            bool inQ = false;
            string section;

            //Reset all fields
            foreach (FieldInfo info in typeof(Filter).GetFields())
            {
                if (info.FieldType == typeof(Op))
                    info.SetValue(this, Op.NotSet);
                else if (info.FieldType == typeof(int))
                    info.SetValue(this, -1);
                else if (info.FieldType == typeof(string))
                    info.SetValue(this, string.Empty);
                else if (info.FieldType == typeof(Bool))
                    info.SetValue(this, Bool.Nil);
            }

            foreach (string sect in parts)
            {
                char[] chars = sect.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    if(chars[i] == '"') inQ = !inQ;
                    else if(inQ && chars[i] == ' ')
                    {
                        chars[i] = '|';
                    }
                }
                section = new string(chars);
                //Split into parts, eg. prop op value
                p2 = section.Split(' ');

                //Check for section count, must be 3 parts
                if (p2.Length != 3)
                    return section;

                //Fix strings
                p2[2] = p2[2].Replace('|', ' ');

                //Tyler: I didn't really want to use reflection, but it reduces code repetition

                //Check if the field actually exists
                if(!FieldExists(p2[0])) return section;

                type = typeof (Filter);

                FieldInfo fi = type.GetField(p2[0]);
                Op operation = StringToOp(p2[1]);
                if(operation == Op.NotSet) return section;

                type.GetField("Op" + p2[0]).SetValue(this, operation);
                if (fi.Name == "Security")
                {
                    //If the security mode doesn't parse, error
                    if(SecurityRanking(p2[2]) == -1) return section;
                    p2[2] = '"' + p2[2] + '"';
                }
                if(fi.FieldType == typeof(int))
                    fi.SetValue(this, int.Parse(p2[2]));
                else if (fi.FieldType == typeof(string))
                {
                    if (p2[2].StartsWith("\"") && p2[2].StartsWith("\""))
                        fi.SetValue(this, p2[2].Trim('"'));
                    else
                        return section;
                }
                else if (fi.FieldType == typeof(Bool))
                    fi.SetValue(this, StringToBool(p2[2]));
                else
                    return section;

            }
            Enabled = true;
            return string.Empty;
        }

        /// <summary>
        /// Gets the string expression for this filter
        /// </summary>
        /// <returns>The string expression</returns>
        public override string ToString()
        {
            return GetExpression();
        }

        #endregion Public Methods

        #region Private Methods

        private bool CompBool(bool value, Op op, Bool cvalue)
        {
            if(cvalue != Bool.Nil)
            {
                switch (op)
                {
                    case Op.NotSet:
                        return false;
                    case Op.Equal:
                        return cvalue.Compare(value);
                    case Op.NotEqual:
                        return !cvalue.Compare(value);
                    default:
                        return false;
                }
            }
            return false;
        }

        private bool CompInt(int value, Op op, int cvalue)
        {
            if (value > -101 && cvalue > -101)
            {
                switch (op)
                {
                    case Op.NotSet:
                        return false;
                    case Op.Equal:
                        return value == cvalue;
                    case Op.NotEqual:
                        return value != cvalue;
                    case Op.GreaterThan:
                        return value > cvalue;
                    case Op.LessThan:
                        return value < cvalue;
                    case Op.GreaterThanOrEqual:
                        return value >= cvalue;
                    case Op.LessThanOrEqual:
                        return value <= cvalue;
                    default:
                        return false;

                }
            }
            return false;
        }

        private bool CompString(string value, Op op, string cvalue,bool lower)
        {
            if (!string.IsNullOrEmpty(value)  && !string.IsNullOrEmpty(cvalue))
            {
                if (lower)
                {
                    //Lowercase for comparison
                    value = value.ToLower();
                    cvalue = cvalue.ToLower();
                }
                switch (op)
                {
                    case Op.NotSet:
                        return false;
                    case Op.Equal:
                        return value == cvalue;
                    case Op.NotEqual:
                        return value != cvalue;
                    case Op.StartsWith:
                        return value.StartsWith(cvalue);
                    case Op.EndsWith:
                        return value.EndsWith(cvalue);
                    case Op.NotStartsWith:
                        return !value.StartsWith(cvalue);
                    case Op.NotEndsWith:
                        return !value.EndsWith(cvalue);
                    default:
                        return false;
                }
            }
            return false;
        }

        private bool FieldExists(string name)
        {
            return typeof (Filter).GetFields().Where(f => f.Name == name).Count() == 1;
        }

        /// <summary>
        /// Gets the expression representation of the filter
        /// </summary>
        /// <returns>The expression</returns>
        private string GetExpression()
        {
            List<string> exprs = new List<string>();

            if (Age != -1 && OpAge != Op.NotSet)
                exprs.Add(string.Format("Age {0} {1}", OpToString(OpAge), Age));

            if (!string.IsNullOrEmpty(Alias) && OpAlias != Op.NotSet)
                exprs.Add(string.Format("Alias {0} \"{1}\"", OpToString(OpAlias), Alias));

            if (Channel != -1 && OpChannel != Op.NotSet)
                exprs.Add(string.Format("Channel {0} {1}", OpToString(OpChannel), Channel));

            if (IsTypeN != Bool.Nil && OpIsTypeN != Op.NotSet)
                exprs.Add(string.Format("IsTypeN {0} {1}", OpToString(OpIsTypeN), IsTypeN));

            if (Is40MHz != Bool.Nil && OpIs40MHz != Op.NotSet)
                exprs.Add(string.Format("Is40MHz {0} {1}", OpToString(OpIs40MHz), Is40MHz));

            if (!string.IsNullOrEmpty(MacAddress) && OpMacAddress != Op.NotSet)
                exprs.Add(string.Format("MacAddress {0} \"{1}\"", OpToString(OpMacAddress), MacAddress));

            if (MaxRate != -1 && OpMaxRate != Op.NotSet)
                exprs.Add(string.Format("MaxRate {0} {1}", OpToString(OpMaxRate), MaxRate));

            if (!string.IsNullOrEmpty(Security) && OpSecurity != Op.NotSet)
                exprs.Add(string.Format("Security {0} {1}", OpToString(OpSecurity), Security));

            if (Rssi != -1 && OpRssi != Op.NotSet)
                exprs.Add(string.Format("Rssi {0} {1}", OpToString(OpRssi), Rssi));

            if (!string.IsNullOrEmpty(Ssid) && OpSsid != Op.NotSet)
                exprs.Add(string.Format("Ssid {0} \"{1}\"", OpToString(OpSsid), Ssid));

            if (!string.IsNullOrEmpty(Vendor) && OpVendor != Op.NotSet)
                exprs.Add(string.Format("Vendor {0} \"{1}\"", OpToString(OpVendor), Vendor));

            if (!string.IsNullOrEmpty(NetworkType) && OpNetworkType != Op.NotSet)
                exprs.Add(string.Format("NetworkType {0} \"{1}\"", OpToString(OpNetworkType), NetworkType));

            if (exprs.Count == 0) return String.Empty;
            if (exprs.Count == 1) return exprs[0];

            StringBuilder sbout = new StringBuilder();

            string sep = "";
            foreach (string s in exprs)
            {
                sbout.Append(sep);
                sbout.Append(s);
                sep = " && ";
            }
            return sbout.ToString();
        }

        private string OpToString(Op op)
        {
            switch (op)
            {
                case Op.Equal:
                    return "==";
                case Op.NotEqual:
                    return "!=";
                case Op.GreaterThan:
                    return ">";
                case Op.LessThan:
                    return "<";
                case Op.GreaterThanOrEqual:
                    return ">=";
                case Op.LessThanOrEqual:
                    return "<=";
                case Op.StartsWith:
                    return "sw";
                case Op.EndsWith:
                    return "ew";
                case Op.NotStartsWith:
                    return "!sw";
                case Op.NotEndsWith:
                    return "!ew";
                default:
                    throw new ArgumentOutOfRangeException("op");
            }
        }

        //None, WEP, WPA_TKIP, WPA_CCMP, WPA2_TKIP, WPA2_CCMP
        private int SecurityRanking(string security)
        {
            security = security.ToLower();
            if (security.Contains("tkip"))
            {
                if (security.Contains("wpa2") || security.Contains("rsna"))
                {
                    return (int)SecurityType.Wpa2Tkip;
                }
                if (security.Contains("wpa"))
                {
                    return (int)SecurityType.WpaTkip;
                }
            }
            else if (security.Contains("ccmp") || security.Contains("aes"))
            {
                if (security.Contains("wpa2") || security.Contains("rsna"))
                {
                    return (int)SecurityType.Wpa2Ccmp;
                }
                if (security.Contains("wpa"))
                {
                    return (int)SecurityType.WpaCcmp;
                }
            }
            else if (security.StartsWith("wpa2") || security.StartsWith("rsna")) return (int)SecurityType.Wpa2Ccmp;
            else if (security.StartsWith("wpa")) return (int)SecurityType.WpaTkip;
            else if (security.StartsWith("wep")) return (int)SecurityType.Wep;
            else if (security.StartsWith("none")) return (int)SecurityType.None;

            return -1;
        }

        private Bool StringToBool(string s)
        {
            switch (s.ToLower())
            {
                case "true":
                    return Bool.True;
                case "false":
                    return Bool.False;
                default:
                    return Bool.Nil;
            }
        }

        private Op StringToOp(string op)
        {
            //Op outp = Op.NotSet;
            op = op.ToLower().Trim();
            if(op.Length > 3) op = op.Remove(3);

            switch (op)
            {
                case "==":
                    return Op.Equal;
                case "!=":
                    return Op.NotEqual;
                case ">":
                    return Op.GreaterThan;
                case "<":
                    return Op.LessThan;
                case ">=":
                    return Op.GreaterThanOrEqual;
                case "<=":
                    return Op.LessThanOrEqual;
                case "sw":
                    return Op.StartsWith;
                case "ew":
                    return Op.EndsWith;
                case "!sw":
                    return Op.NotStartsWith;
                case "!ew":
                    return Op.NotEndsWith;
                default:
                    return Op.NotSet;
            }
        }

        #endregion Private Methods
    }
}