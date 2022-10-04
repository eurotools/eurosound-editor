using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_Misc : UserControl
    {
        internal List<string> DebugLog = new List<string>();

        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_Misc()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMisc_Properties_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(GlobalPrefs.ProjectFolder) && Directory.Exists(GlobalPrefs.ProjectFolder))
            {
                using (PropertiesForm projectProperties = new PropertiesForm())
                {
                    projectProperties.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(string.Format("Invalid Project Directory {0}", GlobalPrefs.ProjectFolder), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMisc_ReSampling_Click(object sender, System.EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Check for new Samples
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "System")) && Directory.Exists(Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master")))
            {
                //Calculate Execution time
                Stopwatch watcher = new Stopwatch();
                watcher.Start();

                //Get Samples File Data
                string samplesFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt");
                SamplePool samples = new SamplePool();
                if (File.Exists(samplesFilePath))
                {
                    samples = TextFiles.ReadSamplesFile(samplesFilePath);
                }

                //Check For New Samples
                string[] missingSamples = SampleFiles.GetMissingSamples(samples);
                if (missingSamples.Length > 0)
                {
                    using (MissingSamplesFound newSamplesForm = new MissingSamplesFound(missingSamples, samples))
                    {
                        newSamplesForm.ShowDialog();
                    }
                }

                // Check for missing Samples
                string[] newSamples = SampleFiles.GetNewSamples(samples);
                if (newSamples.Length > 0)
                {
                    using (NewSamplesFound newSamplesForm = new NewSamplesFound(newSamples, samples))
                    {
                        newSamplesForm.ShowDialog();
                    }
                }

                //Show Form
                if (samples.SamplePoolItems.Count > 0)
                {
                    using (ReSampleForm resampleForm = new ReSampleForm(samples, watcher))
                    {
                        resampleForm.ShowDialog();
                    }
                }
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMisc_MusicMaker_Click(object sender, System.EventArgs e)
        {
            string musicDir = Path.Combine(GlobalPrefs.ProjectFolder, "Music");
            if (Directory.Exists(musicDir))
            {
                using (MusicApp makerForm = new MusicApp())
                {
                    makerForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(string.Format("Invalid Music Directory: {0}", musicDir), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Music_Misc_SfxDefault_Click(object sender, System.EventArgs e)
        {
            using (SFXForm defaults = new SFXForm("SFX Default Setting", true))
            {
                defaults.ShowDialog();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMisc_Advanced_Click(object sender, System.EventArgs e)
        {
            using (Advanced frmAdvanced = new Advanced())
            {
                frmAdvanced.ShowDialog();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnMisc_MarkersEditor_Click(object sender, System.EventArgs e)
        {
            try
            {
                Process.Start(Path.Combine(Application.StartupPath, "SystemFiles", "MarkersEditor.exe"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtMisc_Debug_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            List<string> debugOut = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Misc.DebugLog;
            using (DebugForm debugFrm = new DebugForm(debugOut.ToArray()))
            {
                debugFrm.ShowDialog();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
