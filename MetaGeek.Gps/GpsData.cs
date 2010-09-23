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

namespace MetaGeek.Gps
{
    public class GpsData
    {
        public double Altitude;
        public double Latitude;
        public double Longitude;
        public double Hdop;
        public double Vdop;
        public double Pdop;
        public double Speed;
        public double Course;
        public double MagVar;
        public double DgpsAge;
        public double Dgpsid;
        public double GeoidSeperation;
        public string FixType = string.Empty;
        public int SatellitesUsed;
        public DateTime SatelliteTime = DateTime.MinValue;

        //Empty
        public static readonly GpsData Empty = new GpsData();
    }
}
