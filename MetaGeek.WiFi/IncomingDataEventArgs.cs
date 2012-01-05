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

namespace MetaGeek.WiFi
{
    public class IncomingDataEventArgs<T> : EventArgs
    {
        #region Fields

        private readonly IEnumerable<T> _data;

        #endregion Fields

        #region Properties

        public IEnumerable<T> Data
        {
            get
            {
                return _data;
            }
        }

        #endregion Properties

        #region Constructors

        public IncomingDataEventArgs(IEnumerable<T> data)
        {
            _data = data;
        }

        #endregion Constructors
    }
}