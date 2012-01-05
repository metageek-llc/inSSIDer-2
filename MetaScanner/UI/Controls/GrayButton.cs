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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace inSSIDer.UI.Controls
{
    public class GrayButton : Button
    {
        #region Fields

        private bool _isOver;

        #endregion Fields

        #region Protected Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            _isOver = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isOver = false;
            base.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            //base.OnPaint(pevent);
            Rectangle rect = pevent.ClipRectangle;

            Brush gradientBrush = new LinearGradientBrush(rect, Color.FromArgb(175, 175, 175),
                                                          Color.FromArgb(130, 130, 130), LinearGradientMode.Vertical);

            pevent.Graphics.FillRectangle(gradientBrush, pevent.ClipRectangle);

            //Draw rounded corners
            //Top-left
            pevent.Graphics.FillRectangle(Brushes.Black,0,0,5,5);
            pevent.Graphics.FillEllipse(gradientBrush,0,0,10,10);

            //Bottom-left
            pevent.Graphics.FillRectangle(Brushes.Black, 0, rect.Height-5, 5, 5);
            pevent.Graphics.FillEllipse(gradientBrush, 0, rect.Height - 11, 10, 10);

            //Top-right
            pevent.Graphics.FillRectangle(Brushes.Black, rect.Width -5, 0, 5, 5);
            pevent.Graphics.FillEllipse(gradientBrush, rect.Width - 11, 0, 10, 10);

            //Bottom-right
            pevent.Graphics.FillRectangle(Brushes.Black, rect.Width - 5, rect.Height - 5, 5, 5);
            pevent.Graphics.FillEllipse(gradientBrush, rect.Width - 11, rect.Height - 11, 10, 10);

            rect.Width -= 4;
            rect.Height -= 4;

            rect.Offset(2,2);

            pevent.Graphics.DrawString(Text, Font, new SolidBrush(_isOver && Enabled ? Color.White : ForeColor), rect,
                                       new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center});
        }

        #endregion Protected Methods
    }
}