using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MetaGeek.Diagnostics;

namespace inSSIDer.UI.Controls
{
    /// <summary>
    /// Description of CustomTabControl.
    /// </summary>
    [ToolboxBitmap(typeof(TabControl))]
    public class CustomTabControl : TabControl
    {
        #region Fields

        private const TabControlDisplayManager _displayManager = TabControlDisplayManager.Custom;

        #endregion Fields

        #region Enumerations

        public enum TabControlDisplayManager
        {
            Default,
            Custom
        }

        #endregion Enumerations

        #region Properties

        //This Fixes trackbar flicker. awesome article about this. http://stackoverflow.com/questions/2612487/how-to-fix-the-flickering-in-user-controls
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        [System.ComponentModel.DefaultValue(typeof(TabControlDisplayManager), "Custom")]
        public TabControlDisplayManager DisplayManager
        {
            get {
                return _displayManager;
            }
            set {
                if (_displayManager != value) {
                    SetStyle(ControlStyles.UserPaint, _displayManager.Equals(TabControlDisplayManager.Custom));
                }
            }
        }

        #endregion Properties

        #region Event Fields

        public RegisteredEventHandler<TabHeaderRightClickedEventArgs> TabHeaderRightClickedEvent = new RegisteredEventHandler<TabHeaderRightClickedEventArgs>();

        #endregion Event Fields

        #region Constructors

        public CustomTabControl()
        {
            if (_displayManager.Equals(TabControlDisplayManager.Custom)) {
                SetStyle(ControlStyles.UserPaint, true);
                ItemSize = new Size(0, 15);
                Padding = new Point(0,0);
            }

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeRedraw = true;
            MouseUp += CustomTabControl_MouseUp;
        }

        #endregion Constructors

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            //   Paint the Background
            PaintTransparentBackground(e.Graphics, ClientRectangle);

            PaintAllTheTabs(e);
            PaintTheTabPageBorder(e);
            e.Graphics.DrawLine(new Pen(Brushes.DarkGray, 1), Left, Top + GetTabRect(0).Bottom, Right, Top + GetTabRect(0).Bottom);
        }

        protected void PaintTransparentBackground(Graphics g, Rectangle clipRect)
        {
            if ((Parent != null)) {
                clipRect.Offset(Location);
                PaintEventArgs e = new PaintEventArgs(g, clipRect);
                GraphicsState state = g.Save();
                g.SmoothingMode = SmoothingMode.HighSpeed;
                try {
                    g.TranslateTransform(-Location.X, -Location.Y);
                    InvokePaintBackground(Parent, e);
                    InvokePaint(Parent, e);
                }

                finally {
                    g.Restore(state);
                    clipRect.Offset(-Location.X, -Location.Y);
                }
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void CustomTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int i = 0;
                foreach (TabPage tabPage in TabPages)
                {
                    Rectangle rt = GetTabRect(i++);
                    if(e.X > rt.Left && (e.X < rt.Left + rt.Width )&& e.Y < rt.Bottom)
                    {
                        TabHeaderRightClickedEvent.Raise(this, new TabHeaderRightClickedEventArgs(tabPage, e.Location));
                        break;
                    }
                }
            }
        }

        private GraphicsPath GetPath(int index)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = GetTabRect(index);

