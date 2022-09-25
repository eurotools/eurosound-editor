using sb_editor.Forms;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_RefineSFXList : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_RefineSFXList()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnUpdateList_Click(object sender, System.EventArgs e)
        {
            using (Frm_RefineList refineList = new Frm_RefineList())
            {
                refineList.ShowDialog();
            }
            UpdateCombobox(Path.Combine(GlobalPrefs.ProjectFolder, "System", "RefineSearch.txt"));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnShowAll_Click(object sender, System.EventArgs e)
        {
            //Update Selection
            if (cboWords.Items.Count > 0 && !cboWords.SelectedItem.Equals("All"))
            {
                cboWords.SelectedItem = "All";
            }
            else
            {
                UserControl_MainForm_AvailableSFX parentForm = (UserControl_MainForm_AvailableSFX)Parent.Parent;
                parentForm.LoadSFXs("All");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnUnUsed_Click(object sender, System.EventArgs e)
        {
            UserControl_MainForm_AvailableSFX availableSfxForm = (UserControl_MainForm_AvailableSFX)Parent.Parent;

            //Check if we need to reload
            if (!cboWords.SelectedItem.Equals("All"))
            {
                availableSfxForm.LoadSFXs("All");
            }

            //Get and select unused hashcodes
            string[] unusedSFXs = availableSfxForm.GetUnuSedHashCodes();
            if (availableSfxForm.chkIconView.Checked)
            {
                for (int i = 0; i < unusedSFXs.Length; i++)
                {
                    DataGridViewRow searchItem = availableSfxForm.FindDataGridRow(unusedSFXs[i]);
                    searchItem.Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < unusedSFXs.Length; i++)
                {
                    int itemToSelect = availableSfxForm.lstAvailableSFXs.FindStringExact(unusedSFXs[i]);
                    if (itemToSelect != ListBox.NoMatches)
                    {
                        availableSfxForm.lstAvailableSFXs.SelectedItem = availableSfxForm.lstAvailableSFXs.Items[itemToSelect];
                    }
                }
            }

            //Show highlighted hashcodes
            if (cboWords.SelectedItem.Equals("HighLighted"))
            {
                availableSfxForm.LoadSFXs("HighLighted");
            }
            else
            {
                cboWords.SelectedItem = "HighLighted";
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboWords_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ((UserControl_MainForm_AvailableSFX)Parent.Parent).LoadSFXs(cboWords.SelectedItem.ToString());
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkSortByDate_CheckedChanged(object sender, System.EventArgs e)
        {
            //Update Control
            if (cboWords.SelectedItem != null)
            {
                ((UserControl_MainForm_AvailableSFX)Parent.Parent).LoadSFXs(cboWords.SelectedItem.ToString());
            }

            //Save State
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("Check2", chkSortByDate.Checked ? "1" : "0", "MainForm");
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        public void LoadKeywords()
        {
            string refineListPath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "RefineSearch.txt");
            if (!File.Exists(refineListPath))
            {
                //Create list
                using (Frm_RefineList refineList = new Frm_RefineList())
                {
                    refineList.ShowDialog();
                }
            }

            UpdateCombobox(refineListPath);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdateCombobox(string refineListPath)
        {
            cboWords.BeginUpdate();
            if (cboWords.Items.Count > 0)
            {
                cboWords.Items.Clear();
            }
            cboWords.Items.AddRange(TextFiles.ReadRefineList(refineListPath));
            cboWords.SelectedIndex = 0;
            cboWords.EndUpdate();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
