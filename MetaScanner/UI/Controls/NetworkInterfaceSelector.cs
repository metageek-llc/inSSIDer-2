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
using System.ComponentModel;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Timers;
using System.Windows.Forms;
using inSSIDer.Misc;
using inSSIDer.Scanning;
using ManagedWifi;
using inSSIDer.Localization;

namespace inSSIDer.UI.Controls
{
    public partial class NetworkInterfaceSelector : UserControl
    {
        private readonly Bitmap _myStartImage = new Bitmap(Properties.Resources.wifiPlay);
        private readonly Bitmap _myStopImage = new Bitmap(Properties.Resources.wifiStop);

        public event EventHandler<EventArgs> NetworkScanStartEvent;

        public event EventHandler<EventArgs> NetworkScanStopEvent;

        private System.Timers.Timer _myTimer;

        private delegate void UpdateInterfaceListHandler();

        private delegate void DelInterfaceChange(object sender, InterfaceNotificationEventsArgs e);
        private delegate void DelInvokeNoArg();
        private delegate void DelInvokeBool(bool value);

        private ScanController _scanner;

        private bool _checkInterfaceInit = true;



        public NetworkInterfaceSelector()
        {
            InitializeComponent();
            MaxTextLength = -1;
        }

        public void Initialize(ref ScanController scanner)
        {
            _scanner = scanner;
            if(_scanner == null) return;

            //NetworkController.Instance.Initialize();

            // We have to manually check for new interfaces in Windows XP
            if (Utilities.IsXp())
            {
                _myTimer = new System.Timers.Timer { Interval = 5000.0, Enabled = true };
                _myTimer.Elapsed += MyTimer_Elapsed;
            }
            else
            {
                InterfaceManager.Instance.InterfaceAdded += WlanClient_InterfaceAddedEvent;
                InterfaceManager.Instance.InterfaceRemoved += WlanClient_InterfaceRemoveEvent;
            }
            
            UpdateInterfaceList();

            //If we are not on XP and only have 1 interface, start scanning
            if (!Utilities.IsXp() && InterfaceManager.Instance.Interfaces.Length == 1)
            {
                StartScan();
            }
        }

        private void InvokeNetworkScanStartEvent()
        {
            if (NetworkScanStartEvent != null)
            {
                NetworkScanStartEvent(this, EventArgs.Empty);
            }
        }

        private void InvokeNetworkScanStopEvent()
        {
            if (NetworkScanStopEvent != null)
            {
                NetworkScanStopEvent(this, EventArgs.Empty);
            }
        }

        private void WlanClient_InterfaceAddedEvent(object sender, InterfaceNotificationEventsArgs e)
        {
            UpdateInterfaceList();
            //If we are not scanning and a new interface is added, use it!
            if (!_scanner.NetworkScanner.IsScanning && _scanner.SetInterface(e.MyGuid))
            {
                try
                {
                    //This will always need to be invoked
                    Invoke(new DelInvokeNoArg(StartScan));
                }
                catch (InvalidOperationException)
                {
                    // Exception thrown if UI isn't fully initialized yet. 
                    // Ignoring this exception will force the user to manually click the "Start" button
                }
            }
        }

        private void WlanClient_InterfaceRemoveEvent(object sender, InterfaceNotificationEventsArgs e)
        {
            if (e.MyGuid == new Guid(_scanner.Interface.Id))
            {
                //If we were using the interface that got removed, stop scanning!
                if (InvokeRequired)
                    Invoke(new DelInterfaceChange(WlanClient_InterfaceRemoveEvent), new[] { sender, e });
                else
                {
                    StopScan();
                }
            }
            UpdateInterfaceList();
        }

        private void NetworkInterfaceDropDown_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem clickedItem = e.ClickedItem as ToolStripMenuItem;
            if (clickedItem != null)
            {
                //NetworkInterfaceDropDown.Text = clickedItem.Text;
                NetworkInterfaceDropDown.Text = MaxTextLength > -1 && clickedItem.Text.Length > MaxTextLength ? clickedItem.Text.Remove(MaxTextLength - 1) + "..." : clickedItem.Text;
                UpdateInterfaceListSelection();
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_scanner.NetworkScanner.IsScanning)
                {
                    StopScan();
                }
                else
                {
                    if (ModifierKeys != Keys.Shift)
                    {
                        _scanner.Cache.Clear();
                        Utilities.ResetColor();
                    }
                    StartScan();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateInterfaceList();
        }

