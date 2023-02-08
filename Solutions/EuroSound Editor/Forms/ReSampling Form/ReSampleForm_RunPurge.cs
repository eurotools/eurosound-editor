using sb_editor.Classes;
using sb_editor.Forms;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class ReSampleForm_RunPurge : TimerForm
    {
        private int PurgedFilesCount;
        private readonly ReSampleForm parentFormObj;

        //-------------------------------------------------------------------------------------------------------------------------------
        public ReSampleForm_RunPurge(ReSampleForm parentForm)
        {
            InitializeComponent();
            parentFormObj = parentForm;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_ReSampleRates_RunPurge_Load(object sender, EventArgs e)
        {
            parentFormObj.Hide();
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_ReSampleRates_RunPurge_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                e.Cancel = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Create Master Trash Folder
            string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, string.Format("Master_Trash_{0:dd_M_yyyy}", DateTime.Now));
            Directory.CreateDirectory(folderPath);

            //Start Moving Files
            string reportFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Report", "Last_Purge.txt");
            string[] filesToDelete = TextFiles.ReadPurgeFiles(reportFilePath);
            for (int i = 0; i < filesToDelete.Length; i++)
            {
                string fileSource = Path.Combine(parentFormObj.projectSettings.SampleFilesFolder, "Master", filesToDelete[i].TrimStart('\\'));
                string fileDest = Path.Combine(folderPath, filesToDelete[i].TrimStart('\\'));
                if (File.Exists(fileSource) && !File.Exists(fileDest))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileDest));
                    File.Move(fileSource, fileDest);

                    PurgedFilesCount++;

                    //Update ListView
                    parentFormObj.lvwAllSamples.Invoke((MethodInvoker)delegate
                    {
                        ListViewItem itemToRemove = parentFormObj.lvwAllSamples.FindItemWithText(filesToDelete[i]);
                        itemToRemove.Remove();
                    });
                }
                backgroundWorker1.ReportProgress((int)decimal.Divide(i * 100, filesToDelete.Length), string.Format("Moving Sample: {0} to {1}", fileSource, fileDest));
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
                MessageBox.Show(e.Error.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(string.Format("Purged {0} Files.", PurgedFilesCount), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                parentFormObj.SaveSamplesFile();
            }
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
