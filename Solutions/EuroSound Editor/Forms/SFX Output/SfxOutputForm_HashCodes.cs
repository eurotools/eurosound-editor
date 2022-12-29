using sb_editor.HashCodes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;

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
                hashCodes.CreateTempSfxData(Path.Combine(GlobalPrefs.ProjectFolder, "TempSfxData", fileName + ".txt"), sfxFiles[i], samplesList);
            }

            //-------------------------------------------------------------------------------[SFX_Data.h]-------------------------------------------------------------------------------
            backgroundWorker1.ReportProgress(12, "Writing SFX_Data.h");

            //Read folder files
            SortedDictionary<uint, string> itemsData = new SortedDictionary<uint, string>();
            string[] availableFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "TempSfxData"), "*.txt", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < availableFiles.Length; i++)
            {
                string[] data = File.ReadAllLines(availableFiles[i]);
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
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Data.h"), FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// typedef struct SFXOutputDetails {s32 HashCode;f32 InnerRadius;f32 OuterRadius;f32 Altertness;f32 Duration;s8 Looping;s8 Tracking3d;s8 SampleStreamed;} SFXOutputDetails;");
                sw.WriteLine("SFXOutputDetails SFXOutputData[] = {");
                uint index = 0;
                foreach (KeyValuePair<uint, string> item in itemsData)
                {
                    while (index != item.Key)
                    {
                        sw.WriteLine("{ 0 , 0 ,  0 , 0 ,  0 , 0 , 0 } ,  // Blank");
                        index++;
                    }
                    sw.WriteLine(item.Value);
                    index++;
                }
                sw.WriteLine("};\n");
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
                for (int i = 0; i < GlobalPrefs.Languages.Length; i++)
                {
                    sw.WriteLine(hashCodes.WriteNumber("SFXLanguage_" + GlobalPrefs.Languages[i].ToUpper(), i));
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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
