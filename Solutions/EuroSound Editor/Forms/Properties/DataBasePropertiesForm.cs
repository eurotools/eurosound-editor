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
    public partial class DataBasePropertiesForm : Form
    {
        private readonly string DataBaseFilePath;

        //-------------------------------------------------------------------------------------------------------------------------------
        public DataBasePropertiesForm(string filePath)
        {
            InitializeComponent();
            DataBaseFilePath = filePath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_DataBaseProperties_Load(object sender, System.EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                //Load file
                DataBase dbData = TextFiles.ReadDataBaseFile(DataBaseFilePath);

                //Show info
                lblDataBaseName_Value.Text = string.Format("'{0}'", Path.GetFileNameWithoutExtension(DataBaseFilePath));
                lblFileInfo1.Text = string.Format("{0} :", dbData.bankInfo1.TrimStart('#'));
                lblFileInfo2.Text = string.Format("{0} :", dbData.bankInfo2.TrimStart('#'));
                lblFileInfo3.Text = string.Format("{0} :", dbData.bankInfo3.TrimStart('#'));
                lblFileInfo4.Text = string.Format("{0} :", dbData.bankInfo4.TrimStart('#'));
                lblFileInfo1_Value.Text = dbData.FirstCreated.Equals(DateTime.MinValue) ? string.Empty : dbData.FirstCreated.ToString(GlobalPrefs.FilesDateFormat);
                lblFileInfo2_Value.Text = dbData.CreatedBy.ToString();
                lblFileInfo3_Value.Text = dbData.LastModified.Equals(DateTime.MinValue) ? string.Empty : dbData.LastModified.ToString(GlobalPrefs.FilesDateFormat);
                lblFileInfo4_Value.Text = dbData.ModifiedBy.ToString();

                //Temporal
                lblDatabaseCount_Value.Text = "10";
                lblSFXCount_Value.Text = "77";
                lblSampleCount_Value.Text = "63";
                lblTotalSampleSize_Value.Text = "44.3 (MB) 46,451,917 bytes)";

                //Print total SFXs
                lstSFXs.BeginUpdate();
                lstSFXs.Items.AddRange(dbData.SFXs);
                lstSFXs.EndUpdate();
                lblSFXsCount.Text = string.Format("Total: {0}", lstSFXs.Items.Count);

                //Get All Samples
                HashSet<string> Samples = new HashSet<string>();
                for (int i = 0; i < dbData.SFXs.Length; i++)
                {
                    string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", dbData.SFXs[i] + ".txt");
                    if (File.Exists(filePath))
                    {
                        string[] fileData = File.ReadAllLines(filePath);
                        int j = Array.IndexOf(fileData, "#SFXSamplePoolFiles") + 1;
                        if (j > 0)
                        {
                            string currentLine = fileData[j++];
                            while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                            {
                                Samples.Add(Path.Combine(projectSettings.SampleFilesFolder, "Master", currentLine).ToUpper());
                                currentLine = fileData[j++];
                            }
                        }
                    }
                }

                //Print Samples
                lstTotalSamples.BeginUpdate();
                lstTotalSamples.Items.AddRange(Samples.ToArray());
                lstTotalSamples.EndUpdate();
                lblSamplesCount.Text = string.Format("Total: {0}", lstTotalSamples.Items.Count);

                //Get Dependencies
                string soundBanksPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");
                if (Directory.Exists(soundBanksPath))
                {
                    lstDependencies.BeginUpdate();
                    string dataBaseName = Path.GetFileNameWithoutExtension(DataBaseFilePath);
                    IEnumerable<string> soundbankFiles = Directory.EnumerateFiles(soundBanksPath, "*.txt", SearchOption.TopDirectoryOnly);
                    foreach (string soundBankPath in soundbankFiles)
                    {
                        string[] fileData = File.ReadAllLines(soundBankPath);
                        if (Array.IndexOf(fileData, dataBaseName) > 0)
                        {
                            lstDependencies.Items.Add(Path.GetFileNameWithoutExtension(soundBankPath));
                        }
                    }
                    lstDependencies.EndUpdate();
                    lblDataBase_Dependencies.Text = string.Format("SoundBank File Dependencies: {0}", lstDependencies.Items.Count);
                    lblDependenciesCount.Text = string.Format("Total: {0}", lstDependencies.Items.Count);
                }

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
