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
            cboFormat.Items.AddRange(GlobalPrefs.CurrentProject.platformData.Keys.ToArray());
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
                    string waveFilePath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", selectedSample.FilePath.TrimStart('\\'));
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

        //*===============================================================================================
        //* CHECKBOXES
        //*===============================================================================================
        private void ChkEnableSubSFX_CheckedChanged(object sender, System.EventArgs e)
        {
            UserControl_SFX_Parameters sfxParametersControl = ((SFXForm)Parent.Parent).UserControl_SFX_Parameters;
            sfxParametersControl.grbLockedOnAllFormats.Visible = !sfxParametersControl.grbLockedOnAllFormats.Visible;
            sfxParametersControl.grbLockedOnAllFormats.Enabled = !sfxParametersControl.grbLockedOnAllFormats.Enabled;
            grbSampleProperties.Visible = !grbSampleProperties.Visible;
            grbSampleProperties.Enabled = !grbSampleProperties.Enabled;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkEnableSubSFX_Click(object sender, System.EventArgs e)
        {
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

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkEnableStereo_CheckedChanged(object sender, EventArgs e)
        {
            grbSampleProperties.Visible = !grbSampleProperties.Visible;
            grbSampleProperties.Enabled = !grbSampleProperties.Enabled;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkEnableStereo_CheckStateChanged(object sender, EventArgs e)
        {
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
            int[] indexes = lstSamples.SelectedIndices.Cast<int>().ToArray();
            if (indexes.Length > 0 && indexes[0] > 0)
            {
                for (int i = 0; i < lstSamples.Items.Count; ++i)
                {
                    if (indexes.Contains(i))
                    {
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
            int[] indexes = lstSamples.SelectedIndices.Cast<int>().ToArray();
            if (indexes.Length > 0 && indexes[indexes.Length - 1] < lstSamples.Items.Count - 1)
            {
                for (int i = lstSamples.Items.Count - 1; i > -1; --i)
                {
                    if (indexes.Contains(i))
                    {
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
            if (((SFXForm)Parent.Parent).UserControl_SFX_Parameters.chkStealOnLouder.Checked && nudRandomVolume.Value != 0)
            {
                MessageBox.Show("Steal On Louder & Random Volume NOT allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudRandomVolume.Value = 0;
            }
            else
            {
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
            if (chkEnableSubSFX.Checked)
            {
                //Show selector
                SFXForm mainForm = (SFXForm)Parent.Parent;
                mainForm.OpenHashCodesSelector(mainForm.DesktopLocation);
            }
            else
            {
                OpenFileDiag_Samples.InitialDirectory = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");
                if (OpenFileDiag_Samples.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < OpenFileDiag_Samples.FileNames.Length; i++)
                    {
                        AddItemToSamplePool(OpenFileDiag_Samples.FileNames[i]);
                    }
                }
            }

            //Check Random Pick
            ((SFXForm)Parent.Parent).UserControl_SamplePoolControl.chkRandomPick.Checked = lstSamples.Items.Count > 1;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionRemove()
        {
            if (lstSamples.SelectedItems.Count > 0)
            {
                int selectedIndex = lstSamples.SelectedIndex;

                //Remove selected
                while (lstSamples.SelectedItems.Count > 0)
                {
                    lstSamples.Items.Remove(lstSamples.SelectedItems[0]);
                }

                //Select next item
                if (selectedIndex < lstSamples.Items.Count)
                {
                    lstSamples.SelectedIndex = selectedIndex;
                }

                //Check Random Pick
                ((SFXForm)Parent.Parent).UserControl_SamplePoolControl.chkRandomPick.Checked = lstSamples.Items.Count > 1;

                //Update file
                sfxFileData.Samples = lstSamples.Items.OfType<SfxSample>().ToList();
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionCopy()
        {
            if (lstSamples.SelectedItems.Count > 0)
            {
                lstSamples.Items.AddRange(lstSamples.SelectedItems.OfType<SfxSample>().ToArray());
                sfxFileData.Samples = lstSamples.Items.OfType<SfxSample>().ToList();
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionOpen()
        {
            if (!chkEnableSubSFX.Checked && lstSamples.SelectedItems.Count == 1)
            {
                string fullFilePath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", ((SfxSample)lstSamples.SelectedItem).FilePath);
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
            if (chkEnableSubSFX.Checked)
            {
                if (lstSamples.SelectedItems.Count == 1)
                {
                }
                else
                {
                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                if (lstSamples.SelectedItems.Count > 0)
                {
                    string baseIniFile = Path.Combine(Application.StartupPath, "EuroSound.ini");
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
                                    Process.Start(AudioEditorPath, Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", ((SfxSample)lstSamples.SelectedItems[i]).FilePath));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No editor setup.\nUse Properties form to setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    SystemSounds.Beep.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ContextActionPlay()
        {
            if (!chkEnableSubSFX.Checked && lstSamples.SelectedItems.Count == 1)
            {
                //Play sound
                try
                {
                    string soundRelativePath = ((SfxSample)lstSamples.SelectedItem).FilePath.TrimStart(Path.DirectorySeparatorChar);
                    audioPlayer = new SoundPlayer(Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", soundRelativePath));
                    audioPlayer.Play();
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
            if (filePath.StartsWith(GlobalPrefs.CurrentProject.SampleFilesFolder))
            {
                try
                {
                    using (WaveFileReader waveFile = new WaveFileReader(filePath))
                    {
                        bool addSample = true;
                        if (waveFile.WaveFormat.BitsPerSample != 16)
                        {
                            addSample = false;
                            MessageBox.Show(string.Format("Sample {0} is not 16 bit.", filePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
                                string masterDir = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");
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
