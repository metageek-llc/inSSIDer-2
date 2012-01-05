#region Header

//By:	Andrew Baker
//Email http://www.vbusers.com/email/sendmail.asp?group=csharpcode&threadid=71&postid=1
//Date:	Friday, March 10, 2006

#endregion Header

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace inSSIDer.UnhandledException
{
    /// <summary>
    /// Represents an email message to be sent through MAPI.
    /// </summary>
    public class MapiMailMessage
    {
        #region Fields

        private string _body;
        private readonly ArrayList _files;
        private readonly RecipientCollection _recipientCollection;
        private string _subject;

        #endregion Fields

        #region Enumerations

        /// <summary>
        /// Specifies the valid RecipientTypes for a Recipient.
        /// </summary>
        public enum RecipientType
        {
            /// <summary>
            /// Recipient will be in the TO list.
            /// </summary>
            To = 1,

            /// <summary>
            /// Recipient will be in the CC list.
            /// </summary>
            Cc = 2,

            /// <summary>
            /// Recipient will be in the BCC list.
            /// </summary>
            Bcc = 3
        }

        #endregion Enumerations

        #region Properties

        /// <summary>
        /// Gets or sets the body of this mail message.
        /// </summary>
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        /// <summary>
        /// Gets the recipient list for this mail message.
        /// </summary>
        public RecipientCollection Recipients
        {
            get { return _recipientCollection; }
        }

        /// <summary>
        /// Gets or sets the subject of this mail message.
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        #endregion Properties

        #region Event Fields

        private readonly ManualResetEvent _manualResetEvent;

        #endregion Event Fields

        #region Constructors

        /// <summary>
        /// Creates a blank mail message.
        /// </summary>
        private MapiMailMessage()
        {
            _files = new ArrayList();
            _recipientCollection = new RecipientCollection();
            _manualResetEvent = new ManualResetEvent(false);
        }

        /// <summary>
        /// Creates a new mail message with the specified subject and body.
        /// </summary>
        public MapiMailMessage(string subject, string body)
            : this()
        {
            _subject = subject;
            _body = body;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Displays the mail message dialog asynchronously.
        /// </summary>
        public void ShowDialog(bool sync)
        {
            if (!sync)
            {
            // Create the mail message in an STA thread
            Thread t = new Thread(new ThreadStart(_ShowMail));
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            // only return when the new thread has built it's interop representation
            _manualResetEvent.WaitOne();
            _manualResetEvent.Reset();
            }
            else
            {
                _ShowMail(true);
            }
        }

        #endregion Public Methods

        #region Private Methods

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private class MapiFileDescriptor
        {
        }

        /// <summary>
        /// Internal class for calling MAPI APIs
        /// </summary>
        internal class MapiHelperInterop
        {
            /// <summary>
            /// Private constructor.
            /// </summary>
            private MapiHelperInterop()
            {
                // Intenationally blank
            }

            public const int MapiLogonUi = 0x1;

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class MapiMessage
            {
                public int Reserved = 0;
                public string Subject = null;
                public string NoteText = null;
                public string MessageType = null;
                public string DateReceived = null;
                public string ConversationID = null;
                public int Flags = 0;
                public IntPtr Originator = IntPtr.Zero;
                public int RecipientCount = 0;
                public IntPtr Recipients = IntPtr.Zero;
                public int FileCount = 0;
                public IntPtr Files = IntPtr.Zero;
            }

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class MapiRecipDesc
            {
                public int Reserved = 0;
                public int RecipientClass = 0;
                public string Name = null;
                public string Address = null;
                public int eIDSize = 0;
                public IntPtr EntryID = IntPtr.Zero;
            }

            [DllImport("MAPI32.DLL")]
            public static extern int MAPISendMail(IntPtr session, IntPtr hwnd, MapiMessage message, int flg, int rsv);
        }

        /// <summary>
        /// Allocates the file attachments
        /// </summary>
        /// <param name="fileCount"></param>
        /// <returns></returns>
        private IntPtr _AllocAttachments(out int fileCount)
        {
            fileCount = 0;
            if (_files == null)
            {
                return IntPtr.Zero;
            }
            if ((_files.Count <= 0) || (_files.Count > 5))
            {
                return IntPtr.Zero;
            }

            Type atype = typeof(MapiFileDescriptor);
            int asize = Marshal.SizeOf(atype);
            IntPtr ptra = Marshal.AllocHGlobal(_files.Count * asize);

            MapiFileDescriptor mfd = new MapiFileDescriptor();
            int runptr = (int)ptra;
            foreach (object t in _files)
            {
                string path = t as string;
                Path.GetFileName(path);
                Marshal.StructureToPtr(mfd, (IntPtr)runptr, false);
                runptr += asize;
            }

            fileCount = _files.Count;
            return ptra;
        }

        /// <summary>
        /// Deallocates the files in a message.
        /// </summary>
        /// <param name="message">The message to deallocate the files from.</param>
        private void _DeallocFiles(MapiHelperInterop.MapiMessage message)
        {
            if (message.Files != IntPtr.Zero)
            {
                Type fileDescType = typeof(MapiFileDescriptor);
                int fsize = Marshal.SizeOf(fileDescType);

                // Get the ptr to the files
                int runptr = (int)message.Files;
                // Release each file
                for (int i = 0; i < message.FileCount; i++)
                {
                    Marshal.DestroyStructure((IntPtr)runptr, fileDescType);
                    runptr += fsize;
                }
                // Release the file
                Marshal.FreeHGlobal(message.Files);
            }
        }

        /// <summary>
        /// Logs any Mapi errors.
        /// </summary>
        private void _LogErrorMapi(int errorCode)
        {
            const int mapiUserAbort = 1;
            const int mapiEFailure = 2;
            const int mapiELoginFailure = 3;
            const int mapiEDiskFull = 4;
            const int mapiEInsufficientMemory = 5;
            const int mapiEBlkTooSmall = 6;
            const int mapiETooManySessions = 8;
            const int mapiETooManyFiles = 9;
            const int mapiETooManyRecipients = 10;
            const int mapiEAttachmentNotFound = 11;
            const int mapiEAttachmentOpenFailure = 12;
            const int mapiEAttachmentWriteFailure = 13;
            const int mapiEUnknownRecipient = 14;
            const int mapiEBadReciptype = 15;
            const int mapiENoMessages = 16;
            const int mapiEInvalidMessage = 17;
            const int mapiETextTooLarge = 18;
            const int mapiEInvalidSession = 19;
            const int mapiETypeNotSupported = 20;
            const int mapiEAmbiguousRecipient = 21;
            const int mapiEMessageInUse = 22;
            const int mapiENetworkFailure = 23;
            const int mapiEInvalidEditfields = 24;
            const int mapiEInvalidRecips = 25;
            const int mapiENotSupported = 26;
            const int mapiENoLibrary = 999;
            const int mapiEInvalidParameter = 998;

            string error = string.Empty;
            switch (errorCode)
            {
                case mapiUserAbort:
                    error = "User Aborted.";
                    break;
                case mapiEFailure:
                    error = "MAPI Failure.";
                    break;
                case mapiELoginFailure:
                    error = "Login Failure.";
                    break;
                case mapiEDiskFull:
                    error = "MAPI Disk full.";
                    break;
                case mapiEInsufficientMemory:
                    error = "MAPI Insufficient memory.";
                    break;
                case mapiEBlkTooSmall:
                    error = "MAPI Block too small.";
                    break;
                case mapiETooManySessions:
                    error = "MAPI Too many sessions.";
                    break;
                case mapiETooManyFiles:
                    error = "MAPI too many files.";
                    break;
                case mapiETooManyRecipients:
                    error = "MAPI too many recipients.";
                    break;
                case mapiEAttachmentNotFound:
                    error = "MAPI Attachment not found.";
                    break;
                case mapiEAttachmentOpenFailure:
                    error = "MAPI Attachment open failure.";
                    break;
                case mapiEAttachmentWriteFailure:
                    error = "MAPI Attachment Write Failure.";
                    break;
                case mapiEUnknownRecipient:
                    error = "MAPI Unknown recipient.";
                    break;
                case mapiEBadReciptype:
                    error = "MAPI Bad recipient type.";
                    break;
                case mapiENoMessages:
                    error = "MAPI No messages.";
                    break;
                case mapiEInvalidMessage:
                    error = "MAPI Invalid message.";
                    break;
                case mapiETextTooLarge:
                    error = "MAPI Text too large.";
                    break;
                case mapiEInvalidSession:
                    error = "MAPI Invalid session.";
                    break;
                case mapiETypeNotSupported:
                    error = "MAPI Type not supported.";
                    break;
                case mapiEAmbiguousRecipient:
                    error = "MAPI Ambiguous recipient.";
                    break;
                case mapiEMessageInUse:
                    error = "MAPI Message in use.";
                    break;
                case mapiENetworkFailure:
                    error = "MAPI Network failure.";
                    break;
                case mapiEInvalidEditfields:
                    error = "MAPI Invalid edit fields.";
                    break;
                case mapiEInvalidRecips:
                    error = "MAPI Invalid Recipients.";
                    break;
                case mapiENotSupported:
                    error = "MAPI Not supported.";
                    break;
                case mapiENoLibrary:
                    error = "MAPI No Library.";
                    break;
                case mapiEInvalidParameter:
                    error = "MAPI Invalid parameter.";
                    break;
            }

            Debug.WriteLine("Error sending MAPI Email. Error: " + error + " (code = " + errorCode + ").");
        }

        /// <summary>
        /// Sends the mail message.
        /// </summary>
        private void _ShowMail(bool sync)
        {
            MapiHelperInterop.MapiMessage message = new MapiHelperInterop.MapiMessage();

            using (RecipientCollection.InteropRecipientCollection interopRecipients
                        = _recipientCollection.GetInteropRepresentation())
            {

                message.Subject = _subject;
                message.NoteText = _body;

                message.Recipients = interopRecipients.Handle;
                message.RecipientCount = _recipientCollection.Count;

                // Check if we need to add attachments
                if (_files.Count > 0)
                {
                    // Add attachments
                    message.Files = _AllocAttachments(out message.FileCount);
                }

                if (!sync)
                {
                // Signal the creating thread (make the remaining code async)
                _manualResetEvent.Set();
                }

                const int mapiDialog = 0x8;
                //const int MAPI_LOGON_UI = 0x1;
                const int successSuccess = 0;
                int error = MapiHelperInterop.MAPISendMail(IntPtr.Zero, IntPtr.Zero, message, mapiDialog, 0);

                if (_files.Count > 0)
                {
                    // Deallocate the files
                    _DeallocFiles(message);
                }

                // Check for error
                if (error != successSuccess)
                {
                    _LogErrorMapi(error);
                }
            }
        }

        /// <summary>
        /// Sends the mail message.
        /// </summary>
        private void _ShowMail()
        {
            _ShowMail(false);
        }

        #endregion Private Methods
    }

    /// <summary>
    /// Represents a Recipient for a MapiMailMessage.
    /// </summary>
    public class Recipient
    {
        #region Fields

        /// <summary>
        /// The email address of this recipient.
        /// </summary>
        private readonly string Address;

        /// <summary>
        /// The display name of this recipient.
        /// </summary>
        private string DisplayName = null;

        /// <summary>
        /// How the recipient will receive this message (To, CC, BCC).
        /// </summary>
        public readonly MapiMailMessage.RecipientType RecipientType = MapiMailMessage.RecipientType.To;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates a new recipient with the specified address.
        /// </summary>
        public Recipient(string address)
        {
            Address = address;
        }

        /// <summary>
        /// Creates a new recipient with the specified address and display name.
        /// </summary>
        public Recipient(string address, string displayName)
        {
            Address = address;
            DisplayName = displayName;
        }

        /// <summary>
        /// Creates a new recipient with the specified address and recipient type.
        /// </summary>
        public Recipient(string address, MapiMailMessage.RecipientType recipientType)
        {
            Address = address;
            RecipientType = recipientType;
        }

        /// <summary>
        /// Creates a new recipient with the specified address, display name and recipient type.
        /// </summary>
        public Recipient(string address, string displayName, MapiMailMessage.RecipientType recipientType)
        {
            Address = address;
            DisplayName = displayName;
            RecipientType = recipientType;
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Returns an interop representation of a recepient.
        /// </summary>
        /// <returns></returns>
        internal MapiMailMessage.MapiHelperInterop.MapiRecipDesc GetInteropRepresentation()
        {
            MapiMailMessage.MapiHelperInterop.MapiRecipDesc interop = new MapiMailMessage.MapiHelperInterop.MapiRecipDesc();

            if (DisplayName == null)
            {
                interop.Name = Address;
            }
            else
            {
                interop.Name = DisplayName;
                interop.Address = Address;
            }

            interop.RecipientClass = (int)RecipientType;

            return interop;
        }

        #endregion Private Methods
    }

    /// <summary>
    /// Represents a colleciton of recipients for a mail message.
    /// </summary>
    public class RecipientCollection : CollectionBase
    {
        #region Properties

        /// <summary>
        /// Returns the recipient stored in this collection at the specified index.
        /// </summary>
        public Recipient this[int index]
        {
            get
            {
                return (Recipient)List[index];
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Adds a new recipient with the specified address to this collection.
        /// </summary>
        public void Add(string address)
        {
            this.Add(new Recipient(address));
        }

        /// <summary>
        /// Adds a new recipient with the specified address and display name to this collection.
        /// </summary>
        public void Add(string address, string displayName)
        {
            this.Add(new Recipient(address, displayName));
        }

        /// <summary>
        /// Adds a new recipient with the specified address and recipient type to this collection.
        /// </summary>
        public void Add(string address, MapiMailMessage.RecipientType recipientType)
        {
            this.Add(new Recipient(address, recipientType));
        }

        /// <summary>
        /// Adds a new recipient with the specified address, display name and recipient type to this collection.
        /// </summary>
        public void Add(string address, string displayName, MapiMailMessage.RecipientType recipientType)
        {
            this.Add(new Recipient(address, displayName, recipientType));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Adds the specified recipient to this collection.
        /// </summary>
        private void Add(Recipient value)
        {
            List.Add(value);
        }

        internal InteropRecipientCollection GetInteropRepresentation()
        {
            return new InteropRecipientCollection(this);
        }

        /// <summary>
        /// Struct which contains an interop representation of a colleciton of recipients.
        /// </summary>
        internal struct InteropRecipientCollection : IDisposable
        {
            private IntPtr _handle;
            private int _count;

            /// <summary>
            /// Default constructor for creating InteropRecipientCollection.
            /// </summary>
            /// <param name="outer"></param>
            public InteropRecipientCollection(RecipientCollection outer)
            {
                _count = outer.Count;

                if (_count == 0)
                {
                    _handle = IntPtr.Zero;
                    return;
                }

                // allocate enough memory to hold all recipients
                int size = Marshal.SizeOf(typeof(MapiMailMessage.MapiHelperInterop.MapiRecipDesc));
                _handle = Marshal.AllocHGlobal(_count * size);

                // place all interop recipients into the memory just allocated
                int ptr = (int)_handle;
                foreach (Recipient native in outer)
                {
                    MapiMailMessage.MapiHelperInterop.MapiRecipDesc interop = native.GetInteropRepresentation();

                    // stick it in the memory block
                    Marshal.StructureToPtr(interop, (IntPtr)ptr, false);
                    ptr += size;
                }
            }

            public IntPtr Handle
            {
                get { return _handle; }
            }

            /// <summary>
            /// Disposes of resources.
            /// </summary>
            public void Dispose()
            {
                if (_handle != IntPtr.Zero)
                {
                    Type type = typeof(MapiMailMessage.MapiHelperInterop.MapiRecipDesc);
                    int size = Marshal.SizeOf(type);

                    // destroy all the structures in the memory area
                    int ptr = (int)_handle;
                    for (int i = 0; i < _count; i++)
                    {
                        Marshal.DestroyStructure((IntPtr)ptr, type);
                        ptr += size;
                    }

                    // free the memory
                    Marshal.FreeHGlobal(_handle);

                    _handle = IntPtr.Zero;
                    _count = 0;
                }
            }
        }

        #endregion Private Methods
    }
}