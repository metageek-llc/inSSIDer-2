using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using inSSIDer.Localization;
using ManagedWifi;
using inSSIDer.Misc;

namespace inSSIDer.Scanning
{
    public class InterfaceManager
    {
        //Static instance
        public static readonly InterfaceManager Instance = new InterfaceManager();

        public event EventHandler<InterfaceNotificationEventsArgs> InterfaceRemoved;
        public event EventHandler<InterfaceNotificationEventsArgs> InterfaceAdded;

        private void InitXP()
        {
            
        }

        public void Init(out Exception error)
        {
            error = null;
            if(Utilities.IsXp()) return;
            //{
            //    InitXP();
            //    return;
            //}

            try
            {
                WlanClient = new WlanClient();
            }
            catch (Win32Exception exception)
            {
                error = exception;
                return;
            }
            catch (DllNotFoundException)
            {
                error = new Exception(Localizer.GetString("WlanapiNotFound"));
                return;
            }

            WlanClient.InterfaceArrivedEvent += WlanClient_InterfaceArrivedEvent;
            WlanClient.InterfaceRemovedEvent += WlanClient_InterfaceRemovedEvent;
        }

        private void WlanClient_InterfaceRemovedEvent(object sender, InterfaceNotificationEventsArgs e)
        {
            OnInterfaceRemoved(e.MyGuid);
        }

        private void WlanClient_InterfaceArrivedEvent(object sender, InterfaceNotificationEventsArgs e)
        {
            OnInterfaceAdded(e.MyGuid);
        }

        private void OnInterfaceRemoved(Guid id)
        {
            if (InterfaceRemoved == null) return;
            InterfaceRemoved(this,new InterfaceNotificationEventsArgs(id));
        }

        private void OnInterfaceAdded(Guid id)
        {
            if (InterfaceAdded == null) return;
            InterfaceAdded(this, new InterfaceNotificationEventsArgs(id));
        }

        public NetworkInterface[] Interfaces
        {
            get
            {
                if(Utilities.IsXp())
                {
                    //Return all interfaces that are NOT loopback
                    return NetworkInterface.GetAllNetworkInterfaces().Where(net => net.NetworkInterfaceType != NetworkInterfaceType.Loopback).ToArray();
                }

                return WlanClient.Interfaces.ToList().ConvertAll(wl => wl.NetworkInterface).ToArray();
            }
        }

        /// <summary>
        /// Gets the Wlan Client for the manager
        /// </summary>
        /// <remarks>This is null on Windows XP</remarks>
        public WlanClient WlanClient { get; private set; }
    }
}
