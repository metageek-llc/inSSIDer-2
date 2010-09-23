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

namespace inSSIDer.Misc
{
    public static class SignalColor
    {
        private static Dictionary<int, Color> _colors;

        private const int UpperBounds = 0;
        private const int LowerBounds = -100;
        private const int Threshold = 5;

        private static void InitColors()
        {
            _colors = new Dictionary<int, Color>();

            //Define Color points
            Dictionary<int, Color> defs = new Dictionary<int, Color>
                                              {
                                                  {UpperBounds, Color.LimeGreen},
                                                  {-50, Color.LimeGreen},
                                                  {-55, Color.GreenYellow},
                                                  {-60, Color.Yellow},
                                                  {-70, Color.Orange},
                                                  {-80, Color.OrangeRed},
                                                  {LowerBounds, Color.Red}
                                              };

            //Defs
            Color upC;
            Color lowC;
            Color curC;

            int b1 = UpperBounds;
            int b2 = LowerBounds;

            int r, g, b;

            float distU;
            float distL;

            float acc1;
            float acc2;

            //Interpolate the points
            for (int i = 0; i >= -100; i--)
            {
                //If the current point is equal to a defined point, use that one.
                if(defs.ContainsKey(i))
                {
                    _colors.Add(i, defs[i]);
                    continue;
                }
                //Find which borders the current value lies between
                foreach (int key in defs.Keys)
                {
                    if (key > i)
                    {
                        b1 = key;
                    }
                    if(key < i)
                    {
                        b2 = key;
                        break;
                    }
                }

                //We have the bounderies set, continue
                upC = defs[b1];
                lowC = defs[b2];

                //Find the inverse distance from the current point, i, to the boundries
                distU = Math.Abs(1f / (b1 - i));
                distL = Math.Abs(1f / (b2 - i));

                //Solve Red
                acc1 = (distU * upC.R) + (distL * lowC.R);
                acc2 = distU + distL;

                r = (int)(acc1 / acc2);

                //Solve Green
                acc1 = (distU * upC.G) + (distL * lowC.G);
                acc2 = distU + distL;

                g = (int)(acc1 / acc2);

                //Solve Blue
                acc1 = (distU * upC.B) + (distL * lowC.B);
                acc2 = distU + distL;

                b = (int)(acc1 / acc2);

                //Set the color
                curC = Color.FromArgb(r, g, b);

                //Add the color to the dictionary
                _colors.Add(i, curC);
            }
        }

        public static Color GetColor(int signal)
        {
            if (_colors == null) InitColors();

            if(_colors == null || !_colors.ContainsKey(signal)) return Color.LightGray;
            return _colors[signal];
        }

        public static Color GetColorThreshold(int signal)
        {
            if (_colors == null) InitColors();

            signal = signal - (signal%Threshold);
            if (_colors == null || !_colors.ContainsKey(signal)) return Color.LightGray;
            return _colors[signal];
        }
    }
}
