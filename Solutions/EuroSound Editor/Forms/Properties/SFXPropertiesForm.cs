﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFX Properties Form
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SFXPropertiesForm : Form
    {
        private readonly string SfxFilePath;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SFXPropertiesForm(string filePath)
        {
            InitializeComponent();
            SfxFilePath = filePath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_SfxProperties_Load(object sender, System.EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            //Load file
            SFX sfxData = TextFiles.ReadSfxFile(SfxFilePath);

            //Show info
            lblDataBaseName_Value.Text = string.Format("'{0}'", Path.GetFileNameWithoutExtension(SfxFilePath));
            lblFileInfo1.Text = string.Format("{0} :", sfxData.bankInfo1.TrimStart('#'));
            lblFileInfo2.Text = string.Format("{0} :", sfxData.bankInfo2.TrimStart('#'));
            lblFileInfo3.Text = string.Format("{0} :", sfxData.bankInfo3.TrimStart('#'));
            lblFileInfo4.Text = string.Format("{0} :", sfxData.bankInfo4.TrimStart('#'));
            lblFileInfo1_Value.Text = sfxData.FirstCreated.Equals(DateTime.MinValue) ? string.Empty : sfxData.FirstCreated.ToString(GlobalPrefs.FilesDateFormat);
            lblFileInfo2_Value.Text = sfxData.CreatedBy.ToString();
            lblFileInfo3_Value.Text = sfxData.LastModified.Equals(DateTime.MinValue) ? string.Empty : sfxData.LastModified.ToString(GlobalPrefs.FilesDateFormat);
            lblFileInfo4_Value.Text = sfxData.ModifiedBy.ToString();

            //Temporal
            lblDatabaseCount_Value.Text = "10";
            lblSFXCount_Value.Text = "77";
            lblSampleCount_Value.Text = "63";
            lblTotalSampleSize_Value.Text = "44.3 (MB) 46,451,917 bytes)";

            //Print DataBase dependencies
            string[] dataBaseFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"), "*.txt", SearchOption.TopDirectoryOnly);
            HashSet<string> SfxDependencies = new HashSet<string>();
            for (int i = 0; i < dataBaseFiles.Length; i++)
            {
                string[] fileData = File.ReadAllLines(dataBaseFiles[i]);
                if (Array.IndexOf(fileData, Path.GetFileNameWithoutExtension(SfxFilePath)) > 0)
                {
                    SfxDependencies.Add(Path.GetFileNameWithoutExtension(dataBaseFiles[i]));
                }
            }

            //Add items to the listbox
            lstDataBases.BeginUpdate();
            string[] SfxDependenciesArray = SfxDependencies.ToArray();
            for (int i = 0; i < SfxDependenciesArray.Length; i++)
            {
                lstDataBases.Items.Add(SfxDependenciesArray[i]);
            }
            lstDataBases.EndUpdate();
            lblSfxDependencies.Text = string.Format("DataBase File Dependencies: {0}", lstDataBases.Items.Count);


            //Print Samples
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                lstSamples.BeginUpdate();
                for (int i = 0; i < sfxData.Samples.Count; i++)
                {
                    lstSamples.Items.Add(Path.Combine(projectSettings.SampleFilesFolder, "Master", sfxData.Samples[i].FilePath).ToUpper());
                }
                lstSamples.EndUpdate();
            }
            else
            {
                MessageBox.Show(string.Format("Project Properties File Not Found {0}", projectPropertiesFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
