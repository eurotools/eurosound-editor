//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// MUSX FUNCTIONS -- FINAL SFX FILES
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Text;
using static ESUtils.BytesFunctions;

namespace ESUtils
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class MusXBuild_MusicFile
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void BuildMusicFile(string mkrFilePath, string ssdFilePath, string OutputFilePath, string platform, uint fileHashCode)
        {
            //Ensure that the output file path is not null
            if (!string.IsNullOrEmpty(OutputFilePath))
            {
                bool isBigEndian = platform.Equals("GC__");

                //Create a new binary writer
                using (BinaryWriter binWriter = new BinaryWriter(File.Open(OutputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.ASCII))
                {
                    //--------------------------------------------------[File Header]--------------------------------------------------
                    //--magic[magic value]--
                    binWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
                    //--hashc[Hashcode for the current soundbank without the section prefix]--
                    binWriter.Write(fileHashCode | 0xE00000);
                    //--offst[Constant offset to the next section,]--
                    binWriter.Write(4);
                    //--fulls[Size of the whole file, in bytes. Unused. ]--
                    binWriter.Write(0);
                    //--Platform
                    binWriter.Write(Encoding.ASCII.GetBytes(platform));
                    //--Timespan
                    DateTime initialDate = new DateTime(2000, 1, 1, 1, 0, 0);
                    binWriter.Write((uint)(DateTime.Now.TimeOfDay - initialDate.TimeOfDay).TotalSeconds);
                    //--Adpcm Encoding
                    binWriter.Write(Convert.ToInt32(!platform.Equals("PS2_")));
                    //--Padding
                    binWriter.Write(0);

                    //--------------------------------------------------[File Sections]--------------------------------------------------
                    //--File start 1; an offset that points to the stream look-up file details. Set to 0x800 in the original software. --
                    binWriter.Write(0);
                    //--File length 1; size of the first section, in bytes. --
                    binWriter.Write(0);
                    //--File start 2; offset to the second section with the sample data. Set to 0x1000 in the original software. --
                    binWriter.Write(0);
                    //--File length 2; size of the second section, in bytes. --
                    binWriter.Write(0);

                    //--------------------------------------------------[Read and Write Files Content]--------------------------------------------------
                    uint positionAligned;

                    //Sound Marker File
                    uint soundMarkerFileStart = 0, soundMarkerFileLength = 0;
                    if (File.Exists(mkrFilePath))
                    {
                        //Read file Data
                        byte[] markersFileData = File.ReadAllBytes(mkrFilePath);
                        soundMarkerFileLength = (uint)markersFileData.Length;
                        //Align section 
                        positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                        soundMarkerFileStart = positionAligned;
                        //Write data
                        WriteAlignedDecoration(binWriter, positionAligned);
                        binWriter.Write(markersFileData);
                    }

                    //Sound Sample Data
                    uint soundSampleDataStart = 0, soundSampleDataLength = 0;
                    if (File.Exists(ssdFilePath))
                    {
                        //Read file Data
                        byte[] soundSampleData = File.ReadAllBytes(ssdFilePath);
                        soundSampleDataLength = (uint)soundSampleData.Length;
                        //Align section
                        positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                        soundSampleDataStart = positionAligned;
                        //Write data
                        WriteAlignedDecoration(binWriter, positionAligned);
                        binWriter.Write(soundSampleData);
                    }

                    //Get file length
                    long totalFileLength = binWriter.BaseStream.Position;

                    //--------------------------------------------------[Write Final offsets]--------------------------------------------------
                    //File Full Size
                    binWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
                    binWriter.Write((uint)totalFileLength);

                    //File length 1
                    binWriter.BaseStream.Seek(16, SeekOrigin.Current);
                    binWriter.Write(FlipUInt32(soundMarkerFileStart, isBigEndian));
                    binWriter.Write(FlipUInt32(soundMarkerFileLength, isBigEndian));

                    //File length 2
                    binWriter.Write(FlipUInt32(soundSampleDataStart, isBigEndian));
                    binWriter.Write(FlipUInt32(soundSampleDataLength, isBigEndian));
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
