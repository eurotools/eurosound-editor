//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFXs Multi Editor
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Objects;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MultiEditor : Form
    {
        private readonly string[] sfxFiles;
        private ProjProperties projectSettings;

        //-------------------------------------------------------------------------------------------------------------------------------
        public MultiEditor(string[] filesToRead)
        {
            InitializeComponent();
            sfxFiles = filesToRead;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MultiEditor_Load(object sender, EventArgs e)
        {
            //Read Project settings
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }

            //Display selected SFXs
            lvwItems.BeginUpdate();
            for (int i = 0; i < sfxFiles.Length; i++)
            {
                SFX fileData = TextFiles.ReadSfxFile(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFiles[i] + ".txt"));
                ListViewItem itemToAdd = new ListViewItem(new string[]{
                    sfxFiles[i],
                    fileData.Parameters.ReverbSend.ToString(),
                    cboTrackingType.Items[fileData.Parameters.TrackingType].ToString(),
                    fileData.Parameters.InnerRadius.ToString(),
                    fileData.Parameters.OuterRadius.ToString(),
                    fileData.Parameters.MaxVoices.ToString(),
                    Convert.ToBoolean(fileData.Parameters.Action1).ToString(),
                    fileData.Parameters.Priority.ToString(),
                    fileData.Parameters.Alertness.ToString(),
                    fileData.Parameters.Ducker.ToString(),
                    fileData.Parameters.DuckerLength.ToString(),
                    fileData.Parameters.MasterVolume.ToString(),
                    fileData.Parameters.Outdoors.ToString(),
                    fileData.Parameters.StealOnAge.ToString()
                });
                lvwItems.Items.Add(itemToAdd);
            }
            lvwItems.EndUpdate();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOpenAsExcel_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(Application.StartupPath, "ExcelImport.txt"), FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("#MultiEdit");
                sw.WriteLine(" {0} ", lvwItems.Items.Count);
                for (int i = 0; i < lvwItems.Columns.Count; i++)
                {
                    for (int j = 0; j < lvwItems.Items.Count; j++)
                    {
                        sw.WriteLine(lvwItems.Items[j].SubItems[i].Text);
                    }
                }
                sw.WriteLine("#END");
                sw.WriteLine("");
            }

            //Start Excel
            try
            {
                Process.Start("excel.exe", string.Format("\"{0}\"", Path.Combine(Application.StartupPath, "SystemFiles", "MultiEdit.xls")));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudReverb_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[1].Text = nudReverb.Value.ToString();

                //Update Text File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.ReverbSend = (int)nudReverb.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.ReverbSend = (int)nudReverb.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboTrackingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[2].Text = cboTrackingType.SelectedItem.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.TrackingType = (byte)cboTrackingType.SelectedIndex;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.TrackingType = (byte)cboTrackingType.SelectedIndex;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudInnerRad_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                int outerRadius = Convert.ToInt32(sfxFile.SubItems[4].Text);
                if (nudInnerRad.Value > outerRadius)
                {
                    nudInnerRad.Value = outerRadius;
                }

                //Update UI
                sfxFile.SubItems[3].Text = nudInnerRad.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.InnerRadius = (int)nudInnerRad.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.InnerRadius = (int)nudInnerRad.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudOuterRad_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                int innerRadius = Convert.ToInt32(sfxFile.SubItems[3].Text);
                if (nudOuterRad.Value < innerRadius)
                {
                    nudOuterRad.Value = innerRadius;
                }

                //Update UI
                sfxFile.SubItems[4].Text = nudOuterRad.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.OuterRadius = (int)nudOuterRad.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.OuterRadius = (int)nudOuterRad.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudMaxVoices_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[5].Text = nudMaxVoices.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.MaxVoices = (int)nudMaxVoices.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.MaxVoices = (int)nudMaxVoices.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboSteal_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[6].Text = cboSteal.SelectedItem.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.Action1 = Convert.ToByte(cboSteal.SelectedIndex == 0);
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.Action1 = Convert.ToByte(cboSteal.SelectedIndex == 0);
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudPriority_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[7].Text = nudPriority.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.Priority = (int)nudPriority.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.Priority = (int)nudPriority.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudAlertness_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[8].Text = nudAlertness.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.Alertness = (int)nudAlertness.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.Alertness = (int)nudAlertness.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudDucker_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[9].Text = nudDucker.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.Ducker = (int)nudDucker.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.Ducker = (int)nudDucker.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudDuckerLength_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[10].Text = nudDuckerLength.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.DuckerLength = (int)nudDuckerLength.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.DuckerLength = (int)nudDuckerLength.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudMasterVol_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[11].Text = nudMasterVol.Value.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.MasterVolume = (int)nudMasterVol.Value;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.MasterVolume = (int)nudMasterVol.Value;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboUnderWater_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[12].Text = cboUnderWater.SelectedItem.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.Outdoors = cboUnderWater.SelectedIndex == 1;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.Outdoors = cboUnderWater.SelectedIndex == 1;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboStealOnLouder_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem sfxFile in lvwItems.SelectedItems)
            {
                //Update UI
                sfxFile.SubItems[13].Text = cboStealOnLouder.SelectedItem.ToString();

                //Update SFX File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFile.Text + ".txt");
                SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
                sfxFileData.Parameters.StealOnAge = cboStealOnLouder.SelectedIndex == 1;
                TextFiles.WriteSfxFile(filePath, sfxFileData);

                //Update Other Platforms
                foreach (string platformToCheck in projectSettings.platformData.Keys)
                {
                    string platfilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", platformToCheck, sfxFile.Text + ".txt");
                    if (File.Exists(platfilePath))
                    {
                        SFX sfxcustomFileData = TextFiles.ReadSfxFile(platfilePath);
                        sfxcustomFileData.Parameters.StealOnAge = cboStealOnLouder.SelectedIndex == 1;
                        TextFiles.WriteSfxFile(platfilePath, sfxcustomFileData);
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
