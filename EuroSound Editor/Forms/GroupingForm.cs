using EuroSound_Editor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EuroSound_Editor.Forms
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

            List<string> usedSfx = new List<string>();

            //Groups
            DirectoryInfo groupsDir = Directory.CreateDirectory(Path.Combine(GlobalPrefs.ProjectFolder, "Groups"));
            if (Directory.Exists(groupsDir.FullName))
            {
                string[] availableGroups = Directory.GetFiles(groupsDir.FullName, "*.txt", SearchOption.AllDirectories);
                for (int i = 0; i < availableGroups.Length; i++)
                {
                    GroupFile groupData = TextFiles.ReadGroupsFile(availableGroups[i]);
                    for (int j = 0; j < groupData.Dependencies.Length; j++)
                    {
                        usedSfx.Add(groupData.Dependencies[j]);
                    }
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
                    if (usedSfx.IndexOf(fileName) == -1)
                    {
                        SFX sfxData = TextFiles.ReadSfxFile(availableSfxFiles[i]);
                        lvwAvailable_SFXs.Items.Add(new ListViewItem(new string[] { fileName, sfxData.Parameters.MaxVoices.ToString(), Convert.ToBoolean(sfxData.Parameters.Action1).ToString() }));
                    }
                }
                lblTotalSFXs.Text = string.Format("Total: {0}", lvwAvailable_SFXs.Items.Count);
            }

            //Stop watcher
            watcher.Stop();
            txtBootupTime.Text = string.Format("Bootup Time =  {0:0.##############}", watcher.Elapsed.TotalSeconds);
        }

        //*===============================================================================================
        //* GROUPS SECTION EVENTS
        //*===============================================================================================
        private void LstAvailableGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string groupFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(groupFilePath))
                {
                    GroupFile groupFileData = TextFiles.ReadGroupsFile(groupFilePath);
                    nudMaxVoices.Value = groupFileData.MaxVoices;
                    nudPriority.Value = groupFileData.Priority;
                    chkDistanceWhenTesting.Checked = groupFileData.UseDistCheck;
                    if (groupFileData.Action1 == 0)
                    {
                        RadiobtnAction_Steal.Checked = true;
                    }
                    else
                    {
                        RadiobtnAction_Reject.Checked = true;
                    }

                    //Add dependencies
                    lvwSFXsInGroup.Items.Clear();
                    lvwSFXsInGroup.BeginUpdate();
                    for (int i = 0; i < groupFileData.Dependencies.Length; i++)
                    {
                        SFX sfxData = TextFiles.ReadSfxFile(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", groupFileData.Dependencies[i] + ".txt"));
                        lvwSFXsInGroup.Items.Add(new ListViewItem(new string[] { groupFileData.Dependencies[i], sfxData.Parameters.MaxVoices.ToString(), Convert.ToBoolean(sfxData.Parameters.Action1).ToString() }));
                    }
                    lvwSFXsInGroup.EndUpdate();
                    lblSFXsInGroup_Count.Text = string.Format("Total: {0}", lvwSFXsInGroup.Items.Count);
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
                SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                sfxData.Parameters.MaxVoices = (int)nudMaxChannels_SFXs.Value;

                //Write File Again
                TextFiles.WriteSfxFile(sfxFilePath, sfxData);
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
                SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                sfxData.Parameters.Action1 = Convert.ToByte(cboAvailableSFXs_Steal.SelectedIndex == 0);

                //Write File Again
                TextFiles.WriteSfxFile(sfxFilePath, sfxData);
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

                //Update Sfx File Path
                string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToModify.Text + ".txt");
                SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                sfxData.Parameters.MaxVoices = (int)nudMaxChannels_Group.Value;

                //Write File Again
                TextFiles.WriteSfxFile(sfxFilePath, sfxData);
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
                SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                sfxData.Parameters.Action1 = Convert.ToByte(cboSFXsInGroup_Steal.SelectedIndex == 0);

                //Write File Again
                TextFiles.WriteSfxFile(sfxFilePath, sfxData);
            }
        }

        //*===============================================================================================
        //* BUTTON EVENTS
        //*===============================================================================================
        private void BtnNew_Click(object sender, EventArgs e)
        {
            int fileName = 0;
            string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", fileName + ".txt");
            while (File.Exists(filePath))
            {
                fileName++;
                filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", fileName + ".txt");
            }
            lvwGroups.Items.Add(new ListViewItem(new string[] { fileName.ToString(), "0", "0" }));
            lstAvailableGroups.Items.Add(fileName.ToString());
            lblGroupsCount.Text = string.Format("Total: {0}", lvwGroups.Items.Count);
            TextFiles.WriteGroupsFile(new GroupFile(), filePath);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRename_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    lstAvailableGroups.Items.RemoveAt(lstAvailableGroups.Items.IndexOf(lstAvailableGroups.SelectedItem));
                    lvwGroups.SelectedItems[0].Remove();
                    lblGroupsCount.Text = string.Format("Total: {0}", lvwGroups.Items.Count);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAddSFXsToGroup_Click(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                //Read File
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                HashSet<string> dependencies = new HashSet<string>(groupFileData.Dependencies);
                foreach (ListViewItem itemToAdd in lvwAvailable_SFXs.SelectedItems)
                {
                    dependencies.Add(itemToAdd.Text);
                    itemToAdd.Remove();
                }

                //Update File
                string[] dependenciesArray = dependencies.ToArray();
                Array.Sort(dependenciesArray);
                groupFileData.Dependencies = dependenciesArray;

                //Update Text Files and ListView
                lvwSFXsInGroup.Items.Clear();
                for (int i = 0; i < dependenciesArray.Length; i++)
                {
                    string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", dependenciesArray[i] + ".txt");
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                    sfxData.Parameters.Group = int.Parse(lstAvailableGroups.SelectedItem.ToString());
                    TextFiles.WriteSfxFile(sfxFilePath, sfxData);

                    //Update UI
                    lvwSFXsInGroup.Items.Add(new ListViewItem(new string[] { dependenciesArray[i], sfxData.Parameters.MaxVoices.ToString(), Convert.ToBoolean(sfxData.Parameters.Action1).ToString() }));
                }

                //Write File Again
                TextFiles.WriteGroupsFile(groupFileData, filePath);

                //Update Labels
                lblSFXsInGroup_Count.Text = string.Format("Total: {0}", lvwSFXsInGroup.Items.Count);
                lblTotalSFXs.Text = string.Format("Total: {0}", lvwAvailable_SFXs.Items.Count);
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
                    lvwAvailable_SFXs.Items.Add(itemToRemove);

                    //Update Text File
                    string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", itemToRemove.Text + ".txt");
                    SFX sfxData = TextFiles.ReadSfxFile(sfxFilePath);
                    sfxData.Parameters.Group = 0;
                    TextFiles.WriteSfxFile(sfxFilePath, sfxData);
                }
                foreach (ListViewItem itemToRemove in lvwSFXsInGroup.Items)
                {
                    dependencies.Add(itemToRemove.Text);
                }

                //Read and update file
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                groupFileData.Dependencies = dependencies.ToArray();

                //Write File Again
                TextFiles.WriteGroupsFile(groupFileData, filePath);

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
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                groupFileData.MaxVoices = (int)nudMaxVoices.Value;
                TextFiles.WriteGroupsFile(groupFileData, filePath);

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
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                groupFileData.Priority = (int)nudPriority.Value;
                TextFiles.WriteGroupsFile(groupFileData, filePath);

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
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
                GroupFile groupFileData = TextFiles.ReadGroupsFile(filePath);
                groupFileData.UseDistCheck = chkDistanceWhenTesting.Checked;
                TextFiles.WriteGroupsFile(groupFileData, filePath);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void RadiobtnAction_Steal_CheckedChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
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

        //-------------------------------------------------------------------------------------------------------------------------------
        private void RadiobtnAction_Reject_CheckedChanged(object sender, EventArgs e)
        {
            if (lstAvailableGroups.SelectedItems.Count == 1)
            {
                string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "Groups", lstAvailableGroups.SelectedItem + ".txt");
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

    //-------------------------------------------------------------------------------------------------------------------------------
}
