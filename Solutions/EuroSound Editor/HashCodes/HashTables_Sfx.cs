using sb_editor.Audio_Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CreateTempSfxData(string filePath, string sfxFilePath, SamplePool StreamSamplesList)
        {
            //Create
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                WaveFunctions waveFunction = new WaveFunctions();
                SFX sfxFileData = TextFiles.ReadSfxFile(sfxFilePath);
                if (sfxFileData.Samples.Count > 0)
                {
                    float waveDuration = 0.00002267574F;
                    WavInfo waveFileData = new WavInfo();
                    string samplePath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", sfxFileData.Samples[0].FilePath.TrimStart(Path.DirectorySeparatorChar));
                    if (File.Exists(samplePath))
                    {
                        waveFileData = waveFunction.ReadWaveProperties(samplePath);
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

                    //Write data
                    sw.WriteLine(samplePath);
                    sw.WriteLine(string.Format("{{ {0} ,  {1}f ,  {2}f ,  {3}f ,  {4}f , {5}, {6}, {7} }} ,  // {8}", sfxFileData.HashCode, sfxFileData.Parameters.InnerRadius.ToString("0.0", GlobalPrefs.NumericProvider), sfxFileData.Parameters.OuterRadius.ToString("0.0", GlobalPrefs.NumericProvider), sfxFileData.Parameters.Alertness.ToString("0.0", GlobalPrefs.NumericProvider), GetWaveDurationFormatted(waveDuration), Math.Max(Convert.ToByte(waveFileData.HasLoop), Convert.ToByte(sfxFileData.SamplePool.isLooped)), sfxFileData.Parameters.TrackingType, Convert.ToByte(streamed), Path.GetFileNameWithoutExtension(sfxFilePath)));
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
                foreach(string filePath in filesToCheck)
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
