using sb_editor.Audio_Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void ResSample(SamplePool samplesList)
        {
            Stopwatch soxTimer = new Stopwatch();
            Stopwatch pcTimer = new Stopwatch();
            Stopwatch gcTimer = new Stopwatch();
            Stopwatch xbTimer = new Stopwatch();
            Stopwatch psTimer = new Stopwatch();

            //Check For New Samples
            string[] missingSamples = SampleFiles.GetMissingSamples(samplesList, projectSettings);
            if (missingSamples.Length > 0)
            {
                using (MissingSamplesFound newSamplesForm = new MissingSamplesFound(missingSamples, samplesList))
                {
                    newSamplesForm.ShowDialog();
                }
            }

            // Check for missing Samples
            string[] newSamples = SampleFiles.GetNewSamples(samplesList, projectSettings);
            if (newSamples.Length > 0)
            {
                using (NewSamplesFound newSamplesForm = new NewSamplesFound(newSamples, samplesList))
                {
                    newSamplesForm.ShowDialog();
                }
            }

            //Clone the master directory for each available format, this way we avoid to check each time if the folder exists.
            foreach (KeyValuePair<string, PlatformData> platformData in projectSettings.platformData)
            {
                CopyDirectory(Path.Combine(projectSettings.SampleFilesFolder, "Master"), Path.Combine(GlobalPrefs.ProjectFolder, platformData.Key), false, true);
                switch (platformData.Key.ToLower())
                {
                    case "pc":
                        CopyDirectory(Path.Combine(projectSettings.SampleFilesFolder, "Master"), Path.Combine(GlobalPrefs.ProjectFolder, platformData.Key + "_Software_adpcm"), false, true);
                        break;
                    case "gamecube":
                        CopyDirectory(Path.Combine(projectSettings.SampleFilesFolder, "Master"), Path.Combine(GlobalPrefs.ProjectFolder, platformData.Key + "_Software_adpcm"), false, true);
                        CopyDirectory(Path.Combine(projectSettings.SampleFilesFolder, "Master"), Path.Combine(GlobalPrefs.ProjectFolder, platformData.Key + "_dsp_adpcm"), false, true);
                        break;
                    case "playstation2":
                        CopyDirectory(Path.Combine(projectSettings.SampleFilesFolder, "Master"), Path.Combine(GlobalPrefs.ProjectFolder, platformData.Key + "_VAG"), false, true);
                        break;
                    default:
                        CopyDirectory(Path.Combine(projectSettings.SampleFilesFolder, "Master"), Path.Combine(GlobalPrefs.ProjectFolder, "XBox_adpcm"), false, true);
                        break;
                }
            }

            //Iterate over all available samples
            int index = 0;
            int total = samplesList.SamplePoolItems.Count * projectSettings.platformData.Count;
            foreach (KeyValuePair<string, SamplePoolItem> sample in samplesList.SamplePoolItems)
            {
                if (sample.Value.ReSample)
                {
                    string sampleFilePath = sample.Key.TrimStart(Path.DirectorySeparatorChar);
                    string sampleFullPath = Path.Combine(projectSettings.SampleFilesFolder, "Master", sampleFilePath);
                    WavInfo waveFileData = wavFunctions.ReadWaveProperties(sampleFullPath);

                    //Ensure that this sample is a mono 16 bits Wave
                    if (waveFileData.Channels != 1)
                    {
                        Invoke(method: new Action(() => { MessageBox.Show(string.Format("Sample {0} is not 1 channel.", sampleFullPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                        continue;
                    }
                    if (waveFileData.BitsPerSample != 16)
                    {
                        Invoke(method: new Action(() => { MessageBox.Show(string.Format("Sample {0} is not 16 bit.", sampleFullPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                        continue;
                    }

                    //Resample for all platforms that has the AutoResample Enabled
                    foreach (KeyValuePair<string, PlatformData> platform in projectSettings.platformData)
                    {
                        //Report Progress
                        double progress = (double)decimal.Divide(index++, total) * 100.0;
                        backgroundWorker1.ReportProgress((int)progress, string.Format("ReSampling: {0}  {1}", sample.Key, platform.Key));

                        //Check if we have to ReSample for this platform
                        if (platform.Value.AutoReSample)
                        {
                            //Get the sample rate for this sample.
                            int sampleRateIndex = projectSettings.ResampleRates.IndexOf(sample.Value.ReSampleRate);
                            int sampleRate = projectSettings.platformData[platform.Key].ReSampleRates[sampleRateIndex];

                            //Stream sounds MUST use the default value: 22050Hz
                            if (sample.Value.StreamMe)
                            {
                                sampleRate = 22050;

                                //Ensure that the marker file exists
                                string markerFile = Path.ChangeExtension(sampleFullPath, ".mrk");
                                if (!File.Exists(markerFile))
                                {
                                    CreateMarkerFile(markerFile, waveFileData);
                                }
                            }

                            //Get output path for the reSampled Wave
                            string waveOutputPath = Path.Combine(GlobalPrefs.ProjectFolder, platform.Key, sampleFilePath);

                            //ReSample for each platform
                            switch (platform.Key.ToLower())
                            {
                                case "playstation2":
                                    //----------------------------------------------------------ReSample Master File
                                    string aifOutputPath = Path.ChangeExtension(waveOutputPath, ".aif");
                                    soxTimer.Start();
                                    CommonFunctions.ReSampleWithSox(sampleFullPath, aifOutputPath, waveFileData.SampleRate, sampleRate, GlobalPrefs.SoxEffect, false);
                                    soxTimer.Stop();

                                    //----------------------------------------------------------Add Loop Points (if has)
                                    if (waveFileData.HasLoop && !sample.Value.StreamMe)
                                    {
                                        WavInfo resampledFiledata = aiffFunctions.ReadWaveProperties(aifOutputPath);
                                        int loopStart = (int)decimal.Divide(waveFileData.LoopStart, decimal.Divide(waveFileData.Length, resampledFiledata.Length));
                                        aiffFunctions.AddLoopPoints(aifOutputPath, loopStart, resampledFiledata.SampleCount, waveFileData.MidiNote);
                                    }

                                    //----------------------------------------------------------Run VAG Encoder
                                    psTimer.Start();
                                    CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "AIFF2VAG.EXE"), string.Format("\"{0}\"", aifOutputPath), false);
                                    psTimer.Stop();

                                    //----------------------------------------------------------Move DSP File To Dest Folder
                                    string vagOutputPath = Path.ChangeExtension(waveOutputPath, ".vag");
                                    if (File.Exists(vagOutputPath))
                                    {
                                        string vagDestPath = Path.ChangeExtension(Path.Combine(GlobalPrefs.ProjectFolder, "PlayStation2_VAG", sampleFilePath), ".vag");
                                        File.Delete(vagDestPath);
                                        File.Move(vagOutputPath, vagDestPath);
                                    }
                                    break;
                                case "gamecube":
                                    //----------------------------------------------------------ReSample Master File
                                    soxTimer.Start();
                                    CommonFunctions.ReSampleWithSox(sampleFullPath, waveOutputPath, waveFileData.SampleRate, sampleRate, GlobalPrefs.SoxEffect, false);
                                    soxTimer.Stop();

                                    //----------------------------------------------------------Create IMA file if Required
                                    if (sample.Value.StreamMe)
                                    {
                                        CreateImaAdpcm(platform.Key, sampleFilePath, waveOutputPath);
                                    }
                                    else
                                    {
                                        //----------------------------------------------------------Run DSP Encoder if Required
                                        string dspOutputPath = Path.Combine(GlobalPrefs.ProjectFolder, "GameCube_dsp_adpcm", Path.ChangeExtension(sampleFilePath, ".dsp"));
                                        string args = string.Format("-E \"{0}\" \"{1}\"", waveOutputPath, dspOutputPath);
                                        if (waveFileData.HasLoop)
                                        {
                                            WavInfo resampledFiledata = wavFunctions.ReadWaveProperties(waveOutputPath);
                                            int loopStart = (int)Math.Round(decimal.Divide(waveFileData.LoopStart, decimal.Divide(waveFileData.Length, resampledFiledata.Length)));
                                            args = string.Format("-E \"{0}\" \"{1}\" -l{2}-{3}", waveOutputPath, dspOutputPath, loopStart, resampledFiledata.SampleCount - 1);
                                        }
                                        gcTimer.Start();
                                        CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "DSPADPCM.exe"), args, false);
                                        gcTimer.Stop();

                                        //----------------------------------------------------------Move DSP File To Dest Folder
                                        string fileName = Path.GetFileNameWithoutExtension(waveOutputPath) + ".txt";
                                        string textFilePath = Path.Combine(Application.StartupPath, "SystemFiles", fileName);
                                        if (File.Exists(textFilePath))
                                        {
                                            string destFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "GameCube", fileName);
                                            File.Delete(destFilePath);
                                            File.Move(textFilePath, destFilePath);
                                        }
                                    }
                                    break;
                                case "pc":
                                    //----------------------------------------------------------ReSample Master File
                                    soxTimer.Start();
                                    CommonFunctions.ReSampleWithSox(sampleFullPath, waveOutputPath, waveFileData.SampleRate, sampleRate, GlobalPrefs.SoxEffect, false);
                                    soxTimer.Stop();

                                    //----------------------------------------------------------Create IMA file if Required
                                    if (sample.Value.StreamMe)
                                    {
                                        pcTimer.Start();
                                        CreateImaAdpcm(platform.Key, sampleFilePath, waveOutputPath);
                                        pcTimer.Stop();
                                    }
                                    break;
                                default:
                                    //----------------------------------------------------------ReSample Master File
                                    soxTimer.Start();
                                    CommonFunctions.ReSampleWithSox(sampleFullPath, waveOutputPath, waveFileData.SampleRate, sampleRate, GlobalPrefs.SoxEffect, false);
                                    soxTimer.Stop();

                                    //----------------------------------------------------------Run Xbox Encoder
                                    string xboxOutputPath = Path.Combine(GlobalPrefs.ProjectFolder, "XBox_adpcm", sampleFilePath);
                                    xbTimer.Start();
                                    CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "xbadpcmencode.exe"), string.Format("\"{0}\" \"{1}\"", waveOutputPath, xboxOutputPath), false);
                                    xbTimer.Stop();
                                    break;
                            }
                        }
                    }

                    //Update File
                    sample.Value.ReSample = false;
                    sample.Value.ReSmp1 = "False";
                    sample.Value.ReSmp2 = "False";
                    sample.Value.ReSmp3 = "False";
                    sample.Value.ReSmp4 = "False";

                    //Write File Again
                    TextFiles.WriteSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"), samplesList);
                }
            }

            //Update Debug Output
            if (index > 0)
            {
                parentFormObj.UserControl_Misc.DebugLog.Add("Re-Sample Times");
                parentFormObj.UserControl_Misc.DebugLog.Add("---------------");
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Empty);
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "SoxTime  {0:0.####}", soxTimer.Elapsed.TotalMilliseconds));
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "PCTime  {0:0.####}", pcTimer.Elapsed.TotalMilliseconds));
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "GCTime  {0:0.####}", gcTimer.Elapsed.TotalMilliseconds));
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "XBTime  {0:0.####}", xbTimer.Elapsed.TotalMilliseconds));
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "PSTime  {0:0.####}", psTimer.Elapsed.TotalMilliseconds));
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Empty);
                parentFormObj.UserControl_Misc.DebugLog.Add(string.Empty);
            }

            //Calculate Total Time
            FullOutputTime += soxTimer.Elapsed.TotalMilliseconds;
            FullOutputTime += pcTimer.Elapsed.TotalMilliseconds;
            FullOutputTime += gcTimer.Elapsed.TotalMilliseconds;
            FullOutputTime += xbTimer.Elapsed.TotalMilliseconds;
            FullOutputTime += psTimer.Elapsed.TotalMilliseconds;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CreateImaAdpcm(string currentPlatform, string sampleRelativePath, string waveInputFile)
        {
            //Get file paths
            string ImaOutputFilePath = Path.Combine(GlobalPrefs.ProjectFolder, currentPlatform + "_Software_adpcm", sampleRelativePath);

            //Wave to IMA Adpcm
            byte[] imaData = imaFunctions.Encode(wavFunctions.GetWaveSamples(waveInputFile));
            File.WriteAllBytes(Path.ChangeExtension(ImaOutputFilePath, ".ssp"), imaData);

            //Wave to IMA Adpcm
            byte[] imaStates = imaFunctions.DecodeStatesIma(imaData, (imaData.Length * 2) - 1);
            File.WriteAllBytes(Path.ChangeExtension(ImaOutputFilePath, ".smd"), imaStates);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CreateMarkerFile(string filePath, WavInfo waveInfo)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("Markers");
                sw.WriteLine("{");
                AddMarkerBlock(sw, "Stream Start Marker", 0, 10, 2);
                if (waveInfo.HasLoop)
                {
                    AddMarkerBlock(sw, "Stream Start Loop", waveInfo.LoopStart, 6, 2);
                    AddMarkerBlock(sw, "Stream End Loop", waveInfo.LoopEnd, 10, 2);
                }
                else
                {
                    AddMarkerBlock(sw, "Stream End Marker", (int)waveInfo.SampleCount, 9, 2);
                }
                sw.WriteLine("}");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void AddMarkerBlock(StreamWriter sw, string markerName, int position, int markerType, int markerFlags)
        {
            sw.WriteLine("\tMarker");
            sw.WriteLine("\t{");
            sw.WriteLine("\t\tName={0}", markerName);
            sw.WriteLine("\t\tPos={0}", position);
            sw.WriteLine("\t\tType={0}", markerType);
            sw.WriteLine("\t\tFlags={0}", markerFlags);
            sw.WriteLine("\t\tExtra={0}", 0);
            sw.WriteLine("\t}");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CopyDirectory(string sourceDir, string destinationDir, bool copyFiles, bool recursive)
        {
            //Get information about the source directory
            DirectoryInfo dir = new DirectoryInfo(sourceDir);

            //Check if the source directory exists
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(string.Format("Source directory not found: {0}", dir.FullName));
            }

            //Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            //Create the destination directory
            Directory.CreateDirectory(destinationDir);

            //Get the files in the source directory and copy to the destination directory
            if (copyFiles)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    string targetFilepath = Path.Combine(destinationDir, file.Name);
                    file.CopyTo(targetFilepath);
                }
            }

            //If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, copyFiles, recursive);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
