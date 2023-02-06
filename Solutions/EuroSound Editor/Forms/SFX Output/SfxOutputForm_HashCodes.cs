﻿using ESUtils;
using sb_editor.HashCodes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using static ESUtils.Enumerations;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OutputHashCodes(SamplePool samplesList)
        {
            HashTables hashCodes = new HashTables();
            bool prefixHashCode = false;

            //Check Ini File
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                IniFile systemIni = new IniFile(systemIniFilePath);
                prefixHashCode = systemIni.Read("Prefix_HT_Sound", "PropertiesForm").Equals("1");
            }

            //-------------------------------------------------------------------------------[Temporal SFX_Data.h]-------------------------------------------------------------------------------
            backgroundWorker1.ReportProgress(0, "Creating SFX_Data.h");
            string[] sfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < sfxFiles.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(sfxFiles[i]);

                //Report Progress
                double progress = (double)decimal.Divide(i, sfxFiles.Length) * 100.0;
                backgroundWorker1.ReportProgress((int)progress, string.Format("Creating SFX_Data.h {0}", fileName));

                //Create temp file
                hashCodes.CreateTempSfxData(fileName, sfxFiles[i], outLanguages, samplesList);
            }

            //-------------------------------------------------------------------------------[SFX_Data.h]-------------------------------------------------------------------------------
            backgroundWorker1.ReportProgress(12, "Writing SFX_Data.h");

            //Read folder files
            SortedDictionary<uint, string> itemsData = new SortedDictionary<uint, string>();
            string[] availableFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "TempSfxData"), "*.txt", SearchOption.AllDirectories);
            for (int i = 0; i < outLanguages.Length; i++)
            {
                Language outLang = (Language)Enum.Parse(typeof(Language), outLanguages[i], true);
                for (int j = 0; j < availableFiles.Length; j++)
                {
                    if (availableFiles[j].IndexOf("Speech", StringComparison.OrdinalIgnoreCase) >= 0 && availableFiles[j].IndexOf(outLang.ToString(), StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        continue;
                    }
                    string[] data = File.ReadAllLines(availableFiles[j]);
                    if (data.Length == 2)
                    {
                        string[] dataSplit = data[1].Split(',', '{');
                        uint hashCode = Convert.ToUInt32(dataSplit[1].Trim());
                        if (itemsData.ContainsKey(hashCode))
                        {
                            itemsData[hashCode] = data[1];
                        }
                        else
                        {
                            itemsData.Add(hashCode, data[1]);
                        }
                    }
                }

                //Write file data
                string sfxDataFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, outLang.ToString() + "_SFX_Data.h");
                using (StreamWriter sw = new StreamWriter(File.Open(sfxDataFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    sw.WriteLine("// typedef struct SFXOutputDetails {s32 HashCode;f32 InnerRadius;f32 OuterRadius;f32 Duration;s8 Looping;s8 Tracking3d;s8 SampleStreamed;s8 Is3D;} SFXOutputDetails;");
                    sw.WriteLine("SFXOutputDetails SFXOutputData[] = {");
                    uint index = 0;
                    foreach (KeyValuePair<uint, string> item in itemsData)
                    {
                        while (index != item.Key)
                        {
                            sw.WriteLine("{ 0 , 0 ,  0 , 0 ,  0 , 0 , 0 , 0 } ,  // Blank");
                            index++;
                        }
                        sw.WriteLine(item.Value);
                        index++;
                    }
                    sw.WriteLine("};\n");
                }

                //Convert it to the SFX
                for (int j = 0; j < outputPlatform.Length; j++)
                {
                    string outTmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", outputPlatform[j], "SoundBanks", outLanguages[i], "sfxdetails.bin");
                    BuildSoundDetailsFile(sfxDataFilePath, outTmpFilePath);

                    string sfxOutputPath = Path.Combine(CommonFunctions.GetSoundbankOutPath(outputPlatform[j]), CommonFunctions.GetSfxName(outLang, "sounddetails").ToLower());
                    MusXBuild_MusicDetails.BuildMusicDetails(outTmpFilePath, sfxOutputPath, CommonFunctions.GetPlatformLabel(outputPlatform[j]));
                }
            }

            //-------------------------------------------------------------------------------[SFX_Defines.h]-------------------------------------------------------------------------------
            string sfxDefinesFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Defines.h");
            backgroundWorker1.ReportProgress(24, string.Format("Writing SFX_Defines.h Folder = {0}", Path.GetDirectoryName(sfxDefinesFilePath)));

            SortedDictionary<string, int> hashCodesDict = new SortedDictionary<string, int>();
            SortedDictionary<string, int> soundBankDict = new SortedDictionary<string, int>();

            //Write File
            using (StreamWriter sw = new StreamWriter(File.Open(sfxDefinesFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                //Misc Defines Section
                backgroundWorker1.ReportProgress(36, "Writing SFX_Defines.h Stage 0");
                sw.WriteLine("// SFX Misc defines");
                sw.WriteLine(hashCodes.WriteHashCode("SBIHashCode", 0xFFFFFF));
                sw.WriteLine(hashCodes.WriteHashCode("EX_SFX_PREFIX", 0x1A000000));
                sw.WriteLine(hashCodes.WriteHashCode("EX_SONG_PREFIX", 0x1B000000));
                sw.WriteLine(hashCodes.WriteHashCodeComment("EX_SOUNDBANK_PREFIX", 0x1C000000));
                sw.WriteLine(string.Empty);

                //Language Defines 
                sw.WriteLine("// SFX Language defines");
                sw.WriteLine(hashCodes.WriteHashCode("LanguageHashCodeOffset", 0x10000));
                sw.WriteLine(string.Empty);
                foreach (Language language in Enum.GetValues(typeof(Language)))
                {
                    sw.WriteLine(hashCodes.WriteNumber("SFXLanguage_" + language, (int)language));
                }
                sw.WriteLine(hashCodes.WriteNoAlign("StreamFileHashCode", 0xFFFF));
                sw.WriteLine(string.Empty);

                //SFX HashCodes
                backgroundWorker1.ReportProgress(48, "Writing SFX_Defines.h Stage 1");
                sw.WriteLine("// SFX HashCodes");
                sw.WriteLine("#define SFX_NIS_MUSIC_TRIGGER 0");
                sw.WriteLine(string.Empty);
                int maxSfxHashcodeDefined = 0;
                hashCodes.GetHashCodesWithLabels(hashCodesDict, null);
                foreach (KeyValuePair<string, int> sfxItem in hashCodesDict)
                {
                    if (prefixHashCode)
                    {
                        sw.WriteLine(hashCodes.WriteHashCode("HT_Sound_" + sfxItem.Key, sfxItem.Value | 0x1A000000));
                    }
                    else
                    {
                        sw.WriteLine(hashCodes.WriteHashCode(sfxItem.Key, sfxItem.Value | 0x1A000000));
                    }
                    maxSfxHashcodeDefined = Math.Max(maxSfxHashcodeDefined, sfxItem.Value);
                }
                backgroundWorker1.ReportProgress(60, "Writing SFX_Defines.h Stage 2");
                sw.WriteLine(hashCodes.WriteHashCode("SFX_MaximumDefined", hashCodesDict.Count));
                sw.WriteLine(hashCodes.WriteHashCode("SFX_HashCodeHighest", maxSfxHashcodeDefined));
                sw.WriteLine(string.Empty);

                //Soundbank HashCodes
                sw.WriteLine("// SoundBank HashCodes");
                hashCodes.GetHashCodesWithLabels(null, soundBankDict);
                foreach (KeyValuePair<string, int> soundbankItem in soundBankDict)
                {
                    if (prefixHashCode)
                    {
                        sw.WriteLine(hashCodes.WriteHashCode("HT_Sound_" + soundbankItem.Key, soundbankItem.Value));
                    }
                    else
                    {
                        sw.WriteLine(hashCodes.WriteHashCode(soundbankItem.Key, soundbankItem.Value));
                    }
                }
                sw.WriteLine(hashCodes.WriteHashCode("SB_MaximumDefined", soundBankDict.Count));
            }

            //-------------------------------------------------------------------------------[SFX_Debug.h]-------------------------------------------------------------------------------
            backgroundWorker1.ReportProgress(72, "Writing SFX_Defines.h Stage 3");
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Debug.h"), FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES");
                sw.WriteLine("long NumberToHashCode[] = {");

                //Read and print Data
                hashCodes.GetHashCodesWithLabels(hashCodesDict, null);
                foreach (KeyValuePair<string, int> hashCodeData in hashCodesDict)
                {
                    sw.WriteLine("{0} , ", hashCodeData.Value);
                }
                sw.WriteLine("};");
                sw.WriteLine("#endif");
                sw.WriteLine(string.Empty);
                sw.WriteLine(string.Empty);
                sw.WriteLine("#ifdef SFX_BUILD_DEBUG_TABLES");
                sw.WriteLine("typedef struct HashCodeAndString {long HashCode;char* String;} HashCodeAndString;");
                sw.WriteLine(string.Empty);
                sw.WriteLine("struct HashCodeAndString HashCodeToString[]={");
                foreach (KeyValuePair<string, int> hashCodeData in hashCodesDict)
                {
                    sw.WriteLine("{{{0} , \"{1}\"}} , ", hashCodeData.Value, hashCodeData.Key);
                }
                sw.WriteLine("};");
                sw.WriteLine("#endif");
                sw.WriteLine(string.Empty);
            }

            //-------------------------------------------------------------------------------[SFX_Reverbs.h]-------------------------------------------------------------------------------
            backgroundWorker1.ReportProgress(84, "Writing SFX_Defines.h Stage Pre Close");
            string reverbsFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Reverbs.h");
            Dictionary<string, int> reverbsDict = sbFunctions.GetHashCodesDictionary("Reverbs", "#MiscData");
            using (StreamWriter sw = new StreamWriter(File.Open(reverbsFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// Reverb Hashcodes");
                foreach (KeyValuePair<string, int> reverbData in reverbsDict)
                {
                    sw.WriteLine(hashCodes.WriteHashCode(reverbData.Key, reverbData.Value));
                }
            }

            //-------------------------------------------------------------------------------[Sound.h]-------------------------------------------------------------------------------
            backgroundWorker1.ReportProgress(100, "End");
            hashCodes.BuildSoundHhFile(Path.Combine(GlobalPrefs.CurrentProject.EuroLandHashCodeServerPath, "Sound.h"));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BuildSoundDetailsFile(string musicDataTableFile, string musicDetailsPlatform)
        {
            //Open hashtable and create binary file
            using (StreamReader sr = new StreamReader(File.Open(musicDataTableFile, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(musicDetailsPlatform, FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    int minValue = 0;
                    int maxValue = 0;

                    //Write placeholders
                    bw.Write(0);
                    bw.Write(0);

                    //Read data
                    while (!sr.EndOfStream)
                    {
                        string currentLine = sr.ReadLine().Trim();
                        //Skip empty or commented lines
                        if (string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//"))
                        {
                            continue;
                        }

                        //Header info
                        if (currentLine.StartsWith("{ "))
                        {
                            string[] lineData = currentLine.Split(new char[] { ',', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
                            if (lineData.Length == 10)
                            {
                                int hashcode = Convert.ToInt32(lineData[0].Trim());
                                bw.Write((short)StringFloatToDouble(lineData[1]));
                                bw.Write((short)StringFloatToDouble(lineData[2]));
                                bw.Write(StringFloatToDouble(lineData[3]));
                                bw.Write(Convert.ToSByte(lineData[4]));
                                bw.Write(Convert.ToSByte(lineData[5]));
                                bw.Write(Convert.ToSByte(lineData[6]));
                                bw.Write(Convert.ToSByte(lineData[7].Trim().Equals("True", StringComparison.OrdinalIgnoreCase)));

                                minValue = Math.Min(minValue, hashcode);
                                maxValue = Math.Max(maxValue, hashcode);
                            }
                        }
                    }

                    //Write min and max values
                    bw.Seek(0, SeekOrigin.Begin);
                    bw.Write(minValue | 0x1AF00000);
                    bw.Write(maxValue | 0x1AF00000);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static float StringFloatToDouble(string number)
        {
            string num = number.Trim().Replace("f", string.Empty);
            return float.Parse(num, GlobalPrefs.NumericProvider);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
