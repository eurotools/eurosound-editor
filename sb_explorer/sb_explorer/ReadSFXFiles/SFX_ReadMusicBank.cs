using sb_explorer.EXObjects.Musicbanks;
using System;
using System.IO;
using System.Text;

namespace sb_explorer.ReadSFXFiles
{
    internal class SFX_ReadMusicBank
    {
        //*===============================================================================================
        //* MUSIC FILE
        //*===============================================================================================
        internal EXMusic LoadMusicFile(BinaryReader binaryReader, int interleave_block_size)
        {
            EXMusic MusicToAdd = null;
            bool sfxIsBigEndian = false;

            string Magic = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
            if (Magic.Equals("MUSX"))
            {
                //Hashcode for the current soundbank
                uint fileHashCode = binaryReader.ReadUInt32();

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

                    binaryReader.BaseStream.Seek(4, SeekOrigin.Current);

                    //File Start 2
                    uint FileStart2 = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    uint FileStart2Length = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);

                    //File Start 3
                    binaryReader.BaseStream.Seek(8, SeekOrigin.Current);

                    //Seek Position Section 1
                    binaryReader.BaseStream.Seek(FileStart1, SeekOrigin.Begin);

                    //Stream marker header data 
                    uint StartMarkersCount = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    uint MarkersCount = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian);
                    MusicToAdd = new EXMusic
                    {
                        //Format settings
                        Channels = 1,
                        Bits = 16,
                        Frequency = 32000,
                        HashCode = fileHashCode,

                        //Properties
                        StartMarkerOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                        MarkerOffset = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                        BaseVolume = GenericFunctions.FlipUInt32(binaryReader.ReadUInt32(), sfxIsBigEndian),
                    };

                    //Read Start Markers
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
                        MusicToAdd.m_MusicMarkerStartData.Add(StartMarker);
                    }

                    //Read Markers
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
                        MusicToAdd.Markers.Add(DataMarker);
                    }

                    //Read Section 2
                    uint TracksLength = FileStart2Length / 2;
                    bool InterleavedStereo = true;
                    int IndexLC = 0, IndexRC = 0;

                    //Seek Position
                    binaryReader.BaseStream.Seek(FileStart2, SeekOrigin.Begin);

                    //Save offset
                    MusicToAdd.AudioOffset = (uint)binaryReader.BaseStream.Position;

                    //Init arrays
                    MusicToAdd.SampleByteData_LeftChannel = new byte[TracksLength];
                    MusicToAdd.SampleByteData_RightChannel = new byte[TracksLength];

                    //Read Stereo interleaving
                    while (binaryReader.BaseStream.Position < (FileStart2 + FileStart2Length))
                    {
                        if (InterleavedStereo)
                        {
                            Buffer.BlockCopy(binaryReader.ReadBytes(interleave_block_size), 0, MusicToAdd.SampleByteData_LeftChannel, IndexLC, interleave_block_size);
                            IndexLC += interleave_block_size;
                        }
                        else
                        {
                            Buffer.BlockCopy(binaryReader.ReadBytes(interleave_block_size), 0, MusicToAdd.SampleByteData_RightChannel, IndexRC, interleave_block_size);
                            IndexRC += interleave_block_size;
                        }
                        InterleavedStereo = !InterleavedStereo;
                    }
                }
            }
            return MusicToAdd;
        }
    }
}
