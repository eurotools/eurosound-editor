using sb_explorer.EXObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace sb_explorer.ReadSFXFiles
{
    internal class SFX_ReadSoundBank
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private bool sfxIsBigEndian = false;

        //*===============================================================================================
        //* SOUNDBANKS
        //*===============================================================================================
        internal uint LoadSoundBankFile(BinaryReader binaryReader, Dictionary<uint, EXSound> SoundBanksSFXDictionaryData, SortedDictionary<short, EXAudio> SoundBanksMediaDictionaryData)
        {
            uint HashCode = 0;

            //magic value MUSX
            string Magic = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
            if (Magic.Equals("MUSX"))
            {
                //Hashcode for the current soundbank
                HashCode = binaryReader.ReadUInt32();

                //Constant offset
                if (binaryReader.ReadUInt32() == 0xC9)
                {
                    //Clear Hashtables
                    SoundBanksMediaDictionaryData.Clear();
                    SoundBanksSFXDictionaryData.Clear();

                    //File Full Size
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //SFX start
                    uint sfxStartSection = binaryReader.ReadUInt32();
                    if (sfxStartSection != 0x0800)
                    {
                        sfxIsBigEndian = true;
                        sfxStartSection = 0x0800;
                    }

                    //SFX length
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //Sample info start
                    uint sampleInfoStart = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    //Sample info start length
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //Special sample info start
                    uint specialSampleInfo = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    //Special sample info length
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //Sample data start
                    uint sampleDataStart = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    //Sample data length
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //Go to SFX Start
                    binaryReader.BaseStream.Seek(sfxStartSection, SeekOrigin.Begin);
                    uint sfxCount = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);

                    //Loop througt stored elements
                    for (int i = 0; i < sfxCount; i++)
                    {
                        uint soundHashCode = 0x1A000000 | GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                        uint soundOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                        long prevPosition = binaryReader.BaseStream.Position;

                        //go to sound offset
                        binaryReader.BaseStream.Seek(soundOffset + sfxStartSection, SeekOrigin.Begin);

                        //Read sound properties
                        EXSound sfxObject = new EXSound
                        {
                            Hashcode = soundHashCode,
                            DuckerLenght = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                            MinDelay = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                            MaxDelay = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                            InnerRadiusReal = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                            OuterRadiusReal = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                            ReverbSend = binaryReader.ReadSByte(),
                            TrackingType = binaryReader.ReadSByte(),
                            MaxVoices = binaryReader.ReadSByte(),
                            Priority = binaryReader.ReadSByte(),
                            Ducker = binaryReader.ReadSByte(),
                            MasterVolume = binaryReader.ReadSByte()
                        };
                        sfxObject.FlagsOffset = (uint)(binaryReader.BaseStream.Position - (soundOffset + sfxStartSection));
                        sfxObject.Flags = binaryReader.ReadUInt16();

                        //get samples count
                        sfxObject.SamplePoolOffset = (uint)(binaryReader.BaseStream.Position - (soundOffset + sfxStartSection));
                        ushort sfxSamplesCount = GenericFunctions.FlipUShort(binaryReader.ReadUInt16(), sfxIsBigEndian);

                        //Loop througt all SFX samples
                        for (int j = 0; j < sfxSamplesCount; j++)
                        {
                            //Read sample properties
                            EXSample sfxSample = new EXSample()
                            {
                                FileRef = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                                Pitch = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                                PitchOffset = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian),
                                Volume = binaryReader.ReadSByte(),
                                VolumeOffset = binaryReader.ReadSByte(),
                                Pan = binaryReader.ReadSByte(),
                                PanOffset = binaryReader.ReadSByte()
                            };

                            //Check if is a stream sound
                            if (sfxSample.FileRef < 0)
                            {
                                sfxSample.IsStreamed = true;
                            }

                            //Check if is linked to another SFX
                            if (Convert.ToBoolean((sfxObject.Flags >> 10) & 1))
                            {
                                sfxSample.HasSubSfx = true;
                            }

                            //Add sample
                            sfxObject.Samples.Add(sfxSample);

                            //Padding
                            binaryReader.BaseStream.Seek(2, SeekOrigin.Current);
                        }

                        //Read data to show in the Hex viewer
                        sfxObject.BinaryLength = (uint)(binaryReader.BaseStream.Position - (soundOffset + sfxStartSection));
                        binaryReader.BaseStream.Seek(soundOffset + sfxStartSection, SeekOrigin.Begin);
                        sfxObject.BinaryData = binaryReader.ReadBytes((int)sfxObject.BinaryLength);

                        //Add sound to the dictionary
                        if (SoundBanksSFXDictionaryData.ContainsKey(soundHashCode))
                        {
                            MessageBox.Show("This SFX contains duplicated hashcodes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            SoundBanksSFXDictionaryData.Add(soundHashCode, sfxObject);
                        }

                        //Return to previous position
                        binaryReader.BaseStream.Seek(prevPosition, SeekOrigin.Begin);
                    }

                    //Go to sample info section
                    binaryReader.BaseStream.Seek(sampleInfoStart, SeekOrigin.Begin);
                    uint samplesCount = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);

                    //Loop througt all Samples
                    for (int j = 0; j < samplesCount; j++)
                    {
                        EXAudio newAudio = new EXAudio
                        {
                            Flags = (ushort)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            Address = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            MemorySize = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            Frequency = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            SampleSize = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            Channels = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            Bits = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            RealBits = 16,
                            PSIsample = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            LoopStartOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            DurationInMilliseconds = binaryReader.ReadUInt32(),
                        };

                        long prevPosition = binaryReader.BaseStream.Position;

                        //Read audio pcm data
                        binaryReader.BaseStream.Seek(sampleDataStart + newAudio.Address, SeekOrigin.Begin);
                        newAudio.SampleByteData = binaryReader.ReadBytes((int)newAudio.SampleSize);

                        //Add object to dictionary
                        SoundBanksMediaDictionaryData.Add((short)j, newAudio);

                        //Read special sample section
                        if (specialSampleInfo != sampleDataStart)
                        {
                            binaryReader.BaseStream.Seek(specialSampleInfo + newAudio.PSIsample, SeekOrigin.Begin);
                            binaryReader.BaseStream.Seek(28, SeekOrigin.Current);
                            short[] coefs = new short[16];
                            for (int i = 0; i < coefs.Length; i++)
                            {
                                coefs[i] = GenericFunctions.FlipShort(binaryReader.ReadInt16(), sfxIsBigEndian);
                            }
                            newAudio.DspCoefs = coefs;
                        }

                        //Return to previous position
                        binaryReader.BaseStream.Seek(prevPosition, SeekOrigin.Begin);
                    }
                }
            }
            binaryReader.Close();

            return HashCode;
        }
    }
}
