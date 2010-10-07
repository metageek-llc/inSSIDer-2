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

using System;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MetaGeek.IoctlNdis
{
    /// <summary>
    /// File share attribute flags.
    /// </summary>
    [Flags]
    public enum FileShareFlags : uint
    {
        FileShareRead=0x00000001,
        FileShareWrite=0x00000002,
        FileShareDelete=0x00000004
    }

    /// <summary>
    /// File creation flags.
    /// </summary>
    public enum FileCreateFlags : uint
    {
        CreateNew=1,
        CreateAlways=2,
        OpenExisting=3,
        OpenAlways=4,
        TruncateExisting=5
    }

    /// <summary>
    /// OID codes for querying information from
    /// the NDIS driver.
    /// </summary>
    public enum Oid : uint
    {
        OidGenSupportedList=0x00010101,
        OidGenHardwareStatus=0x00010102,
        OidGenMediaSupported=0x00010103,
        OidGenMediaInUse=0x00010104,
        OidGenMaximumLookahead=0x00010105,
        OidGenMaximumFrameSize=0x00010106,
        OidGenLinkSpeed=0x00010107,
        OidGenTransmitBufferSpace=0x00010108,
        OidGenReceiveBufferSpace=0x00010109,
        OidGenTransmitBlockSize=0x0001010A,
        OidGenReceiveBlockSize=0x0001010B,
        OidGenVendorId=0x0001010C,
        OidGenVendorDescription=0x0001010D,
        OidGenCurrentPacketFilter=0x0001010E,
        OidGenCurrentLookahead=0x0001010F,
        OidGenDriverVersion=0x00010110,
        OidGenMaximumTotalSize=0x00010111,
        OidGenProtocolOptions=0x00010112,
        OidGenMacOptions=0x00010113,
        OidGenMediaConnectStatus=0x00010114,
        OidGenMaximumSendPackets=0x00010115,

        Oid80211Bssid=0x0D010101,
        Oid80211Ssid=0x0D010102,
        Oid80211NetworkTypesSupported=0x0D010203,
        Oid80211NetworkTypeInUse=0x0D010204,
        Oid80211TxPowerLevel=0x0D010205,
        Oid80211Rssi=0x0D010206,
        Oid80211RssiTrigger=0x0D010207,
        Oid80211InfrastructureMode=0x0D010108,
        Oid80211FragmentationThreshold=0x0D010209,
        Oid80211RtsThreshold=0x0D01020A,
        Oid80211NumberOfAntennas=0x0D01020B,
        Oid80211RxAntennaSelected=0x0D01020C,
        Oid80211TxAntennaSelected=0x0D01020D,
        Oid80211SupportedRates=0x0D01020E,
        Oid80211DesiredRates=0x0D010210,
        Oid80211Configuration=0x0D010211,
        Oid80211Statistics=0x0D020212,
        Oid80211AddWep=0x0D010113,
        Oid80211RemoveWep=0x0D010114,
        Oid80211Disassociate=0x0D010115,
        Oid80211PowerMode=0x0D010216,
        Oid80211BssidList=0x0D010217,
        Oid80211AuthenticationMode=0x0D010118,
        Oid80211PrivacyFilter=0x0D010119,
        Oid80211BssidListScan=0x0D01011A,
        Oid80211WepStatus=0x0D01011B,
        Oid80211EncryptionStatus=0x0D01011B,
        Oid80211ReloadDefaults=0x0D01011C,
        Oid80211AddKey=0x0D01011D,
        Oid80211RemoveKey=0x0D01011E,
        Oid80211AssociationInformation=0x0D01011F,
        Oid80211Test=0x0D010120,
        Oid80211Capability=0x0D010122,
        Oid80211Pmkid=0x0D010123,
        /* PnP and power management OIDs */
        OidPnpCapabilities=0xFD010100,
        OidPnpSetPower=0xFD010101,
        OidPnpQueryPower=0xFD010102,
        OidPnpAddWakeUpPattern=0xFD010103,
        OidPnpRemoveWakeUpPattern=0xFD010104,
        OidPnpWakeUpPatternList=0xFD010105,
        OidPnpEnableWakeUp=0xFD010106
    }

    /// <summary>
    /// Information Element (IE) types
    /// </summary>
    public enum IeType : byte
    {
        HtCapabilities = 45,
        HtExtendedCapabilities = 61
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=4)]
    public struct Ndis802Dot11ConfigurationFh
    {
        public uint Length;             // Length of structure
        public uint HopPattern;         // As defined by 802.11, MSB set
        public uint HopSet;             // to one if non-802.11
        public uint DwellTime;          // units are Kusec
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=4)]
    public struct Ndis802Dot11Configuration
    {
        public uint Length;             // Lenght of structure
        public uint BeaconPeriod;       // units are Kusec
        public uint ATIMWindow;         // units are Kusec
        public uint DSConfig;           // Frequency, units are kHz
        public Ndis802Dot11ConfigurationFh FHConfig;
    }// End NDIS_802_11_CONFIGURATION

    // Added new types for OFDM 5G and 2.4G
    public enum NdisNetworkType
    {
        Ndis80211Fh,
        Ndis80211Ds,
        Ndis80211Ofdm5,
        Ndis80211Ofdm24,
        Ndis80211NetworkTypeMax    // not a real type, defined as an upper bound
    } // End NetworkType

    public enum NetworkInfrastructure
    {
        Ndis80211Ibss,
        Ndis80211Infrastructure,
        Ndis80211AutoUnknown,
        Ndis80211InfrastructureMax         // Not a real value, defined as upper bound
    }// End NetworkInfrastructure 

    public enum AuthenticationMode
    {
        Ndis80211AuthModeOpen,
        Ndis80211AuthModeShared,
        Ndis80211AuthModeAutoSwitch,
        Ndis80211AuthModeWpa,
        Ndis80211AuthModeWpapsk,
        Ndis80211AuthModeWpaNone,
        Ndis80211AuthModeWpa2,
        Ndis80211AuthModeWpa2Psk               // Not a real mode, defined as upper bound
    } // End AuthenticationMode

    /// <summary>
    /// This structure maps to the NDIS_WLAN_BSSID_EX structure 
    /// in the Windows NDIS API.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=4)]
    public struct NdisWlanBssidEx
    {
        public UInt32 Length;             // Length of this structure
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=6)]
        public byte[] MacAddress;         // BSSID        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
        public byte[] Reserved;
        public UInt32 SsidLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
        public byte[] Ssid;               // SSI        
        public UInt32 Privacy;            // WEP encryption requirement
        public Int32 Rssi;                // receive signal strength in dBm
        public NdisNetworkType NetworkTypeInUse;
        public Ndis802Dot11Configuration Configuration;
        public NetworkInfrastructure InfrastructureMode;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
        public byte[] SupportedRates;
        public UInt32 IELength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=512)]
        public byte[] IEs;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=4)]
    public struct Ndis802Dot11BssidListEx
    {
        public uint NumberOfItems;      // in list below, at least 1
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
        public NdisWlanBssidEx[] Bssid;
    }// End Ndis802Dot11BssidListEx

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=1)]
    public struct Ndis802Dot11FixedIes
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
        public byte[] Timestamp;
        public UInt16 BeaconInterval;
        public UInt16 Capabilities;
    } // End Ndis802Dot11FixedIes

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=1)]
    public struct Ndis802Dot11VariableIes
    {
        public byte ElementId;
        public byte Length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
        public byte[] data;
    } // End Ndis802Dot11VariableIes
    [
        ComVisible(false),
        System.Security.SuppressUnmanagedCodeSecurityAttribute
    ]
    public class IoctlNdis
    {
        public const byte HtCapabilitiesLength = 28;
        public const byte HtExtendedCapabilitiesLength = 24;

        public const int InvalidHandleValue = -1;
        public const int StatusSuccess = 0;
        public const int ErrorFileNotFound = 2;
        public const string DevicePrefix = "\\\\.\\";
        public const uint IoctlNdisQueryGlobalStats = 0x00170002;

        [DllImport("Kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool DeviceIoControl(
            [In] IntPtr deviceHandle,
            [In] uint controlCode,
            [In] IntPtr inBuffer,
            [In] int oidSize,
            [In] IntPtr outBuffer,
            [In] int outBufferSize,
            [In, Out] ref int bytesReturned,
            [In] IntPtr overlapped);

        [DllImport("Kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern IntPtr CreateFile(
            [In] String fileName,
            [In] UInt32 desiredAccess,
            [In] FileShareFlags shareMode,
            [In, Optional] IntPtr securityAttributes,
            [In] FileCreateFlags creationDisposition,
            [In] UInt32 flagsAndAttributes,
            [In] IntPtr templateFile);

        [DllImport("Kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool CloseHandle(
            [In] IntPtr handle);

        /// <summary>
        /// Converts a pointer to a BSS list (header + entries) to an array of BSS entries.
        /// </summary>
        /// <returns>An array of BSS entries.</returns>
        private NdisWlanBssidEx[] ConvertBssidListPtr(IntPtr bssidListPtr) {
            //
            // Marshal the bssid list structure to get the number of items plus
            // the first bssid entry.
            //
            Ndis802Dot11BssidListEx bssidList =
                (Ndis802Dot11BssidListEx)Marshal.PtrToStructure(bssidListPtr,
                    typeof(Ndis802Dot11BssidListEx));
            //
            // Iterate through memory and marshal each bssid entry. We have 
            // to marshal the individual structures one at a time, since they
            // vary in size. 
            //
            NdisWlanBssidEx[] bssidEntries = null;
            if (bssidList.NumberOfItems > 0) {
                bssidEntries = new NdisWlanBssidEx[bssidList.NumberOfItems];

                long bssidAddress = bssidListPtr.ToInt64() + Marshal.SizeOf(bssidList.NumberOfItems)
                    + bssidList.Bssid[0].Length;
                bssidEntries[0] = bssidList.Bssid[0];
                //
                // Starting from the second array entry, iterate and marshal each of the
                // existing bssid entries.
                //
                for (int i = 1; i < bssidList.NumberOfItems; ++i) {
                    //
                    // Marshal the currently referenced bssid item and add it to the 
                    // array.
                    //                    
                    NdisWlanBssidEx bssidEntry = (NdisWlanBssidEx)Marshal.PtrToStructure(
                        new IntPtr(bssidAddress), typeof(NdisWlanBssidEx));
                    bssidEntries[i] = bssidEntry;

                    //
                    // Move the pointer to the next bssid entry.
                    //
                    bssidAddress += bssidEntry.Length;
                }

                // Patch from cborn:
                // We can receive either NDIS_WLAN_BSSID (104 bytes) or NDIS_WLAN_BSSID_EX (>=120 bytes) here
                // More of my devices send the obsolete 104 byte one than the new one
                // We should handle it properly, but for now I'll just set the IELength field to 0,
                // and clear out the additional 8 supported rate fields
                // DAV 20AUG08
                for (int i = 0; i < bssidList.NumberOfItems; ++i) {
                    if (bssidEntries[i].Length < 120) {
                        bssidEntries[i].IELength = 0;
                        for (int j = 8; j < 16; ++j)
                            bssidEntries[i].SupportedRates[j] = 0;
                    }
                }
            }
            return bssidEntries;
        }// End ConvertBssidListPtr        

        /// <summary>
        /// Opens a network device driver file. If this function
        /// fails to open the device for any reason, an exception is thrown.
        /// </summary>
        /// <param name="serviceName">GUID service name of the device to open</param>
        /// <returns>handle to the device.</returns>
        private IntPtr OpenDevice(string serviceName) {
            //
            // Open the driver to get a device handle.
            //
            string fileName = DevicePrefix + serviceName;
            IntPtr deviceHandle = CreateFile(
                fileName,
                0,
                FileShareFlags.FileShareRead | FileShareFlags.FileShareWrite,
                IntPtr.Zero,
                FileCreateFlags.OpenExisting,
                0,
                IntPtr.Zero
            );

            //
            // Raise an exception if we cannot open the device. Since we 
            // know the adapter name, this should never happen. However, we will
            // get this error if the network adapter is disabled when the
            // application is running.            
            //
            if (deviceHandle.ToInt32() == InvalidHandleValue) {
                String message = String.Format("Unable to open device {0}. Win32Error = {1}",
                    fileName, Marshal.GetLastWin32Error());
                throw new System.ComponentModel.Win32Exception(message);
            }

            return (deviceHandle);
        } // End OpenDevice

/*
        /// <summary>
        /// Performs a network scan for BSSIDs.
        /// </summary>
        /// <param name="adapter">object representing</param>
        public void ScanBssidList(NetworkInterface adapter) {
            IntPtr deviceHandle = OpenDevice(adapter.Id);
            try {
                // dummy reference variable
                int bytesReturned = new int();
                // tell the driver to scan the networks
                bool success = QueryGlobalStats(
                                        deviceHandle,
                                        Oid.Oid80211BssidListScan,
                                        IntPtr.Zero,
                                        0,
                                        ref bytesReturned
                                    );
                if (success) {
                    // Nothing to do
                }
            }
            finally {
                CloseHandle(deviceHandle);
            }
        } */
// End ScanBssidList()

        /// <summary>
        /// Initiates a discovery scan of the available wireless networkds.
        /// </summary>
        public bool Scan(NetworkInterface adapter)
        {
            try
            {
                ManagementClass mc = new ManagementClass("root\\WMI", "MSNDis_80211_BssIdListScan", null);
                ManagementObject mo = mc.CreateInstance();
                if (mo != null)
                {
                    mo["Active"] = true;
                    mo["InstanceName"] = adapter.Description; //.Replace(" - Packet Scheduler Miniport","");
                    mo["UnusedParameter"] = 1;
                    mo.Put();
                }
            }
            catch (ManagementException ex)
            {
                if(ex.ErrorCode == ManagementStatus.NotSupported)
                {
                    //The operation is not supported, probably not an WiFi adapter.
                    return false;
                }
            }
            catch
            {
                // TODO: Verify root cause of exception.
                // Ignore, for now
                // there seems to be some issues with WMI on certain systems. Various exceptions have been
                // reported from this method, which relate to problems with WMI.
            }
            return true;
        } // End Scan()
        /// <summary>
        /// Queries the BSSID List from the NDIS layer.
        /// </summary>
        /// <param name="adapter">object representing the adapter's name</param>
        /// <returns>a list of BSSID</returns>
        public IEnumerable<NdisWlanBssidEx> QueryBssidList(NetworkInterface adapter) {

            NdisWlanBssidEx[] bssidList = null;

            //
            // Get a handle to the device.
            //
            IntPtr deviceHandle = OpenDevice(adapter.Id);
            try {
                //
                // Allocate memory to hold the BSSID list
                //
                //int memSize = Marshal.SizeOf(typeof(Ndis802Dot11BssidListEx)) +
                //              Marshal.SizeOf(typeof(NdisWlanBssidEx)) * 16;
                const int memSize = 65536;
                IntPtr bssidPtr = Marshal.AllocHGlobal(memSize);
                try {
                    //
                    // Send the OID to the driver to get the 
                    // BSSID list, which will contain information for all of the 
                    // available networks.
                    // Note: for more up-to-date results, you may want to 
                    // perform a BSSID_LIST_SCAN first. This implementation
                    // will take the information from the last scan,
                    //
                    int bytesReturned = new int();
                    bool success = QueryGlobalStats(
                        deviceHandle,
                        Oid.Oid80211BssidList,
                        bssidPtr,
                        memSize,
                        ref bytesReturned
                    );
                    if (success) {
                        //
                        // Convert the buffer to an array of BSSID
                        // items.
                        //
                        bssidList = ConvertBssidListPtr(bssidPtr);
                    }
                }
                finally {
                    Marshal.FreeHGlobal(bssidPtr);
                }
            }
            finally {
                bool closed = CloseHandle(deviceHandle);
                if (!closed) {
                    Debug.WriteLine("Close failed with error code " + Marshal.GetLastWin32Error());
                }
            }
            return (bssidList);

        } // End QueryBssidList

        /// <summary>
        /// Queries the BSSID of the currently connected AP from the NDIS layer.
        /// </summary>
        /// <param name="adapter">object representing the adapter's name</param>
        /// <returns>a byte array representing the BSSID of the connected AP</returns>
        public byte[] QueryConnected(NetworkInterface adapter)
        {
            byte[] bssid = new byte[] {0, 0, 0, 0, 0, 0};

            IntPtr deviceHandle = OpenDevice(adapter.Id);
            try
            {
                IntPtr oidBuffer = Marshal.AllocHGlobal(sizeof(byte) * 6);
                try
                {
                    int bytesReturned = new int();
                    bool success = QueryGlobalStats(
                        deviceHandle,
                        Oid.Oid80211Bssid,
                        oidBuffer,
                        sizeof(byte) * 6,
                        ref bytesReturned);
                    if (success)
                    {
                        bssid = ConvertToByteArray(oidBuffer, bytesReturned);
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(oidBuffer);
                }
            }
            finally
            {
                bool closed = CloseHandle(deviceHandle);
                if (!closed)
                {
                    Debug.WriteLine("Unable to close handle with error code " + Marshal.GetLastWin32Error());
                }
            }

            return (bssid);
        } // End QueryConnected()

        /// <summary>
        /// Converts an intPtr into a byte array.
        /// </summary>
        /// <param name="intPtr">unmanaged memory pointer</param>
        /// <param name="bytes">number of bytes</param>
        /// <returns>abyte array</returns>
        private byte[] ConvertToByteArray(IntPtr intPtr, int bytes)
        {
            int arraySize = bytes / sizeof(byte);
            byte[] array = new byte[arraySize];

            for (int i = 0; i < arraySize; ++i)
            {
                array[i] = Marshal.ReadByte(intPtr, i * sizeof(byte));
            }
            return (array);
        } // End ConvertToByteArray()

/*
        /// <summary>
        /// Gets the authenticaion mode for a particular adapter.
        /// </summary>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public AuthenticationMode QueryAuthenticationMode(AdapterInformation adapter) {
            AuthenticationMode authMode = AuthenticationMode.Ndis80211AuthModeWpaNone;
            IntPtr deviceHandle = OpenDevice(adapter.ServiceName);
            try {
                IntPtr authModePtr = Marshal.AllocHGlobal(sizeof(int));
                try {
                    int bytesReturned = new int();
                    bool result = QueryGlobalStats(
                        deviceHandle,
                        Oid.Oid80211AuthenticationMode,
                        authModePtr,
                        sizeof(int),
                        ref bytesReturned
                        );
                    if (result) {
                        authMode = (AuthenticationMode)Marshal.ReadInt32(authModePtr);
                    }
                    else {
                        Debug.WriteLine("Unable to query authentication mode. " + adapter);
                    }
                }
                finally {
                    Marshal.FreeHGlobal(authModePtr);
                }
            }
            finally {
                bool closed = CloseHandle(deviceHandle);
                if (!closed) {
                    Debug.WriteLine("Unable to close handle with error code " + Marshal.GetLastWin32Error());
                }
            }
            return (authMode);
        } */
// End QueryAuthenticationMode()

/*
        /// <summary>
        /// See OID_GEN_SUPPORTED_LIST. The OID_GEN_SUPPORTED_LIST OID specifies an array of 
        /// OIDs for objects that the underlying driver or its NIC supports
        /// </summary>
        /// <param name="adapter">The Adapter to query</param>
        /// <returns>An array of supported OIDs</returns>
        public uint[] QuerySupportedOids(NetworkInterface adapter) {
            uint[] oidList = null;

            IntPtr deviceHandle = OpenDevice(adapter.Id);
            try {
                IntPtr oidBuffer = Marshal.AllocHGlobal(sizeof(uint) * 1024);
                try {
                    int bytesReturned = new int();
                    bool success = QueryGlobalStats(
                        deviceHandle,
                        Oid.OidGenSupportedList,
                        oidBuffer,
                        sizeof(int) * 1024,
                        ref bytesReturned);
                    if (success) {
                        oidList = ConvertToArray(oidBuffer, bytesReturned);
                    }
                }
                finally {
                    Marshal.FreeHGlobal(oidBuffer);
                }
            }
            finally {
                bool closed = CloseHandle(deviceHandle);
                if (!closed) {
                    Debug.WriteLine("Unable to close handle with error code " + Marshal.GetLastWin32Error());
                }
            }

            return (oidList);
        } */
// End QuerySupportedOids()

/*
        /// <summary>
        /// Converts an intPtr into an array of uint objects.
        /// </summary>
        /// <param name="intPtr">unmanaged memory pointer</param>
        /// <param name="bytes">number of bytes</param>
        /// <returns>an array of uints</returns>
        private uint[] ConvertToArray(IntPtr intPtr, int bytes) {
            int arraySize = bytes / sizeof(uint);
            uint[] array = new uint[arraySize];
            //Int64 address = intPtr.ToInt64();
            for (int i = 0; i < arraySize; ++i) {
                array[i] = (uint)Marshal.ReadInt32(intPtr, i * sizeof(uint));
            }
            return (array);
        } */
// End ConvertToArray()

        /// <summary>
        /// Performs a device ioctl operation to query the global stats for
        /// the given device. <see cref="IoctlNdisQueryGlobalStats"/> in the 
        /// DDK documentation.
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="oidCode"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="bytesRead"></param>
        /// <returns></returns>
        private bool QueryGlobalStats(IntPtr deviceHandle, Oid oidCode, IntPtr buffer, int bufferSize, ref int bytesRead) {

            bool result;    // function return value            

            // 
            // Allocate a buffer to hold the OID code that 
            // will be passed to the driver.
            //            
            IntPtr oidPtr = Marshal.AllocHGlobal(sizeof(int));
            try {
                // Get a pointer to the OID code
                Marshal.WriteInt32(oidPtr, (int)oidCode);
                //
                // We successfully opened the driver, format the IOCTL to pass the
                // driver.
                //                              
                result = DeviceIoControl(
                        deviceHandle,
                        IoctlNdisQueryGlobalStats,
                        oidPtr,
                        sizeof(int),
                        buffer,
                        bufferSize,
                        ref bytesRead,
                        IntPtr.Zero);
                if (!result) {
                    Debug.WriteLine("DeviceIoControl failed. Error = " + Marshal.GetLastWin32Error());
                }

            }
            finally {
                // Make sure the memory is freed
                Marshal.FreeHGlobal(oidPtr);
            }
            return (result);
        } // End QueryGlobalStats

        /// <summary>
        /// Information elements
        /// </summary>
        private const byte WlanEidRsn = 48;
        private const byte WlanEidVendorSpecific = 221;

        /// <summary>
        /// Returns the string representation of the privacy mode
        /// for the specified bssid.
        /// </summary>
        /// <param name="bssidItem">bssid from which to read the privacy mode</param>
        /// <returns>a string representing the privacy mode</returns>
        public string GetPrivacyString(NdisWlanBssidEx bssidItem) {
            string privacyMode = "None";

            if (bssidItem.Privacy != 0) {
                privacyMode = "WEP";
                //
                // To get the privacy information, the variable length 
                // information elements must be inspected. Thereore, we 
                // skip past the fixed information elements.
                //
                int index = Marshal.SizeOf(typeof(Ndis802Dot11FixedIes));

                //
                // Iterate through the variable information elements.
                //
                int length = (int)bssidItem.IELength - 2;
                while ((index >= 0) && (index <= length)) {

                    //
                    // Get the element id and the length of the 
                    // element.
                    //
                    // TODO: Should index be incremented here, since
                    // it is incremented again down below?
                    byte elementId = bssidItem.IEs[index++];
                    byte elementLength = bssidItem.IEs[index++];

                    //
                    // Determine the required privacy mode.
                    //
                    if (WlanEidRsn == elementId) {
                        // Parse the RSN cipher suite value
                        if (elementLength >= 6 && 
                             bssidItem.IEs[index + 0] == 0x01 &&  // version msb
                             bssidItem.IEs[index + 1] == 0x00 &&  // version lsb
                             bssidItem.IEs[index + 2] == 0x00 &&  // cipher suite id b1
                             bssidItem.IEs[index + 3] == 0x0F &&  // cipher suite id b2
                             bssidItem.IEs[index + 4] == 0xAC) { // cipher suite id b3

                            // Dispatch on the cipher suite selector
                            switch (bssidItem.IEs[index + 5]) {

                                case 1: // WEP-40
                                    privacyMode = "WEP-40";
                                    break;

                                case 5: // WEP-104
                                    privacyMode = "WEP-104";
                                    break;

                                case 2: // TKIP
                                    privacyMode = "WPA-TKIP";
                                    break;

                                case 4: // CCMP
                                    privacyMode = "WPA2-CCMP";
                                    break;
                                default:
                                    privacyMode = "Unknown";
                                    break;

                            }
                        }

                        Debug.WriteLine("WLAN_EID_RSN : length = " + elementLength);
                        break;
                    }

                    if (WlanEidVendorSpecific == elementId) {
                        Debug.WriteLine("WLAN_EID_VENDOR_SPECIFIC");
                        // BEGIN FM
                        // Parse the Microsoft OUI
                        if (elementLength  >= 3 &&
                            bssidItem.IEs[index + 0] == 0x00 &&
                            bssidItem.IEs[index + 1] == 0x50 &&
                            bssidItem.IEs[index + 2] == 0xF2 &&
                            bssidItem.IEs[index + 3] == 0x01) { // 1 == WPA, 2 == WMM ???
                            privacyMode = "WPA";
                        }
                        // END FM
                    }
                    //
                    // Move to the next information element.
                    //
                    // TODO: We incremented index twice before this call. Is it valid
                    // to increment by element length again?
                    index += elementLength;
                }
            }

            return (privacyMode);
        } // End GetPrivacyString()


/*
        public static object RawDataToObject(ref byte[] rawData, Type overlayType) {
            object result;

            //
            // Pin the memory location, so the garbage collector does
            // not re-arrange the fields of the data.
            //
            GCHandle pinnedRawData = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            try {
                //
                // Get the address of the data array
                //
                IntPtr pinnedRawDataPtr = pinnedRawData.AddrOfPinnedObject();

                // 
                // overlay the data type on top of the raw data
                //
                result = Marshal.PtrToStructure(pinnedRawDataPtr, overlayType);
            }
            finally {
                // 
                // We must explicitly release 
                //
                pinnedRawData.Free();
            }

            return result;
        }
*/
    }
}
