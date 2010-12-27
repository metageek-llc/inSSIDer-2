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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using inSSIDer.Misc;
using inSSIDer.Scanning;
using MetaGeek.WiFi;
using inSSIDer.Localization;
using System.Diagnostics;

namespace inSSIDer.UI.Controls
{
    public partial class ChannelView : UserControl
    {
        //Graph size
        private int _graphWidth;
        private int _graphHeight;

        //Colors
        private readonly Color _gridColor = Color.FromArgb(100, Color.Gray);
        private readonly Color _highChannelForeColor = Color.Green;
        private readonly Color _graphBackColor = Color.Black;
        private readonly Color _outlineColor = Color.DimGray;
        private readonly Color _tickColor = Color.LightGray;

        //Pixel multipilers
        private float _pixelsPerDbm = 1f;
        private float _pixelsPerMHz = 1f;

        //Label spacing
        private int _amplitudeLabelSpacing = 10;

        //Fonts
        private readonly Font _boldFont;

        //The band type
        private BandType _band = BandType.Band2400MHz;

        private ScanController _sc;

        //The copy context menu
        private readonly ContextMenuStrip _cmsCopy;

        public enum BandType
        {
            Band2400MHz = 2400,
            Band5000MHz = 5000
        }

        public ChannelView()
        {
            MinFrequency = 2400;
            MaxFrequency = 2495;
            MinAmplitude = -100;
            MaxAmplitude = -10;
            BottomMargin = 20;
            TopMargin = 10;
            LeftMargin = 55;
            RightMargin = 10;
            InitializeComponent();

            SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            _boldFont = new Font(Font, FontStyle.Bold);


            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy to clipboard");
            copyItem.Click += CopyItemClick;

            _cmsCopy = new ContextMenuStrip();

            _cmsCopy.Items.Add(copyItem);

            ContextMenuStrip = _cmsCopy;
        }

        private void CopyItemClick(object sender, EventArgs e)
        {
            try
            {
                //copy image to clipboard
                using (Bitmap bitClip = new Bitmap(Width, Height))
                {
                    using (Graphics g = Graphics.FromImage(bitClip))
                    {
                        //Fill the background with the back color so the graph looks right
                        g.Clear(BackColor);
                        //Draw the graph to the surface
                        DrawView(g);
                        //Then put it on the clipboard
                        Clipboard.SetImage(bitClip);
                    }
                }
            }
            catch
            {
                MessageBox.Show(Localizer.GetString("CopyGraphError"), Localizer.GetString("Error"),
                                MessageBoxButtons.OK);
            }
        }

        public void SetScanner(ref ScanController scanner)
        {
            _sc = scanner;
        }

        private void ChannelView_SizeChanged(object sender, EventArgs e)
        {
            UpdateGraphDimensions();
            Invalidate();
        }

        private void UpdateGraphDimensions()
        {
            if ((Height <= 0) || (Width <= 0)) return;

            _graphWidth = (Width - LeftMargin - RightMargin);
            _graphHeight = (Height - TopMargin - BottomMargin);

            float viewableRange = MaxAmplitude - MinAmplitude + 1;
            _pixelsPerDbm = _graphHeight / viewableRange;

            if (_pixelsPerDbm < 1.1)
            {
                _amplitudeLabelSpacing = 20;
            }
            else if (_pixelsPerDbm < 3.3)
            {
                _amplitudeLabelSpacing = 10;
            }
            else if (_pixelsPerDbm < 6.5)
            {
                _amplitudeLabelSpacing = 5;
            }
            else if (_pixelsPerDbm >= 6.5)
            {
                _amplitudeLabelSpacing = 2;
            }

            viewableRange = MaxFrequency - MinFrequency + 1;
            _pixelsPerMHz = _graphWidth / viewableRange;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawView(e.Graphics);
        }

        /// <summary>
        /// Draws the whole graph to the specified drawing surface.
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawView(Graphics graphics)
        {
            //graphics.SmoothingMode = SmoothingMode.HighQuality;
            //Draw the amplitude grid and labels
            DrawGrid(graphics);

            DrawLabels(graphics);

            // set cropping region
            graphics.Clip = new Region(new Rectangle(LeftMargin, TopMargin, _graphWidth, _graphHeight));

            DrawNetworks(graphics);

            // reset cropping region
            graphics.ResetClip();
        }

