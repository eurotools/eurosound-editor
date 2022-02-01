using System;
using System.Windows.Forms;

namespace sb_explorer
{
    public partial class SoundbanksList_Options : Form
    {
        public SoundbanksList_Options()
        {
            InitializeComponent();
        }

        private void Frm_SoundbanksList_Options_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 0) & 1))
            {
                RadioButton_ColFlags_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 1) & 1))
            {
                RadioButton_ColAddress_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 2) & 1))
            {
                RadioButton_ColMemSize_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 3) & 1))
            {
                RadioButton_ColSampleSize_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 4) & 1))
            {
                RadioButton_ColLoopStart_Hex.Checked = true;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            short userFlags = 0;
            if (RadioButton_ColFlags_Hex.Checked)
            {
                userFlags |= (1 << 0);
            }
            if (RadioButton_ColAddress_Hex.Checked)
            {
                userFlags |= (1 << 1);
            }
            if (RadioButton_ColMemSize_Hex.Checked)
            {
                userFlags |= (1 << 2);
            }
            if (RadioButton_ColSampleSize_Hex.Checked)
            {
                userFlags |= (1 << 3);
            }
            if (RadioButton_ColLoopStart_Hex.Checked)
            {
                userFlags |= (1 << 4);
            }

            //Ensure that flags have changed
            if (userFlags != GlobalVariables.ListViewWavDataFlags)
            {
                //Update var and list
                GlobalVariables.ListViewWavDataFlags = userFlags;
                ((Frm_MainFrame)Application.OpenForms["Frm_MainFrame"]).PrintWavData();

                //Update regedit
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\Settings", "WavHeaderData", GlobalVariables.ListViewWavDataFlags, Microsoft.Win32.RegistryValueKind.DWord);
            }

            //Close Form
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            //Close Form
            Close();
        }
    }
}
