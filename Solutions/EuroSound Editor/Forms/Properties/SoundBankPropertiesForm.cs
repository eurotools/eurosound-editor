//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SoundBank Properties Form
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ESUtils.Enumerations;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SoundBankPropertiesForm : Form
    {
        private readonly string outputFormat;
        private readonly Language outputLanguage;
        private readonly string soundBankFile;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SoundBankPropertiesForm(string soundBankPath, string formatToShow, Language language)
        {
            InitializeComponent();
            outputFormat = formatToShow;
            outputLanguage = language;
            soundBankFile = soundBankPath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_Soundbank_Properties_Load(object sender, System.EventArgs e)
        {
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            // Load TXT file data
            LoadData(soundBankFile);

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
#if DEBUG
            btn_SaveSFXs.Visible = true;
#else
        btn_SaveSFXs.Visible = false;
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_Soundbank_Properties_Shown(object sender, System.EventArgs e)
        {
            if (ActiveForm != this)
            {
                FlashWindow.FlashWindowAPI(Handle);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LoadData(string soundBankFile)
        {
            SoundBankFunctions sbFunctions = new SoundBankFunctions();
            SoundBank soundBankData = TextFiles.ReadSoundbankFile(soundBankFile);
            SamplePool samplePool = new SamplePool();
            string samplePoolFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt");
            string[] SFXs = sbFunctions.GetSFXs(soundBankData.DataBases, outputFormat);
            string[] samples = sbFunctions.GetSampleList(SFXs, outputLanguage);
            if (File.Exists(samplePoolFile))
            {
                samplePool = TextFiles.ReadSamplesFile(samplePoolFile);
            }

            //Info to the GUI
            grbCurrentSoundbank.Text = Path.GetFileNameWithoutExtension(soundBankFile);
            lblBankInfo1.Text = string.Format("{0} :", soundBankData.bankInfo1.TrimStart('#'));
            lblBankInfo2.Text = string.Format("{0} :", soundBankData.bankInfo2.TrimStart('#'));
            lblBankInfo3.Text = string.Format("{0} :", soundBankData.bankInfo3.TrimStart('#'));
            lblBankInfo4.Text = string.Format("{0} :", soundBankData.bankInfo4.TrimStart('#'));
            if (!soundBankData.FirstCreated.Equals(DateTime.MinValue))
            {
                lblBankInfo1_Value.Text = soundBankData.FirstCreated.ToString(GlobalPrefs.FilesDateFormat);
            }
            lblBankInfo2_Value.Text = soundBankData.CreatedBy.ToString();
            if (!soundBankData.LastModified.Equals(DateTime.MinValue))
            {
                lblBankInfo3_Value.Text = soundBankData.LastModified.ToString(GlobalPrefs.FilesDateFormat);
            }
            lblBankInfo4_Value.Text = soundBankData.ModifiedBy.ToString();
            lblDatabaseCount_Value.Text = soundBankData.DataBases.Length.ToString();
            lblSFXCount_Value.Text = SFXs.Length.ToString();
            lblSampleCount_Value.Text = samples.Length.ToString();
            lblOutputFileName_Value.Text = string.Join(string.Empty, "HC", soundBankData.HashCode.ToString("X6"), ".SFX");

            //Databases
            lstDatabases.BeginUpdate();
            lstDatabases.Items.AddRange(soundBankData.DataBases);
            lstDatabases.EndUpdate();
            lblDatabasesCount.Text = string.Format("DataBases: {0}", lstDatabases.Items.Count);

            //SFXs
            lstSFXs.BeginUpdate();
            lstSFXs.Items.AddRange(SFXs);
            lstSFXs.EndUpdate();
            lblSFX_Count.Text = string.Format("SFXs: {0}", lstSFXs.Items.Count);


            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                //Samples
                lstSamples.BeginUpdate();
                for (int i = 0; i < samples.Length; i++)
                {
                    string samplesFolder = Path.Combine(projectSettings.SampleFilesFolder, "Master");
                    lstSamples.Items.Add(string.Join("\\", samplesFolder, samples[i]).ToUpper());
                }
                lstSamples.EndUpdate();
                lblSoundBankSampleCount.Text = string.Format("Samples: {0}", lstSamples.Items.Count);

                // SoundBank size
                for (int i = 0; i < projectSettings.platformData.Count; i++)
                {
                    string currentFormat = projectSettings.platformData.ElementAt(i).Key;
                    string temporalFiles = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", currentFormat, "SoundBanks", outputLanguage.ToString(), soundBankData.HashCode + ".sbf");
                    string fileSize;
                    if (File.Exists(temporalFiles))
                    {
                        fileSize = BytesFunctions.FormatBytes(new FileInfo(temporalFiles).Length);
                    }
                    else
                    {
                        fileSize = string.Format("{0} - ESTIMATED", BytesFunctions.FormatBytes(sbFunctions.GetEstimatedOutputFileSize(projectSettings, samples, samplePool, currentFormat)));
                    }

                    //Show value
                    Control[] formatNameLabels = grbCurrentSoundbank.Controls.Find("lblFormatName" + i.ToString(), false);
                    if (formatNameLabels.Length > 0)
                    {
                        ((Label)formatNameLabels[0]).Visible = true;
                        ((Label)formatNameLabels[0]).Text = projectSettings.platformData.ElementAt(i).Key;
                    }

                    Control[] formatValueLabels = grbCurrentSoundbank.Controls.Find("lblFormatInfo" + i.ToString(), false);
                    if (formatValueLabels.Length > 0)
                    {
                        ((Label)formatValueLabels[0]).Visible = true;
                        ((Label)formatValueLabels[0]).Text = fileSize;
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("Project Properties File Not Found {0}", projectPropertiesFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            long sampleSize = sbFunctions.GetSampleSize(Path.Combine(GlobalPrefs.ProjectFolder, "Master"), samplePool, samples);
            lblTotalSampleSize_Value.Text = BytesFunctions.FormatBytes(sampleSize);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void btn_SaveSFXs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Create text file
                using (StreamWriter writer = new StreamWriter(File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.UTF8))
                {
                    for (int i = 0; i < lstSFXs.Items.Count; i++)
                    {
                        writer.WriteLine(lstSFXs.Items[i]);
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
