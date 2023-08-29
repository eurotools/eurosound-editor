//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Available SFXs Panel
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_AvailableSFX : UserControl
    {
        public bool EnableReadOnly { get; set; }

        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_AvailableSFX()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UserControl_MainForm_AvailableSFX_Load(object sender, EventArgs e)
        {
            if (chkIconView.Checked)
            {
                DataGrid_SFXs.ClearSelection();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAddSFXs_Click(object sender, EventArgs e)
        {
            AddSfxToDB();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkIconView_CheckedChanged(object sender, EventArgs e)
        {
            //Checkbox
            DataGrid_SFXs.Visible = chkIconView.Checked;
            DataGrid_SFXs.Enabled = chkIconView.Checked;
            lstAvailableSFXs.Visible = !chkIconView.Checked;
            lstAvailableSFXs.Enabled = !chkIconView.Checked;

            //Reload
            object selectedKeyWord = UserControl_RefineSFX.cboWords.SelectedItem;
            if (selectedKeyWord != null)
            {
                LoadSFXs(selectedKeyWord.ToString());
            }

            //Save State
            IniFile iniFile = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            iniFile.Write("Check3", chkIconView.Checked ? "1" : "0", "MainForm");
        }

        //*===============================================================================================
        //* LISTBOX
        //*===============================================================================================
        private void LstAvailableSFXs_DragDrop(object sender, DragEventArgs e)
        {
            DoDragDrop(e);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstAvailableSFXs_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LstAvailableSFXs_DoubleClick(object sender, EventArgs e)
        {
            OpenSfxEditor();
        }

        //*===============================================================================================
        //* LISTVIEW
        //*===============================================================================================
        private void DataGrid_SFXs_DragDrop(object sender, DragEventArgs e)
        {
            DoDragDrop(e);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void DataGrid_SFXs_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void DataGrid_SFXs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu_ListView.Show(DataGrid_SFXs, new Point(e.X, e.Y));
            }
            else if (e.Clicks == 1)
            {
                // create array or collection for all selected items
                HashSet<string> items = new HashSet<string>();
                // optionally add the other selected ones
                foreach (DataGridViewRow lvi in DataGrid_SFXs.SelectedRows)
                {
                    items.Add(lvi.Cells[1].Value.ToString());
                }
                // pass the items to move...
                DataGrid_SFXs.DoDragDrop(items.ToArray(), DragDropEffects.Copy);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void DataGrid_SFXs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSfxEditor();
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void MnuAddToDB_Click(object sender, EventArgs e)
        {
            AddSfxToDB();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuProperties_Click(object sender, EventArgs e)
        {
            if (chkIconView.Checked && DataGrid_SFXs.SelectedRows.Count == 1)
            {
                ShowSfxProperties(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", DataGrid_SFXs.SelectedRows[0].Cells[1].Value + ".txt"));
            }
            else if (!chkIconView.Checked && lstAvailableSFXs.SelectedItems.Count == 1)
            {
                ShowSfxProperties(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", lstAvailableSFXs.SelectedItem + ".txt"));
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuEdit_Click(object sender, EventArgs e)
        {
            if (chkIconView.Checked && DataGrid_SFXs.SelectedRows.Count == 1
            || !chkIconView.Checked && lstAvailableSFXs.SelectedItems.Count == 1)
            {
                OpenSfxEditor();
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuNew_Click(object sender, EventArgs e)
        {
            string sfxDefaultPath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "SFX Defaults.txt");
            if (File.Exists(sfxDefaultPath) && File.ReadAllLines(sfxDefaultPath).Length > 8)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New SFX" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

                    inputDiag.lblText.Text = "Enter Name";
                    inputDiag.txtInputData.Text = MultipleFilesFunctions.GetNextAvailableFilename(folderPath, "SFX_Label");
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
                                    SFX sfxFile = TextFiles.ReadSfxFile(sfxDefaultPath);
                                    sfxFile.HashCode = GlobalPrefs.SFXHashCodeNumber;

                                    //Inform user if ReAlloc is required
                                    if (GlobalPrefs.SFXHashCodeNumber == 0)
                                    {
                                        MessageBox.Show("Please Re-Alloc Hashcodes under Advanced Menu", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    //Write file and update variable
                                    TextFiles.WriteSfxFile(filePath, sfxFile);
                                    GlobalPrefs.SFXHashCodeNumber++;

                                    //Update SFXs
                                    ProjectFileFunctions.UpdateSFXs((MainForm)Parent.Parent.Parent.Parent.Parent);
                                    if (chkIconView.Checked)
                                    {
                                        FindDataGridRow(fileName).Selected = true;
                                    }
                                    else
                                    {
                                        lstAvailableSFXs.SelectedItem = fileName;
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
                MessageBox.Show("Must Setup Default SFX file first!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                using (SFXForm defaults = new SFXForm("SFX Default Setting", true))
                {
                    defaults.ShowDialog();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuCopy_Click(object sender, EventArgs e)
        {
            if (chkIconView.Checked)
            {
                if (DataGrid_SFXs.SelectedRows.Count == 1)
                {
                    CopySFX((string)DataGrid_SFXs.SelectedRows[0].Cells[1].Value);
                }
                else
                {
                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                if (lstAvailableSFXs.SelectedItems.Count == 1)
                {
                    CopySFX((string)lstAvailableSFXs.SelectedItem);
                }
                else
                {
                    SystemSounds.Beep.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuDelete_Click(object sender, EventArgs e)
        {
            if (EnableReadOnly)
            {
                SystemSounds.Beep.Play();
            }
            else if (lstAvailableSFXs.SelectedItems.Count > 0 || DataGrid_SFXs.SelectedRows.Count > 0)
            {
                string[] SFXsToRemove = GetSelectedSfx();
                if (MessageBox.Show(MultipleFilesFunctions.GetFilesRemovingMessage("Are you sure you want to delete SFX(s)", SFXsToRemove), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MultipleFilesFunctions.RemoveFilesAndUpdateDependencies(SFXsToRemove, "SFXs", "DataBases");
                    ProjectFileFunctions.UpdateAll((MainForm)Parent.Parent.Parent.Parent.Parent);
                }
                else
                {
                    SystemSounds.Beep.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRename_Click(object sender, EventArgs e)
        {
            if (EnableReadOnly)
            {
                SystemSounds.Beep.Play();
            }
            else
            {
                //Get file that we want to rename
                string fileToRename = string.Empty;
                if (chkIconView.Checked && DataGrid_SFXs.SelectedRows.Count == 1)
                {
                    fileToRename = DataGrid_SFXs.SelectedRows[0].Cells[1].Value.ToString();
                }
                else if (!chkIconView.Checked && lstAvailableSFXs.SelectedItems.Count == 1)
                {
                    fileToRename = lstAvailableSFXs.SelectedItem.ToString();
                }
                else
                {
                    SystemSounds.Beep.Play();
                }

                if (!string.IsNullOrEmpty(fileToRename))
                {
                    //Ask user for a name
                    using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename SFX" })
                    {
                        string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

                        inputDiag.lblText.Text = string.Format("Enter New Name For SFX {0}", fileToRename);
                        inputDiag.txtInputData.Text = fileToRename;
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
                                        string source = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", fileToRename + ".txt");
                                        File.Move(source, newFilePath);

                                        //Update All SoundBanks
                                        string[] soundBanks = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"), "*.txt", SearchOption.TopDirectoryOnly);
                                        for (int i = 0; i < soundBanks.Length; i++)
                                        {
                                            string[] fileData = File.ReadAllLines(soundBanks[i]);
                                            int index = Array.IndexOf(fileData, fileToRename);
                                            if (index != -1)
                                            {
                                                fileData[index] = fileName;
                                            }
                                            File.WriteAllLines(soundBanks[i], fileData);
                                        }

                                        //Update Control
                                        if (chkIconView.Checked)
                                        {
                                            DataGrid_SFXs.SelectedRows[0].Cells[1].Value = fileName;
                                        }
                                        else
                                        {
                                            lstAvailableSFXs.Items[lstAvailableSFXs.SelectedIndex] = fileName;
                                        }

                                        //Reload Soundbanks
                                        ProjectFileFunctions.UpdateSoundBanks((MainForm)Parent.Parent.Parent.Parent.Parent);
                                        ProjectFileFunctions.UpdateDataBases((MainForm)Parent.Parent.Parent.Parent.Parent);
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

            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuMultiEditor_Click(object sender, EventArgs e)
        {
            if (EnableReadOnly)
            {
                SystemSounds.Beep.Play();
            }
            else
            {
                using (MultiEditor multiEdit = new MultiEditor(GetSelectedSfx()))
                {
                    multiEdit.ShowDialog();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuNewMultiple_Click(object sender, EventArgs e)
        {
            string defaultsFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "SFX Defaults.txt");
            if (File.Exists(defaultsFile))
            {
                using (MultiCreate frmNewMultiple = new MultiCreate())
                {
                    frmNewMultiple.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Must Setup Default SFX file first!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuSetupGroups_Click(object sender, EventArgs e)
        {
            using (GroupingForm sfxGrouping = new GroupingForm())
            {
                sfxGrouping.ShowDialog();
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void AddSfxToDB()
        {
            UserControl_MainForm_SfxInDataBase SfxInDataBase = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_DataBaseSfx;
            UserControl_MainForm_AvailableDataBases AvailableDataBases = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Available_Databases;
            if ((lstAvailableSFXs.SelectedItems.Count > 0 || DataGrid_SFXs.SelectedRows.Count > 0) && AvailableDataBases.lstDataBases.SelectedItems.Count == 1)
            {
                //Get items that we want to add
                if (chkIconView.Checked)
                {
                    foreach (DataGridViewRow item in DataGrid_SFXs.SelectedRows)
                    {
                        if (!SfxInDataBase.lstSfxInDataBase.Items.Contains(item.Cells[1].Value))
                        {
                            SfxInDataBase.lstSfxInDataBase.Items.Add(item.Cells[1].Value);
                        }
                    }
                }
                else
                {
                    List<string> sfxToAdd = lstAvailableSFXs.SelectedItems.OfType<string>().ToList();
                    //Add items to the selected database
                    for (int i = 0; i < sfxToAdd.Count; i++)
                    {
                        if (!SfxInDataBase.lstSfxInDataBase.Items.Contains(sfxToAdd[i]))
                        {
                            SfxInDataBase.lstSfxInDataBase.Items.Add(sfxToAdd[i]);
                        }
                    }
                }

                //Update database file
                string databaseFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", AvailableDataBases.lstDataBases.SelectedItem + ".txt");
                DataBase dataBase = TextFiles.ReadDataBaseFile(databaseFilePath, false);
                dataBase.SFXs = SfxInDataBase.lstSfxInDataBase.Items.Cast<string>().ToArray();
                TextFiles.WriteDataBaseFile(databaseFilePath, dataBase);

                //Update label
                SfxInDataBase.lblSfxCount.Text = string.Format("Total: {0}", SfxInDataBase.lstSfxInDataBase.Items.Count);
                SfxInDataBase.EnableOrDisableButton();
            }
            else
            {
                if (AvailableDataBases.lstDataBases.Items.Count > 0)
                {
                    MessageBox.Show("There isn't any DataBase selected where to add this SFX. Select a DataBase and try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("There isn't any DataBase where to add this SFX to, yet. Create a new DataBase first by right-clicking in the Available DataBases list and try again.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ShowSfxProperties(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (SFXPropertiesForm sfxPropsForm = new SFXPropertiesForm(filePath))
                {
                    sfxPropsForm.ShowDialog();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] LoadSFXs(string keyWord)
        {
            //Save loaded tags
            List<string> LoadedLabels = new List<string>();

            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
            {
                //Clear controls
                lstTempSorted.Items.Clear();

                //Get files that will be added to the list
                string[] SfxToLoad;
                if (keyWord.Equals("HighLighted"))
                {
                    List<string> sfxTextFiles = new List<string>();
                    if (chkIconView.Checked)
                    {
                        for (int i = 0; i < DataGrid_SFXs.SelectedRows.Count; i++)
                        {
                            sfxTextFiles.Add(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", DataGrid_SFXs.SelectedRows[i].Cells[1].Value + ".txt"));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < lstAvailableSFXs.SelectedItems.Count; i++)
                        {
                            sfxTextFiles.Add(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", lstAvailableSFXs.SelectedItems[i] + ".txt"));
                        }
                    }
                    SfxToLoad = sfxTextFiles.ToArray();
                }
                else if (keyWord.Equals("All"))
                {
                    SfxToLoad = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                }
                else
                {
                    List<string> sfxDat = new List<string>();
                    string[] sfxTextFiles = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < sfxTextFiles.Length; i++)
                    {
                        string sfxLabel = Path.GetFileNameWithoutExtension(sfxTextFiles[i]);
                        if (sfxLabel.Contains(keyWord))
                        {
                            sfxDat.Add(sfxTextFiles[i]);
                        }
                    }
                    SfxToLoad = sfxDat.ToArray();
                }

                //Load files
                if (SfxToLoad != null)
                {
                    //Clear data
                    lstAvailableSFXs.Items.Clear();
                    DataGrid_SFXs.Rows.Clear();

                    //Add items to a temporal list to sort them by the name or date. 
                    for (int i = 0; i < SfxToLoad.Length; i++)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(SfxToLoad[i]);
                        if (UserControl_RefineSFX.chkSortByDate.Checked)
                        {
                            string[] fileData = File.ReadAllLines(SfxToLoad[i]);
                            string dateString = string.Empty;
                            if (fileData.Length > 1)
                            {
                                if (fileData[1].StartsWith("## First Created ... "))
                                {
                                    if (fileData[1].Contains("See VSS"))
                                    {
                                        dateString = "020305";
                                    }
                                    else
                                    {
                                        string fileLine = fileData[1].Substring("## First Created ... ".Length, "10-11-2001".Length);
                                        dateString = string.Join(string.Empty, fileLine.Substring(8, 2), fileLine.Substring(0, 2), fileLine.Substring(3, 2));
                                    }
                                }
                            }
                            lstTempSorted.Items.Add(new ListBoxItem(dateString, fileName));
                        }
                        else
                        {
                            lstTempSorted.Items.Add(new ListBoxItem(fileName, null));
                        }

                        //Save Label
                        LoadedLabels.Add(fileName);
                    }

                    //Add SFX Labels to the final list. 
                    for (int i = 0; i < lstTempSorted.Items.Count; i++)
                    {
                        string itemText = ((ListBoxItem)lstTempSorted.Items[i]).Text;
                        if (UserControl_RefineSFX.chkSortByDate.Checked)
                        {
                            itemText = ((ListBoxItem)lstTempSorted.Items[i]).ItemData;
                        }

                        if (chkIconView.Checked)
                        {
                            DataGrid_SFXs.Rows.Add(lvwImageList.Images[0], itemText);
                        }
                        else
                        {
                            lstAvailableSFXs.Items.Add(itemText);
                        }
                    }

                    //Update label
                    if (chkIconView.Checked)
                    {
                        UpdateListViewIcons();
                        DataGrid_SFXs.ClearSelection();
                        lblTotal_SFXs.Text = "Total: " + DataGrid_SFXs.Rows.Count;
                    }
                    else
                    {
                        lblTotal_SFXs.Text = "Total: " + lstAvailableSFXs.Items.Count;
                    }
                    EnableOrDisableButton();
                }
            }

            return LoadedLabels.ToArray();
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdateListViewIcons()
        {
            //Update icons
            foreach (DataGridViewRow itemToChek in DataGrid_SFXs.Rows)
            {
                string[] fileData = File.ReadAllLines(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToChek.Cells[1].Value + ".txt"));
                if (Array.IndexOf(fileData, "EnableSubSFX  1") != -1)
                {
                    itemToChek.Cells[0].Value = lvwImageList.Images[1];
                    int i = Array.IndexOf(fileData, "#SFXSamplePoolFiles") + 1;
                    if (i > 0)
                    {
                        string currentLine = fileData[i++];
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            DataGridViewRow itemToEdit = FindDataGridRow(currentLine);
                            if (itemToEdit != null)
                            {
                                itemToEdit.Cells[0].Value = lvwImageList.Images[2];
                            }
                            currentLine = fileData[i++];
                        }
                    }
                }
            }

            //Highlight unused hashcodes
            string[] unusedSFXs = GetUnuSedHashCodes();
            for (int i = 0; i < unusedSFXs.Length; i++)
            {
                DataGridViewRow itemToEdit = FindDataGridRow(unusedSFXs[i]);
                if (itemToEdit != null)
                {
                    itemToEdit.DefaultCellStyle = new DataGridViewCellStyle { Font = new Font(DataGrid_SFXs.Font, FontStyle.Bold | FontStyle.Italic) };
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal DataGridViewRow FindDataGridRow(string keyword)
        {
            DataGridViewRow searchResult = null;
            for (int i = 0; i < DataGrid_SFXs.Rows.Count; i++)
            {
                if (DataGrid_SFXs.Rows[i].Cells[1].Value.Equals(keyword))
                {
                    searchResult = DataGrid_SFXs.Rows[i];
                    break;
                }
            }
            return searchResult;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetUnuSedHashCodes()
        {
            HashSet<string> UsedSFXs = new HashSet<string>();
            HashSet<string> AllSFXs = new HashSet<string>();

            //Inspect DataBases
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases")))
            {
                string[] dataBases = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"), "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < dataBases.Length; i++)
                {
                    string[] fileData = File.ReadAllLines(dataBases[i]);
                    int j = Array.IndexOf(fileData, "#DEPENDENCIES") + 1;
                    string currentLine = fileData[j];
                    while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                    {
                        UsedSFXs.Add(currentLine);
                        currentLine = fileData[j++];
                    }
                }
            }

            //Get All Available SFXs
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
            {
                string[] SFXs = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < SFXs.Length; i++)
                {
                    AllSFXs.Add(Path.GetFileNameWithoutExtension(SFXs[i]));
                }
            }

            return AllSFXs.Except(UsedSFXs).ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void DoDragDrop(DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move && e.Data.GetDataPresent(typeof(ListBox.SelectedObjectCollection)))
            {
                UserControl_MainForm_AvailableDataBases AvailableDataBases = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Available_Databases;
                ListBox.SelectedObjectCollection itemsData = (ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection));
                if (itemsData.Count > 0)
                {
                    UserControl_MainForm_SfxInDataBase SfxInDataBase = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_DataBaseSfx;
                    while (SfxInDataBase.lstSfxInDataBase.SelectedItems.Count > 0)
                    {
                        SfxInDataBase.lstSfxInDataBase.Items.Remove(SfxInDataBase.lstSfxInDataBase.SelectedItems[0]);
                    }

                    //Update text file
                    string databaseTxt = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", AvailableDataBases.lstDataBases.SelectedItem + ".txt");
                    DataBase dataBase = TextFiles.ReadDataBaseFile(databaseTxt, false);
                    dataBase.SFXs = SfxInDataBase.lstSfxInDataBase.Items.Cast<string>().ToArray();
                    TextFiles.WriteDataBaseFile(databaseTxt, dataBase);

                    //Update label
                    SfxInDataBase.lblSfxCount.Text = string.Format("Total: {0}", SfxInDataBase.lstSfxInDataBase.Items.Count);
                    SfxInDataBase.EnableOrDisableButton();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void OpenSfxEditor()
        {
            string sfxFileName = string.Empty;
            if (chkIconView.Checked)
            {
                sfxFileName = DataGrid_SFXs.SelectedRows[0].Cells[1].Value.ToString();
            }
            else if (lstAvailableSFXs.SelectedItem != null)
            {
                sfxFileName = lstAvailableSFXs.SelectedItem.ToString();
            }

            //Show form
            if (!string.IsNullOrEmpty(sfxFileName))
            {
                SFXForm sfxEditor = new SFXForm(sfxFileName)
                {
                    StartPosition = ((MainForm)Application.OpenForms[nameof(MainForm)]).StartPosition
                };
                sfxEditor.Show();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CopySFX(string SFXfileName)
        {
            //Ask user for a name
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Copy SFX" })
            {
                string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs");

                inputDiag.lblText.Text = string.Format("Enter Copy Name For SFX {0}", SFXfileName);
                inputDiag.txtInputData.Text = SFXfileName;
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
                                string filePath = Path.Combine(folderPath, SFXfileName + ".txt");
                                File.Copy(filePath, newFilePath);

                                //Update SFXs
                                ProjectFileFunctions.UpdateSFXs((MainForm)Parent.Parent.Parent.Parent.Parent);
                                if (chkIconView.Checked)
                                {
                                    FindDataGridRow(fileName).Selected = true;
                                }
                                else
                                {
                                    lstAvailableSFXs.SelectedItem = fileName;
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

        //-------------------------------------------------------------------------------------------------------------------------------
        private string[] GetSelectedSfx()
        {
            string[] SFXs;
            if (chkIconView.Checked)
            {
                List<string> selectedRows = new List<string>();
                foreach (DataGridViewRow rowToCheck in DataGrid_SFXs.SelectedRows)
                {
                    selectedRows.Add((string)rowToCheck.Cells[1].Value);
                }
                SFXs = selectedRows.ToArray();
            }
            else
            {
                SFXs = lstAvailableSFXs.SelectedItems.OfType<string>().ToArray();
            }

            return SFXs;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void EnableOrDisableButton()
        {
            bool status;
            if (chkIconView.Checked)
            {
                status = DataGrid_SFXs.Rows.Count > 0;
            }
            else
            {
                status = lstAvailableSFXs.Items.Count > 0;
            }
            if (btnAddSFXs.Enabled != status)
            {
                btnAddSFXs.Enabled = status;
                lblSFXsTutorial.Visible = !status;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
