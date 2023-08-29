//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// ReSample Form Purge List
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Classes;
using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class ReSampleForm_CreatePurgeList : TimerForm
    {
        internal string[] filesToPurge;
        private readonly ReSampleForm parentFormObj;

        //-------------------------------------------------------------------------------------------------------------------------------
        public ReSampleForm_CreatePurgeList(ReSampleForm parentForm)
        {
            InitializeComponent();
            parentFormObj = parentForm;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_ReSampleRates_CreatePurgeList_Load(object sender, EventArgs e)
        {
            parentFormObj.Hide();
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_ReSampleRates_CreatePurgeList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                e.Cancel = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            HashSet<string> usedSamples = new HashSet<string>();
            HashSet<string> availableSamples = new HashSet<string>();

            //Get All Samples
            var sampleFiles = Directory.EnumerateFiles(Path.Combine(parentFormObj.projectSettings.SampleFilesFolder, "Master"), "*.wav", SearchOption.AllDirectories);
            foreach (var samplePath in sampleFiles)
            {
                int MasterFolderLength = Path.Combine(parentFormObj.projectSettings.SampleFilesFolder, "Master").Length;
                availableSamples.Add(samplePath.Substring(MasterFolderLength));
            }

            //Get Used Samples from SFX Files
            string[] sfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.AllDirectories);
            string[] availableFormats = new string[] { "Common" }.Concat(parentFormObj.projectSettings.platformData.Keys.ToArray()).ToArray();
            for (int i = 0; i < availableFormats.Length; i++)
            {
                string platform = availableFormats[i];
                for (int j = 0; j < sfxFiles.Length; j++)
                {
                    //Get Full Path
                    string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platform, Path.GetFileName(sfxFiles[j]));
                    if (platform.Equals("Common"))
                    {
                        filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", Path.GetFileName(sfxFiles[j]));
                    }

                    //Get Stored Samples
                    if (File.Exists(filePath))
                    {
                        //Skip Misc Folder
                        if (Path.GetDirectoryName(sfxFiles[j]).EndsWith("Misc", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        //Check for Sample Files
                        SFX sfxFileData = TextFiles.ReadSfxFile(sfxFiles[j]);
                        for (int k = 0; k < sfxFileData.Samples.Count; k++)
                        {
                            string sampleFilePath = sfxFileData.Samples[k].FilePath;
                            if (Path.HasExtension(sampleFilePath))
                            {
                                if (!sampleFilePath.StartsWith("\\"))
                                {
                                    sampleFilePath = "\\" + sampleFilePath;
                                }
                                usedSamples.Add(sampleFilePath);
                            }
                        }
                    }

                    //Report progress
                    int prevCoutns = sfxFiles.Length * i;
                    backgroundWorker.ReportProgress((int)(decimal.Divide(j + prevCoutns, sfxFiles.Length * availableFormats.Length) * 100), string.Format("Creating Sample List {0} {1}", platform, Path.GetFileNameWithoutExtension(sfxFiles[j])));
                }
            }

            //Get Unused Samples
            filesToPurge = availableSamples.Except(usedSamples, StringComparer.OrdinalIgnoreCase).ToArray();
            Array.Sort(filesToPurge);

            //Create Txt
            string reportFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Report", "Last_Purge.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(reportFilePath));
            TextFiles.WritePurgeFilesList(reportFilePath, filesToPurge);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
                {
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
                }
                MessageBox.Show(e.Error.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Cancelled", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = (string)e.UserState;
            ProgressBar1.Value = e.ProgressPercentage;
            if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
            {
                TaskbarProgress.SetValue(Handle, e.ProgressPercentage, ProgressBar1.Maximum);
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