        private void UpdateInterfaceList()
        {
            if (InvokeRequired)
            {
                UpdateInterfaceListHandler method = UpdateInterfaceList;
                Invoke(method);
            }
            else
            {
                lock (this)
                {
                    NetworkInterface[] interfaceArray = (NetworkInterface[])InterfaceManager.Instance.Interfaces.Clone();
                    bool clearList = true;

                    try
                    {
                        if (interfaceArray.Length > 0)
                        {
                            if (!NetworkInterfaceDropDown.Pressed)
                            {
                                NetworkInterfaceDropDown.DropDownItems.Clear();
                                foreach (NetworkInterface networkInterface in interfaceArray)
                                {
                                    NetworkInterfaceDropDown.DropDownItems.Add(networkInterface.Description);
                                }
                                NetworkInterfaceDropDown.ShowDropDownArrow =
                                    NetworkInterfaceDropDown.DropDownItems.Count > 0;
                                UpdateInterfaceListSelection();
                            }
                            clearList = false;
                        }
                    }
                    catch(NullReferenceException)
                    {
                    }
                    if (clearList)
                    {
                        NetworkInterfaceDropDown.DropDownItems.Clear();
                        NetworkInterfaceDropDown.Text = Localizer.GetString("NoWiFiInterfacesFound");
                        ScanButton.Enabled = false;
                    }
                }
            }
        }

        private void UpdateInterfaceListSelection()
        {
            bool flag = false;

            //Check for current interface, only once
            if (_scanner.Interface != null && _checkInterfaceInit)
            {
                NetworkInterfaceDropDown.Text = _scanner.Interface.Description;

                foreach (ToolStripMenuItem item in NetworkInterfaceDropDown.DropDownItems)
                {
                    if (item.Text.Equals(NetworkInterfaceDropDown.Text))
                    {
                        item.Checked = true;
                        flag = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
                _checkInterfaceInit = false;
            }


            foreach (ToolStripMenuItem item in NetworkInterfaceDropDown.DropDownItems)
            {
                if (item.Text.Equals(NetworkInterfaceDropDown.Text))
                {
                    if (_scanner.SetInterface(item.Text))
                    {
                        item.Checked = true;
                        flag = true;
                    }
                }
                else
                {
                    item.Checked = false;
                }
            }
            if (!flag)
            {
                _scanner.StopScanning();
                _scanner.Interface = null;
                if (NetworkInterfaceDropDown.DropDownItems.Count > 0)
                {
                    string text = NetworkInterfaceDropDown.DropDownItems[0].Text;
                    _scanner.SetInterface(text);
                    ((ToolStripMenuItem)NetworkInterfaceDropDown.DropDownItems[0]).Checked = true;
                    NetworkInterfaceDropDown.Text = MaxTextLength > -1 && text.Length > MaxTextLength ? text.Remove(MaxTextLength - 1) + "..." : text;
                }
                else
                {
                    NetworkInterfaceDropDown.Text = Localizer.GetString("NoWirelessInterface");
                }
            }
            NetworkInterfaceDropDown.Enabled = !_scanner.NetworkScanner.IsScanning;
            UpdateScanButtonState(_scanner.NetworkScanner.IsScanning);
        }

        private void UpdateScanButtonState(bool isStarted)
        {
            if(InvokeRequired)
            {
                Invoke(new DelInvokeBool(UpdateScanButtonState), new object[] {isStarted});
            }
            else
            {
                if (isStarted)
                {
                    ScanButton.Text = Localizer.GetString("Stop");
                    ScanButton.Image = _myStopImage;
                }
                else
                {
                    ScanButton.Text = Localizer.GetString("Start");
                    ScanButton.Image = _myStartImage;
                }
                ScanButton.Enabled = _scanner.Interface != null;
            }
        }

        /// <summary>
        /// Update control to reflect starting the scan and fire the scan start event
        /// </summary>
        internal void StartScan()
        {
            UpdateScanButtonState(true);
            if (NetworkInterfaceDropDown != null)
            {
                NetworkInterfaceDropDown.Enabled = false;

                _scanner.StartScanning();
                InvokeNetworkScanStartEvent();
            }
        }

        /// <summary>
        /// Update control to reflect stopping the scan and fire the scan stop event
        /// </summary>
        internal void StopScan()
        {
            UpdateScanButtonState(false);
            NetworkInterfaceDropDown.Enabled = true;

            _scanner.StopScanning();
            InvokeNetworkScanStopEvent();
        }

        /// <summary>
        /// Updates the control to reflect the current scan state
        /// </summary>
        internal void UpdateView()
        {
            UpdateScanButtonState(_scanner.NetworkScanner.IsScanning);
            NetworkInterfaceDropDown.Enabled = true;
        }

        /// <summary>
        /// Gets to sets the maximum length of an interface name
        /// </summary>
        [Category("Behavior"),DefaultValue(-1)]
        public int MaxTextLength { get; set; }
        
    }
}
