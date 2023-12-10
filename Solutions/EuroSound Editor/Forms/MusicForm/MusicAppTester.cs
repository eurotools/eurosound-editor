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
            //Get target
            string outputPlatform = cboOutputFormat.SelectedItem.ToString();
            if (outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
            {
                outputPlatform = "Xbox";
            }

            //Start reading
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
                else
                {
                    MessageBox.Show(string.Format("Music File Not Found {0}", outputPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //Get marker files path
            string jumpMarkersFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".mrk");
            if (File.Exists(jumpMarkersFile))
            {
                //Read file data
                List<MarkerInfo> markersData = mkFunctions.LoadTextMarkerFile(jumpMarkersFile, null, null, true);

                //Calculate pos
                decimal position = CalculusLoopOffset.RuleOfThreeLoopOffset(44100, 32000, (int)markersData[lstbx_JumpMakers.SelectedIndex].Position);

                //Call DLL
                int freq = 32000;
                string outputPlatform = cboOutputFormat.SelectedItem.ToString();
                if (outputPlatform.Equals("X box", StringComparison.OrdinalIgnoreCase))
                {
                    position = (decimal)(markersData[lstbx_JumpMakers.SelectedIndex].Position / 0.98888887);
                    freq = 44100;
                }

                pcDll.JumpToMarker((uint)position, freq);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
