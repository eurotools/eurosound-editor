using sb_editor.Audio_Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using static ESUtils.Enumerations;

namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CreateTempSfxData(string fileName, string sfxFilePath, string[] outLanguages, SamplePool StreamSamplesList)
        {
            WaveFunctions waveFunction = new WaveFunctions();
            SFX sfxFileData = TextFiles.ReadSfxFile(sfxFilePath);
            if (sfxFileData.Samples.Count > 0)
            {
                float waveDuration = 0.00002267574F;
                WavInfo waveFileData = new WavInfo();
                bool quitLoop = false;
                for(int i = 0; i < outLanguages.Length; i++)
                {
                    if (quitLoop)
                    {
                        break;
                    }
                    Language outLang = (Language)Enum.Parse(typeof(Language), outLanguages[i], true);
                    string sampleRelPath = CommonFunctions.GetSampleFromSpeechFolder(sfxFileData.Samples[0].FilePath.TrimStart(Path.DirectorySeparatorChar), outLang);
                    string sampleFullPath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", sampleRelPath);
                    if (File.Exists(sampleFullPath))
                    {
                        waveFileData = waveFunction.ReadWaveProperties(sampleFullPath);
                        waveDuration = (float)decimal.Divide(waveFileData.Length, waveFileData.AverageBytesPerSecond);
                    }
                    else
                    {
                        sfxFileData.Parameters.TrackingType = 4;
                    }

                    //Check if streamed
                    bool streamed = false;
                    string sampleName = Path.DirectorySeparatorChar + sfxFileData.Samples[0].FilePath;
                    if (StreamSamplesList.SamplePoolItems.ContainsKey(sampleName))
                    {
                        streamed = StreamSamplesList.SamplePoolItems[sampleName].StreamMe;
                    }

                    //Get File Path
                    string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempSfxData", fileName + ".txt");
                    if (sampleRelPath.IndexOf("Speech", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string outputFolder = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempSfxData", outLang.ToString())).FullName;
                        filePath = Path.Combine(outputFolder, fileName + ".txt");
                    }
                    else
                    {
                        quitLoop = true;
                    }

                    //Write data
                    using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        sw.WriteLine(sampleFullPath);
                        sw.WriteLine(string.Format("{{ {0} ,  {1} ,  {2} ,  {3}f ,  {4} ,  {5} , {6}, {7} }} ,  // {8}",
                            sfxFileData.HashCode,
                            sfxFileData.Parameters.InnerRadius.ToString("0.0", GlobalPrefs.NumericProvider),
                            sfxFileData.Parameters.OuterRadius.ToString("0.0", GlobalPrefs.NumericProvider),
                            GetWaveDurationFormatted(waveDuration),
                            Math.Max(Convert.ToByte(waveFileData.HasLoop), Convert.ToByte(sfxFileData.SamplePool.isLooped)),
                            sfxFileData.Parameters.TrackingType,
                            Convert.ToByte(streamed),
                            sfxFileData.Parameters.TrackingType == 2,
                            Path.GetFileNameWithoutExtension(sfxFilePath)));
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void GetHashCodesWithLabels(SortedDictionary<string, int> hashCodesDict, SortedDictionary<string, int> soundBankDict)
        {
            //SFXs
            if (hashCodesDict != null)
            {
                hashCodesDict.Clear();
                IEnumerable<string> filesToCheck = Directory.EnumerateFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                foreach (string filePath in filesToCheck)
                {
                    SFX sfxFileDat = TextFiles.ReadSfxFile(filePath);
                    hashCodesDict.Add(Path.GetFileNameWithoutExtension(filePath), sfxFileDat.HashCode);
                }
            }

            //SoundBank
            if (soundBankDict != null)
            {
                soundBankDict.Clear();
                IEnumerable<string> soundbankToCheck = Directory.EnumerateFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Soundbanks"), "*.txt", SearchOption.TopDirectoryOnly);
                foreach (string filePath in soundbankToCheck)
                {
                    SoundBank sndFileDat = TextFiles.ReadSoundbankFile(filePath);
                    soundBankDict.Add(Path.GetFileNameWithoutExtension(filePath), sndFileDat.HashCode);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string GetWaveDurationFormatted(float waveDuration)
        {
            //Get the number of decimal places
            string stringValue = waveDuration.ToString(GlobalPrefs.NumericProvider);
            int decimalPointIndex = stringValue.IndexOf(".");
            string duration = waveDuration.ToString(".#######", GlobalPrefs.NumericProvider);

            //Format String
            if (decimalPointIndex > -1 && stringValue.Length - decimalPointIndex > 8)
            {
                duration = waveDuration.ToString("0.######E+00", GlobalPrefs.NumericProvider);
            }
            return duration;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
