using EuroSound_Editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EuroSound_Editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MultiCreate : Form
    {
        private readonly Dictionary<string, List<string>> HashCodesToAdd = new Dictionary<string, List<string>>();

        //-------------------------------------------------------------------------------------------------------------------------------
        public MultiCreate()
        {
            InitializeComponent();
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_NewMultipleSfx_Load(object sender, EventArgs e)
        {
            string iniFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
            if (File.Exists(iniFilePath))
            {
                IniFile systemIni = new IniFile(iniFilePath);
                chkForceUpperCase.Checked = systemIni.Read("Check1_Value", "Form11_Misc").Equals("1");
                chkRandomSeq.Checked = systemIni.Read("Check2_Value", "Form11_Misc").Equals("1");
                txtHashCode_Prefix.Text = systemIni.Read("Text1_Text", "Form11_Misc");
            }

            //Ask for Samples
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lstSampleFiles.Items.AddRange(openFileDialog.FileNames);
                GetHashCodeNames();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_NewMultipleSfx_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Update INI
            IniFile systemIni = new IniFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini"));
            systemIni.Write("Check1_Value", Convert.ToByte(chkForceUpperCase.Checked).ToString(), "Form11_Misc");
            systemIni.Write("Check2_Value", Convert.ToByte(chkRandomSeq.Checked).ToString(), "Form11_Misc");
            systemIni.Write("Text1_Text", txtHashCode_Prefix.Text, "Form11_Misc");

            //Update Project
            ProjectFileFunctions.UpdateAll((MainForm)Application.OpenForms[nameof(MainForm)]);
        }

        //*===============================================================================================
        //* OPTIONS
        //*===============================================================================================
        private void ChkForceUpperCase_CheckStateChanged(object sender, EventArgs e)
        {
            GetHashCodeNames();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkRandomSeq_CheckedChanged(object sender, EventArgs e)
        {
            GetHashCodeNames();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TxtHashCode_Prefix_TextChanged(object sender, EventArgs e)
        {
            //Update UI
            GetHashCodeNames();
        }

        //*===============================================================================================
        //* BUTTONS
        //*===============================================================================================
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                lstSampleFiles.Items.AddRange(openFileDialog.FileNames);
                GetHashCodeNames();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            while (lstSampleFiles.SelectedItems.Count > 0)
            {
                lstSampleFiles.Items.Remove(lstSampleFiles.SelectedItems[0]);
            }
            GetHashCodeNames();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOK_Click(object sender, EventArgs e)
        {
            string defaultsFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "SFX Defaults.txt");
            if (File.Exists(defaultsFile))
            {
                string systemIniPath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                if (File.Exists(systemIniPath))
                {
                    //Get default Samples
                    IniFile systemIni = new IniFile(systemIniPath);
                    decimal PitchOffset = 0, RandomPitch = 0;
                    sbyte BaseVolume = 0, RandomVolume = 0, Pan = 0, RandomPan = 0;
                    if (decimal.TryParse(systemIni.Read("DTextNIndex_0", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out decimal PitchOffsetParsed))
                    {
                        PitchOffset = PitchOffsetParsed;
                    }
                    if (decimal.TryParse(systemIni.Read("DTextNIndex_1", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out decimal RandomPitchParsed))
                    {
                        RandomPitch = RandomPitchParsed;
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_2", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte BaseVolumeParsed))
                    {
                        BaseVolume = BaseVolumeParsed;
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_3", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte RandomVolumeParsed))
                    {
                        RandomVolume = RandomVolumeParsed;
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_4", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte PanParsed))
                    {
                        Pan = PanParsed;
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_5", "SFXForm"), System.Globalization.NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte RandomPanParsed))
                    {
                        RandomPan = RandomPanParsed;
                    }

                    //Create SFXs
                    foreach (KeyValuePair<string, List<string>> HashCodeToCheck in HashCodesToAdd)
                    {
                        SFX fileData = TextFiles.ReadSfxFile(defaultsFile);
                        foreach (string sampleToCheck in HashCodeToCheck.Value)
                        {
                            SfxSample sampleToAdd = new SfxSample
                            {
                                FilePath = sampleToCheck.Substring(Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master").Length),
                                PitchOffset = PitchOffset,
                                RandomPitch = RandomPitch,
                                BaseVolume = BaseVolume,
                                RandomVolume = RandomVolume,
                                Pan = Pan,
                                RandomPan = RandomPan
                            };
                            fileData.Samples.Add(sampleToAdd);
                        }
                        TextFiles.WriteSfxFile(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", HashCodeToCheck.Key + ".txt"), fileData);
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("File Not Found '{0}'", systemIniPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Default SFX file Not Found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void GetHashCodeNames()
        {
            //Clear listbox and dictionary
            lstSfxNames.Items.Clear();
            HashCodesToAdd.Clear();

            //Add Samples to dictionary
            char[] numbersArray = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < lstSampleFiles.Items.Count; i++)
            {
                //Get file name
                string fileName = Path.GetFileNameWithoutExtension((string)lstSampleFiles.Items[i]);
                if (chkForceUpperCase.Checked)
                {
                    fileName = fileName.ToUpper();
                }

                //Trim digits at the end
                string hashCode = txtHashCode_Prefix.Text + fileName;
                if (chkRandomSeq.Checked)
                {
                    hashCode = hashCode.TrimEnd(numbersArray);
                    string sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", hashCode + ".txt");
                    int iterator = 1;
                    string version = string.Empty;
                    while (File.Exists(sfxFilePath))
                    {
                        version = "_V" + iterator++;
                        sfxFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", hashCode + version + ".txt");
                    }
                    hashCode += version;
                }

                //File HashCode
                if (HashCodesToAdd.ContainsKey(hashCode))
                {
                    HashCodesToAdd[hashCode].Add((string)lstSampleFiles.Items[i]);
                }
                else
                {
                    HashCodesToAdd.Add(hashCode, new List<string> { (string)lstSampleFiles.Items[i] });
                    lstSfxNames.Items.Add(hashCode);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
