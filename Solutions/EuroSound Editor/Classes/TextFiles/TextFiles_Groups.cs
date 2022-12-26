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
        public static GroupFile ReadGroupsFile(string filePath)
        {
            GroupFile dataBase = new GroupFile();
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
            if (dependencies.Count > 0)
            {
                dataBase.Dependencies = dependencies.ToArray();
            }

            return dataBase;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteGroupsFile(GroupFile fileData, string filePath)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(filePath))
            {
                fileData.FirstCreated = currentData;
                fileData.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            fileData.LastModified = currentData;
            fileData.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update Text File
            using (StreamWriter outputFile = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                WriteHeader(outputFile, string.Empty, fileData);
                outputFile.WriteLine("#DEPENDENCIES");
                if (fileData.Dependencies != null)
                {
                    for (int i = 0; i < fileData.Dependencies.Length; i++)
                    {
                        outputFile.WriteLine(fileData.Dependencies[i]);
                    }
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#SFXParameters");
                outputFile.WriteLine("MaxVoices {0}", fileData.MaxVoices);
                outputFile.WriteLine("Action1 {0}", fileData.Action1);
                outputFile.WriteLine("Priority {0}", fileData.Priority);
                outputFile.WriteLine("UseDistCheck {0}", fileData.UseDistCheck);
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
