using ESUtils;
using NAudio.Wave;
using sb_editor.Audio_Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ESUtils.Enumerations;

namespace sb_editor.Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class SoundBankFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetSFXs(string[] DataBases, string platform = "")
        {
            // Use a HashSet to store unique SFX names
            HashSet<string> soundBankSFX = new HashSet<string>();

            string dataBasesFolder = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");
            string sfxsFolder = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

            // Iterate over all given data bases
            for (int i = 0; i < DataBases.Length; i++)
            {
                // Get the file path of the data base
                string filePath = Path.Combine(dataBasesFolder, DataBases[i] + ".txt");

                // Check if the data base file exists
                if (File.Exists(filePath))
                {
                    // Read the file data into an array of lines
                    string[] fileData = File.ReadAllLines(filePath);

                    // Find the index of the "#DEPENDENCIES" line
                    int index = Array.IndexOf(fileData, "#DEPENDENCIES") + 1;

                    // Check if the "#DEPENDENCIES" line was found
                    if (index > 0)
                    {
                        // Iterate over all lines after the "#DEPENDENCIES" line
                        string currentLine = fileData[index];
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            // Check if a platform was specified
                            if (!string.IsNullOrEmpty(platform) && File.Exists(Path.Combine(sfxsFolder, platform, currentLine + ".txt")))
                            {
                                // If the platform-specific version exists, add it to the set
                                soundBankSFX.Add(string.Format("{0}/{1}", platform, currentLine));
                            }
                            else
                            {
                                // If the platform-specific version does not exist, add the regular version to the set
                                soundBankSFX.Add(currentLine);
                            }
                            currentLine = fileData[index++].Trim();
                        }
                    }
                }
            }

            // Convert the hash set to an array and sort it
            string[] SfxArray = soundBankSFX.ToArray();
            Array.Sort(SfxArray);

            return SfxArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetSampleList(string[] SFXs, Language outputLanguage)
        {
            HashSet<string> samplesList = new HashSet<string>();

            for (int i = 0; i < SFXs.Length; i++)
            {
                // Combine the path to the SFXs folder with the file name
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", SFXs[i] + ".txt");

                // Read all lines from each SFX file
                string[] fileData = File.ReadAllLines(filePath);

                // Find the index of the line that starts the sample pool section
                int samplePoolIndex = Array.IndexOf(fileData, "#SFXSamplePoolFiles") + 1;
                if (samplePoolIndex > 0)
                {
                    // Iterate over all lines after the "#SFXSamplePoolFiles" line
                    string currentLine = fileData[samplePoolIndex];
                    while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                    {
                        // Get the samples from the speech folder
                        string sampleName = CommonFunctions.GetSampleFromSpeechFolder(currentLine, outputLanguage);
                        if (!string.IsNullOrEmpty(sampleName))
                        {
                            // Add the samples to the list
                            samplesList.Add(sampleName);
                        }
                        currentLine = fileData[samplePoolIndex++].Trim();
                    }
                }
            }

            // Convert the hash set to an array and sort it
            string[] samplesArray = samplesList.ToArray();
            Array.Sort(samplesArray);

            return samplesArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetSampleList(SortedDictionary<string, SFX> fileData, Language outputLanguage)
        {
            // Create a hash set to store the unique sample names
            HashSet<string> samplesList = new HashSet<string>();

            // Iterate through the sfx data
            foreach (KeyValuePair<string, SFX> sfxItem in fileData)
            {
                // Check if the sample pool for this SFX is enabled and that it has samples
                if (!sfxItem.Value.SamplePool.EnableSubSFX && sfxItem.Value.Samples.Count > 0)
                {
                    // Iterate through the samples for this SFX
                    foreach (SfxSample sampleData in sfxItem.Value.Samples)
                    {
                        // Get the sample name from the speech folder for the specified output language
                        string sampleName = CommonFunctions.GetSampleFromSpeechFolder(sampleData.FilePath, outputLanguage);

                        // If the sample name is not empty, add it to the hash set
                        if (!string.IsNullOrEmpty(sampleName))
                        {
                            samplesList.Add(sampleName);
                        }
                    }
                }
            }

            // Convert the hash set to an array and sort it
            string[] samplesArray = samplesList.ToArray();
            Array.Sort(samplesArray);

            return samplesArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal long GetSampleSize(string samplesFolder, SamplePool samplePool, string[] Samples)
        {
            long sampleSize = 0;

            // Iterate through all samples
            for (int i = 0; i < Samples.Length; i++)
            {
                // Get the full file name and check if it exists in the sample pool
                string fileName = MultipleFilesFunctions.GetFullFileName(Samples[i]);
                if (samplePool.SamplePoolItems.ContainsKey(fileName) && Path.HasExtension(fileName) && !Path.IsPathRooted(Samples[i]))
                {
                    long waveLength = 0;
                    string samplePath = Path.Combine(samplesFolder, Samples[i]);

                    // Check if the sample file exists
                    if (File.Exists(samplePath))
                    {
                        // Get the length of the wave file in bytes
                        using (WaveFileReader WReader = new WaveFileReader(samplePath))
                        {
                            waveLength = WReader.Length;
                        }
                    }

                    // Calculate the size of the sample based on whether it should be streamed or fully loaded into memory
                    if (samplePool.SamplePoolItems[fileName].StreamMe)
                    {
                        sampleSize += 2 * Math.Max(waveLength, 1); // Multiply by 2 because streaming requires twice the memory
                    }
                    else
                    {
                        sampleSize += 4 * Math.Max(waveLength, 1); // Multiply by 4 because fully loading into memory requires 4 times the memory
                    }
                }
            }

            return sampleSize;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal long GetEstimatedOutputFileSize(ProjProperties projectSettings, string[] samplesList, SamplePool samplePool, string outputPlatform)
        {
            decimal fileSize = 0;

            //With Master folder
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "Master")))
            {
                for (int i = 0; i < samplesList.Length; i++)
                {
                    if (!Path.HasExtension(samplesList[i]) || Path.IsPathRooted(samplesList[i]))
                    {
                        continue;
                    }
                    //Master wave freq
                    long masterWaveSize = 0;
                    int masterWaveFreq = 0;
                    string samplePath = Path.Combine(GlobalPrefs.ProjectFolder, "Master", samplesList[i].TrimStart('\\'));
                    if (File.Exists(samplePath))
                    {
                        using (WaveFileReader waveReader = new WaveFileReader(samplePath))
                        {
                            masterWaveSize = waveReader.Length;
                            masterWaveFreq = waveReader.WaveFormat.SampleRate;
                        }
                    }

                    //ReSampled wave size
                    string keyToCheck = MultipleFilesFunctions.GetFullFileName(samplesList[i]);
                    if (samplePool.SamplePoolItems.ContainsKey(keyToCheck))
                    {
                        SamplePoolItem sampleItem = samplePool.SamplePoolItems[keyToCheck];
                        int sampleRateIndex = projectSettings.ResampleRates.IndexOf(sampleItem.ReSampleRate);
                        int formatRate = projectSettings.platformData[outputPlatform].ReSampleRates[sampleRateIndex];
                        decimal resampledWaveSize = decimal.Divide(masterWaveSize, decimal.Divide(masterWaveFreq, formatRate));
                        switch (outputPlatform)
                        {
                            case "PC":
                                fileSize += CalculusLoopOffset.GetStreamLoopOffsetPCandGC((uint)resampledWaveSize / 2);
                                break;
                            case "GameCube":
                                decimal dspFileSize = decimal.Divide(resampledWaveSize, (decimal)3.46);
                                fileSize += CalculusLoopOffset.GetStreamLoopOffsetPCandGC((uint)dspFileSize);
                                break;
                            case "PlayStation2":
                                fileSize += CalculusLoopOffset.GetStreamLoopOffsetPlayStation2((uint)resampledWaveSize / 4);
                                break;
                            default:
                                decimal xboxAdpcm = decimal.Divide(resampledWaveSize, (decimal)2.36);
                                fileSize += CalculusLoopOffset.GetXboxAlignedNumber((uint)xboxAdpcm);
                                break;
                        }
                    }
                }
            }
            else
            {
                //Without master folder
                for (int i = 0; i < samplesList.Length; i++)
                {
                    if (!Path.IsPathRooted(samplesList[i]))
                    {
                        string fileName = MultipleFilesFunctions.GetFullFileName(samplesList[i]);
                        if (samplePool.SamplePoolItems.ContainsKey(fileName) && Path.HasExtension(fileName))
                        {
                            //Skip streams for these two platforms
                            if (samplePool.SamplePoolItems[fileName].StreamMe && (outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase)))
                            {
                                continue;
                            }

                            //Calculate sample size
                            if (outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
                            {
                                fileSize += 36;
                            }
                            else
                            {
                                fileSize += 32;
                            }
                        }
                    }
                }
            }

            return (long)fileSize;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal SortedDictionary<string, SFX> GetSfxDataDict(string[] dataBases, string platform, Language language)
        {
            // Dictionary to store the SFX data for each file
            SortedDictionary<string, SFX> sfxFilesData = new SortedDictionary<string, SFX>();
            string[] dataBaseFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"), "*.txt", SearchOption.AllDirectories);
            Array.Sort(dataBaseFiles);

            // Iterate over the array of SFX file names
            for (int i = 0; i < dataBases.Length; i++)
            {
                string dbFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", dataBases[i] + ".txt");
                DataBase sbSfxs = TextFiles.ReadDataBaseFile(dbFilePath);
                for (int j = 0; j < sbSfxs.SFXs.Length; j++)
                {
                    // Create the full path to the SFX file
                    string sfxName = sbSfxs.SFXs[j].TrimStart(Path.DirectorySeparatorChar) + ".txt";
                    string sfxLabel = platform + "\\" + sfxName;
                    string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxLabel);

                    // If the file does not exist in the specified platform folder, check the root SFXs folder
                    if (!File.Exists(filePath))
                    {
                        sfxLabel = sfxName;
                        filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxLabel);
                    }

                    //Ensure that this SFX has not been readed
                    if (!sfxFilesData.ContainsKey(sfxLabel))
                    {
                        // Read the SFX data from the file
                        SFX sfxData = TextFiles.ReadSfxFile(filePath);
                        sfxData.Parameters.Group = Array.IndexOf(dataBaseFiles, dbFilePath) + 1;
                        sfxData.Parameters.GroupStealReject = Convert.ToBoolean(sbSfxs.Action1);
                        sfxData.Parameters.UseGroupDistCheck = sbSfxs.UseDistCheck;

                        // Iterate over the samples in the SFX data
                        foreach (SfxSample sampleData in sfxData.Samples)
                        {
                            // Update the file path of the sample to the correct language folder
                            string samplePath = CommonFunctions.GetSampleFromSpeechFolder(sampleData.FilePath, language);
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                sampleData.FilePath = samplePath;
                            }
                        }

                        // Add the SFX data to the dictionary using the file name as the key
                        sfxFilesData.Add(sfxLabel, sfxData);
                    }
                }
            }

            return sfxFilesData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal long GetMaxBankSize(string currentPlatform, SoundBank sbData)
        {
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                //Max Sizes
                IniFile systemIni = new IniFile(systemIniFilePath);
                switch (currentPlatform.ToLower())
                {
                    case "pc":
                        return sbData.PCSize > 0 ? sbData.PCSize : Convert.ToUInt32(systemIni.Read("PCSize", "PropertiesForm"));
                    case "playstation2":
                        return sbData.PlayStationSize > 0 ? sbData.PlayStationSize : Convert.ToUInt32(systemIni.Read("PlayStationSize", "PropertiesForm"));
                    case "gamecube":
                        return sbData.GameCubeSize > 0 ? sbData.GameCubeSize : Convert.ToUInt32(systemIni.Read("GameCubeSize", "PropertiesForm"));
                    case "xbox":
                    case "x box":
                        return sbData.XboxSize > 0 ? sbData.XboxSize : Convert.ToUInt32(systemIni.Read("XBoxSize", "PropertiesForm"));
                }
            }

            return 0;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void UpdateDuckerLength(SortedDictionary<string, SFX> fileData, string outputPlatform)
        {
            foreach (KeyValuePair<string, SFX> soundToCheck in fileData)
            {
                //Update ducker if is On
                if (soundToCheck.Value.Parameters.Ducker > 0)
                {
                    int duckerLength = 0;

                    //Get Length of all samples
                    foreach (SfxSample sampleToCheck in soundToCheck.Value.Samples)
                    {
                        string sampleFilePath = Path.Combine(GlobalPrefs.ProjectFolder, outputPlatform, sampleToCheck.FilePath.TrimStart(Path.DirectorySeparatorChar));
                        if (outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
                        {
                            sampleFilePath = Path.ChangeExtension(sampleFilePath, ".aif");
                            if (File.Exists(sampleFilePath))
                            {
                                using (AiffFileReader reader = new AiffFileReader(sampleFilePath))
                                {
                                    decimal cents = Math.Round(decimal.Divide((decimal)reader.TotalTime.TotalMilliseconds, 10));
                                    duckerLength += (int)cents;
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists(sampleFilePath))
                            {
                                using (WaveFileReader reader = new WaveFileReader(sampleFilePath))
                                {
                                    decimal cents = Math.Round(decimal.Divide((decimal)reader.TotalTime.TotalMilliseconds, 10));
                                    duckerLength += (int)cents;
                                }
                            }
                        }
                    }

                    //Apply Value
                    if (soundToCheck.Value.Parameters.DuckerLength < 0)
                    {
                        duckerLength -= Math.Abs(soundToCheck.Value.Parameters.DuckerLength);
                    }
                    else
                    {
                        duckerLength += Math.Abs(soundToCheck.Value.Parameters.DuckerLength);
                    }
                    soundToCheck.Value.Parameters.DuckerLength = duckerLength;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int GetFlags(SFX sfxFile)
        {
            int flags = 0;
            if (sfxFile.Parameters.Action1 == 1)
            {
                flags |= 1 << 0;
            }
            if (sfxFile.Parameters.UnPausable)
            {
                flags |= 1 << 1;
            }
            if (sfxFile.Parameters.IgnoreMasterVolume)
            {
                flags |= 1 << 2;
            }
            if (sfxFile.SamplePool.Action1 == 1)
            {
                flags |= 1 << 3;
            }
            if (sfxFile.SamplePool.RandomPick)
            {
                flags |= 1 << 4;
            }
            if (sfxFile.SamplePool.Shuffled)
            {
                flags |= 1 << 5;
            }
            if (sfxFile.SamplePool.isLooped)
            {
                flags |= 1 << 6;
            }
            if (sfxFile.SamplePool.Polyphonic)
            {
                flags |= 1 << 7;
            }
            if (sfxFile.Parameters.Outdoors)
            {
                flags |= 1 << 8;
            }
            if (sfxFile.Parameters.PauseInstant)
            {
                flags |= 1 << 9;
            }
            if (sfxFile.SamplePool.EnableSubSFX)
            {
                flags |= 1 << 10;
            }
            if (sfxFile.Parameters.StealOnAge)
            {
                flags |= 1 << 11;
            }
            if (sfxFile.Parameters.MusicType)
            {
                flags |= 1 << 12;
            }
            if (sfxFile.Parameters.KillMeOwnGroup)
            {
                flags |= 1 << 13;
            }
            if (sfxFile.Parameters.GroupStealReject)
            {
                flags |= 1 << 14;
            }
            if (sfxFile.Parameters.OneInstancePerFrame)
            {
                flags |= 1 << 15;
            }
            return flags;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal byte[] GetDspHeaderData(string dspFilePath)
        {
            byte[] dspFileWithHeader = File.ReadAllBytes(dspFilePath);
            byte[] dspHeaderData = new byte[96];
            if (dspFileWithHeader.Length > 95)
            {
                Array.Copy(dspFileWithHeader, 0, dspHeaderData, 0, 96);
            }

            return dspHeaderData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void WriteSampleInfo(BinaryWriter sifWritter, BinaryWriter sbfWritter, WavInfo masterFileData, WavInfo wavFileData, uint lengthAligned, int formatLength, int psiSampleHeader, uint loopOffset, bool isBigEndian)
        {
            //Write Header Data
            sifWritter.Write(BytesFunctions.FlipInt32(Convert.ToInt32(masterFileData.HasLoop), isBigEndian));
            sifWritter.Write(BytesFunctions.FlipUInt32((uint)sbfWritter.BaseStream.Position, isBigEndian));
            sifWritter.Write(BytesFunctions.FlipUInt32(lengthAligned, isBigEndian));
            sifWritter.Write(BytesFunctions.FlipInt32(wavFileData.SampleRate, isBigEndian));
            sifWritter.Write(BytesFunctions.FlipInt32(formatLength, isBigEndian));
            sifWritter.Write(BytesFunctions.FlipInt32(psiSampleHeader, isBigEndian));
            sifWritter.Write(BytesFunctions.FlipUInt32(loopOffset, isBigEndian));
            sifWritter.Write(BytesFunctions.FlipUInt32((uint)masterFileData.TotalTime.TotalMilliseconds, isBigEndian));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal Dictionary<string, int> GetHashCodesDictionary(string folder, string keyWord)
        {
            Dictionary<string, int> HashCodesDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            string[] files = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, folder), "*.txt", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                string filePath = Path.GetFileNameWithoutExtension(files[i]);
                if (!HashCodesDict.ContainsKey(filePath))
                {
                    string[] fileData = File.ReadAllLines(files[i]);
                    int hashCodeIndex = Array.FindIndex(fileData, s => s.Equals(keyWord, StringComparison.OrdinalIgnoreCase));
                    string[] data = fileData[hashCodeIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 1)
                    {
                        HashCodesDict.Add(filePath, Convert.ToInt32(data[1].Trim()));
                    }
                }
            }

            return HashCodesDict;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
