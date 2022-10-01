using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static ProjectFile ReadProjectFile(string projectFilePath, bool readOnlyHeader = false)
        {
            ProjectFile projectData = new ProjectFile();
            HashSet<string> SoundBanks = new HashSet<string>();
            HashSet<string> DataBases = new HashSet<string>();
            HashSet<string> SFXs = new HashSet<string>();

            using (StreamReader sr = new StreamReader(File.Open(projectFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
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
                        ReadHeaderData(projectData.HeaderData, currentLine);
                    }

                    //Dependencies Block
                    if (currentLine.Equals("#SoundBankList", StringComparison.OrdinalIgnoreCase))
                    {
                        if (readOnlyHeader)
                        {
                            break;
                        }
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            SoundBanks.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Dependencies Block
                    if (currentLine.Equals("#DataBaseList", StringComparison.OrdinalIgnoreCase))
                    {
                        if (readOnlyHeader)
                        {
                            break;
                        }
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            DataBases.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Dependencies Block
                    if (currentLine.Equals("#SFXList", StringComparison.OrdinalIgnoreCase))
                    {
                        if (readOnlyHeader)
                        {
                            break;
                        }
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            SFXs.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }

                //Add lists to project file
                if (SoundBanks.Count > 0)
                {
                    projectData.SoundBanks = SoundBanks.ToArray();
                }
                if (DataBases.Count > 0)
                {
                    projectData.DataBases = DataBases.ToArray();
                }
                if (SFXs.Count > 0)
                {
                    projectData.SFXs = SFXs.ToArray();
                }
            }

            return projectData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteProjectFile(string projectFilePath, ProjectFile projectFile)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(projectFilePath))
            {
                projectFile.HeaderData.FirstCreated = currentData;
                projectFile.HeaderData.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            projectFile.HeaderData.LastModified = currentData;
            projectFile.HeaderData.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            using (StreamWriter outputFile = new StreamWriter(File.Open(projectFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                WriteHeader(outputFile, "EuroSound Project File", projectFile.HeaderData);
                outputFile.WriteLine("#SoundBankList");
                if (projectFile.SoundBanks != null)
                {
                    for (int i = 0; i < projectFile.SoundBanks.Length; i++)
                    {
                        outputFile.WriteLine(projectFile.SoundBanks[i]);
                    }
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#DataBaseList");
                if (projectFile.DataBases != null)
                {
                    for (int i = 0; i < projectFile.DataBases.Length; i++)
                    {
                        outputFile.WriteLine(projectFile.DataBases[i]);
                    }
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#SFXList");
                if (projectFile.SFXs != null)
                {
                    for (int i = 0; i < projectFile.SFXs.Length; i++)
                    {
                        outputFile.WriteLine(projectFile.SFXs[i]);
                    }
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
