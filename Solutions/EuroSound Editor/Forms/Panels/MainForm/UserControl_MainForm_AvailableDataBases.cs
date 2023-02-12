using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_AvailableDataBases : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_AvailableDataBases()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* BUTTONS
        //*===============================================================================================
        private void BtnAddDataBases_Click(object sender, System.EventArgs e)
        {
            AddDataBases();
        }

        //*===============================================================================================
        //* LISTBOX
        //*===============================================================================================
        private void LstDataBases_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the UserControl instance for displaying sfx of the selected database
            UserControl_MainForm_SfxInDataBase SfxInDataBase = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_DataBaseSfx;
            SfxInDataBase.ClearControl();

            //Load DataBase dependencies
            if (lstDataBases.SelectedItems.Count == 1)
            {
                // Get the path of the selected database file
                string databasePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstDataBases.SelectedItem + ".txt");
                if (File.Exists(databasePath))
                {
                    // Read the sfx dependencies list from the database file and display it in the control
                    SfxInDataBase.lstSfxInDataBase.Items.AddRange(TextFiles.ReadListBlock(databasePath, "#DEPENDENCIES"));
                }
                else
                {
                    // Show an error message if the database file does not exist
                    MessageBox.Show(string.Join(" ", "File not found", databasePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Update the sfx count label
                SfxInDataBase.lblSfxCount.Text = string.Join(" ", "Total:", SfxInDataBase.lstSfxInDataBase.Items.Count);

                // Enable or disable buttons in the control based on the number of sfx in the list
                SfxInDataBase.EnableOrDisableButton();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstDataBases_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenDataBaseProperties();
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void MnuAddDataBaseToSoundBank_Click(object sender, EventArgs e)
        {
            AddDataBases();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuNew_Click(object sender, EventArgs e)
        {
            // Show an input dialog to prompt for the new database file name
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New Database" })
            {
                // Get the path of the DataBases folder
                string dataBasesFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");

                // Set the label text and default input value for the input dialog box
                inputDiag.lblText.Text = "Enter Name";
                inputDiag.txtInputData.Text = MultipleFilesFunctions.GetNextAvailableFilename(dataBasesFolderPath, "DB_Label");

                // Keep looping until the user clicks the OK button or cancels the input dialog box
                while (true)
                {
                    if (inputDiag.ShowDialog() == DialogResult.OK)
                    {
                        // Get the user-entered file name
                        string fileName = inputDiag.txtInputData.Text.Trim();

                        // If the user entered an empty string, exit the loop
                        if (string.IsNullOrEmpty(fileName))
                        {
                            break;
                        }
                        else
                        {
                            // Get the path of the new file
                            string filePath = Path.Combine(dataBasesFolderPath, fileName + ".txt");
                            if (File.Exists(filePath))
                            {
                                // If the file already exists, show an error message and loop again
                                MessageBox.Show(string.Format("Label '{0}' already exists please use another name!", fileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                // Create a new DataBase object
                                DataBase databaseFile = new DataBase();

                                // Write the new database file to the file system
                                TextFiles.WriteDataBaseFile(filePath, databaseFile);

                                // Update the list of databases in the main form
                                ProjectFileFunctions.UpdateDataBases((MainForm)Parent.Parent.Parent);

                                //Keep selection 
                                lstDataBases.SelectedItem = fileName;
                                break;
                            }
                        }
                    }
                    else // If the user clicks "Cancel" in the dialog, exit the loop
                    {
                        break;
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuCopy_Click(object sender, System.EventArgs e)
        {
            // Make sure only one item is selected
            if (lstDataBases.SelectedItems.Count == 1)
            {
                // Open input dialog to ask for the name of the copied database
                using (Frm_InputBox inputDialog = new Frm_InputBox() { Text = "Copy Database" })
                {
                    // Get the path to the DataBases folder
                    string dataBasesFolderPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");

                    // Set the label text and default value for the input field
                    inputDialog.lblText.Text = string.Format("Enter Copy Name For Database {0}", lstDataBases.SelectedItems[0]);
                    inputDialog.txtInputData.Text = (string)lstDataBases.SelectedItems[0];

                    // Keep showing the input dialog until the user provides a valid name
                    while (true)
                    {
                        if (inputDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Get the input name
                            string fileName = inputDialog.txtInputData.Text.Trim();

                            // Check if the name is empty
                            if (string.IsNullOrEmpty(fileName))
                            {
                                break;
                            }
                            else
                            {
                                // Check if a database with the same name already exists
                                string newFilePath = Path.Combine(dataBasesFolderPath, fileName + ".txt");
                                if (File.Exists(newFilePath))
                                {
                                    MessageBox.Show(string.Format("Label '{0}' already exists please use another name!", fileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    string filePath = Path.Combine(dataBasesFolderPath, lstDataBases.SelectedItems[0] + ".txt");
                                    File.Copy(filePath, newFilePath);

                                    lstDataBases.Items.Add(fileName);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuDelete_Click(object sender, System.EventArgs e)
        {
            if (lstDataBases.SelectedItems.Count > 0)
            {
                string[] DataBasesToDelete = lstDataBases.SelectedItems.OfType<string>().ToArray();
                if (MessageBox.Show(MultipleFilesFunctions.GetFilesRemovingMessage("Are you sure you want to delete Database(s)", DataBasesToDelete), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MultipleFilesFunctions.RemoveFilesAndUpdateDependencies(DataBasesToDelete, "DataBases", "SoundBanks");
                    ProjectFileFunctions.UpdateAll((MainForm)Parent.Parent.Parent);
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRename_Click(object sender, EventArgs e)
        {
            if (lstDataBases.SelectedItem != null && lstDataBases.SelectedItems.Count == 1)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename Database" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");

                    inputDiag.lblText.Text = string.Format("Enter New Name For Database {0}", lstDataBases.SelectedItem);
                    inputDiag.txtInputData.Text = lstDataBases.SelectedItem.ToString();
                    while (true)
                    {
                        if (inputDiag.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = inputDiag.txtInputData.Text.Trim();
                            if (string.IsNullOrEmpty(fileName))
                            {
                                break;
                            }
                            else
                            {
                                string newFilePath = Path.Combine(folderPath, fileName + ".txt");
                                if (File.Exists(newFilePath))
                                {
                                    MessageBox.Show(string.Format("Label '{0}' already exists please use another name!", fileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    //Rename file
                                    string dataBaseName = lstDataBases.SelectedItem.ToString();
                                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", dataBaseName + ".txt");
                                    if (File.Exists(source))
                                    {
                                        File.Move(source, newFilePath);

                                        //Update All SoundBanks
                                        string[] soundBanks = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks"), "*.txt", SearchOption.TopDirectoryOnly);
                                        for (int i = 0; i < soundBanks.Length; i++)
                                        {
                                            string[] fileData = File.ReadAllLines(soundBanks[i]);
                                            int index = Array.IndexOf(fileData, dataBaseName);
                                            if (index != -1)
                                            {
                                                fileData[index] = fileName;
                                            }
                                            File.WriteAllLines(soundBanks[i], fileData);
                                        }

                                        //Update Listbox
                                        lstDataBases.Items[lstDataBases.SelectedIndex] = fileName;

                                        //Reload Soundbanks
                                        MainForm frmMainForm = (MainForm)Application.OpenForms[nameof(MainForm)];
                                        frmMainForm.UserControl_SoundBanks_CheckBox.LoadSoundBanks();
                                        frmMainForm.UserControl_SoundBanks.LoadSoundBanks(frmMainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks);
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format("File not found: '{0}'", source), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuProperties_Click(object sender, System.EventArgs e)
        {
            OpenDataBaseProperties();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        internal string[] LoadDataBases()
        {
            List<string> LoadedDataBases = new List<string>();
            string DatabasessFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");
            if (Directory.Exists(DatabasessFilePath))
            {
                //Clear the current items
                if (lstDataBases.Items.Count > 0)
                {
                    lstDataBases.Items.Clear();
                }

                //Add existing files
                string[] DatabaseFiles = Directory.GetFiles(DatabasessFilePath, "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < DatabaseFiles.Length; i++)
                {
                    string dataBaseName = Path.GetFileNameWithoutExtension(DatabaseFiles[i]);
                    lstDataBases.Items.Add(dataBaseName);
                    LoadedDataBases.Add(dataBaseName);
                }

                //Clear SFXs in DataBase control
                ((MainForm)Parent.Parent.Parent).UserControl_DataBaseSfx.ClearControl();

                //Update label
                lblDataBases_Count.Text = string.Join(" ", "Total:", lstDataBases.Items.Count);
                EnableOrDisableButton();
            }

            //Sort list
            LoadedDataBases.Sort();

            return LoadedDataBases.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void AddDataBases()
        {
            UserControl_Manform_SoundBanks SoundBanksControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_SoundBanks;
            if (lstDataBases.SelectedItems.Count > 0 && SoundBanksControl.tvwSoundBanks.SelectedNode != null)
            {
                //Ensure that the selected node is valid!
                if (SoundBanksControl.tvwSoundBanks.SelectedNode.Level > 0)
                {
                    SoundBanksControl.tvwSoundBanks.SelectedNode = SoundBanksControl.tvwSoundBanks.SelectedNode.Parent;
                }

                //Read SoundBank
                string soundBankPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", SoundBanksControl.tvwSoundBanks.SelectedNode.Text + ".txt");
                SoundBank SoundBankData = TextFiles.ReadSoundbankFile(soundBankPath);
                string[] databasesToAdd = lstDataBases.SelectedItems.OfType<string>().Except(SoundBankData.DataBases).ToArray();
                if (databasesToAdd.Length > 0)
                {
                    //Update text file
                    string[] dataBasesList = databasesToAdd.Union(SoundBankData.DataBases).OrderBy(x => x).ToArray();
                    SoundBankData.DataBases = dataBasesList;
                    TextFiles.WriteSoundBankFile(soundBankPath, SoundBankData);

                    //Update Node
                    SoundBanksControl.AddDataBases(SoundBanksControl.tvwSoundBanks.SelectedNode, SoundBankData.DataBases, true);
                }
            }
            else
            {
                if (SoundBanksControl.tvwSoundBanks.Nodes.Count > 0)
                {
                    MessageBox.Show("There isn't any SoundBank selected where to add this DataBase. Select a SoundBank and try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("There isn't any SoundBank where to add this DataBase to, yet. Create a new SoundBank first by right-clicking in the SoundBanks list and try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void OpenDataBaseProperties()
        {
            if (lstDataBases.SelectedItems.Count == 1)
            {
                string databaseFullPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstDataBases.SelectedItem.ToString() + ".txt");
                if (File.Exists(databaseFullPath))
                {
                    using (DataBasePropertiesForm dbProperties = new DataBasePropertiesForm(databaseFullPath))
                    {
                        dbProperties.ShowDialog();
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void EnableOrDisableButton()
        {
            bool status = lstDataBases.Items.Count > 0;
            if (btnAddDataBases.Enabled != status)
            {
                btnAddDataBases.Enabled = status;
                lblDataBaseTutorial.Visible = !status;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
