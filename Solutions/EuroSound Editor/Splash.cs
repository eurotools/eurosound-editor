using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Splash : Form
    {
        private readonly MainForm frmMainForm;

        //-------------------------------------------------------------------------------------------------------------------------------
        public Splash(MainForm mainFrame)
        {
            InitializeComponent();
            frmMainForm = mainFrame;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Splash_Load(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "SystemFiles", "Version.txt");
            if (File.Exists(filePath))
            {
                lblVersion.Text = string.Format("Version: {0}", TextFiles.ReadFileVersion(filePath));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_Splash_Shown(object sender, EventArgs e)
        {
            //Custom cursors
            frmMainForm.UserControl_Available_Databases.btnAddDataBases.Cursor = new Cursor(new MemoryStream(Properties.Resources.arrow_left));
            frmMainForm.UserControl_DataBaseSfx.btnRemoveSfx.Cursor = new Cursor(new MemoryStream(Properties.Resources.arrow_right));
            frmMainForm.UserControl_Available_SFXs.btnAddSFXs.Cursor = new Cursor(new MemoryStream(Properties.Resources.arrow_left));
            frmMainForm.UserControl_Output.cboOutputLanguage.Cursor = new Cursor(new MemoryStream(Properties.Resources.lang_english));

            //Recent Files
            frmMainForm.RecentFilesMenu = new MruStripMenuInline(frmMainForm.mnuFile_RecentProjects, frmMainForm.mnuFile_RecentFiles, new MostRecentFilesMenu.ClickedHandler(frmMainForm.MenuItemFile_Recent_Click), Path.Combine(Application.StartupPath, "EuroSound.ini"), 5);
            frmMainForm.RecentFilesMenu.LoadFromIniFile();

            //Load INI file located at the .EXE location
            string iniFilePath = Path.Combine(Application.StartupPath, "EuroSound.ini");
            if (File.Exists(iniFilePath))
            {
                IniFile iniFile = new IniFile(iniFilePath);
                GlobalPrefs.ProjectFolder = iniFile.Read("Last_Project_Opened", "Form1_Misc");
                if (!string.IsNullOrEmpty(GlobalPrefs.ProjectFolder))
                {
                    frmMainForm.RecentFilesMenu.AddFile(GlobalPrefs.ProjectFolder);
                }
                GlobalPrefs.EuroSoundUser = iniFile.Read("UserName", "Form1_Misc");
            }

            //Read last opened project
            string projectFile = Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt");
            if (!string.IsNullOrEmpty(GlobalPrefs.ProjectFolder) && File.Exists(projectFile))
            {
                //Read file containing the count of the hashcodes
                string miscFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Misc.txt");
                if (File.Exists(miscFilePath))
                {
                    Misc miscFileSettings = TextFiles.ReadMiscFile(miscFilePath);

                    //Compare current version with the project version
                    Version projectVersion = new Version(miscFileSettings.Version.ToString());
                    Version euroSoundVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor);
                    if (euroSoundVersion.CompareTo(projectVersion) == 0 || euroSoundVersion.CompareTo(projectVersion) == 1)
                    {
                        //Load properties file
                        string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
                        if (File.Exists(projectPropertiesFile))
                        {
                            //Create Temporal Output Folders
                            CommonFunctions.CheckForMissingFolders();

                            //Update variables
                            GlobalPrefs.SFXHashCodeNumber = miscFileSettings.SFXHashCodeNumber;
                            GlobalPrefs.SoundBankHashCodeNumber = miscFileSettings.SoundBankHashCodeNumber;
                            GlobalPrefs.MFXHashCodeNumber = miscFileSettings.MFXHashCodeNumber;
                            GlobalPrefs.ReverbHashCodeNumber = miscFileSettings.ReverbHashCodeNumber;
                            GlobalPrefs.CurrentProject = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                            //Ask for userName if we don't have it
                            if (string.IsNullOrEmpty(GlobalPrefs.EuroSoundUser))
                            {
                                CommonFunctions.AskForUserName();
                            }

                            //Update title bar
                            frmMainForm.Text = string.Format("EuroSound - \"{0}\"", GlobalPrefs.ProjectFolder);

                            //Get project paths
                            string projectFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt");

                            //Load project data
                            ProjectFile projectData = TextFiles.ReadProjectFile(projectFilePath, true);
                            projectData.SoundBanks = frmMainForm.UserControl_SoundBanks_CheckBox.LoadSoundBanks();
                            projectData.DataBases = frmMainForm.UserControl_Available_Databases.LoadDataBases();
                            TextFiles.WriteProjectFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt"), projectData);
                            frmMainForm.UserControl_SoundBanks.LoadSoundBanks(frmMainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks);
                            frmMainForm.UserControl_Available_SFXs.UserControl_RefineSFX.LoadKeywords();
                            frmMainForm.UserControl_DataBaseSfx.ClearControl();

                            //Load SFX Labels
                            string[] sfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                            string[] labels = new string[sfxFiles.Length];
                            for (int i = 0; i < sfxFiles.Length; i++)
                            {
                                labels[i] = Path.GetFileNameWithoutExtension(sfxFiles[i]);
                            }
                            projectData.SFXs = labels;

                            //Update text file
                            TextFiles.WriteProjectFile(projectFilePath, projectData);

                            //Fill comboboxes
                            frmMainForm.UserControl_Output.cboOutputFormat.Items.AddRange(GlobalPrefs.CurrentProject.platformData.Keys.ToArray());
                            if (frmMainForm.UserControl_Output.cboOutputFormat.Items.Count > 0)
                            {
                                frmMainForm.UserControl_Output.cboOutputFormat.SelectedIndex = 0;
                            }

                            //Check Languages From Speech Folder
                            if (Directory.Exists(Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master")))
                            {
                                string speechDir = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", "Speech");
                                Directory.CreateDirectory(Path.Combine(speechDir, "English"));

                                //Get All Language Directories
                                string[] langDirectories = Directory.GetDirectories(Path.Combine(speechDir));
                                for (int i = 0; i < langDirectories.Length; i++)
                                {
                                    string lang = Path.GetFileName(langDirectories[i]);
                                    if (Array.FindIndex(GlobalPrefs.Languages, s => s.Equals(lang, StringComparison.OrdinalIgnoreCase)) == -1)
                                    {
                                        MessageBox.Show(string.Format("{0} is not a valid Language!", lang), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        frmMainForm.UserControl_Output.cboOutputLanguage.Items.Add(lang);
                                    }
                                }

                                //Select First Item
                                if (frmMainForm.UserControl_Output.cboOutputLanguage.Items.Count > 0)
                                {
                                    frmMainForm.UserControl_Output.cboOutputLanguage.SelectedIndex = 0;
                                }
                            }

                            //Restore last state
                            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                            if (File.Exists(systemIniFilePath))
                            {
                                //Update SFX Panel UI
                                IniFile sysIniFile = new IniFile(systemIniFilePath);
                                frmMainForm.UserControl_Available_SFXs.UserControl_RefineSFX.chkSortByDate.Checked = sysIniFile.Read("Check2", "MainForm").Trim().Equals("1");
                                frmMainForm.UserControl_Available_SFXs.chkIconView.Checked = sysIniFile.Read("Check3", "MainForm").Trim().Equals("1");
                                //Update Output Panel
                                frmMainForm.UserControl_Output.chkFastReSample.Checked = sysIniFile.Read("Check1", "MainForm").Equals("1");
                                if (int.TryParse(sysIniFile.Read("FormatCombo_ListIndex", "Form1_Misc"), out int formatIndex))
                                {
                                    if (formatIndex > 0 && formatIndex < frmMainForm.UserControl_Output.cboOutputFormat.Items.Count)
                                    {
                                        frmMainForm.UserControl_Output.cboOutputFormat.SelectedIndex = formatIndex;
                                    }
                                }
                                if (int.TryParse(sysIniFile.Read("LanguageCombo", "MainForm"), out int langIndex))
                                {
                                    if (langIndex > 0 && langIndex < frmMainForm.UserControl_Output.cboOutputLanguage.Items.Count)
                                    {
                                        frmMainForm.UserControl_Output.cboOutputLanguage.SelectedIndex = langIndex;
                                    }
                                }
                                frmMainForm.UserControl_Output.chkOutputAllLanguages.Checked = sysIniFile.Read("OutputAllLanguages", "MainForm").Equals("1");
                                frmMainForm.UserControl_Output.rdoOutput_Selected.Checked = sysIniFile.Read("SelectedlBankOption_Value", "Form1_Misc").Equals("True", StringComparison.OrdinalIgnoreCase);
                                frmMainForm.UserControl_Output.rdoAllBanksSelectedFormat.Checked = sysIniFile.Read("AllBanksOption_Value", "Form1_Misc").Equals("True", StringComparison.OrdinalIgnoreCase);
                                frmMainForm.UserControl_Output.rdoAllForAll.Checked = sysIniFile.Read("AllFormatsOption_Value", "Form1_Misc").Equals("True", StringComparison.OrdinalIgnoreCase);
                            }

                            //Enable or disable output buttons
                            if (GlobalPrefs.CurrentProject.platformData.Count == 0)
                            {
                                frmMainForm.UserControl_Output.btnFullOutput.Enabled = false;
                                frmMainForm.UserControl_Output.btnQuickOutput.Enabled = false;
                            }

                            //SFXs
                            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
                            {
                                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc"));
                                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "PC"));
                                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "X Box"));
                                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "GameCube"));
                                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "PlayStation2"));
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Project Properties File Not Found {0}", projectPropertiesFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0}:\n\n{1}: {2}\n{3}: {4}\n\n{5}", "EuroSound out of Date for Project", "EuroSound Version", euroSoundVersion.ToString(), "Project Version", projectVersion.ToString(), "Must Get Latest EuroSound to Load this project!"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("Project Not Found {0}", GlobalPrefs.ProjectFolder), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Start timer
            tmrSplash.Start();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TimerSplash_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
