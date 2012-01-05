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
using System.Configuration;

namespace inSSIDer.Properties
{
    internal sealed partial class Settings
    {
        #region Fields

        private bool _ranCheck;
        private bool _useSettings = true;

        #endregion Fields

        #region Properties

        public override object this[string propertyName]
        {
            get
            {
                //Check config system
                if(!_ranCheck)
                {
                    CheckSettingsSystem();
                }
                if (_useSettings)
                {
                    return base[propertyName];
                }
                object o = null;
                SettingsProperty sp = Properties[propertyName];

                if (sp != null)
                {
                        if(sp.SerializeAs == SettingsSerializeAs.String)
                        {
                            o = Parse(sp.DefaultValue.ToString(), sp.PropertyType);
                        }
                }

                return o;
            }
            set
            {
                //Check config system
                if(!_ranCheck)
                {
                    CheckSettingsSystem();
                }
                if (_useSettings)
                {
                    base[propertyName] = value;
                }
            }
        }

        #endregion Properties

        #region Public Methods

        public bool CheckSettingsSystem()
        {
            _ranCheck = true;
            try
            {
                //Try settings get
                string setting = (string)base["settingsTest"];
                //Then set
                base["settingsTest"] = "OK";
                //If we haven't exceptioned yet, return true
                _useSettings = true;
                return true;
            }
            catch (ConfigurationException)
            {
                _useSettings = false;
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        static object Parse(string value, Type type)
        {
            // might need ConvertFromString
            // (rather than Invariant)
            return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
        }

        #endregion Private Methods
    }
}