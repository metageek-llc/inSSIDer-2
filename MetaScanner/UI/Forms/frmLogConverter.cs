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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using inSSIDer.FileIO;
using inSSIDer.Localization;
using inSSIDer.Properties;
using System.IO;

namespace inSSIDer.UI.Forms
{
    public partial class FormLogConverter : Form
    {
        private string[] _inFiles;
        private string _outPath;
        public FormLogConverter()
        {
            InitializeComponent();
        }

        private void FormLogConverterShown(object sender, EventArgs e)
        {
            
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            if(txtInFiles.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Localizer.GetString("ErrorNoLogFileSelected"), Localizer.GetString("Error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtOutDir.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Localizer.GetString("ErrorKmlDirectoryMissing"), Localizer.GetString("Error"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if(!txtOutDir.Text.EndsWith("\\")) txtOutDir.AppendText("\\");
            _outPath = txtOutDir.Text;

            //Append the log filename to the directory path
            int lastS = _inFiles[0].LastIndexOf('\\');
            int lastP = _inFiles[0].Substring(lastS).LastIndexOf('.');
            _outPath += _inFiles[0].Substring(lastS).Remove(lastP) + "\\";

            //Create the output directory
            System.IO.Directory.CreateDirectory(_outPath);

            
            //All is well, let's go
            Waypoint[] allPoints;
            try
            {
                //Load all files. An array is required because all KML classes accept arrays
                allPoints = GpxIO.ReadGpxFiles(_inFiles).ToArray();
            }
            catch(IOException)
            {
                //This happens when the file is locked by another process or inSSIDer is still logging to it.
                MessageBox.Show(
                    Localizer.GetString("GpxFileOpenError"),
                    Localizer.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Filter data
            WaypointFilterArgs farg = new WaypointFilterArgs
                                          {
                                              GpsFixLost = chGPSFixLost.Checked,
                                              GpsLockedUp = chGPSLockup.Checked,
                                              MaximumSpeedKmh = chMaxSpeed.Checked ? (int) numMaxSpeed.Value : -1,
                                              MaxSignal = chMaxSignal.Checked ? (int) numMaxSignal.Value : -1,
                                              MinimumSatsVisible = chGPSsatCount.Checked ? (int) numSatCount.Value : -1
                                          };
            allPoints = KmlWriter.FilterData(allPoints, farg);

            ApOrganization group = (ApOrganization)cmbOrganize.SelectedIndex;


            if(chExportSummary.Checked) //Export a summary file
            {
                if (!System.IO.File.Exists(_outPath + "Summary.kml") ||
                    MessageBox.Show(Localizer.GetString("WarningSummaryKmlExists"),
                                    Localizer.GetString("FileExists"), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    KmlWriter.WriteSummaryKml(allPoints, _outPath + "Summary.kml", group, chShowRssiMarkers.Checked);
                }
            }

            if(chExportComp.Checked) //Comprehensive
            {
                if (!System.IO.File.Exists(_outPath + "Comprehensive.kml") ||
                    MessageBox.Show(Localizer.GetString("WarningComprehensiveKmlExists"),
                                    Localizer.GetString("FileExists"), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    KmlWriter.WriteComprehensiveKml(allPoints, _outPath + "Comprehensive.kml", group, chShowRssiMarkers.Checked);
                }
            }

            if(chExportEachAp.Checked) //Export each AP to ./APs/{SSID}.kml
            {
                KmlWriter.WriteAccessPointKml(allPoints,_outPath,chShowRssiMarkers.Checked);
            }

            MessageBox.Show(Localizer.GetString("ExportComplete"), Localizer.GetString("Finished"), MessageBoxButtons.OK);
        }

        private void FormLogConverterLoad(object sender, EventArgs e)
        {

            //ask for input files if the settings are not saved
            if (Settings.Default.gpxLastInputFiles == null || Settings.Default.gpxLastInputFiles.Count == 0)
            {
                if (openFile.ShowDialog(this) != DialogResult.OK)
                {
                    Close();
                    return;
                }
                _inFiles = openFile.FileNames;
            }
            else
            {
                _inFiles = Settings.Default.gpxLastInputFiles.Cast<string>().ToArray();
            }
            txtInFiles.Text = _inFiles.Aggregate((all, next) => all + " " + next);
            
            //Output path
            if (string.IsNullOrEmpty(Settings.Default.gpxLastOutputDir))
            {
                fbOutput.SelectedPath = Application.StartupPath;
                if (fbOutput.ShowDialog(this) != DialogResult.OK) return;
                _outPath = fbOutput.SelectedPath;
                txtOutDir.Text = _outPath;
            }
            else
            {
                txtOutDir.Text = _outPath = Settings.Default.gpxLastOutputDir;
            }

            //Settings load
            chExportSummary.Checked = Settings.Default.gpxLastSummary;
            chExportComp.Checked = Settings.Default.gpxLastComprehensive;
            chExportEachAp.Checked = Settings.Default.gpxLastEachAp;

            cmbOrganize.SelectedIndex = Settings.Default.gpxLastOrganizeEThenC ? 0 : 1;

            chShowRssiMarkers.Checked = Settings.Default.gpxLastRssiLabels;

            chGPSLockup.Checked = Settings.Default.gpxLastGpsLockedup;
            chGPSFixLost.Checked = Settings.Default.gpxLastGpsFixLost;
            chGPSsatCount.Checked = Settings.Default.gpxLastMinimumSatsEnabled;
            numSatCount.Value = Settings.Default.gpxLastMinimumStas;

            chMaxSpeed.Checked = Settings.Default.gpxLastMaxSpeedEnabled;
            numMaxSpeed.Value = Settings.Default.gpxLastMaxSpeed;

            chMaxSignal.Checked = Settings.Default.gpxLastMaxRssiEnabled;
            numMaxSignal.Value = Settings.Default.gpxLastMaxRssi;


            //Set default organization
            cmbOrganize.SelectedIndex = 0;
        }

        private void ChangeInputFilesButtonClick(object sender, EventArgs e)
        {
            if (openFile.ShowDialog(this) != DialogResult.OK) return;
            _inFiles = openFile.FileNames;
            txtInFiles.Text = _inFiles.Aggregate((all, next) => all + " " + next);
        }

        private void ChangeOutputDirectoryButtonClick(object sender, EventArgs e)
        {
            if (fbOutput.ShowDialog(this) != DialogResult.OK) return;
            _outPath = fbOutput.SelectedPath;
            txtOutDir.Text = _outPath;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            //Settings Save
            Settings.Default.gpxLastSummary = chExportSummary.Checked;
            Settings.Default.gpxLastComprehensive = chExportComp.Checked;
            Settings.Default.gpxLastEachAp = chExportEachAp.Checked;

            Settings.Default.gpxLastOrganizeEThenC = cmbOrganize.SelectedIndex == 0;

            Settings.Default.gpxLastRssiLabels = chShowRssiMarkers.Checked;

            Settings.Default.gpxLastGpsLockedup = chGPSLockup.Checked;
            Settings.Default.gpxLastGpsFixLost = chGPSFixLost.Checked;
            Settings.Default.gpxLastMinimumSatsEnabled = chGPSsatCount.Checked;
            Settings.Default.gpxLastMinimumStas = (int)numSatCount.Value;

            Settings.Default.gpxLastMaxSpeedEnabled = chMaxSpeed.Checked;
            Settings.Default.gpxLastMaxSpeed = (int)numMaxSpeed.Value;

            Settings.Default.gpxLastMaxRssiEnabled = chMaxSignal.Checked;
            Settings.Default.gpxLastMaxRssi = (int)numMaxSignal.Value;

            //Save input file(s)
            //(Settings.Default.gpxLastInputFiles ?? (Settings.Default.gpxLastInputFiles = new StringCollection())).AddRange(openFile.FileNames);
            if (_inFiles != null)
            {
                (Settings.Default.gpxLastInputFiles = new StringCollection()).AddRange(_inFiles);
            }

            Settings.Default.gpxLastOutputDir = txtOutDir.Text;

        }
    }
}
