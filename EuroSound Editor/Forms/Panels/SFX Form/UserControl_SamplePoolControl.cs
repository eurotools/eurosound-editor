using EuroSound_Editor.Forms;
using EuroSound_Editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Editor.Panels
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
            //Show Data
            chkLoop.Checked = sfxFile.SamplePool.isLooped;
            nudMaxDelay.Value = Math.Min(Math.Max(nudMaxDelay.Minimum, sfxFile.SamplePool.MaxDelay), nudMaxDelay.Maximum);
            nudMinDelay.Value = Math.Min(Math.Max(nudMinDelay.Minimum, sfxFile.SamplePool.MinDelay), nudMinDelay.Maximum);
            if (sfxFile.SamplePool.Action1 == 0)
            {
                rdoSingle.Checked = true;
            }
            else
            {
                rdoMultiSample.Checked = true;
            }
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
            if (rdoMultiSample.Checked)
            {
                chkRandomPick.Checked = false;
                chkShuffled.Enabled = true;
                chkPolyphonic.Enabled = true;

                //Update min value
                nudMinDelay.Minimum = -32000;
                nudMaxDelay.Minimum = -32000;
            }
            else
            {
                //Inform user if required
                if (nudMinDelay.Value < 0 || nudMaxDelay.Value < 0)
                {
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
