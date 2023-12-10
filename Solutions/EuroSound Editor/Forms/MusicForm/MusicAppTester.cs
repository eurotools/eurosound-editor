//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Music Tester APP
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using ExMarkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MusicApp : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRunTarget_Click(object sender, EventArgs e)
        {
            //Get file path
            string outputPlatform = cboOutputFormat.SelectedItem.ToString();
            if (outputPlatform.Equals("ALL", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Select a valid output target.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string outputFolder = CommonFunctions.GetSoundbankOutPath(projectSettings, outputPlatform, string.Empty, true);
                string fileName = string.Format("HCE{0:X5}.SFX", int.Parse(lvwMusicFiles.SelectedItems[0].SubItems[3].Text));
                string outputPath = Path.Combine(outputFolder, fileName);

                if (File.Exists(outputPath))
                {
                    pcDll.LoadMusicBank(outputPath, outputPlatform);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            pcDll.PlayMusicBank();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStop_Click(object sender, EventArgs e)
        {
            pcDll.StopMusicPlayer();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPause_Click(object sender, EventArgs e)
        {
            pcDll.PauseMusicPlayer();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnJump_Click(object sender, EventArgs e)
        {
            MarkerFilesFunctions mkFunctions = new MarkerFilesFunctions();

            //Read Markers File
            string jumpMarkers = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".mrk");
            List<MarkerInfo> markersData = mkFunctions.LoadTextMarkerFile(jumpMarkers, null, null, true);

            //Call DLL
            decimal position = CalculusLoopOffset.RuleOfThreeLoopOffset(44100, 32000, (int)markersData[lstbx_JumpMakers.SelectedIndex].Position);
            pcDll.JumpToMarker((uint)position, 32000);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
