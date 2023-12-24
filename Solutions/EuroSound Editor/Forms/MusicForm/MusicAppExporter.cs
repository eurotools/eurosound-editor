//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Music SFXs Exporter
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using ExMarkers;
using MusX.Writers;
using sb_editor.Audio_Classes;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MusicAppExporter : TimerForm
    {
        private readonly string[] filesQueue;
        private readonly string[] outputPlatforms;
        private readonly MusicApp parentFormObj;
        private readonly Stopwatch outputTimer = new Stopwatch();
        private MidiFunctions midiClass;
        private readonly ProjProperties projectSettings;

        //-------------------------------------------------------------------------------------------------------------------------------
        public MusicAppExporter(string[] outputFiles, string[] outPlatforms, MusicApp parentForm)
        {
            InitializeComponent();
            filesQueue = outputFiles;
            parentFormObj = parentForm;
            outputPlatforms = outPlatforms;
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MusicMaker_CreateMarkers_Shown(object sender, EventArgs e)
        {
            parentFormObj.Hide();
            if (!backgroundWorker1.IsBusy)
            {
                //Run Bat scripts
                CommonFunctions.RunOutputScripts(Path.Combine(GlobalPrefs.ProjectFolder, "System", "PreOutput.bat"), "rem Add your pre-output stuff here");

                outputTimer.Start();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MusicMaker_CreateMarkers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                e.Cancel = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //--------------------------------------------------[Midi to Marker File]--------------------------------------------------
            for (int i = 0; i < filesQueue.Length; i++)
            {
                // Get the name and path of the MIDI file
                string midiFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".mid");

                // Check if the MIDI file exists
                if (File.Exists(midiFilePath))
                {
                    backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));

                    // Copy the MIDI file to the working directory
                    string midiWorkFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".midi");
                    if (File.Exists(midiWorkFilePath))
                    {
                        File.Delete(midiWorkFilePath);
                    }
                    File.Copy(midiFilePath, midiWorkFilePath);
                    backgroundWorker1.ReportProgress(25, string.Format("Making Marker File: {0}", filesQueue[i]));

                    // Convert the MIDI file to a text file using MIDI2TXT.exe
                    CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "MIDI2TXT.exe"), string.Join(" ", "-ms", "\"" + midiFilePath + "\"", "\"" + midiWorkFilePath + "\""), false);

                    // Check if the converted text file exists
                    if (File.Exists(midiWorkFilePath))
                    {
                        midiClass = new MidiFunctions();

                        // Open a stream writer to write any errors found during the conversion process to a .err file
                        using (StreamWriter sw = new StreamWriter(File.Open(Path.ChangeExtension(midiWorkFilePath, ".err"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                        {
                            sw.WriteLine("Midi to Marker File Errors Found");
                            sw.WriteLine(string.Empty);

                            // Read the converted text file and check for any fatal errors
                            backgroundWorker1.ReportProgress(50, string.Format("Making Marker File: {0}", filesQueue[i]));
                            bool fatalErrors = midiClass.CheckMarkersFatalErrors(midiClass.GetNotes(midiWorkFilePath), midiClass.GetTexts(midiWorkFilePath), sw);

                            // If fatal errors are found, update the UI and throw an exception
                            if (fatalErrors)
                            {
                                ListViewItem result = parentFormObj.lvwMusicFiles.FindItemWithText(filesQueue[i]);
                                if (result != null)
                                {
                                    result.UseItemStyleForSubItems = false;
                                    result.SubItems[0].ForeColor = Color.Red;
                                    result.SubItems[2].Text = "Has Errors!";
                                }
                                throw new Exception(string.Join("\n", "Errors Found in Midi files During output", "these files have no been output yet.", "MFX File output aborted!"));
                            }
                            else
                            {
                                // If no fatal errors are found, create the marker file and check for any warning errors
                                string markerFilePath = Path.ChangeExtension(midiWorkFilePath, ".mrk");
                                fatalErrors = midiClass.CheckMarkersWarnings(sw);
                                midiClass.WriteMarkerFile(markerFilePath);

                                // Move the marker file to the final location
                                string finalMarkerFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".mrk");
                                if (File.Exists(finalMarkerFilePath))
                                {
                                    File.Delete(finalMarkerFilePath);
                                }
                                File.Copy(markerFilePath, finalMarkerFilePath);
                            }
                            backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                        }
                    }
                }
            }

            //--------------------------------------------------[Stream Files and Marker Files]--------------------------------------------------
            List<int> mfxList = new List<int>();
            string SoxPath = Path.Combine(Application.StartupPath, "SystemFiles", "SOX.EXE");

            // Create objects for handling audio markers and various audio data formats
            StreamMarkerFiles streamMarkers = new StreamMarkerFiles();
            ImaFunctions imaClass = new ImaFunctions();
            WaveFunctions waveData = new WaveFunctions();

            // Iterate through the list of audio files to be processed
            for (int i = 0; i < filesQueue.Length; i++)
            {
                // Initialize variables for the current music file
                bool outputImaData = true;  // flag to indicate if we need to output a .wav file for IMA ADPCM encoding (PC & GameCube)

                //Get File Data
                string musicFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", filesQueue[i] + ".txt");
                MusicFile musicFileData = new MusicFile();
                if (File.Exists(musicFilePath))
                {
                    musicFileData = TextFiles.ReadMusicFile(musicFilePath);
                }
                if (musicFileData.HashCode < 0)
                {
                    musicFileData.HashCode = GlobalPrefs.MFXHashCodeNumber++;
                }

                // Calculate the folder index based on the music file hash code
                int folderIndex = (musicFileData.HashCode & 0xF0) >> 4;

                // Declare paths for the marker file and wave file for the current music file
                string markerFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".mrk");
                string wavePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".wav");
                string aslFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".asl");
                string asrFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".asr");

                // Initialize arrays for storing left and right audio data in IMA format
                byte[] imaLeftChannelData = null;
                byte[] imaRightChannelData = null;

                // Iterate through the list of output platforms
                for (int j = 0; j < outputPlatforms.Length; j++)
                {
                    // Create a path to store the MFX file for the current platform
                    string outputFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", outputPlatforms[j], "Music", "MFX_" + folderIndex);
                    Directory.CreateDirectory(outputFolder);

                    // Get the paths of the sound sample data file and sound marker file for the current platform and music file
                    string soundSampleDataFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                    string soundMarkerFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".smf");
                    string wavLeftChannelData, wavRightChannelData;

                    // Only create the audio file if the "output only marker file" option is not selected
                    if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                    {
                        // Convert the wave file to the required format for the current platform
                        switch (outputPlatforms[j].ToLower())
                        {
                            case "playstation2":
                                // Define the paths for the left and right channel WAV files
                                wavLeftChannelData = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + "L.wav");
                                wavRightChannelData = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + "R.wav");

                                // Split the wave file into left and right channels and re-sample the wave files to 32000 Hz
                                backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -l", wavePath, wavLeftChannelData), false);
                                backgroundWorker1.ReportProgress(25, string.Format("Making Music Stream: {0} for PlayStation2", filesQueue[i]));
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -r", wavePath, wavRightChannelData), false);

                                // Convert the split wave files to AIFF format
                                string aiffLeftChannelData = Path.ChangeExtension(wavLeftChannelData, ".aif");
                                string aiffRightChannelData = Path.ChangeExtension(wavRightChannelData, ".aif");
                                backgroundWorker1.ReportProgress(75, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" \"{1}\"", wavLeftChannelData, aiffLeftChannelData), false);
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" \"{1}\"", wavRightChannelData, aiffRightChannelData), false);

                                // Convert the AIFF files to PS2 VAG format
                                string vagTool = Path.Combine(Application.StartupPath, "SystemFiles", "AIFF2VAG.EXE");
                                backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(vagTool, string.Format("\"{0}\"", aiffLeftChannelData), false);
                                CommonFunctions.RunConsoleProcess(vagTool, string.Format("\"{0}\"", aiffRightChannelData), false);

                                // Build the SMD file from the VAG files
                                string vagLeftChannelData = Path.ChangeExtension(aiffLeftChannelData, ".vag");
                                string vagRightChannelData = Path.ChangeExtension(aiffRightChannelData, ".vag");
                                if (Directory.Exists(outputFolder))
                                {
                                    InterleaveAudioChannels(CommonFunctions.RemoveFileHeader(vagLeftChannelData, 48), CommonFunctions.RemoveFileHeader(vagRightChannelData, 48), 128, soundSampleDataFilePath);
                                }

                                // Delete the temporary files
                                File.Delete(wavLeftChannelData);
                                File.Delete(wavRightChannelData);
                                File.Delete(aiffLeftChannelData);
                                File.Delete(aiffRightChannelData);
                                File.Delete(vagLeftChannelData);
                                File.Delete(vagRightChannelData);
                                break;
                            case "pc":
                            case "gamecube":
                                //Output only one time to save memmory, GC & PC share the same audio file.
                                if (outputImaData)
                                {
                                    // Define the paths for the left and right channel WAV files
                                    wavLeftChannelData = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempWave.wav");
                                    wavRightChannelData = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempWave2.wav");

                                    // Split the wave file into left and right channels and re-sample the waves file to 32000 Hz
                                    backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                    CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -l", wavePath, wavLeftChannelData), false);
                                    backgroundWorker1.ReportProgress(99, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                    CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -r", wavePath, wavRightChannelData), false);

                                    // Convert the split wave files to Ima ADPCM
                                    imaLeftChannelData = imaClass.Encode(waveData.GetWaveSamples(wavLeftChannelData));
                                    imaRightChannelData = imaClass.Encode(waveData.GetWaveSamples(wavRightChannelData));

                                    //Update flag to false
                                    outputImaData = false;
                                }

                                // Create ASL/ASR files
                                File.WriteAllBytes(aslFilePath, imaClass.DecodeStatesIma(imaLeftChannelData, imaLeftChannelData.Length * 2));
                                File.WriteAllBytes(asrFilePath, imaClass.DecodeStatesIma(imaRightChannelData, imaLeftChannelData.Length * 2));

                                //Update UI with the current progress
                                backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));

                                // Build SMD
                                if (Directory.Exists(outputFolder))
                                {
                                    InterleaveAudioChannels(imaLeftChannelData, imaRightChannelData, 1, Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".ssd"));
                                }
                                break;
                            case "x box":
                            case "xbox":
                                backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));

                                // Define the paths for wav file
                                string outputTempFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "X Box", "Music", "MFX_" + folderIndex);
                                Directory.CreateDirectory(outputTempFolder);

                                // Convert the wave file to the Xbox Adpcm format
                                string soundSampleDataFile = Path.Combine(outputTempFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                                backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "xbadpcmencode.EXE"), string.Format("\"{0}\" \"{1}\"", wavePath, soundSampleDataFile), false);

                                //Remove Header Data and build SMD file
                                if (File.Exists(soundSampleDataFile))
                                {
                                    File.WriteAllBytes(soundSampleDataFile, CommonFunctions.RemoveFileHeader(soundSampleDataFile, 48));
                                }
                                break;
                        }
                    }

                    // Check if the project folder exists
                    if (Directory.Exists(GlobalPrefs.ProjectFolder))
                    {
                        // Report progress and update the last output time of the MIDI file
                        backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));
                        musicFileData.MidiFileLastOutput = new FileInfo(markerFile).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                        //Read Marker File
                        MarkerTextFile[] markersData = TextFiles.ReadMarkerFile(markerFile);
                        UpdateMarkerPositions(outputPlatforms[j], markersData, string.Empty, string.Empty);

                        //Write Marker File
                        streamMarkers.BuildBinaryFile(markersData, musicFileData.Volume, soundMarkerFilePath, outputPlatforms[j].Equals("GameCube"));

                        // Report progress after the marker file has been created
                        backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                    }

                    //Build SFX
                    string outputPath = CommonFunctions.GetSoundbankOutPath(projectSettings, outputPlatforms[j], string.Empty, true);
                    if (!string.IsNullOrEmpty(outputPath) && Directory.Exists(outputPath))
                    {
                        if (File.Exists(soundMarkerFilePath) && File.Exists(soundSampleDataFilePath))
                        {
                            MusXBuild_MusicFileOld.BuildMusicFile(soundMarkerFilePath, soundSampleDataFilePath, Path.Combine(outputPath, string.Format("HCE{0:X5}.SFX", musicFileData.HashCode)), (uint)musicFileData.HashCode, outputPlatforms[j].Equals("GameCube", StringComparison.OrdinalIgnoreCase));
                        }
                        else if (!File.Exists(soundMarkerFilePath) && !File.Exists(soundSampleDataFilePath))
                        {
                            Invoke(method: new Action(() => { MessageBox.Show("BindXFile - wot no files!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                        }
                    }
                }

                //Update file
                musicFileData.WavFileLastOutput = new FileInfo(wavePath).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                //Save file changes
                mfxList.Add(musicFileData.HashCode);
                mfxList.Add(TextFiles.CreateJumpMarker(markerFile, Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".jmp")));
                TextFiles.WriteMusicFile(musicFileData, Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", filesQueue[i] + ".txt"));

                //Update UI
                parentFormObj.lvwMusicFiles.Invoke((MethodInvoker)delegate
                {
                    ListViewItem result = parentFormObj.lvwMusicFiles.FindItemWithText(filesQueue[i]);
                    if (result != null)
                    {
                        result.UseItemStyleForSubItems = false;
                        result.SubItems[0].ForeColor = SystemColors.WindowText;
                        result.SubItems[2].Text = "No Errors";
                        result.SubItems[4].Text = "OK";
                        if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                        {
                            result.SubItems[5].Text = "OK";
                        }
                    }
                });
            }

            //Write MFX Valid List
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Valid_MFX_List.txt"), FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                for (int i = 0; i < mfxList.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        sw.WriteLine(mfxList[i]);
                    }
                    else
                    {
                        sw.WriteLine(" {0} ", mfxList[i] - 1);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdateMarkerPositions(string outputPlatform, MarkerTextFile[] markersList, string imaFileLeft, string imaFileRight)
        {
            //Calculate states -- PC & GameCube Platform
            if (outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
            {
                //Update positions Start Markers
                foreach (MarkerTextFile marker in markersList)
                {
                    //Calculate offsets for IMA Adpcm
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetMusicLoopOffsetPCandGC(marker.Position);
                    }
                }


                //Update STATES
                if (File.Exists(imaFileLeft) && File.Exists(imaFileRight))
                {
                    List<string> stl = new List<string>();
                    List<string> str = new List<string>();

                    //Update Markers states
                    foreach (MarkerTextFile marker in markersList)
                    {
                        if (marker.Position > 0)
                        {

                            uint state = 0;
                            using (BinaryReader breader = new BinaryReader(File.Open(imaFileLeft, FileMode.Open, FileAccess.Read, FileShare.Read)))
                            {
                                long offset = ((marker.Position / 256) * 256) / 2;
                                if (offset <= breader.BaseStream.Length)
                                {
                                    breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                    state = breader.ReadUInt32();
                                }
                                marker.ImaStateA = state;

                                //Add items to list
                                stl.Add(state.ToString());
                                stl.Add(marker.Position.ToString());
                            }
                            using (BinaryReader breader = new BinaryReader(File.Open(imaFileRight, FileMode.Open, FileAccess.Read, FileShare.Read)))
                            {
                                long offset = ((marker.Position / 256) * 256) / 2;
                                if (offset <= breader.BaseStream.Length)
                                {
                                    breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                    state = breader.ReadUInt32();
                                }
                                marker.ImaStateB = state;

                                //Add items to list
                                str.Add(state.ToString());
                                str.Add(marker.Position.ToString());
                            }
                        }
                    }

                    //Write file
                    File.WriteAllLines(Path.Combine(Path.ChangeExtension(imaFileLeft, ".str")), str);
                    File.WriteAllLines(Path.Combine(Path.ChangeExtension(imaFileLeft, ".stl")), stl);
                }
            }

            //Update Positions PS2 Platform
            if (outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (MarkerTextFile marker in markersList)
                {
                    //Calculate VAG offsets
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetMusicLoopOffsetPlayStation2(marker.Position);
                    }
                }
            }

            //Update positions for Xbox
            if (outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (MarkerTextFile marker in markersList)
                {
                    //Calculate Xbox Adpcm offsets
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetMusicLoopOffsetXbox(marker.Position);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Stop the timer that was measuring output time
            outputTimer.Stop();

            // If an error occurred during the background work, show a message to the user
            if (e.Error != null)
            {
                if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
                {
                    // Set the taskbar progress to error state (only applicable on Windows 7 and above)
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
                }

                // If there are errors in the midiClass, show them to the user
                if (midiClass != null && midiClass.errorsList.Count > 0)
                {
                    for (int i = 0; i < midiClass.errorsList.Count; i++)
                    {
                        MessageBox.Show(midiClass.errorsList[i], Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Show the error message to the user
                MessageBox.Show(e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Run the post-output script
            CommonFunctions.RunOutputScripts(Path.Combine(GlobalPrefs.ProjectFolder, "System", "PostOutput.bat"), "rem Add your post-output stuff here");

            // Update the output time text in the parent form
            parentFormObj.txtOutputTime.Text = string.Format("Output Time =  {0:0.0000000000000}", outputTimer.Elapsed.TotalMilliseconds);
            parentFormObj.Show();

            //Close Current Form
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Set the text of the form to the message passed in the e.UserState object
            Text = e.UserState.ToString();
            ProgressBar1.Value = e.ProgressPercentage;

            // If the form has not been disposed and the operating system version is greater than or equal to 6.1
            if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
            {
                // Set the value of the taskbar progress bar to the progress percentage passed in e.ProgressPercentage and the maximum value to the progress bar's maximum value
                TaskbarProgress.SetValue(Handle, e.ProgressPercentage, ProgressBar1.Maximum);

                // Set the state of the taskbar progress bar to normal
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void InterleaveAudioChannels(byte[] leftChannelData, byte[] rightChannelData, int interleaveBlockSize, string outputFilePath)
        {
            // Initialize indices for left and right channel data
            int leftChannelIndex = 0;
            int rightChannelIndex = 0;

            // Align array sizes to the interleave block size
            if (interleaveBlockSize > 1)
            {
                Array.Resize(ref leftChannelData, leftChannelData.Length + (interleaveBlockSize - 1) & ~(interleaveBlockSize - 1));
                Array.Resize(ref rightChannelData, rightChannelData.Length + (interleaveBlockSize - 1) & ~(interleaveBlockSize - 1));
            }

            // Calculate the total length of the interleaved data array
            int interleavedDataLength = leftChannelData.Length + rightChannelData.Length;
            byte[] interleavedData = new byte[interleavedDataLength];

            // Interleave the left and right channel data
            for (int i = 0; i < interleavedDataLength; i++)
            {
                if (i % 2 == 0)
                {
                    // Interleave left channel data
                    if (leftChannelIndex < leftChannelData.Length)
                    {
                        Buffer.BlockCopy(leftChannelData, leftChannelIndex, interleavedData, i * interleaveBlockSize, interleaveBlockSize);
                    }
                    leftChannelIndex += interleaveBlockSize;
                }
                else
                {
                    // Interleave right channel data
                    if (rightChannelIndex < rightChannelData.Length)
                    {
                        Buffer.BlockCopy(rightChannelData, rightChannelIndex, interleavedData, i * interleaveBlockSize, interleaveBlockSize);
                    }
                    rightChannelIndex += interleaveBlockSize;
                }
            }

            File.WriteAllBytes(outputFilePath, interleavedData);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
