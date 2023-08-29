//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Sample Pool Control Panel
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_SamplePoolControl : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_SamplePoolControl()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void LoadData(SFX sfxFile)
        {
            //Set the looping property
            chkLoop.Checked = sfxFile.SamplePool.isLooped;

            //Set the maximum and minimum delay value
            nudMaxDelay.Value = Math.Min(Math.Max(nudMaxDelay.Minimum, sfxFile.SamplePool.MaxDelay), nudMaxDelay.Maximum);
            nudMinDelay.Value = Math.Min(Math.Max(nudMinDelay.Minimum, sfxFile.SamplePool.MinDelay), nudMinDelay.Maximum);

            //Set the action radio button
            if (sfxFile.SamplePool.Action1 == 0)
            {
                rdoSingle.Checked = true;
            }
            else
            {
                rdoMultiSample.Checked = true;
            }

            //Update properties values
            chkRandomPick.Checked = sfxFile.SamplePool.RandomPick;
            chkShuffled.Checked = sfxFile.SamplePool.Shuffled;
            chkPolyphonic.Checked = sfxFile.SamplePool.Polyphonic;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void RdoSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSingle.Checked)
            {
                //Update checkboxes
                chkShuffled.Enabled = false;
                chkPolyphonic.Enabled = false;

                //Check random pick
                if (((SFXForm)Parent.Parent).UserControl_SamplePool.lstSamples.Items.Count > 1)
                {
                    chkRandomPick.Checked = true;
                }

                //Update min value
                nudMinDelay.Minimum = 0;
                nudMaxDelay.Minimum = 0;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void RdoMultiSample_CheckedChanged(object sender, EventArgs e)
        {
            // Check if the "MultiSample" radio button is checked
            if (rdoMultiSample.Checked)
            {
                // Uncheck the "Random Pick" checkbox and enable the "Shuffled" and "Polyphonic" checkboxes
                chkRandomPick.Checked = false;
                chkShuffled.Enabled = true;
                chkPolyphonic.Enabled = true;

                // Set the minimum value for the "Minimum Delay" and "Maximum Delay" numeric updown controls to -32000
                nudMinDelay.Minimum = -32000;
                nudMaxDelay.Minimum = -32000;
            }
            else
            {
                // If the "MultiSample" radio button is not checked, check if the value of either the "Minimum Delay" or "Maximum Delay" is less than 0
                if (nudMinDelay.Value < 0 || nudMaxDelay.Value < 0)
                {
                    // If either value is less than 0, show an error message and set the "MultiSample" radio button to be checked
                    MessageBox.Show("Inter Sample Delay cannot be -ve", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    rdoMultiSample.Checked = true;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkShuffled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShuffled.Checked)
            {
                chkPolyphonic.Checked = false;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkPolyphonic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPolyphonic.Checked)
            {
                chkShuffled.Checked = false;
            }
        }

        //*===============================================================================================
        //* SFX Defaults Control
        //*===============================================================================================
        private void NudPitchOffset_ValueChanged(object sender, EventArgs e)
        {
            if (nudPitchOffset.Value > 0)
            {
                IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
                systemIni.Write("DTextNIndex_0", nudPitchOffset.Value.ToString(GlobalPrefs.NumericProvider), "SFXForm");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudRandomPitchOffset_ValueChanged(object sender, EventArgs e)
        {
            IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            systemIni.Write("DTextNIndex_1", nudRandomPitchOffset.Value.ToString(GlobalPrefs.NumericProvider), "SFXForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudBaseVolume_ValueChanged(object sender, EventArgs e)
        {
            IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            systemIni.Write("DTextNIndex_2", nudBaseVolume.Value.ToString(), "SFXForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudRandomVolume_ValueChanged(object sender, EventArgs e)
        {
            IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            systemIni.Write("DTextNIndex_3", nudRandomVolume.Value.ToString(""), "SFXForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudPan_ValueChanged(object sender, EventArgs e)
        {
            IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            systemIni.Write("DTextNIndex_4", nudPan.Value.ToString(), "SFXForm");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudRandomPan_ValueChanged(object sender, EventArgs e)
        {
            IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            systemIni.Write("DTextNIndex_5", nudRandomPan.Value.ToString(), "SFXForm");
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
