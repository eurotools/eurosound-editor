//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Wave SMPL Chunk Manager
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Audio_Classes;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class FrmWaveLoops : Form
    {
        private readonly string outputFilePath;

        //-------------------------------------------------------------------------------------------------------------------------------
        public FrmWaveLoops(string waveFilePath)
        {
            InitializeComponent();
            outputFilePath = waveFilePath;
            Text = String.Format("{0} - Loop Settings", Path.GetFileNameWithoutExtension(waveFilePath));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void FrmWaveLoops_Load(object sender, EventArgs e)
        {
            //Read wave settings
            WaveFunctions wFunctions = new WaveFunctions();
            WavInfo smplChunk = wFunctions.ReadWaveProperties(outputFilePath);

            //Set sample chunk data (if there are)
            chkLoopSettings.Checked = smplChunk.HasLoop;
            nudStartLoop.Value = smplChunk.LoopStart;
            nudEndLoop.Value = smplChunk.LoopEnd;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void FrmWaveLoops_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Ask for confirmation
            if (DialogResult == DialogResult.Cancel)
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to close without saving?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (chkLoopSettings.Checked)
                {
                    try
                    {
                        //Get Values
                        int startLoop = (int)nudStartLoop.Value;
                        int endLoop = (int)nudEndLoop.Value;
                        if (chkToLastSample.Checked)
                        {
                            endLoop = -1;
                        }

                        //Write Wave File
                        WaveFunctions wFunctions = new WaveFunctions();
                        wFunctions.WriteSampleChunk(outputFilePath, startLoop, endLoop);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkLoopSettings_CheckedChanged(object sender, EventArgs e)
        {
            nudStartLoop.Enabled = chkLoopSettings.Checked;
            nudEndLoop.Enabled = chkLoopSettings.Checked;
            chkToLastSample.Enabled = chkLoopSettings.Checked;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkToLastSample_CheckedChanged(object sender, EventArgs e)
        {
            nudEndLoop.Enabled = !chkToLastSample.Checked;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
