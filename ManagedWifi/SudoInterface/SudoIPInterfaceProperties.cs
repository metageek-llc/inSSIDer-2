using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace SudoInterface
{
	class SudoIPInterfaceProperties : IPInterfaceProperties
	{
        public override IPAddressInformationCollection AnycastAddresses
        {
            get { throw new NotImplementedException(); }
        }

        public override IPAddressCollection DhcpServerAddresses
        {
            get { throw new NotImplementedException(); }
        }

        public override IPAddressCollection DnsAddresses
        {
            get { throw new NotImplementedException(); }
        }

        public override string DnsSuffix
        {
            get { throw new NotImplementedException(); }
        }

        public override GatewayIPAddressInformationCollection GatewayAddresses
        {
            get { throw new NotImplementedException(); }
        }

        public override IPv4InterfaceProperties GetIPv4Properties()
        {
            throw new NotImplementedException();
        }

        public override IPv6InterfaceProperties GetIPv6Properties()
        {
            throw new NotImplementedException();
        }

        public override bool IsDnsEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsDynamicDnsEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public override MulticastIPAddressInformationCollection MulticastAddresses
        {
            get { throw new NotImplementedException(); }
        }

        public override UnicastIPAddressInformationCollection UnicastAddresses
        {
            get { throw new NotImplementedException(); }
        }

        public override IPAddressCollection WinsServersAddresses
        {
            get { throw new NotImplementedException(); }
        }
    }
}
