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
using System.Windows.Forms;
using System.IO.Ports;
using MetaGeek.Gps;

namespace inSSIDer.UI.Forms
{
    public partial class FormGpsCfg : Form
    {
        private readonly GpsController _gpsC;
        public FormGpsCfg(GpsController control)
        {
            InitializeComponent();
            _gpsC = control;
        }

        private void SetData()
        {
            cbPortname.Items.Clear();

            //Add all ports now
            cbPortname.Items.AddRange(SerialPort.GetPortNames());

            if(_gpsC == null) return;

            //The current port name
            cbPortname.Text = _gpsC.PortName;

            //Other serial port settings
            cbStopBits.SelectedIndex = (int)_gpsC.PortStopBits;
            cbDataBits.SelectedIndex = _gpsC.PortDataBits - 5;
            numBaudrate.Value = _gpsC.PortBaudrate;
            cbHandshake.SelectedIndex = (int)_gpsC.PortHandshake;
            cbParity.SelectedIndex = (int)_gpsC.PortParity;

        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            if (_gpsC == null) return;
            //Apply settings first!
            _gpsC.PortBaudrate = (int)numBaudrate.Value;
            _gpsC.PortDataBits = cbDataBits.SelectedIndex + 5;
            _gpsC.PortHandshake = (Handshake)cbHandshake.SelectedIndex;
            _gpsC.PortName = cbPortname.Text;
            _gpsC.PortParity = (Parity)cbParity.SelectedIndex;
            _gpsC.PortStopBits = (StopBits)cbStopBits.SelectedIndex;

            //Save the GPS controller settings
            SettingsMgr.SaveGpsSettings(_gpsC);
            
            Close();
        }

        private void FormGpsCfgLoad(object sender, EventArgs e)
        {
            SetData();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
