using sb_editor.Objects;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class NewSamplesFound : Form
    {
        private readonly string[] SamplesArray;
        private readonly SamplePool samplesData;
        private readonly ProjProperties projectSettings;

        //-------------------------------------------------------------------------------------------------------------------------------
        public NewSamplesFound(string[] itemsArray, SamplePool samples)
        {
            InitializeComponent();
            SamplesArray = itemsArray;
            samplesData = samples;

            //Read Project Settings
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NewSamplesFound_Load(object sender, EventArgs e)
        {
            //Add available sample rates
            cboAvailableRates.BeginUpdate();
            cboAvailableRates.Items.AddRange(projectSettings.ResampleRates.ToArray());
            if (cboAvailableRates.Items.Count > 0)
            {
                cboAvailableRates.SelectedIndex = 0;
            }
            cboAvailableRates.EndUpdate();

            //Print Samples
            lstSamplesList.BeginUpdate();
            lstSamplesList.Items.AddRange(SamplesArray);
            lstSamplesList.EndUpdate();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NewSamplesFound_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Add New Samples
            for (int i = 0; i < SamplesArray.Length; i++)
            {
                if (!samplesData.SamplePoolItems.ContainsKey(SamplesArray[i]))
                {
                    string sampleFullPath = Path.Combine(projectSettings.SampleFilesFolder, "Master", SamplesArray[i].TrimStart(Path.DirectorySeparatorChar));
                    SamplePoolItem newSamples = new SamplePoolItem
                    {
                        ReSampleRate = cboAvailableRates.SelectedItem.ToString(),
                        Size = CommonFunctions.GetSampleSize(sampleFullPath),
                        Date = CommonFunctions.GetSampleDate(sampleFullPath),
                        ReSample = true
                    };
                    samplesData.SamplePoolItems.Add(SamplesArray[i], newSamples);
                }
            }

            //Sort Dictionary
            samplesData.SamplePoolItems = samplesData.SamplePoolItems.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

            //Save File
            TextFiles.WriteSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"), samplesData);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
