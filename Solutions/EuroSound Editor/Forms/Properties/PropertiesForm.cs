using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class PropertiesForm : Form
    {
        private int index = 0;
        private ProjProperties temporalObj;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PropertiesForm()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_ProjectProperties_Load(object sender, EventArgs e)
        {
            temporalObj = TextFiles.ReadPropertiesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt"));
            CheckResampleRates(temporalObj);

            //Comboboxes default values
            cboAvailableFormats.SelectedIndex = 0;

            //Project Properties
            txtMasterDirectory.Text = temporalObj.SampleFilesFolder;
            txtHashCodeFile.Text = temporalObj.HashCodeFileDirectory;
            txtEngineXProject.Text = temporalObj.EngineXProjectPath;
            txtEuroLandServer.Text = temporalObj.EuroLandHashCodeServerPath;

            //Listview formats
            lvwAvailableFormats.BeginUpdate();
            foreach (KeyValuePair<string, PlatformData> formatInfo in temporalObj.platformData)
            {
                lvwAvailableFormats.Items.Add(new ListViewItem(new string[] { formatInfo.Key, formatInfo.Value.OutputFolder, formatInfo.Value.AutoReSample ? "On" : "Off" }));
            }
            lvwAvailableFormats.EndUpdate();

            //Available resample rates
            lstAvailableSampleRates.BeginUpdate();
            lstAvailableSampleRates.Items.AddRange(temporalObj.ResampleRates.ToArray());
            lstAvailableSampleRates.EndUpdate();

            cboDefaultRate.BeginUpdate();
            cboDefaultRate.Items.AddRange(temporalObj.ResampleRates.ToArray());
            if (cboDefaultRate.Items.Count > 0)
            {
                cboDefaultRate.SelectedIndex = temporalObj.DefaultRate;
            }
            cboDefaultRate.EndUpdate();

            //Combobox ReSample Rates
            cboFormat.BeginUpdate();
            cboFormat.Items.AddRange(temporalObj.platformData.Keys.ToArray());
            if (cboFormat.Items.Count > 0)
            {
                cboFormat.SelectedIndex = 0;
            }
            cboFormat.EndUpdate();

            //Combobox Memory Slots
            cboMemSlotFormat.BeginUpdate();
            cboMemSlotFormat.Items.AddRange(temporalObj.platformData.Keys.ToArray());
            if (cboMemSlotFormat.Items.Count > 0)
            {
                cboMemSlotFormat.SelectedIndex = 0;
            }
            cboMemSlotFormat.EndUpdate();
            UpdateMemMapsComboboxes();

            //Soundbanks
            string soundbanksPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");
            if (Directory.Exists(soundbanksPath))
            {
                IEnumerable<string> sbFiles = Directory.EnumerateFiles(soundbanksPath, "*.txt", SearchOption.TopDirectoryOnly);
                foreach (string sbFile in sbFiles)
                {
                    SoundBank sbData = TextFiles.ReadSoundbankFile(sbFile);
                    ListViewItem sbItem = new ListViewItem(new string[] { Path.GetFileNameWithoutExtension(sbFile), sbData.MemoryMap });
                    lvwSoundBanks.Items.Add(sbItem);
                }
            }

            //Default Memory Map
            cboDefaultMemMap.BeginUpdate();
            cboDefaultMemMap.Items.AddRange(temporalObj.MemoryMaps.ToArray());
            if (cboDefaultMemMap.Items.Count > 0)
            {
                cboDefaultMemMap.SelectedIndex = temporalObj.DefaultMemMap;
            }
            cboDefaultMemMap.EndUpdate();

            //Ini File Data
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                IniFile systemIni = new IniFile(systemIniFilePath);
                //Max Sizes
                if (uint.TryParse(systemIni.Read("PCSize", "PropertiesForm"), out uint pcMaxSize))
                {
                    nudPCSize.Value = pcMaxSize;
                }
                if (uint.TryParse(systemIni.Read("PlayStationSize", "PropertiesForm"), out uint playStationMaxSize))
                {
                    nudPlayStationSize.Value = playStationMaxSize;
                }
                if (uint.TryParse(systemIni.Read("GameCubeSize", "PropertiesForm"), out uint gameCubeMaxSize))
                {
                    nudGameCubeSize.Value = gameCubeMaxSize;
                }
                if (uint.TryParse(systemIni.Read("XBoxSize", "PropertiesForm"), out uint xboxMaxSize))
                {
                    nudXboxSzie.Value = xboxMaxSize;
                }

                //Misc options
                chkPrefixHashCodes.Checked = systemIni.Read("Prefix_HT_Sound", "PropertiesForm").Equals("1");
                chkPreviewCommands.Checked = systemIni.Read("ViewOutputDos", "PropertiesForm").Equals("1");
            }
            else
            {
                MessageBox.Show(string.Format("File Not Found '{0}'", systemIniFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Ini File Data
            string baseIniFilePath = Path.Combine(Application.StartupPath, "EuroSound.ini");
            if (File.Exists(baseIniFilePath))
            {
                IniFile baseIni = new IniFile(baseIniFilePath);
                txtEditWavsTool.Text = baseIni.Read("Edit_Wavs_With", "Form7_Misc");
                txtSoundForge.Text = baseIni.Read("SoundForge", "Form7_Misc");
                txtUserName.Text = baseIni.Read("UserName", "Form1_Misc");
                txtTextEditor.Text = baseIni.Read("TextEditor", "PropertiesForm");
            }
            else
            {
                MessageBox.Show(string.Format("File Not Found '{0}'", baseIniFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Update all data
            ProjectFileFunctions.UpdateSFXs((MainForm)Application.OpenForms[nameof(MainForm)]);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_ProjectProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel || DialogResult == DialogResult.Abort)
            {
                if (MessageBox.Show("Are you sure you wish to Quit Properties whitout saving?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    //Update all data
                    ProjectFileFunctions.UpdateAll((MainForm)Application.OpenForms[nameof(MainForm)]);
                }
            }
            else if (DialogResult == DialogResult.OK)
            {
                //Update all data
                ProjectFileFunctions.UpdateAll((MainForm)Application.OpenForms[nameof(MainForm)]);
            }
        }

        //*===============================================================================================
        //* BUTTON EVENTS
        //*===============================================================================================
        private void BtnSetMasterFolder_Click(object sender, EventArgs e)
        {
            FolderBrowser.Description = "Set Folder For Sample Files.";
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtMasterDirectory.Text = FolderBrowser.SelectedPath;
                temporalObj.SampleFilesFolder = FolderBrowser.SelectedPath;

                //Create Speech Folder
                if (Directory.Exists(Path.Combine(FolderBrowser.SelectedPath, "Master")))
                {
                    Directory.CreateDirectory(Path.Combine(FolderBrowser.SelectedPath, "Master", "Speech", "English"));
                }
                else
                {
                    MessageBox.Show("'Master' folder not found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSetHashCodesDir_Click(object sender, EventArgs e)
        {
            FolderBrowser.Description = "Set Folder For HashCode Files";
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtHashCodeFile.Text = FolderBrowser.SelectedPath;
                temporalObj.HashCodeFileDirectory = FolderBrowser.SelectedPath;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSetEngineXPath_Click(object sender, EventArgs e)
        {
            FolderBrowser.Description = "Set Folder For EngineX Binary Files";
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtEngineXProject.Text = FolderBrowser.SelectedPath;
                temporalObj.EngineXProjectPath = FolderBrowser.SelectedPath;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSetEuroLandServer_Click(object sender, EventArgs e)
        {
            FolderBrowser.Description = "Set Folder For Sound.h File";
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtEuroLandServer.Text = FolderBrowser.SelectedPath;
                temporalObj.EuroLandHashCodeServerPath = FolderBrowser.SelectedPath;
            }
        }

        //*===============================================================================================
        //* AVAILABLE FORMAT PROPERTIES 
        //*===============================================================================================
        private void BtnAddFormat_Click(object sender, EventArgs e)
        {
            string selectedFormat = cboAvailableFormats.SelectedItem.ToString();
            if (!temporalObj.platformData.ContainsKey(selectedFormat))
            {
                //Create new platform data
                PlatformData formatPlatformData = new PlatformData { OutputFolder = "Set Output Folder.", AutoReSample = true };
                for (int i = 0; i < temporalObj.ResampleRates.Count; i++)
                {
                    formatPlatformData.ReSampleRates.Add(22050);
                }

                //Add data
                temporalObj.platformData.Add(selectedFormat, formatPlatformData);
                lvwAvailableFormats.Items.Add(new ListViewItem(new string[] { selectedFormat, formatPlatformData.OutputFolder, formatPlatformData.AutoReSample ? "On" : "Off" }));

                //Update Combobox
                if (!cboFormat.Items.Contains(selectedFormat))
                {
                    cboFormat.Items.Add(selectedFormat);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSearchOutputFolder_Click(object sender, EventArgs e)
        {
            //Check if any format is selected in the list view
            if (lvwAvailableFormats.SelectedItems.Count > 0)
            {
                //Set the description for the folder browser dialog
                FolderBrowser.Description = "Set Folder For Binary Files.";

                //Open the folder browser dialog
                if (FolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    foreach (ListViewItem selectedFormat in lvwAvailableFormats.SelectedItems)
                    {
                        temporalObj.platformData[selectedFormat.Text].OutputFolder = FolderBrowser.SelectedPath;
                        selectedFormat.SubItems[1].Text = FolderBrowser.SelectedPath;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAutoReSampleOn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature has been removed 17/4/02", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAutoReSampleOff_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature has been removed 17/4/02", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //*===============================================================================================
        //* AVAILABLE RESAMPLE RATES
        //*===============================================================================================
        private void BtnAddSampleRate_Click(object sender, EventArgs e)
        {
            // create an input form to get the new re-sample rate name
            using (Frm_InputBox inputForm = new Frm_InputBox() { Text = "New Re-sample Name" })
            {
                // set the label text and default value for the input form
                inputForm.lblText.Text = "Enter New Re-sample Rate Name";
                inputForm.txtInputData.Text = GetReSampleName();

                // show the input form and check if the user clicked OK
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    if (lstAvailableSampleRates.FindStringExact(inputForm.txtInputData.Text) == ListBox.NoMatches)
                    {
                        // add the new re-sample rate name to the list of available sample rates
                        lstAvailableSampleRates.Items.Add(inputForm.txtInputData.Text);
                        if (cboFormat.SelectedItem != null)
                        {
                            lvwReSampleFormats.Items.Add(new ListViewItem(new string[] { inputForm.txtInputData.Text, "22050" }));
                        }

                        //Update All Formats & Project File
                        temporalObj.ResampleRates.Add(inputForm.txtInputData.Text);
                        foreach (KeyValuePair<string, PlatformData> formatInfo in temporalObj.platformData)
                        {
                            formatInfo.Value.ReSampleRates.Add(22050);
                        }

                        //Add Resample to combobox
                        if (!cboDefaultRate.Items.Contains(inputForm.txtInputData.Text))
                        {
                            cboDefaultRate.Items.Add(inputForm.txtInputData.Text);
                        }
                    }
                }
            }
        }

        //*===============================================================================================
        //* RESAMPLE RATE VALUES PER FORMAT
        //*===============================================================================================
        private void CboFormat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboFormat.SelectedItem != null)
            {
                string format = cboFormat.SelectedItem.ToString();
                if (temporalObj.platformData.ContainsKey(format))
                {
                    List<int> formatRates = temporalObj.platformData[format].ReSampleRates;
                    lvwReSampleFormats.BeginUpdate();
                    lvwReSampleFormats.Items.Clear();
                    for (int i = 0; i < formatRates.Count; i++)
                    {
                        lvwReSampleFormats.Items.Add(new ListViewItem(new string[] { temporalObj.ResampleRates[i].ToString(), formatRates[i].ToString() }));
                    }
                    lvwReSampleFormats.EndUpdate();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwReSampleFormats_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwReSampleFormats.SelectedItems.Count > 0 && temporalObj.platformData.ContainsKey(cboFormat.SelectedItem.ToString()))
            {
                //Ask user for a value
                using (Frm_InputBox inputBox = new Frm_InputBox() { Text = "New Sample Rate" })
                {
                    inputBox.lblText.Text = "Enter New Re-sample Rate";
                    inputBox.txtInputData.Text = lvwReSampleFormats.SelectedItems[0].SubItems[1].Text;
                    if (inputBox.ShowDialog() == DialogResult.OK)
                    {
                        //Ensure that the input value is valid
                        if (inputBox.txtInputData.Text.All(char.IsNumber) && int.TryParse(inputBox.txtInputData.Text, out int inputSampleRate))
                        {
                            //Update subitem && dictionary
                            lvwReSampleFormats.SelectedItems[0].SubItems[1].Text = inputBox.txtInputData.Text;
                            string formatToUpdate = cboFormat.SelectedItem.ToString();
                            if (temporalObj.platformData.ContainsKey(formatToUpdate))
                            {
                                temporalObj.platformData[formatToUpdate].ReSampleRates[lvwReSampleFormats.SelectedItems[0].Index] = inputSampleRate;
                            }
                        }
                        else
                        {
                            //Inform user
                            MessageBox.Show("Invalid sample rate value", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        //*===============================================================================================
        //* Memory Maps
        //*===============================================================================================
        private void ButtonCreateMemSlot_Click(object sender, EventArgs e)
        {
            // create an input form to get the new re-sample rate name
            using (Frm_InputBox inputForm = new Frm_InputBox() { Text = "New Memory-Map" })
            {
                // set the label text and default value for the input form
                inputForm.lblText.Text = "Enter New Memory Map Name";

                // show the input form and check if the user clicked OK
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    // ensure is not duplicated
                    if (temporalObj.MemoryMaps.IndexOf(inputForm.txtInputData.Text) == -1)
                    {
                        lvwAvailableMemSlots.Items.Add(new ListViewItem(new string[] { inputForm.txtInputData.Text, "0" }));
                        temporalObj.MemoryMaps.Add(inputForm.txtInputData.Text);

                        //Add the new value to all platforms
                        foreach (KeyValuePair<string, PlatformData> platformData in temporalObj.platformData)
                        {
                            platformData.Value.MemoryMapsSize.Add(1024);
                        }

                        //Update UI
                        UpdateMemMapsComboboxes();
                        CboMemSlotFormat_SelectedIndexChanged(null, null);

                        //Add Memory Map to combobox
                        if (!cboDefaultMemMap.Items.Contains(inputForm.txtInputData.Text))
                        {
                            cboDefaultMemMap.Items.Add(inputForm.txtInputData.Text);
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboMemSlotFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPlatform = cboMemSlotFormat.SelectedItem.ToString();
            if (temporalObj.platformData.ContainsKey(selectedPlatform))
            {
                lvwAvailableMemSlots.BeginUpdate();
                lvwAvailableMemSlots.Items.Clear();

                // Add format labels to the listview
                int[] platformResampleRates = temporalObj.platformData[selectedPlatform].MemoryMapsSize.ToArray();
                for (int i = 0; i < platformResampleRates.Length; i++)
                {
                    ListViewItem lvItem = new ListViewItem(new string[] { temporalObj.MemoryMaps[i], platformResampleRates[i].ToString() });
                    lvwAvailableMemSlots.Items.Add(lvItem);
                }

                //Draw listview again
                lvwAvailableMemSlots.EndUpdate();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwAvailableMemSlots_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwAvailableMemSlots.SelectedItems.Count > 0 && temporalObj.platformData.ContainsKey(cboMemSlotFormat.SelectedItem.ToString()))
            {
                //Ask user for a value
                using (Frm_InputBox inputBox = new Frm_InputBox() { Text = "Memory-Map Max Size" })
                {
                    inputBox.lblText.Text = "Enter New Memory-Map Max Size";
                    inputBox.txtInputData.Text = lvwAvailableMemSlots.SelectedItems[0].SubItems[1].Text;
                    if (inputBox.ShowDialog() == DialogResult.OK)
                    {
                        //Ensure that the input value is valid
                        if (inputBox.txtInputData.Text.All(char.IsNumber) && int.TryParse(inputBox.txtInputData.Text, out int mapMaxSize))
                        {
                            //Update subitem && dictionary
                            lvwAvailableMemSlots.SelectedItems[0].SubItems[1].Text = inputBox.txtInputData.Text;
                            string formatToUpdate = cboMemSlotFormat.SelectedItem.ToString();
                            if (temporalObj.platformData.ContainsKey(formatToUpdate))
                            {
                                temporalObj.platformData[formatToUpdate].MemoryMapsSize[lvwAvailableMemSlots.SelectedItems[0].Index] = mapMaxSize;
                            }
                        }
                        else
                        {
                            //Inform user
                            MessageBox.Show("Invalid max size value", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdateMemMapsComboboxes()
        {
            int resamplePrevIndex = 0;

            //Clear items if required
            if (cboAvailableMemoryMaps.Items.Count > 0)
            {
                resamplePrevIndex = cboAvailableMemoryMaps.SelectedIndex;
                cboAvailableMemoryMaps.Items.Clear();
            }

            //Add available formats and select the first item
            if (temporalObj.MemoryMaps.Count > 0)
            {
                cboAvailableMemoryMaps.Items.AddRange(temporalObj.MemoryMaps.ToArray());
                cboAvailableMemoryMaps.SelectedIndex = resamplePrevIndex;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboAvailableMemoryMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAvailableMemoryMaps.SelectedItem != null)
            {
                //Read project
                string selectedMapSlot = cboAvailableMemoryMaps.SelectedItem.ToString();
                foreach (ListViewItem soundBank in lvwSoundBanks.SelectedItems)
                {
                    //Update UI
                    soundBank.SubItems[1].Text = selectedMapSlot;

                    //Update File
                    string sbPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", soundBank.Text + ".txt");
                    SoundBank sbData = TextFiles.ReadSoundbankFile(sbPath);
                    sbData.MemoryMap = selectedMapSlot;
                    TextFiles.WriteSoundBankFile(sbPath, sbData);
                }
            }
        }

        //*===============================================================================================
        //* MISC
        //*===============================================================================================
        private void CboDefaultRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDefaultRate.SelectedIndex >= 0)
            {
                temporalObj.DefaultRate = cboDefaultRate.SelectedIndex;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboDefaultMemMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDefaultMemMap.SelectedIndex >= 0)
            {
                temporalObj.DefaultMemMap = cboDefaultMemMap.SelectedIndex;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtEditWavsTool_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OpenFileDiag.ShowDialog() == DialogResult.OK)
            {
                txtEditWavsTool.Text = OpenFileDiag.FileName;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtSoundForge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OpenFileDiag.ShowDialog() == DialogResult.OK)
            {
                txtSoundForge.Text = OpenFileDiag.FileName;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtUserName_DoubleClick(object sender, EventArgs e)
        {
            CommonFunctions.AskForUserName();
            txtUserName.Text = GlobalPrefs.EuroSoundUser;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtTextEditor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OpenFileDiag.ShowDialog() == DialogResult.OK)
            {
                txtTextEditor.Text = OpenFileDiag.FileName;
            }
        }

        //*===============================================================================================
        //* FORM BUTTON EVENTS
        //*===============================================================================================
        private void BtnOK_Click(object sender, EventArgs e)
        {
            //Update variables
            GlobalPrefs.EuroSoundUser = txtUserName.Text;
            TextFiles.WritePropertiesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt"), temporalObj);

            //Update INI Files
            IniFile toolIniFile = new IniFile(Path.Combine(Application.StartupPath, "EuroSound.ini"));
            toolIniFile.Write("Edit_Wavs_With", txtEditWavsTool.Text, "Form7_Misc");
            toolIniFile.Write("SoundForge", txtSoundForge.Text, "Form7_Misc");
            toolIniFile.Write("UserName", txtUserName.Text, "Form1_Misc");
            toolIniFile.Write("TextEditor", txtTextEditor.Text, "PropertiesForm");

            IniFile projectIniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            projectIniFile.Write("PlayStationSize", ((uint)nudPlayStationSize.Value).ToString(), "PropertiesForm");
            projectIniFile.Write("PCSize", ((uint)nudPCSize.Value).ToString(), "PropertiesForm");
            projectIniFile.Write("GameCubeSize", ((uint)nudGameCubeSize.Value).ToString(), "PropertiesForm");
            projectIniFile.Write("XBoxSize", ((uint)nudXboxSzie.Value).ToString(), "PropertiesForm");
            projectIniFile.Write("Prefix_HT_Sound", chkPrefixHashCodes.Checked ? "1" : "0", "PropertiesForm");
            projectIniFile.Write("ViewOutputDos", chkPreviewCommands.Checked ? "1" : "0", "PropertiesForm");

            //Enable or disable output buttons
            MainForm frmMainForm = (MainForm)Application.OpenForms[nameof(MainForm)];
            frmMainForm.UserControl_Output.btnFullOutput.Enabled = temporalObj.platformData.Count != 0;
            frmMainForm.UserControl_Output.btnQuickOutput.Enabled = temporalObj.platformData.Count != 0;

            //Update Combobox
            string[] availableFormats = temporalObj.platformData.Keys.ToArray();
            for (int i = 0; i < availableFormats.Length; i++)
            {
                if (frmMainForm.UserControl_Output.cboOutputFormat.FindStringExact(availableFormats[i]) == -1)
                {
                    frmMainForm.UserControl_Output.cboOutputFormat.Items.Add(availableFormats[i]);
                }
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void CheckResampleRates(ProjProperties tempObject)
        {
            //ReSample Rates for formats can't be empty
            foreach (KeyValuePair<string, PlatformData> formatData in tempObject.platformData)
            {
                // If there are no resample rates for the current platform data
                if (formatData.Value.ReSampleRates.Count == 0)
                {
                    // Add default resample rate (22050) for each resample rate in the temporary object
                    for (int i = 0; i < tempObject.ResampleRates.Count; i++)
                    {
                        formatData.Value.ReSampleRates.Add(22050);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private string GetReSampleName()
        {
            string ReSampleLabel;
            string[] ReSampleNames = new string[] { "Minimum", "Low", "Medium", "High", "Maximum" };
            if (index < ReSampleNames.Length)
            {
                ReSampleLabel = ReSampleNames[index];
            }
            else
            {
                ReSampleLabel = string.Format("{0} {1}", ReSampleNames[4], index - (ReSampleNames.Length - 1));
            }
            index++;

            return ReSampleLabel;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
