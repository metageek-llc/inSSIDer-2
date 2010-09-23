////////////////////////////////////////////////////////////////
//
// Copyright (c) 2007-2008 MetaGeek, LLC
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

using System.Text;


namespace MetaGeek.IoctlNdis
{
    /// <summary>
    /// This class is a simple data class that holds registry
    /// information about a network adapter.
    /// </summary>
    public class AdapterInformation
    {
        #region Private Data
        private string _description;
        private string _title;
        private string _serviceName;
        #endregion

        #region Properties
/*
        /// <summary>
        /// Gets or sets the description for the network adapter.
        /// </summary>
        public string Description {
            get { return _description; }
            set { _description = value; }
        }
*/

/*
        /// <summary>
        /// Gets or sets the title information for the network adapter.
        /// </summary>
        public string Title {
            get { return _title; }
            set { _title = value; }
        }
*/

        /// <summary>
        /// Gets or sets the service name for the network adapter.
        /// </summary>
        public string ServiceName {
            get { return _serviceName; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AdapterInformation() {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="description">network adapter description</param>
        /// <param name="title">network adapter title</param>
        /// <param name="serviceName">service name of the network adapter</param>
        public AdapterInformation(string description, string title, string serviceName) {
            _description = description;
            _title = title;
            _serviceName = serviceName;
        } // End AdapterInforamtion()
        #endregion

        /// <summary>
        /// Returns the string representation of the class.
        /// </summary>
        /// <returns>a string</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("Description: " + _description + "\n");
            sb.Append("Service Name: " + _serviceName + "\n");
            sb.Append("Title: " + _title + "\n");
            return ( sb.ToString());
        }
    }
}
