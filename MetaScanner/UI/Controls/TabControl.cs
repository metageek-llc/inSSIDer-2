using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace inSSIDer.UI.Controls
{
    public partial class TabControl : UserControl
    {
        private int TabMargin = 24;
        private List<string> Tabs = new List<string>();

        public TabControl()
        {
            InitializeComponent();
            Tabs.Add("Test1");
            Tabs.Add("Test2");
            Tabs.Add("Really Long Tab Name!!!");
            Tabs.Add("Extremely looooong tab name to test how well it handles it.");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SetClip(new RectangleF(0, 0, Width, TabMargin));
            //Clear the tab background
            e.Graphics.FillRectangle(Brushes.Gray, 0, 0, Width, TabMargin);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float x = 2;
            float width = 0;
            SizeF str;
            float top = 0;

            Brush brTab = Brushes.White;

            foreach (string tab in Tabs)
            {
                str = e.Graphics.MeasureString(tab, Font);
                width = str.Width + 10;

                //Top left corner
                e.Graphics.FillEllipse(brTab, x, top, 12, 12);

                //Top right corner
                e.Graphics.FillEllipse(brTab, x + width - 12, top, 12, 12);

                //Top left to right
                e.Graphics.FillRectangle(brTab, x + 6, top, width - 12, 12);

                //Left Side
                e.Graphics.FillRectangle(brTab, x, top + 6, 6, TabMargin - 6);

                //Right Side
                e.Graphics.FillRectangle(brTab, x + width - 6, top + 6, 6, TabMargin - 6);

                //Main body
                e.Graphics.FillRectangle(brTab, x + 5, top + 11, width - 10, TabMargin - 11);

                //Text
                e.Graphics.DrawString(tab, Font, Brushes.Black, x + ((width / 2) - (str.Width / 2)), top + ((TabMargin / 2) - (str.Height / 2)));

                //e.Graphics.FillRectangle(Brushes.Green, x, 0, width, TabMargin);
                //e.Graphics.DrawRectangle(Pens.White, x, 0, width, TabMargin);

                //e.Graphics.DrawString(tab, Font, Brushes.White, new RectangleF(x, 0, width, TabMargin), new StringFormat() { LineAlignment = StringAlignment.Center });
                x += width + 1.0f;
            }

            e.Graphics.ResetClip();
        }
    }
}
