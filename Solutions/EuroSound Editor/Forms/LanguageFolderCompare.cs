//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Language Folder Comparer
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class LanguageFolderCompare : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public LanguageFolderCompare()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSetPrimaryFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtPrimaryPath.Text = folderBrowser.SelectedPath;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSetSecondaryFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtSecondaryPath.Text = folderBrowser.SelectedPath;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDoCompare_Click(object sender, EventArgs e)
        {
            //Clear Textbox
            txtAdditionFilesSecondary.Clear();
            txtMissingFilesSecondary.Clear();

            //Get Files
            string[] primaryFolderFiles = Directory.GetFiles(txtPrimaryPath.Text, "*.wav", SearchOption.AllDirectories);
            string[] secondaryFolderFiles = Directory.GetFiles(txtSecondaryPath.Text, "*.wav", SearchOption.AllDirectories);

            //Get Relative Paths
            for (int i = 0; i < primaryFolderFiles.Length; i++)
            {
                primaryFolderFiles[i] = primaryFolderFiles[i].Substring(txtPrimaryPath.Text.Length);
            }
            for (int i = 0; i < secondaryFolderFiles.Length; i++)
            {
                secondaryFolderFiles[i] = secondaryFolderFiles[i].Substring(txtSecondaryPath.Text.Length);
            }

            //Missing Files in Secondary Path
            string[] missingInSecondaryPath = primaryFolderFiles.Except(secondaryFolderFiles).ToArray();
            Array.Sort(missingInSecondaryPath);
            for (int i = 0; i < missingInSecondaryPath.Length; i++)
            {
                txtMissingFilesSecondary.Text += missingInSecondaryPath[i] + Environment.NewLine;
            }

            //Addition FIles in Secondary Path
            string[] additionInSecondaryPath = secondaryFolderFiles.Except(primaryFolderFiles).ToArray();
            Array.Sort(additionInSecondaryPath);
            for (int i = 0; i < additionInSecondaryPath.Length; i++)
            {
                txtAdditionFilesSecondary.Text += additionInSecondaryPath[i] + Environment.NewLine;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
