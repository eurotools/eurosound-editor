//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Sample Pool Panel
//-------------------------------------------------------------------------------------------------------------------------------
using NAudio.Wave;
using sb_editor.Audio_Classes;
using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_SamplePool : UserControl
    {
        private SFX sfxFileData;
        private SamplePool samplesFileData;
        internal SoundPlayer audioPlayer;
        internal WaveFunctions waveClass = new WaveFunctions();

        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_SamplePool()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void LoadData(SFX sfxFile)
        {
            sfxFileData = sfxFile;
            if (File.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt")))
            {
                samplesFileData = TextFiles.ReadSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"));
            }

            //Update checkboxes
            chkEnableSubSFX.Checked = sfxFile.SamplePool.EnableSubSFX;
            chkEnableStereo.Checked = sfxFile.SamplePool.EnableStereo;

            //Clear items and fill the list
            lstSamples.Items.Clear();
            lstSamples.Items.AddRange(sfxFile.Samples.ToArray());
            if (lstSamples.Items.Count > 0)
            {
                lstSamples.SelectedIndex = 0;
            }

            //Add available formats
            cboFormat.Items.Clear();
            cboFormat.Items.AddRange(((SFXForm)Parent.Parent).projectSettings.platformData.Keys.ToArray());
            if (cboFormat.Items.Count > 0)
            {
                cboFormat.SelectedIndex = 0;
            }
        }

        //*===============================================================================================
        //* LISTBOX SAMPLE POOL
        //*===============================================================================================
        private void LstSamples_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Show info
            if (chkEnableSubSFX.Checked)
            {
                lblSampleInfo_FrequencyValue.Text = string.Empty;
                lblSampleInfo_SizeValue.Text = string.Empty;
                lblSampleInfo_LengthValue.Text = string.Empty;
                lblSampleInfo_LoopValue.Text = string.Empty;
                lblSampleInfo_StreamedValue.Text = string.Empty;
            }
            else if (!chkEnableSubSFX.Checked && lstSamples.SelectedItems.Count > 0)
            {
                //Show parameters
                SfxSample selectedSample = (SfxSample)lstSamples.SelectedItem;
                nudPitchOffset.Value = Math.Min(Math.Max(nudPitchOffset.Minimum, selectedSample.PitchOffset), nudPitchOffset.Maximum);
                nudRandomPitchOffset.Value = Math.Min(Math.Max(nudRandomPitchOffset.Minimum, selectedSample.RandomPitch), nudRandomPitchOffset.Maximum);
                nudBaseVolume.Value = Math.Min(Math.Max(nudBaseVolume.Minimum, selectedSample.BaseVolume), nudBaseVolume.Maximum);
                nudRandomVolume.Value = Math.Min(Math.Max(nudRandomVolume.Minimum, selectedSample.RandomVolume), nudRandomVolume.Maximum);
                nudPan.Value = Math.Min(Math.Max(nudPan.Minimum, selectedSample.Pan), nudPan.Maximum);
                nudRandomPan.Value = Math.Min(Math.Max(nudRandomPan.Minimum, selectedSample.RandomPan), nudRandomPan.Maximum);

                //Show sample info
                if (lstSamples.SelectedItems.Count == 1)
                {
                    string waveFilePath = Path.Combine(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder, "Master", selectedSample.FilePath.TrimStart('\\'));
                    if (File.Exists(waveFilePath))
                    {
                        WavInfo fileData = waveClass.ReadWaveProperties(waveFilePath);
                        lblSampleInfo_FrequencyValue.Text = fileData.SampleRate.ToString();
                        lblSampleInfo_SizeValue.Text = fileData.Length.ToString();
                        lblSampleInfo_LengthValue.Text = string.Format("{0:0.#}", fileData.TotalTime.TotalSeconds);
                        lblSampleInfo_LoopValue.Text = fileData.HasLoop ? "True" : "False";
                        string keyToCheck = MultipleFilesFunctions.GetFullFileName(selectedSample.FilePath);
                        if (samplesFileData != null && samplesFileData.SamplePoolItems.ContainsKey(keyToCheck))
                        {
                            lblSampleInfo_StreamedValue.Text = samplesFileData.SamplePoolItems[keyToCheck].StreamMe.ToString();
                        }
                        else
                        {
                            lblSampleInfo_StreamedValue.Text = "??";
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("File Not Found: {0}", waveFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //Update Sample Properties Control
                    foreach (Control controlToModify in grbSampleProperties.Controls)
                    {
                        if (controlToModify.GetType() == typeof(Label))
                        {
                            ((Label)controlToModify).BackColor = SystemColors.Control;
                            ((Label)controlToModify).ForeColor = SystemColors.ControlText;
                        }
                    }
                }
                else
                {
                    lblSampleInfo_FrequencyValue.Text = "--";
                    lblSampleInfo_SizeValue.Text = "--";
                    lblSampleInfo_LengthValue.Text = "--";
                    lblSampleInfo_LoopValue.Text = "--";
                    lblSampleInfo_StreamedValue.Text = "--";

                    //Update Sample Properties Control
                    foreach (Control controlToModify in grbSampleProperties.Controls)
                    {
                        if (controlToModify.GetType() == typeof(Label))
                        {
                            ((Label)controlToModify).BackColor = Color.Black;
                            ((Label)controlToModify).ForeColor = SystemColors.HighlightText;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstSamples_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstSamples_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                string[] itemsData = new string[0];
                if (chkEnableSubSFX.Checked)
                {
                    //Get string array of the items we must add
                    if (e.Data.GetDataPresent(typeof(ListBox.SelectedObjectCollection)))
                    {
                        itemsData = ((ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection))).OfType<string>().ToArray();

                    }
                    else if (e.Data.GetDataPresent(typeof(string[])))
                    {
                        itemsData = (string[])e.Data.GetData(typeof(string[]));
                    }

                    //Add item avoiding duplicates
                    for (int i = 0; i < itemsData.Length; i++)
                    {
                        SfxSample sampleObj = new SfxSample()
                        {
                            FilePath = itemsData[i].ToString(),
                            PitchOffset = 0,
                            RandomPitch = 0,
                            BaseVolume = 0,
                            RandomVolume = 0,
                            Pan = 0,
                            RandomPan = 0
                        };
                        lstSamples.Items.Add(sampleObj);
                        sfxFileData.Samples = lstSamples.Items.OfType<SfxSample>().ToList();
                    }
                }
                else
                {
                    itemsData = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (itemsData != null)
                    {
                        //Add item avoiding duplicates
                        for (int i = 0; i < itemsData.Length; i++)
                        {
                            AddItemToSamplePool(itemsData[i]);
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstSamples_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ContextActioEdit();
        }

        //*===============================================================================================
        //* CHECKBOXES
        //*===============================================================================================
        private void ChkEnableSubSFX_CheckedChanged(object sender, System.EventArgs e)
        {
            // Get a reference to the SFXParameters control
            UserControl_SFX_Parameters sfxParametersControl = ((SFXForm)Parent.Parent).UserControl_SFX_Parameters;

            // Toggle the visibility and enabled state of the LockedOnAllFormats group box
            sfxParametersControl.grbLockedOnAllFormats.Visible = !sfxParametersControl.grbLockedOnAllFormats.Visible;
            sfxParametersControl.grbLockedOnAllFormats.Enabled = !sfxParametersControl.grbLockedOnAllFormats.Enabled;

            // Toggle the visibility and enabled state of the SampleProperties group box
            grbSampleProperties.Visible = !grbSampleProperties.Visible;
            grbSampleProperties.Enabled = !grbSampleProperties.Enabled;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkEnableSubSFX_Click(object sender, System.EventArgs e)
        {
            // Check if there are items in the sample list
            if (lstSamples.Items.Count > 0)
            {
                // If there are items in the sample list, uncheck the enable subsfx checkbox
                chkEnableSubSFX.Checked = false;
                ((SFXForm)Parent.Parent).pnlAlert.Visible = true;
                ((SFXForm)Parent.Parent).tmrTabPageBlink.Start();

                // Show an error message
                if (MessageBox.Show("Sample Pool File List Must be empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    ((SFXForm)Parent.Parent).pnlAlert.Visible = false;
                    ((SFXForm)Parent.Parent).tmrTabPageBlink.Stop();
                    ((SFXForm)Parent.Parent).tabCtrl.Refresh();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkEnableStereo_CheckedChanged(object sender, EventArgs e)
        {
            grbSampleProperties.Visible = !grbSampleProperties.Visible;
            grbSampleProperties.Enabled = !grbSampleProperties.Enabled;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkEnableStereo_CheckStateChanged(object sender, EventArgs e)
        {
            // If the sample pool has items, display an error message and prevent the stereo mode from being enabled
            if (lstSamples.Items.Count > 0)
            {
                chkEnableSubSFX.Checked = false;
                ((SFXForm)Parent.Parent).pnlAlert.Visible = true;
                ((SFXForm)Parent.Parent).tmrTabPageBlink.Start();
                if (MessageBox.Show("Sample Pool File List Must be empty!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    ((SFXForm)Parent.Parent).pnlAlert.Visible = false;
                    ((SFXForm)Parent.Parent).tmrTabPageBlink.Stop();
                    ((SFXForm)Parent.Parent).tabCtrl.Refresh();
                }
            }
        }

        //*===============================================================================================
        //* SAMPLE POOL - CONTEXT MENU
        //*===============================================================================================
        private void MnuAdd_Click(object sender, EventArgs e)
        {
            ContextActionAdd();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRemove_Click(object sender, EventArgs e)
        {
            ContextActionRemove();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuCopy_Click(object sender, EventArgs e)
        {
            ContextActionCopy();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuOpen_Click(object sender, EventArgs e)
        {
            ContextActionOpen();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuEdit_Click(object sender, EventArgs e)
        {
            ContextActioEdit();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuPlay_Click(object sender, EventArgs e)
        {
            ContextActionPlay();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuStop_Click(object sender, EventArgs e)
        {
            ContextActionStop();
        }

        //*===============================================================================================
        //* SAMPLE POOL BUTTONS
        //*===============================================================================================
        private void BtnMoveUp_Click(object sender, System.EventArgs e)
        {
            // Get the indices of the selected items in the list
            int[] indexes = lstSamples.SelectedIndices.Cast<int>().ToArray();

            // If there are items selected and they are not already at the top of the list
            if (indexes.Length > 0 && indexes[0] > 0)
            {
                // Loop through the items in the list
                for (int i = 0; i < lstSamples.Items.Count; ++i)
                {
                    // If the current item is selected
                    if (indexes.Contains(i))
                    {
                        // Remove the item from the list and insert it one position above its current position
                        object moveItem = lstSamples.Items[i];
                        lstSamples.Items.Remove(moveItem);
                        lstSamples.Items.Insert(i - 1, moveItem);
                        lstSamples.SetSelected(i - 1, true);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMoveDown_Click(object sender, System.EventArgs e)
        {
            // Get the selected indices in the list
            int[] indexes = lstSamples.SelectedIndices.Cast<int>().ToArray();

            // Check if there are any selected items, and if the last selected item is not already the last item in the list
            if (indexes.Length > 0 && indexes[indexes.Length - 1] < lstSamples.Items.Count - 1)
            {
                // Iterate through the list in reverse order
                for (int i = lstSamples.Items.Count - 1; i > -1; --i)
                {
                    // If the current index is in the list of selected indices
                    if (indexes.Contains(i))
                    {
                        // Save the item to move
                        object moveItem = lstSamples.Items[i];
                        lstSamples.Items.Remove(moveItem);
                        lstSamples.Items.Insert(i + 1, moveItem);
                        lstSamples.SetSelected(i + 1, true);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ContextActionAdd();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            ContextActionRemove();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            ContextActionCopy();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            ContextActionOpen();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            ContextActioEdit();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            ContextActionPlay();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStop_Click(object sender, EventArgs e)
        {
            ContextActionStop();
        }

        //*===============================================================================================
        //* SAMPLE PROPERTIES
        //*===============================================================================================
        private void NudPitchOffset_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
            {
                ((SfxSample)lstSamples.SelectedItems[i]).PitchOffset = nudPitchOffset.Value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudRandomPitchOffset_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
            {
                ((SfxSample)lstSamples.SelectedItems[i]).RandomPitch = nudRandomPitchOffset.Value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudBaseVolume_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
            {
                ((SfxSample)lstSamples.SelectedItems[i]).BaseVolume = (sbyte)nudBaseVolume.Value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudRandomVolume_ValueChanged(object sender, EventArgs e)
        {
            // Get the SFX Parameters control from the parent form
            UserControl_SFX_Parameters sfxParametersControl = ((SFXForm)Parent.Parent).UserControl_SFX_Parameters;

            // Check if the "Steal On Louder" option is checked and the new random volume value is not 0
            if (sfxParametersControl.chkStealOnLouder.Checked && nudRandomVolume.Value != 0)
            {
                // Show a message warning the user that these options cannot be used together
                MessageBox.Show("Steal On Louder & Random Volume NOT allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudRandomVolume.Value = 0;
            }
            else
            {
                // Set the random volume value for each selected sample
                for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
                {
                    ((SfxSample)lstSamples.SelectedItems[i]).RandomVolume = (sbyte)nudRandomVolume.Value;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudPan_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
            {
                ((SfxSample)lstSamples.SelectedItems[i]).Pan = (sbyte)nudPan.Value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudRandomPan_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
            {
                ((SfxSample)lstSamples.SelectedItems[i]).RandomPan = (sbyte)nudRandomPan.Value;
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void ContextActionAdd()
        {
            // Get the parent form
            SFXForm mainForm = (SFXForm)Parent.Parent;

            // Check if sub-sfx is enabled
            if (chkEnableSubSFX.Checked)
            {
                // Open the hash codes selector
                mainForm.OpenHashCodesSelector(mainForm.DesktopLocation);
            }
            else
            {
                //Start blinking
                if (mainForm.tabCtrl.SelectedIndex == 0)
                {
                    mainForm.pnlAlert.Visible = true;
                }
                mainForm.tmrTabPageBlink.Start();

                // Set the initial directory for the open file dialog to the "Master" folder inside the project's sample files folder
                OpenFileDiag_Samples.InitialDirectory = Path.Combine(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder, "Master");

                // Show the open file dialog
                if (OpenFileDiag_Samples.ShowDialog() == DialogResult.OK)
                {
                    // Loop through all the selected files
                    for (int i = 0; i < OpenFileDiag_Samples.FileNames.Length; i++)
                    {
                        // Add the selected file to the sample pool
                        AddItemToSamplePool(OpenFileDiag_Samples.FileNames[i]);
                    }
                }

                //Stop Blinking
                if (mainForm.tabCtrl.SelectedIndex == 0)
                {
                    mainForm.pnlAlert.Visible = false;
                }
                mainForm.tmrTabPageBlink.Stop();
                mainForm.tabCtrl.Refresh();
            }

            // Enable the random pick option if there are more than 1 item in the sample pool
            mainForm.UserControl_SamplePoolControl.chkRandomPick.Checked = lstSamples.Items.Count > 1;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionRemove()
        {
            // Check if there are any selected items in the list
            if (lstSamples.SelectedItems.Count > 0)
            {
                // Store the index of the currently selected item
                int selectedIndex = lstSamples.SelectedIndex;

                // Remove all the selected items from the list
                for (int i = lstSamples.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    int idx = lstSamples.SelectedIndices[i];
                    lstSamples.Items.RemoveAt(idx);
                }

                // If the index of the previously selected item is still within the range of the list, set it as the currently selected item again
                if (selectedIndex < lstSamples.Items.Count)
                {
                    lstSamples.SelectedIndex = selectedIndex;
                }

                // If there is more than one item in the list, set the random pick checkbox to true, otherwise set it to false
                ((SFXForm)Parent.Parent).UserControl_SamplePoolControl.chkRandomPick.Checked = lstSamples.Items.Count > 1;

                // Update the list of samples in the sfxFileData object
                sfxFileData.Samples = lstSamples.Items.OfType<SfxSample>().ToList();
            }
            else
            {
                // If there are no selected items, play the system beep sound
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionCopy()
        {
            // Check if there are any items selected in the list
            if (lstSamples.SelectedItems.Count > 0)
            {
                // Add a copy of all selected items to the list
                lstSamples.Items.AddRange(lstSamples.SelectedItems.OfType<SfxSample>().ToArray());

                // Update the list of samples in the sfx file data object
                sfxFileData.Samples = lstSamples.Items.OfType<SfxSample>().ToList();
            }
            else
            {
                // Play an error sound if no items are selected
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionOpen()
        {
            if (!chkEnableSubSFX.Checked && lstSamples.SelectedItems.Count == 1)
            {
                string fullFilePath = Path.Combine(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder, "Master", ((SfxSample)lstSamples.SelectedItem).FilePath);
                try
                {
                    Process.Start("explorer.exe", Path.GetDirectoryName(fullFilePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActioEdit()
        {
            //Check if Sub-SFXs are enabled
            if (chkEnableSubSFX.Checked)
            {
                if (lstSamples.SelectedItems.Count == 1)
                {
                    //Get selected SFX hashcode
                    string sfxFileName = ((SfxSample)lstSamples.SelectedItems[0]).FilePath;

                    //Open SFX Form with the selected SFX
                    SFXForm sfxEditor = new SFXForm(sfxFileName)
                    {
                        StartPosition = ((MainForm)Application.OpenForms[nameof(MainForm)]).StartPosition
                    };
                    sfxEditor.Show();
                }
                else
                {
                    //Error sound if no or multiple items are selected
                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                //Check if any items are selected
                if (lstSamples.SelectedItems.Count > 0)
                {
                    //Get base INI file path
                    string baseIniFile = Path.Combine(Application.StartupPath, "EuroSound.ini");

                    //Check if INI file exists
                    if (File.Exists(baseIniFile))
                    {
                        //Get Audio File Path
                        IniFile iniFile = new IniFile(baseIniFile);
                        string AudioEditorPath = iniFile.Read("Edit_Wavs_With", "Form7_Misc");

                        //Open Audio Editor
                        if (File.Exists(AudioEditorPath))
                        {
                            for (int i = 0; i < lstSamples.SelectedItems.Count; i++)
                            {
                                try
                                {
                                    //Open the selected audio file in the audio editor
                                    Process.Start(AudioEditorPath, Path.Combine(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder, "Master", ((SfxSample)lstSamples.SelectedItems[i]).FilePath));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            //Show message if no audio editor is setup
                            MessageBox.Show("No editor setup.\nUse Properties form to setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    //Error sound if no items are selected
                    SystemSounds.Beep.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionPlay()
        {
            if (!chkEnableSubSFX.Checked && lstSamples.SelectedItems.Count == 1)
            {
                //Play sound if the SubSFX checkbox is not checked and only one sample is selected
                try
                {
                    //Get the selected sample's file path
                    string soundRelativePath = ((SfxSample)lstSamples.SelectedItem).FilePath.TrimStart(Path.DirectorySeparatorChar);

                    //Create a new SoundPlayer object with the full file path of the selected sample
                    audioPlayer = new SoundPlayer(Path.Combine(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder, "Master", soundRelativePath));
                    audioPlayer.Play();
                }
                catch (Exception ex)
                {
                    //Show an error message if there was a problem playing the sound
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Play a beep if no samples are selected or more than one sample is selected
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionStop()
        {
            if (audioPlayer != null)
            {
                audioPlayer.Stop();
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void AddItemToSamplePool(string filePath)
        {
            //Check if filePath is inside SampleFilesFolder
            if (filePath.IndexOf(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder) >= 0)
            {
                try
                {
                    //Open wave file
                    using (WaveFileReader waveFile = new WaveFileReader(filePath))
                    {
                        bool addSample = true;

                        //Check if sample is 16 bits
                        if (waveFile.WaveFormat.BitsPerSample != 16)
                        {
                            addSample = false;
                            MessageBox.Show(string.Format("Sample {0} is not 16 bit.", filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //Check if sample has 1 channel
                        if (waveFile.WaveFormat.Channels != 1)
                        {
                            addSample = false;
                            MessageBox.Show(string.Format("Sample {0} is not 1 channel.", filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (addSample)
                        {
                            //Read Default Samples
                            string systemIniPath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                            if (File.Exists(systemIniPath))
                            {
                                //Get default Samples
                                IniFile systemIni = new IniFile(systemIniPath);
                                decimal PitchOffset = 0, RandomPitch = 0;
                                sbyte BaseVolume = 0, RandomVolume = 0, Pan = 0, RandomPan = 0;
                                if (decimal.TryParse(systemIni.Read("DTextNIndex_0", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out decimal PitchOffsetParsed))
                                {
                                    PitchOffset = PitchOffsetParsed;
                                }
                                if (decimal.TryParse(systemIni.Read("DTextNIndex_1", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out decimal RandomPitchParsed))
                                {
                                    RandomPitch = RandomPitchParsed;
                                }
                                if (sbyte.TryParse(systemIni.Read("DTextNIndex_2", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte BaseVolumeParsed))
                                {
                                    BaseVolume = BaseVolumeParsed;
                                }
                                if (sbyte.TryParse(systemIni.Read("DTextNIndex_3", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte RandomVolumeParsed))
                                {
                                    RandomVolume = RandomVolumeParsed;
                                }
                                if (sbyte.TryParse(systemIni.Read("DTextNIndex_4", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte PanParsed))
                                {
                                    Pan = PanParsed;
                                }
                                if (sbyte.TryParse(systemIni.Read("DTextNIndex_5", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte RandomPanParsed))
                                {
                                    RandomPan = RandomPanParsed;
                                }

                                //Add New Sample
                                string masterDir = Path.Combine(((SFXForm)Parent.Parent).projectSettings.SampleFilesFolder, "Master");
                                SfxSample sfxSample = new SfxSample
                                {
                                    FilePath = filePath.Substring(masterDir.Length + 1),
                                    PitchOffset = PitchOffset,
                                    RandomPitch = RandomPitch,
                                    BaseVolume = BaseVolume,
                                    RandomVolume = RandomVolume,
                                    Pan = Pan,
                                    RandomPan = RandomPan
                                };
                                lstSamples.Items.Add(sfxSample);
                                sfxFileData.Samples = lstSamples.Items.OfType<SfxSample>().ToList();
                            }
                            else
                            {
                                MessageBox.Show(string.Format("File Not Found '{0}'", systemIniPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(string.Format("Drag Drop Failed with File: {0}", filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