        private void DrawGrid(Graphics graphics)
        {
            Pen pen = new Pen(_outlineColor);
            SolidBrush brush = new SolidBrush(_graphBackColor);

            graphics.FillRectangle(brush, LeftMargin - 1, TopMargin - 1, _graphWidth + 1, _graphHeight);

            graphics.DrawRectangle(pen, LeftMargin - 1, TopMargin - 1, _graphWidth + 1, _graphHeight + 1);

            brush.Color = ForeColor;
            //graphics.DrawString(Localizer.GetString("ChannelView"), this.Font, brush, _leftMargin, 3);

            //Draw rotated line and text
            float y = (_graphHeight / 2f) + graphics.MeasureString(Localizer.GetString("AmplitudedBm"), Font).Width / 2 + TopMargin;
            PointF rotationPoint = new PointF(8, y);
            Matrix matrix = new Matrix();
            matrix.RotateAt(270, rotationPoint);
            graphics.Transform = matrix;
            graphics.DrawString("Amplitude [dB]"/*Localizer.GetString("AmplitudedBm")*/, Font, brush, 8, y);
            matrix.RotateAt(90, rotationPoint);
            graphics.Transform = matrix;

            // Y axis
            float maxAmpToLabel = MaxAmplitude - (_amplitudeLabelSpacing / 3f);
            int labelAmplitude = (int)(MinAmplitude - (MinAmplitude % _amplitudeLabelSpacing) + _amplitudeLabelSpacing);

            StringFormat sfAmp = new StringFormat {Alignment = StringAlignment.Far};

            //sfAmp.LineAlignment = StringAlignment.Center;

            while (labelAmplitude < maxAmpToLabel)
            {
                brush.Color = SignalColor.GetColor(labelAmplitude);
                // amplitude label
                y = TopMargin + _graphHeight - ((labelAmplitude - MinAmplitude) * _pixelsPerDbm);
                graphics.DrawString(labelAmplitude.ToString(), Font, brush, LeftMargin - 5, y - 7,sfAmp);

                // draw the horizontal graph lines
                pen.Color = _gridColor;
                pen.DashStyle = DashStyle.Dot;
                graphics.DrawLine(pen, LeftMargin, y, LeftMargin + _graphWidth, y);

                // Tick marks next to amplitude labels
                pen.Color = _tickColor;
                pen.DashStyle = DashStyle.Solid;
                graphics.DrawLine(pen, LeftMargin - 3, y, LeftMargin + 1, y);

                labelAmplitude += _amplitudeLabelSpacing;
            }

            brush.Color = SignalColor.GetColor(-100);

            //Draw floor label and tick
            y = DbToY((int) MinAmplitude);
            pen.Color = _tickColor;
            pen.DashStyle = DashStyle.Solid;
            //Tick
            graphics.DrawLine(pen, LeftMargin - 3, y, LeftMargin + 1, y);
            //label
            graphics.DrawString(MinAmplitude.ToString(), Font, brush, LeftMargin - 5, y - 7, sfAmp);

            pen.Dispose();
        }

        // Draw channel labels
        private void DrawLabels(Graphics graphics)
        {
            SolidBrush brush = new SolidBrush(ForeColor);

            float x;
            int freq;

            // X axis labels
            int y = Height - BottomMargin + 5;


            if (_band == BandType.Band2400MHz)
            {
                for (int channel = 0; channel < 14; channel++)
                {
                    freq = 2412 + (5 * channel);
                    if (channel == 13)
                    {
                        freq += 7;
                    }

                    x = LeftMargin + (int)(_pixelsPerMHz * (freq - MinFrequency) + (_pixelsPerMHz * 0.5f));	// 11 for middle of channel, 1 for 2401 MHz offset
                    brush.Color = ForeColor;

                    if (channel < 9)
                    {
                        graphics.DrawString((channel + 1).ToString(), Font, brush, x - 4, y);
                    }
                    else
                    {
                        graphics.DrawString((channel + 1).ToString(), Font, brush, x - 8, y);
                    }
                }
            }
            else
            {
                for (int channel = 36; channel <= 165; )
                {
                    freq = 5000 + (5 * channel);

                    x = LeftMargin + (int)(_pixelsPerMHz * (freq - MinFrequency) + (_pixelsPerMHz * 0.5f));
                    if (channel <= 64 || channel >= 149)
                    {
                        brush.Color = ForeColor;
                        graphics.DrawString((channel).ToString(), Font, brush, x - (channel > 99 ? 8 : 4), y);
                    }
                    else
                    {
                        brush.Color = _highChannelForeColor;
                        graphics.DrawString((channel).ToString(), Font, brush, x - (channel > 99 ? 8 : 4), y);
                    }

                    switch (channel)
                    {
                        case 64:
                            channel = 100;
                            break;
                        case 140:
                            channel = 149;
                            break;
                        default:
                            channel += 4;
                            break;
                    }
                }
            }
            brush.Dispose();
        }

