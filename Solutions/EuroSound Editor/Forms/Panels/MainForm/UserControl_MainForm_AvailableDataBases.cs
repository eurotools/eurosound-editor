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
            //Clear current items
            UserControl_MainForm_SfxInDataBase SfxInDataBase = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_DataBaseSfx;
            SfxInDataBase.ClearControl();

            //Load DataBase dependencies
            if (lstDataBases.SelectedItems.Count == 1)
            {
                string DataBasePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstDataBases.SelectedItem + ".txt");
                if (File.Exists(DataBasePath))
                {
                    SfxInDataBase.lstSfxInDataBase.Items.AddRange(TextFiles.ReadListBlock(DataBasePath, "#DEPENDENCIES"));
                }
                else
                {
                    MessageBox.Show(string.Join(" ", "File not found", DataBasePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Update Counter
                SfxInDataBase.lblSfxCount.Text = string.Join(" ", "Total:", SfxInDataBase.lstSfxInDataBase.Items.Count);
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
            //Ask user for a name
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New Database" })
            {
                string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");

                inputDiag.lblText.Text = "Enter Name";
                inputDiag.txtInputData.Text = MultipleFilesFunctions.GetNextAvailableFilename(folderPath, "DB_Label");
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
                            string filePath = Path.Combine(folderPath, fileName + ".txt");
                            if (File.Exists(filePath))
                            {
                                MessageBox.Show(string.Format("Label '{0}' already exists please use another name!", fileName), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //Create new soundbank file
                                DataBase databaseFile = new DataBase();
                                TextFiles.WriteDataBaseFile(filePath, databaseFile);

                                //Update UI
                                ProjectFileFunctions.UpdateDataBases((sb_editor.MainForm)Parent.Parent.Parent);
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

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuCopy_Click(object sender, System.EventArgs e)
        {
            if (lstDataBases.SelectedItems.Count == 1)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Copy Database" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases");

                    inputDiag.lblText.Text = string.Format("Enter Copy Name For Database {0}", lstDataBases.SelectedItems[0]);
                    inputDiag.txtInputData.Text = (string)lstDataBases.SelectedItems[0];
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
                                    //Copy file
                                    string filePath = Path.Combine(folderPath, lstDataBases.SelectedItems[0] + ".txt");
                                    File.Copy(filePath, newFilePath);

                                    //Update UI
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
                SystemSounds.Beep.Play();
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
