using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class GroupingForm : Form
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public GroupingForm()
        {
            InitializeComponent();

            //Custom cursors
            btnAddSFXsToGroup.Cursor = new Cursor(new MemoryStream(Properties.Resources.arrow_left));
            btnRemoveSFXsFromGroup.Cursor = new Cursor(new MemoryStream(Properties.Resources.arrow_right));
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void GroupingForm_Load(object sender, System.EventArgs e)
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();

            //Groups
            DirectoryInfo groupsDir = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"));
            if (Directory.Exists(groupsDir.FullName))
            {
                string[] availableGroups = Directory.GetFiles(groupsDir.FullName, "*.txt", SearchOption.AllDirectories);
                for (int i = 0; i < availableGroups.Length; i++)
                {
                    GroupFile groupData = TextFiles.ReadGroupsFile(availableGroups[i]);
                    lstAvailableGroups.Items.Add(Path.GetFileNameWithoutExtension(availableGroups[i]));
                    lvwGroups.Items.Add(new ListViewItem(new string[] { Path.GetFileNameWithoutExtension(availableGroups[i]), groupData.MaxVoices.ToString(), groupData.Priority.ToString() }));
                }
            }

            //Available SFXs
            DirectoryInfo sfxDir = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"));
            if (Directory.Exists(sfxDir.FullName))
            {
                string[] availableSfxFiles = Directory.GetFiles(sfxDir.FullName, "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < availableSfxFiles.Length; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(availableSfxFiles[i]);
                    SFX sfxData = TextFiles.ReadSfxFile(availableSfxFiles[i]);
                    lvwAvailable_SFXs.Items.Add(new ListViewItem(new string[] { fileName, sfxData.Parameters.MaxVoices.ToString(), Convert.ToBoolean(sfxData.Parameters.Action1).ToString(), sfxData.Parameters.Priority.ToString() }));
                }
                lblTotalSFXs.Text = string.Format("Total: {0}", lvwAvailable_SFXs.Items.Count);
            }

            //Select first group
            if (lstAvailableGroups.Items.Count > 0)
            {
                lstAvailableGroups.SelectedIndex = 0;
                lblGroupsCount.Text = string.Format("Total: {0}", lstAvailableGroups.Items.Count);
            }

            //Stop watcher
            watcher.Stop();
            txtBootupTime.Text = string.Format("Bootup Time =  {0:0.##############}", watcher.Elapsed.TotalSeconds);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void GroupingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm mainForm = ((MainForm)Application.OpenForms[nameof(MainForm)]);
            ProjectFileFunctions.UpdateDataBases(mainForm);
            ProjectFileFunctions.UpdateSoundBanks(mainForm);
        }

        //*===============================================================================================
        //* GROUPS SECTION EVENTS
        //*===============================================================================================
        private void LstAvailableGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string groupFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(groupFilePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(groupFilePath);

                    //Add dependencies
                    lvwSFXsInGroup.Items.Clear();
                    lvwSFXsInGroup.BeginUpdate();
                    for (int i = 0; i < groupFileData.Dependencies.Length; i++)
                    {
                        string sfxPath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", groupFileData.Dependencies[i] + ".txt");
                        if (File.Exists(sfxPath))
                        {
                            SFX sfxData = TextFiles.ReadSfxFile(sfxPath);
                            lvwSFXsInGroup.Items.Add(new ListViewItem(new string[] { groupFileData.Dependencies[i], sfxData.Parameters.MaxVoices.ToString(), Convert.ToBoolean(sfxData.Parameters.Action1).ToString(), sfxData.Parameters.Priority.ToString() }));
                        }
                    }
                    lvwSFXsInGroup.EndUpdate();
                    lblSFXsInGroup_Count.Text = string.Format("Total: {0}", lvwSFXsInGroup.Items.Count);

                    //Update Controls
                    nudMaxVoices.ValueChanged -= NudMaxVoices_ValueChanged;
                    nudMaxVoices.Value = Math.Min(Math.Max(nudMaxVoices.Minimum, groupFileData.MaxVoices), nudMaxVoices.Maximum);
                    nudMaxVoices.ValueChanged += NudMaxVoices_ValueChanged;

                    nudPriority.ValueChanged -= NudPriority_ValueChanged;
                    nudPriority.Value = groupFileData.Priority;
                    nudPriority.ValueChanged += NudPriority_ValueChanged;

                    chkDistanceWhenTesting.Checked = groupFileData.UseDistCheck;
                    if (groupFileData.Action1 == 0)
                    {
                        RadiobtnAction_Steal.Checked = true;
                    }
                    else
                    {
                        RadiobtnAction_Reject.Checked = true;
                    }
                }
            }
        }

        //*===============================================================================================
        //* AVAILABLE SFXs SECTION
        //*===============================================================================================
        private void NudMaxChannels_SFXs_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem itemToModify in lvwAvailable_SFXs.SelectedItems)
            {
                itemToModify.SubItems[1].Text = nudMaxChannels_SFXs.Value.ToString();

                //Update Sfx File Path
                string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToModify.Text + ".txt");
                if (File.Exists(sfxFilePath))
                {
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                    sfxData.Parameters.MaxVoices = (int)nudMaxChannels_SFXs.Value;
                    TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboAvailableSFXs_Steal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach (ListViewItem itemToModify in lvwAvailable_SFXs.SelectedItems)
            {
                itemToModify.SubItems[2].Text = cboAvailableSFXs_Steal.SelectedItem.ToString();

                //Update Sfx File Path
                string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToModify.Text + ".txt");
                if (File.Exists(sfxFilePath))
                {
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                    sfxData.Parameters.Action1 = Convert.ToByte(cboAvailableSFXs_Steal.SelectedIndex == 0);
                    TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwAvailable_SFXs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwAvailable_SFXs.SelectedItems.Count > 0)
            {
                nudMaxChannels_SFXs.ValueChanged -= NudMaxChannels_SFXs_ValueChanged;
                nudMaxChannels_SFXs.Value = int.Parse(lvwAvailable_SFXs.SelectedItems[0].SubItems[1].Text);
                nudMaxChannels_SFXs.ValueChanged += NudMaxChannels_SFXs_ValueChanged;

                cboAvailableSFXs_Steal.SelectedItem = lvwAvailable_SFXs.SelectedItems[0].SubItems[2].Text;
            }
        }

        //*===============================================================================================
        //* SFXs IN GROUP
        //*===============================================================================================
        private void NudMaxChannels_Group_ValueChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem itemToModify in lvwSFXsInGroup.SelectedItems)
            {
                itemToModify.SubItems[1].Text = nudMaxChannels_Group.Value.ToString();

                //Update Sfx File Again
                string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToModify.Text + ".txt");
                if (File.Exists(sfxFilePath))
                {
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                    sfxData.Parameters.MaxVoices = (int)nudMaxChannels_Group.Value;
                    TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CboSFXsInGroup_Steal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            foreach (ListViewItem itemToModify in lvwSFXsInGroup.SelectedItems)
            {
                itemToModify.SubItems[2].Text = cboSFXsInGroup_Steal.SelectedItem.ToString();

                //Update Sfx File Path
                string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToModify.Text + ".txt");
                if (File.Exists(sfxFilePath))
                {
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                    sfxData.Parameters.Action1 = Convert.ToByte(cboSFXsInGroup_Steal.SelectedIndex == 0);
                    TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void LvwSFXsInGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwSFXsInGroup.SelectedItems.Count > 0)
            {
                nudMaxChannels_Group.ValueChanged -= NudMaxChannels_Group_ValueChanged;
                nudMaxChannels_Group.Value = int.Parse(lvwSFXsInGroup.SelectedItems[0].SubItems[1].Text);
                nudMaxChannels_Group.ValueChanged += NudMaxChannels_Group_ValueChanged;

                cboSFXsInGroup_Steal.SelectedItem = lvwSFXsInGroup.SelectedItems[0].SubItems[2].Text;
            }
        }

        //*===============================================================================================
        //* BUTTON EVENTS
        //*===============================================================================================
        private void BtnNew_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New Group" })
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
                                lvwGroups.Items.Add(new ListViewItem(new string[] { fileName.ToString(), "0", "0" }));
                                lstAvailableGroups.Items.Add(fileName.ToString());
                                lblGroupsCount.Text = string.Format("Total: {0}", lvwGroups.Items.Count);
                                GroupFile groupData = new GroupFile();
                                TextFiles.WriteGroupsFile(groupData, filePath);

                                //Update counter
                                lblGroupsCount.Text = string.Format("Total: {0}", lstAvailableGroups.Items.Count);
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
        private void BtnRename_Click(object sender, EventArgs e)
        {
            //Ask user for a name
            if (lstAvailableGroups.SelectedItem != null)
            {
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename SFX Group" })
                {
                    inputDiag.lblText.Text = string.Format("Enter New Name For '{0}'", lstAvailableGroups.SelectedItem.ToString());
                    inputDiag.txtInputData.Text = lstAvailableGroups.SelectedItem.ToString();
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
                                string newFilePath = Path.Combine(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases"), fileName + ".txt");
                                if (File.Exists(newFilePath))
                                {
                                    MessageBox.Show("This HashCode Name is used. Pick another!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    //Rename file
                                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem.ToString() + ".txt");
                                    File.Move(source, newFilePath);

                                    //Update Control
                                    lstAvailableGroups.Items[lstAvailableGroups.SelectedIndex] = fileName;
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

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);

                    //Remove form listview
                    foreach (ListViewItem item in lvwGroups.Items)
                    {
                        if (item.Text.Equals(lstAvailableGroups.SelectedItem))
                        {
                            item.Remove();
                            break;
                        }
                    }

                    //Remove from listbox
                    lstAvailableGroups.Items.RemoveAt(lstAvailableGroups.Items.IndexOf(lstAvailableGroups.SelectedItem));

                    //Update counter
                    lblGroupsCount.Text = string.Format("Total: {0}", lstAvailableGroups.Items.Count);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAddSFXsToGroup_Click(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                //Read File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    HashSet<string> dependencies = new HashSet<string>(groupFileData.Dependencies);
                    foreach (ListViewItem itemToAdd in lvwAvailable_SFXs.SelectedItems)
                    {
                        dependencies.Add(itemToAdd.Text);
                    }

                    //Update File
                    string[] dependenciesArray = dependencies.ToArray();
                    Array.Sort(dependenciesArray);
                    groupFileData.Dependencies = dependenciesArray;

                    //Update Text Files and ListView
                    lvwSFXsInGroup.Items.Clear();
                    for (int i = 0; i < dependenciesArray.Length; i++)
                    {
                        //Update SFX Data
                        string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", dependenciesArray[i] + ".txt");
                        SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);

                        //Update UI
                        lvwSFXsInGroup.Items.Add(new ListViewItem(new string[] { dependenciesArray[i], sfxData.Parameters.MaxVoices.ToString(), Convert.ToBoolean(sfxData.Parameters.Action1).ToString(), sfxData.Parameters.Priority.ToString() }));
                    }

                    //Write File Again
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }

                //Update Labels
                lblSFXsInGroup_Count.Text = string.Format("Total: {0}", lvwSFXsInGroup.Items.Count);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemoveSFXsFromGroup_Click(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                HashSet<string> dependencies = new HashSet<string>();
                foreach (ListViewItem itemToRemove in lvwSFXsInGroup.SelectedItems)
                {
                    itemToRemove.Remove();
                }
                foreach (ListViewItem itemToRemove in lvwSFXsInGroup.Items)
                {
                    dependencies.Add(itemToRemove.Text);
                }

                //Read and update file
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    groupFileData.Dependencies = dependencies.ToArray();
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }

                //Update Labels
                lblSFXsInGroup_Count.Text = string.Format("Total: {0}", lvwSFXsInGroup.Items.Count);
                lblTotalSFXs.Text = string.Format("Total: {0}", lvwAvailable_SFXs.Items.Count);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudMaxVoices_ValueChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    groupFileData.MaxVoices = (int)nudMaxVoices.Value;
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }

                //Update Sfx File Path
                foreach (ListViewItem listItem in lvwSFXsInGroup.Items)
                {
                    listItem.SubItems[1].Text = nudMaxVoices.Value.ToString();
                    string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", listItem.Text + ".txt");
                    if (File.Exists(sfxFilePath))
                    {
                        SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                        sfxData.Parameters.MaxVoices = (int)nudMaxVoices.Value;
                        TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                    }
                }

                //Update UI
                ListViewItem itemToModify = lvwGroups.FindItemWithText(lstAvailableGroups.SelectedItem.ToString());
                if (itemToModify != null)
                {
                    itemToModify.SubItems[1].Text = nudMaxVoices.Value.ToString();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void NudPriority_ValueChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    groupFileData.Priority = (int)nudPriority.Value;
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }

                //Update Sfx File Path
                foreach (ListViewItem listItem in lvwSFXsInGroup.Items)
                {
                    listItem.SubItems[3].Text = nudPriority.Value.ToString();
                    string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", listItem.Text + ".txt");
                    if (File.Exists(sfxFilePath))
                    {
                        SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                        sfxData.Parameters.Priority = (int)nudPriority.Value;
                        TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                    }
                }

                //Update UI
                ListViewItem itemToModify = lvwGroups.FindItemWithText(lstAvailableGroups.SelectedItem.ToString());
                if (itemToModify != null)
                {
                    itemToModify.SubItems[2].Text = nudPriority.Value.ToString();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkDistanceWhenTesting_CheckedChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    groupFileData.UseDistCheck = chkDistanceWhenTesting.Checked;
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void RadiobtnAction_Steal_CheckedChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    if (RadiobtnAction_Steal.Checked)
                    {
                        groupFileData.Action1 = 0;
                    }
                    else
                    {
                        groupFileData.Action1 = 1;
                    }
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void RadiobtnAction_Reject_CheckedChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                    if (RadiobtnAction_Reject.Checked)
                    {
                        groupFileData.Action1 = 1;
                    }
                    else
                    {
                        groupFileData.Action1 = 0;
                    }
                    TextFiles.WriteGroupsFile(groupFileData, filePath);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
