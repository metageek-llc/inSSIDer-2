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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace inSSIDer.UI.Controls
{
    public class GrayToolStripRenderer : ToolStripRenderer
    {
        #region Fields

        private readonly Color _darkHeaderColor = Color.FromArgb(126, 126, 126);
        private readonly Color _disabledTextColor = Color.FromArgb(75, 75, 75);

        //private readonly Color _lightHeaderColor = Color.FromArgb(204, 204, 204);
        //private readonly Color _darkHeaderColor = Color.FromArgb(154, 154, 154);
        private readonly Color _lightHeaderColor = Color.FromArgb(175, 175, 175);
        private readonly Color _separatorColor = Color.FromArgb(100, 100, 100);
        private readonly Color _triangleColor = Color.FromArgb(36, 36, 36);

        #endregion Fields

        #region Protected Methods

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);

            ToolStripButton button = e.Item as ToolStripButton;

            if (null != button)
            {
                if (button.Checked)
                {
                    SolidBrush brush = new SolidBrush(_darkHeaderColor);
                    Pen pen = new Pen(_separatorColor);

                    e.Graphics.FillRectangle(brush, 0, 0, button.Width - 1, button.Height - 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, button.Width - 1, button.Height - 1);
                }
                else if (button.Selected)
                {
                    SolidBrush brush = new SolidBrush(_lightHeaderColor);
                    Pen pen = new Pen(_separatorColor);

                    e.Graphics.FillRectangle(brush, 0, 0, button.Width - 1, button.Height - 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, button.Width - 1, button.Height - 1);
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? e.TextColor : _disabledTextColor;
            OnRenderItemTextNew(e);
        }

        //Modification of original OnRenderItemText to allow color to be set even if the control is disabled
        protected void OnRenderItemTextNew(ToolStripItemTextRenderEventArgs e)
        {
            Graphics dc = e.Graphics;
                Color textColor = e.TextColor;
                Font textFont = e.TextFont;
                string text = e.Text;
                Rectangle textRectangle = e.TextRectangle;
                TextFormatFlags textFormat = e.TextFormat;
                //textColor = item.Enabled ? textColor : SystemColors.GrayText;
                if (((e.TextDirection != ToolStripTextDirection.Horizontal) && (textRectangle.Width > 0)) && (textRectangle.Height > 0))
                {
                    Size size = /*LayoutUtils.FlipSize(*/textRectangle.Size/*)*/;
                    using (Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppPArgb))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(bitmap))
                        {
                            graphics2.TextRenderingHint = TextRenderingHint.AntiAlias;
                            TextRenderer.DrawText(graphics2, text, textFont, new Rectangle(Point.Empty, size), textColor, textFormat);
                            bitmap.RotateFlip((e.TextDirection == ToolStripTextDirection.Vertical90) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate270FlipNone);
                            dc.DrawImage(bitmap, textRectangle);
                        }
                        return;
                    }
                }
                TextRenderer.DrawText(dc, text, textFont, textRectangle, textColor, textFormat);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            if (e.Item.Selected)
            {
                using (SolidBrush brush = new SolidBrush(_separatorColor))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, e.Item.Width, e.Item.Height);
                }
                using (Pen pen = new Pen(_triangleColor))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, e.Item.Width - 1, e.Item.Height - 1);
                }
            }
        }

        //protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        //{
        //    base.OnRenderArrow(e);
        //}
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Pen pen = new Pen(_separatorColor, 1);

            if (e.Vertical)
            {
                int x = e.Item.Width / 2;

                // draw main separator line
                e.Graphics.DrawLine(pen, x, 6, x, e.Item.Height - 6);

                // draw the shadow line
                pen.Color = _lightHeaderColor;
                x += 1;
                e.Graphics.DrawLine(pen, x, 7, x, e.Item.Height - 5);
            }
            else
            {
                int y = e.Item.Height / 2;

                e.Graphics.DrawLine(pen, 4, y, e.Item.Width - 4, y);
            }
        }

        //protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        //{
        //    base.OnRenderDropDownButtonBackground(e);
        //    SolidBrush brush = new SolidBrush(Color.HotPink);
        //    e.Graphics.FillRectangle(brush, e.Item.Bounds);
        //}
        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);
            SolidBrush brush = new SolidBrush(_lightHeaderColor);

            if (e.Item.Selected)
            {
                Pen pen = new Pen(_separatorColor);

                e.Graphics.FillRectangle(brush, 1, 1, e.Item.Width-2, e.Item.Height-2);
                e.Graphics.DrawRectangle(pen, 1, 1, e.Item.Width-2, e.Item.Height-2);
                e.Graphics.DrawLine(pen, e.Item.Width - 12, 1, e.Item.Width - 12, e.Item.Height - 2);
            }

            Point[] points = new Point[3];
            int y = e.Item.Height / 2;
            int x = e.Item.Width - 3;
            points[0] = new Point(x - 5, y);
            points[1] = new Point(x, y);
            points[2] = new Point(x - 3, y+3);

            brush.Color = _triangleColor;
            e.Graphics.FillPolygon(brush, points);
        }

        //protected override void Initialize(ToolStrip toolStrip)
        //{
        //    base.Initialize(toolStrip);
        //}
        //protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        //{
        //    base.OnRenderToolStripBorder(e);
        //    //ControlPaint.DrawFocusRectangle(e.Graphics, e.AffectedBounds, Color.FromArgb(102, 102, 102), Color.FromArgb(36, 36, 36));
        //    Pen pen = new Pen(Color.FromArgb(150,150,150));
        //    e.Graphics.DrawRectangle(pen, e.AffectedBounds);
        //}
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            LinearGradientBrush backgroundBrush = new LinearGradientBrush(
                   e.ToolStrip.ClientRectangle,
                   _lightHeaderColor,
                   _darkHeaderColor,
                   90,
                   true);

            // Paint the GridStrip control's background.
            e.Graphics.FillRectangle(
                backgroundBrush,
                e.AffectedBounds);

            using (Pen pen = new Pen(_separatorColor))
            {
                e.Graphics.DrawLine(pen, 0, e.ToolStrip.Height-1, e.ToolStrip.Width, e.ToolStrip.Height-1);
                e.Graphics.DrawLine(pen, e.ToolStrip.Width - 1, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            }
        }

        #endregion Protected Methods
    }
}