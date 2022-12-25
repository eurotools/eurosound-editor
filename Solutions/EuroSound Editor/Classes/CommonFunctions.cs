using sb_editor.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class CommonFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void AskForUserName()
        {
            string userName = string.Empty;
            while (string.IsNullOrEmpty(userName))
            {
                Frm_InputBox inputForm = new Frm_InputBox
                {
                    Text = "Enter UserName."
                };
                inputForm.lblText.Text = "Please Enter Your UserName";
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    userName = inputForm.txtInputData.Text.Trim();
                }
            }
            //Updata global var & Ini
            GlobalPrefs.EuroSoundUser = userName;
            IniFile toolIniFile = new IniFile(Path.Combine(Application.StartupPath, "EuroSound.ini"));
            toolIniFile.Write("UserName", userName, "Form1_Misc");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void RunConsoleProcess(string toolFileName, string toolArguments, bool ViewOutputDos)
        {
            Process processToRun = new Process();
            processToRun.StartInfo.UseShellExecute = true;
            processToRun.StartInfo.FileName = toolFileName;
            processToRun.StartInfo.Arguments = toolArguments;
            processToRun.StartInfo.WorkingDirectory = Path.GetDirectoryName(processToRun.StartInfo.FileName);
            if (ViewOutputDos)
            {
                processToRun.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                processToRun.StartInfo.CreateNoWindow = false;
            }
            else
            {
                processToRun.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processToRun.StartInfo.CreateNoWindow = true;
            }
            processToRun.Start();
            processToRun.WaitForExit();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void ReSampleWithSox(string inputFile, string outputFile, int currentFrequency, int destinationFrequency, string effect, bool viewOutputDos)
        {
            if (destinationFrequency >= currentFrequency)
            {
                RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "Sox.exe"), string.Format("\"{0}\" \"{1}\"", inputFile, outputFile), viewOutputDos);
            }
            else
            {
                RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "Sox.exe"), string.Format("\"{0}\" -r {1} \"{2}\" {3}", inputFile, destinationFrequency, outputFile, effect), viewOutputDos);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetEnginexFolder(string platform)
        {
            switch (platform.ToLower())
            {
                case "pc":
                    return "_bin_PC";
                case "playstation2":
                    return "_bin_PS2";
                case "gamecube":
                    return "_bin_GC";
                case "xbox":
                case "x box":
                    return "_bin_XB";
                default:
                    return string.Empty;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetLanguageFolder(string language)
        {
            string langFolderName = char.ToUpper(language[0]) + language.Substring(1, 2).ToLower();
            return string.Format("_{0}", langFolderName);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetOutputPlatforms()
        {
            //Get Output Platforms
            UserControl_MainForm_Output mainForm = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output;
            string[] platforms = new string[] { mainForm.cboOutputFormat.SelectedItem.ToString() };
            if (mainForm.rdoAllForAll.Checked)
            {
                platforms = GlobalPrefs.CurrentProject.platformData.Keys.ToArray();
            }
            return platforms;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetOutputLanguages()
        {
            UserControl_MainForm_Output mainForm = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output;
            string[] languages = new string[] { "English" };
            if (mainForm.chkOutputAllLanguages.Checked && mainForm.cboOutputLanguage.Items.Count > 0)
            {
                languages = mainForm.cboOutputLanguage.Items.Cast<string>().ToArray();
            }
            else if (mainForm.cboOutputLanguage.SelectedItem != null)
            {
                languages = new string[] { mainForm.cboOutputLanguage.SelectedItem.ToString() };
            }

            return languages;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static int GetSfxName(int language, int fileHashCode)
        {
            int languageIndex = Math.Max(language, 0);
            return ((languageIndex & 0xF) << 16) | ((fileHashCode & 0xFFFF) << 0);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static byte[] RemoveFileHeader(string inputFile, int fileHeaderSize)
        {
            byte[] fileData = File.ReadAllBytes(inputFile);
            byte[] fileDataFinal = new byte[fileData.Length - fileHeaderSize];
            Array.Copy(fileData, fileHeaderSize, fileDataFinal, 0, fileDataFinal.Length);

            return fileDataFinal;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSampleDate(string filePath)
        {
            //Get Last Write Time
            DateTime fileInfo = File.GetLastWriteTimeUtc(filePath).Add(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now));

            //Apply EuroSound Format.
            string year = fileInfo.Year.ToString();
            string month = fileInfo.Month.ToString(), day = fileInfo.Day.ToString();
            if (fileInfo.Month < 10)
            {
                month = "00" + fileInfo.Month.ToString();
            }
            if (fileInfo.Day < 10)
            {
                day = "00" + fileInfo.Day.ToString();
            }

            //Return as a string
            return string.Format("{0}/{1}/{2} {3:#0}:{4:00}:{5:00}", year, day, month, fileInfo.Hour, fileInfo.Minute, fileInfo.Second);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSampleSize(string sampleFilePath)
        {
            string fileLength = new FileInfo(sampleFilePath).Length.ToString();
            return fileLength.PadLeft(fileLength.Length + Math.Max(11 - fileLength.Length, 0));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static string GetSampleFromSpeechFolder(string sampleRelPath, string outLanguage)
        {
            if (sampleRelPath.IndexOf("Speech", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                string[] data = sampleRelPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                int startPoint = Array.FindIndex(data, s => s.Equals("Speech", StringComparison.OrdinalIgnoreCase));
                if (startPoint >= 0)
                {
                    data[startPoint + 1] = outLanguage;
                    return string.Join("\\", data);
                }
            }
            else
            {
                return sampleRelPath;
            }

            return string.Empty;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void CheckForMissingFolders()
        {
            if (Directory.Exists(GlobalPrefs.ProjectFolder))
            {
                //Temporal Output Folder
                foreach (KeyValuePair<string, Objects.PlatformData> folderName in GlobalPrefs.CurrentProject.platformData)
                {
                    string stringFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", folderName.Key, "SoundBanks");
                    Directory.CreateDirectory(stringFolder);
                }

                //Debug Folder
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Debug_Report", "ForES2", "MarkerFileData"));

                //Reverbs
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs"));

                //Music
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"));
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork"));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void RunOutputScripts(bool preOutput = false)
        {
            bool viewOutputDos = false;
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(systemIniFilePath))
            {
                IniFile systemIni = new IniFile(systemIniFilePath);
                viewOutputDos = systemIni.Read("ViewOutputDos", "PropertiesForm").Equals("1");
            }

            if (preOutput)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "PreOutput.bat");
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        sw.WriteLine("rem Add your pre-output stuff here");
                    }
                }
                RunConsoleProcess(filePath, string.Empty, viewOutputDos);
            }
            else
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "PostOutput.bat");
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        sw.WriteLine("rem Add your post-output stuff here");
                    }
                }
                RunConsoleProcess(filePath, string.Empty, viewOutputDos);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSoundbankOutPath(string platform, string language, bool musicFolder = false)
        {
            //Get Output Path
            string outputPath = string.Empty;

            //EngineX project Path
            if (!string.IsNullOrEmpty(GlobalPrefs.CurrentProject.EngineXProjectPath) && Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath))
            {
                if (musicFolder)
                {
                    outputPath = Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", GetEnginexFolder(platform), "music")).FullName;
                }
                else
                {
                    Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Sonix"));
                    outputPath = Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", GetEnginexFolder(platform), GetLanguageFolder(language))).FullName;
                }
            }

            //Default Path
            if (string.IsNullOrEmpty(outputPath) && GlobalPrefs.CurrentProject.platformData.ContainsKey(platform))
            {
                string outFolder = GlobalPrefs.CurrentProject.platformData[platform].OutputFolder;
                if (!string.IsNullOrEmpty(outFolder) && Path.IsPathRooted(outFolder))
                {
                    outputPath = Directory.CreateDirectory(outFolder).FullName;
                }
            }
            return outputPath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void BuildSoundHFile()
        {
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(GlobalPrefs.CurrentProject.EuroLandHashCodeServerPath, "Sound.h"), FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("/* HT_Sound */");
                string sfxDefinesFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Defines.h");
                if (File.Exists(sfxDefinesFilePath))
                {
                    string[] fileData = File.ReadAllLines(sfxDefinesFilePath);
                    for (int i = 0; i < fileData.Length; i++)
                    {
                        sw.WriteLine(fileData[i]);
                    }
                }
                string mfxDefinesFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "MFX_Defines.h");
                if (File.Exists(mfxDefinesFilePath))
                {
                    sw.WriteLine(string.Empty);
                    string[] fileData = File.ReadAllLines(mfxDefinesFilePath);
                    for (int i = 0; i < fileData.Length; i++)
                    {
                        sw.WriteLine(fileData[i]);
                    }
                }
                string reverbsFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Reverbs.h");
                if (File.Exists(reverbsFilePath))
                {
                    sw.WriteLine(string.Empty);
                    string[] fileData = File.ReadAllLines(reverbsFilePath);
                    for (int i = 0; i < fileData.Length; i++)
                    {
                        sw.WriteLine(fileData[i]);
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
