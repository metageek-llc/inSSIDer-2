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
using System.Collections;
using System.Collections.Generic;

namespace ManagedWifi
{
    public static class IeParser
    {
        #region Public Methods

        /*public static TypeNSettings Parse(byte[] ies)
        {
            try
            {
                int index = -1;

                //for (int i = 0; i < ies.Length; i++)
                for (int i = 0; i < ies.Length - 31; i++)
                {
                    //loop until we find the HT IE field 41 with length of 26 and the extended info futher down
                    //HT IE 1's signature is 2D 1A
                    //HT IE 2's signature is 3D 16
                    if ((ies[i] == 0x2D && ies[i + 1] == 0x1A) && (ies[i + 28] == 0x3D && ies[i + 29] == 0x16))
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1) return null;

                // We know that ies.Length > index + 31 so all of the following array indexes should be in bounds

                TypeNSettings settings = new TypeNSettings
                                             {
                                                 Is40MHz = ((ies[index + 2] & 0x02) == 0x02),
                                                 ShortGi20MHz = (ies[index + 2] & 0x20) == 0x20,
                                                 ShortGi40MHz = (ies[index + 2] & 0x40) == 0x40
                                             };

                //Supported MCS indexes
                //1 bit per index
                byte[] bits = new byte[4];
                Array.ConstrainedCopy(ies, index + 5, bits, 0, 4);

                BitArray b = new BitArray(bits);
                settings.Rates = new List<double>();
                int maxIndex = -1;

                for (int i = 0; i < b.Length; i++)
                {
                    if (b[i] == false) continue;
                    //Update the highest MCS index
                    if (b[i]) maxIndex = i;
                    //Add the rate
                    settings.Rates.Add(McsSet.GetSpeed((uint)i, settings.ShortGi20MHz, settings.ShortGi40MHz,
                                                       settings.Is40MHz));
                }
                if (maxIndex == -1) return null; // there has been an error

                //settings.MaxMcs = (uint)maxIndex;
                //Extended info
                //Primary channel
                settings.PrimaryChannel = ies[index + 30];

                //Secondary channel location
                settings.SecondaryChannelLower = (ies[index + 31] & 0x03) == 0x03;

                //Check if there is no secondary channel and set 40MHz to false
                if (settings.Is40MHz)
                    settings.Is40MHz = (ies[index + 31] & 0x03) == 0x03 || (ies[index + 31] & 0x01) == 0x01;

                return settings;
            }
            catch (IndexOutOfRangeException)
            {
                //We can't get the 802.11n IEs. Just return null
                return null;
            }
        }*/
        public static TypeNSettings Parse(byte[] ies)
        {
            //The indexes for both elements
            int HtCapIndex = -1;
            int HtInfoIndex = -1;

            bool returnNull = true;

            //Look for the HT Capabilities element
            for (int i = 0; i < ies.Length - 31; i++)
            {
                //HT Capabilities's signature is 2D 1A
                if ((ies[i] == 0x2D && ies[i + 1] == 0x1A) && (ies.Length >= i + 27))
                {
                    //Found it
                    HtCapIndex = i;
                }

                //HT Information's signature is 3D 16
                if ((ies[i] == 0x3D && ies[i + 1] == 0x16) && (ies.Length >= i + 24))
                {
                    //Found it
                    HtInfoIndex = i;
                }
            }

            TypeNSettings settings = new TypeNSettings();

            //Parse the info blocks if we found them
            if (HtCapIndex > -1)
            {
                settings.Is40MHz = ((ies[HtCapIndex + 2] & 0x02) == 0x02);

                settings.ShortGi20MHz = (ies[HtCapIndex + 2] & 0x20) == 0x20;
                settings.ShortGi40MHz = (ies[HtCapIndex + 2] & 0x40) == 0x40;

                //Get supported MCS indexes
                //1 bit per index

                byte[] bits = new byte[4];
                //Array.ConstrainedCopy(ies, index + 5, bits, 0, 4);
                Array.Copy(ies, HtCapIndex + 5, bits, 0, 4);

                BitArray b = new BitArray(bits);
                //settings.Rates = new List<double>();

                //The MCS indexes are in little endian,
                //so this loop will start at the lowest rates
                for (int i = 0; i < b.Length; i++)
                {
                    //If the MCS index bit is 0, skip it
                    if (b[i] == false) continue;

                    //Add the rate
                    settings.Rates.Add(McsSet.GetSpeed((uint)i, settings.ShortGi20MHz, settings.ShortGi40MHz,
                                                       settings.Is40MHz));
                }

                returnNull = false;
            }

            if (HtInfoIndex > -1)
            {
                //Primary channel
                settings.PrimaryChannel = ies[HtInfoIndex + 2];

                //Secondary channel location
                settings.SecondaryChannelLower = (ies[HtInfoIndex + 3] & 0x03) == 0x03;

                //Check if there is no secondary channel and set 40MHz to false
                if (settings.Is40MHz)
                    settings.Is40MHz = (ies[HtInfoIndex + 3] & 0x03) == 0x03 || (ies[HtInfoIndex + 3] & 0x01) == 0x01;

                returnNull = false;
            }

            return returnNull ? null : settings;
        }

