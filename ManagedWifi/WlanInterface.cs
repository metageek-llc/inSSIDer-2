using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ManagedWifi
{
    public class WlanInterface
    {
        private readonly WlanClient _client;
        private readonly Queue<object> _eventQueue = new Queue<object>();
        private readonly AutoResetEvent _eventQueueFilled = new AutoResetEvent(false);
        private Wlan.WlanInterfaceInfo _info;
        private bool _queueEvents;

        public event WlanConnectionNotificationEventHandler WlanConnectionNotification;

        public event WlanNotificationEventHandler WlanNotification;

        public event WlanReasonNotificationEventHandler WlanReasonNotification;

        internal WlanInterface(WlanClient client, Wlan.WlanInterfaceInfo info)
        {
            _client = client;
            _info = info;
        }

        private void Connect(Wlan.WlanConnectionParameters connectionParams)
        {
            Wlan.ThrowIfError(Wlan.WlanConnect(_client.MyClientHandle, _info.interfaceGuid, ref connectionParams, IntPtr.Zero));
        }

        private void Connect(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, string profile)
        {
            Wlan.WlanConnectionParameters parameters2 = new Wlan.WlanConnectionParameters
            {
                wlanConnectionMode = connectionMode,
                profile = profile,
                dot11BssType = bssType,
                flags = 0
            };
            Wlan.WlanConnectionParameters connectionParams = parameters2;
            Connect(connectionParams);
        }

        public void Connect(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, Wlan.Dot11Ssid ssid, Wlan.WlanConnectionFlags flags)
        {
            Wlan.WlanConnectionParameters parameters2 = new Wlan.WlanConnectionParameters
            {
                wlanConnectionMode = connectionMode,
                dot11SsidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ssid)),
                dot11BssType = bssType,
                flags = flags
            };
            Wlan.WlanConnectionParameters connectionParams = parameters2;
            this.Connect(connectionParams);
            Marshal.StructureToPtr(ssid, connectionParams.dot11SsidPtr, false);
            Marshal.DestroyStructure(connectionParams.dot11SsidPtr, ssid.GetType());
            Marshal.FreeHGlobal(connectionParams.dot11SsidPtr);
        }

        public bool ConnectSynchronously(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, string profile, int connectTimeout)
        {
            _queueEvents = true;
            try
            {
                Connect(connectionMode, bssType, profile);
                while (_queueEvents && _eventQueueFilled.WaitOne(connectTimeout, true))
                {
                    lock (_eventQueue)
                    {
                        while (_eventQueue.Count != 0)
                        {
                            object obj2 = _eventQueue.Dequeue();
                            if (obj2 is WlanConnectionNotificationEventData)
                            {
                                WlanConnectionNotificationEventData data = (WlanConnectionNotificationEventData)obj2;
                                if (((data.NotifyData.notificationSource != Wlan.WlanNotificationSource.Acm) || (data.NotifyData.notificationCode != 10)) || data.ConnNotifyData.profileName != profile)
                                {
                                    break;
                                }
                                return true;
                            }
                        }
                        continue;
                    }
                }
            }
            finally
            {
                _queueEvents = false;
                _eventQueue.Clear();
            }
            return false;
        }

        private Wlan.WlanAvailableNetwork[] ConvertAvailableNetworkListPtr(IntPtr availNetListPtr)
        {
            Wlan.WlanAvailableNetworkListHeader header = (Wlan.WlanAvailableNetworkListHeader)Marshal.PtrToStructure(availNetListPtr, typeof(Wlan.WlanAvailableNetworkListHeader));
            long num = availNetListPtr.ToInt64() + Marshal.SizeOf(typeof(Wlan.WlanAvailableNetworkListHeader));
            Wlan.WlanAvailableNetwork[] networkArray = new Wlan.WlanAvailableNetwork[header.numberOfItems];
            for (int i = 0; i < header.numberOfItems; i++)
            {
                networkArray[i] = (Wlan.WlanAvailableNetwork)Marshal.PtrToStructure(new IntPtr(num), typeof(Wlan.WlanAvailableNetwork));
                num += Marshal.SizeOf(typeof(Wlan.WlanAvailableNetwork));
            }
            return networkArray;
        }

        private Wlan.WlanBssEntryN[] ConvertBssListPtr(IntPtr bssListPtr)
        {
            Wlan.WlanBssListHeader header = (Wlan.WlanBssListHeader)Marshal.PtrToStructure(bssListPtr, typeof(Wlan.WlanBssListHeader));
            long num = bssListPtr.ToInt64() + Marshal.SizeOf(typeof(Wlan.WlanBssListHeader));
            Wlan.WlanBssEntryN[] entryArray = new Wlan.WlanBssEntryN[header.numberOfItems];
            for (int i = 0; i < header.numberOfItems; i++)
            {
                entryArray[i] = new Wlan.WlanBssEntryN((Wlan.WlanBssEntry)Marshal.PtrToStructure(new IntPtr(num), typeof(Wlan.WlanBssEntry)));

                int size = (int)entryArray[i].BaseEntry.ieSize;
                byte[] IEs = new byte[size];

                Marshal.Copy(new IntPtr(num + entryArray[i].BaseEntry.ieOffset), IEs, 0, size);

                //IEs = System.IO.File.ReadAllBytes("ies.dat");

                //Parse 802.11n IEs if avalible
                entryArray[i].NSettings = IeParser.Parse(IEs);

                //===DEBUGGING===
                //BitConverter.ToString(entryArray[i].BaseEntry.dot11Bssid);
                
                //string ssid = Encoding.ASCII.GetString(entryArray[i].BaseEntry.dot11Ssid.SSID);
                //string mac = BitConverter.ToString(entryArray[i].BaseEntry.dot11Bssid);
                //System.IO.File.WriteAllBytes("data" + mac.Trim("\0".ToCharArray())  + ".dat", IEs);

                //Console.WriteLine(IEs.Length);

                //Test t = (Test)Marshal.PtrToStructure(new IntPtr(num), typeof(Test));
                //===END DEBUGGING===
                num += Marshal.SizeOf(typeof(Wlan.WlanBssEntry));
            }
            return entryArray;
        }

        public void DeleteProfile(string profileName)
        {
            Wlan.ThrowIfError(Wlan.WlanDeleteProfile(_client.MyClientHandle, _info.interfaceGuid, profileName, IntPtr.Zero));
        }

        private void EnqueueEvent(object queuedEvent)
        {
            lock (_eventQueue)
            {
                _eventQueue.Enqueue(queuedEvent);
            }
            _eventQueueFilled.Set();
        }

        public IEnumerable<Wlan.WlanAvailableNetwork> GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags flags)
        {
            IntPtr ptr;
            Wlan.WlanAvailableNetwork[] networkArray;
            Wlan.ThrowIfError(Wlan.WlanGetAvailableNetworkList(_client.MyClientHandle, _info.interfaceGuid, flags, IntPtr.Zero, out ptr));
            try
            {
                networkArray = ConvertAvailableNetworkListPtr(ptr);
            }
            finally
            {
                Wlan.WlanFreeMemory(ptr);
            }
            return networkArray;
        }

        private int GetInterfaceInt(Wlan.WlanIntfOpcode opCode)
        {
            IntPtr ptr;
            int num;
            Wlan.WlanOpcodeValueType type;
            int num2;
            Wlan.ThrowIfError(Wlan.WlanQueryInterface(_client.MyClientHandle, _info.interfaceGuid, opCode, IntPtr.Zero, out num, out ptr, out type));
            try
            {
                num2 = Marshal.ReadInt32(ptr);
            }
            finally
            {
                Wlan.WlanFreeMemory(ptr);
            }
            return num2;
        }

        public IEnumerable<Wlan.WlanBssEntryN> GetNetworkBssList()
        {
            IntPtr ptr;
            Wlan.WlanBssEntryN[] entryArray;
            Wlan.ThrowIfError(Wlan.WlanGetNetworkBssList(_client.MyClientHandle, _info.interfaceGuid, IntPtr.Zero, Wlan.Dot11BssType.Any, false, IntPtr.Zero, out ptr));
            try
            {
                entryArray = ConvertBssListPtr(ptr);
            }
            finally
            {
                Wlan.WlanFreeMemory(ptr);
            }
            return entryArray;
        }

        public Wlan.WlanBssEntryN[] GetNetworkBssList(Wlan.Dot11Ssid ssid, Wlan.Dot11BssType bssType, bool securityEnabled)
        {
            Wlan.WlanBssEntryN[] entryArray;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(ssid));
            Marshal.StructureToPtr(ssid, ptr, false);
            try
            {
                IntPtr ptr2;
                Wlan.ThrowIfError(Wlan.WlanGetNetworkBssList(_client.MyClientHandle, _info.interfaceGuid, ptr, bssType, securityEnabled, IntPtr.Zero, out ptr2));
                try
                {
                    entryArray = ConvertBssListPtr(ptr2);
                }
                finally
                {
                    Wlan.WlanFreeMemory(ptr2);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return entryArray;
        }

        public Wlan.WlanProfileInfo[] GetProfiles()
        {
            IntPtr ptr;
            Wlan.WlanProfileInfo[] infoArray2;
            Wlan.ThrowIfError(Wlan.WlanGetProfileList(_client.MyClientHandle, _info.interfaceGuid, IntPtr.Zero, out ptr));
            try
            {
                Wlan.WlanProfileInfoListHeader structure = (Wlan.WlanProfileInfoListHeader)Marshal.PtrToStructure(ptr, typeof(Wlan.WlanProfileInfoListHeader));
                Wlan.WlanProfileInfo[] infoArray = new Wlan.WlanProfileInfo[structure.numberOfItems];
                long num = ptr.ToInt64() + Marshal.SizeOf(structure);
                for (int i = 0; i < structure.numberOfItems; i++)
                {
                    Wlan.WlanProfileInfo info = (Wlan.WlanProfileInfo)Marshal.PtrToStructure(new IntPtr(num), typeof(Wlan.WlanProfileInfo));
                    infoArray[i] = info;
                    num += Marshal.SizeOf(info);
                }
                infoArray2 = infoArray;
            }
            finally
            {
                Wlan.WlanFreeMemory(ptr);
            }
            return infoArray2;
        }

        public string GetProfileXml(string profileName)
        {
            IntPtr ptr;
            Wlan.WlanProfileFlags flags;
            Wlan.WlanAccess access;
            string str;
            Wlan.ThrowIfError(Wlan.WlanGetProfile(_client.MyClientHandle, _info.interfaceGuid, profileName, IntPtr.Zero, out ptr, out flags, out access));
            try
            {
                str = Marshal.PtrToStringUni(ptr);
            }
            finally
            {
                Wlan.WlanFreeMemory(ptr);
            }
            return str;
        }

        internal void OnWlanConnection(Wlan.WlanNotificationData notifyData, Wlan.WlanConnectionNotificationData connNotifyData)
        {
            if (WlanConnectionNotification != null)
            {
                WlanConnectionNotification(notifyData, connNotifyData);
            }
            if (_queueEvents)
            {
                WlanConnectionNotificationEventData data2 = new WlanConnectionNotificationEventData
                {
                    NotifyData = notifyData,
                    ConnNotifyData = connNotifyData
                };
                WlanConnectionNotificationEventData queuedEvent = data2;
                EnqueueEvent(queuedEvent);
            }
        }

        internal void OnWlanNotification(Wlan.WlanNotificationData notifyData)
        {
            if (WlanNotification != null)
            {
                WlanNotification(notifyData);
            }
        }

        internal void OnWlanReason(Wlan.WlanNotificationData notifyData, Wlan.WlanReasonCode reasonCode)
        {
            if (WlanReasonNotification != null)
            {
                WlanReasonNotification(notifyData, reasonCode);
            }
            if (_queueEvents)
            {
                WlanReasonNotificationData data2 = new WlanReasonNotificationData
                {
                    NotifyData = notifyData,
                    ReasonCode = reasonCode
                };
                WlanReasonNotificationData queuedEvent = data2;
                EnqueueEvent(queuedEvent);
            }
        }

        public void Scan()
        {
            Wlan.ThrowIfError(Wlan.WlanScan(_client.MyClientHandle, _info.interfaceGuid, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
        }

        private void SetInterfaceInt(Wlan.WlanIntfOpcode opCode, int value)
        {
            IntPtr ptr = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ptr, value);
            try
            {
                Wlan.ThrowIfError(Wlan.WlanSetInterface(_client.MyClientHandle, _info.interfaceGuid, opCode, 4, ptr, IntPtr.Zero));
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        public Wlan.WlanReasonCode SetProfile(Wlan.WlanProfileFlags flags, string profileXml, bool overwrite)
        {
            Wlan.WlanReasonCode code;
            Wlan.ThrowIfError(Wlan.WlanSetProfile(_client.MyClientHandle, _info.interfaceGuid, flags, profileXml, null, overwrite, IntPtr.Zero, out code));
            return code;
        }

        public bool Autoconf
        {
            get
            {
                return (GetInterfaceInt(Wlan.WlanIntfOpcode.AutoconfEnabled) != 0);
            }
            set
            {
                SetInterfaceInt(Wlan.WlanIntfOpcode.AutoconfEnabled, value ? 1 : 0);
            }
        }

        public Wlan.Dot11BssType BssType
        {
            get
            {
                return (Wlan.Dot11BssType)GetInterfaceInt(Wlan.WlanIntfOpcode.BssType);
            }
            set
            {
                SetInterfaceInt(Wlan.WlanIntfOpcode.BssType, (int)value);
            }
        }

        public int Channel
        {
            get
            {
                return GetInterfaceInt(Wlan.WlanIntfOpcode.ChannelNumber);
            }
        }

        public Wlan.WlanConnectionAttributes CurrentConnection
        {
            get
            {
                int num;
                IntPtr ptr;
                Wlan.WlanOpcodeValueType type;
                Wlan.WlanConnectionAttributes attributes = new Wlan.WlanConnectionAttributes();
                int code = Wlan.WlanQueryInterface(_client.MyClientHandle, _info.interfaceGuid,
                                                   Wlan.WlanIntfOpcode.CurrentConnection, IntPtr.Zero, out num,
                                                   out ptr, out type);
                // 0x0000139F is the code returned when not connected
                if (code != 0x0000139F)
                {
                    Wlan.ThrowIfError(code);
                    try
                    {
                        attributes =
                            (Wlan.WlanConnectionAttributes)
                            Marshal.PtrToStructure(ptr, typeof(Wlan.WlanConnectionAttributes));
                    }
                    finally
                    {
                        Wlan.WlanFreeMemory(ptr);
                    }
                }
                return attributes;
            }
        }

        public Wlan.Dot11OperationMode CurrentOperationMode
        {
            get
            {
                return (Wlan.Dot11OperationMode)GetInterfaceInt(Wlan.WlanIntfOpcode.CurrentOperationMode);
            }
        }

        public string InterfaceDescription
        {
            get
            {
                return _info.interfaceDescription;
            }
        }

        public Guid InterfaceGuid
        {
            get
            {
                return _info.interfaceGuid;
            }
        }

        //public string InterfaceName
        //{
        //    get
        //    {
        //        return NetworkInterface.Name;
        //    }
        //}

        public Wlan.WlanInterfaceState InterfaceState
        {
            get
            {
                return (Wlan.WlanInterfaceState)GetInterfaceInt(Wlan.WlanIntfOpcode.InterfaceState);
            }
        }

        public NetworkInterface NetworkInterface
        {
            get
            {
                var nd = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface interface2 in nd)
                {
                    if (interface2.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        Guid guid = new Guid(interface2.Id);
                        if (guid.Equals(_info.interfaceGuid))
                        {
                            return interface2;
                        }
                    }
                }
                //We haven't found one yet, create one.
                return new SudoInterface(this);

                //return null;
            }
        }

        public int Rssi
        {
            get
            {
                return GetInterfaceInt(Wlan.WlanIntfOpcode.Rssi);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WlanConnectionNotificationEventData
        {
            public Wlan.WlanNotificationData NotifyData;
            public Wlan.WlanConnectionNotificationData ConnNotifyData;
        }

        public delegate void WlanConnectionNotificationEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanConnectionNotificationData connNotifyData);

        public delegate void WlanNotificationEventHandler(Wlan.WlanNotificationData notifyData);

        [StructLayout(LayoutKind.Sequential)]
        private struct WlanReasonNotificationData
        {
            public Wlan.WlanNotificationData NotifyData;
            public Wlan.WlanReasonCode ReasonCode;
        }

        public delegate void WlanReasonNotificationEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanReasonCode reasonCode);
    }
}
