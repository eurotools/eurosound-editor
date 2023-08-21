using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_SFX_Parameters : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_SFX_Parameters()
        {
            InitializeComponent();

            //Reverb Track Bar
            txtReverbSend.DataBindings.Add(new Binding("Text", TrackBar_ReverbSend, "Value"));
            TrackBar_ReverbSend.DataBindings.Add(new Binding("Value", txtReverbSend, "Text"));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void LoadData(SFX sfxFile)
        {
            //SFX Parameters
            TrackBar_ReverbSend.Value = Math.Min(Math.Max(TrackBar_ReverbSend.Minimum, sfxFile.Parameters.ReverbSend), TrackBar_ReverbSend.Maximum);
            nudMasterVolume.Value = Math.Min(Math.Max(nudMasterVolume.Minimum, sfxFile.Parameters.MasterVolume), nudMasterVolume.Maximum);
            switch (sfxFile.Parameters.TrackingType)
            {
                case 0:
                    RadiobtnTrackingType_2D.Checked = true;
                    break;
                case 1:
                    RadiobtnTrackingType_Amb.Checked = true;
                    break;
                case 2:
                    RadiobtnTrackingType_3D.Checked = true;
                    break;
                case 3:
                    RadiobtnTrackingType_3DRndPos.Checked = true;
                    break;
                case 4:
                    RadiobtnTrackingType_2DPL2.Checked = true;
                    break;
            }
            Trackbar_InnerRadius.Value = Math.Min(Math.Max(Trackbar_InnerRadius.Minimum, sfxFile.Parameters.InnerRadius), Trackbar_InnerRadius.Maximum);
            Trackbar_OuterRadius.Value = Math.Min(Math.Max(Trackbar_OuterRadius.Minimum, sfxFile.Parameters.OuterRadius), Trackbar_OuterRadius.Maximum);
            nudMaxVoice.Value = Math.Min(Math.Max(nudMaxVoice.Minimum, sfxFile.Parameters.MaxVoices), nudMaxVoice.Maximum);
            if (sfxFile.Parameters.Action1 == 0)
            {
                RadiobtnSteal.Checked = true;
            }
            else
            {
                RadiobtnReject.Checked = true;
            }
            nudPriority.Value = Math.Min(Math.Max(nudPriority.Minimum, sfxFile.Parameters.Priority), nudPriority.Maximum);
            chkStealOnLouder.Checked = sfxFile.Parameters.StealOnAge;
            nudDucker.Value = Math.Min(Math.Max(nudDucker.Minimum, sfxFile.Parameters.Ducker), nudDucker.Maximum);
            nudDuckerLength.Value = Math.Min(Math.Max(nudDuckerLength.Minimum, sfxFile.Parameters.DuckerLength), nudDuckerLength.Maximum);
            chkUnderWater.Checked = sfxFile.Parameters.Outdoors;
            chkPauseInstant.Checked = sfxFile.Parameters.PauseInstant;
            chkUnPausable.Checked = sfxFile.Parameters.UnPausable;
            chkMusicType.Checked = sfxFile.Parameters.MusicType;
            chkOneInstancePerFrame.Checked = sfxFile.Parameters.OneInstancePerFrame;
            chkKillMeOwnGroup.Checked = sfxFile.Parameters.KillMeOwnGroup;
            chkIgnoreMasterVolume.Checked = sfxFile.Parameters.IgnoreMasterVolume;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Trackbar_InnerRadius_ValueChanged(object sender, EventArgs e)
        {
            if (Trackbar_InnerRadius.Value > Trackbar_OuterRadius.Value)
            {
                Trackbar_OuterRadius.Value = Trackbar_InnerRadius.Value;
            }
            txtInnerRadius.Text = Trackbar_InnerRadius.Value.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Trackbar_OuterRadius_ValueChanged(object sender, EventArgs e)
        {
            if (Trackbar_OuterRadius.Value < Trackbar_InnerRadius.Value)
            {
                Trackbar_InnerRadius.Value = Trackbar_OuterRadius.Value;
            }
            txtOuterRadius.Text = Trackbar_OuterRadius.Value.ToString();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtReverbSend_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtReverbSend.Text, out _))
            {
                txtReverbSend.Text = "0";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtInnerRadius_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(txtInnerRadius.Text, out int userValue))
            {
                if (userValue > Trackbar_OuterRadius.Value)
                {
                    txtInnerRadius.Text = Trackbar_OuterRadius.Value.ToString();
                    Trackbar_InnerRadius.Value = Trackbar_OuterRadius.Value;
                    SystemSounds.Beep.Play();
                }
                else if (userValue < Trackbar_InnerRadius.Maximum)
                {
                    Trackbar_InnerRadius.Value = userValue;
                }
                else
                {
                    Trackbar_InnerRadius.Value = Trackbar_InnerRadius.Maximum;
                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                txtInnerRadius.Text = "0";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtOuterRadius_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(txtOuterRadius.Text, out int userValue))
            {
                if (userValue < Trackbar_InnerRadius.Value)
                {
                    txtOuterRadius.Text = Trackbar_InnerRadius.Value.ToString();
                    Trackbar_OuterRadius.Value = Trackbar_InnerRadius.Value;
                    SystemSounds.Beep.Play();
                }
                else if (userValue < Trackbar_OuterRadius.Maximum)
                {
                    Trackbar_OuterRadius.Value = userValue;
                }
                else
                {
                    Trackbar_OuterRadius.Value = Trackbar_OuterRadius.Maximum;
                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                txtOuterRadius.Text = "0";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkStealOnLouder_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStealOnLouder.Checked)
            {
                if (((SFXForm)Parent.Parent).UserControl_SamplePool.nudRandomVolume.Value != 0)
                {
                    MessageBox.Show("Steal On Louder & Random Volume NOT allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    chkStealOnLouder.Checked = false;
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
