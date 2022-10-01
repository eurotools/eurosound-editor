using NAudio.Wave;
using sb_editor.Objects;
using System;
using System.IO;

namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CreateMfxDefines(string filePath)
        {
            //File Hashcodes
            int maxSfxHashcodeDefined = 0;
            string[] musicFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"), "*.txt", SearchOption.TopDirectoryOnly);
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// Music HashCodes");
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(musicFiles[i]);
                    if (!fileName.Equals("MFXFiles", StringComparison.OrdinalIgnoreCase))
                    {
                        MusicFile fileData = TextFiles.ReadMusicFile(musicFiles[i]);
                        sw.WriteLine(WriteHashCode("MFX_" + fileName, string.Format("0x{0:X8}", fileData.HashCode | 0x1BE00000)));
                        maxSfxHashcodeDefined++;
                    }
                }
                sw.WriteLine("#define MFX_MaximumDefined {0}", maxSfxHashcodeDefined);
            }

            //Add Jump Markers
            for (int i = 0; i < musicFiles.Length; i++)
            {
                //Get Music File Data
                string fileName = Path.GetFileNameWithoutExtension(musicFiles[i]);
                if (!fileName.Equals("MFXFiles", StringComparison.OrdinalIgnoreCase))
                {
                    //Get Jump File Data
                    string jumpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", fileName + ".jmp");
                    if (File.Exists(jumpFilePath))
                    {
                        MusicFile fileData = TextFiles.ReadMusicFile(musicFiles[i]);
                        using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.Read)))
                        {
                            sw.WriteLine(string.Empty);
                            sw.WriteLine("// Music Jump Codes For Level {0}", "MFX_" + fileName);
                            string[] jumpHashcodes = TextFiles.ReadJumpHashCodes(jumpFilePath);
                            for (int j = 0; j < jumpHashcodes.Length; j++)
                            {
                                sw.WriteLine("#define JMP_{0} 0x{1:X8}", jumpHashcodes[j], ((0x1BE & 0xFFF) << 20) | (((short)j & 0xFF) << 8) | ((fileData.HashCode & 0xFF) << 0));
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CreateMfxValidList(string filePath)
        {
            string[] musicFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"), "*.txt", SearchOption.TopDirectoryOnly);
            Array.Sort(musicFiles);
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("s32 MFX_ValidList[]={");
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    //Get Music File Data
                    string fileName = Path.GetFileNameWithoutExtension(musicFiles[i]);
                    if (!fileName.Equals("MFXFiles", StringComparison.OrdinalIgnoreCase))
                    {
                        //Get Jump File Data
                        string jumpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", fileName + ".jmp");
                        if (File.Exists(jumpFilePath))
                        {
                            MusicFile fileData = TextFiles.ReadMusicFile(musicFiles[i]);
                            string[] jumpHashcodes = TextFiles.ReadJumpHashCodes(jumpFilePath);
                            for (int j = 0; j < jumpHashcodes.Length; j++)
                            {
                                sw.WriteLine("0x{0:X8},// JMP_{1}", ((0x1BE & 0xFFF) << 20) | (((short)j & 0xFF) << 8) | ((fileData.HashCode & 0xFF) << 0), jumpHashcodes[j]);
                            }
                        }
                    }
                }
                sw.WriteLine("-1};");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CreateMfxData(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// Music Data table from EuroSound 1");
                sw.WriteLine("// {0}", DateTime.Now.ToLongDateString());
                sw.WriteLine(string.Empty);
                sw.WriteLine("typedef struct{");
                sw.WriteLine("\tu32      MusicHashCode;");
                sw.WriteLine("\tfloat    DurationInSeconds;");
                sw.WriteLine("\tbool     Looping;");
                sw.WriteLine("} MusicDetails;");
                sw.WriteLine(string.Empty);
                sw.WriteLine("MusicDetails MusicData[]={");
                string[] musicFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"), "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(musicFiles[i]);
                    if (!fileName.Equals("MFXFiles", StringComparison.OrdinalIgnoreCase))
                    {
                        string waveFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", fileName + ".wav");
                        using (WaveFileReader wReader = new WaveFileReader(waveFilePath))
                        {
                            MusicFile fileData = TextFiles.ReadMusicFile(musicFiles[i]);
                            float duration = (float)decimal.Divide(wReader.Length, wReader.WaveFormat.AverageBytesPerSecond);
                            string strDuration = duration.ToString("G7", GlobalPrefs.NumericProvider);
                            if (duration % 1 == 0)
                            {
                                strDuration = duration.ToString("F1", GlobalPrefs.NumericProvider);
                            }
                            sw.WriteLine("\t{{0x{0:X8},{1}f,{2}}},", fileData.HashCode | 0x1BE00000, strDuration, "FALSE");
                        }
                    }
                }
                sw.WriteLine("};");
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
