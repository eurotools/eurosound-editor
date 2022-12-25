using ESUtils;
using sb_editor.Audio_Classes;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SFXForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void CreateTestSfx(string outputFolder, string fileName)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();

            //Ensure that the debug folder exists
            DirectoryInfo debugFolder = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Debug_Report"));

            //Get HashCodes Dictionary
            Dictionary<string, uint> HashCodesDict = sbFunctions.GetHashCodesDictionary("SFXs", "#HASHCODE");

            //Build SFX File
            Dictionary<string, SFX> sbFileData = sbFunctions.GetSfxDataDict(new string[] { Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common") }, "PC", "English");
            string[] samplesList = sbFunctions.GetSampleList(sbFileData, "English");
            sbFunctions.UpdateDuckerLength(sbFileData, "PC");

            //Get File Paths
            string debugFilePath = Path.Combine(debugFolder.FullName, string.Format("StreamDebugSoundBank_{0}_{1}_{2}.txt", "___SB_TEST_SFX___", "PC", "English"));
            string outputTempFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", "PC", "SoundBanks", "English", "65534.tmp");

            //Create Folder to store the Temporal SoundBank files
            Directory.CreateDirectory(Path.GetDirectoryName(outputTempFilePath));

            //Create Debug File
            using (StreamWriter sw = new StreamWriter(File.Open(debugFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("SoundBank Output Debug Data");
                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy"));
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                sw.WriteLine("");
                sw.WriteLine("SoundBankName = {0}", "___SB_TEST_SFX___");
                sw.WriteLine("SoundBankSaveName = {0}", 65534);
                sw.WriteLine("SoundBankFileName = {0}", Path.ChangeExtension(outputTempFilePath, ".sfx"));
                sw.WriteLine("Stream PoolFiles(n).FileRef");

                //Output temporal files
                using (BinaryWriter sbfWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outputTempFilePath, ".sbf"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    using (BinaryWriter sfxWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outputTempFilePath, ".sfx"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        using (BinaryWriter sifWritter = new BinaryWriter(File.Open(Path.ChangeExtension(outputTempFilePath, ".sif"), FileMode.Create, FileAccess.Write, FileShare.Read)))
                        {
                            //Write SFX Data
                            WriteSfxFile(HashCodesDict, sbFileData, samplesList, "___SB_TEST_SFX___", sfxWritter, false, sw);
                            WriteSifFile(sifWritter, sbfWritter, samplesList, false);
                        }
                    }
                }

                if (Directory.Exists(outputFolder))
                {
                    //Build MusX
                    string sbfTempFile = Path.ChangeExtension(outputTempFilePath, ".sbf");
                    string sfxTempFile = Path.ChangeExtension(outputTempFilePath, ".sfx");
                    string sifTempFile = Path.ChangeExtension(outputTempFilePath, ".sif");

                    //Get Output Path 
                    DirectoryInfo musXFolder = Directory.CreateDirectory(outputFolder);

                    //Build File
                    MusXBuild_Soundbank.BuildSoundbankFile(sfxTempFile, sifTempFile, sbfTempFile, string.Empty, Path.Combine(musXFolder.FullName, fileName), 65534, false);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void WriteSfxFile(Dictionary<string, uint> hashCodesDict, Dictionary<string, SFX> fileData, string[] sampleList, string outputBank, BinaryWriter sfxWritter, bool isBigEndian, StreamWriter debugFile)
        {
            List<long> sfxLut = new List<long>();
            SoundBankFunctions sbFunctions = new SoundBankFunctions();

            //Sfx Header
            sfxWritter.Write(BytesFunctions.FlipInt32(fileData.Count, isBigEndian));
            foreach (KeyValuePair<string, SFX> sfxItem in fileData)
            {
                sfxWritter.Write(BytesFunctions.FlipInt32(0, isBigEndian));
                sfxWritter.Write(0);
            }

            //Sfx Parameter Entry
            int streamFileCheckSum = 0;
            foreach (KeyValuePair<string, SFX> sfxData in fileData)
            {
                sfxLut.Add(sfxWritter.BaseStream.Position);
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.Parameters.DuckerLength, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.SamplePool.MinDelay, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.SamplePool.MaxDelay, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.Parameters.InnerRadius, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.Parameters.OuterRadius, isBigEndian));
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.ReverbSend);
                sfxWritter.Write((sbyte)0);
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.MaxVoices);
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.Priority);
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.Ducker);
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.MasterVolume);
                sfxWritter.Write((ushort)sbFunctions.GetFlags(sfxData.Value));

                //Calculate references
                sfxWritter.Write(BytesFunctions.FlipUShort((ushort)sfxData.Value.Samples.Count, isBigEndian));
                foreach (SfxSample sampleToCheck in sfxData.Value.Samples)
                {
                    int fileRef = 0;
                    if (sfxData.Value.SamplePool.EnableSubSFX)
                    {
                        string hashCode = Path.GetFileNameWithoutExtension(sampleToCheck.FilePath);
                        if (hashCodesDict.ContainsKey(hashCode))
                        {
                            fileRef = (short)hashCodesDict[hashCode];
                        }
                        else
                        {
                            MessageBox.Show(string.Format("HashCode Not Found {0}", sampleToCheck.FilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //If there is a missing sample, cancel output at this point. 
                        string samplePath = sampleToCheck.FilePath;
                        string fullPath = Path.Combine(GlobalPrefs.ProjectFolder, "Master", samplePath.TrimStart(Path.DirectorySeparatorChar));
                        if (!File.Exists(fullPath))
                        {
                            MessageBox.Show(string.Format("Output Error: Sample File Missing\n{0}\n\nIn SFX : {1}\nWithin SoundBank : {2}", fullPath, sfxData.Key, outputBank), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }

                        fileRef = (short)Array.FindIndex(sampleList, s => s.Equals(sampleToCheck.FilePath, StringComparison.OrdinalIgnoreCase));
                    }
                    sfxWritter.Write(BytesFunctions.FlipShort((short)fileRef, isBigEndian));
                    sfxWritter.Write(BytesFunctions.FlipShort((short)Math.Round(sampleToCheck.PitchOffset * 1024), isBigEndian));
                    sfxWritter.Write(BytesFunctions.FlipShort((short)Math.Round(sampleToCheck.RandomPitch * 1024), isBigEndian));
                    sfxWritter.Write(sampleToCheck.BaseVolume);
                    sfxWritter.Write(sampleToCheck.RandomVolume);
                    sfxWritter.Write(sampleToCheck.Pan);
                    sfxWritter.Write(sampleToCheck.RandomPan);
                    sfxWritter.Write((byte)0);
                    sfxWritter.Write((byte)0);
                }
            }
            debugFile.WriteLine("StreamFileRefCheckSum = {0}", streamFileCheckSum * -1);

            //Write Start Offsetss
            sfxWritter.BaseStream.Seek(4, SeekOrigin.Begin);
            for (int i = 0; i < sfxLut.Count; i++)
            {
                sfxWritter.BaseStream.Seek(4, SeekOrigin.Current);
                sfxWritter.Write(BytesFunctions.FlipUInt32((uint)sfxLut[i], isBigEndian));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private long WriteSifFile(BinaryWriter sifWritter, BinaryWriter sbfWritter, string[] sampleList, bool isBigEndian)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();
            WaveFunctions wavFunctions = new WaveFunctions();

            long sampleBankSize = 0;
            sifWritter.Write(BytesFunctions.FlipInt32(sampleList.Length, isBigEndian));
            for (int i = 0; i < sampleList.Length; i++)
            {
                string masterFile = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", sampleList[i].TrimStart(Path.DirectorySeparatorChar));
                if (File.Exists(masterFile))
                {
                    WavInfo masterFileData = wavFunctions.ReadWaveProperties(masterFile);
                    byte[] pcmData = wavFunctions.GetByteWaveData(masterFile);

                    //Write Header Data
                    uint loopOffset = 0;
                    if (masterFileData.HasLoop)
                    {
                        loopOffset = BytesFunctions.AlignNumber((uint)CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileData.SampleRate, masterFileData.SampleRate, masterFileData.LoopStart * 2), 2);
                    }
                    sbFunctions.WriteSampleInfo(sifWritter, sbfWritter, masterFileData, masterFileData, BytesFunctions.AlignNumber((uint)masterFileData.Length, 4), (int)masterFileData.Length, i * 96, loopOffset, isBigEndian);

                    //Write Sample Data
                    byte[] filedata = new byte[BytesFunctions.AlignNumber((uint)masterFileData.Length, 4)];
                    Array.Copy(pcmData, filedata, pcmData.Length);
                    sbfWritter.Write(filedata);

                    //Update value
                    sampleBankSize += pcmData.Length;
                }
                else
                {
                    throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", masterFile));
                }
            }

            return sampleBankSize;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
