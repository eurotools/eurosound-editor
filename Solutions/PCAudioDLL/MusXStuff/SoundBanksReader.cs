using PCAudioDLL.MusXStuff.Objects;
using System.IO;
using System.Text;

namespace PCAudioDLL.MusXStuff
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class SoundBanksReader
    {
        internal enum Flags
        {
            MaxReject = 0,
            NextFreeOneToUse = 1,
            IgnoreAge = 2,
            MultiSample = 3,
            RandomPick = 4,
            Shuffled = 5,
            Loop = 6,
            Polyphonic = 7,
            UnderWater = 8,
            PauseInNis = 9,
            HasSubSfx = 10,
            StealOnLouder = 11,
            TreatLikeMusic = 12
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal SfxHeaderData ReadSfxHeader(string filePath)
        {
            SfxHeaderData headerData = new SfxHeaderData();

            using (BinaryReader BReader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //Magic value MUSX
                string Magic = Encoding.ASCII.GetString(BReader.ReadBytes(4));
                if (Magic.Equals("MUSX"))
                {
                    //Hashcode for the current soundbank 
                    headerData.FileHashCode = BReader.ReadUInt32();
                    //Current version of the file
                    headerData.FileVersion = BReader.ReadUInt32();
                    //Size of the whole file, in bytes
                    headerData.FileSize = BReader.ReadUInt32();

                    //Section where soundbanks are stored
                    headerData.SFXStart = BReader.ReadUInt32();
                    //Size of the first section, in bytes
                    headerData.SFXLenght = BReader.ReadUInt32();

                    //Section where the sample properties are stored
                    headerData.SampleInfoStart = BReader.ReadUInt32();
                    //Size of the second section, in bytes. 
                    headerData.SampleInfoLenght = BReader.ReadUInt32();

                    //Section where the ADPCM metadata and parameters for the GameCube DSP are stored
                    headerData.SpecialSampleInfoStart = BReader.ReadUInt32();
                    //Size of the block, in bytes.
                    headerData.SpecialSampleInfoLength = BReader.ReadUInt32();

                    //Points to the beginning of the PCM data, where sound is actually stored. 
                    headerData.SampleDataStart = BReader.ReadUInt32();
                    //Size of the block, in bytes. 
                    headerData.SampleDataLength = BReader.ReadUInt32();
                }

                //Close
                BReader.Close();
            }

            return headerData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal Sample ReadSoundbank(string filePath, SfxHeaderData headerData, ref SampleData[] wavesList)
        {
            Sample sample = null;
            using (BinaryReader BReader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //Go to SFX Start
                BReader.BaseStream.Seek(headerData.SFXStart, SeekOrigin.Begin);

                //Loop througt stored elements
                uint sfxCount = BReader.ReadUInt32();
                for (int i = 0; i < sfxCount; i++)
                {
                    uint hashcode = BReader.ReadUInt32();
                    uint sfxPos = BReader.ReadUInt32();
                    long prevPos = BReader.BaseStream.Position;

                    //go to sound offset
                    BReader.BaseStream.Seek(sfxPos + headerData.SFXStart, SeekOrigin.Begin);

                    //Read sound properties
                    sample = new Sample
                    {
                        DuckerLenght = BReader.ReadInt16(),
                        MinDelay = BReader.ReadInt16(),
                        MaxDelay = BReader.ReadInt16(),
                        InnerRadius = BReader.ReadInt16(),
                        OuterRadius = BReader.ReadInt16(),
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
                    ushort sfxSamplesCount = BReader.ReadUInt16();

                    //Loop througt all SFX samples
                    for (int j = 0; j < sfxSamplesCount; j++)
                    {
                        //Read sample properties
                        SampleInfo samplePoolItem = new SampleInfo()
                        {
                            FileRef = BReader.ReadInt16(),
                            Pitch = (float)decimal.Divide(BReader.ReadInt16(), 1024),
                            PitchOffset = (float)decimal.Divide(BReader.ReadInt16(), 1024),
                            Volume = (float)decimal.Divide(BReader.ReadSByte(), 100),
                            VolumeOffset = (float)decimal.Divide(BReader.ReadSByte(), 100),
                            Pan = (float)decimal.Divide(BReader.ReadSByte(), 100),
                            PanOffset = (float)decimal.Divide(BReader.ReadSByte(), 100),
                        };
                        sample.samplesList.Add(samplePoolItem);

                        //Padding
                        BReader.BaseStream.Seek(2, SeekOrigin.Current);
                    }

                    //Read data to show in the Hex viewer
                    BReader.BaseStream.Seek(sfxPos + headerData.SFXStart, SeekOrigin.Begin);

                    //return 
                    BReader.BaseStream.Seek(prevPos, SeekOrigin.Begin);
                }

                //Go to sample info section
                BReader.BaseStream.Seek(headerData.SampleInfoStart, SeekOrigin.Begin);
                wavesList = new SampleData[BReader.ReadInt32()];
                for (int i = 0; i < wavesList.Length; i++)
                {
                    SampleData wavHeaderData = new SampleData
                    {
                        Flags = (ushort)BReader.ReadUInt32(),
                        Address = BReader.ReadInt32(),
                        MemorySize = BReader.ReadInt32(),
                        Frequency = BReader.ReadInt32(),
                        SampleSize = BReader.ReadInt32(),
                        Channels = BReader.ReadInt32(),
                        Bits = BReader.ReadInt32(),
                        PsiSampleHeader = BReader.ReadInt32(),
                        LoopStartOffset = BReader.ReadInt32(),
                        Duration = BReader.ReadInt32()
                    };

                    //Store current position
                    long prevPos = BReader.BaseStream.Position;

                    //Read audio pcm data
                    BReader.BaseStream.Seek(headerData.SampleDataStart + wavHeaderData.Address, SeekOrigin.Begin);
                    wavHeaderData.EncodedData = BReader.ReadBytes(wavHeaderData.SampleSize);

                    //Store data
                    wavesList[i] = wavHeaderData;

                    //Return to previous position
                    BReader.BaseStream.Seek(prevPos, SeekOrigin.Begin);
                }
            }

            return sample;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
