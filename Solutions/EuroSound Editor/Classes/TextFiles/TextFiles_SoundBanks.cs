using sb_editor.Objects;
using System;
using System.Collections.Generic;
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
        public static SoundBank ReadSoundbankFile(string filePath)
        {
            SoundBank soundBank = new SoundBank();
            List<string> dependencies = new List<string>();

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

                    //Header info
                    if (currentLine.StartsWith("##"))
                    {
                        ReadHeaderData(soundBank, currentLine);
                    }

                    //Dependencies Block
                    if (currentLine.Equals("#DEPENDENCIES", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            dependencies.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //HashCodes Block
                    if (currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string hashcodeNumber = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            soundBank.HashCode = Convert.ToInt32(hashcodeNumber);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Check for max bank sizes
                    if (currentLine.Equals("#MaxBankSizes", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "PLAYSTATIONSIZE":
                                    soundBank.PlayStationSize = Convert.ToUInt32(lineData[1]);
                                    break;
                                case "PCSIZE":
                                    soundBank.PCSize = Convert.ToUInt32(lineData[1]);
                                    break;
                                case "XBOXSIZE":
                                    soundBank.XboxSize = Convert.ToUInt32(lineData[1]);
                                    break;
                                case "GAMECUBESIZE":
                                    soundBank.GameCubeSize = Convert.ToUInt32(lineData[1]);
                                    break;
                            }

                            //Continue reading
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            //Add dependencies to the object
            if (dependencies.Count > 0)
            {
                soundBank.DataBases = dependencies.ToArray();
            }

            return soundBank;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static void WriteSoundBankFile(string filePath, SoundBank soundBankFile, bool includeMaxSizes = false)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(filePath))
            {
                soundBankFile.FirstCreated = currentData;
                soundBankFile.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            soundBankFile.LastModified = currentData;
            soundBankFile.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            string tmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt");
            using (StreamWriter outputFile = new StreamWriter(File.Open(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                WriteHeader(outputFile, string.Empty, soundBankFile);
                outputFile.WriteLine("#DEPENDENCIES");
                for (int i = 0; i < soundBankFile.DataBases.Length; i++)
                {
                    outputFile.WriteLine(soundBankFile.DataBases[i]);
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#HASHCODE");
                outputFile.WriteLine(string.Format("HashCodeNumber {0}", soundBankFile.HashCode));
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                if (includeMaxSizes)
                {
                    outputFile.WriteLine("#MaxBankSizes");
                    outputFile.WriteLine(string.Format("PlayStationSize {0}", soundBankFile.PlayStationSize));
                    outputFile.WriteLine(string.Format("PCSize {0}", soundBankFile.PCSize));
                    outputFile.WriteLine(string.Format("XBoxSize {0}", soundBankFile.XboxSize));
                    outputFile.WriteLine(string.Format("GameCubeSize {0}", soundBankFile.GameCubeSize));
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                }
            }

            //Copy file to the final folder
            File.Delete(filePath);
            File.Copy(tmpFilePath, filePath);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
