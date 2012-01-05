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

namespace inSSIDer.UnhandledException
{
    public class SendExceptionClickEventArgs : EventArgs
    {
        #region Fields

        private readonly bool _sendExceptionDetails;
        private readonly Exception _unhandledException;

        #endregion Fields

        #region Properties

        public bool SendExceptionDetails
        {
            get { return _sendExceptionDetails; }
        }

        public Exception UnhandledException
        {
            get { return _unhandledException; }
        }

        #endregion Properties

        #region Constructors

        public SendExceptionClickEventArgs(bool sendDetails, Exception exception)
        {
            _sendExceptionDetails = sendDetails;     // TRUE if user clicked on "Send Error Report" button and FALSE if on "Don't Send"
            _unhandledException = exception;         // Used to store captured exception
        }

        #endregion Constructors
    }
}