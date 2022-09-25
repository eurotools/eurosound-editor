using EuroSound_Editor.Forms;
using EuroSound_Editor.Objects;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace EuroSound_Editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_Manform_SoundBanks : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_Manform_SoundBanks()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* TREE VIEW
        //*===============================================================================================
        private void TvwSoundBanks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Update label
            if (e.Node.Level == 0)
            {
                //Load SoundBank Data
                string soundBankFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", e.Node.Text + ".txt");
                if (File.Exists(soundBankFilePath))
                {
                    //Add DataBases
                    AddDataBases(e.Node, TextFiles.ReadListBlock(soundBankFilePath, "#DEPENDENCIES"));
                    lblDataBases_Total.Text = string.Join(" ", "DB Total:", e.Node.Nodes.Count);

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
            else
            {
                lblDataBases_Total.Text = string.Join(" ", "DB Total:", e.Node.Parent.Nodes.Count);
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
            using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Create New" })
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
                                //Create new soundbank file
                                SoundBank soundBankFile = new SoundBank
                                {
                                    HashCode = GlobalPrefs.SoundBankHashCodeNumber,
                                    DataBases = new string[0]
                                };
                                TextFiles.WriteSoundBankFile(filePath, soundBankFile);
                                GlobalPrefs.SoundBankHashCodeNumber++;

                                //Update UI
                                ProjectFileFunctions.UpdateSoundBanks((MainForm)Parent);
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
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Copy Sound Bank" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                    inputDiag.lblText.Text = string.Format("Enter New Name For Sound Bank {0}", tvwSoundBanks.SelectedNode.Text);
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
            if (tvwSoundBanks.SelectedNode != null && tvwSoundBanks.SelectedNode.Level == 0)
            {
                if (MessageBox.Show(string.Format("Are you sure you want delete Sound Bank(s)\n'{0}'\nTotal Files: {1}", tvwSoundBanks.SelectedNode.Text, 1), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuRename_SoundBank_Click(object sender, System.EventArgs e)
        {
            if (tvwSoundBanks.SelectedNode != null)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename Sound Bank" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                    inputDiag.lblText.Text = string.Format("Enter New Name For Sound Bank {0}", tvwSoundBanks.SelectedNode.Text);
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
                                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", tvwSoundBanks.SelectedNode.Text);
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
                ComboBox outputPlatform = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output.cboOutputFormat;
                ComboBox outputLanguage = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output.cboOutputLanguage;

                if (File.Exists(soundBankpath) && outputPlatform.SelectedItem != null)
                {
                    SoundBankPropertiesForm soundBankProperties = new SoundBankPropertiesForm(soundBankpath, outputPlatform.Text, outputLanguage.Text);
                    soundBankProperties.ShowDialog();
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
                //Clear soundbanks
                if (tvwSoundBanks.Nodes.Count > 0)
                {
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
                        if (Visible)
                        {
                            sbNode.Expand();
                        }
                    }
                }

                //Update label
                lblSoundBanksTotal.Text = string.Join(" ", "SB Total:", tvwSoundBanks.Nodes.Count);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void AddDataBases(TreeNode soundBankNode, string[] dependencies)
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
            }
            else
            {
                soundBankNode.Nodes.Add("Empty", "Empty Sound Bank", 3, 3);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
