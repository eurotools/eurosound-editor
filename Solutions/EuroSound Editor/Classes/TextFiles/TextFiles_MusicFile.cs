using sb_editor.Objects;
using System;
using System.IO;
using System.Text;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static MusicFile ReadMusicFile(string filePath)
        {
            MusicFile musicFile = new MusicFile();
            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine().Trim();
                    //Skip empty or commented lines
                    if (string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//"))
                    {
                        continue;
                    }

                    //HashCode Block
                    if (currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string hashcodeNumber = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            musicFile.HashCode = Convert.ToInt32(hashcodeNumber);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Volume Block
                    if (currentLine.Equals("#MusicData", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "VOLUME":
                                    musicFile.Volume = Convert.ToByte(lineData[1].Trim());
                                    break;
                                case "USERVALUE":
                                    musicFile.UserValue = Convert.ToUInt32(uint.Parse(lineData[1].Trim()));
                                    break;
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Timestamps
                    if (currentLine.Equals("#TIMESTAMPS", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "MIDIFILELASTOUTPUT":
                                    musicFile.MidiFileLastOutput = GetKeyWordValue("MidiFileLastOutput", currentLine);
                                    break;
                                case "WAVFILELASTOUTPUT":
                                    musicFile.WavFileLastOutput = GetKeyWordValue("WAVFILELASTOUTPUT", currentLine);
                                    break;
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return musicFile;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteMusicFile(MusicFile musicFile, string filePath)
        {
            string tmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt");
            using (StreamWriter outputFile = new StreamWriter(File.Open(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                if (musicFile.HashCode >= 0)
                {
                    outputFile.WriteLine("#HASHCODE");
                    outputFile.WriteLine("HashCodeNumber {0}", musicFile.HashCode);
                    outputFile.WriteLine("#END");
                }
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#MusicData");
                outputFile.WriteLine("Volume {0}", musicFile.Volume);
                if (musicFile.UserValue > 0)
                {
                    outputFile.WriteLine("UserValue {0}", musicFile.UserValue);
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                if (!string.IsNullOrEmpty(musicFile.MidiFileLastOutput) && !string.IsNullOrEmpty(musicFile.WavFileLastOutput))
                {
                    outputFile.WriteLine("#TIMESTAMPS");
                    if (!string.IsNullOrEmpty(musicFile.MidiFileLastOutput))
                    {
                        outputFile.WriteLine("MidiFileLastOutput {0}", musicFile.MidiFileLastOutput);
                    }
                    if (!string.IsNullOrEmpty(musicFile.WavFileLastOutput))
                    {
                        outputFile.WriteLine("WavFileLastOutput {0}", musicFile.WavFileLastOutput);
                    }
                    outputFile.WriteLine("#END");
                }
            }

            //Copy file to the final folder
            File.Delete(filePath);
            File.Copy(tmpFilePath, filePath);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
