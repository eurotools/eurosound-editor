using EuroSound_Editor.Forms;
using EuroSound_Editor.Objects;
using System;
using System.Windows.Forms;

namespace EuroSound_Editor.Panels
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

            //Inner Radius Track Bar
            txtInnerRadius.DataBindings.Add(new Binding("Text", Trackbar_InnerRadius, "Value"));
            Trackbar_InnerRadius.DataBindings.Add(new Binding("Value", txtInnerRadius, "Text"));

            //Reverb Track Bar
            txtOuterRadius.DataBindings.Add(new Binding("Text", Trackbar_OuterRadius, "Value"));
            Trackbar_OuterRadius.DataBindings.Add(new Binding("Value", txtOuterRadius, "Text"));
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
            nudAlertness.Value = Math.Min(Math.Max(nudAlertness.Minimum, sfxFile.Parameters.Alertness), nudAlertness.Maximum);
            chkStealOnLouder.Checked = sfxFile.Parameters.StealOnAge;
            nudDucker.Value = Math.Min(Math.Max(nudDucker.Minimum, sfxFile.Parameters.Ducker), nudDucker.Maximum);
            nudDuckerLength.Value = Math.Min(Math.Max(nudDuckerLength.Minimum, sfxFile.Parameters.DuckerLength), nudDuckerLength.Maximum);
            chkUnderWater.Checked = sfxFile.Parameters.Outdoors;
            chkPauseInNis.Checked = sfxFile.Parameters.PauseInNis;
            chkIgnoreAge.Checked = sfxFile.Parameters.IgnoreAge;
            chkMusicType.Checked = sfxFile.Parameters.MusicType;
            chkDoppler.Checked = sfxFile.Parameters.Doppler;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Trackbar_InnerRadius_ValueChanged(object sender, EventArgs e)
        {
            if (Trackbar_InnerRadius.Value > Trackbar_OuterRadius.Value)
            {
                Trackbar_OuterRadius.Value = Trackbar_InnerRadius.Value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Trackbar_OuterRadius_ValueChanged(object sender, EventArgs e)
        {
            if (Trackbar_OuterRadius.Value < Trackbar_InnerRadius.Value)
            {
                Trackbar_InnerRadius.Value = Trackbar_OuterRadius.Value;
            }
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
        private void TxtInnerRadius_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtInnerRadius.Text, out int userValue))
            {
                if (userValue > Trackbar_InnerRadius.Maximum)
                {
                    txtInnerRadius.Text = Trackbar_InnerRadius.Maximum.ToString();
                }
                if (userValue > Trackbar_OuterRadius.Value)
                {
                    txtInnerRadius.Text = Trackbar_OuterRadius.Value.ToString();
                }
            }
            else
            {
                txtInnerRadius.Text = "0";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtOuterRadius_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtOuterRadius.Text, out int userValue))
            {
                if (userValue > Trackbar_OuterRadius.Maximum)
                {
                    txtOuterRadius.Text = Trackbar_OuterRadius.Maximum.ToString();
                }
                if (userValue < Trackbar_InnerRadius.Value)
                {
                    txtOuterRadius.Text = Trackbar_InnerRadius.Value.ToString();
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
