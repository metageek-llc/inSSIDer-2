using System;
using System.Drawing;
using System.Windows.Forms;

namespace inSSIDer.UI.Controls
{
    public class TabHeaderRightClickedEventArgs : EventArgs
    {
        #region Properties

        public Point ItsClickedLocation
        {
            get; private set;
        }

        public TabPage ItsTabPage
        {
            get; private set;
        }

        #endregion Properties

        #region Constructors

        public TabHeaderRightClickedEventArgs(TabPage page, Point location)
        {
            ItsTabPage = page;
            ItsClickedLocation = location;
        }

        #endregion Constructors
    }
}