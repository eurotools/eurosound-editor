using ESUtils;
using EuroSound_Editor.Audio_Classes;
using EuroSound_Editor.Objects;
using ExMarkers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MusicAppExporter : TimerForm
    {
        private readonly string[] filesQueue;
        private readonly string outputPlatform;
        private readonly MusicApp parentFormObj;
        private readonly Stopwatch outputTimer = new Stopwatch();
        private MidiFunctions midiClass;

        //-------------------------------------------------------------------------------------------------------------------------------
        public MusicAppExporter(string[] outputFiles, string outPlatform, MusicApp parentForm)
        {
            InitializeComponent();
            filesQueue = outputFiles;
            parentFormObj = parentForm;
            outputPlatform = outPlatform;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_MusicMaker_CreateMarkers_Shown(object sender, EventArgs e)
        {
            parentFormObj.Hide();
            if (!backgroundWorker1.IsBusy)
            {
                //Run Bat scripts
                CommonFunctions.RunOutputScripts(true);

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
                //Check if there are MIDI files to convert into Marker Files
                string midiFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".mid");
                if (File.Exists(midiFile))
                {
                    backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));
                    //Copy midi file in the working directory
                    string midiFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".midi");
                    if (File.Exists(midiFilePath))
                    {
                        File.Delete(midiFilePath);
                    }
                    File.Copy(midiFile, midiFilePath);
                    backgroundWorker1.ReportProgress(25, string.Format("Making Marker File: {0}", filesQueue[i]));

                    //Convert midi to text
                    CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "MIDI2TXT.exe"), string.Join(" ", "-ms", "\"" + midiFile + "\"", "\"" + midiFilePath + "\""), false);
                    if (File.Exists(midiFilePath))
                    {
                        midiClass = new MidiFunctions();
                        using (StreamWriter sw = new StreamWriter(File.Open(Path.ChangeExtension(midiFilePath, ".err"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                        {
                            sw.WriteLine("Midi to Marker File Errors Found");
                            sw.WriteLine(string.Empty);

                            //Read midi file and validate
                            backgroundWorker1.ReportProgress(50, string.Format("Making Marker File: {0}", filesQueue[i]));
                            bool fatalErrors = midiClass.CheckMarkersFatalErrors(midiClass.GetNotes(midiFilePath), midiClass.GetTexts(midiFilePath), sw);
                            if (fatalErrors)
                            {
                                //Update UI
                                ListViewItem result = parentFormObj.lvwMusicFiles.FindItemWithText(filesQueue[i]);
                                if (result != null)
                                {
                                    result.UseItemStyleForSubItems = false;
                                    result.SubItems[0].ForeColor = Color.Red;
                                    result.SubItems[2].Text = "Has Errors!";
                                }

                                //Create Marker From Scratch
                                throw new Exception(string.Join("\n", "Errors Found in Midi files During output", "these files have no been output yet.", "MFX File output aborted!"));
                            }
                            else
                            {
                                //Do Check
                                string markerFile = Path.ChangeExtension(midiFilePath, ".mrk");
                                fatalErrors = midiClass.CheckMarkersWarnings(sw);
                                midiClass.WriteMarkerFile(markerFile);

                                //Move marker file to the final location
                                string markerFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".mrk");
                                if (File.Exists(markerFilePath))
                                {
                                    File.Delete(markerFilePath);
                                }
                                File.Copy(markerFile, markerFilePath);
                            }
                            backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                        }
                    }
                }
            }

            //--------------------------------------------------[Stream Files and Marker Files]--------------------------------------------------
            List<int> mfxList = new List<int>();
            string SoxPath = Path.Combine(Application.StartupPath, "SystemFiles", "SOX.EXE");
            for (int i = 0; i < filesQueue.Length; i++)
            {
                MusicMarkerFiles markerFiles = new MusicMarkerFiles();

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
                int folderIndex = (musicFileData.HashCode & 0xF0) >> 4;

                //Get Wave File
                string markerFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".mrk");
                string wavePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + ".wav");

                //ReSample for PlayStation2
                if (outputPlatform.Equals("All", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
                {
                    //------------------------------------------------------------------[PlayStation2]------------------------------------------------------------------
                    string outputFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "PlayStation2", "Music", "MFX_" + folderIndex);
                    Directory.CreateDirectory(outputFolder);

                    string soundSampleDataFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                    string soundMarkerFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".smf");

                    //Output Only Marker File
                    if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                    {
                        string wavLeft = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + "L.wav");
                        string wavRight = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + "R.wav");

                        //ReSample Wav
                        backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for PlayStation2", filesQueue[i]));
                        CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -l", wavePath, wavLeft), false);
                        backgroundWorker1.ReportProgress(25, string.Format("Making Music Stream: {0} for PlayStation2", filesQueue[i]));
                        CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -r", wavePath, wavRight), false);

                        //Create Aiff
                        string aiffLeft = Path.ChangeExtension(wavLeft, ".aif");
                        string aiffRight = Path.ChangeExtension(wavRight, ".aif");
                        backgroundWorker1.ReportProgress(75, string.Format("Making Music Stream: {0} for PlayStation2", filesQueue[i]));
                        CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" \"{1}\"", wavLeft, aiffLeft), false);
                        CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" \"{1}\"", wavRight, aiffRight), false);

                        //Create VAG
                        string vagTool = Path.Combine(Application.StartupPath, "SystemFiles", "AIFF2VAG.EXE");
                        backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for PlayStation2", filesQueue[i]));
                        CommonFunctions.RunConsoleProcess(vagTool, string.Format("\"{0}\"", aiffLeft), false);
                        CommonFunctions.RunConsoleProcess(vagTool, string.Format("\"{0}\"", aiffRight), false);

                        //Build SMD
                        string vagLeft = Path.ChangeExtension(aiffLeft, ".vag");
                        string vagRight = Path.ChangeExtension(aiffRight, ".vag");
                        if (Directory.Exists(GlobalPrefs.ProjectFolder))
                        {
                            MergeChannels(CommonFunctions.RemoveFileHeader(vagLeft, 48), CommonFunctions.RemoveFileHeader(vagRight, 48), 128, soundSampleDataFilePath);
                        }

                        //Update file
                        musicFileData.WavFileLastOutput = new FileInfo(wavePath).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

                        //Remove Files
                        File.Delete(wavLeft);
                        File.Delete(wavRight);
                        File.Delete(aiffLeft);
                        File.Delete(aiffRight);
                        File.Delete(vagLeft);
                        File.Delete(vagRight);
                    }

                    //Build SMF
                    if (Directory.Exists(GlobalPrefs.ProjectFolder))
                    {
                        backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));
                        musicFileData.MidiFileLastOutput = new FileInfo(markerFile).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                        markerFiles.CreateMarkerFile(string.Empty, string.Empty, markerFile, musicFileData.Volume, "PlayStation2", soundMarkerFilePath);
                        backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                    }

                    //Build SFX
                    if (Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath))
                    {
                        string sfxOutFolder = Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", CommonFunctions.GetEnginexFolder("PlayStation2"), "music");
                        Directory.CreateDirectory(sfxOutFolder);
                        MusXBuild_MusicFile.BuildMusicFile(soundMarkerFilePath, soundSampleDataFilePath, Path.Combine(sfxOutFolder, string.Format("HCE{0:X5}.SFX", musicFileData.HashCode)), (uint)musicFileData.HashCode, false);
                    }
                }
                //ReSample for GameCube and PC
                if (outputPlatform.Equals("All", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase))
                {
                    //------------------------------------------------------------------[GameCube && PC]------------------------------------------------------------------
                    string aslFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".asl");
                    string asrFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".asr");
                    string wavLeft = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempWave.wav");
                    string wavRight = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempWave2.wav");
                    byte[] imaLeft, imaRight;
                    imaLeft = imaRight = null;

                    if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                    {
                        ImaFunctions imaClass = new ImaFunctions();
                        WaveFunctions waveData = new WaveFunctions();

                        //-----------------------------------ReSample Left Channel
                        backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for GameCube", filesQueue[i]));
                        CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -l", wavePath, wavLeft), false);
                        //Convert to IMA
                        imaLeft = imaClass.Encode(waveData.GetWaveSamples(wavLeft));
                        //States IMA
                        File.WriteAllBytes(aslFilePath, imaClass.DecodeStatesIma(imaLeft, imaLeft.Length * 2));

                        //-----------------------------------ReSample Right Channel
                        backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for GameCube", filesQueue[i]));
                        CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -r", wavePath, wavRight), false);
                        //Convert to IMA
                        imaRight = imaClass.Encode(waveData.GetWaveSamples(wavRight));
                        //States IMA
                        File.WriteAllBytes(asrFilePath, imaClass.DecodeStatesIma(imaRight, imaLeft.Length * 2));

                        //Update file
                        musicFileData.WavFileLastOutput = new FileInfo(wavePath).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                    }

                    //In the case that we output for all platforms, we need to output for GameCube && PC. 
                    if (Directory.Exists(GlobalPrefs.ProjectFolder))
                    {
                        int iterations = 1;
                        string platform = outputPlatform;
                        if (outputPlatform.Equals("All", StringComparison.OrdinalIgnoreCase))
                        {
                            platform = "GameCube";
                            iterations = 2;
                        }

                        for (int k = 0; k < iterations; k++)
                        {
                            if (k == 1)
                            {
                                platform = "PC";
                            }
                            if (Directory.Exists(GlobalPrefs.ProjectFolder))
                            {
                                string outputFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", platform, "Music", "MFX_" + folderIndex);
                                Directory.CreateDirectory(outputFolder);

                                //Build SMD
                                string soundSampleDataFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                                if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                                {
                                    MergeChannels(imaLeft, imaRight, 1, soundSampleDataFilePath);
                                }

                                //Build SMF
                                string soundMarkerFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".smf");
                                backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));
                                musicFileData.MidiFileLastOutput = new FileInfo(markerFile).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                                backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                                markerFiles.CreateMarkerFile(aslFilePath, asrFilePath, markerFile, musicFileData.Volume, platform, soundMarkerFilePath);

                                //Build SFX
                                if (Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath))
                                {
                                    string sfxOutFolder = Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", CommonFunctions.GetEnginexFolder(platform), "music");
                                    Directory.CreateDirectory(sfxOutFolder);
                                    MusXBuild_MusicFile.BuildMusicFile(soundMarkerFilePath, soundSampleDataFilePath, Path.Combine(sfxOutFolder, "HCE" + musicFileData.HashCode.ToString("X5") + ".SFX"), (uint)musicFileData.HashCode, platform.Equals("GameCube"));
                                }
                            }
                        }
                    }
                }
                if (outputPlatform.Equals("All", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase))
                {
                    //------------------------------------------------------------------[Xbox]------------------------------------------------------------------
                    backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for X Box", filesQueue[i]));

                    //Get output file path
                    if (Directory.Exists(GlobalPrefs.ProjectFolder))
                    {
                        string outputTempFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "X Box", "Music", "MFX_" + folderIndex);
                        Directory.CreateDirectory(outputTempFolder);

                        string soundSampleDataFile = Path.Combine(outputTempFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                        if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                        {
                            //Create Adpcm
                            backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for X Box", filesQueue[i]));
                            CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "xbadpcmencode.EXE"), string.Format("\"{0}\" \"{1}\"", wavePath, soundSampleDataFile), false);

                            //Remove Header Data
                            if (File.Exists(soundSampleDataFile))
                            {
                                File.WriteAllBytes(soundSampleDataFile, CommonFunctions.RemoveFileHeader(soundSampleDataFile, 48));
                            }

                            //Update file
                            musicFileData.WavFileLastOutput = new FileInfo(wavePath).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                        }

                        //Build SMF
                        string soundMarkerFile = Path.Combine(outputTempFolder, "MFX_" + musicFileData.HashCode + ".smf");
                        if (Directory.Exists(GlobalPrefs.ProjectFolder))
                        {
                            backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));
                            musicFileData.MidiFileLastOutput = new FileInfo(markerFile).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                            backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                            markerFiles.CreateMarkerFile(string.Empty, string.Empty, markerFile, musicFileData.Volume, "X Box", soundMarkerFile);
                        }

                        //Build SFX
                        if (Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath))
                        {
                            string sfxOutFolder = Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", CommonFunctions.GetEnginexFolder("X Box"), "music");
                            Directory.CreateDirectory(sfxOutFolder);
                            MusXBuild_MusicFile.BuildMusicFile(soundMarkerFile, soundSampleDataFile, Path.Combine(sfxOutFolder, "HCE" + musicFileData.HashCode.ToString("X5") + ".SFX"), (uint)musicFileData.HashCode, false);
                        }
                    }
                }

                //Save file changes
                mfxList.Add(musicFileData.HashCode);
                mfxList.Add(markerFiles.CreateJumpMarker(markerFile, Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".jmp")));
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
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            outputTimer.Stop();
            if (e.Error != null)
            {
                if (midiClass != null && midiClass.errorsList.Count > 0)
                {
                    for (int i = 0; i < midiClass.errorsList.Count; i++)
                    {
                        MessageBox.Show(midiClass.errorsList[i], Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show(e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Run Bat scripts
            CommonFunctions.RunOutputScripts();

            //Update Textbox and show form again
            parentFormObj.txtOutputTime.Text = string.Format("Output Time =  {0:0.0000000000000}", outputTimer.Elapsed.TotalMilliseconds);
            parentFormObj.Show();

            //Close Current Form
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = e.UserState.ToString();
            ProgressBar1.Value = e.ProgressPercentage;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MergeChannels(byte[] LeftChannelData, byte[] RightChannelData, int interleave_block_size, string outputFilePath)
        {
            int IndexLC, IndexRC;
            IndexLC = IndexRC = 0;

            //Read data and align array size
            if (interleave_block_size > 1)
            {
                Array.Resize(ref LeftChannelData, LeftChannelData.Length + (interleave_block_size - 1) & ~(interleave_block_size - 1));
                Array.Resize(ref RightChannelData, RightChannelData.Length + (interleave_block_size - 1) & ~(interleave_block_size - 1));
            }

            //Get total length
            int arrayLength = LeftChannelData.Length + RightChannelData.Length;
            byte[] interleavedData = new byte[arrayLength];

            // Start channels interleaving
            for (int i = 0; i < arrayLength; i++)
            {
                if (i % 2 == 0)
                {
                    if (IndexLC < LeftChannelData.Length)
                    {
                        Buffer.BlockCopy(LeftChannelData, IndexLC, interleavedData, i * interleave_block_size, interleave_block_size);
                    }
                    IndexLC += interleave_block_size;
                }
                else
                {
                    if (IndexRC < RightChannelData.Length)
                    {
                        Buffer.BlockCopy(RightChannelData, IndexRC, interleavedData, i * interleave_block_size, interleave_block_size);
                    }
                    IndexRC += interleave_block_size;
                }
            }
            File.WriteAllBytes(outputFilePath, interleavedData);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
