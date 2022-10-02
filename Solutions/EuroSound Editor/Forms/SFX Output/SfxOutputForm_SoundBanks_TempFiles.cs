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
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void WriteSfxFile(Dictionary<string, uint> hashCodesDict, Dictionary<string, SFX> fileData, string[] sampleList, string[] streamsList, string outputPlatform, string outputBank, BinaryWriter sfxWritter, bool isBigEndian, StreamWriter debugFile)
        {
            List<long> sfxLut = new List<long>();
            SoundBankFunctions sbFunctions = new SoundBankFunctions();

            //Sfx Header
            sfxWritter.Write(BytesFunctions.FlipInt32(fileData.Count, isBigEndian));
            foreach (KeyValuePair<string, SFX> sfxItem in fileData)
            {
                sfxWritter.Write(BytesFunctions.FlipInt32(sfxItem.Value.HashCode, isBigEndian));
                sfxWritter.Write(0);
            }

            //Sfx Parameter Entry
            int streamFileCheckSum = 0;
            foreach (KeyValuePair<string, SFX> sfxData in fileData)
            {
                if (abortQuickOutput)
                {
                    break;
                }
                sfxLut.Add(sfxWritter.BaseStream.Position);
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.Parameters.DuckerLength, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.SamplePool.MinDelay, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.SamplePool.MaxDelay, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.Parameters.InnerRadius, isBigEndian));
                sfxWritter.Write(BytesFunctions.FlipShort((short)sfxData.Value.Parameters.OuterRadius, isBigEndian));
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.ReverbSend);
                sfxWritter.Write((sbyte)sfxData.Value.Parameters.TrackingType);
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
                            Invoke(method: new Action(() => { MessageBox.Show(string.Format("HashCode Not Found {0}", sampleToCheck.FilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                        }
                    }
                    else
                    {
                        //If there is a missing sample, cancel output at this point. 
                        if (fastOutput)
                        {
                            string samplePath = sampleToCheck.FilePath;
                            string sampleFolder = "XBox_adpcm";
                            switch (outputPlatform.Trim().ToLower())
                            {
                                case "pc":
                                    sampleFolder = "PC";
                                    break;
                                case "playstation2":
                                    sampleFolder = "PlayStation2_VAG";
                                    samplePath = Path.ChangeExtension(sampleToCheck.FilePath, ".vag");
                                    break;
                                case "gamecube":
                                    sampleFolder = "GameCube_dsp_adpcm";
                                    samplePath = Path.ChangeExtension(sampleToCheck.FilePath, ".dsp");
                                    break;
                            }
                            string fullPath = Path.Combine(GlobalPrefs.ProjectFolder, sampleFolder, samplePath.TrimStart(Path.DirectorySeparatorChar));
                            if (!File.Exists(fullPath))
                            {
                                MessageBox.Show(string.Format("Output Error: Sample File Missing\n{0}\n\nIn SFX : {1}\nWithin SoundBank : {2}", fullPath, sfxData.Key, outputBank), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                abortQuickOutput = true;
                                break;
                            }
                        }

                        fileRef = (short)Array.FindIndex(sampleList, s => s.Equals(sampleToCheck.FilePath, StringComparison.OrdinalIgnoreCase));
                        if (fileRef == -1)
                        {
                            fileRef = (short)Array.FindIndex(streamsList, s => s.Equals(sampleToCheck.FilePath, StringComparison.OrdinalIgnoreCase));
                            if (fileRef >= 0)
                            {
                                fileRef += 1;
                                fileRef *= -1;

                                //Debug File
                                debugFile.WriteLine("{0}    \\{1}", fileRef, sampleToCheck.FilePath);
                                streamFileCheckSum -= fileRef;
                            }
                            else
                            {
                                Invoke(method: new Action(() => { MessageBox.Show(string.Format("Stream Ref Not Found {0}", sampleToCheck.FilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                            }
                        }
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

            if (!abortQuickOutput)
            {
                //Write Start Offsetss
                sfxWritter.BaseStream.Seek(4, SeekOrigin.Begin);
                for (int i = 0; i < sfxLut.Count; i++)
                {
                    sfxWritter.BaseStream.Seek(4, SeekOrigin.Current);
                    sfxWritter.Write(BytesFunctions.FlipUInt32((uint)sfxLut[i], isBigEndian));
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private long WriteSifFile(BinaryWriter sifWritter, BinaryWriter sbfWritter, string[] sampleList, string platform, List<byte[]> dspHeader, bool isBigEndian)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();
            long sampleBankSize = 0;
            sifWritter.Write(BytesFunctions.FlipInt32(sampleList.Length, isBigEndian));
            for (int i = 0; i < sampleList.Length; i++)
            {
                string masterFile = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", sampleList[i].TrimStart(Path.DirectorySeparatorChar));
                WavInfo masterFileData = new WavInfo();
                if (File.Exists(masterFile))
                {
                    masterFileData = wavFunctions.ReadWaveProperties(masterFile);
                }

                //-------------------------------------------------------------------------------[ PC ]-------------------------------------------------------------------
                if (platform.Equals("PC", StringComparison.OrdinalIgnoreCase))
                {
                    string pcFilepath = Path.Combine(GlobalPrefs.ProjectFolder, "PC", sampleList[i].TrimStart(Path.DirectorySeparatorChar));
                    if (File.Exists(pcFilepath))
                    {
                        WavInfo pcFileData = wavFunctions.ReadWaveProperties(pcFilepath);
                        byte[] pcmData = wavFunctions.GetByteWaveData(pcFilepath);

                        //Write Header Data
                        uint loopOffset = 0;
                        if (masterFileData.HasLoop)
                        {
                            loopOffset = BytesFunctions.AlignNumber((uint)CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileData.SampleRate, pcFileData.SampleRate, masterFileData.LoopStart * 2), 2);
                        }
                        sbFunctions.WriteSampleInfo(sifWritter, sbfWritter, masterFileData, pcFileData, BytesFunctions.AlignNumber((uint)pcFileData.Length, 4), (int)pcFileData.Length, i * 96, loopOffset, isBigEndian);

                        //Write Sample Data
                        byte[] filedata = new byte[BytesFunctions.AlignNumber((uint)pcFileData.Length, 4)];
                        Array.Copy(pcmData, filedata, pcmData.Length);
                        sbfWritter.Write(filedata);

                        //Update value
                        sampleBankSize += pcmData.Length;
                    }
                    else if (!fastOutput)
                    {
                        throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", pcFilepath));
                    }
                }

                //-------------------------------------------------------------------------------[ GameCube ]-------------------------------------------------------------------
                if (platform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
                {
                    string wavFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "GameCube", sampleList[i].TrimStart(Path.DirectorySeparatorChar));
                    string dspFilePath = Path.ChangeExtension(Path.Combine(GlobalPrefs.ProjectFolder, "GameCube_dsp_adpcm", sampleList[i].TrimStart(Path.DirectorySeparatorChar)), ".dsp");

                    if (File.Exists(wavFilePath))
                    {
                        WavInfo wavFileData = wavFunctions.ReadWaveProperties(wavFilePath);
                        if (File.Exists(dspFilePath))
                        {
                            byte[] dspData = CommonFunctions.RemoveFileHeader(dspFilePath, 96);
                            dspHeader.Add(sbFunctions.GetDspHeaderData(dspFilePath));

                            //Write Header Data
                            uint loopOffset = 0;
                            if (masterFileData.HasLoop)
                            {
                                loopOffset = (uint)CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileData.SampleRate, wavFileData.SampleRate, masterFileData.LoopStart * 2);
                            }
                            sbFunctions.WriteSampleInfo(sifWritter, sbfWritter, masterFileData, wavFileData, BytesFunctions.AlignNumber((uint)dspData.Length, 32), dspData.Length, i * 96, loopOffset, isBigEndian);

                            //Write Sample Data
                            byte[] filedata = new byte[BytesFunctions.AlignNumber((uint)dspData.Length, 32)];
                            Array.Copy(dspData, filedata, dspData.Length);
                            sbfWritter.Write(filedata);

                            //Update value
                            sampleBankSize += dspData.Length;
                        }
                        else
                        {
                            throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", dspFilePath));
                        }
                    }
                    else if (!fastOutput)
                    {
                        throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", wavFilePath));
                    }
                }

                //-------------------------------------------------------------------------------[ PlayStation 2 ]-------------------------------------------------------------------
                if (platform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
                {
                    string aifFilePath = Path.ChangeExtension(Path.Combine(GlobalPrefs.ProjectFolder, "PlayStation2", sampleList[i].TrimStart(Path.DirectorySeparatorChar)), ".aif");
                    string vagFilePath = Path.ChangeExtension(Path.Combine(GlobalPrefs.ProjectFolder, "PlayStation2_VAG", sampleList[i].TrimStart(Path.DirectorySeparatorChar)), ".vag");

                    if (File.Exists(aifFilePath))
                    {
                        WavInfo aifFileData = aiffFunctions.ReadWaveProperties(aifFilePath);
                        if (File.Exists(vagFilePath))
                        {
                            byte[] vagData = CommonFunctions.RemoveFileHeader(vagFilePath, 48);

                            //Write Header Data
                            uint loopOffset = 0;
                            if (masterFileData.HasLoop)
                            {
                                loopOffset = (uint)CalculusLoopOffset.RuleOfThreeLoopOffset(masterFileData.SampleRate, aifFileData.SampleRate, masterFileData.LoopStart * 2);
                            }
                            sbFunctions.WriteSampleInfo(sifWritter, sbfWritter, masterFileData, aifFileData, BytesFunctions.AlignNumber((uint)vagData.Length, 64), vagData.Length, i * 96, loopOffset, isBigEndian);

                            //Write Sample Data
                            byte[] filedata = new byte[BytesFunctions.AlignNumber((uint)vagData.Length, 64)];
                            Array.Copy(vagData, filedata, vagData.Length);
                            sbfWritter.Write(filedata);

                            //Update value
                            sampleBankSize += vagData.Length;
                        }
                        else
                        {
                            throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", vagFilePath));
                        }
                    }
                    else if (!fastOutput)
                    {
                        throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", aifFilePath));
                    }
                }

                //-------------------------------------------------------------------------------[ Xbox ]-------------------------------------------------------------------
                if (platform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) || platform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
                {
                    string wavFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "X Box", sampleList[i].TrimStart(Path.DirectorySeparatorChar));
                    if (File.Exists(wavFilePath))
                    {
                        string adpcmFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "XBox_adpcm", sampleList[i].TrimStart(Path.DirectorySeparatorChar));
                        if (File.Exists(adpcmFilePath))
                        {
                            byte[] adpcmData = CommonFunctions.RemoveFileHeader(adpcmFilePath, 48);

                            //Write Header Data
                            uint loopOffset = 0;
                            if (masterFileData.HasLoop)
                            {
                                loopOffset = CalculusLoopOffset.GetXboxAlignedNumber((uint)masterFileData.LoopStart);
                            }
                            sbFunctions.WriteSampleInfo(sifWritter, sbfWritter, masterFileData, wavFunctions.ReadWaveProperties(wavFilePath), (uint)adpcmData.Length, adpcmData.Length, i * 96, loopOffset, isBigEndian);

                            //Write Sample Data
                            sbfWritter.Write(adpcmData);

                            //Update value
                            sampleBankSize += adpcmData.Length;
                        }
                        else
                        {
                            throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", adpcmFilePath));
                        }
                    }
                    else if (!fastOutput)
                    {
                        throw new IOException(string.Format("Output Error: Sample File Missing: UNKNOWN SFX & BANK\n{0}", wavFilePath));
                    }
                }
            }

            return sampleBankSize;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
