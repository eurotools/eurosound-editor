using sb_editor.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static ESUtils.Enumerations;

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
            // Initialize the userName variable to an empty string
            string userName = string.Empty;

            // Keep asking the user for their username until they enter a non-empty string
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

            // Update the global user name variable and the EuroSound.ini file with the user's username
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
            // Get the first character of the language string and make it uppercase
            char firstCharacter = char.ToUpper(language[0]);

            // Get the next two characters of the language string and make them lowercase
            string nextTwoCharacters = language.Substring(1, 2).ToLower();

            // Concatenate the three strings and format them as a language folder name
            string langFolderName = string.Format("_{0}{1}", firstCharacter, nextTwoCharacters);

            return langFolderName;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetOutputPlatforms()
        {
            //Get Output Platforms
            UserControl_MainForm_Output outputControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output;
            string[] platforms = new string[] { outputControl.cboOutputFormat.SelectedItem.ToString() };
            if (outputControl.rdoAllForAll.Checked)
            {
                platforms = GlobalPrefs.CurrentProject.platformData.Keys.ToArray();
            }
            return platforms;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetOutputLanguages()
        {
            // Get the output user control from the main form
            UserControl_MainForm_Output outputControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output;

            // Initialize the languages array to "English"
            string[] languages = new string[] { "English" };

            // If the "output all languages" checkbox is checked and the output language combo box has items, set the languages array to the items in the combo box
            if (outputControl.chkOutputAllLanguages.Checked && outputControl.cboOutputLanguage.Items.Count > 0)
            {
                languages = outputControl.cboOutputLanguage.Items.Cast<string>().ToArray();
            }
            // If the output language combo box has a selected item, set the languages array to the selected item
            else if (outputControl.cboOutputLanguage.SelectedItem != null)
            {
                languages = new string[] { outputControl.cboOutputLanguage.SelectedItem.ToString() };
            }

            return languages;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSfxName(Language language, string fileName)
        {
            string lang = language.ToString();
            return string.Format("{0}_{1}.SFX", lang.Substring(0, Math.Min(3, lang.Length)), fileName).ToLower();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static byte[] RemoveFileHeader(string filePath, int numBytesToRemove)
        {
            // Read the contents of the file into a byte array
            byte[] fileContents = File.ReadAllBytes(filePath);

            // Check if the number of bytes to remove is greater than the length of the file
            if (numBytesToRemove >= fileContents.Length)
            {
                // If it is, clear the file
                return fileContents;
            }
            else
            {
                // If it's not, create a new byte array that is numBytesToRemove shorter than the original array, starting at the numBytesToRemove index
                byte[] newFileContents = new byte[fileContents.Length - numBytesToRemove];
                Array.Copy(fileContents, numBytesToRemove, newFileContents, 0, newFileContents.Length);

                // Write the new byte array back to the file
                return newFileContents;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSampleDate(string filePath)
        {
            // Get the last write time of the file in UTC
            DateTime fileLastWriteTimeUtc = File.GetLastWriteTimeUtc(filePath);

            // Get the current time zone's offset from UTC
            TimeSpan timeZoneOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);

            // Add the time zone offset to the file last write time in UTC to get the local time
            DateTime fileLastWriteTimeLocal = fileLastWriteTimeUtc.Add(timeZoneOffset);

            // Get the year, month, and day as strings
            string year = fileLastWriteTimeLocal.Year.ToString();
            string month = fileLastWriteTimeLocal.Month.ToString();
            string day = fileLastWriteTimeLocal.Day.ToString();

            // Add leading zeros to the month and day if necessary
            if (fileLastWriteTimeLocal.Month < 10)
            {
                month = "00" + fileLastWriteTimeLocal.Month.ToString();
            }
            if (fileLastWriteTimeLocal.Day < 10)
            {
                day = "00" + fileLastWriteTimeLocal.Day.ToString();
            }

            // Return the date and time as a string in the specified format
            return string.Format("{0}/{1}/{2} {3:#0}:{4:00}:{5:00}", year, day, month, fileLastWriteTimeLocal.Hour, fileLastWriteTimeLocal.Minute, fileLastWriteTimeLocal.Second);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSampleSize(string sampleFilePath)
        {
            // Get the length of the file in bytes and convert them to a string
            string fileLengthInBytes = new FileInfo(sampleFilePath).Length.ToString();

            // Calculate the number of padding characters needed to make the file length string have a length of 11
            int paddingCount = Math.Max(11 - fileLengthInBytes.Length, 0);

            // Add padding to the left side of the file length string
            string paddedFileLength = fileLengthInBytes.PadLeft(fileLengthInBytes.Length + paddingCount);

            return paddedFileLength;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static string GetSampleFromSpeechFolder(string sampleFilePath, Language languageFolder)
        {
            // Split the file path into an array of strings
            string[] pathElements = sampleFilePath.Split('\\');

            // Find the index of the "Speech" folder
            int speechFolderIndex = Array.FindIndex(pathElements, s => s.Equals("Speech", StringComparison.OrdinalIgnoreCase));

            // Check if the "Speech" folder was found
            if (speechFolderIndex >= 0)
            {
                // If it was found, change the folder after "Speech" to the given language folder name
                pathElements[speechFolderIndex + 1] = languageFolder.ToString();

                // Join the array of strings back into a file path and return it
                return string.Join("\\", pathElements);
            }

            // If the "Speech" folder was not found, return the original file path
            return sampleFilePath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void CheckForMissingFolders()
        {
            // Check if the project folder exists
            if (Directory.Exists(GlobalPrefs.ProjectFolder))
            {
                // Create the temporal output folders for each platform in the platformData dictionary
                foreach (KeyValuePair<string, Objects.PlatformData> platformData in GlobalPrefs.CurrentProject.platformData)
                {
                    string temporalOutputFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", platformData.Key, "SoundBanks");
                    Directory.CreateDirectory(temporalOutputFolder);
                }

                // Create the Debug_Report, Reverbs, and Music folders
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Debug_Report", "ForES2", "MarkerFileData"));
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Reverbs"));
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"));
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESWork"));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void RunOutputScripts(string filePath, string fileContent)
        {
            // Initialize the viewOutputDos flag to false
            bool viewOutputInDosWindow = false;

            // Get the path to the EuroSound.ini file
            string systemIniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");

            // Check if the EuroSound.ini file exists
            if (File.Exists(systemIniFilePath))
            {
                // If it does, read the ViewOutputDos setting from the PropertiesForm section
                IniFile systemIni = new IniFile(systemIniFilePath);
                viewOutputInDosWindow = systemIni.Read("ViewOutputDos", "PropertiesForm").Equals("1");
            }

            // Check if the output script file does not exist
            if (!File.Exists(filePath))
            {
                // If it does not exist, create it and write the file content to it using a StreamWriter
                using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    sw.WriteLine(fileContent);
                }
            }

            // Run the console process with the specified file path and viewOutputDos flag
            RunConsoleProcess(filePath, string.Empty, viewOutputInDosWindow);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSoundbankOutPath(string platform)
        {
            // Initialize the output path to an empty string
            string outputPath = string.Empty;

            // Check if the EngineX project path is not null and the directory exists
            if (!string.IsNullOrEmpty(GlobalPrefs.CurrentProject.EngineXProjectPath) && Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath))
            {
                // Create the "Sonix" folder within the EngineX project path
                Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Sonix"));

                // Set the output path to the language folder within the EngineX project path
                outputPath = Directory.CreateDirectory(Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", GetEnginexFolder(platform), "audio")).FullName;
            }

            // If the output path is still empty, check if the platform is in the platformData dictionary
            if (string.IsNullOrEmpty(outputPath) && GlobalPrefs.CurrentProject.platformData.ContainsKey(platform))
            {
                // Get the output folder for the platform
                string outFolder = GlobalPrefs.CurrentProject.platformData[platform].OutputFolder;

                // Check if the output folder is not null and taht is a rooted path
                if (!string.IsNullOrEmpty(outFolder) && Path.IsPathRooted(outFolder))
                {
                    // If it is, set the output path to the output folder
                    outputPath = Directory.CreateDirectory(outFolder).FullName;
                }
            }

            return outputPath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetPlatformLabel(string outPlatform)
        {
            string platform = "____";
            if (outPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
            {
                platform = "PS2_";
            }
            else if (outPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
            {
                platform = "GC__";
            }
            else if (outPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase))
            {
                platform = "PC__";
            }
            else if (outPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase) || outPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase))
            {
                platform = "XB__";
            }

            return platform;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
