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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using inSSIDer.Scanning;
using inSSIDer.UI.Forms;

using MetaGeek.WiFi;

namespace inSSIDer.UI.Controls
{
    public partial class FilterManager : UserControl
    {
        #region Fields

        //selection ignore flag - set to makes it ignore the first the auto-select when a new rows is added
        private bool _ignoreSelection = true;
        private ScanController _sc;
        private int _selectedRow = -1;

        #endregion Fields

        #region Constructors

        public FilterManager()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Rleases all hooked external events
        /// </summary>
        public void ReleaseEvents()
        {
            _sc.Cache.FiltersChanged -= Cache_FiltersChanged;
        }

        public void SetScanner(ref ScanController s)
        {
            _sc = s;
            _sc.Cache.FiltersChanged += Cache_FiltersChanged;
            UpdateFilters();
        }

        #endregion Public Methods

        #region Private Methods

        private void AddAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in lastFiltersUsedToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>())
            {
                if(item.Name.EndsWith("LastFilter"))
                {
                    Filter fNew = new Filter(item.Text);
                    _sc.Cache.AddFilter(fNew);
                }
            }
        }

        private void Cache_FiltersChanged(object sender, EventArgs e)
        {
            UpdateFilters();
        }

        private void channel10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 10"));
        }

        private void channel11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 11"));
        }

        private void channel12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 12"));
        }

        private void channel13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 13"));
        }

        private void channel14ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 14"));
        }

        private void channel1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 1"));
        }

        private void channel2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 2"));
        }

        private void channel3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 3"));
        }

        private void channel4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 4"));
        }

        private void channel5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 5"));
        }

        private void channel6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 6"));
        }

        private void channel7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 7"));
        }

        private void channel8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 8"));
        }

        private void channel9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel == 9"));
        }

        private void CreateNewToolStripMenuItemClick(object sender, EventArgs e)
        {
            using(FormFilterBuilder form = new FormFilterBuilder())
            {
                if(form.ShowDialog(ParentForm) == DialogResult.OK && !string.IsNullOrEmpty(form.Expr))
                {
                    _sc.Cache.AddFilter(new Filter(form.Expr));
                }
            }
        }

        private void DeleteSelectedToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(dgvFilters.SelectedRows.Count < 1) return;

            _sc.Cache.RemoveFilterById((Guid)dgvFilters.SelectedRows[0].Cells["idColumn"].Value);
        }

        private void DgvFiltersCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Clicked on row selector
            if (e.RowIndex == -1)
            {
                _ignoreSelection = true;
                return;
            }

            switch (e.ColumnIndex)
            {
                case 1:
                    if (e.Button == MouseButtons.Left)
                    {
                        //if((int) scannerView.Rows[e.RowIndex].Cells["idColumn"].Value > 20)
                        //    Console.WriteLine("Hi");
                        //AccessPoint ap =
                        //    _sc.Cache.GetApByIndex((long)scannerView.Rows[e.RowIndex].Cells["idColumn"].Value);
                        //if (ap == null) break;
                        //ap.Graph = !ap.Graph;

                        Filter f = _sc.Cache.GetFilterById((Guid)dgvFilters.Rows[e.RowIndex].Cells["idColumn"].Value);
                        f.Enabled = !f.Enabled;
                        dgvFilters.Rows[e.RowIndex].Cells["checkColumn"].Value = f.Enabled;
                    }
                    break;
            }

            if (dgvFilters.Rows[e.RowIndex].Selected && _selectedRow != e.RowIndex)
            {
                _selectedRow = e.RowIndex;
            }
            else if (dgvFilters.Rows[e.RowIndex].Selected && _selectedRow == e.RowIndex)
            {
                //If the selected row is the current row and is selected, deselect it.
                dgvFilters.Rows[e.RowIndex].Selected = false;
                _selectedRow = -1;
            }
        }

        private void DgvFiltersCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentForeground);
            e.Handled = true;
        }

        private void DgvFiltersSelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreSelection)
            {
                //scannerView.SelectedRows.Clear();
                foreach (DataGridViewRow row in dgvFilters.Rows)
                {
                    row.Selected = false;
                }
                _ignoreSelection = false;
            }
            // Edit/delete buttons
            SetButtonsState(dgvFilters.SelectedRows.Count > 0);
        }

        private void EditSelectedToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (dgvFilters.SelectedRows.Count < 1) return;
            Filter f = _sc.Cache.GetFilterById((Guid)dgvFilters.SelectedRows[0].Cells["idColumn"].Value);
            if (f == null) return;

            using (FormFilterBuilder form = new FormFilterBuilder(f.ToString()))
            {
                if (form.ShowDialog(ParentForm) == DialogResult.OK && !string.IsNullOrEmpty(form.Expr))
                {
                    f.SetExpression(form.Expr);
                }
            }
            UpdateFilters();
        }

        private void FilterMgr_Load(object sender, EventArgs e)
        {
            //Load last filters ro menu
            Filter[] lastfilters = SettingsMgr.GetFilterList();
            if(lastfilters.Length > 0)
            {
                lastFiltersUsedToolStripMenuItem.Visible = true;
                lastFiltersUsedToolStripMenuItem.DropDownItems.Clear();
                ToolStripMenuItem newItem;
                foreach (Filter f in lastfilters)
                {
                    newItem = new ToolStripMenuItem(f.ToString().Replace("&&", "&&&&")) {Name = f.Id + "_LastFilter"};
                    lastFiltersUsedToolStripMenuItem.DropDownItems.Add(newItem);
                }
                //Add the last 2 buttons back
                lastFiltersUsedToolStripMenuItem.DropDownItems.Add(toolStripSeparator1);
                lastFiltersUsedToolStripMenuItem.DropDownItems.Add(addAllToolStripMenuItem);

            }
        }

        private DataGridViewRow FindRow(Guid id)
        {
            foreach (DataGridViewRow row2 in dgvFilters.Rows)
            {
                if ((Guid) row2.Cells["idColumn"].Value == id)
                {
                    return row2;
                }
            }
            return null;
        }

        private void FiveGHzBandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel >= 36"));
        }

        private void fiveSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 5"));
        }

        private void Is80211NToolStripMenuItemClick(object sender, EventArgs e)
        {
            Filter fNew = new Filter("IsTypeN == True");
            _sc.Cache.AddFilter(fNew);
        }

        private void LastFiltersUsedToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Console.WriteLine(e.ClickedItem.Name);
            if(e.ClickedItem.Name.EndsWith("LastFilter"))
            {
                Filter fNew = new Filter(e.ClickedItem.Text);
                _sc.Cache.AddFilter(fNew);
            }
        }

        private void oneMinuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 60"));
        }

        private void oneSecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 1"));
        }

        private void OnlyOpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            Filter fNew = new Filter("Security == None");
            _sc.Cache.AddFilter(fNew);
        }

        private void OnlySecuredToolStripMenuItemClick(object sender, EventArgs e)
        {
            Filter fNew = new Filter("Security > None");
            _sc.Cache.AddFilter(fNew);
        }

        private void RSsiToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == null) return;
            string rssi = e.ClickedItem.Text.Split(' ')[1];
            Filter fNew = new Filter("Rssi > " + rssi);
            _sc.Cache.AddFilter(fNew);
        }

        private void SetButtonsState(bool enabled)
        {
            editSelectedToolStripMenuItem.Enabled = enabled;
            deleteSelectedToolStripMenuItem.Enabled = enabled;
        }

        private void tenSecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 10"));
        }

        private void thirtySecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 30"));
        }

        private void twentySecondsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 20"));
        }

        private void TwoFourGHzBandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Channel <= 14"));
        }

        private void twoMinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sc.Cache.AddFilter(new Filter("Age <= 120"));
        }

        private void UpdateFilters()
        {
            List<DataGridViewRow> allrows = new List<DataGridViewRow>();
            allrows.AddRange(dgvFilters.Rows.Cast<DataGridViewRow>());
            foreach (Filter filter in _sc.Cache.Filters)
            {
                DataGridViewRow row = FindRow(filter.Id);
                if (row == null)
                {
                    //New Filter!
                    DataGridViewRow newrow = new DataGridViewRow();
                    newrow.CreateCells(dgvFilters, filter.GetData());

                    dgvFilters.Rows.Add(newrow);
                }
                else
                {
                    allrows.Remove(row);
                    //update old filters
                    row.Cells["exprColumn"].Value = filter.ToString();
                    row.Cells["checkColumn"].Value = filter.Enabled;
                }
            }
            for (int i = 0; i < dgvFilters.Rows.Count; i++)
            {
                if (allrows.Contains(dgvFilters.Rows[i])) { dgvFilters.Rows.RemoveAt(i); continue; }
            }
        }

        private void Uses40MHzChannelToolStripMenuItemClick(object sender, EventArgs e)
        {
            Filter fNew = new Filter("Is40MHz == True");
            _sc.Cache.AddFilter(fNew);
        }

        #endregion Private Methods
    }
}