using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using inSSIDer.Properties;

namespace inSSIDer.Misc
{
    public class SignalBars
    {
        #region Public Methods

        public static Image GetImage(int rssi, bool secure)
        {
            if (rssi >= -54)
            {
                return secure ? Resources.Signal5E : Resources.Signal5;
            }
            else if(rssi >= -59)
            {
                return secure ? Resources.Signal4E : Resources.Signal4;
            }
            else if (rssi >= -69)
            {
                return secure ? Resources.Signal3E : Resources.Signal3;
            }
            else if (rssi >= -79)
            {
                return secure ? Resources.Signal2E : Resources.Signal2;
            }
            else if (rssi >= -89)
            {
                return secure ? Resources.Signal1E : Resources.Signal1;
            }
            else
            {
                return secure ? Resources.Signal0E : Resources.Signal0;
            }
        }

        #endregion Public Methods
    }
}