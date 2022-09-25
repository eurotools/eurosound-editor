using EuroSound_Editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SetMaxBankSizeForm : Form
    {
        private readonly string sbFilePath;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SetMaxBankSizeForm(string soundBankFilePath)
        {
            InitializeComponent();
            sbFilePath = soundBankFilePath;
            txtBankName.Text = Path.GetFileNameWithoutExtension(soundBankFilePath);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (nudPlayStation.Value != 0 || nudPC.Value != 0 || nudGameCube.Value != 0 || nudXbox.Value != 0)
            {
                if (File.Exists(sbFilePath))
                {
                    SoundBank sbData = TextFiles.ReadSoundbankFile(sbFilePath);
                    sbData.PlayStationSize = (uint)nudPlayStation.Value;
                    sbData.PCSize = (uint)nudPC.Value;
                    sbData.GameCubeSize = (uint)nudGameCube.Value;
                    sbData.XboxSize = (uint)nudXbox.Value;
                    TextFiles.WriteSoundBankFile(sbFilePath, sbData, true);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