        /// <summary>
        /// Draws Wi-Fi network overlays
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawNetworks(Graphics graphics)
        {
            try
            {
                if (_sc == null) return;
                AccessPoint[] networks = _sc.Cache.GetAccessPoints();
                if (networks.Length == 0) return;
                SolidBrush brush = new SolidBrush(Color.Red);

                //TODO: Is this lock really needed?
                lock (networks)
                {
                    try
                    {
                        foreach (AccessPoint ap in networks)
                        {
                            //Skip APs that aren't in the visible band
                            if(Band == BandType.Band2400MHz && ap.Channel > 14) continue;
                            if(Band == BandType.Band5000MHz && ap.Channel <= 14) continue;

                            if (!ap.Graph) continue;
                            //if (!network.Visible) continue;
                            int x = LeftMargin +
                                    (int)
                                    ((Utilities.ConvertToFrequency(ap.Channel) - MinFrequency)*_pixelsPerMHz);
                            int y = DbToY(ap.LastData.Rssi);

                            //Dims when inactive for 6 seconds
                            //network.Age < 255 ? 255 - network.Age : 0
                            Pen pen = Pens.White;
                            try
                            {
                                pen = new Pen(
                                    Color.FromArgb(ap.Age * 20 < 255 ? 255 - (ap.Age * 20) : 0, ap.MyColor), 2);
                            }
                            catch (ArgumentException)
                            {
                                
                            }
                            if (ap.Highlight)
                            {
                                pen.Width = 4;
                            }

                            switch (ap.Privacy)
                            {
                                case "None":
                                    pen.DashStyle = DashStyle.Dot;
                                    break;

                                case "WEP":
                                    pen.DashStyle = DashStyle.Dash;
                                    break;

                                default:
                                    pen.DashStyle = DashStyle.Solid;
                                    break;
                            }

                            int halfChannelWidthMhz;
                            if (ap.Channel > 14)
                            {
                                halfChannelWidthMhz = 10;
                                x += (int) (3*_pixelsPerMHz);
                            }
                            else if (ap.Channel <= 14 || ap.MaxRate >= 20)
                            {
                                halfChannelWidthMhz = 10;
                            }
                            else
                            {
                                halfChannelWidthMhz = 11;
                            }

                            float floorY = TopMargin + _graphHeight;

                            //Size the SSID string
                            SizeF stringSize = graphics.MeasureString(ap.Ssid, ap.Highlight ? _boldFont : Font);


                            // 802.11b arch shape
                            if ((ap.MaxRate <= 20) && (Utilities.ConvertToFrequency(ap.Channel) < 2500))
                            {
                                //Debug.WriteLine("Draw 802.11b arch", "ChannelView Draw");
                                PointF[] points = new PointF[3];
                                points[0] = new PointF(x - (halfChannelWidthMhz*_pixelsPerMHz), floorY);
                                points[1] = new PointF(x, y);
                                points[2] = new PointF(x + (halfChannelWidthMhz*_pixelsPerMHz), floorY);
                                graphics.DrawCurve(pen, points, 1);

                                //Set SSID label to center on a non 40MHz channel
                                x = x - (int)(stringSize.Width / 2f);
                            }
                                // 802.11a/g/n plateau shape
                            else
                            {
                                //Debug.WriteLine("Draw 802.11a/g/n plateau", "ChannelView Draw");
                                PointF[] points = new PointF[5];

                                float quarterY = (floorY - y)/4;
                                if (/*ap.IsN &&*/ ap.NSettings != null && ap.NSettings.Is40MHz)
                                {
                                    //Debug.WriteLine("40MHz 802.11n channel", "ChannelView Draw");
                                    //Extend for 40Mhz channel
                                    if (ap.NSettings.SecondaryChannelLower)
                                    {
                                        //Debug.WriteLine("Secondary channel lower", "ChannelView Draw");
                                        points[0] = new PointF(x - ((halfChannelWidthMhz*3)*_pixelsPerMHz), floorY);
                                        points[1] = new PointF(x - (((halfChannelWidthMhz*3) - 1)*_pixelsPerMHz),
                                                               floorY - quarterY);
                                        points[2] = new PointF(x - (((halfChannelWidthMhz*3) - 1.5f)*_pixelsPerMHz),
                                                               floorY - (2*quarterY));
                                        points[3] = new PointF(
                                            x - (((halfChannelWidthMhz*3) - 1.5f)*_pixelsPerMHz), points[2].Y - 5);
                                        points[4] = new PointF(
                                            x - (((halfChannelWidthMhz*3) - 1.5f)*_pixelsPerMHz), y);
                                        graphics.DrawCurve(pen, points, 0.3f);

                                        PointF topleft = points[4];

                                        points[0].X = x + halfChannelWidthMhz*_pixelsPerMHz;
                                        points[1].X = x + (halfChannelWidthMhz - 1)*_pixelsPerMHz;
                                        points[2].X = x + (halfChannelWidthMhz - 1.5f)*_pixelsPerMHz;
                                        points[3].X = x + (halfChannelWidthMhz - 1.5f)*_pixelsPerMHz;
                                        points[4].X = x + (halfChannelWidthMhz - 1.5f)*_pixelsPerMHz;
                                        graphics.DrawCurve(pen, points, 0.2f);

                                        graphics.DrawLine(pen, points[4], topleft);

                                        //Set the SSID location for lower channel bonded 40MHZ channel
                                        x = (x - (int)(halfChannelWidthMhz * _pixelsPerMHz)) - (int)(stringSize.Width / 2f);
                                    }
                                    else
                                    {
                                        //Debug.WriteLine("Secondary channel higher", "ChannelView Draw");
                                        points[0] = new PointF(x - (halfChannelWidthMhz*_pixelsPerMHz), floorY);
                                        points[1] = new PointF(x - ((halfChannelWidthMhz - 1)*_pixelsPerMHz),
                                                               floorY - quarterY);
                                        points[2] = new PointF(x - ((halfChannelWidthMhz - 1.5f)*_pixelsPerMHz),
                                                               floorY - (2*quarterY));
                                        points[3] = new PointF(x - ((halfChannelWidthMhz - 1.5f)*_pixelsPerMHz),
                                                               points[2].Y - 5);
                                        points[4] = new PointF(x - ((halfChannelWidthMhz - 1.5f)*_pixelsPerMHz), y);
                                        graphics.DrawCurve(pen, points, 0.3f);

                                        PointF topleft = points[4];

                                        points[0].X = x + (halfChannelWidthMhz*3)*_pixelsPerMHz;
                                        points[1].X = x + ((halfChannelWidthMhz*3) - 1)*_pixelsPerMHz;
                                        points[2].X = x + ((halfChannelWidthMhz*3) - 1.5f)*_pixelsPerMHz;
                                        points[3].X = x + ((halfChannelWidthMhz*3) - 1.5f)*_pixelsPerMHz;
                                        points[4].X = x + ((halfChannelWidthMhz*3) - 1.5f)*_pixelsPerMHz;
                                        graphics.DrawCurve(pen, points, 0.2f);

                                        graphics.DrawLine(pen, points[4], topleft);

                                        //Set the SSID location for upper channel bonded 40MHZ channel
                                        x = (x + (int)(halfChannelWidthMhz * _pixelsPerMHz)) - (int)(stringSize.Width / 2f);
                                    }
                                }
                                else // draw a 20MHz channe;
                                {
                                    //Debug.WriteLine("20MHz channel", "ChannelView Draw");
                                    points[0] = new PointF(x - (halfChannelWidthMhz*_pixelsPerMHz), floorY);
                                    points[1] = new PointF(x - ((halfChannelWidthMhz - 1)*_pixelsPerMHz),
                                                           floorY - quarterY);
                                    points[2] = new PointF(x - ((halfChannelWidthMhz - 1.5f)*_pixelsPerMHz),
                                                           floorY - (2*quarterY));
                                    points[3] = new PointF(x - ((halfChannelWidthMhz - 1.5f)*_pixelsPerMHz),
                                                           points[2].Y - 5);
                                    points[4] = new PointF(x - ((halfChannelWidthMhz - 1.5f)*_pixelsPerMHz), y);
                                    graphics.DrawCurve(pen, points, 0.3f);

                                    PointF topleft = points[4];

                                    points[0].X = x + halfChannelWidthMhz*_pixelsPerMHz;
                                    points[1].X = x + (halfChannelWidthMhz - 1)*_pixelsPerMHz;
                                    points[2].X = x + (halfChannelWidthMhz - 1.5f)*_pixelsPerMHz;
                                    points[3].X = x + (halfChannelWidthMhz - 1.5f)*_pixelsPerMHz;
                                    points[4].X = x + (halfChannelWidthMhz - 1.5f)*_pixelsPerMHz;
                                    graphics.DrawCurve(pen, points, 0.2f);

                                    graphics.DrawLine(pen, points[4], topleft);

                                    //Set SSID label to center on a non 40MHz channel
                                    x = x - (int)(stringSize.Width / 2f);
                                }
                            }
                            y -= 15;

                            //Debug.WriteLine("Draw SSID", "ChannelView Draw");
                            brush.Color = Color.FromArgb(ap.Age * 20 < 255 ? 255 - (ap.Age * 20) : 0, ap.MyColor);
                            graphics.DrawString(ap.Ssid, ap.Highlight ? _boldFont : Font, brush, x, y);
                            //}
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        // occurs when collection is changed...
                    }
                }
            }
            catch(ArgumentException)
            {
                
            }
        }

/*
        private int WifiChannelToX(uint channel)
        {
            if (_band == BandType.Band5000MHz) {
                return (LeftMargin + (int)(((5165 - MinFrequency) + (channel * 5)) * _pixelsPerMHz));
            }
            return (LeftMargin + (int)(((2407 - MinFrequency) + (channel * 5)) * _pixelsPerMHz));
        }
*/

        private int DbToY(int db)
        {
            return (int)(TopMargin + _graphHeight - ((db - MinAmplitude) * _pixelsPerDbm));
        }

        //public void SetNetworks(AccessPoint[] networks)
        //{
        //    _networks = networks;
        //    Invalidate();
        //}

        //Properties

        /// <summary>
        /// Sets the band of the graph
        /// </summary>
        [Category("Configuration"), DefaultValue(BandType.Band2400MHz)]
        public BandType Band
        {
            get { return _band; }
            set
            {
                _band = value;

                if (_band == BandType.Band5000MHz)
                {
                    MinFrequency = 5150F;
                    MaxFrequency = 5850F;
                }
                else
                {
                    MinFrequency = 2400F;
                    MaxFrequency = 2495F;
                }

                float viewableRange = MaxFrequency - MinFrequency + 1;
                _pixelsPerMHz = _graphWidth / viewableRange;
            }
        }

        /// <summary>
        /// Pixels from right to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(10)]
        public int RightMargin { get; set; }

        /// <summary>
        /// Pixels from left to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(55)]
        public int LeftMargin { get; set; }

        /// <summary>
        /// Pixels from top to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(10)]
        public int TopMargin { get; set; }

        /// <summary>
        /// Pixels from bottom to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(20)]
        public int BottomMargin { get; set; }

        /// <summary>
        /// The maximum amplitude in dB
        /// </summary>
        [Category("Configuration"), DefaultValue(-20)]
        public float MaxAmplitude { get; set; }

        /// <summary>
        /// The minimum amplitude in dB
        /// </summary>
        [Category("Configuration"), DefaultValue(-100)]
        public float MinAmplitude { get; set; }

        /// <summary>
        /// The maximum (right most) frequency visible in MHz
        /// </summary>
        [Category("Configuration"), DefaultValue(2495)]
        public float MaxFrequency { get; set; }

        /// <summary>
        /// The minimum (left most) frequency visible in MHz
        /// </summary>
        [Category("Configuration"), DefaultValue(2400)]
        public float MinFrequency { get; set; }
    }
}
