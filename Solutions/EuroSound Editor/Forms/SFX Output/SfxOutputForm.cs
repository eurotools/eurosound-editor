//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFX Form Output
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using MusX.Writers;
using sb_editor.Audio_Classes;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm : TimerForm
    {
        private readonly MainForm parentFormObj;
        private readonly WaveFunctions wavFunctions = new WaveFunctions();
        private readonly EurocomImaFunctions eurocomImaFunction = new EurocomImaFunctions();
        private readonly AiffFunctions aiffFunctions = new AiffFunctions();
        private readonly SoundBankFunctions sbFunctions = new SoundBankFunctions();
        private readonly string[] filesQueue;
        private readonly string[] outputPlatform;
        private readonly string[] outLanguages;
        private readonly bool fastOutput;
        private bool abortQuickOutput = false;
        private double FullOutputTime = 0;
        internal readonly ProjProperties projectSettings;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SfxOutputForm(string[] outputFiles, string[] outPlatform, string[] languages, bool quickOutput, MainForm parentForm)
        {
            InitializeComponent();
            filesQueue = outputFiles;
            parentFormObj = parentForm;
            outputPlatform = outPlatform;
            fastOutput = quickOutput;
            outLanguages = languages;

            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SfxOutputForm_Shown(object sender, EventArgs e)
        {
            parentFormObj.Hide();
            if (!backgroundWorker1.IsBusy)
            {
                //Run Bat scripts
                CommonFunctions.RunOutputScripts(Path.Combine(GlobalPrefs.ProjectFolder, "System", "PreOutput.bat"), "rem Add your pre-output stuff here");

                //Start Working
                backgroundWorker1.RunWorkerAsync();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SfxOutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                e.Cancel = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Reset Global vars
            parentFormObj.UserControl_Misc.DebugLog.Clear();

            //Create Missing Folders 
            Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "TempSfxData"));
            string samplesFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt");
            SamplePool samplesList = new SamplePool();
            if (File.Exists(samplesFilePath))
            {
                samplesList = TextFiles.ReadSamplesFile(samplesFilePath);
                samplesList.CheckForUpdates(projectSettings);
            }

            //Ensure that the debug folder exists
            DirectoryInfo debugFolder = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Debug_Report"));

            //Re-Sample for each platform if required
            if (!fastOutput)
            {
                ResSample(samplesList);
            }

            //Output Strams
            if (GlobalPrefs.ReSampleStreams)
            {
                OutputStreams(samplesList, outLanguages, debugFolder.FullName);
                GlobalPrefs.ReSampleStreams = false;
            }

            //Output SoundBanks
            OutputSoundBanks(samplesList, debugFolder.FullName);

            //Output Project Details
            for (int i = 0; i < outputPlatform.Length; i++)
            {
                bool isBigEndian = outputPlatform[i].Equals("GameCube", StringComparison.OrdinalIgnoreCase);

                string tempFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", outputPlatform[i], "projectdetails.pdf");
                OutputProjectDetailsFile(tempFilePath, outputPlatform[i], isBigEndian);

                string sfxFilePath = Path.Combine(CommonFunctions.GetSoundbankOutPath(outputPlatform[i], projectSettings), "__projectdetails.sfx");
                MusXBuild_ProjectDetails.BuildProjectDetailsFile(tempFilePath, sfxFilePath, CommonFunctions.GetPlatformLabel(outputPlatform[i]), CommonFunctions.GetFileHashCode(Enumerations.FileType.ProjectDetails, Enumerations.Language.English, 0), isBigEndian, 5);
            }

            //Create HashTables
            if (!fastOutput && !string.IsNullOrEmpty(projectSettings.HashCodeFileDirectory) && Directory.Exists(projectSettings.HashCodeFileDirectory))
            {
                OutputHashCodes(samplesList);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = e.UserState.ToString();
            ProgressBar1.Value = e.ProgressPercentage;
            if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
            {
                TaskbarProgress.SetValue(Handle, e.ProgressPercentage, ProgressBar1.Maximum);
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
                {
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
                }
                MessageBox.Show(e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Run Bat scripts
            CommonFunctions.RunOutputScripts(Path.Combine(GlobalPrefs.ProjectFolder, "System", "PostOutput.bat"), "rem Add your post-output stuff here");

            //Update Textbox and show form again
            if (fastOutput)
            {
                parentFormObj.UserControl_Misc.txtMisc_Debug.Text = string.Format("Quick Output Time =  {0:0.0000000000000}", FullOutputTime);
            }
            else
            {
                parentFormObj.UserControl_Misc.txtMisc_Debug.Text = string.Format("Full Output Time = {0:0.0000000000000}", FullOutputTime);
            }
            parentFormObj.Show();

            //Close Current Form
            Close();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
