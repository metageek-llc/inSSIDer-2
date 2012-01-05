using System;
using System.Net.NetworkInformation;

using ManagedWifi;

namespace ManagedWifi
{
    class SudoInterface : NetworkInterface
    {
        #region Fields

        private string _desc;
        private string _id;
        private string _name;

        #endregion Fields

        #region Properties

        public override string Description
        {
            get { return _desc; }
        }

        public override string Id
        {
            get { return _id; }
        }

        public override bool IsReceiveOnly
        {
            get { return false; }
        }

        public override string Name
        {
            get { return _name; }
        }

        public override NetworkInterfaceType NetworkInterfaceType
        {
            get { return NetworkInterfaceType.Wireless80211; }
        }

        public override OperationalStatus OperationalStatus
        {
            get { return OperationalStatus.Up; }
        }

        public override long Speed
        {
            get { return 0; }
        }

        public override bool SupportsMulticast
        {
            get { return false; }
        }

        #endregion Properties

        #region Constructors

        public SudoInterface(WlanInterface wlan)
        {
            _id = wlan.InterfaceGuid.ToString();
            //Calling InterfaceName cuases an infinite loop
            //_name = wlan.InterfaceName;
            _desc = wlan.InterfaceDescription;
        }

        public SudoInterface(string id, string description, string name)
        {
            _id = id;
            _name = name;
            _desc = description;
        }

        #endregion Constructors

        #region Public Methods

        public override IPInterfaceProperties GetIPProperties()
        {
            //throw new NotImplementedException();
            return null;
        }

        public override IPv4InterfaceStatistics GetIPv4Statistics()
        {
            throw new NotImplementedException();
            //return null;
        }

        public override PhysicalAddress GetPhysicalAddress()
        {
            return PhysicalAddress.None;
        }

        public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
        {
            return true;
        }

        #endregion Public Methods
    }
}