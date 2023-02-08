using ExMarkers;
using NAudio.Wave;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            bool prefixHashCode = false;

            //Check Ini File
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                IniFile systemIni = new IniFile(systemIniFilePath);
                prefixHashCode = systemIni.Read("Prefix_HT_Sound", "PropertiesForm").Equals("1");
            }

            //File Hashcodes
            int maxSfxHashcodeDefined = 0;
            string[] musicFiles = TextFiles.ReadListBlock(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), "#MFXFiles");
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// Music HashCodes");
                for (int i = 0; i < musicFiles.Length; i++)
                {
                    MusicFile fileData = TextFiles.ReadMusicFile(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", musicFiles[i] + ".txt"));
                    if (prefixHashCode)
                    {
                        sw.WriteLine(WriteHashCode("HT_Sound_MFX_" + musicFiles[i], fileData.HashCode | 0x1B000000, "\t\t\t"));
                    }
                    else
                    {
                        sw.WriteLine(WriteHashCode("MFX_" + musicFiles[i], fileData.HashCode | 0x1B000000, "\t\t\t"));
                    }
                    maxSfxHashcodeDefined++;
                }
                sw.WriteLine(WriteNumber("MFX_MaximumDefined", maxSfxHashcodeDefined));
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
                            if (prefixHashCode)
                            {
                                sw.WriteLine("#define HT_Sound_JMP_{0} 0x{1:X8}", jumpHashcodes[j], ((0x1B0 & 0xFFF) << 20) | (((short)j & 0xFF) << 12) | ((fileData.HashCode & 0xFFF) << 0));
                            }
                            else
                            {
                                sw.WriteLine("#define JMP_{0} 0x{1:X8}", jumpHashcodes[j], ((0x1B0 & 0xFFF) << 20) | (((short)j & 0xFF) << 12) | ((fileData.HashCode & 0xFFF) << 0));
                            }
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
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();

            //Get music data and sort it ascending by hashcode
            SortedDictionary<int, string> itemsData = new SortedDictionary<int, string>();
            string[] musicFiles = TextFiles.ReadListBlock(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", "MFXFiles.txt"), "#MFXFiles");
            for (int i = 0; i < musicFiles.Length; i++)
            {
                string waveFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", musicFiles[i] + ".wav");
                string markerFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", musicFiles[i] + ".mrk");
                if (File.Exists(waveFilePath))
                {
                    using (WaveFileReader wReader = new WaveFileReader(waveFilePath))
                    {
                        MusicFile musicFileData = TextFiles.ReadMusicFile(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData", musicFiles[i] + ".txt"));
                        List<MarkerInfo> markerData = streamMarkersFunctions.LoadTextMarkerFile(markerFilePath, null, null, true);
                        float duration = (float)decimal.Divide(wReader.Length, wReader.WaveFormat.AverageBytesPerSecond);
                        string strDuration = duration.ToString("G7", GlobalPrefs.NumericProvider);
                        if (duration % 1 == 0)
                        {
                            strDuration = duration.ToString("F1", GlobalPrefs.NumericProvider);
                        }

                        //Add item to dictionary if not exists
                        if (!itemsData.ContainsKey(musicFileData.HashCode))
                        {
                            itemsData.Add(musicFileData.HashCode, string.Format("\t{{0x{0:X8},{1}f,{2}, {3} }},", musicFileData.HashCode | 0x1B000000, strDuration, MusicLoops(markerData).ToString().ToUpper(), musicFileData.UserValue));
                        }
                    }
                }
            }

            //Print data in the header file
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("// Music Data table from EuroSound 1");
                sw.WriteLine("// {0}", DateTime.Now.ToLongDateString());
                sw.WriteLine(string.Empty);
                sw.WriteLine("typedef struct{");
                sw.WriteLine("\tu32      MusicHashCode;");
                sw.WriteLine("\tfloat    DurationInSeconds;");
                sw.WriteLine("\tbool     Looping;");
                sw.WriteLine("\ts32      UserValue");
                sw.WriteLine("} MusicDetails;");
                sw.WriteLine(string.Empty);
                sw.WriteLine("MusicDetails MusicData[]={");
                uint index = 0;
                foreach (KeyValuePair<int, string> item in itemsData)
                {
                    while (index != item.Key)
                    {
                        sw.WriteLine("\t{0,0,0,0},");
                        index++;
                    }
                    sw.WriteLine(item.Value);
                    index++;
                }
                sw.WriteLine("};");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private bool MusicLoops(List<MarkerInfo> markerFileData)
        {
            foreach (MarkerInfo markerData in markerFileData)
            {
                if (markerData.Type == 6)
                {
                    return true;
                }
            }

            return false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] CreateAndValidateMfxDefines(ProjProperties projectSettings)
        {
            // Initialize a list to store missing MFX defines
            string[] missingInTempFile = null;

            // Set the file path for the temp MFX defines file
            string tempFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Temp_MFX_Defines.h");

            // Check if System directory exists
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "System")))
            {
                // Create MFX defines in temp file
                CreateMfxDefines(tempFilePath);

                // Read the file data into memory for faster search
                string[] tempFileData = GetHashtableLabels(tempFilePath);

                // Check if the project's hash code directory exists
                if (!string.IsNullOrEmpty(projectSettings.HashCodeFileDirectory) && Directory.Exists(projectSettings.HashCodeFileDirectory))
                {
                    // Set file path for MFX defines file
                    string mfxDefinesFilePath = Path.Combine(projectSettings.HashCodeFileDirectory, "MFX_Defines.h");
                    if (File.Exists(mfxDefinesFilePath))
                    {
                        // Read the MFX defines data into memory for faster search
                        string[] mfxDefinesData = GetHashtableLabels(mfxDefinesFilePath);

                        //Get missing HashCodes
                        missingInTempFile = mfxDefinesData.Except(tempFileData).ToArray();

                    }
                    else if (File.Exists(tempFilePath))
                    {
                        // If the MFX defines file does not exist, copy the temp file to create it
                        File.Copy(tempFilePath, mfxDefinesFilePath);
                    }
                }
            }

            // Check if the project's hash code directory exists
            if (!string.IsNullOrEmpty(projectSettings.HashCodeFileDirectory) && Directory.Exists(projectSettings.HashCodeFileDirectory))
            {
                CreateMfxValidList(Path.Combine(projectSettings.HashCodeFileDirectory, "MFX_ValidList.h"));
                CreateMfxData(Path.Combine(projectSettings.HashCodeFileDirectory, "MFX_Data.h"));
                BuildSoundHhFile(Path.Combine(projectSettings.EuroLandHashCodeServerPath, "Sound.h"), projectSettings);
            }

            return missingInTempFile;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
