using sb_editor.Objects;
using sb_editor.Panels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
                    DirectoryInfo reportFolder = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Report"));
                    CreateReport(Path.Combine(reportFolder.FullName, soundBankName + ".txt"), soundBankPath, CommonFunctions.GetOutputPlatforms()[0], CommonFunctions.GetOutputLanguages()[0]);
                }
            }

            //Restore default cursor
            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnCheckHashCodes_Click(object sender, System.EventArgs e)
        {
            //Update to wait cursor
            Cursor.Current = Cursors.WaitCursor;

            //Get files to check
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
            {
                string[] sfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                uint[] hashCodes = new uint[sfxFiles.Length];
                List<string> Errors = new List<string>();
                bool firstTime = true;

                //Start Check
                for (int i = 0; i < sfxFiles.Length; i++)
                {
                    string[] fileData = File.ReadAllLines(sfxFiles[i]);
                    int hashCodeIndex = Array.FindIndex(fileData, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    string[] data = fileData[hashCodeIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 0)
                    {
                        uint hashCode = Convert.ToUInt32(data[1]);
                        if (Array.IndexOf(hashCodes, hashCode) > 0)
                        {
                            if (firstTime)
                            {
                                Errors.Add("SFXs Found With Duplicate HashCodes");
                                Errors.Add(string.Empty);
                                firstTime = !firstTime;
                            }
                            Errors.Add(Path.GetFileNameWithoutExtension(sfxFiles[i]));

                            //Update File
                            fileData[hashCodeIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SFXHashCodeNumber++);
                            File.WriteAllLines(sfxFiles[i], fileData);
                        }
                        hashCodes[i] = hashCode;
                    }
                }

                if (Errors.Count == 0)
                {
                    Errors.Add("No Duplicate HashCodes Found");
                }

                //Show Form
                using (DebugForm debugFrm = new DebugForm(Errors.ToArray()))
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
            //Update to wait cursor
            Cursor.Current = Cursors.WaitCursor;

            //Reallocate SFX HashCodes
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
            {
                GlobalPrefs.SFXHashCodeNumber = 1;
                string[] sfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < sfxFiles.Length; i++)
                {
                    string[] fileData = File.ReadAllLines(sfxFiles[i]);
                    int hashCodeIndex = Array.FindIndex(fileData, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    string[] data = fileData[hashCodeIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 0)
                    {
                        //Update File
                        fileData[hashCodeIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SFXHashCodeNumber++);
                        File.WriteAllLines(sfxFiles[i], fileData);
                    }
                }
            }
            //ReAllocate SoundBank HashCodes
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks")))
            {
                GlobalPrefs.SoundBankHashCodeNumber = 1;
                string[] sbFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks"), "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < sbFiles.Length; i++)
                {
                    string[] fileData = File.ReadAllLines(sbFiles[i]);
                    int hashCodeIndex = Array.FindIndex(fileData, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    string[] data = fileData[hashCodeIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 0)
                    {
                        //Update File
                        fileData[hashCodeIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SoundBankHashCodeNumber++);
                        File.WriteAllLines(sbFiles[i], fileData);
                    }
                }
            }

            //ReAllocate Mfx Files
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData")))
            {
                GlobalPrefs.MFXHashCodeNumber = 1;
                string[] mfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "Music", "ESData"), "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < mfxFiles.Length; i++)
                {
                    string[] fileData = File.ReadAllLines(mfxFiles[i]);
                    int hashCodeIndex = Array.FindIndex(fileData, s => s.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase));
                    string[] data = fileData[hashCodeIndex + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length > 0)
                    {
                        //Update File
                        fileData[hashCodeIndex + 1] = string.Format("HashCodeNumber {0}", GlobalPrefs.SoundBankHashCodeNumber++);
                        File.WriteAllLines(mfxFiles[i], fileData);
                    }
                }
            }

            //Restore default cursor
            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnValidateInterSample_Click(object sender, EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Get all SFXs that has sub SFXs
            List<string> errorsToShow = new List<string>();

            string baseDir = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(baseDir))
            {
                // Inspect files
                string[] filesToInspect = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly);
                for (int fileIndex = 0; fileIndex <= filesToInspect.Length - 1; fileIndex++)
                {
                    string sfxFilePath = filesToInspect[fileIndex];
                    SFX sfxFileData = TextFiles.ReadSfxFile(sfxFilePath);

                    // Check for negative multi samples and loops
                    if (sfxFileData.SamplePool.Action1 == 1 && sfxFileData.SamplePool.EnableSubSFX)
                    {
                        if (sfxFileData.SamplePool.MinDelay < 0 | sfxFileData.SamplePool.MaxDelay < 0)
                        {
                            errorsToShow.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + "   -ve Multi");
                        }
                    }
                    else if (sfxFileData.SamplePool.isLooped && sfxFileData.SamplePool.Action1 == 0)
                    {
                        if (sfxFileData.SamplePool.MinDelay < 0 | sfxFileData.SamplePool.MaxDelay < 0)
                        {
                            errorsToShow.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + "   -ve Loop");
                        }
                    }
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                // Check what we need to show to the user
                if (errorsToShow.Count > 0)
                {
                    // Show info to the user
                    using (DebugForm debugInfo = new DebugForm(errorsToShow.ToArray()))
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

            // Get all SFXs that has sub SFXs
            List<string> errorsToShow = new List<string>();
            string baseDir = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(baseDir))
            {
                // Inspect files
                string[] filesToInspect = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly);
                for (int fileIndex = 0; fileIndex <= filesToInspect.Length - 1; fileIndex++)
                {
                    string sfxFilePath = filesToInspect[fileIndex];
                    SFX sfxFileData = TextFiles.ReadSfxFile(sfxFilePath);

                    // Seems that if the flag Steal On Age is on and the random volume is different than zero is interpreted as an error
                    if (sfxFileData.Parameters.StealOnAge)
                    {
                        for (int sampleIndex = 0; sampleIndex <= sfxFileData.Samples.Count - 1; sampleIndex++)
                        {
                            SfxSample currentSample = sfxFileData.Samples[sampleIndex];
                            if (currentSample.RandomVolume != 0)
                            {
                                errorsToShow.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + " -->> " + currentSample.FilePath);
                            }
                        }
                    }
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                // Check what we need to show to the user
                if (errorsToShow.Count > 0)
                {
                    // Show info to the user
                    using (DebugForm debugInfo = new DebugForm(errorsToShow.ToArray()))
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

            // Get all SFXs that has sub SFXs
            List<string> missingLinks = new List<string>();

            string baseDir = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");
            if (Directory.Exists(baseDir))
            {
                // Inspect files
                string[] filesToInspect = Directory.GetFiles(baseDir, "*.txt", SearchOption.TopDirectoryOnly);
                for (int fileIndex = 0; fileIndex <= filesToInspect.Length - 1; fileIndex++)
                {
                    string sfxFilePath = filesToInspect[fileIndex];
                    SFX sfxFileData = TextFiles.ReadSfxFile(sfxFilePath);
                    if (sfxFileData.SamplePool.EnableSubSFX)
                    {
                        // Add links to dictionary
                        for (int sampleIndex = 0; sampleIndex <= sfxFileData.Samples.Count - 1; sampleIndex++)
                        {
                            string linkHashCode = sfxFileData.Samples[sampleIndex].FilePath;
                            string subSfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", linkHashCode + ".txt");

                            // Add missing link to list
                            if (!File.Exists(subSfxFilePath))
                            {
                                missingLinks.Add(Path.GetFileNameWithoutExtension(sfxFilePath) + " #=# " + linkHashCode);
                            }
                        }
                    }
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                // Check what we need to show to the user
                if (missingLinks.Count > 0)
                {
                    missingLinks.Add("------------------------------------------");
                    // Show info to the user
                    using (DebugForm debugInfo = new DebugForm(missingLinks.ToArray()))
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
        private void BtnValidatePlatforms_Click(object sender, EventArgs e)
        {
            List<string> sfxPlatformsList = new List<string>();
            string baseDir = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Get GameCube SFXs
            string gameCubeDir = Path.Combine(baseDir, "GameCube");
            if (Directory.Exists(gameCubeDir))
            {
                GetPlatformSFXs(gameCubeDir, sfxPlatformsList, "GameCube");
            }

            // Get PC SFXs
            string pcDir = Path.Combine(baseDir, "PC");
            if (Directory.Exists(pcDir))
            {
                GetPlatformSFXs(pcDir, sfxPlatformsList, "PC");
            }

            // Get PC SFXs
            string playStation2 = Path.Combine(baseDir, "PlayStation2");
            if (Directory.Exists(playStation2))
            {
                GetPlatformSFXs(playStation2, sfxPlatformsList, "PlayStation2");
            }

            // Get X Box SFXs
            string Xbox = Path.Combine(baseDir, "X Box");
            if (Directory.Exists(Xbox))
            {
                GetPlatformSFXs(Xbox, sfxPlatformsList, "X Box");
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;

            // Show info to user
            sfxPlatformsList.Sort();
            using (DebugForm debugInfo = new DebugForm(sfxPlatformsList.ToArray()))
            {
                debugInfo.ShowDialog();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void GetPlatformSFXs(string folderToInspect, List<string> sfxPlatformsList, string platform)
        {
            string[] filesToInspect = Directory.GetFiles(folderToInspect, "*.txt", SearchOption.TopDirectoryOnly);
            for (int fileIndex = 0; fileIndex <= filesToInspect.Length - 1; fileIndex++)
            {
                string currentFilePath = filesToInspect[fileIndex];
                sfxPlatformsList.Add(Path.GetFileNameWithoutExtension(currentFilePath) + "  " + platform);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
