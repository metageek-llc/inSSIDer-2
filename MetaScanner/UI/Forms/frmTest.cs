using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace inSSIDer.UI.Forms
{
    public partial class frmTest : Form
    {
        #region Fields

        System.Timers.Timer t = new System.Timers.Timer(500);
        StringWriter tw;

        #endregion Fields

        #region Delegates

        delegate void timertick(object sender, EventArgs e);

        delegate void TimerTicked(object sender, System.Timers.ElapsedEventArgs e);

        #endregion Delegates

        #region Constructors

        public frmTest()
        {
            InitializeComponent();
            t.Elapsed += t_Elapsed;
        }

        #endregion Constructors

        #region Protected Methods

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            timer1.Stop();
            t.Stop();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Debug.AutoFlush = true;
            tw = new StringWriter();
            TraceListener tl = new TextWriterTraceListener(tw);
            Debug.Listeners.Add(tl);
            Debug.WriteLine("Debug output console enabled");

            t.Start();
        }

        #endregion Protected Methods

        #region Private Methods

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new TimerTicked(t_Elapsed), sender, e);
                }
                catch (ObjectDisposedException) { }
                catch (InvalidOperationException) { }
                return;
            }

            txtDebug.AppendText(tw.ToString());
            StringBuilder sb = tw.GetStringBuilder();
            //Clear it
            sb.Remove(0, sb.Length);
        }

        #endregion Private Methods
    }
}