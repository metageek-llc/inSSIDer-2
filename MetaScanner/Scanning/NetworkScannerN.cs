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
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using inSSIDer.Misc;
using inSSIDer.Scanning.Interfaces;

using MetaGeek.WiFi;

using Timer = System.Timers.Timer;

namespace inSSIDer.Scanning
{
    public class NetworkScannerN : IDisposable
    {
        #region Fields

        private bool IsStopping;
        private Thread MyScanThread;
        private WaitHandle[] MyWaitHandleArray;
        private IScanningInterface scanInterface;
        private Timer SpeedTimer = new Timer(1000) { AutoReset = false };
        private WaitHandle[] SpeedWait;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets if this scanner is scanning.
        /// </summary>
        public bool IsScanning
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the network interface to scan with.
        /// </summary>
        /// <remarks>Must be a WiFi adapter</remarks>
        public NetworkInterface NetworkInterface
        {
            get;
            set;
        }

        #endregion Properties

        #region Event Fields

        private readonly AutoResetEvent MyScanCompleteEvent = new AutoResetEvent(true);
        private readonly AutoResetEvent MyScanThrottleEvent = new AutoResetEvent(true);
        private readonly ManualResetEvent MyTerminateEvent = new ManualResetEvent(false);

        #endregion Event Fields

        #region Events

        /// <summary>
        /// Raised when an invalid interface is selected
        /// </summary>
        public event EventHandler InterfaceError;

        /// <summary>
        /// Raised when new network data is acquired
        /// </summary>
        public event EventHandler<IncomingDataEventArgs<NetworkData>> NewNetworkDataEvent;

        #endregion Events

        #region Delegates

        private delegate void DelTest();

        #endregion Delegates

        #region Constructors

        public NetworkScannerN()
        {
            MyWaitHandleArray = new WaitHandle[] { MyTerminateEvent, MyScanCompleteEvent };
            SpeedWait = new WaitHandle[] { MyScanThrottleEvent };
            SpeedTimer.Elapsed += SpeedTimer_Elapsed;

            if (Utilities.IsXp())
            {
                scanInterface = new NdisScanInterface();
            }
            else
            {
                scanInterface = new ManagedScanInterface();

            }

            scanInterface.ScanComplete += scanInterface_ScanComplete;
            scanInterface.InterfaceError += scanInterface_InterfaceError;
        }

        #endregion Constructors

        #region Dispose

        public void Dispose()
        {
            Stop();
        }

        #endregion Dispose

        #region Public Methods

        public bool Start()
        {
            return NetworkInterface != null && Start(NetworkInterface);
        }

        public bool Start(NetworkInterface networkInterface)
        {
            Exception error;

            //Set up the interface with the specified interface
            scanInterface.Init(networkInterface, out error);

            //Set the interface used
            NetworkInterface = networkInterface;

            if (error != null) return false;

            MyTerminateEvent.Reset();

            //Create a new thread for scanning
            MyScanThread = new Thread(ScanThreadFunc);
            MyScanThread.Start();

            IsScanning = true;
            return true;
        }

        public void Stop()
        {
            if (!IsScanning) return;

            if (MyScanThread != null)
            {
                //Give the terminate signal
                MyTerminateEvent.Set();
                //If the thread isn't stopped in 1 second, kill it.
                if (!MyScanThread.Join(1000))
                {
                    MyScanThread.Abort();
                }
                MyScanThread = null;

                //MyTerminateEvent.Reset();
                MyScanThrottleEvent.Set();
            }
            IsScanning = false;
        }

        #endregion Public Methods

        #region Private Methods

        private void OnInterfaceError()
        {
            if (InterfaceError == null) return;
            InterfaceError(this, EventArgs.Empty);
        }

        private void OnNewNetworkData(IEnumerable<NetworkData> data)
        {
            if (NewNetworkDataEvent == null) return;
            NewNetworkDataEvent(this, new IncomingDataEventArgs<NetworkData>(data));
        }

        private void scanInterface_InterfaceError(object sender, EventArgs e)
        {
            new Thread(StopOnError).Start();
        }

        private void scanInterface_ScanComplete(object sender, EventArgs e)
        {
            MyScanCompleteEvent.Set();
        }

        private void ScanThreadFunc()
        {
            while (true)
            {
                //Wait for the terminate signal, the scan complete signal, or 3 seconds
                //WaitHandle.WaitAll()
                try
                {
                    int num = WaitHandle.WaitAny(MyWaitHandleArray, 3000);
                    if (num != WaitHandle.WaitTimeout && (num != 1))
                    {
                        //Stop the scanning loop
                        break;
                    }
                }
                catch (MissingMethodException)
                {
                    MessageBox.Show("Could not start scanning, please check for .NET Framework service pack update");
                    return;
                }

                //Scan speed throttling
                WaitHandle.WaitAll(SpeedWait);

                try
                {
                    lock (scanInterface)
                    {
                        IEnumerable<NetworkData> dataList = scanInterface.GetNetworkData();
                        OnNewNetworkData(dataList);

                        scanInterface.ScanNetworks();

                        //Start the wait
                        SpeedTimer.Start();
                    }
                }
                catch (Win32Exception)
                {
                }
            }
        }

        private void SpeedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MyScanThrottleEvent.Set();
        }

        private void StopOnError()
        {
            if (IsStopping) return;
            lock (this)
            {
                IsStopping = true;

                //Stop the scanning
                Stop();

                OnInterfaceError();
                IsStopping = false;
            }
        }

        #endregion Private Methods
    }
}