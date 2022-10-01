using sb_editor.Forms;
using sb_editor.Objects;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_SfxInDataBase : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_SfxInDataBase()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemoveSfx_Click(object sender, System.EventArgs e)
        {
            RemoveFromDataBase();
        }

        //*===============================================================================================
        //* LISTBOX
        //*===============================================================================================
        private void LstSfxInDataBase_DragDrop(object sender, DragEventArgs e)
        {
            //Get data from the other control
            UserControl_MainForm_AvailableDataBases AvailableDataBases = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Available_Databases;
            if (e.Effect == DragDropEffects.Copy && AvailableDataBases.lstDataBases.SelectedItems.Count == 1)
            {
                string[] sfxItems = null;
                if (e.Data.GetDataPresent(typeof(ListBox.SelectedObjectCollection)))
                {
                    ListBox.SelectedObjectCollection itemsData = (ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection));
                    sfxItems = itemsData.Cast<string>().ToArray();
                }
                else if (e.Data.GetDataPresent(typeof(string[])))
                {
                    sfxItems = (string[])e.Data.GetData(typeof(string[]));
                }

                //Add items
                if (sfxItems != null)
                {
                    string[] listboxItems = lstSfxInDataBase.Items.Cast<string>().ToArray();
                    string[] itemsToAdd = sfxItems.Except(listboxItems).ToArray();

                    if (itemsToAdd.Length > 0)
                    {
                        lstSfxInDataBase.Items.AddRange(itemsToAdd);

                        //Update database file
                        string databaseFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", AvailableDataBases.lstDataBases.SelectedItem + ".txt");
                        DataBase dataBase = TextFiles.ReadDataBaseFile(databaseFilePath, false);
                        dataBase.SFXs = lstSfxInDataBase.Items.Cast<string>().ToArray();
                        TextFiles.WriteDataBaseFile(databaseFilePath, dataBase);

                        //Update label
                        lblSfxCount.Text = string.Format("Total: {0}", lstSfxInDataBase.Items.Count);
                        EnableOrDisableButton();
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstSfxInDataBase_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstSfxInDataBase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstSfxInDataBase.SelectedItem != null)
            {
                OpenSfxEditor();
            }
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void MnuProperties_Click(object sender, System.EventArgs e)
        {
            if (lstSfxInDataBase.SelectedItems.Count == 1)
            {
                string sfxFile = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", lstSfxInDataBase.SelectedItem + ".txt");
                if (File.Exists(sfxFile))
                {
                    using (SFXPropertiesForm sfxPropsForm = new SFXPropertiesForm(sfxFile))
                    {
                        sfxPropsForm.ShowDialog();
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuEdit_Click(object sender, System.EventArgs e)
        {
            if (lstSfxInDataBase.SelectedItems.Count == 1)
            {
                OpenSfxEditor();
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuSelectSFX_Click(object sender, System.EventArgs e)
        {
            MainForm mainForm = (MainForm)Application.OpenForms[nameof(MainForm)];

            //Reload All SFXs
            mainForm.UserControl_Available_SFXs.LoadSFXs("All");

            //Select items
            if (mainForm.UserControl_Available_SFXs.chkIconView.Checked)
            {
                mainForm.UserControl_Available_SFXs.DataGrid_SFXs.Rows.Clear();
                foreach (string item in lstSfxInDataBase.SelectedItems)
                {
                    DataGridViewRow itemResults = mainForm.UserControl_Available_SFXs.DataGrid_SFXs.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[1].Value.ToString().Equals(item)).First();
                    if (itemResults != null)
                    {
                        itemResults.Selected = true;
                    }
                }
            }
            else
            {
                mainForm.UserControl_Available_SFXs.lstAvailableSFXs.SelectedItems.Clear();
                foreach (string item in lstSfxInDataBase.SelectedItems)
                {
                    int itemIndex = mainForm.UserControl_Available_SFXs.lstAvailableSFXs.Items.IndexOf(item);
                    if (itemIndex != ListBox.NoMatches)
                    {
                        mainForm.UserControl_Available_SFXs.lstAvailableSFXs.SelectedItem = mainForm.UserControl_Available_SFXs.lstAvailableSFXs.Items[itemIndex];
                    }
                }
            }

            //Update combobox
            if (mainForm.UserControl_Available_SFXs.UserControl_RefineSFX.cboWords.Items.Count > 1)
            {
                if (mainForm.UserControl_Available_SFXs.UserControl_RefineSFX.cboWords.SelectedIndex == 1)
                {
                    mainForm.UserControl_Available_SFXs.LoadSFXs("HighLighted");
                }
                else
                {
                    mainForm.UserControl_Available_SFXs.UserControl_RefineSFX.cboWords.SelectedIndex = 1;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRemoveSFX_Click(object sender, System.EventArgs e)
        {
            RemoveFromDataBase();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void RemoveFromDataBase()
        {
            UserControl_MainForm_AvailableDataBases AvailableDataBases = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Available_Databases;
            if (lstSfxInDataBase.SelectedItems.Count > 0 && AvailableDataBases.lstDataBases.SelectedItems.Count == 1)
            {
                int selectedIndex = lstSfxInDataBase.SelectedIndex;

                //Remove selected items
                for (int i = lstSfxInDataBase.SelectedItems.Count - 1; i >= 0; i--)
                {
                    lstSfxInDataBase.Items.Remove(lstSfxInDataBase.SelectedItems[i]);
                }

                //Select next item
                if (selectedIndex < lstSfxInDataBase.Items.Count)
                {
                    lstSfxInDataBase.SelectedIndex = selectedIndex;
                }

                //Update database file
                string databaseFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", AvailableDataBases.lstDataBases.SelectedItem + ".txt");
                DataBase dataBase = TextFiles.ReadDataBaseFile(databaseFilePath, false);
                dataBase.SFXs = lstSfxInDataBase.Items.Cast<string>().ToArray();
                TextFiles.WriteDataBaseFile(databaseFilePath, dataBase);

                //Update label
                lblSfxCount.Text = string.Format("Total: {0}", lstSfxInDataBase.Items.Count);
                EnableOrDisableButton();
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void ClearControl()
        {
            if (lstSfxInDataBase.Items.Count > 0)
            {
                lstSfxInDataBase.Items.Clear();
                lblSfxCount.Text = string.Format("Total: {0}", lstSfxInDataBase.Items.Count);
                EnableOrDisableButton();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void OpenSfxEditor()
        {
            //Show form
            SFXForm sfxEditor = new SFXForm(lstSfxInDataBase.SelectedItem.ToString())
            {
                StartPosition = ((MainForm)Application.OpenForms[nameof(MainForm)]).StartPosition
            };
            sfxEditor.Show();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void EnableOrDisableButton()
        {
            bool status = lstSfxInDataBase.Items.Count > 0;
            if (btnRemoveSfx.Enabled != status)
            {
                btnRemoveSfx.Enabled = status;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
