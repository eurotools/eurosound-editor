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
            string[] musicFiles = TextFiles.ReadListBlock(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), "#MFXFiles");
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// Music HashCodes");
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    MusicFile fileData = TextFiles.ReadMusicFile(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", musicFiles[i] + ".txt"));
                    sw.WriteLine(WriteHashCode("MFX_" + musicFiles[i], fileData.HashCode | 0x1BE00000));
                    maxSfxHashcodeDefined++;
                }
                sw.WriteLine("#define MFX_MaximumDefined {0}", maxSfxHashcodeDefined);
            }

            //Add Jump Markers
            for (int i = 0; i < musicFiles.Length; i++)
            {
                //Get Jump File Data
                string jumpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", musicFiles[i] + ".jmp");
                if (File.Exists(jumpFilePath))
                {
                    MusicFile fileData = TextFiles.ReadMusicFile(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", musicFiles[i] + ".txt"));
                    using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.Read)))
                    {
                        sw.WriteLine(string.Empty);
                        sw.WriteLine("// Music Jump Codes For Level {0}", "MFX_" + musicFiles[i]);
                        string[] jumpHashcodes = TextFiles.ReadJumpHashCodes(jumpFilePath);
                        for (int j = 0; j < jumpHashcodes.Length; j++)
                        {
                            sw.WriteLine("#define JMP_{0} 0x{1:X8}", jumpHashcodes[j], ((0x1BE & 0xFFF) << 20) | (((short)j & 0xFF) << 8) | ((fileData.HashCode & 0xFF) << 0));
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CreateMfxValidList(string filePath)
        {
            string[] musicFiles = TextFiles.ReadListBlock(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), "#MFXFiles");
            Array.Sort(musicFiles);
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("s32 MFX_ValidList[]={");
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    //Get Jump File Data
                    string jumpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork", musicFiles[i] + ".jmp");
                    if (File.Exists(jumpFilePath))
                    {
                        MusicFile fileData = TextFiles.ReadMusicFile(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", musicFiles[i] + ".txt"));
                        string[] jumpHashcodes = TextFiles.ReadJumpHashCodes(jumpFilePath);
                        for (int j = 0; j < jumpHashcodes.Length; j++)
                        {
                            sw.WriteLine("0x{0:X8},// JMP_{1}", ((0x1BE & 0xFFF) << 20) | (((short)j & 0xFF) << 8) | ((fileData.HashCode & 0xFF) << 0), jumpHashcodes[j]);
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
                string[] musicFiles = TextFiles.ReadListBlock(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), "#MFXFiles");
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    string waveFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", musicFiles[i] + ".wav");
                    if (File.Exists(waveFilePath))
                    {
                        using (WaveFileReader wReader = new WaveFileReader(waveFilePath))
                        {
                            MusicFile fileData = TextFiles.ReadMusicFile(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", musicFiles[i] + ".txt"));
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
