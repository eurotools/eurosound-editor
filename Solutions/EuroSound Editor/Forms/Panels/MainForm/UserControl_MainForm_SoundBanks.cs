using ESUtils;
using sb_editor.Forms;
using sb_editor.Objects;
using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_Mainform_SoundBanks : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_Mainform_SoundBanks()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* TREE VIEW
        //*===============================================================================================
        private void TvwSoundBanks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Update label
            if (e.Node.Level == 0 && !e.Node.IsExpanded)
            {
                //Load SoundBank Data
                string soundBankFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", e.Node.Text + ".txt");
                if (File.Exists(soundBankFilePath))
                {
                    //Add DataBases
                    AddDataBases(e.Node, TextFiles.ReadListBlock(soundBankFilePath, "#DEPENDENCIES"));
                    if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Name.Equals("Empty"))
                    {
                        lblDataBases_Total.Text = "DB Total: 0";
                    }
                    else
                    {
                        lblDataBases_Total.Text = string.Join(" ", "DB Total:", e.Node.Nodes.Count);
                    }

                    //Update Checklistbox
                    MainForm mainForm = (MainForm)Application.OpenForms[nameof(MainForm)];
                    if (mainForm != null)
                    {
                        CheckedListBox checkListBox = mainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks;
                        for (int i = 0; i < checkListBox.Items.Count; i++)
                        {
                            CheckState itemState = CheckState.Unchecked;
                            if (i == e.Node.Index)
                            {
                                itemState = CheckState.Checked;
                            }
                            checkListBox.SetItemCheckState(i, itemState);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File Not Found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.Node.Level > 0) // Update Databases count
            {
                if (e.Node.Parent.Nodes.Count == 1 && e.Node.Parent.Nodes[0].Name.Equals("Empty"))
                {
                    lblDataBases_Total.Text = "DB Total: 0";
                }
                else
                {
                    lblDataBases_Total.Text = string.Join(" ", "DB Total:", e.Node.Parent.Nodes.Count);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TvwSoundBanks_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvwSoundBanks.SelectedNode = e.Node;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TvwSoundBanks_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            //Folder closed
            if (e.Node.Level == 0)
            {
                e.Node.SelectedImageIndex = 0;
                e.Node.ImageIndex = 0;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TvwSoundBanks_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //Folder closed
            if (e.Node.Level == 0)
            {
                e.Node.SelectedImageIndex = 1;
                e.Node.ImageIndex = 1;

                //Collapse other nodes
                foreach (TreeNode node in tvwSoundBanks.Nodes)
                {
                    if (node != e.Node)
                    {
                        node.Collapse();
                        node.Nodes.Clear();
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TvwSoundBanks_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level > 0 && e.Node.IsSelected)
            {
                MainForm mainForm = (MainForm)Application.OpenForms[nameof(MainForm)];
                ListBox listbox = mainForm.UserControl_Available_Databases.lstDataBases;

                int itemIndex = listbox.FindString(e.Node.Text);
                if (itemIndex != ListBox.NoMatches)
                {
                    listbox.SelectedIndices.Clear();
                    listbox.SelectedIndex = itemIndex;
                }
            }
        }

        //*===============================================================================================
        //* CONTEXT MENU TREE VIEW
        //*===============================================================================================
        private void MnuNew_SoundBank_Click(object sender, System.EventArgs e)
        {
            //Ask user for a name
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New SoundBank" })
            {
                string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                inputDiag.lblText.Text = "Enter Name";
                inputDiag.txtInputData.Text = MultipleFilesFunctions.GetNextAvailableFilename(folderPath, "SB_Label");
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
                                string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
                                if (File.Exists(projectPropertiesFile))
                                {
                                    ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                                    //Check if we have memory maps
                                    if (projectSettings.MemoryMaps.Count > 0)
                                    {
                                        //Create new soundbank file
                                        SoundBank soundBankFile = new SoundBank
                                        {
                                            HashCode = GlobalPrefs.SoundBankHashCodeNumber,
                                            DataBases = new string[0],
                                            MemoryMap = projectSettings.MemoryMaps[projectSettings.DefaultMemMap]
                                        };
                                        TextFiles.WriteSoundBankFile(filePath, soundBankFile);
                                        GlobalPrefs.SoundBankHashCodeNumber++;

                                        //Update UI
                                        ProjectFileFunctions.UpdateSoundBanks((MainForm)Parent);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Must Setup Memory Maps first!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
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
        private void MnuCopy_SoundBank_Click(object sender, System.EventArgs e)
        {
            if (tvwSoundBanks.SelectedNode != null)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Copy SoundBank" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                    inputDiag.lblText.Text = string.Format("Enter New Name For SoundBank {0}", tvwSoundBanks.SelectedNode.Text);
                    inputDiag.txtInputData.Text = tvwSoundBanks.SelectedNode.Text;
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
                                    string filePath = Path.Combine(folderPath, tvwSoundBanks.SelectedNode.Text + ".txt");
                                    File.Copy(filePath, newFilePath);

                                    //Copy node
                                    TreeNode clonedSoundBank = (TreeNode)tvwSoundBanks.SelectedNode.Clone();
                                    clonedSoundBank.Text = fileName;
                                    clonedSoundBank.Name = fileName;

                                    //Add node again
                                    tvwSoundBanks.Nodes.Add(clonedSoundBank);
                                    tvwSoundBanks.Sort();
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
        private void MnuDelete_SoundBank_Click(object sender, System.EventArgs e)
        {
            if (tvwSoundBanks.SelectedNode != null)
            {
                if (tvwSoundBanks.SelectedNode.Level == 0)
                {
                    if (MessageBox.Show(string.Format("Are you sure you want delete SoundBank(s)\n'{0}'\nTotal Files: {1}", tvwSoundBanks.SelectedNode.Text, 1), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Create trash folder if not exists
                        string trashFolder = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks_Trash");
                        Directory.CreateDirectory(trashFolder);

                        string source = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", tvwSoundBanks.SelectedNode.Text + ".txt");
                        string destination = Path.Combine(trashFolder, tvwSoundBanks.SelectedNode.Text + ".txt");

                        //Ensure that the file doesn't exists
                        File.Delete(destination);
                        File.Move(source, destination);

                        //Update UI
                        ProjectFileFunctions.UpdateAll((MainForm)Parent);
                    }
                }
                else if (tvwSoundBanks.SelectedNode.Level > 0 && !tvwSoundBanks.SelectedNode.Name.Equals("Empty")) // DataBase Level
                {
                    //Get file path
                    TreeNode soundBankNode = tvwSoundBanks.SelectedNode.Parent;
                    string soundBankPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", soundBankNode.Text + ".txt");

                    //Remove database
                    int selectedIndex = tvwSoundBanks.SelectedNode.Index;
                    soundBankNode.Nodes.Remove(tvwSoundBanks.SelectedNode);

                    //Read and update soundbank
                    SoundBank soundBankData = TextFiles.ReadSoundbankFile(soundBankPath);
                    soundBankData.DataBases = GetChildNodeTextValues(soundBankNode);
                    TextFiles.WriteSoundBankFile(soundBankPath, soundBankData);

                    //Update Node
                    AddDataBases(soundBankNode, soundBankData.DataBases, true);

                    //Select previous index
                    if (selectedIndex < soundBankNode.Nodes.Count)
                    {
                        tvwSoundBanks.SelectedNode = soundBankNode.Nodes[selectedIndex];
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private string[] GetChildNodeTextValues(TreeNode node)
        {
            TreeNodeCollection childNodes = node.Nodes;

            //Fill array
            string[] childNodeTextValues = new string[childNodes.Count];
            for (int i = 0; i < childNodes.Count; i++)
            {
                childNodeTextValues[i] = (childNodes[i].Text);
            }

            //Sort array
            Array.Sort(childNodeTextValues);

            return childNodeTextValues;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRename_SoundBank_Click(object sender, System.EventArgs e)
        {
            if (tvwSoundBanks.SelectedNode != null)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename SoundBank" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                    inputDiag.lblText.Text = string.Format("Enter New Name For SoundBank {0}", tvwSoundBanks.SelectedNode.Text);
                    inputDiag.txtInputData.Text = tvwSoundBanks.SelectedNode.Text;
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
                                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", tvwSoundBanks.SelectedNode.Text + ".txt");
                                    File.Move(source, newFilePath);
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
        private void MnuProperties_SoundBank_Click(object sender, System.EventArgs e)
        {
            //Get soundbank
            if (tvwSoundBanks.SelectedNode != null)
            {
                string soundBankName = tvwSoundBanks.SelectedNode.Text;
                if (tvwSoundBanks.SelectedNode.Level > 0)
                {
                    soundBankName = tvwSoundBanks.SelectedNode.Parent.Text;
                }

                //Display properties form
                string soundBankpath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", soundBankName + ".txt");
                ComboBox selectedPlatform = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output.cboOutputFormat;
                ComboBox selectedLanguage = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output.cboOutputLanguage;

                //Ensure that we selected a platform and that the sb file exists
                if (File.Exists(soundBankpath) && selectedPlatform.SelectedItem != null)
                {
                    //Get the output Language
                    string outputLanguage = "English";
                    if (!string.IsNullOrEmpty(selectedLanguage.Text))
                    {
                        outputLanguage = selectedLanguage.Text;
                    }

                    //Display Form
                    using (SoundBankPropertiesForm soundBankProperties = new SoundBankPropertiesForm(soundBankpath, selectedPlatform.Text, (Enumerations.Language)Enum.Parse(typeof(Enumerations.Language), outputLanguage, true)))
                    {
                        soundBankProperties.ShowDialog();
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuMaxOutSize_SoundBank_Click(object sender, System.EventArgs e)
        {
            if (tvwSoundBanks.SelectedNode != null)
            {
                string soundBankName = tvwSoundBanks.SelectedNode.Text;
                if (tvwSoundBanks.SelectedNode.Level > 0)
                {
                    soundBankName = tvwSoundBanks.SelectedNode.Parent.Text;
                }
                using (SetMaxBankSizeForm maxBankSize = new SetMaxBankSizeForm(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", soundBankName + ".txt")))
                {
                    maxBankSize.ShowDialog();
                }
            }
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        internal void LoadSoundBanks(CheckedListBox soundBankCheckedList)
        {
            string soundBanksFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Soundbanks");
            if (Directory.Exists(soundBanksFilePath))
            {
                //Get the name of the expanded node to restore the selection
                string expandedNodeName = string.Empty;
                if (tvwSoundBanks.Nodes.Count > 0)
                {
                    //Exit at the first match
                    foreach (TreeNode node in tvwSoundBanks.Nodes)
                    {
                        if (node.IsExpanded)
                        {
                            expandedNodeName = node.Name;
                            break;
                        }
                    }

                    //Clear all soundbanks
                    tvwSoundBanks.Nodes.Clear();
                }

                //Add Soundbanks
                for (int i = 0; i < soundBankCheckedList.Items.Count; i++)
                {
                    string soundBankName = soundBankCheckedList.Items[i].ToString();
                    TreeNode sbNode = tvwSoundBanks.Nodes.Add(soundBankName, soundBankName, 0, 0);
                    if (soundBankCheckedList.GetItemChecked(i))
                    {
                        tvwSoundBanks.SelectedNode = sbNode;

                        //Restore the expanded node if exists
                        if (sbNode.Name.Equals(expandedNodeName))
                        {
                            sbNode.Expand();
                        }
                    }
                }

                //Update label
                lblSoundBanksTotal.Text = string.Join(" ", "SB Total:", tvwSoundBanks.Nodes.Count);
                lblDataBases_Total.Text = "DB Total: 0";
                EnableOrDisableButton();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void AddDataBases(TreeNode soundBankNode, string[] dependencies, bool autoExpand = false)
        {
            //Clear Current Dependencies
            if (soundBankNode.Nodes.Count > 0)
            {
                soundBankNode.Nodes.Clear();
            }

            //Add Dependencies
            if (dependencies != null && dependencies.Length > 0)
            {
                for (int j = 0; j < dependencies.Length; j++)
                {
                    soundBankNode.Nodes.Add(dependencies[j], dependencies[j], 2, 2);
                }
                if (autoExpand)
                {
                    soundBankNode.Expand();
                }
            }
            else
            {
                soundBankNode.Nodes.Add("Empty", "Empty SoundBank", 3, 3);
            }

            // Update Label
            lblDataBases_Total.Text = string.Join(" ", "DB Total:", dependencies.Length);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void EnableOrDisableButton()
        {
            lblSoundBanksTutorial.Visible = !(tvwSoundBanks.Nodes.Count > 0);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
