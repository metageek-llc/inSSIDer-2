////////////////////////////////////////////////////////////////
//
// Copyright (c) 2007-2011 MetaGeek, LLC
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
using System.Text;
using System.Net.NetworkInformation;
using System.Linq;

namespace MetaGeek.WiFi
{
    /// <summary>
    /// This class represents a MAC address of a network
    /// device.
    /// </summary>
    public class MacAddress : IComparable
    {
        #region Members and Properties

        private string _myCachedToString;

        // individual bytes of the mac address
        private readonly byte[] _bytes;
        /// <summary>
        /// Gets an array of bytes representing the raw MAC
        /// address value.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                return (byte[])_bytes.Clone();
            }
        }

        private Int64 _myValue;
        public Int64 MyValue
        {
            get
            {
                return _myValue;
            }
            set
            {
                _myValue = value;
            }
        }

        /// <summary>
        /// Gets an indexed byte of the mac address
        /// </summary>
        /// <param name="index">index of the byte to return</param>
        /// <returns>byte value</returns>
        public byte this[int index]
        {
            get { return _bytes[index]; }
            set { _bytes[index] = value; }
        }

        /// <summary>
        /// Number of bytes that make up the address.
        /// </summary>
        public int Length
        {
            get { return _bytes.Length; }
        }

        #endregion Members and Properties

        #region Constructors

        public MacAddress( byte[] bytes ) {
            if (null == bytes || bytes.Length == 0) {
                throw new ArgumentException("Invalid byte array argument.");
            }
            _bytes = new byte[bytes.Length];
            Array.Copy(bytes, _bytes, bytes.Length);

            foreach (byte b in _bytes)
            {
                _myValue = _myValue << 8;
                _myValue |= b;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the MacAddress object to a string. The format
        /// of the string will be like the following:
        /// A1:2B:3C:4D:5E:6F
        /// </summary>
        /// <returns>human readable form of the mac address</returns>
        public override string ToString() {
            if (_myCachedToString != null)
                return _myCachedToString;
            StringBuilder sb = new StringBuilder();
            string separator = "";
            foreach (byte t in _bytes)
            {
                sb.Append(separator);
                sb.AppendFormat("{0:x2}", t);
                separator = ":";
            }

            _myCachedToString = sb.ToString().ToUpper();
            return _myCachedToString;
        }

        /// <summary>
        /// Determines if this object is equal to another
        /// MacAddress object.
        /// </summary>
        /// <param name="obj">a MacAddress object to compare with the current object</param>
        /// <returns>true if the two objects are equal, false otherwise.</returns>
        public override bool Equals(object obj) {
            MacAddress ma = obj as MacAddress;
            return null != ma && ma.MyValue == MyValue;
        }

        // <summary>
        // Hash function for this type.
        // </summary>
        // <returns>integer hash value</returns>
        // this is here so that resharper doesn't whine, it just hacks MyValue down to 32 bits
        public override int GetHashCode()
        {
            // Hash the mac address into a 32-bit hash
            Int64 key = _myValue;
            key = (~key) + (key << 18);
            key ^= key >> 31;
            key *= 21;
            key ^= key >> 11;
            key += key << 6;
            key ^= key >> 22;
            return (int)key;
        }

        /// <summary>
        /// Compares this MacAddress to a PhysicalAddress
        /// </summary>
        /// <param name="address">The PhysicalAddress to compare to</param>
        /// <returns>true if equal, otherwise false</returns>
        public bool CompareToPhysicalAddress(PhysicalAddress address)
        {
            try
            {
                return address != null && Bytes.SequenceEqual(address.GetAddressBytes());
            }
            catch(NullReferenceException)
            {
                return false;
            }
        }

        #endregion Methods

        #region IComparable Members

        public int CompareTo(object obj) {
            MacAddress compareObject = obj as MacAddress;
            if (null == obj) {
                return -1;
            }

            if (compareObject != null)
                for (int i = 0; i < compareObject.Length; ++i)
                {
                    if (this[i] > compareObject[i]) {
                        return 1;
                    }
                    if (this[i] < compareObject[i]) {
                        return -1;
                    }
                }
            return 0;
        }

        #endregion
    }
}
