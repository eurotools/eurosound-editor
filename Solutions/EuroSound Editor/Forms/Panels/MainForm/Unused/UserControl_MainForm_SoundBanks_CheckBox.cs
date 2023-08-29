//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Available SoundBanks Panel
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace sb_editor.Panels
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class UserControl_MainForm_SoundBanks_CheckBox : UserControl
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public UserControl_MainForm_SoundBanks_CheckBox()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] LoadSoundBanks()
        {
            List<string> LoadedSoundBanks = new List<string>();
            string soundBanksFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Soundbanks");
            if (Directory.Exists(soundBanksFilePath))
            {
                //Get previous selection
                int prevSelection = -1;
                for (int i = 0; i < cbllstSoundbanks.Items.Count; i++)
                {
                    if (cbllstSoundbanks.GetItemChecked(i))
                    {
                        prevSelection = i;
                        break;
                    }
                }
                cbllstSoundbanks.Items.Clear();

                //Load files again
                string[] soundBankFiles = Directory.GetFiles(soundBanksFilePath, "*.txt", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < soundBankFiles.Length; i++)
                {
                    string soundBankName = Path.GetFileNameWithoutExtension(soundBankFiles[i]);
                    //Add item to checklistbox
                    cbllstSoundbanks.Items.Add(soundBankName);
                    LoadedSoundBanks.Add(soundBankName);
                }

                //Restore last selection
                if (prevSelection == -1)
                {
                    string iniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                    if (File.Exists(iniFilePath))
                    {
                        IniFile iniFile = new IniFile(iniFilePath);
                        for (int i = 0; i < cbllstSoundbanks.Items.Count; i++)
                        {
                            if (iniFile.Read(i.ToString(), "Form1_List1_CHECKBOXS").Equals("True", StringComparison.OrdinalIgnoreCase))
                            {
                                cbllstSoundbanks.SetItemCheckState(i, CheckState.Checked);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(string.Format("File Not Found '{0}'", iniFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (cbllstSoundbanks.Items.Count > prevSelection)
                {
                    cbllstSoundbanks.SetItemChecked(prevSelection, true);
                }
            }

            //Sort list
            LoadedSoundBanks.Sort();

            return LoadedSoundBanks.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSelect_Clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbllstSoundbanks.Items.Count; i++)
            {
                cbllstSoundbanks.SetItemChecked(i, false);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSelect_Invert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbllstSoundbanks.Items.Count; i++)
            {
                bool itemState = cbllstSoundbanks.GetItemChecked(i);
                cbllstSoundbanks.SetItemChecked(i, !itemState);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSelect_All_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbllstSoundbanks.Items.Count; i++)
            {
                cbllstSoundbanks.SetItemChecked(i, true);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Checked_lstSoundbanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbllstSoundbanks.SelectedItem != null)
            {
                lblBankNumber.Text = string.Format("Bank No: {0}", cbllstSoundbanks.SelectedIndex);

                //Add data to control
                UserControl_MainForm_DataBasesInSoundBank dataBasesControl = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_DataBasesInSoundBank;
                SoundBank soundBankFile = TextFiles.ReadSoundbankFile(Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", cbllstSoundbanks.SelectedItem + ".txt"));

                //Add items to listview
                dataBasesControl.lstDatabases.Items.Clear();
                dataBasesControl.lstDatabases.Items.AddRange(soundBankFile.DataBases);
            }
        }

        //*===============================================================================================
        //* CONTEXT MENU
        //*===============================================================================================
        private void MnuNew_Click(object sender, EventArgs e)
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

                                //Write file
                                TextFiles.WriteSoundBankFile(filePath, soundBankFile);

                                //Update UI
                                cbllstSoundbanks.Items.Add(fileName);

                                //Update global variable 
                                GlobalPrefs.SoundBankHashCodeNumber++;
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
        private void MnuCopy_Click(object sender, EventArgs e)
        {
            if (cbllstSoundbanks.SelectedItem != null)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Copy SoundBank" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                    inputDiag.lblText.Text = string.Format("Enter New Name For SoundBank {0}", cbllstSoundbanks.SelectedItem);
                    inputDiag.txtInputData.Text = cbllstSoundbanks.SelectedItem.ToString();
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
                                    string filePath = Path.Combine(folderPath, cbllstSoundbanks.SelectedItem + ".txt");
                                    File.Copy(filePath, newFilePath);

                                    //Add cloned item
                                    cbllstSoundbanks.Items.Add(fileName);
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
        private void MnuDelete_Click(object sender, EventArgs e)
        {
            if (cbllstSoundbanks.SelectedItem != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want delete SoundBank(s)\n'{0}'\nTotal Files: {1}", cbllstSoundbanks.SelectedItem, 1), Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Create trash folder if not exists
                    string trashFolder = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks_Trash");
                    Directory.CreateDirectory(trashFolder);

                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", cbllstSoundbanks.SelectedItem + ".txt");
                    string destination = Path.Combine(trashFolder, cbllstSoundbanks.SelectedItem + ".txt");

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
        private void MnuRename_Click(object sender, EventArgs e)
        {
            if (cbllstSoundbanks.SelectedItem != null)
            {
                //Ask user for a name
                using (Frm_InputBox inputDiag = new Frm_InputBox() { Text = "Rename SoundBank" })
                {
                    string folderPath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks");

                    inputDiag.lblText.Text = string.Format("Enter New Name For SoundBank {0}", cbllstSoundbanks.SelectedItem);
                    inputDiag.txtInputData.Text = cbllstSoundbanks.SelectedItem.ToString();
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
                                    string source = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", cbllstSoundbanks.SelectedItem + ".txt");
                                    File.Move(source, newFilePath);

                                    //Update UI
                                    cbllstSoundbanks.Items[cbllstSoundbanks.SelectedIndex] = fileName;
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
        private void MnuProperties_Click(object sender, EventArgs e)
        {
            //Get soundbank
            if (cbllstSoundbanks.SelectedItem != null)
            {
                //Display properties form
                string soundBankpath = Path.Combine(GlobalPrefs.ProjectFolder, "SoundBanks", cbllstSoundbanks.SelectedItem + ".txt");
                ComboBox outputPlatform = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output.cboOutputFormat;
                ComboBox outputLanguage = ((MainForm)Application.OpenForms[nameof(MainForm)]).UserControl_Output.cboOutputLanguage;

                if (File.Exists(soundBankpath) && outputPlatform.SelectedItem != null)
                {
                    SoundBankPropertiesForm soundBankProperties = new SoundBankPropertiesForm(soundBankpath, outputPlatform.Text, (Enumerations.Language)Enum.Parse(typeof(Enumerations.Language), outputLanguage.Text, true));
                    soundBankProperties.ShowDialog();
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void MnuMaxOutputSize_Click(object sender, EventArgs e)
        {

        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
