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
        public static DataBase ReadDataBaseFile(string filePath, bool readDependencies = true)
        {
            DataBase dataBase = new DataBase();
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
                        ReadHeaderData(dataBase, currentLine);
                    }

                    //Dependencies Block
                    if (currentLine.Equals("#DEPENDENCIES", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!readDependencies)
                        {
                            break;
                        }
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            dependencies.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Read parameters block
                    if (currentLine.Equals("#SFXParameters", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "MAXVOICES":
                                    dataBase.MaxVoices = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "ACTION1":
                                    dataBase.Action1 = Convert.ToByte(lineData[1].Trim());
                                    break;
                                case "PRIORITY":
                                    dataBase.Priority = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "USEDISTCHECK":
                                    dataBase.UseDistCheck = lineData[1].Trim().Equals("True");
                                    break;
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            //Add dependencies to the object
            dataBase.SFXs = dependencies.ToArray();

            return dataBase;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteDataBaseFile(string filePath, DataBase dataBaseFile)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(filePath))
            {
                dataBaseFile.FirstCreated = currentData;
                dataBaseFile.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            dataBaseFile.LastModified = currentData;
            dataBaseFile.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            string tmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt");
            using (StreamWriter outputFile = new StreamWriter(File.Open(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                WriteHeader(outputFile, string.Empty, dataBaseFile);
                outputFile.WriteLine("#DEPENDENCIES");
                if (dataBaseFile.SFXs != null)
                {
                    for (int i = 0; i < dataBaseFile.SFXs.Length; i++)
                    {
                        outputFile.WriteLine(dataBaseFile.SFXs[i]);
                    }
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#SFXParameters");
                outputFile.WriteLine("Action1 {0}", dataBaseFile.Action1);
                outputFile.WriteLine("MaxVoices {0}", dataBaseFile.MaxVoices);
                outputFile.WriteLine("Priority {0}", dataBaseFile.Priority);
                outputFile.WriteLine("UseDistCheck {0}", dataBaseFile.UseDistCheck);
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
            }

            //Copy file to the final folder
            File.Delete(filePath);
            File.Copy(tmpFilePath, filePath);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
