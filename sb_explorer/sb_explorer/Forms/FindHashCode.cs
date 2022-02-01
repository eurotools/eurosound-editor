using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace sb_explorer
{
    public partial class FindHashCode : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        public FindHashCode()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void FindHashCode_Load(object sender, EventArgs e)
        {
            RadioButton_MatchPartial.Checked = Convert.ToBoolean(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\SearchDlg", "MatchPartial", 1));
            Textbox_TextSearch.Text = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\SearchDlg", "LastSearch");
        }

        private void FindHashCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\SearchDlg", "MatchPartial", Convert.ToInt32(RadioButton_MatchPartial.Checked), RegistryValueKind.DWord);
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\SearchDlg", "LastSearch", Textbox_TextSearch.Text, RegistryValueKind.String);
        }

        //*===============================================================================================
        //* FORM CONTROLS EVENTS
        //*===============================================================================================
        private void Button_Find_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
