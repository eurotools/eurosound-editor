using sb_editor.Audio_Classes;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
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
        private readonly ImaFunctions imaFunctions = new ImaFunctions();
        private readonly AiffFunctions aiffFunctions = new AiffFunctions();
        private readonly SoundBankFunctions sbFunctions = new SoundBankFunctions();
        private readonly string[] filesQueue;
        private readonly string[] outputPlatform;
        private readonly string[] outLanguages;
        private readonly bool fastOutput;
        private bool abortQuickOutput = false;
        private double FullOutputTime = 0;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SfxOutputForm(string[] outputFiles, string[] outPlatform, string[] languages, bool quickOutput, MainForm parentForm)
        {
            InitializeComponent();
            filesQueue = outputFiles;
            parentFormObj = parentForm;
            outputPlatform = outPlatform;
            fastOutput = quickOutput;
            outLanguages = languages;
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
                samplesList.CheckForUpdates();
            }

            //Get HashCodes Dictionary
            Dictionary<string, uint> HashCodesDict = sbFunctions.GetHashCodesDictionary("SFXs", "#HASHCODE");

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
            OutputSoundBanks(samplesList, HashCodesDict, debugFolder.FullName);

            //Create HashTables
            if (!fastOutput && !string.IsNullOrEmpty(GlobalPrefs.CurrentProject.HashCodeFileDirectory) && Directory.Exists(GlobalPrefs.CurrentProject.HashCodeFileDirectory))
            {
                OutputHashCodes(samplesList);

                //Create SFX Data
                string sfxFilePath = Path.Combine(GlobalPrefs.CurrentProject.HashCodeFileDirectory, "SFX_Data.h");
                if (!string.IsNullOrEmpty(GlobalPrefs.CurrentProject.EngineXProjectPath) && Directory.Exists(GlobalPrefs.CurrentProject.EngineXProjectPath) && File.Exists(sfxFilePath))
                {
                    for (int i = 0; i < outputPlatform.Length; i++)
                    {
                        string outputPath = Path.Combine(GlobalPrefs.CurrentProject.EngineXProjectPath, "Binary", CommonFunctions.GetEnginexFolder(outputPlatform[i]), "Music");
                        Directory.CreateDirectory(outputPath);
                        CommonFunctions.RunConsoleProcess(Path.Combine(Application.StartupPath, "SystemFiles", "SFXStructToBin.exe"), string.Format("\"{0}\" \"{1}\"", sfxFilePath, Path.Combine(outputPath, "SFX_Data.bin")), false);
                    }
                }
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
