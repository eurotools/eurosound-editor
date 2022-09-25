using ESUtils;
using EuroSound_Editor.Classes;
using EuroSound_Editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EuroSound_Editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OutputSoundBanks(SamplePool samplePoolList, Dictionary<string, uint> hashCodesDict, string debugFolder)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();

            //For Each Language
            for (int i = 0; i < outLanguages.Length; i++)
            {
                string[] streamSamples = GetStreamSamples(samplePoolList, outLanguages[i]);

                //For Each SoundBank
                for (int j = 0; j < filesQueue.Length; j++)
                {
                    //Create Timers
                    Stopwatch Query = new Stopwatch();
                    Stopwatch SFXData = new Stopwatch();
                    Stopwatch Samples = new Stopwatch();

                    //Read SoundBank File
                    SoundBank soundBankData = TextFiles.ReadSoundbankFile(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", filesQueue[j] + ".txt"));

                    //Export this soundbank for each platform
                    for (int k = 0; k < outputPlatform.Length; k++)
                    {
                        bool isBigEndian = outputPlatform[k].Equals("GameCube", StringComparison.OrdinalIgnoreCase);

                        //Report Progress
                        decimal progress = decimal.Divide(j, filesQueue.Length) * 100;
                        backgroundWorker1.ReportProgress((int)progress, string.Format("Outputting {0} SoundBank {1} for {2}", outLanguages[i], filesQueue[j], outputPlatform[k]));

                        //Get Current SoundBank Data
                        Query.Start();
                        Dictionary<string, SFX> sbFileData = GetSfxDataDict(sbFunctions.GetSFXs(soundBankData.DataBases), outputPlatform[k], outLanguages[i]);
                        string[] samplesList = sbFunctions.GetSampleList(sbFileData, outLanguages[i]).Except(streamSamples).ToArray();
                        UpdateDuckerLength(sbFileData, outputPlatform[k]);
                        Query.Stop();

                        //Get File Paths
                        string debugFilePath = Path.Combine(debugFolder, string.Format("StreamDebugSoundBank_{0}_{1}_{2}.txt", filesQueue[j], outputPlatform[k], outLanguages[i]));
                        string outputTempFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", outputPlatform[k], "SoundBanks", outLanguages[i], soundBankData.HashCode + ".tmp");

                        //Create Folder to store the Temporal SoundBank files
                        Directory.CreateDirectory(Path.GetDirectoryName(outputTempFilePath));

                        //Create Debug File
                        using (StreamWriter sw = new StreamWriter(File.Open(debugFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                        {
                            sw.WriteLine("SoundBank Output Debug Data");
                            sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy"));
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                            sw.WriteLine("");
                            sw.WriteLine("SoundBankName = {0}", filesQueue[j]);
                            sw.WriteLine("SoundBankSaveName = {0}", soundBankData.HashCode);
                            sw.WriteLine("SoundBankFileName = {0}", Path.ChangeExtension(outputTempFilePath, ".sfx"));
                            sw.WriteLine("Stream PoolFiles(n).FileRef");

                            //Output temporal files
                            long sampleBankSize = OutputTempFiles(hashCodesDict, sbFileData, samplesList, streamSamples, outputPlatform[k], outputTempFilePath, filesQueue[j], sw, isBigEndian, SFXData, Samples);
                            if (!abortQuickOutput)
                            {
                                //Check Sample Bank Limit is not Exceeded
                                long myMaxSize = GetMaxBankSize(outputPlatform[k], soundBankData);
                                if (sampleBankSize > myMaxSize)
                                {
                                    //Delete Files
                                    File.Delete(Path.ChangeExtension(outputTempFilePath, ".sbf"));
                                    File.Delete(Path.ChangeExtension(outputTempFilePath, ".sfx"));
                                    File.Delete(Path.ChangeExtension(outputTempFilePath, ".sif"));
                                    File.Delete(Path.ChangeExtension(outputTempFilePath, ".ssf"));

                                    //Inform User
                                    string message = string.Format("Sample Bank Limit Exceeded With:\n\nSoundBank: {0}\nFormat: {1}\nMy Size: {2}K\nMy Max Size: {3}K\n\nOutput Aborted and Files Deleted.", filesQueue[j], outputPlatform[k], sampleBankSize, myMaxSize);
                                    throw new IOException(message);
                                }
                                else
                                {
                                    if (Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath))
                                    {
                                        //Build MusX
                                        string sbfTempFile = Path.ChangeExtension(outputTempFilePath, ".sbf");
                                        string sfxTempFile = Path.ChangeExtension(outputTempFilePath, ".sfx");
                                        string sifTempFile = Path.ChangeExtension(outputTempFilePath, ".sif");
                                        string ssfTempFile = Path.ChangeExtension(outputTempFilePath, ".ssf");

                                        //Get Output Path 
                                        Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Sonix"));
                                        DirectoryInfo musXFolder = Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", CommonFunctions.GetEnginexFolder(outputPlatform[k]), CommonFunctions.GetLanguageFolder(outLanguages[i])));
                                        string fileName = string.Format("HC{0:X6}.SFX", CommonFunctions.GetSfxName(Array.FindIndex(GlobalPrefs.Languages, s => s.Equals(outLanguages[i], StringComparison.OrdinalIgnoreCase)), soundBankData.HashCode));

                                        //Build File
                                        MusXBuild_Soundbank.BuildSoundbankFile(sfxTempFile, sifTempFile, sbfTempFile, ssfTempFile, Path.Combine(musXFolder.FullName, fileName), soundBankData.HashCode, isBigEndian);
                                    }
                                }
                            }
                            abortQuickOutput = false;

                            double totalTime = Query.Elapsed.TotalMilliseconds + SFXData.Elapsed.TotalMilliseconds + Samples.Elapsed.TotalMilliseconds;
                            parentFormObj.UserControl_Misc.DebugLog.Add(string.Format("Timings For Bank {0}", filesQueue[j]));
                            parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "Total   = {0:0.####}", totalTime));
                            parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "Query   = {0:0.####}", Query.Elapsed.TotalMilliseconds));
                            parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "SFXDate = {0:0.####}", SFXData.Elapsed.TotalMilliseconds));
                            parentFormObj.UserControl_Misc.DebugLog.Add(string.Format(GlobalPrefs.NumericProvider, "Samples = {0:0.####}", Samples.Elapsed.TotalMilliseconds));
                            FullOutputTime += totalTime;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private Dictionary<string, SFX> GetSfxDataDict(string[] sbSfxs, string platform, string language)
        {
            Dictionary<string, SFX> sfxFilesData = new Dictionary<string, SFX>();

            for (int i = 0; i < sbSfxs.Length; i++)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platform, sbSfxs[i].TrimStart(Path.DirectorySeparatorChar) + ".txt");
                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sbSfxs[i].TrimStart(Path.DirectorySeparatorChar) + ".txt");
                }

                //Update Sample File Paths
                SFX sfxData = TextFiles.ReadSfxFile(filePath);
                foreach (SfxSample sampleData in sfxData.Samples)
                {
                    string samplePath = CommonFunctions.GetSampleFromSpeechFolder(sampleData.FilePath, language);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        sampleData.FilePath = samplePath;
                    }
                }

                //Add Data To Dictionary
                sfxFilesData.Add(Path.GetFileNameWithoutExtension(filePath), sfxData);
            }

            return sfxFilesData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private long GetMaxBankSize(string currentPlatform, SoundBank sbData)
        {
            long myMaxSize = 0;
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                //Max Sizes
                IniFile systemIni = new IniFile(systemIniFilePath);
                if (currentPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase))
                {
                    if (sbData.PCSize > 0)
                    {
                        myMaxSize = sbData.PCSize;
                    }
                    else
                    {
                        myMaxSize = Convert.ToUInt32(systemIni.Read("PCSize", "PropertiesForm"));
                    }
                }
                else if (currentPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
                {
                    if (sbData.PlayStationSize > 0)
                    {
                        myMaxSize = sbData.PlayStationSize;
                    }
                    else
                    {
                        myMaxSize = Convert.ToUInt32(systemIni.Read("PlayStationSize", "PropertiesForm"));
                    }
                }
                else if (currentPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
                {
                    if (sbData.GameCubeSize > 0)
                    {
                        myMaxSize = sbData.GameCubeSize;
                    }
                    else
                    {
                        myMaxSize = Convert.ToUInt32(systemIni.Read("GameCubeSize", "PropertiesForm"));
                    }
                }
                else
                {
                    if (sbData.XboxSize > 0)
                    {
                        myMaxSize = sbData.XboxSize;
                    }
                    else
                    {
                        myMaxSize = Convert.ToUInt32(systemIni.Read("XBoxSize", "PropertiesForm"));
                    }
                }
            }

            return myMaxSize;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
