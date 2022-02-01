using System;
using System.Windows.Forms;

namespace sb_explorer
{
    public partial class StreambanksList_Options : Form
    {
        public StreambanksList_Options()
        {
            InitializeComponent();
        }

        private void StreambanksList_Options_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 0) & 1))
            {
                RadioButton_ColMarkerOffset_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 1) & 1))
            {
                RadioButton_ColMarkerSize_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 2) & 1))
            {
                RadioButton_ColAudioOffset_Hex.Checked = true;
            }
            if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 3) & 1))
            {
                RadioButton_ColAudioLength_Hex.Checked = true;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            short userFlags = 0;
            if (RadioButton_ColMarkerOffset_Hex.Checked)
            {
                userFlags |= (1 << 0);
            }
            if (RadioButton_ColMarkerSize_Hex.Checked)
            {
                userFlags |= (1 << 1);
            }
            if (RadioButton_ColAudioOffset_Hex.Checked)
            {
                userFlags |= (1 << 2);
            }
            if (RadioButton_ColAudioLength_Hex.Checked)
            {
                userFlags |= (1 << 3);
            }

            //Ensure that flags have changed
            if (userFlags != GlobalVariables.ListViewStreamDataFlags)
            {
                //Update var and list
                GlobalVariables.ListViewStreamDataFlags = userFlags;
                ((Frm_MainFrame)Application.OpenForms["Frm_MainFrame"]).UpdateStreamDataView();

                //Update regedit
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\Settings", "StreamData", GlobalVariables.ListViewStreamDataFlags, Microsoft.Win32.RegistryValueKind.DWord);
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
