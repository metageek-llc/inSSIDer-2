////////////////////////////////////////////////////////////////
//
// Copyright (c) 2009-2010 MetaGeek, LLC
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

using System.Windows.Forms;
using System.Drawing;
using inSSIDer.Misc;
using MetaGeek.WiFi;

namespace inSSIDer.UI.Controls
{
    /// <summary>
    /// This class is used for displaying spark lines of the RSSI values in a DataGridView.
    /// The content of the cell is a spark line (line graph) providing a visual 
    /// representation of the signal strength over time.
    /// </summary>
    public class SparkLineCell : DataGridViewTextBoxCell
    {
        #region Constants

        private const int LeftPadding = 4;
        private const int RightPadding = 28;
        #endregion Constants      

        #region Methods

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {

            // paint the content cell
            if (rowIndex >= 0)
            {

                int[] sparks;
                if (value is int[])
                {
                    sparks = (int[])value;

                    if (sparks.Length > 0)
                    {
                        // let the base class draw the numeric contents
                        cellStyle.ForeColor = SignalColor.GetColorThreshold(sparks[sparks.Length - 1]);
                        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, sparks[sparks.Length - 1],
                                   sparks[sparks.Length - 1].ToString(), errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.All);
                    }

                    using (Pen pen = new Pen(Color.Red))
                    {
                        float x0 = cellBounds.X + cellBounds.Width - RightPadding;
                        float xStepSize = ((cellBounds.Width - LeftPadding - RightPadding) / (float)AccessPoint.MaxDataPoints);

                        for (int i = sparks.Length - 1; i >= 0; i--)
                        {
                            float x = cellBounds.X + cellBounds.Width - RightPadding - (sparks.Length - i) * xStepSize;

                            // calculating Y value of each point... use range of -100 to -25 dBm
                            float y = cellBounds.Y + (-25f - sparks[i]) * ((float)cellBounds.Height / 75);

                            if (y < cellBounds.Y)
                            {
                                y = cellBounds.Y;
                            }
                            if (y > cellBounds.Y + cellBounds.Height - 1)
                            {
                                y = cellBounds.Y + cellBounds.Height - 1;
                            }

                            pen.Color = SignalColor.GetColorThreshold(sparks[i]);
                            graphics.DrawLine(pen, x0, y, x, y);

                            if (i == sparks.Length - 1)
                            {
                                // draw a dot at the last point to signify it is current..
                                pen.Color = Color.White;
                                graphics.DrawLine(pen, x0, y, x, y);
                            }

                            x0 = x;
                        }
                    }
                }
                else
                {
                    // let the base class draw the numeric contents
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null,
                        null, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.All);

                }
            }
            // paint the header row.
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value,
                    formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            }
        }

        #endregion 
    }
}
