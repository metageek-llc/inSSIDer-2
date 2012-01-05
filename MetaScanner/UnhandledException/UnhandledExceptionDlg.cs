#region Header

/*
 *                  UnhandledExceptionDlg Class v. 1.1
 *
 *                      Copyright (c)2006 Vitaly Zayko
 *
 * History:
 * September 26, 2006 - Added "ThreadException" handler, "SetUnhandledExceptionMode", OnShowErrorReport event
 *                      and updated the Demo and code comments;
 * August 29, 2006 - Updated information about Microsoft Windows Error Reporting service and its link;
 * July 18, 2006 - Initial release.
 *
 */
/* More info on MSDN:
 * http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
 * http://msdn2.microsoft.com/en-us/library/system.windows.forms.application.threadexception.aspx
 * http://msdn2.microsoft.com/en-us/library/system.appdomain.unhandledexception.aspx
 * http://msdn2.microsoft.com/en-us/library/system.windows.forms.unhandledexceptionmode.aspx
 */

#endregion Header

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using inSSIDer.UnhandledException;

namespace MetaGeek.Utils
{
    /// <summary>
    /// Class for catching unhandled exception with UI dialog.
    /// </summary>
    public class UnhandledExceptionDlg
    {
        #region Fields

        private bool _userPrefChecked = false;

        //private bool _userPrefVisible = false;
        private string _userPrefText = string.Empty;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Set to true if you want to restart your App after falure
        /// </summary>
        public bool UserPrefChecked
        {
            get { return _userPrefChecked; }
            set { _userPrefChecked = value; }
        }

        #endregion Properties

        #region Events

        public event SendExceptionClickHandler OnCopyToClipboardClick;

        //public delegate void ShowErrorReportHandler(object sender, System.EventArgs args);
        /// <summary>
        /// Occurs when user clicks on "Send Error report" button
        /// </summary>
        public event SendExceptionClickHandler OnSendExceptionClick;

        /// <summary>
        /// Occurs when user clicks on "click here" link lable to get data that will be send
        /// </summary>
        public event SendExceptionClickHandler OnShowErrorReportClick;

        #endregion Events

        #region Delegates

        public delegate void SendExceptionClickHandler(object sender, SendExceptionClickEventArgs args);

        #endregion Delegates

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UnhandledExceptionDlg()
        {
            // Add the event handler for handling UI thread exceptions to the event:
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionFunction);

            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler:
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event:
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionFunction);
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Raise Exception Dialog box for both UI and non-UI Unhandled Exceptions
        /// </summary>
        /// <param name="e">Catched exception</param>
        private void ShowUnhandledExceptionDlg(Exception e)
        {
            Exception unhandledException = e;

            if(unhandledException == null)
                unhandledException = new Exception("Unknown unhandled exception occurred!");

            UnhandledExDlgForm exDlgForm = new UnhandledExDlgForm();
            try
            {
                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                exDlgForm.Text = appName;
                exDlgForm.labelTitle.Text = String.Format(exDlgForm.labelTitle.Text, appName);

                // Do not show link label if OnShowErrorReport is not handled
                exDlgForm.labelLinkTitle.Visible = (OnShowErrorReportClick != null);
                exDlgForm.linkLabelData.Visible = (OnShowErrorReportClick != null);

                // Disable the Button if OnSendExceptionClick event is not handled
                exDlgForm.buttonSend.Enabled = (OnSendExceptionClick != null);

                // Disable the Button if OnCopyToClipboardClick event is not handled
                exDlgForm.buttonCopy.Enabled = (OnCopyToClipboardClick != null);

                // Handle clicks on report link label
                exDlgForm.linkLabelData.LinkClicked += delegate(object o, LinkLabelLinkClickedEventArgs ev)
                {
                    if(OnShowErrorReportClick != null)
                    {
                        SendExceptionClickEventArgs ar = new SendExceptionClickEventArgs((bool) true, unhandledException);
                        OnShowErrorReportClick(this, ar);
                    }
                };

                exDlgForm.buttonCopy.Click += delegate(object o, EventArgs ev)
                {
                    if (OnCopyToClipboardClick != null)
                    {
                        SendExceptionClickEventArgs ar = new SendExceptionClickEventArgs((bool) true, unhandledException);
                        OnCopyToClipboardClick(this, ar);
                    }
                    exDlgForm.buttonCopy.Enabled = false;
                };

                // Show the Dialog box:
                bool sendDetails = (exDlgForm.ShowDialog() == System.Windows.Forms.DialogResult.Yes);

                if(OnSendExceptionClick != null)
                {
                    SendExceptionClickEventArgs ar = new SendExceptionClickEventArgs(sendDetails, unhandledException);
                    OnSendExceptionClick(this, ar);
                }
            }
            finally
            {
                exDlgForm.Dispose();
            }
        }

        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadExceptionFunction(Object sender, ThreadExceptionEventArgs e)
        {
            // Suppress the Dialog in Debug mode:
            #if !DEBUG
            //Settings.Default.UsesBeforeUpdateReminder = 0;
            //Settings.Default.Save();

            ShowUnhandledExceptionDlg(e.Exception);
            #endif
        }

        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="args">Passing arguments: original exception etc.</param>
        private void UnhandledExceptionFunction(Object sender, UnhandledExceptionEventArgs args)
        {
            // Suppress the Dialog in Debug mode:
            #if !DEBUG
            //Settings.Default.UsesBeforeUpdateReminder = 0;
            //Settings.Default.Save();

            ShowUnhandledExceptionDlg((Exception)args.ExceptionObject);
            #endif
        }

        #endregion Private Methods
    }
}