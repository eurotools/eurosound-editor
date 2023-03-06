using ESUtils;
using PCAudioDLL;
using sb_editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class ReverbTester : Form
    {
        internal readonly ProjProperties projectSettings;
        private ReverbFile currentReverbFile;
        private bool askSaveChanges = false;

        //-------------------------------------------------------------------------------------------------------------------------------
        public ReverbTester()
        {
            InitializeComponent();

            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ReverbTester_Load(object sender, EventArgs e)
        {
            string[] reverbFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs"), "*.txt", SearchOption.TopDirectoryOnly);
            lstbHashCodes.BeginUpdate();
            for (int i = 0; i < reverbFiles.Length; i++)
            {
                lstbHashCodes.Items.Add(Path.GetFileNameWithoutExtension(reverbFiles[i]));
            }
            lstbHashCodes.EndUpdate();

            //Select first item
            if (lstbHashCodes.Items.Count > 0)
            {
                lstbHashCodes.SelectedIndex = 0;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ReverbTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Check the way the form is closing
            if (DialogResult == DialogResult.OK)
            {
                SaveReverbData();
            }
            else
            {
                //Ask user what wants to do
                if (MessageBox.Show(string.Format("Save Changes to : '{0}' ?", currentReverbFile.TextFileName), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TextFiles.WriteReverbFile(currentReverbFile, Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs", currentReverbFile.TextFileName + ".txt"));
                }
            }
        }

        //*===============================================================================================
        //* ListBox
        //*===============================================================================================
        private void LstbHashCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbHashCodes.SelectedItem != null)
            {
                //Save Changes if required
                if (askSaveChanges)
                {
                    if (MessageBox.Show(string.Format("Save Changes to : '{0}' ?", currentReverbFile.TextFileName), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        TextFiles.WriteReverbFile(currentReverbFile, Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs", currentReverbFile.TextFileName + ".txt"));
                    }
                    askSaveChanges = false;
                }

                //Load New File
                currentReverbFile = TextFiles.ReadReverbFile(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs", lstbHashCodes.SelectedItem + ".txt"));
                lblHashCode.Text = string.Format("HashCode: 0x{0:X8}", currentReverbFile.HashCode);
                LoadReverbData();
            }
        }

        //*===============================================================================================
        //* TabControl
        //*===============================================================================================
        private void TabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReverbData();
        }

        //*===============================================================================================
        //* BUTTONS
        //*===============================================================================================
        private void BtnRenameSelected_Click(object sender, EventArgs e)
        {
            //Ask user for a name
            if (lstbHashCodes.SelectedItem != null)
            {
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename Reverb" })
                {
                    inputDiag.lblText.Text = string.Format("Enter New Name For '{0}'", lstbHashCodes.SelectedItem.ToString());
                    inputDiag.txtInputData.Text = lstbHashCodes.SelectedItem.ToString();
                    while (true)
                    {
                        if (inputDiag.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = inputDiag.txtInputData.Text.Trim();
                            if (string.IsNullOrEmpty(fileName))
                            {
                                break;
                            }
                            else
                            {
                                string newFilePath = Path.Combine(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs"), fileName + ".txt");
                                if (File.Exists(newFilePath))
                                {
                                    MessageBox.Show("This HashCode Name is used. Pick another!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    //Rename file
                                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs", lstbHashCodes.SelectedItem.ToString() + ".txt");
                                    File.Move(source, newFilePath);

                                    //Update Control
                                    lstbHashCodes.Items[lstbHashCodes.SelectedIndex] = fileName;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnReMapHashCodes_Click(object sender, EventArgs e)
        {
            if (lstbHashCodes.Items.Count > 0)
            {
                if (MessageBox.Show("Are you sure you wish to ReMap the Reverb HashCodes?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Reset Global Var
                    GlobalPrefs.ReverbHashCodeNumber = 0;

                    //Update Files
                    string[] filesToUpdate = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs"), "*.txt", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < filesToUpdate.Length; i++)
                    {
                        ReverbFile fileData = TextFiles.ReadReverbFile(filesToUpdate[i]);
                        fileData.HashCode = ++GlobalPrefs.ReverbHashCodeNumber;
                        TextFiles.WriteReverbFile(fileData, filesToUpdate[i]);
                    }

                    //Select First item
                    lstbHashCodes.ClearSelected();
                    lstbHashCodes.SelectedIndex = 0;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (lstbHashCodes.SelectedItems.Count == 1)
            {
                if (MessageBox.Show(string.Format("Delete Reverb '{0}'?", lstbHashCodes.SelectedItem), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs", lstbHashCodes.SelectedItem + ".txt");
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                        while (lstbHashCodes.SelectedItems.Count > 0)
                        {
                            lstbHashCodes.Items.Remove(lstbHashCodes.SelectedItems[0]);
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnCopySelected_Click(object sender, EventArgs e)
        {
            if (lstbHashCodes.SelectedItems.Count == 1)
            {
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Copy Reverb" })
                {
                    inputDiag.lblText.Text = string.Format("Enter New Name for Copy of '{0}'", lstbHashCodes.SelectedItem);
                    inputDiag.txtInputData.Text = lstbHashCodes.SelectedItem.ToString();
                    while (true)
                    {
                        if (inputDiag.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = inputDiag.txtInputData.Text.Trim();
                            if (string.IsNullOrEmpty(fileName))
                            {
                                break;
                            }
                            else
                            {
                                string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs");
                                string newFilePath = Path.Combine(folderPath, fileName + ".txt");
                                if (File.Exists(newFilePath))
                                {
                                    MessageBox.Show("This HashCode Name is used. Pick another!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    //Copy file
                                    string filePath = Path.Combine(folderPath, lstbHashCodes.SelectedItem + ".txt");
                                    File.Copy(filePath, newFilePath);

                                    //Update UI
                                    lstbHashCodes.ClearSelected();
                                    lstbHashCodes.Items.Add(fileName);
                                    lstbHashCodes.SelectedItem = fileName;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New Reverb" })
            {
                string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs");

                inputDiag.lblText.Text = "Enter Name";
                inputDiag.txtInputData.Text = MultipleFilesFunctions.GetNextAvailableFilename(folderPath, "RVB_ROOM_");
                while (true)
                {
                    if (inputDiag.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = inputDiag.txtInputData.Text.Trim();
                        if (string.IsNullOrEmpty(fileName))
                        {
                            break;
                        }
                        else
                        {
                            string filePath = Path.Combine(folderPath, fileName + ".txt");
                            if (File.Exists(filePath))
                            {
                                MessageBox.Show(string.Format("Label '{0}' already exists please use another name!", fileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //Create and write reverb file
                                ReverbFile rvbFile = new ReverbFile
                                {
                                    HashCode = ++GlobalPrefs.ReverbHashCodeNumber
                                };
                                TextFiles.WriteReverbFile(rvbFile, filePath);

                                //Update UI
                                lstbHashCodes.ClearSelected();
                                lstbHashCodes.Items.Add(fileName);
                                lstbHashCodes.SelectedItem = fileName;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPlayTest_Click(object sender, EventArgs e)
        {
            //Get output folder & name
            string outputFilePath = CommonFunctions.GetSoundbankOutPath(projectSettings, "PC", "English");
            string fileName = string.Format("HC{0:X6}.SFX", CommonFunctions.GetSfxName((int)Enumerations.Language.English, 0xFFFE));

            //Calculate Effect values
            int cutoff_frequency = (int)(300 + (22050 - 300) * decimal.Divide(trkBarLowPassFilter.Value, trkBarLowPassFilter.Maximum));

            //Call DLL
            string filePath = Path.Combine(outputFilePath, fileName);
            if (File.Exists(filePath))
            {
                if (PCAudioDll.IsSoundBankLoaded(0xFFFE))
                {
                    PCAudioDll.UnloadSoundbank();
                }
                PCAudioDll.LoadSoundBank(filePath);
                PCAudioDll.PlaySfx(0, cutoff_frequency);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* TRACKBARS
        //*===============================================================================================
        private void TrkBarRoomSize_Scroll(object sender, EventArgs e)
        {
            toolTipTrackBars.SetToolTip(trkBarRoomSize, trkBarRoomSize.Value.ToString());
            SaveReverbData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TrkBarWidth_Scroll(object sender, EventArgs e)
        {
            toolTipTrackBars.SetToolTip(trkBarWidth, trkBarWidth.Value.ToString());
            SaveReverbData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TrkBarDamp_Scroll(object sender, EventArgs e)
        {
            toolTipTrackBars.SetToolTip(trkBarDamp, trkBarDamp.Value.ToString());
            SaveReverbData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TrkBarLowPassFilter_Scroll(object sender, EventArgs e)
        {
            toolTipTrackBars.SetToolTip(trkBarLowPassFilter, trkBarLowPassFilter.Value.ToString());
            SaveReverbData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TrkBarFilter1_Scroll(object sender, EventArgs e)
        {
            toolTipTrackBars.SetToolTip(trkBarFilter1, trkBarFilter1.Value.ToString());
            SaveReverbData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TrkBarFilter2_Scroll(object sender, EventArgs e)
        {
            toolTipTrackBars.SetToolTip(trkBarFilter2, trkBarFilter2.Value.ToString());
            SaveReverbData();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadReverbData()
        {
            if (currentReverbFile != null)
            {
                switch (tabCtrl.SelectedTab.Text)
                {
                    case "PC":
                        trkBarRoomSize.Value = currentReverbFile.PCReverb.RoomSize;
                        trkBarWidth.Value = currentReverbFile.PCReverb.Width;
                        trkBarDamp.Value = currentReverbFile.PCReverb.Damp;
                        trkBarLowPassFilter.Value = currentReverbFile.PCReverb.LowPassFilter;
                        trkBarFilter1.Value = currentReverbFile.PCReverb.Filter1;
                        trkBarFilter2.Value = currentReverbFile.PCReverb.Filter2;
                        break;
                    case "XBox":
                        trkBarRoomSize.Value = currentReverbFile.XBReverb.RoomSize;
                        trkBarWidth.Value = currentReverbFile.XBReverb.Width;
                        trkBarDamp.Value = currentReverbFile.XBReverb.Damp;
                        trkBarLowPassFilter.Value = currentReverbFile.XBReverb.LowPassFilter;
                        trkBarFilter1.Value = currentReverbFile.XBReverb.Filter1;
                        trkBarFilter2.Value = currentReverbFile.XBReverb.Filter2;
                        break;
                    case "GameCube":
                        trkBarRoomSize.Value = currentReverbFile.GCReverb.RoomSize;
                        trkBarWidth.Value = currentReverbFile.GCReverb.Width;
                        trkBarDamp.Value = currentReverbFile.GCReverb.Damp;
                        trkBarLowPassFilter.Value = currentReverbFile.GCReverb.LowPassFilter;
                        trkBarFilter1.Value = currentReverbFile.GCReverb.Filter1;
                        trkBarFilter2.Value = currentReverbFile.GCReverb.Filter2;
                        break;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SaveReverbData()
        {
            //Update file
            if (currentReverbFile != null)
            {
                askSaveChanges = true;
                switch (tabCtrl.SelectedTab.Text)
                {
                    case "PC":
                        currentReverbFile.PCReverb.RoomSize = trkBarRoomSize.Value;
                        currentReverbFile.PCReverb.Width = trkBarWidth.Value;
                        currentReverbFile.PCReverb.Damp = trkBarDamp.Value;
                        currentReverbFile.PCReverb.LowPassFilter = trkBarLowPassFilter.Value;
                        currentReverbFile.PCReverb.Filter1 = trkBarFilter1.Value;
                        currentReverbFile.PCReverb.Filter2 = trkBarFilter2.Value;
                        break;
                    case "XBox":
                        currentReverbFile.XBReverb.RoomSize = trkBarRoomSize.Value;
                        currentReverbFile.XBReverb.Width = trkBarWidth.Value;
                        currentReverbFile.XBReverb.Damp = trkBarDamp.Value;
                        currentReverbFile.XBReverb.LowPassFilter = trkBarLowPassFilter.Value;
                        currentReverbFile.XBReverb.Filter1 = trkBarFilter1.Value;
                        currentReverbFile.XBReverb.Filter2 = trkBarFilter2.Value;
                        break;
                    case "GameCube":
                        currentReverbFile.GCReverb.RoomSize = trkBarRoomSize.Value;
                        currentReverbFile.GCReverb.Width = trkBarWidth.Value;
                        currentReverbFile.GCReverb.Damp = trkBarDamp.Value;
                        currentReverbFile.GCReverb.LowPassFilter = trkBarLowPassFilter.Value;
                        currentReverbFile.GCReverb.Filter1 = trkBarFilter1.Value;
                        currentReverbFile.GCReverb.Filter2 = trkBarFilter2.Value;
                        break;
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
