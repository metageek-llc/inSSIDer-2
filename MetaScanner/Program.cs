//#define LOG
using System;
using System.Windows.Forms;
using inSSIDer.Misc;
using inSSIDer.Scanning;
using inSSIDer.UI.Forms;
using inSSIDer.UI.Mini;
using inSSIDer.UnhandledException;
using MetaGeek.Utils;
using inSSIDer.Localization;
using inSSIDer.Properties;
using System.Net.NetworkInformation;

namespace inSSIDer
{
    static class Program
    {
        //Mode of switch
        public static Utilities.SwitchMode Switching = Utilities.SwitchMode.None;
        public static bool WasScanning;

        static void InitializeExceptionHandler(UnhandledExceptionDlg exDlg)
        {
            exDlg.UserPrefChecked = false;

            // Add handling of OnShowErrorReport.
            // If you skip this then link to report details won't be showing.
            exDlg.OnShowErrorReportClick += delegate(object sender, SendExceptionClickEventArgs ar)
            {
                MessageBox.Show("[Message]\r\n" + ar.UnhandledException.Message + "\r\n\r\n"
                     + "[Version]\r\n" + Application.ProductVersion + "\r\n\r\n"
                     + "[WinVer]\r\n" + Environment.OSVersion.VersionString + "\r\n\r\n"
                     + "[Platform]\r\n" + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "\r\n\r\n"
                     + "[StackTrace]\r\n" + PathScrubber.Scrub(ar.UnhandledException.ToString()) + "\r\n\r\n");
            };

            // Add handling of OnCopytoClipbooard
            // if you skip, the button is disabled
            exDlg.OnCopyToClipboardClick += delegate(object sender, SendExceptionClickEventArgs ar)
            {
                try
                {
                    // TUT: needs to be STA apt. thread for accessing clipboard
                    System.Threading.Thread clipThread = new System.Threading.Thread(delegate()
                    {
                        String body = Localizer.GetString("ErrorDescription");
                        body += "\r\n\r\n[Description]\r\n\r\n\r\n\r\n";
                        body += "[Message]\r\n" + ar.UnhandledException.Message + "\r\n\r\n";
                        body += "[Version]\r\n" + Application.ProductVersion + "\r\n\r\n";
                        body += "[WinVer]\r\n" + Environment.OSVersion.VersionString + "\r\n\r\n";
                        body += "[Platform]\r\n" + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "\r\n\r\n";
                        body += "[StackTrace]\r\n" + PathScrubber.Scrub(ar.UnhandledException.ToString()) + "\r\n\r\n";

                        Clipboard.SetText(body);
                    });
                    clipThread.SetApartmentState(System.Threading.ApartmentState.STA);
                    clipThread.Start();
                }
                catch (Exception)
                {
                    // Ignore
                }
            };

            // Implement your sending protocol here. You can use any information from System.Exception
            exDlg.OnSendExceptionClick += delegate(object sender, SendExceptionClickEventArgs ar)
            {
                // User clicked on "Send Error Report" button:
                if (ar.SendExceptionDetails)
                {
                    String body = Localizer.GetString("ErrorDescription");
                    body += "\r\n\r\n[Description]\r\n\r\n\r\n\r\n";
                    body += "[Message]\r\n" + ar.UnhandledException.Message + "\r\n\r\n";
                    body += "[Version]\r\n" + Application.ProductVersion + "\r\n\r\n";
                    body += "[WinVer]\r\n" + Environment.OSVersion.VersionString + "\r\n\r\n";
                    body += "[Platform]\r\n" + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "\r\n\r\n";
                    body += "[StackTrace]\r\n" + PathScrubber.Scrub(ar.UnhandledException.ToString()) + "\r\n\r\n";

                    MapiMailMessage message = new MapiMailMessage(@"inSSIDer 2 Error Report", body);
                    message.Recipients.Add("error.reports@metageek.net");
                    message.ShowDialog(true);
                }

                Application.Exit();
            };
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //TODO: Make conmmand line option to enable logging on debug builds. Like /log
#if DEBUG //&& LOG
            Log.Start();
#endif
            // Create new instance of UnhandledExceptionDlg:
            // NOTE: this hooks up the exception handler functions 
            UnhandledExceptionDlg exDlg = new UnhandledExceptionDlg();
            InitializeExceptionHandler(exDlg);

            //Check for config system condition here
            if(!Settings.Default.CheckSettingsSystem())
            {
                //The settings system is broken, notify and exit
                MessageBox.Show(
                    Localizer.GetString("ConfigSystemError"),
                    "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initalize the scanner object before passing it to any interface
            ScanController scanner = new ScanController();
            Exception error;

            scanner.Initalize(out error);
            if (error != null)
            {
                //An error!
                scanner.Dispose();
                scanner = null;
                //So the error handler will catch it
                //throw ex;

                //Log it
                Log.WriteLine(string.Format("Exception message:\r\n\r\n{0}\r\n\r\nStack trace:\r\n{1}", error.Message, error.StackTrace));

                if (error is System.ComponentModel.Win32Exception)
                {
                    //The wireless system failed
                    if (Utilities.IsXp())
                    {
                        MessageBox.Show(Localizer.GetString("WlanServiceNotFoundXP"), "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MessageBox.Show(Localizer.GetString("WlanServiceNotFound7"), "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    //Any other exceptions
                    MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Hand);
                }
            }

            if (scanner == null) return;

            //Start the scanning if it was last time and we have the last interface
            //Otherwise, if we only have the interface, but not scanning, just set the interface selector to the last interface.
            //TODO: Actually have the auto-start as an option. :)

            NetworkInterface netInterface = InterfaceManager.Instance.LastInterface;
            if (netInterface != null)
            {
                //Set the interface
                scanner.Interface = netInterface;
                if (Settings.Default.scanLastEnabled)
                    scanner.StartScanning();
            }

            //The main form will run unless mini is specified
            IScannerUi form;

            if(Settings.Default.lastMini)
            {
                form = new FormMini();
                SettingsMgr.ApplyMiniFormSettings((Form)form);
            }
            else
            {
                form = new FormMain();
                SettingsMgr.ApplyMainFormSettings((Form)form);
            }

            //Apply settings now 
            SettingsMgr.ApplyGpsSettings(scanner.GpsControl);
            
            //Start GPS if it was started last time


            do
            {
                //Check for switching
                switch (Switching)
                {
                    case Utilities.SwitchMode.None:
                        //We're not switching, close program
                        break;
                    case Utilities.SwitchMode.ToMain:
                        //We're switching to the main form
                        form = new FormMain();
                        SettingsMgr.ApplyMainFormSettings((Form)form);
                        break;
                    case Utilities.SwitchMode.ToMini:
                        //We're switching to the mini form
                        form = new FormMini();
                        SettingsMgr.ApplyMiniFormSettings((Form)form);
                        break;
                }
                //If we've switched, we don't need to get stuck in a loop
                Switching = Utilities.SwitchMode.None;

                form.Initalize(ref scanner);
                try
                {
                    Application.Run(form as Form);
                }
                catch (ObjectDisposedException)
                {

                }

            } while (Switching != Utilities.SwitchMode.None);

            Settings.Default.lastMini = form.GetType() == typeof(FormMini);

            //GPS enabled setting
            Settings.Default.gpsEnabled = scanner.GpsControl.Enabled;

            //Save settings before exit
            Settings.Default.Save();

            //Dispose the scanner, we're done with it
            scanner.Dispose();
        }
    }
}
