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
                        ReadHeaderData(dataBase.HeaderData, currentLine);
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
                dataBaseFile.HeaderData.FirstCreated = currentData;
                dataBaseFile.HeaderData.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            dataBaseFile.HeaderData.LastModified = currentData;
            dataBaseFile.HeaderData.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            string tmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt");
            using (StreamWriter outputFile = new StreamWriter(File.Open(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                WriteHeader(outputFile, string.Empty, dataBaseFile.HeaderData);
                outputFile.WriteLine("#DEPENDENCIES");
                if (dataBaseFile.SFXs != null)
                {
                    for (int i = 0; i < dataBaseFile.SFXs.Length; i++)
                    {
                        outputFile.WriteLine(dataBaseFile.SFXs[i]);
                    }
                }
                outputFile.WriteLine("#END");
            }

            //Copy file to the final folder
            File.Delete(filePath);
            File.Copy(tmpFilePath, filePath);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
