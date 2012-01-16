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
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace MetaGeek.Gps
{
    public class GpsController
    {
        #region Fields

        public int PortBaudrate = 4800;
        public int PortDataBits = 8;
        public Handshake PortHandshake = Handshake.None;
        public Parity PortParity = Parity.None;
        public StopBits PortStopBits = StopBits.One;

        // Thread properties
        private Thread _gpsThread;
        private readonly NmeaParser _nmea;

        // Serial port settings
        private SerialPort _port;
        readonly AutoResetEvent _terminate = new AutoResetEvent(false);
        private readonly WaitHandle[] _waitHandles;

        #endregion Fields

        #region Properties

        /*
        public int[] SatelliteIDs { get { return _nmea._satIDs; } }
        */
        public bool AllSatellitesLoaded
        {
            get { return _nmea.GetAllSatellitesLoaded(); }
        }

        public bool Enabled
        {
            get; private set;
        }

        public bool HasFix
        {
            get; private set;
        }

        public bool HasTalked
        {
            get; private set;
        }

        private int MaxTimeout
        {
            get; set;
        }

        /*
        public bool Connected { get { return _port != null && _port.IsOpen; } }
        */
        public GpsData MyGpsData
        {
            get; private set;
        }

        public string PortName
        {
            get; set;
        }

        public List<Satellite> Satellites
        {
            get; private set;
        }

        public int SatellitesVisible
        {
            get; private set;
        }

        // Time is set, but never used?
        private DateTime Time
        {
            get; set;
        }

        public bool TimedOut
        {
            get; private set;
        }

        #endregion Properties

        #region Events

        public event EventHandler<StringEventArgs> GpsError;

        public event EventHandler GpsLocationUpdated;

        public event EventHandler<StringEventArgs> GpsMessage;

        public event EventHandler GpsStatUpdated;

        public event EventHandler GpsTimeout;

        public event EventHandler GpsUpdated;

        #endregion Events

        #region Invoke Methods

        private void InvokeGpsError(string message)
        {
            if (null != GpsError)
            {
                GpsError(this, new StringEventArgs(message));
            }
        }

        private void InvokeGpsLocationUpdated()
        {
            if (null != GpsLocationUpdated)
            {
                GpsLocationUpdated(this, EventArgs.Empty);
            }
        }

        private void InvokeGpsMessage(string message)
        {
            if (null != GpsMessage)
            {
                GpsMessage(this, new StringEventArgs(message));
            }
        }

        private void InvokeGpsStatUpdated()
        {
            if (null != GpsStatUpdated)
            {
                GpsStatUpdated(this, EventArgs.Empty);
            }
        }

        private void InvokeGpsTimeout()
        {
            if (null != GpsTimeout)
            {
                TimedOut = true;
                GpsTimeout(this, EventArgs.Empty);
            }
        }

        private void InvokeGpsUpdated()
        {
            if (null != GpsUpdated)
            {
                GpsUpdated(this, EventArgs.Empty);
            }
        }

        #endregion Invoke Methods

        #region Constructors

        public GpsController()
        {
            _nmea = new NmeaParser();
            MyGpsData = new GpsData();

            //Set timeout for 45 sec.
            MaxTimeout = 90;
            _waitHandles = new WaitHandle[] { _terminate, };
            #if DEBUG
            _nmea.Validate = false;
            #endif
        }

        #endregion Constructors

        #region Public Methods

        /*
        public void Restart()
        {
            Stop();
            Start();
        }
        */
        public GpsData GetCurrentGpsData()
        {
            return new GpsData
                       {
                           Altitude = MyGpsData.Altitude,
                           Course = MyGpsData.Course,
                           DgpsAge = MyGpsData.DgpsAge,
                           Dgpsid = MyGpsData.Dgpsid,
                           FixType = MyGpsData.FixType,
                           GeoidSeperation = MyGpsData.GeoidSeperation,
                           Hdop = MyGpsData.Hdop,
                           Vdop = MyGpsData.Vdop,
                           Pdop = MyGpsData.Pdop,
                           Latitude = MyGpsData.Latitude,
                           Longitude = MyGpsData.Longitude,
                           MagVar = MyGpsData.MagVar,
                           SatellitesUsed = MyGpsData.SatellitesUsed,
                           Speed = MyGpsData.Speed,
                           SatelliteTime = MyGpsData.SatelliteTime
                       };
        }

        public void Start()
        {
            ClosePort();
            if (!OpenPort())
            {
                InvokeGpsError("Failed to open serial port!");
                return;
            }

            if(_gpsThread != null)
            {
                //Stop old thread
                _terminate.Set();
                if (!_gpsThread.Join(1000))
                {
                    _gpsThread.Abort();
                }
            }

            //Create new thread and start it
            _gpsThread = new Thread(UpdateGps);
            _gpsThread.Start();
            Enabled = true;
            InvokeGpsMessage("GPS enabled");
        }

        /*
        public void Start(SerialPort port)
        {
            if(port != null)_port = port;
            Start();
        }
        */
        /*
        public void Start(string portname,int baudrate,Handshake handshake)
        {
            PortName = portname;
            PortBaudrate = baudrate;
            PortHandshake = handshake;
            Start();
        }
        */
        /*
        public void Start(string portname, int baudrate, Parity parity, int databits, StopBits stopbits, Handshake handshake)
        {
            PortName = portname;
            PortBaudrate = baudrate;
            PortParity = parity;
            PortDataBits = databits;
            PortStopBits = stopbits;
            PortHandshake = handshake;
            Start();
        }
        */
        public void Stop()
        {
            TerminateThread();
        }

        #endregion Public Methods

        #region Private Methods

        private void ClosePort()
        {
            if(_port == null || !_port.IsOpen) return;

            try
            {
                _port.Close();
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }

            HasTalked = false;
        }

        private bool OpenPort()
        {
            try
            {
                _port = new SerialPort(PortName, PortBaudrate, PortParity, PortDataBits, PortStopBits)
                            {
                                Handshake = PortHandshake,
                                ReadTimeout = 500
                            };
                _port.Open();
                // Some GPSes require this (GPS2Blue)
               _port.DtrEnable = true;
               _port.RtsEnable = true;
                HasTalked = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void TerminateThread()
        {
            ClosePort();
            if (_gpsThread == null) return;
            _terminate.Set();
            if (!_gpsThread.Join(1000))
            {
                _gpsThread.Abort();
            }
            Enabled = false;
            TimedOut = false;
            InvokeGpsMessage("GPS disabled");
        }

        private void UpdateGps()
        {
            int timeoutCounter = 0;
            _terminate.Reset();

            //Infinite loop!
            while(true)
            {
                try
                {
                    int eventIdx = WaitHandle.WaitAny(_waitHandles, 100, false);
                    if (eventIdx == WaitHandle.WaitTimeout)
                    {
                        if (_port != null && _port.IsOpen)
                        {
                            string sentence = string.Empty;
                            try
                            {
                                //Console.WriteLine(_port.ReadExisting());
                                sentence = _port.ReadLine();
                                timeoutCounter = 0;
                                HasTalked = true;
                                TimedOut = false;
                            }
                            //TODO: Find out why ArgumentOutOfRangeException gets thrown and catch if necessary
                            catch (ArithmeticException)
                            {
                                // TODO: wtf??
                            }
                            catch (TimeoutException)
                            {
                                //TODO: make GPS keep listening and just notify that no GPS is found, check cables and make sure the GPS is powered on
                                // Give up after the maximum timeout is reached
                                // The read timeout is 500ms, so the number of timeouts to seconds is a ratio of 2 timeouts for 1 second
                                if (!HasTalked && MaxTimeout <= timeoutCounter)
                                {
                                    InvokeGpsTimeout();
                                    //Reset the timeout count
                                    //Stop();
                                    break;
                                }
                                timeoutCounter++;
                            }
                            catch (IOException)
                            {
                                ClosePort();
                                break;
                            }

                            //Process sentence
                            NmeaParser.SentenceType type = _nmea.Parse(sentence);

                            switch (type)
                            {
                                case NmeaParser.SentenceType.Gprmc:
                                    MyGpsData.Latitude = _nmea.Latitude;
                                    MyGpsData.Longitude = _nmea.Longitude;
                                    MyGpsData.Speed = _nmea.Speed;
                                    MyGpsData.SatelliteTime = _nmea.SatelliteTime;
                                    MyGpsData.Course = _nmea.Course;
                                    MyGpsData.MagVar = _nmea.MagVar;

                                    HasFix = _nmea.HasFix;
                                    Time = _nmea.Timestamp;

                                    InvokeGpsLocationUpdated();
                                    break;

                                case NmeaParser.SentenceType.Gpgsv:
                                    Satellites = _nmea.Satellites;
                                    SatellitesVisible = _nmea.SatelliteCount;
                                    InvokeGpsStatUpdated();
                                    break;

                                case NmeaParser.SentenceType.Gpgsa:
                                    MyGpsData.Pdop = _nmea.Pdop;
                                    MyGpsData.Hdop = _nmea.Hdop;
                                    MyGpsData.Vdop = _nmea.Vdop;
                                    MyGpsData.SatellitesUsed = _nmea.SatellitesUsed;

                                    InvokeGpsStatUpdated();
                                    break;

                                case NmeaParser.SentenceType.Gpgga:
                                    MyGpsData.Latitude = _nmea.Latitude;
                                    MyGpsData.Longitude = _nmea.Longitude;
                                    MyGpsData.Altitude = _nmea.Altitude;

                                    Time = _nmea.Timestamp;

                                    InvokeGpsLocationUpdated();
                                    break;

                                case NmeaParser.SentenceType.Gpvtg:
                                    MyGpsData.Speed = _nmea.Speed;
                                    break;

                                default:
                                    //gpsConnected = false;
                                    break;
                            }
                        }
                        else
                        {
                            // The port was previously closed due to being out of sync, reopen
                            Thread.Sleep(500);
                            ClosePort();
                            OpenPort();
                        }

                        InvokeGpsUpdated();
                    }
                    else //Terminate was signaled
                    {
                        break;
                    }
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
        }

        #endregion Private Methods
    }

    public class StringEventArgs : EventArgs
    {
        #region Properties

        public string Message
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public StringEventArgs(string message)
        {
            Message = message;
        }

        #endregion Constructors
    }
}