        public class TypeNSettings
        {
            public bool Is40MHz;
            public bool ShortGi20MHz;
            public bool ShortGi40MHz;
            public uint PrimaryChannel;
            public bool SecondaryChannelLower;

            //public uint MaxMcs;
            public List<double> Rates;

            //public static TypeNSettings Empty = new TypeNSettings() { Rates = new List<double>() };
            public TypeNSettings()
            {
                Rates = new List<double>();
            }

            public TypeNSettings(TypeNSettings settings)
            {
                Is40MHz = settings.Is40MHz;
                ShortGi20MHz = settings.ShortGi20MHz;
                ShortGi40MHz = settings.ShortGi40MHz;
                PrimaryChannel = settings.PrimaryChannel;
                SecondaryChannelLower = settings.SecondaryChannelLower;
                //MaxMcs = settings.MaxMcs;
                Rates = settings.Rates;
            }

            public override bool Equals(object obj)
            {
                if (obj is TypeNSettings)
                {
                    TypeNSettings set = (TypeNSettings)obj;
                    bool yes = set.Is40MHz == Is40MHz;
                    yes &= set.ShortGi20MHz == ShortGi20MHz;
                    yes &= set.ShortGi40MHz == ShortGi40MHz;
                    yes &= set.PrimaryChannel == PrimaryChannel;
                    yes &= set.SecondaryChannelLower == SecondaryChannelLower;
                    //Don't compare rates

                    return yes;
                }
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private class McsSet
        {
            //20MHz long GI
            private static readonly Dictionary<uint, float> LGiTable20 = new Dictionary<uint, float>
                                                                              {
                                                                {0, 6f},//6.5
                                                                {1, 13f},
                                                                {2, 19f}, //19.5
                                                                {3, 26f},
                                                                {4, 39f},
                                                                {5, 52f},
                                                                {6, 58f},
                                                                {7, 65f}
                                                            };

            //20MHz short GI
            private static readonly Dictionary<uint, float> SGiTable20 = new Dictionary<uint, float>
                                                                              {
                                                                {0, 7f}, //7.2
                                                                {1, 14f},//14.4
                                                                {2, 22f},//21.7
                                                                {3, 29f},//28.9
                                                                {4, 43f},//43.3
                                                                {5, 58f},//57.8
                                                                {6, 65f},
                                                                {7, 72f} //72.2
                                                            };

            //40MHz long GI
            private static readonly Dictionary<uint, float> LGiTable40 = new Dictionary<uint, float>
                                                                              {
                                                                {0, 13f}, //13.5
                                                                {1, 27f},
                                                                {2, 40f},//40.5
                                                                {3, 54f},
                                                                {4, 81f},
                                                                {5, 108f},
                                                                {6, 121f},
                                                                {7, 135f}
                                                            };

            //40MHz short GI
            private static readonly Dictionary<uint, float> SGiTable40 = new Dictionary<uint, float>
                                                                              {
                                                                {0, 15f},
                                                                {1, 30f},
                                                                {2, 45f},
                                                                {3, 60f},
                                                                {4, 90f},
                                                                {5, 120f},
                                                                {6, 135f},
                                                                {7, 150f}
                                                            };

            public static float GetSpeed(uint index, bool shortGi20MHz, bool shortGi40MHz, bool fortyMHz)
            {
                float output;

                if (index > 32) return 0f;
                int streams = 0;

                if (index >= 0 && index < 8)
                {
                    streams = 1;
                }
                else if (index >= 8 && index < 16)
                {
                    streams = 2;
                    index -= 8;
                }
                else if (index >= 16 && index < 24)
                {
                    streams = 3;
                    index -= 16;
                }
                else if (index >= 24 && index < 32)
                {
                    streams = 4;
                    index -= 24;
                }

                if (fortyMHz)
                {
                    if (shortGi40MHz)
                    {
                        output = SGiTable40[index];
                        output *= streams;
                    }
                    else
                    {
                        output = LGiTable40[index];
                        output *= streams;
                    }
                }
                else //20 MHz channel
                {
                    if (shortGi20MHz)
                    {
                        output = SGiTable20[index];
                        output *= streams;
                    }
                    else
                    {
                        output = LGiTable20[index];
                        output *= streams;
                    }
                }

                return output;
            }
        }

        #endregion Private Methods
    }
}