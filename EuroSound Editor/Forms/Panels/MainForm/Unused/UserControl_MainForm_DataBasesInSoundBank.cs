using EuroSound_Editor.Forms;
using EuroSound_Editor.Objects;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_DataBasesInSoundBank : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_DataBasesInSoundBank()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemoveDataBase_Click(object sender, EventArgs e)
        {
            RemoveDataBases();
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void MnuRemoveDataBases_Click(object sender, EventArgs e)
        {
            RemoveDataBases();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuProperties_Click(object sender, EventArgs e)
        {
            if (lstDatabases.SelectedItems.Count == 1)
            {
                string databaseFullPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstDatabases.SelectedItems[0].ToString() + ".txt");
                if (File.Exists(databaseFullPath))
                {
                    using (DataBasePropertiesForm dbProperties = new DataBasePropertiesForm(databaseFullPath))
                    {
                        dbProperties.ShowDialog();
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuSelectDataBase_Click(object sender, EventArgs e)
        {
            if (lstDatabases.SelectedItems.Count > 0)
            {
                MainForm mainForm = (MainForm)Application.OpenForms[nameof(MainForm)];
                ListBox listbox = mainForm.UserControl_Available_Databases.lstDataBases;

                listbox.SelectedIndices.Clear();
                for (int i = 0; i < lstDatabases.SelectedItems.Count; i++)
                {
                    int itemIndex = listbox.FindString(lstDatabases.SelectedItems[i].ToString());
                    if (itemIndex != ListBox.NoMatches)
                    {
                        listbox.SelectedIndex = itemIndex;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRemoveSelDataBase_Click(object sender, EventArgs e)
        {
            UserControl_MainForm_AvailableDataBases databasesControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Available_Databases;
            if (databasesControl.lstDataBases.SelectedItems.Count > 0)
            {
                for (int i = 0; i < databasesControl.lstDataBases.SelectedItems.Count; i++)
                {
                    int itemIndex = lstDatabases.FindString(databasesControl.lstDataBases.SelectedItems[i].ToString());
                    if (itemIndex != ListBox.NoMatches)
                    {
                        lstDatabases.Items.RemoveAt(itemIndex);
                    }
                }

                UserControl_MainForm_SoundBanks_CheckBox soundBankControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_SoundBanks_CheckBox;
                if (soundBankControl.cbllstSoundbanks.SelectedItems.Count == 1)
                {
                    //Update text files
                    UpdateSoundBankDataBases(soundBankControl);
                }
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void RemoveDataBases()
        {
            UserControl_MainForm_SoundBanks_CheckBox soundBankControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_SoundBanks_CheckBox;
            if (lstDatabases.SelectedItems.Count > 0 && soundBankControl.cbllstSoundbanks.SelectedItems.Count == 1)
            {
                //Remove items 
                for (int i = lstDatabases.SelectedItems.Count - 1; i >= 0; i--)
                {
                    lstDatabases.Items.Remove(lstDatabases.SelectedItems[i]);
                }

                //Update text files
                UpdateSoundBankDataBases(soundBankControl);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdateSoundBankDataBases(UserControl_MainForm_SoundBanks_CheckBox soundBankControl)
        {
            //Update text files
            string sbFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", soundBankControl.cbllstSoundbanks.SelectedItems[0] + ".txt");
            if (File.Exists(sbFilePath))
            {
                SoundBank sbData = TextFiles.ReadSoundbankFile(sbFilePath);
                sbData.DataBases = lstDatabases.Items.OfType<string>().ToArray();
                Array.Sort(sbData.DataBases);

                //Write file again
                TextFiles.WriteSoundBankFile(sbFilePath, sbData);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
