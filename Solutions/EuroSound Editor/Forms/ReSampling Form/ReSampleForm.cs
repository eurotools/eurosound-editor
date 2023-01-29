using NAudio.Wave;
using sb_editor.Custom_Controls;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class ReSampleForm : Form
    {
        private readonly Stopwatch watcher;
        private SoundPlayer audioPlayer;
        private SamplePool samples;

        //-------------------------------------------------------------------------------------------------------------------------------
        public ReSampleForm(Stopwatch methodWatcher)
        {
            InitializeComponent();
            Width = 925;
            Height = 605;
            watcher = methodWatcher;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_ReSampleRates_Load(object sender, EventArgs e)
        {
            //Add master folder
            txtMasterFolder.Text = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");

            //Add available formats
            cboPreviewFormat.BeginUpdate();
            cboPreviewFormat.Items.Add("Original (Not Re-sampled)");
            foreach (string platformData in GlobalPrefs.CurrentProject.platformData.Keys)
            {
                cboPreviewFormat.Items.Add(platformData);
            }
            cboPreviewFormat.SelectedIndex = 0;
            cboPreviewFormat.EndUpdate();

            //Add available ReSample Rates
            cboSampleRate.BeginUpdate();
            cboSampleRate.Items.AddRange(GlobalPrefs.CurrentProject.ResampleRates.ToArray());
            if (cboSampleRate.Items.Count > 0)
            {
                cboSampleRate.SelectedIndex = 0;
            }
            cboSampleRate.EndUpdate();

            //Check for updates and Print Sample Pool
            string samplesFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt");
            if (File.Exists(samplesFilePath))
            {
                samples = TextFiles.ReadSamplesFile(samplesFilePath);
                samples.CheckForUpdates();
                SamplePoolToListView(samples);
            }

            //Restore last path
            string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(filePath))
            {
                IniFile systemFile = new IniFile(filePath);
                string lastSelectedPath = systemFile.Read("Text3", "ReSampleForm");
                if (!string.IsNullOrEmpty(lastSelectedPath))
                {
                    txtSelectionFolder.Text = lastSelectedPath;
                }
            }

            //Clear files from latest sessions
            string purgeFile = Path.Combine(GlobalPrefs.ProjectFolder, "Report", "Last_Purge.txt");
            if (File.Exists(purgeFile))
            {
                File.Delete(purgeFile);
            }

            //Stop watcher
            watcher.Stop();
            txtBootupTime.Text = string.Format("Bootup Time =  {0:0.##############}", watcher.Elapsed.TotalSeconds);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_ReSampleRates_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save TXT file
            SaveSamplesFile();

            //File Copy
            string previewFilePah = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "preview.wav");
            try
            {
                File.Delete(previewFilePah);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Resample file
            string previewResampled = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "preview_resampled.wav");
            try
            {
                File.Delete(previewResampled);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Stop playing
            StopPlayingAudio();
        }

        //*===============================================================================================
        //* LISTVIEW EVENTS
        //*===============================================================================================
        private void LvwAllSamples_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditSample();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwAllSamples_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwAllSamples.SelectedItems.Count > 0)
            {
                cboSampleRate.SelectedItem = lvwAllSamples.SelectedItems[0].SubItems[1].Text;
            }
        }

        //*===============================================================================================
        //* COMBOBOX EVENTS
        //*===============================================================================================
        private void CboSampleRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < lvwAllSamples.SelectedItems.Count; i++)
            {
                lvwAllSamples.SelectedItems[i].SubItems[1].Text = cboSampleRate.SelectedItem.ToString();
                lvwAllSamples.SelectedItems[i].SubItems[4].Text = "True";
            }
        }

        //*===============================================================================================
        //* TEXBOX EVENTS
        //*===============================================================================================
        private void TxtSelectionFolder_DoubleClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtSelectionFolder.Text = folderBrowserDialog.SelectedPath;
                IniFile systemFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
                systemFile.Write("Text3", folderBrowserDialog.SelectedPath, "ReSampleForm");
            }
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void MnuPlay_Click(object sender, EventArgs e)
        {
            PreviewSample();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuStop_Click(object sender, EventArgs e)
        {
            StopPlayingAudio();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuEdit_Click(object sender, EventArgs e)
        {
            EditSample();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuSample_Play_Click(object sender, EventArgs e)
        {
            PreviewSample();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuSample_Stop_Click(object sender, EventArgs e)
        {
            StopPlayingAudio();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuSample_Edit_Click(object sender, EventArgs e)
        {
            EditSample();
        }

        //*===============================================================================================
        //* BUTTON EVENTS
        //*===============================================================================================
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            PreviewSample();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStopPreview_Click(object sender, EventArgs e)
        {
            StopPlayingAudio();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnReSampleAll_Click(object sender, EventArgs e)
        {
            //Reload file
            string samplesFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt");
            if (File.Exists(samplesFilePath))
            {
                samples = TextFiles.ReadSamplesFile(samplesFilePath);
                SamplePoolToListView(samples);
            }

            //Update global var
            GlobalPrefs.ReSampleStreams = true;

            //Update rows
            for (int i = 0; i < lvwAllSamples.Items.Count; i++)
            {
                lvwAllSamples.Items[i].SubItems[4].Text = "True";
            }

            //Save Changes
            SaveSamplesFile();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDeReSampleAll_Click(object sender, EventArgs e)
        {
            //Reload file
            string samplesFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt");
            if (File.Exists(samplesFilePath))
            {
                samples = TextFiles.ReadSamplesFile(samplesFilePath);
                SamplePoolToListView(samples);
            }

            //Update global var
            GlobalPrefs.ReSampleStreams = false;

            //Update rows
            for (int i = 0; i < lvwAllSamples.Items.Count; i++)
            {
                lvwAllSamples.Items[i].SubItems[4].Text = "False";
            }

            //Save Changes
            SaveSamplesFile();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStreamSel_Click(object sender, EventArgs e)
        {
            GlobalPrefs.ReSampleStreams = true;
            for (int i = 0; i < lvwAllSamples.SelectedItems.Count; i++)
            {
                lvwAllSamples.SelectedItems[i].SubItems[4].Text = "True";
                lvwAllSamples.SelectedItems[i].SubItems[5].Text = "True";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSampleSel_Click(object sender, EventArgs e)
        {
            GlobalPrefs.ReSampleStreams = true;
            for (int i = 0; i < lvwAllSamples.SelectedItems.Count; i++)
            {
                lvwAllSamples.SelectedItems[i].SubItems[4].Text = "True";
                lvwAllSamples.SelectedItems[i].SubItems[5].Text = "False";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnEditSample_Click(object sender, EventArgs e)
        {
            EditSample();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMoveSelection_Click(object sender, EventArgs e)
        {
            if (txtSelectionFolder.Text.Equals("Set Folder", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Please Set Folder by Clicking TextBox.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string masterFolder = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");
                if (txtSelectionFolder.Text.Contains(masterFolder))
                {
                    if (MessageBox.Show("Are You Sure You Want to Move Selection?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Directory.Exists(txtSelectionFolder.Text))
                        {
                            // Set cursor as hourglass
                            Cursor.Current = Cursors.WaitCursor;

                            //Add files to a dictionary and check for duplicates
                            bool duplicates = false;
                            Dictionary<string, string> RenameDict = new Dictionary<string, string>();
                            List<string> tempList = new List<string>();
                            for (int i = 0; i < lvwAllSamples.SelectedItems.Count; i++)
                            {
                                string relativePath = Path.Combine(txtSelectionFolder.Text.Substring(masterFolder.Length), Path.GetFileName(lvwAllSamples.SelectedItems[i].Text));
                                string fullDestination = Path.Combine(masterFolder, relativePath.TrimStart('\\'));
                                if (File.Exists(fullDestination) || tempList.IndexOf(relativePath) > 0)
                                {
                                    duplicates = true;
                                    MessageBox.Show(string.Format("Cannot Move Files Because of duplicate Name: {0}", Path.GetFileName(fullDestination).ToUpper()), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                                else
                                {
                                    RenameDict.Add(lvwAllSamples.SelectedItems[i].Text, relativePath);
                                    tempList.Add(relativePath);
                                }
                            }

                            //Get SFX Files and Inspect them
                            if (!duplicates)
                            {
                                tempList.Clear();
                                IEnumerable<string> sfxFiles = Directory.EnumerateFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                                foreach(string sfxFile in sfxFiles)
                                {
                                    bool fileModified = false;
                                    string[] fileData = File.ReadAllLines(sfxFile);
                                    foreach (KeyValuePair<string, string> fileName in RenameDict)
                                    {
                                        int index = Array.IndexOf(fileData, fileName.Key.TrimStart('\\'));
                                        if (index > 0)
                                        {
                                            fileData[index] = fileName.Value.TrimStart('\\');
                                            fileModified = true;
                                        }
                                        else //Some files has the "\" char for error. 
                                        {
                                            index = Array.IndexOf(fileData, fileName);
                                            if (index > 0)
                                            {
                                                fileData[index] = fileName.Value.TrimStart('\\');
                                                fileModified = true;
                                            }
                                        }
                                    }

                                    //Update Text File
                                    if (fileModified)
                                    {
                                        File.WriteAllLines(sfxFile, fileData);
                                    }
                                }

                                //Move files
                                foreach (KeyValuePair<string, string> fileToMove in RenameDict)
                                {
                                    //Update Control && Move files
                                    lvwAllSamples.FindItemWithText(fileToMove.Key).Text = fileToMove.Value;
                                    File.Move(Path.Combine(masterFolder, fileToMove.Key.TrimStart('\\')), Path.Combine(masterFolder, fileToMove.Value.TrimStart('\\')));
                                }
                            }

                            // Set cursor as default arrow
                            Cursor.Current = Cursors.Default;
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Directory Not Found: {0}", txtSelectionFolder.Text), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Cannot Move Outside Master Folder: {0}", masterFolder), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //*===============================================================================================
        //* PURGE FUNCTIONS
        //*===============================================================================================
        private void BtnMakePurgeList_Click(object sender, EventArgs e)
        {
            using (ReSampleForm_CreatePurgeList purgeMaker = new ReSampleForm_CreatePurgeList(this))
            {
                if (purgeMaker.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(string.Format("Found {0} Files.", purgeMaker.filesToPurge.Length), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!Visible)
                {
                    Show();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnViewPurgedLis_Click(object sender, EventArgs e)
        {
            string purgeFile = Path.Combine(GlobalPrefs.ProjectFolder, "Report", "Last_Purge.txt");
            if (File.Exists(purgeFile))
            {
                string baseIniFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                if (File.Exists(baseIniFile))
                {
                    //Get Audio File Path
                    IniFile iniFile = new IniFile(baseIniFile);
                    string TextEditorPath = iniFile.Read("TextEditor", "PropertiesForm");
                    if (File.Exists(TextEditorPath))
                    {
                        try
                        {
                            Process.Start(TextEditorPath, purgeFile);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Text Editor setup.\nUse Properties form to setup one.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Purge file found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPurgeGo_Click(object sender, EventArgs e)
        {
            string purgeFile = Path.Combine(GlobalPrefs.ProjectFolder, "Report", "Last_Purge.txt");
            if (File.Exists(purgeFile))
            {
                if (MessageBox.Show("Are you Sure You want to Purge UnUsed Files", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (ReSampleForm_RunPurge samplesPurge = new ReSampleForm_RunPurge(this))
                    {
                        samplesPurge.ShowDialog();
                    }
                    if (!Visible)
                    {
                        lblSampleCount.Text = string.Format("Sample Count:   {0}", lvwAllSamples.Items.Count);
                        Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("No Purge file found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void PreviewSample()
        {
            //Move Samples
            if (lvwAllSamples.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", lvwAllSamples.SelectedItems[0].Text.TrimStart('\\'));
                if (File.Exists(filePath))
                {
                    //Copy Original Sample
                    if (cboPreviewFormat.SelectedIndex > 0)
                    {
                        string tempFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "preview.wav");
                        Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath));
                        File.Delete(tempFilePath);
                        File.Copy(filePath, tempFilePath);

                        //Get Sample Rate and ReSample file
                        string selectedFormat = cboPreviewFormat.SelectedItem.ToString();
                        int destinationSampleRate = GlobalPrefs.CurrentProject.platformData[selectedFormat].ReSampleRates[cboSampleRate.SelectedIndex];

                        //ReSampleFile
                        filePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "preview_resampled.wav");
                        int sampleRate;
                        using (WaveFileReader WaveReader = new WaveFileReader(tempFilePath))
                        {
                            sampleRate = WaveReader.WaveFormat.SampleRate;
                        }
                        CommonFunctions.ReSampleWithSox(tempFilePath, filePath, sampleRate, destinationSampleRate, GlobalPrefs.SoxEffect, false);
                    }

                    //Play sound
                    audioPlayer = new SoundPlayer(filePath);
                    audioPlayer.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void StopPlayingAudio()
        {
            if (audioPlayer != null)
            {
                audioPlayer.Stop();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void EditSample()
        {
            if (lvwAllSamples.SelectedItems.Count > 0)
            {
                string baseIniFile = Path.Combine(Application.StartupPath, "EuroSound.ini");
                if (File.Exists(baseIniFile))
                {
                    //Get Audio File Path
                    IniFile iniFile = new IniFile(baseIniFile);
                    string AudioEditorPath = iniFile.Read("Edit_Wavs_With", "Form7_Misc");
                    if (File.Exists(AudioEditorPath))
                    {
                        for (int i = 0; i < lvwAllSamples.SelectedItems.Count; i++)
                        {
                            try
                            {
                                Process.Start(AudioEditorPath, Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", lvwAllSamples.SelectedItems[i].Text.TrimStart('\\')));
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

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void SaveSamplesFile()
        {
            //Sort ascending by the first column
            lvwAllSamples.ListViewItemSorter = new ListViewColumnSorter(0, SortOrder.Ascending);
            lvwAllSamples.Sort();

            //Create Sample
            samples.SamplePoolItems.Clear();
            for (int i = 0; i < lvwAllSamples.Items.Count; i++)
            {
                ListViewItem listItem = lvwAllSamples.Items[i];
                SamplePoolItem sampleData = new SamplePoolItem
                {
                    ReSampleRate = listItem.SubItems[1].Text,
                    Size = listItem.SubItems[2].Text,
                    Date = listItem.SubItems[3].Text,
                    ReSample = Convert.ToBoolean(listItem.SubItems[4].Text),
                    StreamMe = Convert.ToBoolean(listItem.SubItems[5].Text),
                    ReSmp1 = listItem.SubItems[6].Text,
                    ReSmp2 = listItem.SubItems[7].Text,
                    ReSmp3 = listItem.SubItems[8].Text,
                    ReSmp4 = listItem.SubItems[9].Text
                };
                samples.SamplePoolItems.Add(listItem.Text, sampleData);
            }
            TextFiles.WriteSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"), samples);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SamplePoolToListView(SamplePool samples)
        {
            lvwAllSamples.Items.Clear();
            lvwAllSamples.BeginUpdate();
            if (samples.SamplePoolItems.Count > 0)
            {
                int i = 0;
                ListViewItem[] itemsToAdd = new ListViewItem[samples.SamplePoolItems.Count];
                foreach (KeyValuePair<string, SamplePoolItem> sampleItem in samples.SamplePoolItems)
                {
                    itemsToAdd[i++] = new ListViewItem(new string[] { sampleItem.Key, sampleItem.Value.ReSampleRate, sampleItem.Value.Size.ToString(), sampleItem.Value.Date, sampleItem.Value.ReSample.ToString(), sampleItem.Value.StreamMe.ToString(), sampleItem.Value.ReSmp1.ToString(), sampleItem.Value.ReSmp2.ToString(), sampleItem.Value.ReSmp3.ToString(), sampleItem.Value.ReSmp4.ToString() });
                }
                lvwAllSamples.Items.AddRange(itemsToAdd);
                lvwAllSamples.Items[0].Selected = true;
            }
            lvwAllSamples.EndUpdate();
            lblSampleCount.Text = string.Format("Sample Count:   {0}", lvwAllSamples.Items.Count);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
