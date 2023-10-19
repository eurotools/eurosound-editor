//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Text Files - Project Settings
//-------------------------------------------------------------------------------------------------------------------------------
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
        public static ProjProperties ReadPropertiesFile(string projectFilePath)
        {
            ProjProperties projectData = new ProjProperties();

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
                        ReadHeaderData(projectData, currentLine);
                    }

                    //Available formats section
                    if (currentLine.Equals("#AvailableFormats", StringComparison.OrdinalIgnoreCase))
                    {
                        uint formatsCount = Convert.ToUInt32(sr.ReadLine().Trim());
                        string[,] formatData = new string[formatsCount, 3];
                        if (formatsCount > 0)
                        {
                            //Columns
                            for (int i = 0; i < 3; i++)
                            {
                                //Row info
                                for (int j = 0; j < formatsCount; j++)
                                {
                                    currentLine = sr.ReadLine().Trim();
                                    if (currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                                    {
                                        break;
                                    }
                                    formatData[j, i] = currentLine;
                                }
                            }
                        }

                        //Add data to dictionary
                        for (int i = 0; i < formatData.GetLength(0); i++)
                        {
                            PlatformData platformInfo = new PlatformData
                            {
                                OutputFolder = formatData[i, 1],
                                AutoReSample = formatData[i, 2].Equals("On")
                            };
                            projectData.platformData.Add(formatData[i, 0], platformInfo);
                        }
                    }

                    //Read Available Sample Rates
                    if (currentLine.Equals("#AvailableReSampleRates", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            projectData.ResampleRates.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //ReSample Rates for Format
                    if (currentLine.Contains("#ReSampleRates"))
                    {
                        int index = Convert.ToInt32(currentLine.Substring(currentLine.Length - 1));
                        PlatformData formatObj = projectData.platformData.ElementAt(index).Value;

                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            formatObj.ReSampleRates.Add(Convert.ToInt32(currentLine));
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Read Available Memory Maps
                    if (currentLine.Equals("#AvailableMemoryMaps", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            projectData.MemoryMaps.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Memory Maps for Format
                    if (currentLine.Contains("#MemoryMaps"))
                    {
                        int index = Convert.ToInt32(currentLine.Substring(currentLine.Length - 1));
                        PlatformData formatObj = projectData.platformData.ElementAt(index).Value;

                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            formatObj.MemoryMapsSize.Add(Convert.ToInt32(currentLine));
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Misc properties block
                    if (currentLine.Equals("#MiscProperites", StringComparison.OrdinalIgnoreCase))
                    {
                        //Read line
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            //Split Line
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "DEFAULTRATE":
                                    projectData.DefaultRate = Convert.ToInt32(lineData[1]);
                                    break;
                                case "DEFAULTMAP":
                                    projectData.DefaultMemMap = Convert.ToInt32(lineData[1]);
                                    break;
                                case "SAMPLEFILEFOLDER":
                                    projectData.SampleFilesFolder = GetKeyWordValue("SAMPLEFILEFOLDER", currentLine);
                                    break;
                                case "HASHCODEFILEFOLDER":
                                    projectData.HashCodeFileDirectory = GetKeyWordValue("HASHCODEFILEFOLDER", currentLine);
                                    break;
                                case "ENGINEXFOLDER":
                                    projectData.EngineXProjectPath = GetKeyWordValue("ENGINEXFOLDER", currentLine);
                                    break;
                                case "EUROLANDHASHCODESERVERPATH":
                                    projectData.EuroLandHashCodeServerPath = GetKeyWordValue("EUROLANDHASHCODESERVERPATH", currentLine);
                                    break;
                            }

                            //Continue Reading
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return projectData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WritePropertiesFile(string projectFilePath, ProjProperties projectFile)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(projectFilePath))
            {
                projectFile.FirstCreated = currentData;
                projectFile.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            projectFile.LastModified = currentData;
            projectFile.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            if (Directory.Exists(Path.GetDirectoryName(projectFilePath)))
            {
                using (StreamWriter outputFile = new StreamWriter(File.Open(projectFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
                {
                    WriteHeader(outputFile, "Properties", projectFile);
                    outputFile.WriteLine("#AvailableFormats");
                    outputFile.WriteLine(" {0} ", projectFile.platformData.Count);
                    for (int i = 0; i < 3; i++)
                    {
                        foreach (KeyValuePair<string, PlatformData> platformData in projectFile.platformData)
                        {
                            switch (i)
                            {
                                case 0:
                                    outputFile.WriteLine(platformData.Key);
                                    break;
                                case 1:
                                    outputFile.WriteLine(platformData.Value.OutputFolder);
                                    break;
                                case 2:
                                    outputFile.WriteLine(platformData.Value.AutoReSample ? "On" : "Off");
                                    break;

                            }
                        }
                    }
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                    outputFile.WriteLine("#AvailableReSampleRates");
                    for (int i = 0; i < projectFile.ResampleRates.Count; i++)
                    {
                        outputFile.WriteLine(projectFile.ResampleRates[i]);
                    }
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                    int platformIndex = 0;
                    foreach (KeyValuePair<string, PlatformData> formatData in projectFile.platformData)
                    {
                        outputFile.WriteLine("// ReSample Rates for Format {0}", formatData.Key);
                        outputFile.WriteLine("#ReSampleRates{0}", platformIndex++);
                        for (int i = 0; i < formatData.Value.ReSampleRates.Count; i++)
                        {
                            outputFile.WriteLine(formatData.Value.ReSampleRates[i]);
                        }
                        outputFile.WriteLine("#END");
                        outputFile.WriteLine(string.Empty);
                    }
                    outputFile.WriteLine("#AvailableMemoryMaps");
                    for (int i = 0; i < projectFile.MemoryMaps.Count; i++)
                    {
                        outputFile.WriteLine(projectFile.MemoryMaps[i]);
                    }
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                    platformIndex = 0;
                    foreach (KeyValuePair<string, PlatformData> formatData in projectFile.platformData)
                    {
                        outputFile.WriteLine("// Memory Maps for Format {0}", formatData.Key);
                        outputFile.WriteLine("#MemoryMaps{0}", platformIndex++);
                        for (int i = 0; i < formatData.Value.MemoryMapsSize.Count; i++)
                        {
                            outputFile.WriteLine(formatData.Value.MemoryMapsSize[i]);
                        }
                        outputFile.WriteLine("#END");
                        outputFile.WriteLine(string.Empty);
                    }
                    outputFile.WriteLine("#MiscProperites");
                    outputFile.WriteLine("DefaultRate  {0}", projectFile.DefaultRate);
                    outputFile.WriteLine("DefaultMap  {0}", projectFile.DefaultMemMap);
                    outputFile.WriteLine("SampleFileFolder {0}", projectFile.SampleFilesFolder);
                    outputFile.WriteLine("HashCodeFileFolder {0}", projectFile.HashCodeFileDirectory);
                    outputFile.WriteLine("EngineXFolder {0}", projectFile.EngineXProjectPath);
                    outputFile.WriteLine("EuroLandHashCodeServerPath {0}", projectFile.EuroLandHashCodeServerPath);
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
