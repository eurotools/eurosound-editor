using EuroSound_Editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Editor.Forms
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
            lblFileInfo1.Text = string.Format("{0} :", sfxData.HeaderData.bankInfo1.TrimStart('#'));
            lblFileInfo2.Text = string.Format("{0} :", sfxData.HeaderData.bankInfo2.TrimStart('#'));
            lblFileInfo3.Text = string.Format("{0} :", sfxData.HeaderData.bankInfo3.TrimStart('#'));
            lblFileInfo4.Text = string.Format("{0} :", sfxData.HeaderData.bankInfo4.TrimStart('#'));
            lblFileInfo1_Value.Text = sfxData.HeaderData.FirstCreated.Equals(DateTime.MinValue) ? string.Empty : sfxData.HeaderData.FirstCreated.ToString(GlobalPrefs.FilesDateFormat);
            lblFileInfo2_Value.Text = sfxData.HeaderData.CreatedBy.ToString();
            lblFileInfo3_Value.Text = sfxData.HeaderData.LastModified.Equals(DateTime.MinValue) ? string.Empty : sfxData.HeaderData.LastModified.ToString(GlobalPrefs.FilesDateFormat);
            lblFileInfo4_Value.Text = sfxData.HeaderData.ModifiedBy.ToString();

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
            lstSamples.BeginUpdate();
            for (int i = 0; i < sfxData.Samples.Count; i++)
            {
                lstSamples.Items.Add(Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", sfxData.Samples[i].FilePath).ToUpper());
            }
            lstSamples.EndUpdate();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
