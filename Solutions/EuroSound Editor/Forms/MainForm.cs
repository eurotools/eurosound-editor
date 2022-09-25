using sb_editor.Classes;
using sb_editor.Objects;
using System.IO;
using System.Windows.Forms;
using static sb_editor.Classes.MostRecentFilesMenu;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MainForm : Form
    {
        internal MostRecentFilesMenu RecentFilesMenu;

        //-------------------------------------------------------------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MainForm_Shown(object sender, System.EventArgs e)
        {
            if (ActiveForm != this)
            {
                FlashWindow.FlashWindowAPI(Handle);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));

            //Save checklistbox state
            for (int i = 0; i < UserControl_SoundBanks_CheckBox.cbllstSoundbanks.Items.Count; i++)
            {
                string state = UserControl_SoundBanks_CheckBox.cbllstSoundbanks.GetItemChecked(i) ? "True" : "False";
                iniFile.Write(i.ToString(), state, "Form1_List1_CHECKBOXS");
            }

            //Save controls state
            iniFile.Write("FormatCombo_ListIndex", UserControl_Output.cboOutputFormat.SelectedIndex.ToString(), "Form1_Misc");
            iniFile.Write("SelectedlBankOption_Value", UserControl_Output.rdoOutput_Selected.Checked ? "True" : "False", "Form1_Misc");
            iniFile.Write("AllBanksOption_Value", UserControl_Output.rdoAllBanksSelectedFormat.Checked ? "True" : "False", "Form1_Misc");
            iniFile.Write("AllFormatsOption_Value", UserControl_Output.rdoAllForAll.Checked ? "True" : "False", "Form1_Misc");

            //Save Changes
            RecentFilesMenu.SaveToIniFile();

            //Update Base INI
            IniFile baseIni = new IniFile(Path.Combine(Application.StartupPath, "EuroSound.ini"));
            baseIni.Write("Last_Project_Opened", GlobalPrefs.ProjectFolder, "Form1_Misc");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void MenuItemFile_Recent_Click(int fileNumber, string projectFilePath)
        {
            if (Directory.Exists(projectFilePath))
            {
                GlobalPrefs.ProjectFolder = projectFilePath;
                Application.Restart();
            }
            else
            {
                MessageBox.Show(string.Format("{0} {1}", "Project Not Found", projectFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                RecentFilesMenu.RemoveFile(fileNumber);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuFile_Open_Click(object sender, System.EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Update recent files list
                GlobalPrefs.ProjectFolder = Path.GetDirectoryName(OpenFileDialog.FileName);
                RecentFilesMenu.AddFile(GlobalPrefs.ProjectFolder);

                //Restart Tool
                Application.Restart();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuFile_New_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show(string.Format("This will create the following folders\n{0}\n{1}\n{2}\n{3}\n{4}\n\nProceed?", folderBrowserDialog.SelectedPath, Path.Combine(folderBrowserDialog.SelectedPath, "DataBases"), Path.Combine(folderBrowserDialog.SelectedPath, "SFXs"), Path.Combine(folderBrowserDialog.SelectedPath, "SoundBanks"), Path.Combine(folderBrowserDialog.SelectedPath, "System")), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    GlobalPrefs.ProjectFolder = folderBrowserDialog.SelectedPath;
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "System"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Debug_Report", "ForES2", "MarkerFileData"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "GameCube"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "PC"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "PlayStation2"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "X Box"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "GameCube"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "PC"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "PlayStation2"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "X Box"));
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc"));
                    TextFiles.WriteProjectFile(Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt"), new ProjectFile());
                    TextFiles.WritePropertiesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt"), new ProjProperties() { SampleFilesFolder = Application.StartupPath });
                    TextFiles.WriteRefine(Path.Combine(GlobalPrefs.ProjectFolder, "System", "RefineSearch.txt"), new string[] { "All", "HighLighted" });
                    TextFiles.WriteMiscFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Misc.txt"), true);

                    //Update recent files list
                    RecentFilesMenu.AddFile(GlobalPrefs.ProjectFolder);

                    //Copy Ini File to the new project
                    string iniPath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                    File.Delete(iniPath);
                    File.Copy(Path.Combine(Application.StartupPath, "EuroSound.ini"), iniPath);

                    //Update INI
                    IniFile systemIni = new IniFile(iniPath);
                    systemIni.Write("Last_Project_Opened", GlobalPrefs.ProjectFolder, "Form1_Misc");
                    for (int i = 0; i < mnuFile_RecentProjects.MenuItems.Count; i++)
                    {
                        systemIni.Write("Recent" + i, ((MruMenuItem)mnuFile_RecentProjects.MenuItems[i]).Filename, "RecentFiles");
                    }

                    //Restart Tool
                    Application.Restart();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuFile_Exit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuHelp_About_Click(object sender, System.EventArgs e)
        {
            using (HelpForm aboutFrm = new HelpForm())
            {
                aboutFrm.ShowDialog();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
