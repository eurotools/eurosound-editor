using sb_editor.Objects;
using sb_editor.Panels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static ESUtils.Enumerations;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Advanced : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public Advanced()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMakeReport_Click(object sender, System.EventArgs e)
        {
            //Update to wait cursor
            Cursor.Current = Cursors.WaitCursor;

            //Create report from the default selected soundbank
            UserControl_Manform_SoundBanks mainForm = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_SoundBanks;
            if (mainForm.tvwSoundBanks.Nodes.Count > 0)
            {
                string soundBankName = mainForm.tvwSoundBanks.Nodes[0].Text;
                string soundBankPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", soundBankName + ".txt");
                if (File.Exists(soundBankPath))
                {
                    // Create the report folder if it doesn't exist
                    string reportFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "Report");
                    if (!Directory.Exists(reportFolderPath))
                    {
                        Directory.CreateDirectory(reportFolderPath);
                    }

                    // Create the report file in the report folder
                    Language outLang = (Language)Enum.Parse(typeof(Language), CommonFunctions.GetOutputLanguages()[0], true);
                    CreateReport(Path.Combine(reportFolderPath, soundBankName + ".txt"), soundBankPath, CommonFunctions.GetOutputPlatforms()[0], outLang);
                }
            }

            //Restore default cursor
            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnCheckHashCodes_Click(object sender, System.EventArgs e)
        {
            // Change cursor to wait cursor
            Cursor.Current = Cursors.WaitCursor;

            // Get the SFX files in the SFXs folder
            string sfxFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(sfxFolderPath))
            {
                string[] sfxFilePaths = Directory.GetFiles(sfxFolderPath, "*.txt", SearchOption.TopDirectoryOnly);

                // Array to store the hash codes of all SFX files
                uint[] hashCodes = new uint[sfxFilePaths.Length];
                // List to store the names of SFX files with duplicate hash codes
                List<string> duplicateSfxNames = new List<string>() { "No Duplicate HashCodes Found" };
                // Flag to check if this is the first time a duplicate hash code is found
                bool firstDuplicateFound = true;

                // Start checking the hash codes of the SFX files
                for (int i = 0; i < sfxFilePaths.Length; i++)
                {
                    string[] fileLines = File.ReadAllLines(sfxFilePaths[i]);
                    // Get the index of the line with the hash code
                    int hashCodeLineIndex = Array.FindIndex(fileLines, line => line.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    // Split the line with the hash code value into an array of strings
                    string[] hashCodeData = fileLines[hashCodeLineIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (hashCodeData.Length > 0)
                    {
                        // Get the hash code value as an unsigned integer
                        uint hashCode = Convert.ToUInt32(hashCodeData[1]);
                        // Check if the hash code has already been found in another SFX file
                        if (Array.IndexOf(hashCodes, hashCode) > 0)
                        {
                            // If this is the first time a duplicate hash code is found, add a header to the list of duplicate SFX names
                            if (firstDuplicateFound)
                            {
                                duplicateSfxNames[0] = "SFXs Found With Duplicate HashCodes";
                                duplicateSfxNames.Add(string.Empty);
                                firstDuplicateFound = !firstDuplicateFound;
                            }
                            // Add the name of the current SFX file to the list
                            duplicateSfxNames.Add(Path.GetFileNameWithoutExtension(sfxFilePaths[i]));

                            // Update the hash code value in the SFX file
                            fileLines[hashCodeLineIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SFXHashCodeNumber++);
                            File.WriteAllLines(sfxFilePaths[i], fileLines);
                        }
                        // Store the hash code value in the array
                        hashCodes[i] = hashCode;
                    }
                }

                //Show Form
                using (DebugForm debugFrm = new DebugForm(duplicateSfxNames.ToArray()))
                {
                    debugFrm.ShowDialog();
                }
            }

            //Restore default cursor
            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnReAllocateHashcodes_Click(object sender, EventArgs e)
        {
            // Change cursor to wait cursor
            Cursor.Current = Cursors.WaitCursor;

            // Reallocate hashCodes for SFX files
            string sfxFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(sfxFolderPath))
            {
                // Reset the SFX hash code number
                GlobalPrefs.SFXHashCodeNumber = 1;
                // Get the SFX files in the SFXs folder
                string[] sfxFilePaths = Directory.GetFiles(sfxFolderPath, "*.txt", SearchOption.TopDirectoryOnly);
                // Loop through the SFX files
                for (int i = 0; i < sfxFilePaths.Length; i++)
                {
                    // Read the lines of the SFX file
                    string[] fileLines = File.ReadAllLines(sfxFilePaths[i]);
                    // Get the index of the line with the hashCode
                    int hashCodeLineIndex = Array.FindIndex(fileLines, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    // Split the line with the hash code value into an array of strings
                    string[] hashCodeData = fileLines[hashCodeLineIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (hashCodeData.Length > 0)
                    {
                        // Update the SFX file with the new hash code value
                        fileLines[hashCodeLineIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SFXHashCodeNumber++);
                        File.WriteAllLines(sfxFilePaths[i], fileLines);
                    }
                }
            }
            //ReAllocate SoundBank HashCodes
            string sbFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");
            if (Directory.Exists(sbFolderPath))
            {
                // Reset the SoundBank hash code number
                GlobalPrefs.SoundBankHashCodeNumber = 1;
                // Get the SB files in the SoundBanks folder
                IEnumerable<string> sbFiles = Directory.EnumerateFiles(sbFolderPath, "*.txt", SearchOption.TopDirectoryOnly);
                // Loop through the SB files
                foreach (string sbFile in sbFiles)
                {
                    // Read the lines of the SB file
                    string[] fileLines = File.ReadAllLines(sbFile);
                    // Get the index of the line with the hashCode
                    int hashCodeLineIndex = Array.FindIndex(fileLines, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    // Split the line with the hash code value into an array of strings
                    string[] hashCodeData = fileLines[hashCodeLineIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (hashCodeData.Length > 0)
                    {
                        // Update the SB file with the new hash code value
                        fileLines[hashCodeLineIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SoundBankHashCodeNumber++);
                        File.WriteAllLines(sbFile, fileLines);
                    }
                }
            }

            //ReAllocate Mfx Files
            string mfxFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData");
            if (Directory.Exists(mfxFolderPath))
            {
                // Reset the MFX hash code number
                GlobalPrefs.MFXHashCodeNumber = 1;
                // Get the MFX files in the Music folder
                IEnumerable<string> mfxFiles = Directory.EnumerateFiles(mfxFolderPath, "*.txt", SearchOption.TopDirectoryOnly);
                // Loop through the MFX files
                foreach (string mfxFile in mfxFiles)
                {
                    // Read the lines of the MFX file
                    string[] fileLines = File.ReadAllLines(mfxFile);
                    // Get the index of the line with the hashCode
                    int hashCodeLineIndex = Array.FindIndex(fileLines, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    // Split the line with the hashCode value into an array of strings
                    string[] hashCodeData = fileLines[hashCodeLineIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (hashCodeData.Length > 0)
                    {
                        // Update the MFX file with the new hash code value
                        fileLines[hashCodeLineIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SoundBankHashCodeNumber++);
                        File.WriteAllLines(mfxFile, fileLines);
                    }
                }
            }

            //Change cursor to default cursor
            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnValidateInterSample_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Get all SFXs that have sub SFXs
            List<string> errorMessages = new List<string>();

            string sfxsFolder = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(sfxsFolder))
            {
                // Read all SFX files in the SFXs folder
                IEnumerable<string> sfxFiles = Directory.EnumerateFiles(sfxsFolder, "*.txt", SearchOption.TopDirectoryOnly);

                // Inspect each SFX file
                foreach (string sfxFilePath in sfxFiles)
                {
                    // Read the SFX data from the file
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);

                    // Check if the SFX has negative multi samples or loops
                    if (sfxData.SamplePool.Action1 == 1 && sfxData.SamplePool.EnableSubSFX)
                    {
                        if (sfxData.SamplePool.MinDelay < 0 | sfxData.SamplePool.MaxDelay < 0)
                        {
                            errorMessages.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + "   -ve Multi");
                        }
                    }
                    else if (sfxData.SamplePool.isLooped && sfxData.SamplePool.Action1 == 0)
                    {
                        if (sfxData.SamplePool.MinDelay < 0 | sfxData.SamplePool.MaxDelay < 0)
                        {
                            errorMessages.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + "   -ve Loop");
                        }
                    }
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                // Check what we need to show to the user
                if (errorMessages.Count > 0)
                {
                    // Show the error messages to the user
                    using (DebugForm debugInfo = new DebugForm(errorMessages.ToArray()))
                    {
                        debugInfo.ShowDialog();
                    }
                }
                else
                {
                    // No errors were found, show a success message
                    MessageBox.Show("All OK", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnLanguageFolderTool_Click(object sender, EventArgs e)
        {
            using (LanguageFolderCompare langTool = new LanguageFolderCompare())
            {
                langTool.ShowDialog();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnCheckStealOnLouder_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Initialize list of error messages
            List<string> errorMessages = new List<string>();

            // Get path of SFXs folder
            string sfxsFolder = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(sfxsFolder))
            {
                // Inspect files
                IEnumerable<string> sfxFiles = Directory.EnumerateFiles(sfxsFolder, "*.txt", SearchOption.TopDirectoryOnly);
                foreach (string sfxFilePath in sfxFiles)
                {
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);

                    // Check if Steal On Age flag is on
                    if (sfxData.Parameters.StealOnAge)
                    {
                        // Check if any of the samples have non-zero random volume
                        for (int sampleIndex = 0; sampleIndex < sfxData.Samples.Count; sampleIndex++)
                        {
                            SfxSample currentSample = sfxData.Samples[sampleIndex];
                            if (currentSample.RandomVolume != 0)
                            {
                                // Add error message to list
                                errorMessages.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + " -->> " + currentSample.FilePath);
                            }
                        }
                    }
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                // Check if any errors were found
                if (errorMessages.Count > 0)
                {
                    // Show info to the user
                    using (DebugForm debugInfo = new DebugForm(errorMessages.ToArray()))
                    {
                        debugInfo.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("All OK", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnValidateSubSFXs_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // List to store missing links
            List<string> missingLinks = new List<string>();

            // Get the path of the SFXs folder
            string sfxsFolder = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

            // Check if the SFXs folder exists
            if (Directory.Exists(sfxsFolder))
            {
                // Inspect files
                IEnumerable<string> sfxFiles = Directory.EnumerateFiles(sfxsFolder, "*.txt", SearchOption.TopDirectoryOnly);

                // Inspect each SFX file
                foreach (string sfxFilePath in sfxFiles)
                {
                    // Read the SFX file data
                    SFX sfxFileData = TextFiles.ReadSfxFile(sfxFilePath);

                    // Check if the SFX file has sub SFXs enabled
                    if (sfxFileData.SamplePool.EnableSubSFX)
                    {
                        // Check each sample in the SFX file
                        for (int sampleIndex = 0; sampleIndex < sfxFileData.Samples.Count; sampleIndex++)
                        {
                            // Get the file path of the sub SFX file
                            string linkHashCode = sfxFileData.Samples[sampleIndex].FilePath;
                            string subSfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", linkHashCode + ".txt");

                            // Check if the sub SFX file exists
                            if (!File.Exists(subSfxFilePath))
                            {
                                // Add the missing link to the list
                                missingLinks.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + " #=# " + linkHashCode);
                            }
                        }
                    }
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                // Check if any missing links were found
                if (missingLinks.Count > 0)
                {
                    // Add a separator to the list
                    missingLinks.Add("------------------------------------------");

                    // Show the missing links to the user
                    using (DebugForm debugInfo = new DebugForm(missingLinks.ToArray()))
                    {
                        debugInfo.ShowDialog();
                    }
                }
                else
                {
                    // No missing links were found, show a message to the user
                    MessageBox.Show("All OK", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnValidatePlatforms_Click(object sender, EventArgs e)
        {
            // Create a list to store the SFXs for each platform
            List<string> sfxPlatformsList = new List<string>();

            // Get the base directory for the SFXs
            string sfxFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Get the directories for each platform
            string[] platforms = { "GameCube", "PC", "PlayStation2", "X Box" };
            foreach (string platform in platforms)
            {
                string platformDir = Path.Combine(sfxFolderPath, platform);
                if (Directory.Exists(platformDir))
                {
                    GetPlatformSFXs(platformDir, sfxPlatformsList, platform);
                }
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;

            // Show the list of SFXs for each platform to the user
            sfxPlatformsList.Sort();
            using (DebugForm debugInfo = new DebugForm(sfxPlatformsList.ToArray()))
            {
                debugInfo.ShowDialog();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void GetPlatformSFXs(string folderToInspect, List<string> sfxPlatformsList, string platform)
        {
            // Get all the files in the specified folder
            IEnumerable<string> filesToInspect = Directory.EnumerateFiles(folderToInspect, "*.txt", SearchOption.TopDirectoryOnly);

            // Iterate over the files and add their names and the platform to the sfxPlatformsList
            foreach (string currentFilePath in filesToInspect)
            {
                sfxPlatformsList.Add(Path.GetFileNameWithoutExtension(currentFilePath) + "  " + platform);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSetupSfxGroups_Click(object sender, EventArgs e)
        {
            using(GroupingForm sfxGroups = new GroupingForm())
            {
                sfxGroups.ShowDialog();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
