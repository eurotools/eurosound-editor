//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Music App
//-------------------------------------------------------------------------------------------------------------------------------
using PCAudioDLL;
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
        private readonly PCAudio pcDll = new PCAudio();
        private readonly HashTables htFunctions = new HashTables();
        private ProjProperties projectSettings;

        //-------------------------------------------------------------------------------------------------------------------------------
        public MusicApp()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MusicMaker_Load(object sender, EventArgs e)
        {
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }

            // Begin updating the combo box so that it doesn't redraw after every item is added
            cboOutputFormat.BeginUpdate();

            // Iterate through the keys (platform names) in the platformData dictionary and add them to the combo box as options
            foreach (string outPlatform in projectSettings.platformData.Keys)
            {
                cboOutputFormat.Items.Add(outPlatform);
            }
            cboOutputFormat.Items.Add("All");
            cboOutputFormat.SelectedItem = "All";
            cboOutputFormat.EndUpdate();

            // Call the LoadData method
            LoadData();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MusicApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            pcDll.StopMusicPlayer();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwMusicFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwMusicFiles.SelectedItems.Count > 0)
            {
                nudVolume.Value = int.Parse(lvwMusicFiles.SelectedItems[0].SubItems[1].Text);
                nudUserValue.Value = int.Parse(lvwMusicFiles.SelectedItems[0].SubItems[7].Text);

                //Add jump markers for testing
                string jumpMarkers = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", lvwMusicFiles.SelectedItems[0].Text + ".jmp");
                if (File.Exists(jumpMarkers))
                {
                    lstbx_JumpMakers.Items.Clear();
                    lstbx_JumpMakers.Items.AddRange(TextFiles.ReadJumpHashCodes(jumpMarkers));
                    trackBar1.Value = (int)nudVolume.Value;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnUpdateFiles_Click(object sender, EventArgs e)
        {
            // Check if an item is selected in the listview
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                // Reset text color and error message for the selected item
                lvwMusicFiles.SelectedItems[0].SubItems[0].ForeColor = SystemColors.WindowText;
                lvwMusicFiles.SelectedItems[0].SubItems[2].Text = "No Errors";

                // Get the path of the selected MIDI file
                string midiFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".mid");
                if (File.Exists(midiFile))
                {
                    // Create a copy of the MIDI file in the "ESWork" directory
                    string midiFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", lvwMusicFiles.SelectedItems[0].Text + ".midi");
                    if (File.Exists(midiFilePath))
                    {
                        File.Delete(midiFilePath);
                    }
                    File.Copy(midiFile, midiFilePath);

                    // Set the UseItemStyleForSubItems property to false to allow changing the text color of the selected item's subitems
                    lvwMusicFiles.SelectedItems[0].UseItemStyleForSubItems = false;

                    // Check if the "CreateMarkerFile" method returns true (indicating that there are errors in the MIDI file)
                    if (CreateMarkerFile(midiFilePath))
                    {
                        // Set the text color and error message for the selected item
                        lvwMusicFiles.SelectedItems[0].SubItems[0].ForeColor = Color.Red;
                        lvwMusicFiles.SelectedItems[0].SubItems[2].Text = "Has Errors!";
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOutput_Click(object sender, EventArgs e)
        {
            // Check if MFXFiles.txt exists
            string mfxFilesPath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt");
            if (File.Exists(mfxFilesPath))
            {
                // Get a list of music files that need to be output
                List<string> filesToOutput = new List<string>();
                for (int i = 0; i < lvwMusicFiles.Items.Count; i++)
                {
                    if (lvwMusicFiles.Items[i].SubItems[4].Text.Equals("Output", StringComparison.OrdinalIgnoreCase) || lvwMusicFiles.Items[i].SubItems[5].Text.Equals("Output", StringComparison.OrdinalIgnoreCase))
                    {
                        filesToOutput.Add(lvwMusicFiles.Items[i].Text);
                    }
                }

                // Check if there are any files to output
                if (filesToOutput.Count > 0)
                {
                    // Determine the output platform
                    string[] outputPlatform = new string[] { cboOutputFormat.SelectedItem.ToString() };
                    if (cboOutputFormat.SelectedItem.ToString().Equals("All"))
                    {
                        outputPlatform = projectSettings.platformData.Keys.ToArray();
                    }

                    // Output the music files
                    if (outputPlatform.Length > 0)
                    {
                        using (MusicAppExporter exporter = new MusicAppExporter(filesToOutput.ToArray(), outputPlatform, this))
                        {
                            exporter.ShowDialog();
                            if (!Visible)
                            {
                                Show();
                            }

                            // Create and validate MfxDefines.h file
                            string finalFile = Path.Combine(projectSettings.HashCodeFileDirectory, "MFX_Defines.h");
                            string tempFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Temp_MFX_Defines.h");

                            //Create hashtable and get missing HashCodes
                            string[] missingInTempFile = htFunctions.CreateAndValidateMfxDefines(projectSettings);
                            if (missingInTempFile != null && missingInTempFile.Length > 0)
                            {
                                // Show a warning message if there are missing defines in the new MfxDefines.h file
                                string message = string.Join("\n", missingInTempFile);

                                //Truncate string if required
                                if (message.Length > 914)
                                {
                                    message = message.Substring(0, 914);
                                }
                                if (MessageBox.Show(string.Format("{0}:\n\n{1}\n\nAre you sure you wish to overwrite this file?", "The following defines are missing from the new MFX_Defines.h file", message), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    //Copy the temporal file to the final output folder (Sonix)
                                    if (File.Exists(tempFile) && File.Exists(finalFile))
                                    {
                                        File.Delete(finalFile);
                                        File.Copy(tempFile, finalFile);
                                    }
                                }
                            }
                            else
                            {
                                //Copy the temporal file to the final output folder (Sonix)
                                if (File.Exists(tempFile) && File.Exists(finalFile))
                                {
                                    File.Delete(finalFile);
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
            // Make sure that an item is selected in the list view
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                // Get the properties file path
                string propsFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", lvwMusicFiles.SelectedItems[0].Text + ".txt");

                // Read the music file data from the properties file
                MusicFile fileData = new MusicFile();
                if (File.Exists(propsFile))
                {
                    fileData = TextFiles.ReadMusicFile(propsFile);
                }

                // If the volume has changed, update the file data and the list view
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
            // Check if a single item is selected in the list view
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                // Get the path to the base INI file
                string baseIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                if (File.Exists(baseIniFilePath))
                {
                    // Read the INI file
                    IniFile iniFile = new IniFile(baseIniFilePath);
                    // Get the path to the text editor specified in the INI file
                    string TextEditorPath = iniFile.Read("TextEditor", "PropertiesForm");
                    if (File.Exists(TextEditorPath))
                    {
                        try
                        {
                            // Get the path to the error file
                            string errorFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", lvwMusicFiles.SelectedItems[0].Text + ".err");
                            // Open the error file with the text editor
                            Process.Start(TextEditorPath, errorFilePath);
                        }
                        catch (Exception ex)
                        {
                            // Show an error message if an exception occurs
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Show a message if no text editor is setup
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
            // Check if MFXFiles.txt exists
            string mfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt");
            if (File.Exists(mfxFilePath))
            {
                // Check if there are any items in the list view
                if (lvwMusicFiles.Items.Count > 0)
                {
                    // Check if there are any missing defines in the temp MFX_Defines.h file
                    string[] missingInTempFile = htFunctions.CreateAndValidateMfxDefines(projectSettings);
                    if (missingInTempFile != null && missingInTempFile.Length > 0)
                    {
                        // Truncate message if necessary
                        string message = string.Join("\n", missingInTempFile);
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
        private void NudUserValue_ValueChanged(object sender, EventArgs e)
        {
            // Check if a single item is selected in the music file list
            if (lvwMusicFiles.SelectedItems.Count == 1)
            {
                // Get the path of the selected music file's properties file
                string musicFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", lvwMusicFiles.SelectedItems[0].Text + ".txt");

                // Initialize the music file data object with default values
                MusicFile musicFileData = new MusicFile();

                // If the properties file exists, read the music file data from it
                if (File.Exists(musicFilePath))
                {
                    musicFileData = TextFiles.ReadMusicFile(musicFilePath);
                }

                // If the user value of the music file has changed
                if (musicFileData.UserValue != nudUserValue.Value)
                {
                    // Update the user value displayed in the list view
                    lvwMusicFiles.SelectedItems[0].SubItems[1].Text = nudUserValue.Value.ToString();
                    lvwMusicFiles.SelectedItems[0].SubItems[4].Text = "Output";

                    // Update the user value in the music file data object
                    musicFileData.UserValue = (uint)nudUserValue.Value;
                    musicFileData.MidiFileLastOutput = "99";

                    // Write the updated music file data to the properties file
                    TextFiles.WriteMusicFile(musicFileData, musicFilePath);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------<
        private void LoadData()
        {
            // Open the MFXFiles.txt file for writing and truncate it
            using (StreamWriter mfxFileWriter = new StreamWriter(File.Open(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                // Write the header line to the MFXFiles.txt file
                mfxFileWriter.WriteLine("#MFXFiles");

                // Get the path to the project's Music folder
                string musicFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "Music");

                // Get an array of all the .wav files in the Music folder
                string[] waveFiles = Directory.GetFiles(musicFolderPath, "*.wav", SearchOption.TopDirectoryOnly);

                // Iterate through each .wav file
                for (int i = 0; i < waveFiles.Length; i++)
                {
                    // Get the path to the .mrk file for the current .wav file
                    string markerFilePath = Path.ChangeExtension(waveFiles[i], ".mrk");

                    // Get the path to the .mid file for the current .wav file
                    string midFilePath = Path.ChangeExtension(waveFiles[i], ".mid");

                    // Check if either the .mrk file or the .mid file exists for the current .wav file
                    if (File.Exists(markerFilePath) || File.Exists(midFilePath))
                    {
                        // Get the name of the current .wav file without the extension
                        string fileName = Path.GetFileNameWithoutExtension(waveFiles[i]);

                        // Create a ListViewItem to represent the current .wav file
                        ListViewItem itemToAdd = new ListViewItem(new string[] { fileName, "##", "No Errors", "##", "Output", "Output", "#####", "##" });

                        // Get the path to the .txt file for the current .wav file
                        string musicFilePropertiesPath = Path.Combine(musicFolderPath, "ESData", fileName + ".txt");

                        // Check if the .txt file for the current .wav file exists
                        if (File.Exists(musicFilePropertiesPath))
                        {
                            // Read the properties of the current .wav file from the .txt file
                            MusicFile musicFileData = TextFiles.ReadMusicFile(musicFilePropertiesPath);

                            // If the hash code for the current .wav file is negative, assign it a new hash code
                            if (musicFileData.HashCode < 0)
                            {
                                musicFileData.HashCode = GlobalPrefs.MFXHashCodeNumber++;
                            }

                            // Update the volume and hash code columns of the ListViewItem with the values from the .txt file
                            itemToAdd.SubItems[1].Text = musicFileData.Volume.ToString();
                            itemToAdd.SubItems[3].Text = musicFileData.HashCode.ToString();

                            // Get the last write time of the .mid file for the current .wav file
                            string midiFileDate = new FileInfo(markerFilePath).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                            // Check if the last modified date of the .mid file matches the recorded output date...
                            if (musicFileData.MidiFileLastOutput.Equals(midiFileDate))
                            {
                                itemToAdd.SubItems[4].Text = "OK";
                            }
                            else
                            {
                                itemToAdd.SubItems[2].Text = "Output Required.";
                            }

                            // Get the last modified date of the .wav file
                            string waveFileDate = new FileInfo(waveFiles[i]).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                            // If the last modified date of the .wav file matches the recorded output date...
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
                            // Create a new MusicFile with default values
                            MusicFile musicFileData = new MusicFile
                            {
                                Volume = 100,
                                HashCode = GlobalPrefs.MFXHashCodeNumber++
                            };

                            // Set the volume and hash code values for the ListViewItem
                            itemToAdd.SubItems[1].Text = musicFileData.Volume.ToString();
                            itemToAdd.SubItems[2].Text = "NewFile";
                            itemToAdd.SubItems[3].Text = musicFileData.HashCode.ToString();
                            itemToAdd.SubItems[6].Text = string.Format("HC{0}.SFX", musicFileData.HashCode.ToString("X6"));
                            itemToAdd.SubItems[7].Text = musicFileData.UserValue.ToString();

                            // Write the MusicFile data to the .txt file
                            TextFiles.WriteMusicFile(musicFileData, musicFilePropertiesPath);
                        }

                        // Add the ListViewItem to the list view
                        lvwMusicFiles.Items.Add(itemToAdd);

                        // Write the file name to the MFXFiles.txt file
                        mfxFileWriter.WriteLine(fileName);
                    }
                }
                mfxFileWriter.WriteLine("#END");
                mfxFileWriter.WriteLine(string.Empty);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private bool CreateMarkerFile(string midiFile)
        {
            bool hasFatalErrors = false; // flag to track if there are fatal errors

            // Path for the MIDI text file
            string midiTextFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", Path.GetFileNameWithoutExtension(midiFile) + ".txt");

            // Convert MIDI file to text file
            CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "MIDI2TXT.exe"), string.Join(" ", "-ms", "\"" + midiFile + "\"", "\"" + midiTextFilePath + "\""), false);

            // Check if the MIDI text file exists
            if (File.Exists(midiTextFilePath))
            {
                // Create new instance of HashTables
                MidiFunctions midiClass = new MidiFunctions();

                // Open a StreamWriter to write errors to a .err file
                using (StreamWriter sw = new StreamWriter(File.Open(Path.ChangeExtension(midiTextFilePath, ".err"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    sw.WriteLine("Midi to Marker File Errors Found");
                    sw.WriteLine(string.Empty);

                    // Check for fatal errors in the marker file
                    hasFatalErrors = midiClass.CheckMarkersFatalErrors(midiClass.GetNotes(midiTextFilePath), midiClass.GetTexts(midiTextFilePath), sw);

                    // If there are no fatal errors, create the marker file
                    if (!hasFatalErrors)
                    {
                        // Path for the marker file
                        string markerFilePath = Path.ChangeExtension(midiTextFilePath, ".mrk");

                        // Check for warnings in the marker file
                        hasFatalErrors = midiClass.CheckMarkersWarnings(sw);

                        // Write the marker file
                        midiClass.WriteMarkerFile(markerFilePath);

                        // Path for the marker file in the Music folder
                        string markerFilePathDest = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".mrk");

                        // If the marker file in the Music folder already exists, delete it
                        if (File.Exists(markerFilePathDest))
                        {
                            File.Delete(markerFilePathDest);
                        }

                        // Copy the marker file to the Music folder
                        File.Copy(markerFilePath, markerFilePathDest);
                    }
                }

                // Display any errors
                for (int i = 0; i < midiClass.errorsList.Count; i++)
                {
                    MessageBox.Show(midiClass.errorsList[i], Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // If the MIDI text file doesn't exist
            {
                // Display an error message
                MessageBox.Show(string.Format("Cannot create txt version of midi file {0}\n\nEnsure that Midi files located on a local (non-Network) drive", midiFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return hasFatalErrors;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemapHashCodes_Click(object sender, EventArgs e)
        {
            //Reset variable
            GlobalPrefs.MFXHashCodeNumber = 1;

            // Iterate through all the items in the list view
            foreach (ListViewItem listItem in lvwMusicFiles.Items)
            {
                // Construct the path to the music file
                string musicFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", listItem.Text + ".txt");

                // Check if the music file exists
                if (File.Exists(musicFilePath))
                {
                    // Read the music file data
                    MusicFile musicFileData = TextFiles.ReadMusicFile(musicFilePath);

                    // Increment the global MFX hash code number and assign it to the current music file's hash code
                    musicFileData.HashCode = GlobalPrefs.MFXHashCodeNumber++;

                    // Update Error Status Column
                    listItem.SubItems[2].Text = "Output Required.";

                    // Update the hash code in the list item's subitem
                    listItem.SubItems[3].Text = musicFileData.HashCode.ToString();

                    // Update Wav & Marker Column
                    listItem.SubItems[4].Text = "Output";
                    listItem.SubItems[5].Text = "Output";

                    // Update output filename
                    listItem.SubItems[6].Text = string.Format("HC{0}.SFX", musicFileData.HashCode.ToString("X6"));

                    // Write the updated music file data to the music file
                    TextFiles.WriteMusicFile(musicFileData, musicFilePath);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