            if (index == SelectedIndex)
            {
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + 4);
                path.AddCurve(new[] { new Point(rect.Left, rect.Top + 4), new Point(rect.Left + 1, rect.Top + 2), new Point(rect.Left + 2, rect.Top + 1), new Point(rect.Left + 4, rect.Top) });
                path.AddLine(rect.Left + 4, rect.Top, rect.Right - 4, rect.Top);
                path.AddCurve(new[] { new Point(rect.Right - 4, rect.Top), new Point(rect.Right - 2, rect.Top + 1), new Point(rect.Right - 1, rect.Top + 2), new Point(rect.Right, rect.Top + 4) });
                path.AddLine(rect.Right, rect.Top + 4, rect.Right, rect.Bottom);
                path.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);
            }
            else
            {
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + 6);
                path.AddCurve(new[] { new Point(rect.Left, rect.Top + 6), new Point(rect.Left + 1, rect.Top + 4), new Point(rect.Left + 2, rect.Top + 3), new Point(rect.Left + 4, rect.Top + 2) });
                path.AddLine(rect.Left + 4, rect.Top + 2, rect.Right - 4, rect.Top + 2);
                path.AddCurve(new[] { new Point(rect.Right - 4, rect.Top + 2), new Point(rect.Right - 2, rect.Top + 3), new Point(rect.Right - 1, rect.Top + 4), new Point(rect.Right, rect.Top + 6) });
                path.AddLine(rect.Right, rect.Top + 6, rect.Right, rect.Bottom);
                path.AddLine(rect.Right, rect.Top, rect.Left, rect.Top);
            }

            return path;
        }

        private void PaintAllTheTabs(PaintEventArgs e)
        {
            if (TabCount > 0)
            {
                for (int index = 0; index < TabCount; index++)
                {
                    PaintTab(e, index);
                }
            }
        }

        private void PaintTab(PaintEventArgs e, int index)
        {
            GraphicsPath path = GetPath(index);
            path.AddRectangle(GetTabRect(index));
            PaintTabBackground(e.Graphics, index, path);
            PaintTabBorder(e.Graphics, index);
            PaintTabText(e.Graphics, index);
            PaintTabImage(e.Graphics, index);
        }

        private void PaintTabBackground(Graphics graph, int index, GraphicsPath path)
        {
            Rectangle rect = GetTabRect(index);
            Brush buttonBrush =
                new LinearGradientBrush(
                    rect,
                    Color.FromArgb(160,160,160),
                    Color.FromArgb(120,120,120),
                    LinearGradientMode.Vertical);

            if (index == SelectedIndex)
            {
                buttonBrush =
                new LinearGradientBrush(
                    rect,
                    Color.FromArgb(160,160,160),
                    Color.FromArgb(180,180,180),
                    LinearGradientMode.Vertical);
            }

            graph.FillPath(buttonBrush, path);
            buttonBrush.Dispose();
        }

        private void PaintTabBorder(Graphics graphics, int index)
        {
            var state = graphics.Save();
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = GetTabRect(index);
            if (index == SelectedIndex)
            {
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + 4);
                path.AddCurve(new[]
                                  {
                                      new Point(rect.Left, rect.Top + 4), new Point(rect.Left + 1, rect.Top + 2),
                                      new Point(rect.Left + 2, rect.Top + 1), new Point(rect.Left + 4, rect.Top)
                                  });
                path.AddLine(rect.Left + 4, rect.Top, rect.Right - 4, rect.Top);
                path.AddCurve(new[]
                                  {
                                      new Point(rect.Right - 4, rect.Top), new Point(rect.Right - 2, rect.Top + 1),
                                      new Point(rect.Right - 1, rect.Top + 2), new Point(rect.Right, rect.Top + 4)
                                  });
                path.AddLine(rect.Right, rect.Top + 4, rect.Right, rect.Bottom);
            }
            else
            {
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top + 6);
                path.AddCurve(new[] { new Point(rect.Left, rect.Top + 6), new Point(rect.Left + 1, rect.Top + 4), new Point(rect.Left + 2, rect.Top + 3), new Point(rect.Left + 4, rect.Top + 2) });
                path.AddLine(rect.Left + 4, rect.Top + 2, rect.Right - 4, rect.Top + 2);
                path.AddCurve(new[] { new Point(rect.Right - 4, rect.Top + 2), new Point(rect.Right - 2, rect.Top + 3), new Point(rect.Right - 1, rect.Top + 4), new Point(rect.Right, rect.Top + 6) });
                path.AddLine(rect.Right, rect.Top + 6, rect.Right, rect.Bottom);
            }
            using (var pen = new Pen(SystemColors.ControlDarkDark))
            {
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawPath(pen, path);
            }
            graphics.Restore(state);
        }

        private void PaintTabImage(Graphics graph, int index)
        {
            Image tabImage = null;
            if (TabPages[index].ImageIndex > -1 && ImageList != null)
            {
                tabImage = ImageList.Images[TabPages[index].ImageIndex];
            }
            else if (TabPages[index].ImageKey.Trim().Length > 0 && ImageList != null)
            {
                tabImage = ImageList.Images[TabPages[index].ImageKey];
            }
            if (tabImage != null)
            {
                Rectangle rect = GetTabRect(index);
                graph.DrawImage(tabImage, rect.Left + (int)(rect.Width * 0.08), 6, rect.Height - 4, rect.Height - 4);
            }
        }

        private void PaintTabText(Graphics graph, int index)
        {
            Rectangle rect = GetTabRect(index);
            Rectangle rect2 = new Rectangle(rect.Left, rect.Top, rect.Width - (int)(rect.Width * 0.08), rect.Height);
            if(ImageList == null)
            {
                rect2 = new Rectangle(rect.Left-6, rect.Top+1, rect.Width - (int)(rect.Width * 0.08), rect.Height);
            }
            string tabtext = TabPages[index].Text;

            StringFormat format = new StringFormat
                                      {
                                          Alignment = StringAlignment.Far,
                                          LineAlignment = StringAlignment.Center,
                                          Trimming = StringTrimming.EllipsisCharacter
                                      };

            Brush forebrush = TabPages[index].Enabled == false ? SystemBrushes.ControlDark : SystemBrushes.ControlText;

            graph.DrawString(tabtext, Font, forebrush, rect2, format);
        }

        private void PaintTheTabPageBorder(PaintEventArgs e)
        {
            if (TabCount > 0)
            {
                Rectangle borderRect = TabPages[0].Bounds;
                borderRect.Inflate(1, 1);
                ControlPaint.DrawBorder(e.Graphics, borderRect, Color.Black, ButtonBorderStyle.Solid);
            }
        }

        #endregion Private Methods
    }
}