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
using System.ComponentModel;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Timers;
using System.Windows.Forms;
using inSSIDer.Localization;
using inSSIDer.Misc;
using inSSIDer.Scanning;
using ManagedWifi;

namespace inSSIDer.UI.Controls
{
    public partial class NetworkInterfaceSelector : UserControl
    {
        #region Fields

        private bool _checkInterfaceInit = true;
        private readonly Bitmap _myStartImage = new Bitmap(Properties.Resources.wifiPlay);
        private readonly Bitmap _myStopImage = new Bitmap(Properties.Resources.wifiStop);
        private System.Timers.Timer _myTimer;
        private ScanController _scanner;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets to sets the maximum length of an interface name
        /// </summary>
        [Category("Behavior"),
        DefaultValue(-1)]
        public int MaxTextLength
        {
            get; set;
        }

        #endregion Properties

        #region Events

        public event EventHandler<EventArgs> NetworkScanStartEvent;

        public event EventHandler<EventArgs> NetworkScanStopEvent;

        #endregion Events

        #region Invoke Methods

        private delegate void DelInvokeBool(bool value);

        private delegate void DelInvokeNoArg();

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

        #endregion Invoke Methods

        #region Delegates

        private delegate void DelInterfaceChange(object sender, InterfaceNotificationEventsArgs e);

        private delegate void UpdateInterfaceListHandler();

        #endregion Delegates

        #region Constructors

        public NetworkInterfaceSelector()
        {
            InitializeComponent();
            MaxTextLength = -1;
        }

        #endregion Constructors

        #region Public Methods

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

        #endregion Public Methods

        #region Private Methods

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
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
                NetworkInterfaceDropDown.Text = MaxTextLength > -1 && _scanner.Interface.Description.Length > MaxTextLength ? _scanner.Interface.Description.Remove(MaxTextLength - 1) + "..." : _scanner.Interface.Description;

                foreach (ToolStripMenuItem item in NetworkInterfaceDropDown.DropDownItems)
                {
                    if (item.Text.StartsWith(NetworkInterfaceDropDown.Text.Replace("...", ""), StringComparison.InvariantCultureIgnoreCase))
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
                if (item.Text.StartsWith(NetworkInterfaceDropDown.Text.Replace("...",""), StringComparison.InvariantCultureIgnoreCase))
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
        /// Updates the displayed text on the interface selector. Used for auto-start on plug-in
        /// </summary>
        private void UpdateSelection()
        {
            if (_scanner == null || _scanner.Interface == null) return;

            foreach (ToolStripMenuItem item in NetworkInterfaceDropDown.DropDownItems)
            {
                if (item.Text.Equals(_scanner.Interface.Description))
                {
                    NetworkInterfaceDropDown.Text = MaxTextLength > -1 && _scanner.Interface.Description.Length > MaxTextLength ? _scanner.Interface.Description.Remove(MaxTextLength - 1) + "..." : _scanner.Interface.Description;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        /// <summary>
        /// Updates the control to reflect the current scan state
        /// </summary>
        internal void UpdateView()
        {
            UpdateScanButtonState(_scanner.NetworkScanner.IsScanning);
            NetworkInterfaceDropDown.Enabled = true;
        }

        private void WlanClient_InterfaceAddedEvent(object sender, InterfaceNotificationEventsArgs e)
        {
            UpdateInterfaceList();
            //If we are not scanning and a new interface is added, use it!
            if (!_scanner.NetworkScanner.IsScanning && _scanner.SetInterface(e.ItsGuid))
            {
                try
                {
                    //Reset cache data
                    _scanner.Cache.Clear();
                    Utilities.ResetColor();

                    //These will always need to be invoked
                    Invoke(new DelInvokeNoArg(UpdateSelection));
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
            if (e.ItsGuid == new Guid(_scanner.Interface.Id))
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

        #endregion Private Methods
    }
}