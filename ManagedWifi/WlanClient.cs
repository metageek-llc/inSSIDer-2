////////////////////////////////////////////////////////////////
//
// Copyright (c) 2007-2010 MetaGeek, LLC
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedWifi
{
    public class WlanClient
    {
        private readonly IntPtr _clientHandle;
        public IntPtr MyClientHandle
        {
            get { return _clientHandle; }
        }

        private readonly Dictionary<Guid, WlanInterface> _ifaces = new Dictionary<Guid, WlanInterface>();
        private uint _negotiatedVersion;
        private readonly Wlan.WlanNotificationCallbackDelegate _wlanNotificationCallback;

        public event EventHandler<InterfaceNotificationEventsArgs> InterfaceArrivedEvent;

        public event EventHandler<InterfaceNotificationEventsArgs> InterfaceRemovedEvent;

        public WlanClient()
        {
            Wlan.ThrowIfError(Wlan.WlanOpenHandle(1, IntPtr.Zero, out _negotiatedVersion, out _clientHandle));
            try
            {
                Wlan.WlanNotificationSource source;
                _wlanNotificationCallback = new Wlan.WlanNotificationCallbackDelegate(OnWlanNotification);
                Wlan.ThrowIfError(Wlan.WlanRegisterNotification(_clientHandle, Wlan.WlanNotificationSource.All, false, _wlanNotificationCallback, IntPtr.Zero, IntPtr.Zero, out source));
            }
            catch
            {
                Wlan.WlanCloseHandle(_clientHandle, IntPtr.Zero);
                throw;
            }
        }

        ~WlanClient()
        {
            Wlan.WlanCloseHandle(_clientHandle, IntPtr.Zero);
        }

        public string GetStringForReasonCode(Wlan.WlanReasonCode reasonCode)
        {
            StringBuilder stringBuffer = new StringBuilder(0x400);
            Wlan.ThrowIfError(Wlan.WlanReasonCodeToString(reasonCode, stringBuffer.Capacity, stringBuffer, IntPtr.Zero));
            return stringBuffer.ToString();
        }

        protected virtual void InvokeInterfaceArrivedEvent(InterfaceNotificationEventsArgs e)
        {
            if (InterfaceArrivedEvent != null)
            {
                InterfaceArrivedEvent(this, e);
            }
        }

        protected virtual void InvokeInterfaceRemovedEvent(InterfaceNotificationEventsArgs e)
        {
            if (InterfaceRemovedEvent != null)
            {
                InterfaceRemovedEvent(this, e);
            }
        }

        private void OnWlanNotification(ref Wlan.WlanNotificationData notifyData, IntPtr context)
        {
            WlanInterface interface2 = _ifaces.ContainsKey(notifyData.interfaceGuid) ? _ifaces[notifyData.interfaceGuid] : null;
            switch (notifyData.notificationSource)
            {
                case Wlan.WlanNotificationSource.Acm:
                    switch (notifyData.notificationCode)
                    {
                        case 8:
                            if (notifyData.dataSize >= Marshal.SizeOf(0))
                            {
                                Wlan.WlanReasonCode reasonCode = (Wlan.WlanReasonCode) Marshal.ReadInt32(notifyData.dataPtr);
                                if (interface2 != null)
                                {
                                    interface2.OnWlanReason(notifyData, reasonCode);
                                }
                            }
                            goto Label_0194;

                        case 9:
                        case 10:
                        case 11:
                        case 20:
                        case 0x15:
                        {
                            Wlan.WlanConnectionNotificationData? nullable = ParseWlanConnectionNotification(ref notifyData);
                            if (nullable.HasValue && (interface2 != null))
                            {
                                interface2.OnWlanConnection(notifyData, nullable.Value);
                            }
                            goto Label_0194;
                        }
                        case 12:
                        case 15:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                        case 0x13:
                            goto Label_0194;

                        case 13:
                            InvokeInterfaceArrivedEvent(new InterfaceNotificationEventsArgs(notifyData.interfaceGuid));
                            goto Label_0194;

                        case 14:
                            InvokeInterfaceRemovedEvent(new InterfaceNotificationEventsArgs(notifyData.interfaceGuid));
                            goto Label_0194;
                    }
                    break;

                case Wlan.WlanNotificationSource.Msm:
                    switch (notifyData.notificationCode)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        {
                            Wlan.WlanConnectionNotificationData? nullable2 = ParseWlanConnectionNotification(ref notifyData);
                            if (nullable2.HasValue && (interface2 != null))
                            {
                                interface2.OnWlanConnection(notifyData, nullable2.Value);
                            }
                            goto Label_0194;
                        }
                        case 7:
                        case 8:
                            goto Label_0194;
                    }
                    goto Label_0194;
            }
        Label_0194:
            if (interface2 != null)
            {
                interface2.OnWlanNotification(notifyData);
            }
        }

        private Wlan.WlanConnectionNotificationData? ParseWlanConnectionNotification(ref Wlan.WlanNotificationData notifyData)
        {
            int num = Marshal.SizeOf(typeof(Wlan.WlanConnectionNotificationData));
            if (notifyData.dataSize < num)
            {
                return null;
            }
            Wlan.WlanConnectionNotificationData data = (Wlan.WlanConnectionNotificationData) Marshal.PtrToStructure(notifyData.dataPtr, typeof(Wlan.WlanConnectionNotificationData));
            if (data.wlanReasonCode == Wlan.WlanReasonCode.Success)
            {
                IntPtr ptr = new IntPtr(notifyData.dataPtr.ToInt64() + Marshal.OffsetOf(typeof(Wlan.WlanConnectionNotificationData), "profileXml").ToInt64());
                data.profileXml = Marshal.PtrToStringUni(ptr);
            }
            return data;
        }

        public WlanInterface[] Interfaces
        {
            get
            {
                IntPtr ptr;
                WlanInterface[] interfaceArray2;
                Wlan.ThrowIfError(Wlan.WlanEnumInterfaces(_clientHandle, IntPtr.Zero, out ptr));
                try
                {
                    Wlan.WlanInterfaceInfoListHeader structure = (Wlan.WlanInterfaceInfoListHeader) Marshal.PtrToStructure(ptr, typeof(Wlan.WlanInterfaceInfoListHeader));
                    long num = ptr.ToInt64() + Marshal.SizeOf(structure);
                    WlanInterface[] interfaceArray = new WlanInterface[structure.numberOfItems];
                    List<Guid> list = new List<Guid>();
                    for (int i = 0; i < structure.numberOfItems; i++)
                    {
                        Wlan.WlanInterfaceInfo info = (Wlan.WlanInterfaceInfo) Marshal.PtrToStructure(new IntPtr(num), typeof(Wlan.WlanInterfaceInfo));
                        num += Marshal.SizeOf(info);
                        list.Add(info.interfaceGuid);
                        WlanInterface interface2 = _ifaces.ContainsKey(info.interfaceGuid) ? _ifaces[info.interfaceGuid] : new WlanInterface(this, info);
                        interfaceArray[i] = interface2;
                        _ifaces[info.interfaceGuid] = interface2;
                    }
                    Queue<Guid> queue = new Queue<Guid>();
                    foreach (Guid guid in _ifaces.Keys)
                    {
                        if (!list.Contains(guid))
                        {
                            queue.Enqueue(guid);
                        }
                    }
                    while (queue.Count != 0)
                    {
                        Guid key = queue.Dequeue();
                        _ifaces.Remove(key);
                    }
                    interfaceArray2 = interfaceArray;
                }
                finally
                {
                    Wlan.WlanFreeMemory(ptr);
                }
                return interfaceArray2;
            }
        }

    }
}

