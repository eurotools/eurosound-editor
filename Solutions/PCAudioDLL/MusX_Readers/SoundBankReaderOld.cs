using PCAudioDLL.MusX_Objects;
using System.Collections.Generic;
using System.IO;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class SoundBankReaderOld : SoundBankReader
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal void ReadSoundbank(string filePath, SoundbankHeader headerData, SortedDictionary<uint, Sample> samplesDictionary, List<SampleData> wavesList, List<uint> duplicatedHashCodes)
        {
            using (BinaryReader BReader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //Go to SFX Start
                BReader.BaseStream.Seek(headerData.SFXStart, SeekOrigin.Begin);

                //Loop througt stored elements
                uint sfxCount = BinaryFunctions.FlipData(BReader.ReadUInt32(), headerData.IsBigEndian);
                for (int i = 0; i < sfxCount; i++)
                {
                    uint hashcode = BinaryFunctions.FlipData(BReader.ReadUInt32(), headerData.IsBigEndian);
                    uint sfxPos = BinaryFunctions.FlipData(BReader.ReadUInt32(), headerData.IsBigEndian);
                    long prevPos = BReader.BaseStream.Position;

                    //go to sound offset
                    BReader.BaseStream.Seek(sfxPos + headerData.SFXStart, SeekOrigin.Begin);

                    //Read sound properties
                    Sample sample = new Sample
                    {
                        DuckerLenght = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian),
                        MinDelay = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian),
                        MaxDelay = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian),
                        InnerRadius = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian),
                        OuterRadius = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian),
                        ReverbSend = BReader.ReadSByte(),
                        TrackingType = BReader.ReadSByte(),
                        MaxVoices = BReader.ReadSByte(),
                        Priority = BReader.ReadSByte(),
                        Ducker = BReader.ReadSByte(),
                        MasterVolume = BReader.ReadSByte()
                    };

                    //Read flags
                    sample.Flags = BReader.ReadUInt16();

                    //get samples count
                    ushort sfxSamplesCount = BinaryFunctions.FlipData(BReader.ReadUInt16(), headerData.IsBigEndian);

                    //Loop througt all SFX samples
                    for (int j = 0; j < sfxSamplesCount; j++)
                    {
                        //Read sample properties
                        SampleInfo samplePoolItem = new SampleInfo()
                        {
                            FileRef = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian),
                            Pitch = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian) / 1024.0f,
                            PitchOffset = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian) / 1024.0f,
                            Volume = BReader.ReadSByte() / 100.0f,
                            VolumeOffset = BReader.ReadSByte() / 100.0f,
                            Pan = BReader.ReadSByte() / 100.0f,
                            PanOffset = BReader.ReadSByte() / 100.0f
                        };
                        sample.samplesList.Add(samplePoolItem);

                        //Padding
                        BReader.BaseStream.Seek(2, SeekOrigin.Current);
                    }

                    //Save in dictionary
                    if (samplesDictionary.ContainsKey(hashcode))
                    {
                        duplicatedHashCodes.Add(hashcode);
                    }
                    else
                    {
                        samplesDictionary.Add(hashcode, sample);
                    }

                    //Read data to show in the Hex viewer
                    BReader.BaseStream.Seek(sfxPos + headerData.SFXStart, SeekOrigin.Begin);

                    //return 
                    BReader.BaseStream.Seek(prevPos, SeekOrigin.Begin);
                }

                //Go to sample info section
                BReader.BaseStream.Seek(headerData.SampleInfoStart, SeekOrigin.Begin);
                uint waveCount = BinaryFunctions.FlipData(BReader.ReadUInt32(), headerData.IsBigEndian);
                for (int i = 0; i < waveCount; i++)
                {
                    SampleData wavHeaderData = new SampleData
                    {
                        Flags = (ushort)BinaryFunctions.FlipData(BReader.ReadUInt32(), headerData.IsBigEndian),
                        Address = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        MemorySize = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        Frequency = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        SampleSize = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        Channels = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        Bits = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        PsiSampleHeader = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        LoopStartOffset = BinaryFunctions.FlipData(BReader.ReadInt32(), headerData.IsBigEndian),
                        Duration = BReader.ReadInt32(),
                    };

                    //Store current position
                    long prevPos = BReader.BaseStream.Position;

                    //Read audio pcm data
                    BReader.BaseStream.Seek(headerData.SampleDataStart + wavHeaderData.Address, SeekOrigin.Begin);
                    wavHeaderData.EncodedData = BReader.ReadBytes(wavHeaderData.SampleSize);

                    //Read coeffs
                    if (headerData.SpecialSampleInfoLength > 0)
                    {
                        BReader.BaseStream.Seek(headerData.SpecialSampleInfoStart + wavHeaderData.PsiSampleHeader, SeekOrigin.Begin);
                        BReader.BaseStream.Seek(28, SeekOrigin.Current);
                        wavHeaderData.DspCoeffs = new short[16];
                        for (int j = 0; j < wavHeaderData.DspCoeffs.Length; j++)
                        {
                            wavHeaderData.DspCoeffs[j] = BinaryFunctions.FlipData(BReader.ReadInt16(), headerData.IsBigEndian);
                        }
                    }

                    //Store data
                    wavesList.Add(wavHeaderData);

                    //Return to previous position
                    BReader.BaseStream.Seek(prevPos, SeekOrigin.Begin);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
