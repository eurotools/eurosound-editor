using EuroSound_Editor.Forms;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_Output : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_Output()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkFastReSample_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkFastReSample.Checked)
            {
                GlobalPrefs.SoxEffect = "resample 0.97";
            }
            else
            {
                GlobalPrefs.SoxEffect = "resample -qs 0.97";
            }

            //Save State
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("Check1", chkFastReSample.Checked ? "1" : "0", "MainForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboOutputLanguage_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            //Save State
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("LanguageCombo", cboOutputLanguage.SelectedIndex.ToString(), "MainForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkOutputAllLanguages_Click(object sender, System.EventArgs e)
        {
            //Save State
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("OutputAllLanguages", chkOutputAllLanguages.Checked ? "1" : "0", "MainForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnFullOutput_Click(object sender, System.EventArgs e)
        {
            //Start Output
            string[] outputFiles = GetOutputFiles();
            if (outputFiles != null && outputFiles.Length > 0)
            {
                using (SfxOutputForm outputForm = new SfxOutputForm(outputFiles, CommonFunctions.GetOutputPlatforms(), CommonFunctions.GetOutputLanguages(), false, (MainForm)Application.OpenForms[nameof(MainForm)]))
                {
                    outputForm.ShowDialog();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnQuickOutput_Click(object sender, System.EventArgs e)
        {
            //Start Output
            string[] outputFiles = GetOutputFiles();
            if (outputFiles != null && outputFiles.Length > 0)
            {
                using (SfxOutputForm outputForm = new SfxOutputForm(outputFiles, CommonFunctions.GetOutputPlatforms(), CommonFunctions.GetOutputLanguages(), true, (MainForm)Application.OpenForms[nameof(MainForm)]))
                {
                    outputForm.ShowDialog();
                }
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private string[] GetOutputFiles()
        {
            string[] outputFiles = null;
            MainForm mainForm = (MainForm)Application.OpenForms[nameof(MainForm)];

            if (rdoOutput_Selected.Checked)
            {
                if (mainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks.CheckedItems.Count > 0)
                {
                    outputFiles = new string[] { mainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks.CheckedItems[0].ToString() };
                }
            }
            else
            {
                outputFiles = mainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks.Items.Cast<string>().ToArray();
            }

            return outputFiles;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
