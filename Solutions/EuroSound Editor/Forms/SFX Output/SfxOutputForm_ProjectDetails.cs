using ESUtils;
using System.Collections.Generic;
using System.IO;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void OutputProjectDetailsFile(string outputPath, string outputPlatform, bool isBigEndian)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(outputPath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                Dictionary<string, int> mapsData = GetMemSlotsTable();
                SortedDictionary<int, int> soundBanks = GetSoundBankDictionary(mapsData);

                //Data Offsets
                bw.Write(BytesFunctions.FlipInt32(projectSettings.MemoryMaps.Count, isBigEndian));
                bw.Write(0);
                bw.Write(BytesFunctions.FlipInt32(soundBanks.Count, isBigEndian));
                bw.Write(0);

                //Padding
                bw.BaseStream.Seek(16, SeekOrigin.Current);

                //Project flags
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                bw.Write(0);
                List<int> memoryMaps = projectSettings.platformData[outputPlatform].MemoryMapsSize;

                //Mem Slots
                long memSlotsStartPos = bw.BaseStream.Position;
                for (int i = memoryMaps.Count - 1; i >= 0; i--)
                {
                    bw.Write(BytesFunctions.FlipInt32(i, isBigEndian));
                    bw.Write(BytesFunctions.FlipInt32(memoryMaps[i], isBigEndian));
                    bw.Write(BytesFunctions.FlipInt32(1, isBigEndian));
                }

                //Print 
                long sbSlotStartPos = bw.BaseStream.Position;
                foreach (KeyValuePair<int, int> sbFile in soundBanks)
                {
                    bw.Write(BytesFunctions.FlipInt32(sbFile.Key, isBigEndian));
                    bw.Write(BytesFunctions.FlipInt32(sbFile.Value, isBigEndian));
                }

                //Write offsets
                bw.BaseStream.Seek(4, SeekOrigin.Begin);
                bw.Write(BytesFunctions.FlipInt32((int)memSlotsStartPos, isBigEndian));
                bw.BaseStream.Seek(4, SeekOrigin.Current);
                bw.Write(BytesFunctions.FlipInt32((int)sbSlotStartPos, isBigEndian));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private Dictionary<string, int> GetMemSlotsTable()
        {
            Dictionary<string, int> Data = new Dictionary<string, int>();
            for (int i = projectSettings.MemoryMaps.Count - 1; i >= 0; i--)
            {
                Data.Add(projectSettings.MemoryMaps[i], i);
            }

            return Data;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private SortedDictionary<int, int> GetSoundBankDictionary(Dictionary<string, int> mapsData)
        {
            SortedDictionary<int, int> sbData = new SortedDictionary<int, int>();

            string[] soundBanks = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks"), "*.txt", SearchOption.TopDirectoryOnly);
            foreach (string sbFile in soundBanks)
            {
                Objects.SoundBank sbfileData = TextFiles.ReadSoundbankFile(sbFile);
                sbData.Add(sbfileData.HashCode, mapsData[sbfileData.MemoryMap]);
            }

            return sbData;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
