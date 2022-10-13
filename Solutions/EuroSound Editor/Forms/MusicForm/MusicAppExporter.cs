using ESUtils;
using ExMarkers;
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

        //-------------------------------------------------------------------------------------------------------------------------------
        public MusicAppExporter(string[] outputFiles, string[] outPlatforms, MusicApp parentForm)
        {
            InitializeComponent();
            filesQueue = outputFiles;
            parentFormObj = parentForm;
            outputPlatforms = outPlatforms;
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
            MusicMarkerFiles markerFiles = new MusicMarkerFiles();
            ImaFunctions imaClass = new ImaFunctions();
            WaveFunctions waveData = new WaveFunctions();

            for (int i = 0; i < filesQueue.Length; i++)
            {
                bool outWavForIma = true; //PC & GameCube Shares the same wav and IMA data,
                                          //if we have to output for these two platforms,
                                          //we can skip the ReSample and IMA Encoding step for the next one.

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
                string aslFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".asl");
                string asrFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", filesQueue[i] + ".asr");
                byte[] imaLeft, imaRight;
                imaLeft = imaRight = null;

                //Output For Each Platform
                for (int j = 0; j < outputPlatforms.Length; j++)
                {
                    string outputFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", outputPlatforms[j], "Music", "MFX_" + folderIndex);
                    Directory.CreateDirectory(outputFolder);

                    string soundSampleDataFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                    string soundMarkerFilePath = Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".smf");
                    string wavLeft, wavRight;
                    //ReSample
                    if (!parentFormObj.chkOutputOnlyMarkerFile.Checked)
                    {
                        switch (outputPlatforms[j].ToLower())
                        {
                            case "playstation2":
                                wavLeft = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + "L.wav");
                                wavRight = Path.Combine(GlobalPrefs.ProjectFolder, "Music", filesQueue[i] + "R.wav");

                                //ReSample Wav
                                backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -l", wavePath, wavLeft), false);
                                backgroundWorker1.ReportProgress(25, string.Format("Making Music Stream: {0} for PlayStation2", filesQueue[i]));
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -r", wavePath, wavRight), false);

                                //Create Aiff
                                string aiffLeft = Path.ChangeExtension(wavLeft, ".aif");
                                string aiffRight = Path.ChangeExtension(wavRight, ".aif");
                                backgroundWorker1.ReportProgress(75, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" \"{1}\"", wavLeft, aiffLeft), false);
                                CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" \"{1}\"", wavRight, aiffRight), false);

                                //Create VAG
                                string vagTool = Path.Combine(Application.StartupPath, "SystemFiles", "AIFF2VAG.EXE");
                                backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(vagTool, string.Format("\"{0}\"", aiffLeft), false);
                                CommonFunctions.RunConsoleProcess(vagTool, string.Format("\"{0}\"", aiffRight), false);

                                //Build SMD
                                string vagLeft = Path.ChangeExtension(aiffLeft, ".vag");
                                string vagRight = Path.ChangeExtension(aiffRight, ".vag");
                                if (Directory.Exists(outputFolder))
                                {
                                    MergeChannels(CommonFunctions.RemoveFileHeader(vagLeft, 48), CommonFunctions.RemoveFileHeader(vagRight, 48), 128, soundSampleDataFilePath);
                                }

                                //Remove Files
                                File.Delete(wavLeft);
                                File.Delete(wavRight);
                                File.Delete(aiffLeft);
                                File.Delete(aiffRight);
                                File.Delete(vagLeft);
                                File.Delete(vagRight);
                                break;
                            case "pc":
                            case "gamecube":
                                if (outWavForIma)
                                {
                                    wavLeft = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempWave.wav");
                                    wavRight = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempWave2.wav");

                                    //-----------------------------------ReSample Left Channel
                                    backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                    CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -l", wavePath, wavLeft), false);
                                    //Convert to IMA
                                    imaLeft = imaClass.Encode(waveData.GetWaveSamples(wavLeft));
                                    //States IMA
                                    File.WriteAllBytes(aslFilePath, imaClass.DecodeStatesIma(imaLeft, imaLeft.Length * 2));

                                    //-----------------------------------ReSample Right Channel
                                    backgroundWorker1.ReportProgress(99, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                    CommonFunctions.RunConsoleProcess(SoxPath, string.Format("\"{0}\" -c 1 -r 32000 \"{1}\" resample -qs 0.97 avg -r", wavePath, wavRight), false);
                                    //Convert to IMA
                                    imaRight = imaClass.Encode(waveData.GetWaveSamples(wavRight));
                                    outWavForIma = false;
                                }

                                //States IMA
                                backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                File.WriteAllBytes(asrFilePath, imaClass.DecodeStatesIma(imaRight, imaLeft.Length * 2));
                                if (Directory.Exists(outputFolder))
                                {
                                    MergeChannels(imaLeft, imaRight, 1, Path.Combine(outputFolder, "MFX_" + musicFileData.HashCode + ".ssd"));
                                }
                                break;
                            case "x box":
                            case "xbox":
                                backgroundWorker1.ReportProgress(0, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                string outputTempFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "X Box", "Music", "MFX_" + folderIndex);
                                Directory.CreateDirectory(outputTempFolder);

                                //Create Adpcm
                                string soundSampleDataFile = Path.Combine(outputTempFolder, "MFX_" + musicFileData.HashCode + ".ssd");
                                backgroundWorker1.ReportProgress(100, string.Format("Making Music Stream: {0} for {1}", filesQueue[i], outputPlatforms[j]));
                                CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "xbadpcmencode.EXE"), string.Format("\"{0}\" \"{1}\"", wavePath, soundSampleDataFile), false);

                                //Remove Header Data
                                if (File.Exists(soundSampleDataFile))
                                {
                                    File.WriteAllBytes(soundSampleDataFile, CommonFunctions.RemoveFileHeader(soundSampleDataFile, 48));
                                }
                                break;
                        }
                    }

                    //Build SMF
                    if (Directory.Exists(GlobalPrefs.ProjectFolder))
                    {
                        backgroundWorker1.ReportProgress(0, string.Format("Making Marker File: {0}", filesQueue[i]));
                        musicFileData.MidiFileLastOutput = new FileInfo(markerFile).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");
                        markerFiles.CreateMarkerFile(string.Empty, string.Empty, markerFile, musicFileData.Volume, outputPlatforms[j], soundMarkerFilePath);
                        backgroundWorker1.ReportProgress(100, string.Format("Making Marker File: {0}", filesQueue[i]));
                    }

                    //Build SFX
                    string outputPath = CommonFunctions.GetSoundbankOutPath(outputPlatforms[j], string.Empty, true);
                    if (!string.IsNullOrEmpty(outputPath) && Directory.Exists(outputPath))
                    {
                        MusXBuild_MusicFile.BuildMusicFile(soundMarkerFilePath, soundSampleDataFilePath, Path.Combine(outputPath, string.Format("HCE{0:X5}.SFX", musicFileData.HashCode)), (uint)musicFileData.HashCode, outputPlatforms[j].Equals("GameCube", StringComparison.OrdinalIgnoreCase));
                    }
                }

                //Update file
                musicFileData.WavFileLastOutput = new FileInfo(wavePath).LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss");

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
                if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
                {
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
                }
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
            if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
            {
                TaskbarProgress.SetValue(Handle, e.ProgressPercentage, ProgressBar1.Maximum);
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            }
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
