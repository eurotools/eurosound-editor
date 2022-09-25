using ESUtils;
using EuroSound_Editor.Objects;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EuroSound_Editor.Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class SoundBankFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetSFXs(string[] DataBases, string platform = "")
        {
            HashSet<string> soundBankSFX = new HashSet<string>();

            for (int i = 0; i < DataBases.Length; i++)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", DataBases[i] + ".txt");
                if (File.Exists(filePath))
                {
                    string[] fileData = File.ReadAllLines(filePath);
                    int index = Array.IndexOf(fileData, "#DEPENDENCIES") + 1;
                    if (index > 0)
                    {
                        string currentLine = fileData[index];
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            if (string.IsNullOrEmpty(platform))
                            {
                                soundBankSFX.Add(currentLine);
                            }
                            else
                            {
                                string specificFormat = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platform, currentLine + ".txt");
                                if (File.Exists(specificFormat))
                                {
                                    soundBankSFX.Add(string.Format("{0}/{1}", platform, currentLine));
                                }
                                else
                                {
                                    soundBankSFX.Add(currentLine);
                                }
                            }
                            currentLine = fileData[index++].Trim();
                        }
                    }
                }
            }

            //Hashset to array
            string[] SfxArray = soundBankSFX.ToArray();
            Array.Sort(SfxArray);

            return SfxArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetSampleList(string[] SFXs, string outputLanguage)
        {
            HashSet<string> samplesList = new HashSet<string>();

            for (int i = 0; i < SFXs.Length; i++)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", SFXs[i] + ".txt");
                string[] fileData = File.ReadAllLines(filePath);
                int index = Array.IndexOf(fileData, "#SFXSamplePoolFiles") + 1;
                if (index > 0)
                {
                    string currentLine = fileData[index];
                    while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                    {
                        string sampleName = CommonFunctions.GetSampleFromSpeechFolder(currentLine, outputLanguage);
                        if (!string.IsNullOrEmpty(sampleName))
                        {
                            samplesList.Add(sampleName);
                        }
                        currentLine = fileData[index++].Trim();
                    }
                }
            }

            //Hashset to array
            string[] samplesArray = samplesList.ToArray();
            Array.Sort(samplesArray);

            return samplesArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetSampleList(Dictionary<string, SFX> fileData, string outputLanguage)
        {
            HashSet<string> samplesList = new HashSet<string>();

            foreach (KeyValuePair<string, SFX> sfxItem in fileData)
            {
                if (!sfxItem.Value.SamplePool.EnableSubSFX && sfxItem.Value.Samples.Count > 0)
                {
                    foreach (SfxSample sampleData in sfxItem.Value.Samples)
                    {
                        string sampleName = CommonFunctions.GetSampleFromSpeechFolder(sampleData.FilePath, outputLanguage);
                        if (!string.IsNullOrEmpty(sampleName))
                        {
                            samplesList.Add(sampleName);
                        }
                    }
                }
            }

            //Hashset to array
            string[] samplesArray = samplesList.ToArray();
            Array.Sort(samplesArray);

            return samplesArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal long GetSampleSize(string samplesFolder, SamplePool samplePool, string[] Samples)
        {
            long sampleSize = 0;

            for (int i = 0; i < Samples.Length; i++)
            {
                string fileName = MultipleFilesFunctions.GetFullFileName(Samples[i]);
                if (samplePool.SamplePoolItems.ContainsKey(fileName) && Path.HasExtension(fileName) && !Path.IsPathRooted(Samples[i]))
                {
                    //Get wave length
                    long waveLength = 0;
                    if (Directory.Exists(samplesFolder))
                    {
                        string samplePath = Path.Combine(samplesFolder, Samples[i]);
                        if (File.Exists(samplePath))
                        {
                            using (WaveFileReader WReader = new WaveFileReader(samplePath))
                            {
                                waveLength = WReader.Length;
                            }
                        }
                    }

                    //Count soundbank size
                    if (samplePool.SamplePoolItems[fileName].StreamMe)
                    {
                        sampleSize += 2 * Math.Max(waveLength, 1);
                    }
                    else
                    {
                        sampleSize += 4 * Math.Max(waveLength, 1);
                    }
                }
            }

            return sampleSize;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal long GetEstimatedOutputFileSize(string[] samplesList, SamplePool samplePool, string outputPlatform)
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
                    long masterWaveSize;
                    int masterWaveFreq;
                    using (WaveFileReader waveReader = new WaveFileReader(Path.Combine(GlobalPrefs.ProjectFolder, "Master", samplesList[i].TrimStart('\\'))))
                    {
                        masterWaveSize = waveReader.Length;
                        masterWaveFreq = waveReader.WaveFormat.SampleRate;
                    }

                    //ReSampled wave size
                    SamplePoolItem sampleItem = samplePool.SamplePoolItems[MultipleFilesFunctions.GetFullFileName(samplesList[i])];
                    int sampleRateIndex = GlobalPrefs.CurrentProject.ResampleRates.IndexOf(sampleItem.ReSampleRate);
                    int formatRate = GlobalPrefs.CurrentProject.platformData[outputPlatform].ReSampleRates[sampleRateIndex];
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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
