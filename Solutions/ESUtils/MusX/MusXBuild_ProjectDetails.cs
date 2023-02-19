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
    public static class MusXBuild_ProjectDetails
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void BuildProjectDetailsFile(string projDataFilePath, string OutputFilePath, string platform, uint fileHashCode, bool bigEndian)
        {
            //Ensure that the output file path is not null
            if (!string.IsNullOrEmpty(OutputFilePath))
            {
                //Create a new binary writer
                using (BinaryWriter binWriter = new BinaryWriter(File.Open(OutputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.ASCII))
                {
                    //--------------------------------------------------[File Header]--------------------------------------------------
                    //--magic[magic value]--
                    binWriter.Write(Encoding.ASCII.GetBytes("MUSX"));
                    //--hashc[Hashcode for the current soundbank without the section prefix]--
                    binWriter.Write(fileHashCode);
                    //--version[Current version of the MusX file]--
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
                    //--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--
                    binWriter.Write(0);
                    //--sfxlength[size of the first section, in bytes]--
                    binWriter.Write(0);
                   
                    //--------------------------------------------------[Read and Write Files Content]--------------------------------------------------
                    //Write Hashcodes SFX Section
                    uint positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                    uint projDataStart = FlipUInt32(positionAligned, bigEndian);
                    uint projDataLength = 0;
                    if (File.Exists(projDataFilePath))
                    {
                        byte[] sfxFileData = File.ReadAllBytes(projDataFilePath);
                        if (sfxFileData.Length > 0)
                        {
                            projDataLength = FlipUInt32((uint)sfxFileData.Length, bigEndian);
                            WriteAlignedDecoration(binWriter, positionAligned);
                            binWriter.Write(sfxFileData);
                        }
                    }

                    //--------------------------------------------------[Write Final offsets]--------------------------------------------------
                    uint fileSize = (uint)binWriter.BaseStream.Position;
                    binWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
                    binWriter.Write(fileSize);
                    binWriter.BaseStream.Seek(0x20, SeekOrigin.Begin);
                    binWriter.Write(projDataStart);
                    binWriter.Write(projDataLength);
                }
            }
        }


    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
