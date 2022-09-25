using PcAudioTest;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class PCGameDebugForm : Form
    {
        private readonly IntPtr _bufferReadyEvent;
        private readonly IntPtr _dataReadyEvent;
        private readonly IntPtr _mapping;
        private readonly IntPtr _file;
        private const int WaitTimeout = 500;
        private const uint WAIT_OBJECT_0 = 0;
        private Thread _reader;

        //-------------------------------------------------------------------------------------------------------------------------------
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetEvent(IntPtr hEvent);
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, FileMapProtection flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint WaitForSingleObject(IntPtr handle, uint milliseconds);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, FileMapAccess dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        //-------------------------------------------------------------------------------------------------------------------------------
        [Flags]
        private enum FileMapAccess
        {
            FileMapRead = 0x0004,
        }

        [Flags]
        private enum FileMapProtection
        {
            PageReadWrite = 0x04,
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCGameDebugForm()
        {
            InitializeComponent();

            _bufferReadyEvent = CreateEvent(IntPtr.Zero, false, false, "DBWIN_BUFFER_READY");
            if (_bufferReadyEvent == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            _dataReadyEvent = CreateEvent(IntPtr.Zero, false, false, "DBWIN_DATA_READY");
            if (_dataReadyEvent == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            _mapping = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, FileMapProtection.PageReadWrite, 0, 4096, "DBWIN_BUFFER");
            if (_mapping == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }

            _file = MapViewOfFile(_mapping, FileMapAccess.FileMapRead, 0, 0, 1024);
            if (_file == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_TestSfxDebug_Shown(object sender, EventArgs e)
        {
            _reader = new Thread(Read)
            {
                IsBackground = true
            };
            _reader.Start();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Read()
        {
            try
            {
                while (true)
                {
                    SetEvent(_bufferReadyEvent);

                    if (chkPauseDebug.Checked)
                    {
                        continue;
                    }

                    uint wait = WaitForSingleObject(_dataReadyEvent, WaitTimeout);
                    if (wait == WAIT_OBJECT_0) // we don't care about other return values
                    {
                        int pid = Marshal.ReadInt32(_file);
                        string text = Marshal.PtrToStringAnsi(new IntPtr(_file.ToInt32() + Marshal.SizeOf(typeof(int)))).TrimEnd(null);
                        if (string.IsNullOrEmpty(text))
                        {
                            continue;
                        }

                        //Add Only Sphinx Data
                        if (pid == SfxTestMethods.GetGamePID() && IsHandleCreated)
                        {
                            txtDebugData.Invoke((MethodInvoker)delegate
                            {
                                txtDebugData.AppendText(string.Format("[{0}] -> {1}", pid, text + Environment.NewLine));
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDebugData.Clear();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
