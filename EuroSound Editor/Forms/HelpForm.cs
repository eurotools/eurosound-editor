using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EuroSound_Editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class HelpForm : Form
    {
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetConnectedState(out int Description, int dwReserved);

        //-------------------------------------------------------------------------------------------------------------------------------
        public HelpForm()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void HelpForm_Load(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "SystemFiles", "Version.txt");
            if (File.Exists(filePath))
            {
                lblCurrentVersion.Text = string.Format("Version: {0}", TextFiles.ReadFileVersion(filePath));
            }
        }

        //*===============================================================================================
        //* FORM BUTTONS
        //*===============================================================================================
        private void Button_GetUpdate_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/eurotools/eurosound_project/releases/latest");
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
