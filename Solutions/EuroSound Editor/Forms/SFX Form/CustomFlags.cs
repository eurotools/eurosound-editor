using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sb_editor.Forms.SFX_Form
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class CustomFlags : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public CustomFlags()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CustomFlags_Load(object sender, EventArgs e)
        {
            string iniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(iniFilePath))
            {
                IniFile iniFile = new IniFile(iniFilePath);
                string filePath = iniFile.Read("ListPath", "UserFlags");
                if (File.Exists(filePath))
                {
                    txtLabelsPath.Text = filePath;
                    ReadLabelsList(filePath);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSearchList_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtLabelsPath.Text = openFileDialog.FileName;
                ReadLabelsList(openFileDialog.FileName);

                //Save State
                IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
                iniFile.Write("ListPath", openFileDialog.FileName, "UserFlags");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ReadLabelsList(string filePath)
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                int counter = 0;
                string ln;
                while (((ln = file.ReadLine()) != null) && counter < 16)
                {
                    chkListFlags.Items[counter] = ln;
                    counter++;
                }
                file.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void SetFlags(int flags)
        {
            for (int i = 0; i < 32; i++)
            {
                if (Convert.ToBoolean((flags >> i) & 1))
                {
                    chkListFlags.SetItemCheckState(i, CheckState.Checked);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal ushort GetFlags()
        {
            int CheckedFlagsResult = 0;
            for (int i = 0; i < 16; i++)
            {
                if (chkListFlags.GetItemCheckState(i) == CheckState.Checked)
                {
                    CheckedFlagsResult |= 1 << i;
                }
                if (chkListFlags.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    CheckedFlagsResult &= ~(1 << i);
                }
            }

            return (ushort)CheckedFlagsResult;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
