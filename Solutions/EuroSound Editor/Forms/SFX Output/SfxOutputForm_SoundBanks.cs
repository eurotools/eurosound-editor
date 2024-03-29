﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFX Form Output SoundBanks
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using MusX.Writers;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static ESUtils.Enumerations;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OutputSoundBanks(SamplePool samplePoolList, string debugFolder)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();

            //For Each Language
            for (int i = 0; i < outLanguages.Length; i++)
            {
                Language outputLanguage = (Language)Enum.Parse(typeof(Language), outLanguages[i], true);
                string[] streamSamples = GetStreamSamples(samplePoolList, outputLanguage);

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
                        Dictionary<string, SFX> sbFileData = sbFunctions.GetSfxDataDict(sbFunctions.GetSFXs(soundBankData.DataBases), outputPlatform[k], outputLanguage);
                        string[] samplesList = sbFunctions.GetSampleList(sbFileData, outputLanguage).Except(streamSamples).ToArray();
                        sbFunctions.UpdateDuckerLength(sbFileData, outputPlatform[k]);
                        Query.Stop();

                        //Get File Paths
                        string debugFilePath = Path.Combine(debugFolder, string.Format("StreamDebugSoundBank_{0}_{1}_{2}.txt", filesQueue[j], outputPlatform[k], outLanguages[i]));
                        string outTmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", outputPlatform[k], "SoundBanks", outLanguages[i], soundBankData.HashCode + ".tmp");

                        //Create Folder to store the Temporal SoundBank files
                        Directory.CreateDirectory(Path.GetDirectoryName(outTmpFilePath));

                        //Create Debug File
                        using (StreamWriter sw = new StreamWriter(File.Open(debugFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                        {
                            sw.WriteLine("SoundBank Output Debug Data");
                            sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy"));
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                            sw.WriteLine("");
                            sw.WriteLine("SoundBankName = {0}", filesQueue[j]);
                            sw.WriteLine("SoundBankSaveName = {0}", soundBankData.HashCode);
                            sw.WriteLine("SoundBankFileName = {0}", Path.ChangeExtension(outTmpFilePath, ".sfx"));
                            sw.WriteLine("Stream PoolFiles(n).FileRef");

                            //Write Temporal Files
                            long sampleBankSize = 0;
                            using (BinaryWriter sbfWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outTmpFilePath, ".sbf"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                            {
                                using (BinaryWriter sfxWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outTmpFilePath, ".sfx"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                                {
                                    using (BinaryWriter sifWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outTmpFilePath, ".sif"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                                    {
                                        List<byte[]> dspHeaderData = new List<byte[]>();

                                        //Write SFX Data
                                        SFXData.Start();
                                        WriteSfxFile(sbFileData, samplesList, streamSamples, outputPlatform[k], filesQueue[j], sfxWritter, isBigEndian, sw);
                                        SFXData.Stop();
                                        if (!abortQuickOutput)
                                        {
                                            //Write SFX Samples
                                            Samples.Start();
                                            sampleBankSize = WriteSifFile(sifWritter, sbfWritter, samplesList, outputPlatform[k], dspHeaderData, isBigEndian);
                                            Samples.Stop();

                                            if (dspHeaderData.Count > 0)
                                            {
                                                using (BinaryWriter ssfWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outTmpFilePath, ".ssf"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                                                {
                                                    for (int l = 0; l < dspHeaderData.Count; l++)
                                                    {
                                                        ssfWritter.Write(dspHeaderData[l]);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            sampleBankSize = (long)Math.Round(decimal.Divide(sampleBankSize, 1024));
                            if (!abortQuickOutput)
                            {
                                //Check Sample Bank Limit is not Exceeded
                                long myMaxSize = sbFunctions.GetMaxBankSize(outputPlatform[k], soundBankData);
                                if (sampleBankSize > myMaxSize)
                                {
                                    //Delete Files
                                    File.Delete(Path.ChangeExtension(outTmpFilePath, ".sbf"));
                                    File.Delete(Path.ChangeExtension(outTmpFilePath, ".sfx"));
                                    File.Delete(Path.ChangeExtension(outTmpFilePath, ".sif"));
                                    File.Delete(Path.ChangeExtension(outTmpFilePath, ".ssf"));

                                    //Inform User
                                    string message = string.Format("Sample Bank Limit Exceeded With:\n\nSoundBank: {0}\nFormat: {1}\nMy Size: {2}K\nMy Max Size: {3}K\n\nOutput Aborted and Files Deleted.", filesQueue[j], outputPlatform[k], sampleBankSize, myMaxSize);
                                    throw new IOException(message);
                                }
                                else
                                {
                                    //Get Output Path
                                    string outputPath = CommonFunctions.GetSoundbankOutPath(projectSettings, outputPlatform[k], outLanguages[i]);
                                    if (!string.IsNullOrEmpty(outputPath) && Directory.Exists(outputPath))
                                    {
                                        string fileName = string.Format("HC{0:X6}.SFX", CommonFunctions.GetSfxName((int)Enum.Parse(typeof(Enumerations.Language), outLanguages[i], true), soundBankData.HashCode));
                                        string sbfTempFile = Path.ChangeExtension(outTmpFilePath, ".sbf");
                                        string sfxTempFile = Path.ChangeExtension(outTmpFilePath, ".sfx");
                                        string sifTempFile = Path.ChangeExtension(outTmpFilePath, ".sif");
                                        string ssfTempFile = Path.ChangeExtension(outTmpFilePath, ".ssf");
                                        MusXBuild_SoundbankOld.BuildSoundbankFile(sfxTempFile, sifTempFile, sbfTempFile, ssfTempFile, Path.Combine(outputPath, fileName), soundBankData.HashCode, isBigEndian);
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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
