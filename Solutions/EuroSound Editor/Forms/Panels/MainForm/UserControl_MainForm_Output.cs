﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Output Settings Panel
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Forms;
using sb_editor.Objects;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor.Panels
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
                //Read project settings
                string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
                if (File.Exists(projectPropertiesFile))
                {
                    ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                    //Show output form
                    using (SfxOutputForm outputForm = new SfxOutputForm(outputFiles, CommonFunctions.GetOutputPlatforms(projectSettings), CommonFunctions.GetOutputLanguages(), false, (MainForm)Application.OpenForms[nameof(MainForm)]))
                    {
                        outputForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Project Properties File Not Found {0}", projectPropertiesFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //Read project settings
                string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
                if (File.Exists(projectPropertiesFile))
                {
                    ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                    //Show output form
                    using (SfxOutputForm outputForm = new SfxOutputForm(outputFiles, CommonFunctions.GetOutputPlatforms(projectSettings), CommonFunctions.GetOutputLanguages(), true, (MainForm)Application.OpenForms[nameof(MainForm)]))
                    {
                        outputForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Project Properties File Not Found {0}", projectPropertiesFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
