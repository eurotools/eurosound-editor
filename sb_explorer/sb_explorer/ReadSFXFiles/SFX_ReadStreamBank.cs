using System;
using System.Collections;
using System.IO;
using System.Text;

namespace sb_explorer.ReadSFXFiles
{
    internal class SFX_ReadStreamBank
    {
        bool sfxIsBigEndian = false;

        //*===============================================================================================
        //* STREAM FILE
        //*===============================================================================================
        internal uint LoadStreamFile(BinaryReader binaryReader, ArrayList StreamFileDictionaryData)
        {
            uint fileHashcode = 0;
            //Clear Dictionaries
            StreamFileDictionaryData.Clear();

            //Start reading
            string Magic = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
            if (Magic.Equals("MUSX"))
            {
                fileHashcode = binaryReader.ReadUInt32();

                //Check Version
                if (binaryReader.ReadUInt32() == 0xC9)
                {
                    //File Full Size
                    binaryReader.ReadUInt32();

                    //File Start 1
                    uint FileStart1 = binaryReader.ReadUInt32();
                    if (FileStart1 != 0x0800)
                    {
                        sfxIsBigEndian = true;
                        FileStart1 = 0x0800;
                    }
                    uint FileStart1Length = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);

                    //File Start 2
                    uint FileStart2 = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //File Start 3
                    binaryReader.BaseStream.Seek(8, SeekOrigin.Current);

                    //Read Section 1
                    uint NumberOfElements = FileStart1Length / 4;
                    uint[] ElementsToLoad = new uint[NumberOfElements];

                    //Go to section
                    binaryReader.BaseStream.Seek(FileStart1, SeekOrigin.Begin);

                    //Read Offsets
                    for (int i = 0; i < NumberOfElements; i++)
                    {
                        ElementsToLoad[i] = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    }

                    //Read Section 2
                    for (int i = 0; i < ElementsToLoad.Length; i++)
                    {
                        binaryReader.BaseStream.Seek(FileStart2 + ElementsToLoad[i], SeekOrigin.Begin);

                        EXSoundStream StreamSoundToAdd = new EXSoundStream
                        {
                            //Details
                            MarkerSize = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            AudioOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            AudioSize = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian)
                        };

                        //Stream marker header data 
                        uint StartMarkersCount = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                        uint MarkersCount = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                        StreamSoundToAdd.StartMarkerOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                        StreamSoundToAdd.MarkerOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                        StreamSoundToAdd.BaseVolume = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);

                        //Stream marker start data
                        for (int j = 0; j < StartMarkersCount; j++)
                        {
                            EXStreamStartMarker StartMarker = new EXStreamStartMarker
                            {
                                Index = GenericFunctions.FlipInt32(binaryReader.ReadInt32(), sfxIsBigEndian),
                                Position = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                Type = (byte)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                Flags = (byte)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                Extra = (byte)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                LoopStart = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                MarkerCount = (int)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                LoopMarkerCount = (int)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),

                                //StartMarker
                                MarkerPos = (int)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                IsInstant = Convert.ToBoolean(GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian)),
                                InstantBuffer = Convert.ToBoolean(GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian)),
                                StateA = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                StateB = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian)
                            };

                            //Add marker
                            StreamSoundToAdd.m_MusicMarkerStartData.Add(StartMarker);
                        }

                        //Stream marker data 
                        for (int k = 0; k < MarkersCount; k++)
                        {
                            EXStreamMarker DataMarker = new EXStreamMarker
                            {
                                Index = GenericFunctions.FlipInt32(binaryReader.ReadInt32(), sfxIsBigEndian),
                                Position = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                Type = (byte)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                Flags = (byte)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                Extra = (byte)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                LoopStart = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                MarkerCount = (int)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                                LoopMarkerCount = (int)GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                            };

                            //Add marker
                            StreamSoundToAdd.Markers.Add(DataMarker);
                        }

                        //Read Audio Data
                        binaryReader.BaseStream.Seek(FileStart2 + StreamSoundToAdd.AudioOffset, SeekOrigin.Begin);
                        StreamSoundToAdd.SampleByteData = binaryReader.ReadBytes((int)StreamSoundToAdd.AudioSize);
                        StreamSoundToAdd.Frequency = 22050;
                        StreamSoundToAdd.Channels = 1;
                        StreamSoundToAdd.Bits = 16;

                        //Add Sound to Dictionary
                        StreamFileDictionaryData.Add(StreamSoundToAdd);
                    }
                }
            }
            binaryReader.Close();

            return fileHashcode;
        }
    }
}
