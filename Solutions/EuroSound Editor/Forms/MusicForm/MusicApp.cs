using sb_editor.Audio_Classes;
using sb_editor.HashCodes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MusicApp : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public MusicApp()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MusicMaker_Load(object sender, EventArgs e)
        {
            //Get Output Platforms
            cboOutputFormat.BeginUpdate();
            foreach (string outPlatform in GlobalPrefs.CurrentProject.platformData.Keys)
            {
                cboOutputFormat.Items.Add(outPlatform);
            }
            cboOutputFormat.Items.Add("All");
            cboOutputFormat.SelectedItem = "All";
            cboOutputFormat.EndUpdate();

            //Load MFX Data
            LoadData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwMusicFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwMusicFiles.SelectedItems.Count > 0)
            {
                nudVolume.Value = int.Parse(lvwMusicFiles.SelectedItems[0].SubItems[1].Text);
                nudUserValue.Value = int.Parse(lvwMusicFiles.SelectedItems[0].SubItems[7].Text);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnUpdateFiles_Click(object sender, EventArgs e)
        {
            //Search for the midi file
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                //Update control
                lvwMusicFiles.SelectedItems[0].SubItems[0].ForeColor = SystemColors.WindowText;
                lvwMusicFiles.SelectedItems[0].SubItems[2].Text = "No Errors";

                //Check Midi and start conversion
                string midiFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".mid");
                if (File.Exists(midiFile))
                {
                    //Copy midi file in the working directory
                    string midiFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", lvwMusicFiles.SelectedItems[0].Text + ".midi");
                    if (File.Exists(midiFilePath))
                    {
                        File.Delete(midiFilePath);
                    }
                    File.Copy(midiFile, midiFilePath);

                    //Start process
                    lvwMusicFiles.SelectedItems[0].UseItemStyleForSubItems = false;
                    if (CreateMarkerFile(midiFilePath))
                    {
                        lvwMusicFiles.SelectedItems[0].SubItems[0].ForeColor = Color.Red;
                        lvwMusicFiles.SelectedItems[0].SubItems[2].Text = "Has Errors!";
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOutput_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt")))
            {
                //Get files to output
                List<string> filesToOutput = new List<string>();
                for (int i = 0; i < lvwMusicFiles.Items.Count; i++)
                {
                    if (lvwMusicFiles.Items[i].SubItems[4].Text.Equals("Output", StringComparison.OrdinalIgnoreCase) || lvwMusicFiles.Items[i].SubItems[5].Text.Equals("Output", StringComparison.OrdinalIgnoreCase))
                    {
                        filesToOutput.Add(lvwMusicFiles.Items[i].Text);
                    }
                }

                //Check for output files
                if (filesToOutput.Count > 0)
                {
                    //Open Output Form
                    string[] outputPlatform = new string[] { cboOutputFormat.SelectedItem.ToString() };
                    if (cboOutputFormat.SelectedItem.ToString().Equals("All"))
                    {
                        outputPlatform = GlobalPrefs.CurrentProject.platformData.Keys.ToArray();
                    }
                    if (outputPlatform.Length > 0)
                    {
                        using (MusicAppExporter exporter = new MusicAppExporter(filesToOutput.ToArray(), outputPlatform, this))
                        {
                            exporter.ShowDialog();
                            if (!Visible)
                            {
                                Show();
                            }

                            //Create MFX Hashcodes
                            string finalFile = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "MFX_Defines.h");
                            string tempFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Temp_MFX_Defines.h");
                            List<string> missingInTempFile = CreateAndValidateMfxDefines();
                            if (missingInTempFile.Count > 0)
                            {
                                string message = string.Join("\n", missingInTempFile.ToArray());
                                //Truncate string if required
                                if (message.Length > 914)
                                {
                                    message = message.Substring(0, 914);
                                }
                                if (MessageBox.Show(string.Format("{0}:\n\n{1}\n\nAre you sure you wish to overwrite this file?", "The following defines are missing from the new MFX_Defines.h file", message), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    if (File.Exists(tempFile))
                                    {
                                        if (File.Exists(finalFile))
                                        {
                                            File.Delete(finalFile);
                                        }
                                        File.Copy(tempFile, finalFile);
                                    }
                                }
                            }
                            else
                            {
                                if (File.Exists(tempFile))
                                {
                                    if (File.Exists(finalFile))
                                    {
                                        File.Delete(finalFile);
                                    }
                                    File.Copy(tempFile, finalFile);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("File Not Found: 'MFXFiles.txt'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudVolume_ValueChanged(object sender, EventArgs e)
        {
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                //Get file data
                string propsFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", lvwMusicFiles.SelectedItems[0].Text + ".txt");
                MusicFile fileData = new MusicFile();
                if (File.Exists(propsFile))
                {
                    fileData = TextFiles.ReadMusicFile(propsFile);
                }
                if (fileData.Volume != nudVolume.Value)
                {
                    //Update List
                    lvwMusicFiles.SelectedItems[0].SubItems[1].Text = nudVolume.Value.ToString();
                    lvwMusicFiles.SelectedItems[0].SubItems[4].Text = "Output";

                    //Update text file
                    fileData.Volume = (uint)nudVolume.Value;
                    fileData.MidiFileLastOutput = "99";
                    TextFiles.WriteMusicFile(fileData, propsFile);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnViewErrorFile_Click(object sender, EventArgs e)
        {
            if (lvwMusicFiles.SelectedItems.Count == 1)
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
                            string errorFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", lvwMusicFiles.SelectedItems[0].Text + ".err");
                            Process.Start(TextEditorPath, errorFilePath);
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
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnForceSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvwMusicFiles.SelectedItems.Count; i++)
            {
                lvwMusicFiles.SelectedItems[i].SubItems[4].Text = "Output";
                lvwMusicFiles.SelectedItems[i].SubItems[5].Text = "Output";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnForceOutput_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvwMusicFiles.Items.Count; i++)
            {
                lvwMusicFiles.Items[i].SubItems[4].Text = "Output";
                lvwMusicFiles.Items[i].SubItems[5].Text = "Output";
            }
        }


        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnVerifyHashCodes_Click(object sender, EventArgs e)
        {
            //Do Comparison
            if (File.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt")))
            {
                if (lvwMusicFiles.Items.Count > 0)
                {
                    List<string> missingInTempFile = CreateAndValidateMfxDefines();
                    if (missingInTempFile.Count > 0)
                    {
                        string message = string.Join("\n", missingInTempFile.ToArray());
                        //Truncate string if required
                        if (message.Length > 914)
                        {
                            message = message.Substring(0, 914);
                        }
                        MessageBox.Show(string.Format("{0}:\n\n{1}", "The following defines are missing from the new MFX_Defines.h file", message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("All checks out OK with MFX_Defines.h File.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("File Not Found: 'MFXFiles.txt'", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private List<string> CreateAndValidateMfxDefines()
        {
            List<string> missingInTempFile = new List<string>();

            //HashTables
            HashTables hashCodes = new HashTables();
            string tempFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Temp_MFX_Defines.h");
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "System")))
            {
                //Get the MFX Temporal Data
                hashCodes.CreateMfxDefines(tempFilePath);
                string[] tempFileData = File.ReadAllLines(tempFilePath);

                //Get the Final MFX Data
                if (!string.IsNullOrEmpty(GlobalPrefs.CurrentProject.HashCodeFileDirectory) && Directory.Exists(GlobalPrefs.CurrentProject.HashCodeFileDirectory))
                {
                    string mfxDefinesFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "MFX_Defines.h");
                    if (File.Exists(mfxDefinesFilePath))
                    {
                        //Check items that are missing in the final
                        string[] mfxDefinesData = File.ReadAllLines(mfxDefinesFilePath);
                        for (int i = 0; i < mfxDefinesData.Length; i++)
                        {
                            string currentLine = mfxDefinesData[i];
                            if (currentLine.StartsWith("//") || string.IsNullOrEmpty(currentLine))
                            {
                                continue;
                            }

                            //Add item to list
                            if (Array.IndexOf(tempFileData, currentLine) == -1)
                            {
                                string[] fileData = currentLine.Split(null);
                                if (fileData.Length > 2)
                                {
                                    missingInTempFile.Add(fileData[1].Trim());
                                }
                            }
                        }
                    }
                    else if (File.Exists(tempFilePath))
                    {
                        File.Copy(tempFilePath, mfxDefinesFilePath);
                    }
                }
            }

            //Valid List
            if (!string.IsNullOrEmpty(GlobalPrefs.CurrentProject.HashCodeFileDirectory) && Directory.Exists(GlobalPrefs.CurrentProject.HashCodeFileDirectory))
            {
                hashCodes.CreateMfxValidList(Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "MFX_ValidList.h"));
                hashCodes.CreateMfxData(Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "MFX_Data.h"));
                hashCodes.BuildSoundHhFile(Path.Combine(GlobalPrefs.CurrentProject.EuroLandHashCodeServerPath, "Sound.h"));
            }

            return missingInTempFile;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudUserValue_ValueChanged(object sender, EventArgs e)
        {
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                //Get file data
                string propsFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", lvwMusicFiles.SelectedItems[0].Text + ".txt");
                MusicFile fileData = new MusicFile();
                if (File.Exists(propsFile))
                {
                    fileData = TextFiles.ReadMusicFile(propsFile);
                }
                if (fileData.UserValue != nudUserValue.Value)
                {
                    //Update List
                    lvwMusicFiles.SelectedItems[0].SubItems[1].Text = nudUserValue.Value.ToString();
                    lvwMusicFiles.SelectedItems[0].SubItems[4].Text = "Output";

                    //Update text file
                    fileData.UserValue = (uint)nudUserValue.Value;
                    fileData.MidiFileLastOutput = "99";
                    TextFiles.WriteMusicFile(fileData, propsFile);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------<
        private void LoadData()
        {
            //Update MFX Files
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                sw.WriteLine("#MFXFiles");
                //Check for new files
                string[] waveFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Music"), "*.wav", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < waveFiles.Length; i++)
                {
                    //Find new Files
                    string markerFile = Path.ChangeExtension(waveFiles[i], ".mrk");
                    string midFile = Path.ChangeExtension(waveFiles[i], ".mid");
                    if (File.Exists(markerFile) || File.Exists(midFile))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(waveFiles[i]);
                        ListViewItem itemToAdd = new ListViewItem(new string[] { fileName, "##", "No Errors", "##", "Output", "Output", "#####", "##" });
                        string propsFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", fileName + ".txt");
                        if (File.Exists(propsFile))
                        {
                            MusicFile musicFileData = TextFiles.ReadMusicFile(propsFile);
                            if (musicFileData.HashCode < 0)
                            {
                                musicFileData.HashCode = GlobalPrefs.MFXHashCodeNumber++;
                            }
                            itemToAdd.SubItems[1].Text = musicFileData.Volume.ToString();
                            itemToAdd.SubItems[3].Text = musicFileData.HashCode.ToString();

                            //Check Marker File
                            string midiFileDate = new FileInfo(markerFile).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                            if (musicFileData.MidiFileLastOutput.Equals(midiFileDate))
                            {
                                itemToAdd.SubItems[4].Text = "OK";
                            }
                            else
                            {
                                itemToAdd.SubItems[2].Text = "Output Required.";
                            }

                            //Check Wave File
                            string waveFileDate = new FileInfo(waveFiles[i]).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                            if (musicFileData.WavFileLastOutput.Equals(waveFileDate))
                            {
                                itemToAdd.SubItems[5].Text = "OK";
                            }
                            else
                            {
                                itemToAdd.SubItems[2].Text = "Output Required.";
                            }
                            itemToAdd.SubItems[6].Text = string.Format("HC{0}.SFX", musicFileData.HashCode.ToString("X6"));
                            itemToAdd.SubItems[7].Text = musicFileData.UserValue.ToString();
                        }
                        else
                        {
                            MusicFile musicFileData = new MusicFile
                            {
                                Volume = 100,
                                HashCode = GlobalPrefs.MFXHashCodeNumber++
                            };
                            itemToAdd.SubItems[1].Text = musicFileData.Volume.ToString();
                            itemToAdd.SubItems[2].Text = "NewFile";
                            itemToAdd.SubItems[3].Text = musicFileData.HashCode.ToString();
                            itemToAdd.SubItems[6].Text = string.Format("HC{0}.SFX", musicFileData.HashCode.ToString("X6"));
                            itemToAdd.SubItems[7].Text = musicFileData.UserValue.ToString();
                            TextFiles.WriteMusicFile(musicFileData, propsFile);
                        }

                        //Add item to list
                        lvwMusicFiles.Items.Add(itemToAdd);
                        sw.WriteLine(fileName);
                    }
                }
                sw.WriteLine("#END");
                sw.WriteLine(string.Empty);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private bool CreateMarkerFile(string midiFile)
        {
            bool fatalErrors = false;
            string midiTextFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", Path.GetFileNameWithoutExtension(midiFile) + ".txt");
            CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "MIDI2TXT.exe"), string.Join(" ", "-ms", "\"" + midiFile + "\"", "\"" + midiTextFilePath + "\""), false);
            if (File.Exists(midiTextFilePath))
            {
                //Initialice Class
                MidiFunctions midiClass = new MidiFunctions();
                using (StreamWriter sw = new StreamWriter(File.Open(Path.ChangeExtension(midiTextFilePath, ".err"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    sw.WriteLine("Midi to Marker File Errors Found");
                    sw.WriteLine(string.Empty);

                    //Read midi file and validate
                    fatalErrors = midiClass.CheckMarkersFatalErrors(midiClass.GetNotes(midiTextFilePath), midiClass.GetTexts(midiTextFilePath), sw);
                    if (!fatalErrors)
                    {
                        string markerFile = Path.ChangeExtension(midiTextFilePath, ".mrk");
                        fatalErrors = midiClass.CheckMarkersWarnings(sw);
                        midiClass.WriteMarkerFile(markerFile);

                        //Move marker file to the final location
                        string markerFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".mrk");
                        if (File.Exists(markerFilePath))
                        {
                            File.Delete(markerFilePath);
                        }
                        File.Copy(markerFile, markerFilePath);
                    }
                }

                //Print Error Messages
                for (int i = 0; i < midiClass.errorsList.Count; i++)
                {
                    MessageBox.Show(midiClass.errorsList[i], Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(string.Format("Cannot create txt version of midi file {0}\n\nEnsure that Midi files located on a local (non-Network) drive", midiFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return fatalErrors;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemapHashCodes_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in lvwMusicFiles.Items)
            {
                string musicFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", listItem.Text + ".txt");
                if (File.Exists(musicFilePath))
                {
                    //Update HashCode
                    MusicFile musicFileData = TextFiles.ReadMusicFile(musicFilePath);
                    musicFileData.HashCode = GlobalPrefs.MFXHashCodeNumber++;

                    //Update ListView
                    listItem.SubItems[3].Text = musicFileData.HashCode.ToString();

                    //Update File
                    TextFiles.WriteMusicFile(musicFileData, musicFilePath);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
