//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Missing Samples Form
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MissingSamplesFound : Form
    {
        private readonly string[] SamplesArray;
        private readonly SamplePool samplesData;

        //-------------------------------------------------------------------------------------------------------------------------------
        public MissingSamplesFound(string[] itemsArray, SamplePool samples)
        {
            InitializeComponent();
            SamplesArray = itemsArray;
            samplesData = samples;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MissingSamplesFound_Load(object sender, EventArgs e)
        {
            lstSamplesList.BeginUpdate();
            lstSamplesList.Items.AddRange(SamplesArray);
            lstSamplesList.EndUpdate();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MissingSamplesFound_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Remove items
            for (int i = 0; i < SamplesArray.Length; i++)
            {
                if (samplesData.SamplePoolItems.ContainsKey(SamplesArray[i]))
                {
                    samplesData.SamplePoolItems.Remove(SamplesArray[i]);
                }
            }

            //Save File
            TextFiles.WriteSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"), samplesData);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
