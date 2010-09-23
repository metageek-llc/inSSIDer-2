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
using System.Windows.Forms;
using inSSIDer.Scanning;
using MetaGeek.Gps;
using System.Drawing.Drawing2D;

namespace inSSIDer.UI.Controls
{
    public partial class GpsGraph : UserControl
    {
        private Scanner _sc;
        private Satellite[] _lastSats;

        private LinearGradientBrush _lgb;

        private float _pxPerAmp;

        public GpsGraph()
        {
            TopMargin = 5;
            LeftMargin = 50;
            BottomMargin = 20;
            RightMargin = 5;

            InitializeComponent();

            SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawView(e.Graphics);
        }

        public void SetScanner(ref Scanner s)
        {
            _sc = s;
        }

        private void DrawView(Graphics g)
        {
            DrawBars(g);
            DrawGrid(g);
        }

        private void DrawBars(Graphics g)
        {
            if (_sc == null || _sc.GpsControl == null || _sc.GpsControl.Satellites == null) return;

            if(_sc.GpsControl.AllSatellitesLoaded)
            {
                _lastSats = _sc.GpsControl.Satellites.ToArray();
            }
            //Wait for the inital satellite readings first
            if (_lastSats == null) return;

            //The number of satellites to draw
            int numBars = _lastSats.Length;
            float barWidth = ((Width - LeftMargin - RightMargin) / (float)numBars) - 4f;

            float x = LeftMargin;

            //g.Clear(BackColor);

            RectangleF recId;
            RectangleF recBar;
            StringFormat sfCenter = new StringFormat {Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center};

            Satellite satTemp;

            //recId = new RectangleF(x, Height - BottomMargin, barWidth, BottomMargin);

            g.DrawString("ID:", Font, Brushes.Lime, new RectangleF(0, Height - BottomMargin, LeftMargin, BottomMargin),
                         sfCenter);

            for (int i = 0; i < numBars; i++)
            {
                satTemp = _lastSats[i];
                if (satTemp.Snr < 0) satTemp.Snr = 0;
                x += 2;

                recBar = new RectangleF(x, TopMargin, barWidth, Height - TopMargin - BottomMargin);                

                //SNR
                g.FillRectangle(_lgb, x, (Height - BottomMargin) - (float)(satTemp.Snr * _pxPerAmp), barWidth, (float)(satTemp.Snr * _pxPerAmp));
                
                //Gray out the unused satellites a little
                if(!satTemp.IsUsed)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(145, Color.Gray)), x, (Height - BottomMargin) - (float)(satTemp.Snr * _pxPerAmp), barWidth, (float)(satTemp.Snr * _pxPerAmp));
                }

                g.DrawString(satTemp.Snr.ToString("F0"), Font, satTemp.IsUsed ? Brushes.Lime : Brushes.DarkGray, recBar, sfCenter);


                recId = new RectangleF(x, Height - BottomMargin, barWidth, BottomMargin);

                //Draw ID
                g.DrawString(satTemp.Id.ToString(), Font, satTemp.IsUsed ? Brushes.Lime : Brushes.DarkGray, recId, sfCenter);

                //Tyler: I did this becuase there is no DrawRectangle method that accepts a RectangleF
                g.DrawRectangles(Pens.DimGray, new[] { recBar });

                x += barWidth;

                x += 2;
            }

            //for (int x = LeftMargin; x < Width - RightMargin; x+=barWidth)
            //{
                


            //}
        }

        private void DrawGrid(Graphics g)
        {
            int x = LeftMargin;
            float y;

            StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };

            for (int i = 0; i < 100; i+=10)
            {
                y = Height - BottomMargin - (i * _pxPerAmp);
                g.DrawLine(Pens.Gray, x, y, x + 4, y);

                g.DrawString(i.ToString(), Font, Brushes.Lime, x - 2, y-6,sfRight);
            }

            //Draw rotated line and text
            y = ((Height - TopMargin - BottomMargin) / 2f) + g.MeasureString("Signal-to-Noise Ratio [dB]"/*Localizer.GetString("AmplitudedBm")*/, Font).Width / 2 + TopMargin;
            PointF rotationPoint = new PointF(8, y);
            Matrix matrix = new Matrix();
            matrix.RotateAt(270, rotationPoint);
            g.Transform = matrix;
            g.DrawString("Signal-to-Noise Ratio [dB]"/*Localizer.GetString("AmplitudedBm")*/, Font, Brushes.Lime, 8, y);
            matrix.RotateAt(90, rotationPoint);
            g.Transform = matrix;

            //Draw outline
            g.DrawRectangle(Pens.Gray, LeftMargin+2, TopMargin, Width - LeftMargin - RightMargin, Height - TopMargin - BottomMargin);
        }

        private void CreateBrush()
        {
            _lgb = new LinearGradientBrush(new Point(0, TopMargin), new Point(0, Height - BottomMargin), Color.Black,
                                          Color.White);


            ColorBlend cb = new ColorBlend(7)
                                {
                                    Colors = new[]
                                                 {
                                                     Color.Green, Color.Green, Color.YellowGreen, Color.Yellow,
                                                     Color.Orange, Color.OrangeRed,
                                                     Color.Red
                                                 },
                                    Positions = new[] {0, 0.5f, 0.55f, 0.7f, 0.8f, 0.9f, 1f}
                                };

            _lgb.InterpolationColors = cb;

            //We need to map SNR 0..99 to a float 0..1

            //
            //0 = 0 = Red
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _pxPerAmp = (Height - TopMargin - BottomMargin) / 100f;

            //Regenerate the gradient
            CreateBrush();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// Pixels from right to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(5)]
        private int RightMargin { get; set; }

        /// <summary>
        /// Pixels from left to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(50)]
        public int LeftMargin { get; set; }

        /// <summary>
        /// Pixels from top to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(5)]
        private int TopMargin { get; set; }

        /// <summary>
        /// Pixels from bottom to place border
        /// </summary>
        [Category("Configuration"), DefaultValue(30)]
        public int BottomMargin { get; set; }
    }
}
