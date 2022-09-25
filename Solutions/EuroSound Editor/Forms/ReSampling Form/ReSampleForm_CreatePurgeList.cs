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
            string[] sampleFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master"), "*.wav", SearchOption.AllDirectories);
            for (int i = 0; i < sampleFiles.Length; i++)
            {
                int MasterFolderLength = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master").Length;
                availableSamples.Add(sampleFiles[i].Substring(MasterFolderLength));
            }

            //Get Used Samples from SFX Files
            string[] sfxFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.AllDirectories);
            string[] availableFormats = new string[] { "Common" }.Concat(GlobalPrefs.CurrentProject.platformData.Keys.ToArray()).ToArray();
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
            ProgressBar1.Value = e.ProgressPercentage;
            Text = (string)e.UserState;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
