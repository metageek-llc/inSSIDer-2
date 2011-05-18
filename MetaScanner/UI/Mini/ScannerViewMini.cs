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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using inSSIDer.Scanning;
using MetaGeek.WiFi;
using inSSIDer.Localization;

namespace inSSIDer.UI.Mini
{
    public partial class ScannerViewMini : UserControl
    {
        private ScanController _sc;
        //selection ignore flag - set to makes it ignore the first the auto-select when a new rows is added
        private bool _ignoreSelection = true;

        private int _selectedRow = -1;

        // Checkbox for the "select all networks"
        private CheckBox _selectAllNetworksCheckBox;

        public event EventHandler RequireRefresh;

        delegate void DelInvokeEvent(object sender, EventArgs e);

        public ScannerViewMini()
        {
            InitializeComponent();
        }

        public void SetScanner(ref ScanController scanner)
        {
            _sc = scanner;
            _sc.Cache.DataReset += Cache_DataReset;
        }

        private void Cache_DataReset(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new DelInvokeEvent(Cache_DataReset), new object[] { sender, e });
                }
                catch (InvalidOperationException) { }
                return;
            }

            scannerGrid.Rows.Clear();
            _ignoreSelection = true;
        }

        private void ScannerViewCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Header!
            if (e.RowIndex != -1) return;
            // fill gradient background 
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                e.CellBounds,
                Color.FromArgb(202, 202, 202),
                Color.FromArgb(158, 158, 158),
                LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(gradientBrush, e.CellBounds);
            gradientBrush.Dispose();

            // paint rest of cell 
            //e.Paint(e.CellBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentForeground);
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ContentBackground);
            e.Handled = true;
            //Update the checkbox position
            ScannerViewUpdateHeaderCheckBoxPos();
        }

        private void ScannerViewSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //Sort numeric columns like numbers
            if (e.Column == scannerGrid.Columns["rssiColumn"] || e.Column == scannerGrid.Columns["maxrateColumn"] || e.Column == scannerGrid.Columns["channelColumn"])
            {
                //Remove anything after a space
                string value1 = e.CellValue1.ToString();
                if(value1.IndexOf(' ') > 0)
                    value1 = value1.Remove(value1.IndexOf(' '));

                string value2 = e.CellValue2.ToString();
                if (value2.IndexOf(' ') > 0)
                    value2 = value2.Remove(value2.IndexOf(' '));

                if (Convert.ToInt32(value1) > Convert.ToInt32(value2)) { e.SortResult = 1; }
                else if (Convert.ToInt32(value1) < Convert.ToInt32(value2)) { e.SortResult = -1; }
                else { e.SortResult = 0; }
                e.Handled = true;
            }

            //else if (e.Column == scannerGrid.Columns["channelColumn"])
            //{
            //    string c1 = e.CellValue1.ToString(), c2 = e.CellValue2.ToString();
            //    //Channel may have a + in it
            //    if (c1.Contains("+"))
            //    {
            //        c1 = c1.Remove(c1.LastIndexOf(" + "));
            //    }

            //    if (c2.Contains("+"))
            //    {
            //        c2 = c2.Remove(c2.LastIndexOf(" + "));
            //    }
            //    if (Convert.ToInt32(c1) > Convert.ToInt32(c2)) { e.SortResult = 1; }
            //    else if (Convert.ToInt32(c1) < Convert.ToInt32(c2)) { e.SortResult = -1; }
            //    else { e.SortResult = 0; }
            //    e.Handled = true;

            //}
            //Location sorting, they are doubles, not ints
            else if (e.Column == scannerGrid.Columns["latColumn"] || e.Column == scannerGrid.Columns["lonColumn"])
            {
                if (Convert.ToDouble(e.CellValue1) > Convert.ToDouble(e.CellValue2)) { e.SortResult = 1; }
                else if (Convert.ToDouble(e.CellValue1) < Convert.ToDouble(e.CellValue2)) { e.SortResult = -1; }
                else { e.SortResult = 0; }
                e.Handled = true;
            }
        }

        private delegate void DelGo();
        public void Go()
        {
            //System.Diagnostics.Debug.WriteLineIf(Parent == null, "Orphaned control!");
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new DelGo(Go));
                }
                catch (InvalidOperationException) { }
                catch (NullReferenceException) { }
            }
            else
            {
                try
                {
                    lock (scannerGrid)
                    {
                        if (_sc == null) return;

                        //Clear non-existent rows
                        CleanRows();

                        foreach (AccessPoint ap in _sc.Cache.GetAccessPoints())
                        {
                            DataGridViewRow row = FindRow(ap);
                            if (row == null)
                            {
                                //Add a new Row
                                DataGridViewRow newrow = new DataGridViewRow();
                                newrow.CreateCells(scannerGrid, ap.GetData());

                                scannerGrid.Rows.Add(newrow);

                                // Check for blank SSID
                                if (string.IsNullOrEmpty(newrow.Cells["ssidColumn"].Value.ToString()))
                                {
                                    newrow.Cells["ssidColumn"].Value = Localizer.GetString("UnknownSSID");
                                }

                                //Replace sparkline data with RSSI
                                newrow.Cells["rssiColumn"].Value = ap.LastData.Rssi;

                                newrow.Cells["checkColumn"].Style.BackColor = ap.MyColor;
                                newrow.Cells["checkColumn"].Style.SelectionBackColor = ap.MyColor;
                                newrow.Cells["maxrateColumn"].ToolTipText = ap.SupportedRates;

                                //Age column
                                newrow.Cells["ageColumn"].Value = ap.Age + " sec";

                                //Check for indeterminate state of checkbox
                                if(_selectAllNetworksCheckBox.CheckState == CheckState.Indeterminate)
                                {
                                    //If at least one AP is unchecked, don't check this one
                                    newrow.Cells["checkColumn"].Value = false;
                                    ap.Graph = false;
                                }
                            }
                            else
                            {
                                //Update vendor if null. This has happend before
                                if (row.Cells["vendorColumn"].Value == null)
                                    row.Cells["vendorColumn"].Value = ap.Vendor;


                                //It is possible that the SSID of the AP has changed
                                row.Cells["ssidColumn"].Value = string.IsNullOrEmpty(ap.Ssid) ? Localizer.GetString("UnknownSSID") : ap.Ssid;

                                row.Cells["maxrateColumn"].Value = ap.MaxRate + (ap.IsN ? " (N)" : "");

                                //Update the channel
                                row.Cells["channelColumn"].Value = ap.IsN && ap.NSettings != null &&
                                                                   ap.NSettings.Is40MHz
                                                                       ? ap.NSettings.SecondaryChannelLower
                                                                             ? ap.Channel + " + " + (ap.Channel - 4)
                                                                             : ap.Channel + " + " + (ap.Channel + 4)
                                                                       : ap.Channel.ToString();

                                //Update the RSSI
                                row.Cells["rssiColumn"].Value = ap.Age > 10 ? -100 : ap.LastData.Rssi;

                                //Update the timestamp
                                row.Cells["ageColumn"].Value = ap.Age + " sec";

                                //Update Location
                                row.Cells["latColumn"].Value = ap.GpsData.Latitude.ToString("F5");
                                row.Cells["lonColumn"].Value = ap.GpsData.Longitude.ToString("F5");
                            }
                        }
                    }
                }
                catch(Exception)
                {
                    
                }
            }
        }

        private DataGridViewRow FindRow(AccessPoint ap)
        {
            foreach (DataGridViewRow row in scannerGrid.Rows)
            {
                if (row.Cells["macColumn"].Value.ToString() == ap.MacAddress.ToString())
                {
                    return row;
                }
            }
            return null;
        }

        private void CleanRows()
        {
            //If there are more rows than APs, we need to remove the bogas one(s)
            if (scannerGrid.RowCount <= _sc.Cache.Count) return;

            List<string> macs = new List<string>();
            _sc.Cache.GetAccessPoints().ToList().ForEach(ap => macs.Add(ap.MacAddress.ToString().ToUpper()));
            //macs.AddRange();

            foreach (AccessPoint ap in _sc.Cache.GetAccessPoints())
                macs.Add(ap.MacAddress.ToString().ToUpper());

            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in scannerGrid.Rows)
                if(!macs.Contains(row.Cells["macColumn"].Value.ToString().ToUpper()))
                    rowsToRemove.Add(row);

            //If the first row gets removed, the next will be selected
            _ignoreSelection = true;
            rowsToRemove.ForEach(r => scannerGrid.Rows.Remove(r));
            macs.Clear();
            rowsToRemove.Clear();
        }

        private void ScannerViewSelectionChanged(object sender, EventArgs e)
        {
            //if(scannerView.SelectedRows.Count > 0) Console.WriteLine(scannerView.SelectedRows[0]);
            if(_ignoreSelection)
            {
                //scannerView.SelectedRows.Clear();
                foreach (DataGridViewRow row in scannerGrid.Rows)
                {
                    row.Selected = false;
                }
                _ignoreSelection = false;
            }

            foreach (DataGridViewRow row in scannerGrid.Rows)
            {
            //    if(row.Selected)
            //    {
            //        _sc.Cache.GetAccessPointByMacAddress(row.Cells["macColumn"].Value.ToString()).Highlight = true;
            //    }
                AccessPoint apTemp = _sc.Cache.GetAccessPointById((long) row.Cells["idColumn"].Value);
                if(apTemp == null) continue;

                apTemp.Highlight = row.Selected;
            }
            OnRequireRefresh();
            //if (scannerView.SelectedRows.Count > 0) _selectedRow = scannerView.SelectedRows[0].Index;
        }

        private void ScannerViewCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //return;
            //Console.WriteLine(e.ColumnIndex);
            // Clicked on row selector 
            if (e.RowIndex == -1)
            {
                _ignoreSelection = true;
                return;
            }
            //Console.WriteLine(scannerView.Rows[e.RowIndex].Cells["idColumn"].Value);
            //if(e.ColumnIndex == 1)
            //{
            //    _ignoreSelection = true;
            //    return;
            //}

            switch(e.ColumnIndex)
            {
                case 1:
                    if(e.Button == MouseButtons.Left)
                    {
                        //if((int) scannerView.Rows[e.RowIndex].Cells["idColumn"].Value > 20)
                        //    Console.WriteLine("Hi");
                        AccessPoint ap =
                            _sc.Cache.GetAccessPointByMacAddress(scannerGrid.Rows[e.RowIndex].Cells["macColumn"].Value.ToString());
                        if(ap == null) break;
                        ap.Graph = !ap.Graph;

                        scannerGrid.Rows[e.RowIndex].Cells["checkColumn"].Value = ap.Graph;
                    }
                    OnRequireRefresh();
                    break;
            }
            //Console.WriteLine(scannerView.Rows[e.RowIndex].Cells["checkColumn"].Value);

            //Console.WriteLine(scannerView.Rows[e.RowIndex].Selected);
            if(scannerGrid.Rows[e.RowIndex].Selected && _selectedRow != e.RowIndex)
            {
                _selectedRow = e.RowIndex;
            }
            else if (scannerGrid.Rows[e.RowIndex].Selected && _selectedRow == e.RowIndex)
            {
                //If the selected row is the current row and is selected, deselect it.
                scannerGrid.Rows[e.RowIndex].Selected = false;
                _selectedRow = -1;
            }
            //if(scannerView.SelectedRows.Count >=1)
            //{
            //    if(scannerView.SelectedRows[0].Index == e.RowIndex)
            //    {
            //        scannerView.Rows[e.RowIndex].Selected = false;
            //    }
            //}

            int visCount = _sc.Cache.GetAccessPoints().Count(ap => ap.Graph);
            int inVisCount = _sc.Cache.GetAccessPoints().Count(ap => !ap.Graph);

            _selectAllNetworksCheckBox.CheckState = visCount == 0
                                                       ? CheckState.Unchecked
                                                       : (inVisCount == 0
                                                              ? CheckState.Checked
                                                              : CheckState.Indeterminate);
        }

        private void ScannerViewVisibleChanged(object sender, EventArgs e)
        {
            if (!scannerGrid.Visible) return;
            if(_selectAllNetworksCheckBox == null)
            {
                _selectAllNetworksCheckBox = new CheckBox
                                                {
                                                    ThreeState = true,
                                                    CheckState = CheckState.Checked
                                                };
                _selectAllNetworksCheckBox.CheckedChanged += SelectAllNetworksCheckBoxCheckedChanged;

                //Add the CheckBox into the DataGridView
                scannerGrid.Controls.Add(_selectAllNetworksCheckBox);
            }

            // TUT: this little hack causes the column indices to get updated on XP
            if (scannerGrid != null)
            {
                scannerGrid.Columns["idColumn"].Visible = true;
                scannerGrid.Columns["idColumn"].Visible = false;
            }

            ScannerViewUpdateHeaderCheckBoxPos();
        }

        private void SelectAllNetworksCheckBoxCheckedChanged(object source, EventArgs e)
        {
            switch (_selectAllNetworksCheckBox.CheckState)
            {
                case CheckState.Unchecked:
                    foreach (AccessPoint ap in _sc.Cache.GetAccessPoints())
                    {
                        ap.Graph = false;
                    }
                    //Update data grid
                    foreach (DataGridViewRow row in scannerGrid.Rows)
                    {
                        row.Cells["checkColumn"].Value = false;
                    }
                    OnRequireRefresh();
                    break;
                case CheckState.Checked:
                    foreach (AccessPoint ap in _sc.Cache.GetAccessPoints())
                    {
                        ap.Graph = true;
                    }
                    //Update data grid
                    foreach (DataGridViewRow row in scannerGrid.Rows)
                    {
                        row.Cells["checkColumn"].Value = true;
                    } 
                    OnRequireRefresh();
                    break;
            }
        }

        private void ScannerViewUpdateHeaderCheckBoxPos()
        {
            if (scannerGrid == null || _selectAllNetworksCheckBox == null || scannerGrid.Columns == null) return;

            Rectangle rect = scannerGrid.GetColumnDisplayRectangle(scannerGrid.Columns["checkColumn"].Index, true);
            _selectAllNetworksCheckBox.Size = new Size(13, 13);
            rect.X += 3;
            rect.Y += 5;

            //Change the location of the CheckBox to make it stay on the header
            _selectAllNetworksCheckBox.Location = rect.Location;
            _selectAllNetworksCheckBox.Invalidate();
        }

        private void ScannerViewColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ScannerViewUpdateHeaderCheckBoxPos();
        }

        private void OnRequireRefresh()
        {
            if(RequireRefresh != null)
            {
                RequireRefresh(this, EventArgs.Empty);
            }
        }

        private void ScannerGridColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                UpdateColumnList();
                cmsColumns.Show(Cursor.Position);
            }
        }

        private void MacAddressToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(sender as ToolStripMenuItem == null || scannerGrid == null) return;

            switch ((sender as ToolStripMenuItem).Name)
            {
                case "mACAddressToolStripMenuItem":
                    scannerGrid.Columns["macColumn"].Visible = mACAddressToolStripMenuItem.Checked;
                    break;
                case "sSIDToolStripMenuItem":
                    scannerGrid.Columns["ssidColumn"].Visible = sSIDToolStripMenuItem.Checked;
                    break;
                case "rSSIToolStripMenuItem":
                    scannerGrid.Columns["rssiColumn"].Visible = rSSIToolStripMenuItem.Checked;
                    break;
                case "channelToolStripMenuItem":
                    scannerGrid.Columns["channelColumn"].Visible = channelToolStripMenuItem.Checked;
                    break;
                case "vendorToolStripMenuItem":
                    scannerGrid.Columns["vendorColumn"].Visible = vendorToolStripMenuItem.Checked;
                    break;
                case "privacyToolStripMenuItem":
                    scannerGrid.Columns["privacyColumn"].Visible = privacyToolStripMenuItem.Checked;
                    break;
                case "maxRateToolStripMenuItem":
                    scannerGrid.Columns["maxrateColumn"].Visible = maxRateToolStripMenuItem.Checked;
                    break;
                case "networkTypeToolStripMenuItem":
                    scannerGrid.Columns["networktypeColumn"].Visible = networkTypeToolStripMenuItem.Checked;
                    break;
                case "firstSeenToolStripMenuItem":
                    scannerGrid.Columns["firstseenColumn"].Visible = firstSeenToolStripMenuItem.Checked;
                    break;
                case "lastSeenToolStripMenuItem":
                    scannerGrid.Columns["ageColumn"].Visible = lastSeenToolStripMenuItem.Checked;
                    break;
                case "latitudeToolStripMenuItem":
                    scannerGrid.Columns["latColumn"].Visible = latitudeToolStripMenuItem.Checked;
                    break;
                case "longitudeToolStripMenuItem":
                    scannerGrid.Columns["lonColumn"].Visible = longitudeToolStripMenuItem.Checked;
                    break;
                default:
                    break;
            }
        }

        public void UpdateColumnList()
        {
            if (scannerGrid != null)
            {
                mACAddressToolStripMenuItem.Checked = scannerGrid.Columns["macColumn"].Visible;
                sSIDToolStripMenuItem.Checked = scannerGrid.Columns["ssidColumn"].Visible;
                rSSIToolStripMenuItem.Checked = scannerGrid.Columns["rssiColumn"].Visible;
                channelToolStripMenuItem.Checked = scannerGrid.Columns["channelColumn"].Visible;
                vendorToolStripMenuItem.Checked = scannerGrid.Columns["vendorColumn"].Visible;
                privacyToolStripMenuItem.Checked = scannerGrid.Columns["privacyColumn"].Visible;
                maxRateToolStripMenuItem.Checked = scannerGrid.Columns["maxrateColumn"].Visible;
                networkTypeToolStripMenuItem.Checked = scannerGrid.Columns["networktypeColumn"].Visible;
                firstSeenToolStripMenuItem.Checked = scannerGrid.Columns["firstseenColumn"].Visible;
                lastSeenToolStripMenuItem.Checked = scannerGrid.Columns["ageColumn"].Visible;
                latitudeToolStripMenuItem.Checked = scannerGrid.Columns["latColumn"].Visible;
                longitudeToolStripMenuItem.Checked = scannerGrid.Columns["lonColumn"].Visible;
                cmsColumns.Invalidate();
            }
        }
    }
}
