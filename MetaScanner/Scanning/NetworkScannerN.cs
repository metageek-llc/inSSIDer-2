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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using inSSIDer.Scanning.Interfaces;
using MetaGeek.WiFi;
using inSSIDer.Misc;
using System.Net.NetworkInformation;
using Timer=System.Timers.Timer;

namespace inSSIDer.Scanning
{
    public class NetworkScannerN : IDisposable
    {
        private IScanningInterface scanInterface;
        private WaitHandle[] MyWaitHandleArray;

        private readonly AutoResetEvent MyScanCompleteEvent = new AutoResetEvent(true);
        private readonly AutoResetEvent MyScanThrottleEvent = new AutoResetEvent(true);
        private readonly ManualResetEvent MyTerminateEvent = new ManualResetEvent(false);

        private Timer SpeedTimer = new Timer(1000) {AutoReset = false};
        private WaitHandle[] SpeedWait;

        private bool IsStopping;

        private delegate void DelTest();

        /// <summary>
        /// Raised when new network data is acquired
        /// </summary>
        public event EventHandler<IncomingDataEventArgs<NetworkData>> NewNetworkDataEvent;

        /// <summary>
        /// Raised when an invalid interface is selected
        /// </summary>
        public event EventHandler InterfaceError;

        private Thread MyScanThread;

        public NetworkScannerN()
        {
            MyWaitHandleArray = new WaitHandle[] { MyTerminateEvent, MyScanCompleteEvent };
            SpeedWait = new WaitHandle[] {MyScanThrottleEvent};
            SpeedTimer.Elapsed += SpeedTimer_Elapsed;

            if(Utilities.IsXp())
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

        private void scanInterface_InterfaceError(object sender, EventArgs e)
        {
            new Thread(StopOnError).Start();
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

        private void scanInterface_ScanComplete(object sender, EventArgs e)
        {
            MyScanCompleteEvent.Set();
        }

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

            if(error != null) return false;

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

                MyTerminateEvent.Reset();
                MyScanThrottleEvent.Set();
            }
            IsScanning = false;
        }

        private void ScanThreadFunc()
        {
            while (true)
            {
                //Wait for the terminate signal, the scan complete signal, or 3 seconds
                //WaitHandle.WaitAll()
                int num = WaitHandle.WaitAny(MyWaitHandleArray, 3000);
                if ((num != 258) && (num != 1))
                {
                    //Stop the scanning loop
                    break;
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

        private void OnNewNetworkData(IEnumerable<NetworkData> data)
        {
            if (NewNetworkDataEvent == null) return;
            NewNetworkDataEvent(this, new IncomingDataEventArgs<NetworkData>(data));
        }

        private void OnInterfaceError()
        {
            if (InterfaceError == null) return;
            InterfaceError(this, EventArgs.Empty);
        }

        private void SpeedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MyScanThrottleEvent.Set();
        }

        /// <summary>
        /// Gets if this scanner is scanning.
        /// </summary>
        public bool IsScanning { get; private set; }

        /// <summary>
        /// Gets or sets the network interface to scan with.
        /// </summary>
        /// <remarks>Must be a WiFi adapter</remarks>
        public NetworkInterface NetworkInterface { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
