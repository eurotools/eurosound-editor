using ESUtils;
using EuroSound_Editor.Classes;
using EuroSound_Editor.Objects;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SoundBankPropertiesForm : Form
    {
        private readonly string outputFormat;
        private readonly string outputLanguage;
        private readonly string soundBankFile;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SoundBankPropertiesForm(string soundBankPath, string formatToShow, string language)
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
            SamplePool samplePool = TextFiles.ReadSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"));
            SoundBank soundBankData = TextFiles.ReadSoundbankFile(soundBankFile);

            string[] SFXs = sbFunctions.GetSFXs(soundBankData.DataBases, outputFormat);
            string[] samples = sbFunctions.GetSampleList(SFXs, outputLanguage);

            //Info to the GUI
            grbCurrentSoundbank.Text = Path.GetFileNameWithoutExtension(soundBankFile);
            lblBankInfo1.Text = string.Format("{0} :", soundBankData.HeaderData.bankInfo1.TrimStart('#'));
            lblBankInfo2.Text = string.Format("{0} :", soundBankData.HeaderData.bankInfo2.TrimStart('#'));
            lblBankInfo3.Text = string.Format("{0} :", soundBankData.HeaderData.bankInfo3.TrimStart('#'));
            lblBankInfo4.Text = string.Format("{0} :", soundBankData.HeaderData.bankInfo4.TrimStart('#'));
            if (!soundBankData.HeaderData.FirstCreated.Equals(DateTime.MinValue))
            {
                lblBankInfo1_Value.Text = soundBankData.HeaderData.FirstCreated.ToString(GlobalPrefs.FilesDateFormat);
            }
            lblBankInfo2_Value.Text = soundBankData.HeaderData.CreatedBy.ToString();
            if (!soundBankData.HeaderData.LastModified.Equals(DateTime.MinValue))
            {
                lblBankInfo3_Value.Text = soundBankData.HeaderData.LastModified.ToString(GlobalPrefs.FilesDateFormat);
            }
            lblBankInfo4_Value.Text = soundBankData.HeaderData.ModifiedBy.ToString();
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

            //Samples
            lstSamples.BeginUpdate();
            for (int i = 0; i < samples.Length; i++)
            {
                string samplesFolder = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");
                lstSamples.Items.Add(string.Join("\\", samplesFolder, samples[i]).ToUpper());
            }
            lstSamples.EndUpdate();
            lblSoundBankSampleCount.Text = string.Format("Samples: {0}", lstSamples.Items.Count);

            // SoundBank size
            for (int i = 0; i < GlobalPrefs.CurrentProject.platformData.Count; i++)
            {
                string currentFormat = GlobalPrefs.CurrentProject.platformData.ElementAt(i).Key;
                string temporalFiles = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", currentFormat, "SoundBanks", outputLanguage, soundBankData.HashCode + ".sbf");
                string fileSize;
                if (File.Exists(temporalFiles))
                {
                    fileSize = BytesFunctions.FormatBytes(new FileInfo(temporalFiles).Length);
                }
                else
                {
                    fileSize = string.Format("{0} - ESTIMATED", BytesFunctions.FormatBytes(sbFunctions.GetEstimatedOutputFileSize(samples, samplePool, currentFormat)));
                }

                //Show value
                Control[] formatNameLabels = grbCurrentSoundbank.Controls.Find("lblFormatName" + i.ToString(), false);
                if (formatNameLabels.Length > 0)
                {
                    ((Label)formatNameLabels[0]).Text = GlobalPrefs.CurrentProject.platformData.ElementAt(i).Key;
                }

                Control[] formatValueLabels = grbCurrentSoundbank.Controls.Find("lblFormatInfo" + i.ToString(), false);
                if (formatValueLabels.Length > 0)
                {
                    ((Label)formatValueLabels[0]).Text = fileSize;
                }
            }

            long sampleSize = sbFunctions.GetSampleSize(Path.Combine(GlobalPrefs.ProjectFolder, "Master"), samplePool, samples);
            lblTotalSampleSize_Value.Text = BytesFunctions.FormatBytes(sampleSize);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
