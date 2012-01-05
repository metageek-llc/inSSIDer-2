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
using System.Windows.Forms;

namespace inSSIDer.UI.Controls
{
    public class GripSplitContainer : SplitContainer
    {
        #region Properties

        public override bool Focused
        {
            get
            {
                return false;
            }
        }

        #endregion Properties

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int x = (SplitterRectangle.Width - Properties.Resources.longGripOff.Width) / 2;
            e.Graphics.DrawImageUnscaled(Properties.Resources.longGripOff, x, SplitterRectangle.Top);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Invalidate();
        }

        #endregion Protected Methods
    }
}