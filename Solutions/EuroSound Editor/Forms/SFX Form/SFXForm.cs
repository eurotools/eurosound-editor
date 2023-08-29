//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFX Settings Form
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using PCAudioDLL;
using sb_editor.Objects;
using sb_editor.Panels;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SFXForm : Form
    {
        private readonly bool sfxDefaults;
        private readonly string sfxFileName;
        private Color currentColor = SystemColors.Control;
        private Brush colorText = SystemBrushes.ControlText;
        internal readonly ProjProperties projectSettings;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SFXForm(string fileName, bool setDefaultSettings = false)
        {
            InitializeComponent();
            sfxFileName = fileName;
            sfxDefaults = setDefaultSettings;
            if (setDefaultSettings)
            {
                ShowInTaskbar = false;
                tabCtrl.Visible = false;
                UserControl_SamplePool.Visible = false;
                pnlOptions.Visible = false;
                Height = 406;
                StartPosition = FormStartPosition.CenterParent;
                btnDefSettings_Accept.Visible = true;
                btnDefSettings_Cancel.Visible = true;
                UserControl_SamplePoolControl.grbSampleProperties.Visible = true;
            }

            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);
            }
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void Frm_SFX_Form_Load(object sender, System.EventArgs e)
        {
            //Get SFX File full path
            string filepath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFileName + ".txt");
            if (sfxDefaults)
            {
                filepath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "SFX Defaults.txt");
                if (!File.Exists(filepath))
                {
                    File.WriteAllText(filepath, string.Empty);
                }

                //Read Data
                string systemIniPath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "EuroSound.ini");
                if (File.Exists(systemIniPath))
                {
                    //Get default Samples
                    IniFile systemIni = new IniFile(systemIniPath);
                    UserControl_SamplePoolControl smplPoolCtrl = UserControl_SamplePoolControl;
                    if (decimal.TryParse(systemIni.Read("DTextNIndex_0", "SFXForm"), NumberStyles.Any, GlobalPrefs.NumericProvider, out decimal PitchOffset))
                    {
                        smplPoolCtrl.nudPitchOffset.Value = Math.Min(Math.Max(smplPoolCtrl.nudPitchOffset.Minimum, PitchOffset), smplPoolCtrl.nudPitchOffset.Maximum);
                    }
                    if (decimal.TryParse(systemIni.Read("DTextNIndex_1", "SFXForm"), NumberStyles.Any, GlobalPrefs.NumericProvider, out decimal RandomPitch))
                    {
                        smplPoolCtrl.nudRandomPitchOffset.Value = Math.Min(Math.Max(smplPoolCtrl.nudRandomPitchOffset.Minimum, RandomPitch), smplPoolCtrl.nudRandomPitchOffset.Maximum);
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_2", "SFXForm"), NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte BaseVolume))
                    {
                        smplPoolCtrl.nudBaseVolume.Value = Math.Min(Math.Max(smplPoolCtrl.nudBaseVolume.Minimum, BaseVolume), smplPoolCtrl.nudBaseVolume.Maximum);
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_3", "SFXForm"), NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte RandomVolume))
                    {
                        smplPoolCtrl.nudRandomVolume.Value = Math.Min(Math.Max(smplPoolCtrl.nudRandomVolume.Minimum, RandomVolume), smplPoolCtrl.nudRandomVolume.Maximum);
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_4", "SFXForm"), NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte Pan))
                    {
                        smplPoolCtrl.nudPan.Value = Math.Min(Math.Max(smplPoolCtrl.nudPan.Minimum, Pan), smplPoolCtrl.nudPan.Maximum);
                    }
                    if (sbyte.TryParse(systemIni.Read("DTextNIndex_5", "SFXForm"), NumberStyles.Any, GlobalPrefs.NumericProvider, out sbyte RandomPan))
                    {
                        smplPoolCtrl.nudRandomPan.Value = Math.Min(Math.Max(smplPoolCtrl.nudRandomPan.Minimum, RandomPan), smplPoolCtrl.nudRandomPan.Maximum);
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("File Not Found '{0}'", systemIniPath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Enable ONLY available formats buttons
            foreach (string formatName in projectSettings.platformData.Keys)
            {
                ((Button)grbCreateFormat.Controls[string.Join(string.Empty, "btn", formatName.ToUpper().Replace(" ", string.Empty))]).Enabled = true;
            }

            //Common Tab 
            if (File.Exists(filepath))
            {
                CopyFile(filepath, Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common.txt"));
                //Add data to dictionary
                tabCtrl.TabPages.Add("tabCommon", "Common");
                SFX sfxFileData = TextFiles.ReadSfxFile(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common.txt"));
                //Print Hashcode
                txtHashCode.Text = sfxFileData.HashCode.ToString();
                //Show parameters
                UserControl_SFX_Parameters.LoadData(sfxFileData);
            }
            else
            {
                MessageBox.Show(string.Format("File Not Found: {0}", filepath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.OK;
                Close();
            }

            //Add a tab for each specific format and disable button
            foreach (string formatName in projectSettings.platformData.Keys)
            {
                filepath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", formatName, sfxFileName + ".txt");
                if (File.Exists(filepath))
                {
                    CopyFile(filepath, Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", formatName + ".txt"));
                    //Create tab and add data to dictionary
                    tabCtrl.TabPages.Add("tab" + formatName, formatName);
                    //Disable buttons
                    ((Button)grbCreateFormat.Controls[string.Join(string.Empty, "btn", formatName.ToUpper().Replace(" ", string.Empty))]).Enabled = false;
                }
            }

            //Update tab control
            if (tabCtrl.TabPages.Count > 0)
            {
                tabCtrl.SelectedIndex = 0;
                TabCtrl_SelectedIndexChanged(null, EventArgs.Empty);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_SFX_Form_Shown(object sender, System.EventArgs e)
        {
            //Hide main form & show selector
            if (!sfxDefaults)
            {
                ((MainForm)Application.OpenForms[nameof(MainForm)]).Hide();
                OpenHashCodesSelector(DesktopLocation);
            }

            //Update form controls
            Text = sfxFileName;
            lblCurrentSFX.Text = string.Format(">Name: {0}", sfxFileName);
            UserControl_SFX_Parameters.txtSFX_Name.Text = sfxFileName;
            btnClipboardPaste.Enabled = File.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Clipboard.txt"));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_SFX_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            PCAudioDll.UnloadSoundbank();

            //Stop any audio that could be playing
            if (UserControl_SamplePool.audioPlayer != null)
            {
                UserControl_SamplePool.audioPlayer.Stop();
            }

            //Check the way the form is closing
            if (DialogResult == DialogResult.OK)
            {
                CloseSelectorAndShowMainForm();
            }
            else
            {
                //Start blinking
                if (tabCtrl.SelectedIndex == 0)
                {
                    pnlAlert.Visible = true;
                }
                tmrTabPageBlink.Start();

                //Ask user what wants to do
                if (MessageBox.Show("Do you want to exit the SFX editor without saving first?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CloseSelectorAndShowMainForm();
                }
                else
                {
                    e.Cancel = true;
                    //Stop Blinking
                    if (tabCtrl.SelectedIndex == 0)
                    {
                        pnlAlert.Visible = false;
                    }
                    tmrTabPageBlink.Stop();
                    tabCtrl.Refresh();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CloseSelectorAndShowMainForm()
        {
            if (Application.OpenForms.OfType<SFXForm>().Count() == 1)
            {
                //Close Selector
                CloseHashCodesSelector();

                //Show Main Form again
                if (!((MainForm)Application.OpenForms[nameof(MainForm)]).Visible)
                {
                    ((MainForm)Application.OpenForms[nameof(MainForm)]).Show();
                }
            }
        }

        //*===============================================================================================
        //* CREATE SPECIFIC VERSION BUTTONS
        //*===============================================================================================
        private void BtnPLAYSTATION2_Click(object sender, EventArgs e)
        {
            string Common = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common.txt");
            string PlayStation2 = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "PlayStation2.txt");
            CopyFile(Common, PlayStation2);
            tabCtrl.TabPages.Add("tabPlayStation2", "PlayStation2");
            btnPLAYSTATION2.Enabled = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnGAMECUBE_Click(object sender, EventArgs e)
        {
            string Common = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common.txt");
            string GameCube = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "GameCube.txt");
            CopyFile(Common, GameCube);
            tabCtrl.TabPages.Add("tabGameCube", "GameCube");
            btnGAMECUBE.Enabled = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnXBOX_Click(object sender, EventArgs e)
        {
            string Common = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common.txt");
            string Xbox = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "X Box.txt");
            CopyFile(Common, Xbox);
            tabCtrl.TabPages.Add("tabXbox", "X Box");
            btnXBOX.Enabled = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPC_Click(object sender, EventArgs e)
        {
            string Common = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "Common.txt");
            string PC = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "PC.txt");
            CopyFile(Common, PC);
            tabCtrl.TabPages.Add("tabPC", "PC");
            btnPC.Enabled = false;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnClipboardCopy_Click(object sender, EventArgs e)
        {
            //Remove the current file if exists
            string clipboardFile = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "ClipBoard.txt");
            File.Delete(clipboardFile);

            //Create clipboard file
            TabCtrl_Deselecting(null, null);
            string filepath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", tabCtrl.SelectedTab.Text + ".txt");
            File.Copy(filepath, clipboardFile);

            //Enable button
            btnClipboardPaste.Enabled = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnClipboardPaste_Click(object sender, System.EventArgs e)
        {
            string clipboardFile = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", "ClipBoard.txt");
            if (File.Exists(clipboardFile))
            {
                string filepath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", tabCtrl.SelectedTab.Text + ".txt");
                File.Delete(filepath);
                File.Copy(clipboardFile, filepath);
                TabCtrl_SelectedIndexChanged(null, System.EventArgs.Empty);
            }
            else
            {
                btnClipboardPaste.Enabled = false;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRemoveFormat_Click(object sender, System.EventArgs e)
        {
            //Disable buttons
            ((Button)grbCreateFormat.Controls[string.Join(string.Empty, "btn", tabCtrl.SelectedTab.Name.Substring(3).ToUpper().Replace(" ", string.Empty))]).Enabled = true;
            tabCtrl.TabPages.RemoveAt(tabCtrl.SelectedIndex);
        }

        //*===============================================================================================
        //* FORM BUTTONS
        //*===============================================================================================
        private void BtnTestSFX_Click(object sender, System.EventArgs e)
        {
            //Start Timer
            Stopwatch watch = Stopwatch.StartNew();

            //Get output folder & name
            string outputFilePath = CommonFunctions.GetSoundbankOutPath(projectSettings, "PC", "English");
            string fileName = string.Format("HC{0:X6}.SFX", CommonFunctions.GetSfxName((int)Enumerations.Language.English, 0xFFFE));

            //Create file
            CreateTestSfx(outputFilePath, fileName);

            //Show time
            txtEsTime.Text = string.Format("ES Time {0:0.###}", watch.Elapsed.TotalSeconds);
            watch.Stop();

            //Call DLL
            string filePath = Path.Combine(outputFilePath, fileName);
            if (File.Exists(filePath))
            {
                if (PCAudioDll.IsSoundBankLoaded(0xFFFE))
                {
                    PCAudioDll.UnloadSoundbank();
                }
                txtDllTime.Text = string.Format("DLL Time {0:0.###}", PCAudioDll.LoadSoundBank(filePath));
                PCAudioDll.PlaySfx(0);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStopSFX_Click(object sender, System.EventArgs e)
        {
            if (PCAudioDll.IsSoundBankLoaded(0xFFFE))
            {
                PCAudioDll.UnloadSoundbank();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnSfxTestDebug_Click(object sender, System.EventArgs e)
        {
            //Show Debugger
            OpenDebuggerForm(DesktopLocation);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ButtonDllVoices_Click(object sender, EventArgs e)
        {
            OpenVoicesDebuggerForm(DesktopLocation);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnReverbTester_Click(object sender, EventArgs e)
        {
            OpenReverbTesterForm(DesktopLocation);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnAccept_Click(object sender, EventArgs e)
        {
            BtnOK_Click(null, EventArgs.Empty);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "System")) && Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
            {
                //Check if is the default file or a common one
                if (sfxDefaults)
                {
                    TabCtrl_Deselecting(null, null);
                    string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "SFX Defaults.txt");
                    File.Delete(filePath);
                    File.Copy(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", tabCtrl.TabPages[0].Text + ".txt"), filePath);
                }
                else
                {
                    //Remove previous SFXs
                    string commonFile = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxFileName + ".txt");
                    File.Delete(commonFile);
                    foreach (string formatName in projectSettings.platformData.Keys)
                    {
                        commonFile = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", formatName, sfxFileName + ".txt");
                        File.Delete(commonFile);
                    }

                    //Save all Sfx File data
                    TabCtrl_Deselecting(null, null);
                    foreach (TabPage currentTab in tabCtrl.TabPages)
                    {
                        string formatName = currentTab.Text;
                        string filePath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", formatName + ".txt");
                        if (formatName.Equals("Common", System.StringComparison.OrdinalIgnoreCase))
                        {
                            formatName = string.Empty;
                        }
                        File.Copy(filePath, Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", formatName, sfxFileName + ".txt"));
                    }
                }
            }

            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnDefSettings_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //*===============================================================================================
        //* TAB CONTROL BUTTONS
        //*===============================================================================================
        private void TabCtrl_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            //Save common for all formats
            foreach (TabPage currentTab in tabCtrl.TabPages)
            {
                string filepath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", currentTab.Text + ".txt");
                SaveSfxCommon(filepath);
            }

            //Save current format
            if (tabCtrl.TabPages.Count > 0 && tabCtrl.SelectedTab != null)
            {
                SaveSfxSamplePool(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", tabCtrl.SelectedTab.Text + ".txt"));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Load and show new format
            string formatName = tabCtrl.TabPages[tabCtrl.SelectedIndex].Text;
            string filepath = Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", "Misc", formatName + ".txt");
            if (File.Exists(filepath))
            {
                SFX sfxFileData = TextFiles.ReadSfxFile(filepath);
                UserControl_SamplePoolControl.LoadData(sfxFileData);
                if (!sfxDefaults)
                {
                    UserControl_SamplePool.LoadData(sfxFileData);
                }
            }

            //Enable or disable delete format button
            btnRemoveFormat.Enabled = !tabCtrl.SelectedTab.Name.Equals("tabCommon");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void TabCtrl_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (tmrTabPageBlink.Enabled && e.Index == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(currentColor), e.Bounds);
                e.Graphics.DrawString(tabCtrl.TabPages[0].Text, tabCtrl.Font, colorText, tabCtrl.GetTabRect(0));
            }
            else
            {
                e.Graphics.DrawString(tabCtrl.TabPages[e.Index].Text, tabCtrl.Font, SystemBrushes.ControlText, tabCtrl.GetTabRect(e.Index));
            }
        }

        //*===============================================================================================
        //* TIMER
        //*===============================================================================================
        private void TmrTabPageBlink_Tick(object sender, EventArgs e)
        {
            if (currentColor == SystemColors.Control)
            {
                currentColor = SystemColors.Highlight;
                colorText = SystemBrushes.HighlightText;
            }
            else
            {
                currentColor = SystemColors.Control;
                colorText = SystemBrushes.ControlText;
            }
            tabCtrl.Refresh();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void CopyFile(string sourceFilePath, string destPath)
        {
            File.Delete(destPath);
            File.Copy(sourceFilePath, destPath);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SaveSfxCommon(string filePath)
        {
            //Read and update file
            SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
            ParseSfxCommonData(sfxFileData);

            //Update textfile
            TextFiles.WriteSfxFile(filePath, sfxFileData, sfxDefaults);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void SaveSfxSamplePool(string filePath)
        {
            //Read and update file
            SFX sfxFileData = TextFiles.ReadSfxFile(filePath);
            ParseSfxSamplePool(sfxFileData);

            //Update textfile
            TextFiles.WriteSfxFile(filePath, sfxFileData, sfxDefaults);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private SFX ParseSfxCommonData(SFX sfxFileData)
        {
            sfxFileData.Parameters.ReverbSend = UserControl_SFX_Parameters.TrackBar_ReverbSend.Value;
            sfxFileData.Parameters.MasterVolume = (int)UserControl_SFX_Parameters.nudMasterVolume.Value;
            if (UserControl_SFX_Parameters.RadiobtnTrackingType_2D.Checked)
            {
                sfxFileData.Parameters.TrackingType = 0;
            }
            else if (UserControl_SFX_Parameters.RadiobtnTrackingType_Amb.Checked)
            {
                sfxFileData.Parameters.TrackingType = 1;
            }
            else if (UserControl_SFX_Parameters.RadiobtnTrackingType_3D.Checked)
            {
                sfxFileData.Parameters.TrackingType = 2;
            }
            else if (UserControl_SFX_Parameters.RadiobtnTrackingType_3DRndPos.Checked)
            {
                sfxFileData.Parameters.TrackingType = 3;
            }
            else if (UserControl_SFX_Parameters.RadiobtnTrackingType_2DPL2.Checked)
            {
                sfxFileData.Parameters.TrackingType = 4;
            }
            sfxFileData.Parameters.InnerRadius = UserControl_SFX_Parameters.Trackbar_InnerRadius.Value;
            sfxFileData.Parameters.OuterRadius = UserControl_SFX_Parameters.Trackbar_OuterRadius.Value;
            sfxFileData.Parameters.MaxVoices = (int)UserControl_SFX_Parameters.nudMaxVoice.Value;
            if (UserControl_SFX_Parameters.RadiobtnSteal.Checked)
            {
                sfxFileData.Parameters.Action1 = 0;
            }
            else if (UserControl_SFX_Parameters.RadiobtnReject.Checked)
            {
                sfxFileData.Parameters.Action1 = 1;
            }
            sfxFileData.Parameters.Priority = (int)UserControl_SFX_Parameters.nudPriority.Value;
            sfxFileData.Parameters.Alertness = (int)UserControl_SFX_Parameters.nudAlertness.Value;
            sfxFileData.Parameters.StealOnAge = UserControl_SFX_Parameters.chkStealOnLouder.Checked;
            sfxFileData.Parameters.Ducker = (int)UserControl_SFX_Parameters.nudDucker.Value;
            sfxFileData.Parameters.DuckerLength = (int)UserControl_SFX_Parameters.nudDuckerLength.Value;
            sfxFileData.Parameters.Outdoors = UserControl_SFX_Parameters.chkUnderWater.Checked;
            sfxFileData.Parameters.PauseInNis = UserControl_SFX_Parameters.chkPauseInNis.Checked;
            sfxFileData.Parameters.IgnoreAge = UserControl_SFX_Parameters.chkIgnoreAge.Checked;
            sfxFileData.Parameters.MusicType = UserControl_SFX_Parameters.chkMusicType.Checked;
            sfxFileData.Parameters.Doppler = UserControl_SFX_Parameters.chkDoppler.Checked;

            return sfxFileData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private SFX ParseSfxSamplePool(SFX sfxFileData)
        {
            sfxFileData.SamplePool.isLooped = UserControl_SamplePoolControl.chkLoop.Checked;
            sfxFileData.SamplePool.MaxDelay = (int)UserControl_SamplePoolControl.nudMaxDelay.Value;
            sfxFileData.SamplePool.MinDelay = (int)UserControl_SamplePoolControl.nudMinDelay.Value;
            if (UserControl_SamplePoolControl.rdoSingle.Checked)
            {
                sfxFileData.SamplePool.Action1 = 0;
            }
            else if (UserControl_SamplePoolControl.rdoMultiSample.Checked)
            {
                sfxFileData.SamplePool.Action1 = 1;
            }
            sfxFileData.SamplePool.RandomPick = UserControl_SamplePoolControl.chkRandomPick.Checked;
            sfxFileData.SamplePool.Shuffled = UserControl_SamplePoolControl.chkShuffled.Checked;
            sfxFileData.SamplePool.Polyphonic = UserControl_SamplePoolControl.chkPolyphonic.Checked;

            sfxFileData.SamplePool.EnableSubSFX = UserControl_SamplePool.chkEnableSubSFX.Checked;
            sfxFileData.SamplePool.EnableStereo = UserControl_SamplePool.chkEnableStereo.Checked;
            sfxFileData.Samples.Clear();
            for (int i = 0; i < UserControl_SamplePool.lstSamples.Items.Count; i++)
            {
                sfxFileData.Samples.Add((SfxSample)UserControl_SamplePool.lstSamples.Items[i]);
            }
            return sfxFileData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void OpenHashCodesSelector(Point desktopLoc)
        {
            Form selectorForm = Application.OpenForms[nameof(Frm_HashCodes)];
            if (selectorForm == null)
            {
                desktopLoc.X += 915;
                desktopLoc.Y += 205;
                Frm_HashCodes hashCodesSelector = new Frm_HashCodes
                {
                    DesktopLocation = desktopLoc
                };
                hashCodesSelector.Show();
            }
            else
            {
                selectorForm.Focus();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CloseHashCodesSelector()
        {
            //Show selector
            Form selectorForm = Application.OpenForms[nameof(Frm_HashCodes)];
            if (selectorForm != null)
            {
                selectorForm.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void OpenDebuggerForm(Point desktopLoc)
        {
            Form debuggerForm = Application.OpenForms[nameof(PCDllDebugForm)];
            if (debuggerForm == null)
            {
                desktopLoc.X += 915;
                PCDllDebugForm hashCodesSelector = new PCDllDebugForm()
                {
                    DesktopLocation = desktopLoc
                };
                hashCodesSelector.Show();
            }
            else
            {
                debuggerForm.Focus();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void OpenVoicesDebuggerForm(Point desktopLoc)
        {
            Form debuggerForm = Application.OpenForms[nameof(PCDllVoicesForm)];
            if (debuggerForm == null)
            {
                desktopLoc.X += 915;
                PCDllVoicesForm hashCodesSelector = new PCDllVoicesForm
                {
                    DesktopLocation = desktopLoc
                };
                hashCodesSelector.Show();
            }
            else
            {
                debuggerForm.Focus();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void OpenReverbTesterForm(Point desktopLoc)
        {
            Form debuggerForm = Application.OpenForms[nameof(ReverbTester)];
            if (debuggerForm == null)
            {
                desktopLoc.X += 415;
                desktopLoc.Y += 125;
                ReverbTester reverbTesterForm = new ReverbTester()
                {
                    DesktopLocation = desktopLoc
                };
                reverbTesterForm.Show();
            }
            else
            {
                debuggerForm.Focus();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
