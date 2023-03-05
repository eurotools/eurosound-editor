using PCAudioDLL.MusX_Objects;
using System.IO;
using System.Text;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SfxFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public enum FileType
        {
            MusicFile = 1,
            StreamFile = 2,
            SoundbankFile = 3,
            SoundDetailsFile = 4,
            ProjectDetails = 5,
            MusicDetails = 6,
            SBI = 7,
            Unknown = 8
        }

        //-------------------------------------------------------------------------------------------
        //  GET TYPE OF FILE
        //-------------------------------------------------------------------------------------------
        public int GetFileHashCode(string filePath)
        {
            int hashCode = -1;
            using (BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                string Magic = Encoding.ASCII.GetString(br.ReadBytes(4));
                if (Magic.Equals("MUSX"))
                {
                    hashCode = br.ReadInt32();
                }
            }

            return hashCode;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public int GetFileVersion(string filePath)
        {
            int fileVersion = -1;
            using (BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                string Magic = Encoding.ASCII.GetString(br.ReadBytes(4));
                if (Magic.Equals("MUSX"))
                {
                    br.BaseStream.Seek(4, SeekOrigin.Current);
                    fileVersion = br.ReadInt32();
                }
            }

            return fileVersion;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public SfxCommonHeader ReadCommonHeader(string filePath, string platform)
        {
            SfxCommonHeader headerData = new SfxCommonHeader
            {
                Platform = platform
            };

            using (BinaryReader BReader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //Magic value MUSX
                string Magic = Encoding.ASCII.GetString(BReader.ReadBytes(4));
                if (Magic.Equals("MUSX"))
                {
                    //Hashcode for the current soundbank 
                    headerData.FileHashCode = BReader.ReadUInt32();
                    //Current version of the file
                    headerData.FileVersion = BReader.ReadInt32();
                    if (headerData.FileVersion < 7 || headerData.FileVersion == 201)
                    {
                        //Size of the whole file, in bytes
                        headerData.FileSize = BReader.ReadUInt32();

                        //Fields in the new versions
                        if (headerData.FileVersion > 3 && headerData.FileVersion < 10)
                        {
                            //Platform PS2_ PC__ GC__ XB__
                            headerData.Platform = Encoding.ASCII.GetString(BReader.ReadBytes(4));
                            //Seconds from 1/1/2000, 1:00:00 (946684800)
                            headerData.Timespan = BReader.ReadUInt32();
                            //Seems that when the data is encoded in adpcm is set to 1.
                            headerData.UsesAdpcm = BReader.ReadUInt32();
                            //Padding??
                            BReader.ReadUInt32();
                        }

                        //Store the last SFX
                        headerData.EndOffset = BReader.BaseStream.Position;

                        //Big endian
                        if (headerData.Platform.Contains("GC") || headerData.Platform.Contains("GameCube"))
                        {
                            headerData.IsBigEndian = true;
                        }
                    }
                    else
                    {
                        throw new InvalidDataException(string.Format("This file version ({0}) is unsupported by this version of the EuroSound Explorer", headerData.FileVersion));
                    }
                }

                //Close
                BReader.Close();
            }

            return headerData;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
