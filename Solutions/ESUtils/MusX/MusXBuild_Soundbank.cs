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
    public static class MusXBuild_Soundbank
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void BuildSoundbankFile(string sfxFilePath, string sifFilePath, string sbFilePath, string ssFilePath, string OutputFilePath, string platform, int fileHashCode, bool bigEndian)
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

                    //--sampleinfostart[offset to the second section where the sample properties are stored]--
                    binWriter.Write(0);
                    //--sampleinfolen[size of the second section, in bytes]--
                    binWriter.Write(0);

                    //--specialsampleinfostart[used for gamecube adpcm struct info]--
                    binWriter.Write(0);
                    //--specialsampleinfolen[Size of the block, in bytes]--
                    binWriter.Write(0);

                    //--sampledatastart[Offset that points to the beginning of the PCM data, where sound Is actually stored]--
                    binWriter.Write(0);
                    //--sampledatalen[Size of the block, in bytes]--
                    binWriter.Write(0);

                    //--------------------------------------------------[Read and Write Files Content]--------------------------------------------------
                    //Write Hashcodes SFX Section
                    uint positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                    uint SFXStart = FlipUInt32(positionAligned, bigEndian);
                    uint SFXLength = 0;
                    if (File.Exists(sfxFilePath))
                    {
                        byte[] sfxFileData = File.ReadAllBytes(sfxFilePath);
                        if (sfxFileData.Length > 0)
                        {
                            SFXLength = FlipUInt32((uint)sfxFileData.Length, bigEndian);
                            WriteAlignedDecoration(binWriter, positionAligned);
                            binWriter.Write(sfxFileData);
                            positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                        }
                    }

                    //Write SampleInfo SFX Section
                    uint SampleInfoStart = FlipUInt32(positionAligned, bigEndian);
                    uint SampleInfoLength = 0;
                    if (File.Exists(sifFilePath))
                    {
                        byte[] sifFileData = File.ReadAllBytes(sifFilePath);
                        if (sifFileData.Length > 0)
                        {
                            SampleInfoLength = FlipUInt32((uint)sifFileData.Length, bigEndian);
                            WriteAlignedDecoration(binWriter, positionAligned);
                            binWriter.Write(sifFileData);
                            positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                        }
                    }

                    //Write special section
                    uint SpecialSampleInfoStart = FlipUInt32(positionAligned, bigEndian);
                    uint SpecialSampleInfoLength = 0;
                    if (File.Exists(ssFilePath))
                    {
                        byte[] ssfFileData = File.ReadAllBytes(ssFilePath);
                        if (ssfFileData.Length > 0)
                        {
                            SpecialSampleInfoLength = FlipUInt32((uint)ssfFileData.Length, bigEndian);
                            WriteAlignedDecoration(binWriter, positionAligned);
                            binWriter.Write(ssfFileData);
                            positionAligned = AlignNumber((uint)binWriter.BaseStream.Position, 0x800);
                        }
                    }

                    //Write Sample Data SFX Section
                    uint SampleDataStart = FlipUInt32(positionAligned, bigEndian);
                    uint SampleDataLength = 0;
                    if (File.Exists(sbFilePath))
                    {
                        byte[] sbfFileData = File.ReadAllBytes(sbFilePath);
                        if (sbfFileData.Length > 0)
                        {
                            SampleDataLength = FlipUInt32((uint)sbfFileData.Length, bigEndian);
                            WriteAlignedDecoration(binWriter, positionAligned);
                            binWriter.Write(sbfFileData);
                        }
                    }

                    //--------------------------------------------------[Write Final offsets]--------------------------------------------------
                    uint fileSize = (uint)binWriter.BaseStream.Position;
                    binWriter.BaseStream.Seek(0xC, SeekOrigin.Begin);
                    binWriter.Write(fileSize);
                    binWriter.BaseStream.Seek(16, SeekOrigin.Current);
                    binWriter.Write(SFXStart);
                    binWriter.Write(SFXLength);
                    binWriter.Write(SampleInfoStart);
                    binWriter.Write(SampleInfoLength);
                    binWriter.Write(SpecialSampleInfoStart);
                    binWriter.Write(SpecialSampleInfoLength);
                    binWriter.Write(SampleDataStart);
                    binWriter.Write(SampleDataLength);
                }
            }
        }


    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
