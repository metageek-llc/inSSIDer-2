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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace inSSIDer.UI.Controls
{
    public class GrayGroupBox : GroupBox
    {
        #region Properties

        [Category("Appearance")]
        public Color BorderColor
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public GrayGroupBox()
        {
            BorderColor = Color.Gray;
        }

        #endregion Constructors

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            Size tSize = TextRenderer.MeasureText(Text, Font);

            Rectangle borderRect = e.ClipRectangle;
            borderRect.Y += tSize.Height / 2;
            borderRect.Height -= tSize.Height / 2;
            ControlPaint.DrawBorder(e.Graphics, borderRect, BorderColor, ButtonBorderStyle.Solid);

            Rectangle textRect = e.ClipRectangle;
            textRect.X += 6;
            textRect.Width  = tSize.Width;
            textRect.Height = tSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(BackColor), textRect);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), textRect);
        }

        #endregion Protected Methods
    }
}