using Microsoft.Win32;
using sb_explorer.AudioDecoders;
using sb_explorer.Classes;
using sb_explorer.EXObjects;
using sb_explorer.MediaPlayer;
using sb_explorer.ReadSFXFiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sb_explorer
{
    public partial class Frm_MainFrame : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private readonly Dictionary<uint, EXSound> SoundBanksSFXDictionaryData = new Dictionary<uint, EXSound>();
        private readonly SortedDictionary<short, EXAudio> SoundBanksMediaDictionaryData = new SortedDictionary<short, EXAudio>();
        private readonly ArrayList StreamFileDictionaryData = new ArrayList();
        internal MostRecentFilesMenu RecentFilesMenu;
        private readonly string RecentFilesMenuRegKey = "SOFTWARE\\Eurocomm\\EuroSound Explorer\\RecentFiles";
        private IEnumerator SearchEnumerator;
        private Regex SearchRegEx;
        private readonly string[] arguments;

        public Frm_MainFrame(string[] args)
        {
            InitializeComponent();

            //Check arguments
            arguments = args;
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            //Load user prefs
            GlobalVariables.ListViewWavDataFlags = (short)WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\Settings", "WavHeaderData", 0);
            GlobalVariables.ListViewStreamDataFlags = (short)WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\Settings", "StreamData", 0);

            //Load Recent Files
            RecentFilesMenu = new MruStripMenuInline(MenuItem_File, MenuItemFile_RecentFiles, new MostRecentFilesMenu.ClickedHandler(MenuItemFile_RecentFiles_Click), RecentFilesMenuRegKey, 8);
            RecentFilesMenu.LoadFromRegistry();

            //Load Last State
            if (Convert.ToBoolean(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_IsIconic", 0)))
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (Convert.ToBoolean(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_IsMaximized", 0)))
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                Location = new Point(Convert.ToInt32(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_PositionX", 0)), Convert.ToInt32(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_PositionY", 0)));
            }
            Width = Convert.ToInt32(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_Width", 1208));
            Height = Convert.ToInt32(WinRegFunctions.GetSubkeyIntValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_Height", 949));

            //Get Sonix dir
            GlobalVariables.SoundhDir = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\SoundhDir", "SoundhFolder");

            //Load HashCodes
            if (!string.IsNullOrEmpty(GlobalVariables.SoundhDir))
            {
                Hashcodes.Read_Sound_h();

                //Enable search menu items
                MenuItemView_FindHashCode.Enabled = true;
                MenuItemView_FindNextHashCode.Enabled = true;
            }

            //Load file
            if (arguments != null && arguments.Length > 0)
            {
                LoadSfxFile(arguments[0]);
            }
        }

        private void Frm_MainFrame_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                //Read only the first file
                if (File.Exists(files[0]))
                {
                    LoadSfxFile(files[0]);
                }
            }
        }

        private void Frm_MainFrame_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Frm_MainFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Save Window position
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_PositionX", Location.X, RegistryValueKind.DWord);
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_PositionY", Location.Y, RegistryValueKind.DWord);
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_Width", Width, RegistryValueKind.DWord);
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_Height", Height, RegistryValueKind.DWord);
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_IsMaximized", WindowState == FormWindowState.Maximized, RegistryValueKind.DWord);
            WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\WindowState", "MainFrame_IsIconic", WindowState == FormWindowState.Minimized, RegistryValueKind.DWord);

            //Save recent files list
            RecentFilesMenu.SaveToRegistry();
        }

        //*===============================================================================================
        //* MENU ITEM FILE
        //*===============================================================================================
        private void MenuItemFile_OpenSoundbank_Click(object sender, EventArgs e)
        {
            //Restore the last selected path
            string filesExplorerLastPath = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "OpenSoundbanksDiag");
            if (Directory.Exists(filesExplorerLastPath))
            {
                openFileDialog_Soundbanks.FileName = string.Empty;
                openFileDialog_Soundbanks.InitialDirectory = filesExplorerLastPath;
            }

            //Show files explorer
            if (openFileDialog_Soundbanks.ShowDialog() == DialogResult.OK)
            {
                //Load file and show hashcodes list
                LoadSoundBank(openFileDialog_Soundbanks.FileName);
                ShowSoundBankList();

                //Save the selected path
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "OpenSoundbanksDiag", Path.GetDirectoryName(openFileDialog_Soundbanks.FileName), RegistryValueKind.String);
            }
        }

        private void MenuItemFile_ViewMusic_Click(object sender, EventArgs e)
        {
            //Restore the last selected path
            string filesExplorerLastPath = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "OpenMusicFileDiag");
            if (Directory.Exists(filesExplorerLastPath))
            {
                openFileDialog_MusicFile.FileName = string.Empty;
                openFileDialog_MusicFile.InitialDirectory = filesExplorerLastPath;
            }

            //Show files explorer
            if (openFileDialog_MusicFile.ShowDialog() == DialogResult.OK)
            {
                ViewMusic musicExplorer = new ViewMusic(openFileDialog_MusicFile.FileName);
                musicExplorer.ShowDialog();

                //Save the selected path
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "OpenMusicFileDiag", Path.GetDirectoryName(openFileDialog_MusicFile.FileName), RegistryValueKind.String);
            }
        }

        private void MenuItemFile_SoundhDir_Click(object sender, EventArgs e)
        {
            //Restore the last selected path
            string folderBrowserLastPath = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "FolderBrowserSoundh");
            if (Directory.Exists(folderBrowserLastPath))
            {
                FolderBrowserDialog.SelectedPath = folderBrowserLastPath;
            }

            //Open Folders Explorer
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                GlobalVariables.SoundhDir = FolderBrowserDialog.SelectedPath;
                Hashcodes.Read_Sound_h();
                ShowSoundBankList();

                //Enable search menu items
                MenuItemView_FindHashCode.Enabled = true;
                MenuItemView_FindNextHashCode.Enabled = true;

                //Save Sonix directory
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\SoundhDir", "SoundhFolder", GlobalVariables.SoundhDir, RegistryValueKind.String);

                //Save the selected path
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "FolderBrowserSoundh", FolderBrowserDialog.SelectedPath, RegistryValueKind.String);
            }
        }

        private void MenuItemFile_RecentFiles_Click(int number, string filename)
        {
            //Load file
            if (File.Exists(filename))
            {
                LoadSfxFile(filename);
            }
            else
            {
                MessageBox.Show(string.Join(" ", new string[] { "Loading File:", filename, "\n", "\n", "Error:", filename, "was not found" }), "EuroSound", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RecentFilesMenu.RemoveFile(number);
            }
        }

        private void MenuItemFile_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //*===============================================================================================
        //* MENU ITEM VIEW
        //*===============================================================================================
        private void MenuItemView_FindHashCode_Click(object sender, EventArgs e)
        {
            //Show form
            using (FindHashCode findHashcode = new FindHashCode())
            {
                if (findHashcode.ShowDialog() == DialogResult.OK)
                {
                    ListView_HashCodes.Focus();
                    SearchHashcodes(findHashcode.Textbox_TextSearch.Text);
                }
            }
        }

        public void SearchHashcodes(string str)
        {
            //Start iterating
            SearchEnumerator = ListView_HashCodes.Items.GetEnumerator();
            SearchRegEx = new Regex(str, RegexOptions.IgnoreCase);
            if (!SearchEnumerator.MoveNext())
            {
                return;
            }

            //Search items
            ListViewItem listViewItem;
            while (true)
            {
                listViewItem = SearchEnumerator.Current as ListViewItem;
                if (SearchRegEx.Match(listViewItem.SubItems[2].Text).Success)
                {
                    break;
                }
                if (!SearchEnumerator.MoveNext())
                {
                    return;
                }
            }

            //Select item
            ListView_HashCodes.EnsureVisible(listViewItem.Index);
            if (ListView_HashCodes.SelectedItems.Count != 0)
            {
                ListView_HashCodes.SelectedItems[0].Selected = false;
            }
            listViewItem.Selected = true;
            listViewItem.Focused = true;
        }

        private void MenuItemView_FindNextHashCode_Click(object sender, EventArgs e)
        {
            //Check that the search is not null
            if (SearchEnumerator == null || SearchRegEx == null)
            {
                return;
            }

            //Select item
            if (SearchEnumerator.MoveNext())
            {
                do
                {
                    ListViewItem listViewItem = SearchEnumerator.Current as ListViewItem;
                    if (SearchRegEx.Match(listViewItem.SubItems[2].Text).Success)
                    {
                        ListView_HashCodes.EnsureVisible(listViewItem.Index);
                        if (ListView_HashCodes.SelectedItems.Count != 0)
                        {
                            ListView_HashCodes.SelectedItems[0].Selected = false;
                        }
                        listViewItem.Selected = true;
                        listViewItem.Focused = true;
                        return;
                    }
                }
                while (SearchEnumerator.MoveNext());
            }

            //Inform user
            MessageBox.Show("No more occurrences Found!", "Find Hashcodes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SearchEnumerator = ListView_HashCodes.Items.GetEnumerator();
        }

        //*===============================================================================================
        //* MENU ITEM HELP
        //*===============================================================================================
        private void MenuItemHelp_About_Click(object sender, EventArgs e)
        {
            About FrmAbout = new About();
            FrmAbout.ShowDialog();
        }

        //*===============================================================================================
        //* CHANGE FONT FROM HEX VIEWER
        //*===============================================================================================
        private void MenuItem_HexContextMenu_ChangeFont_Click(object sender, EventArgs e)
        {
            if (HexViewfontDialog.ShowDialog() == DialogResult.OK)
            {
                ListView_HexEditor.Font = HexViewfontDialog.Font;
            }
        }

        //*===============================================================================================
        //* MAIN FORM BUTTONS
        //*===============================================================================================
        private void Button_ReloadFile_Click(object sender, EventArgs e)
        {
            LoadSoundBank(SoundbankFileName.Text);
            ShowSoundBankList();
        }

        private void Button_LoadStreamData_Click(object sender, EventArgs e)
        {
            //Restore the last selected path
            string filesExplorerLastPath = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "OpenStreamFileDiag");
            if (Directory.Exists(filesExplorerLastPath))
            {
                openFileDialog_StreamFile.FileName = string.Empty;
                openFileDialog_StreamFile.InitialDirectory = filesExplorerLastPath;
            }

            //Open Files Explorer
            if (openFileDialog_StreamFile.ShowDialog() == DialogResult.OK)
            {
                LoadStreamData(openFileDialog_StreamFile.FileName);
                UpdateStreamDataView();
                UpdateStreamMarkerView(0);

                //Save the selected path
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "OpenStreamFileDiag", Path.GetDirectoryName(openFileDialog_StreamFile.FileName), RegistryValueKind.String);
            }
        }

        private void Button_ValidateADPCM_Click(object sender, EventArgs e)
        {
            ADPCMValidator adpcmchk2 = new ADPCMValidator(StreamFileDictionaryData);
            adpcmchk2.ShowDialog();
        }

        //*===============================================================================================
        //* LISTVIEW EVENTS
        //*===============================================================================================
        private void ListView_HashCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView_HashCodes.SelectedItems.Count != 0)
            {
                ListViewItem.ListViewSubItemCollection subItems = ListView_HashCodes.SelectedItems[0].SubItems;
                UpdateSampleDetails(Convert.ToUInt32(subItems[0].Text, 16));
            }
        }

        private void ContextMenuItem_CopyName_Click(object sender, EventArgs e)
        {
            if (ListView_HashCodes.SelectedItems.Count != 0)
            {
                Clipboard.SetText(ListView_HashCodes.SelectedItems[0].SubItems[2].Text);
            }
        }

        private void ContextMenuItem_CopyHashCode_Click(object sender, EventArgs e)
        {
            if (ListView_HashCodes.SelectedItems.Count != 0)
            {
                Clipboard.SetText(ListView_HashCodes.SelectedItems[0].SubItems[0].Text);
            }
        }

        private void ListView_SamplePool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView_SamplePool.SelectedIndices.Count != 0)
            {
                //Get index
                short Index = (short)ListView_SamplePool.SelectedItems[0].Tag;
                if (Index >= 0 && CheckedListBox_SampleFlags.GetItemCheckState(10) == CheckState.Unchecked)
                {
                    //Select tab
                    if (Tab_Options.SelectedTab != Tab_Wav_Head_Data)
                    {
                        Tab_Options.SelectedTab = Tab_Wav_Head_Data;
                    }

                    //Unselect all items
                    ListView_WavData.SelectedItems.Clear();

                    //Select item
                    foreach (ListViewItem itemToCheck in ListView_WavData.Items)
                    {
                        if (itemToCheck.SubItems[0].Text.Equals(Index.ToString()))
                        {
                            itemToCheck.Selected = true;
                            itemToCheck.Focused = true;
                            itemToCheck.EnsureVisible();
                            break;
                        }
                    }
                }
            }
        }

        private void ListView_WavData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_WavData.SelectedIndices.Count != 0)
            {
                //Get item
                short Index = short.Parse(ListView_WavData.SelectedItems[0].SubItems[0].Text);
                if (SoundBanksMediaDictionaryData.ContainsKey(Index))
                {
                    EXAudio SelectedAudio = SoundBanksMediaDictionaryData[Index] as EXAudio;

                    //Decode audio
                    byte[] parsedData = null;
                    try
                    {
                        if (GlobalVariables.SoundbankPlatform == (byte)GenericFunctions.CurrentPlatform.PC)
                        {
                            parsedData = SelectedAudio.SampleByteData;
                        }
                        else if (GlobalVariables.SoundbankPlatform == (byte)GenericFunctions.CurrentPlatform.PS2)
                        {
                            SonyAdpcm vagDecoder = new SonyAdpcm();
                            parsedData = vagDecoder.Decode(SelectedAudio.SampleByteData);
                        }
                        else if (GlobalVariables.SoundbankPlatform == (byte)GenericFunctions.CurrentPlatform.GC)
                        {
                            DspAdpcm gameCubeDecoder = new DspAdpcm();
                            parsedData = AudioFunctions.ShortArrayToByteArray(gameCubeDecoder.Decode(SelectedAudio.SampleByteData, SelectedAudio.DspCoefs));
                        }
                        else if (GlobalVariables.SoundbankPlatform == (byte)GenericFunctions.CurrentPlatform.XBX)
                        {
                            XboxAdpcm xboxDecoder = new XboxAdpcm();
                            parsedData = xboxDecoder.Decode(SelectedAudio.SampleByteData);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //Show and play item
                    if (parsedData != null)
                    {
                        MediaPlayerMono soundsMediaPlayer = new MediaPlayerMono(parsedData, (int)SelectedAudio.Frequency);
                        soundsMediaPlayer.ShowDialog();
                    }
                }
            }
        }

        private void ListView_StreamData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListView_StreamData.SelectedIndices.Count != 0)
            {
                UpdateStreamMarkerView(ListView_StreamData.SelectedIndices[0]);
            }
        }

        private void MenuItem_ExportStreamMarkers_Click(object sender, EventArgs e)
        {
            if (ListView_StreamData.SelectedIndices.Count != 0)
            {
                //Get stream sound
                int selectedStream = ListView_StreamData.SelectedIndices[0];
                EXSoundStream streamSound = StreamFileDictionaryData[selectedStream] as EXSoundStream;

                //Get markers Array
                EXStreamMarker[] markersArray = (EXStreamMarker[])streamSound.Markers.ToArray(typeof(EXStreamMarker));

                //Export file
                MarkerFiles_Exporter mrkExporter = new MarkerFiles_Exporter();

                //Restore the last selected path
                string filesExplorerLastPath = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "SaveMarkersDiag");
                if (Directory.Exists(filesExplorerLastPath))
                {
                    SaveFileDlg_SaveFile.FileName = string.Empty;
                    SaveFileDlg_SaveFile.InitialDirectory = filesExplorerLastPath;
                }

                //Set file name and extension
                SaveFileDlg_SaveFile.Filter = "Markers File (*.MRK)|*.mrk";
                SaveFileDlg_SaveFile.FileName = "StreamMarkers" + selectedStream + ".mrk";

                //Show dialog
                DialogResult saveFileDialog = SaveFileDlg_SaveFile.ShowDialog();
                if (saveFileDialog == DialogResult.OK)
                {
                    string filePath = SaveFileDlg_SaveFile.FileName;
                    try
                    {
                        mrkExporter.ExportMarkers(filePath, markersArray);

                        //Inform user
                        MessageBox.Show("File saved successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //Save the selected path
                    WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "SaveMarkersDiag", Path.GetDirectoryName(SaveFileDlg_SaveFile.FileName), RegistryValueKind.String);
                }
            }
        }

        private void ListView_StreamData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListView_StreamData.SelectedIndices.Count != 0)
            {
                string adpcmStatus = ListView_StreamData.SelectedItems[0].SubItems[1].Text;
                if (adpcmStatus.Equals("OK"))
                {
                    int soundIndex = int.Parse(ListView_StreamData.SelectedItems[0].SubItems[0].Text);
                    EXSoundStream soundToPlay = StreamFileDictionaryData[soundIndex] as EXSoundStream;
                    MediaPlayerMono soundsMediaPlayer = new MediaPlayerMono(soundToPlay.SampleParsedData, (int)soundToPlay.Frequency);
                    soundsMediaPlayer.ShowDialog();
                }
                else if (adpcmStatus.Equals("??"))
                {
                    MessageBox.Show("The selected stream has not been validated, please validate the streams before playing them.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("The selected stream could not be played because data is corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListViewWavData_Options_Click(object sender, EventArgs e)
        {
            SoundbanksList_Options userOptions = new SoundbanksList_Options();
            userOptions.ShowDialog();
        }

        private void ListViewStreamData_Options_Click(object sender, EventArgs e)
        {
            StreambanksList_Options userOptions = new StreambanksList_Options();
            userOptions.ShowDialog();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadSfxFile(string FilePath)
        {
            //Check Magic
            try
            {
                using (BinaryReader BReader = new BinaryReader(File.Open(FilePath, FileMode.Open, FileAccess.Read)))
                {
                    string Magic = Encoding.ASCII.GetString(BReader.ReadBytes(4));
                    if (Magic.Equals("MUSX"))
                    {
                        uint hashCode = BReader.ReadUInt32();
                        if ((hashCode & 0xf00000) == 0xE00000)
                        {
                            BReader.Close();
                            ViewMusic musicExplorer = new ViewMusic(FilePath);
                            musicExplorer.ShowDialog();
                        }
                        else if (hashCode == 0xFFFF)
                        {
                            BReader.Close();
                            LoadStreamData(FilePath);
                            UpdateStreamDataView();
                            UpdateStreamMarkerView(0);
                        }
                        else if ((hashCode & 0xfff00000) == 0)
                        {
                            BReader.Close();
                            LoadSoundBank(FilePath);
                            ShowSoundBankList();
                        }
                    }
                    else
                    {
                        StringBuilder errorFileNotFound = new StringBuilder();
                        errorFileNotFound.Append("Loading File: ");
                        errorFileNotFound.Append(FilePath);
                        errorFileNotFound.Append("\n\n");
                        errorFileNotFound.Append("Error: ");
                        errorFileNotFound.Append(FilePath);
                        errorFileNotFound.Append("does not have a valid format.");
                        MessageBox.Show(errorFileNotFound.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSoundBank(string FileName)
        {
            //Try to discover the platform
            bool LoadFile = false;
            byte filePlatform = GenericFunctions.FindPlatform(FileName);
            if (filePlatform == byte.MaxValue)
            {
                using (SetPlatform specPlatform = new SetPlatform(FileName))
                {
                    specPlatform.Owner = this;
                    if (specPlatform.ShowDialog() == DialogResult.OK)
                    {
                        filePlatform = (byte)specPlatform.Combobox_Platform.SelectedIndex;
                    }
                }
            }

            //Set Platform
            switch (filePlatform)
            {
                case 0:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: PC";
                    GlobalVariables.SoundbankPlatform = (byte)GenericFunctions.CurrentPlatform.PC;
                    break;
                case 1:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: PS2";
                    GlobalVariables.SoundbankPlatform = (byte)GenericFunctions.CurrentPlatform.PS2;
                    break;
                case 2:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: GC";
                    GlobalVariables.SoundbankPlatform = (byte)GenericFunctions.CurrentPlatform.GC;
                    break;
                case 3:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: XB";
                    GlobalVariables.SoundbankPlatform = (byte)GenericFunctions.CurrentPlatform.XBX;
                    break;
            }

            //Load file
            if (LoadFile)
            {
                FileStream fileStream;
                try
                {
                    fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BinaryReader binaryReader = new BinaryReader(fileStream);
                try
                {
                    SFX_ReadSoundBank sfxReader = new SFX_ReadSoundBank();
                    uint soundbankHashCode = sfxReader.LoadSoundBankFile(binaryReader, SoundBanksSFXDictionaryData, SoundBanksMediaDictionaryData);

                    //Update Statusbar
                    StatusLabel_HashCode.Text = "HashCode : 0x" + soundbankHashCode.ToString("X8");

                    //Enable reload button
                    Button_ReloadFile.Enabled = true;

                    //Add file to recent list
                    RecentFilesMenu.AddFile(FileName);
                }
                catch (Exception ex)
                {
                    binaryReader.Close();
                    fileStream.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Show file path in the textbox
            SoundbankFileName.Text = FileName;
        }

        private void LoadStreamData(string FileName)
        {
            //Try to discover the platform
            bool LoadFile = false;
            byte filePlatform = GenericFunctions.FindPlatform(FileName);
            if (filePlatform == byte.MaxValue)
            {
                using (SetPlatform specPlatform = new SetPlatform(FileName))
                {
                    specPlatform.Owner = this;
                    if (specPlatform.ShowDialog() == DialogResult.OK)
                    {
                        filePlatform = (byte)specPlatform.Combobox_Platform.SelectedIndex;
                    }
                }
            }

            //Set Platform
            switch (filePlatform)
            {
                case 0:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: PC";
                    GlobalVariables.StreamFilePlatform = (byte)GenericFunctions.CurrentPlatform.PC;
                    break;
                case 1:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: PS2";
                    GlobalVariables.StreamFilePlatform = (byte)GenericFunctions.CurrentPlatform.PS2;
                    break;
                case 2:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: GC";
                    GlobalVariables.StreamFilePlatform = (byte)GenericFunctions.CurrentPlatform.GC;
                    break;
                case 3:
                    LoadFile = true;
                    StatusLabel_Platform.Text = "Platform: XB";
                    GlobalVariables.StreamFilePlatform = (byte)GenericFunctions.CurrentPlatform.XBX;
                    break;
            }

            //Load file
            if (LoadFile)
            {
                FileStream fileStream;
                try
                {
                    fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BinaryReader binaryReader = new BinaryReader(fileStream);
                try
                {
                    SFX_ReadStreamBank sfxReader = new SFX_ReadStreamBank();
                    uint streamFileHashCode = sfxReader.LoadStreamFile(binaryReader, StreamFileDictionaryData);

                    //Update Statusbar
                    StatusLabel_HashCode.Text = "HashCode : 0x" + streamFileHashCode.ToString("X8");

                    //Add file to recent list
                    RecentFilesMenu.AddFile(FileName);
                }
                catch (Exception ex)
                {
                    binaryReader.Close();
                    fileStream.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Textbox_StreamFilePath.Text = FileName;
                Button_ValidateADPCM.Enabled = true;
            }
        }

        private void ShowSoundBankList()
        {
            //Print hashcodes
            if (SoundBanksSFXDictionaryData != null)
            {
                IDictionaryEnumerator enumerator = SoundBanksSFXDictionaryData.GetEnumerator();
                enumerator.Reset();
                ListView_HashCodes.Items.Clear();
                ListView_HashCodes.BeginUpdate();
                if (enumerator.MoveNext())
                {
                    do
                    {
                        bool hashCodeFound = false;

                        //Check if hashcode exists
                        string[] array = new string[3] { string.Format("0x{0:X8}", enumerator.Key), "*", null };
                        if (Hashcodes.sound_HashCodes == null)
                        {
                            array[2] = "N/A";
                        }
                        else if (Hashcodes.sound_HashCodes.ContainsKey(enumerator.Key))
                        {
                            array[2] = Hashcodes.sound_HashCodes[enumerator.Key].ToString();
                            hashCodeFound = true;
                        }
                        else
                        {
                            array[2] = "Hashcode not found";
                        }

                        //Create item and add it to list
                        ListViewItem listViewItem = new ListViewItem(array);
                        if (!hashCodeFound)
                        {
                            listViewItem.ForeColor = Color.Red;
                        }
                        ListView_HashCodes.Items.Add(listViewItem);
                    }
                    while (enumerator.MoveNext());
                    ListView_HashCodes.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                ListView_HashCodes.EndUpdate();
            }
            if (SoundBanksMediaDictionaryData == null)
            {
                return;
            }

            PrintWavData();

        }

        internal void PrintWavData()
        {
            //Print Wav Data
            IDictionaryEnumerator enumerator2 = SoundBanksMediaDictionaryData.GetEnumerator();
            enumerator2.Reset();
            ListView_WavData.Items.Clear();
            ListView_WavData.BeginUpdate();
            if (enumerator2.MoveNext())
            {
                do
                {
                    string[] array2 = new string[8];
                    EXAudio wavHeaderData = enumerator2.Value as EXAudio;
                    array2[0] = enumerator2.Key.ToString();
                    if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 0) & 1))
                    {
                        array2[1] = wavHeaderData.Flags.ToString("X");
                    }
                    else
                    {
                        array2[1] = wavHeaderData.Flags.ToString();
                    }
                    if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 1) & 1))
                    {
                        array2[2] = wavHeaderData.Address.ToString("X");
                    }
                    else
                    {
                        array2[2] = wavHeaderData.Address.ToString();
                    }
                    if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 2) & 1))
                    {
                        array2[3] = wavHeaderData.MemorySize.ToString("X");
                    }
                    else
                    {
                        array2[3] = wavHeaderData.MemorySize.ToString();
                    }
                    if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 3) & 1))
                    {
                        array2[4] = wavHeaderData.SampleSize.ToString("X");
                    }
                    else
                    {
                        array2[4] = wavHeaderData.SampleSize.ToString();
                    }
                    array2[5] = wavHeaderData.Frequency.ToString();
                    if (Convert.ToBoolean((GlobalVariables.ListViewWavDataFlags >> 4) & 1))
                    {
                        array2[6] = wavHeaderData.LoopStartOffset.ToString("X");
                    }
                    else
                    {
                        array2[6] = wavHeaderData.LoopStartOffset.ToString();
                    }
                    array2[7] = wavHeaderData.DurationInMilliseconds.ToString();

                    //Create item and add it to list
                    ListViewItem listViewItem2 = new ListViewItem(array2);
                    if (wavHeaderData.LoopStartOffset > wavHeaderData.MemorySize)
                    {
                        listViewItem2.UseItemStyleForSubItems = false;
                        listViewItem2.SubItems[6].ForeColor = Color.Red;
                    }
                    ListView_WavData.Items.Add(listViewItem2);
                }
                while (enumerator2.MoveNext());
            }
            ListView_WavData.EndUpdate();
        }

        private void UpdateSampleDetails(uint hashcode)
        {
            string text = ((Hashcodes.sound_HashCodes == null) ? "N/A" : ((!Hashcodes.sound_HashCodes.ContainsKey(hashcode)) ? "Hashcode not found" : Hashcodes.sound_HashCodes[hashcode].ToString()));
            HashcodeName.Text = text;
            EXSound sample = SoundBanksSFXDictionaryData[hashcode];

            //Show SFX Sample Properties
            switch (sample.TrackingType)
            {
                case 0:
                    Textbox_TrackingType.Text = "2D";
                    break;
                case 1:
                    Textbox_TrackingType.Text = "2D AMB ";
                    break;
                case 4:
                    Textbox_TrackingType.Text = "2D_PL2";
                    break;
                case 2:
                    Textbox_TrackingType.Text = "3D";
                    break;
                case 3:
                    Textbox_TrackingType.Text = "3D_Rnd_Pos";
                    break;
            }
            vMasterVolume.Text = sample.MasterVolume.ToString();
            vMinDelay.Text = sample.MinDelay.ToString();
            vMaxDelay.Text = sample.MaxDelay.ToString();
            vPriority.Text = sample.Priority.ToString();
            vDucker.Text = sample.Ducker.ToString();
            vDuckerLength.Text = sample.DuckerLenght.ToString();
            vMaxVoices.Text = sample.MaxVoices.ToString();
            vReverbSend.Text = sample.ReverbSend.ToString();
            vInnerRadiusReal.Text = sample.InnerRadiusReal.ToString();
            vOuterRadiusReal.Text = sample.OuterRadiusReal.ToString();

            if ((sample.ReverbSend > 100) | (sample.ReverbSend < 0))
            {
                vReverbSend.ForeColor = Color.Red;
            }

            //Flags
            for (int i = 0; i < CheckedListBox_SampleFlags.Items.Count; i++)
            {
                bool fChecked = Convert.ToBoolean((sample.Flags >> i) & 1);
                CheckedListBox_SampleFlags.SetItemChecked(i, fChecked);
            }

            //Print SFX associated Samples
            vSampleCount.Text = sample.Samples.Count.ToString();
            IEnumerator enumerator = sample.Samples.GetEnumerator();
            enumerator.Reset();
            ListView_SamplePool.Items.Clear();
            ListView_SamplePool.BeginUpdate();
            if (enumerator.MoveNext())
            {
                do
                {
                    EXSample samplePoolItem = enumerator.Current as EXSample;
                    string[] array = new string[7];
                    bool error = false;

                    //Check for SubSFX
                    if (samplePoolItem.HasSubSfx)
                    {
                        if (samplePoolItem.FileRef < 0)
                        {
                            array[0] = "Stream: " + samplePoolItem.FileRef;
                        }
                        else if (Hashcodes.sound_HashCodes == null)
                        {
                            array[0] = samplePoolItem.FileRef.ToString();
                        }
                        else
                        {
                            uint hashcodeToCheck = (uint)(0x1A000000 | (ushort)samplePoolItem.FileRef);
                            if (Hashcodes.sound_HashCodes.ContainsKey(hashcodeToCheck))
                            {
                                array[0] = Hashcodes.sound_HashCodes[hashcodeToCheck].ToString();
                            }
                            else
                            {
                                array[0] = hashcodeToCheck.ToString("X8");
                                error = true;
                            }
                        }
                    }
                    else if (samplePoolItem.FileRef < 0)
                    {
                        array[0] = "Stream: " + samplePoolItem.FileRef;
                    }
                    else
                    {
                        if ((uint)samplePoolItem.FileRef > SoundBanksMediaDictionaryData.Count)
                        {
                            error = true;
                        }
                        array[0] = "Wav: " + samplePoolItem.FileRef;
                    }
                    array[1] = samplePoolItem.Volume.ToString();
                    array[2] = samplePoolItem.VolumeOffset.ToString();
                    array[3] = decimal.Divide(samplePoolItem.Pitch, 1000).ToString();
                    array[4] = decimal.Divide(samplePoolItem.PitchOffset, 1000).ToString();
                    array[5] = samplePoolItem.Pan.ToString();
                    array[6] = samplePoolItem.PanOffset.ToString();

                    //Add item to list
                    ListViewItem listViewItem = new ListViewItem(array)
                    {
                        Tag = samplePoolItem.FileRef
                    };

                    //Highlight item if there is an error
                    if (error)
                    {
                        listViewItem.UseItemStyleForSubItems = false;
                        listViewItem.SubItems[0].ForeColor = Color.Red;
                    }

                    //Add item to listview
                    ListView_SamplePool.Items.Add(listViewItem);
                }
                while (enumerator.MoveNext());
            }
            ListView_SamplePool.EndUpdate();

            //Add hexadecimal data
            ListView_HexEditor.Items.Clear();
            ListView_HexEditor.BeginUpdate();
            int index = 0;
            bool flag = false;
            do
            {
                string[] fullData = new string[10];
                fullData[0] = index.ToString("X4");
                StringBuilder hexString = new StringBuilder();
                int flagsOffset = -1, samplePoolOffset = -1;

                for (int j = 0; j < 8; j++)
                {
                    if (index < sample.BinaryLength)
                    {
                        //Get hex string and store data in hex format
                        if (sample.BinaryData[index] >= 32 && sample.BinaryData[index] < 127)
                        {
                            hexString.Append(Convert.ToChar(sample.BinaryData[index]));
                        }
                        else
                        {
                            hexString.Append(".");
                        }

                        //Check for sample pool or flags offset
                        if (index == sample.FlagsOffset)
                        {
                            flagsOffset = j;
                            flag = true;
                        }
                        if (index == sample.SamplePoolOffset)
                        {
                            samplePoolOffset = j;
                            flag = true;
                        }

                        //Add data
                        fullData[j + 1] = sample.BinaryData[index++].ToString("X2");
                    }
                    else
                    {
                        break;
                    }
                }

                fullData[9] = hexString.ToString();
                ListViewItem listViewItem2 = new ListViewItem(fullData)
                {
                    UseItemStyleForSubItems = false
                };

                for (int i = 0; i < 8; i++)
                {
                    Color itemCol = SystemColors.ControlText;
                    if (i >= flagsOffset && flag == true)
                    {
                        itemCol = Color.Blue;
                    }
                    if (i >= samplePoolOffset && flag == true)
                    {
                        itemCol = Color.Green;
                    }
                    listViewItem2.SubItems[i + 1].ForeColor = itemCol;
                }
                ListView_HexEditor.Items.Add(listViewItem2);
            }
            while (index < sample.BinaryLength);
            ListView_HexEditor.EndUpdate();
        }

        internal void UpdateStreamDataView()
        {
            //Print Wav Data
            IEnumerator enumerator2 = StreamFileDictionaryData.GetEnumerator();
            enumerator2.Reset();
            ListView_StreamData.Items.Clear();
            ListView_StreamData.BeginUpdate();
            if (enumerator2.MoveNext())
            {
                int num = 0;
                ushort errors = 0;
                do
                {
                    EXSoundStream wavHeaderData = enumerator2.Current as EXSoundStream;
                    string[] array2 = new string[8];
                    array2[0] = num.ToString();
                    num++;
                    array2[1] = "??";
                    if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 0) & 1))
                    {
                        array2[2] = wavHeaderData.MarkerOffset.ToString("X");
                    }
                    else
                    {
                        array2[2] = wavHeaderData.MarkerOffset.ToString();
                    }
                    if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 1) & 1))
                    {
                        array2[3] = wavHeaderData.MarkerSize.ToString("X");
                    }
                    else
                    {
                        array2[3] = wavHeaderData.MarkerSize.ToString();
                    }
                    if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 2) & 1))
                    {
                        array2[4] = wavHeaderData.AudioOffset.ToString("X");
                    }
                    else
                    {
                        array2[4] = wavHeaderData.AudioOffset.ToString();
                    }
                    if (Convert.ToBoolean((GlobalVariables.ListViewStreamDataFlags >> 3) & 1))
                    {
                        array2[5] = wavHeaderData.AudioSize.ToString("X");
                    }
                    else
                    {
                        array2[5] = wavHeaderData.AudioSize.ToString();
                    }
                    array2[6] = wavHeaderData.BaseVolume.ToString();

                    if (wavHeaderData.MarkerSize < 0)
                    {
                        errors |= (1 << 3);
                    }
                    if (wavHeaderData.AudioOffset < 0)
                    {
                        errors |= (1 << 4);
                    }
                    if (wavHeaderData.AudioSize < 0)
                    {
                        errors |= (1 << 5);
                    }
                    if (wavHeaderData.BaseVolume < 0 || wavHeaderData.BaseVolume > 100)
                    {
                        errors |= (1 << 6);
                    }

                    //Create item and add it to list
                    ListViewItem listViewItem2 = new ListViewItem(array2)
                    {
                        UseItemStyleForSubItems = false
                    };
                    for (int i = 0; i < 7; i++)
                    {
                        if (Convert.ToBoolean((errors >> i) & 1))
                        {
                            listViewItem2.SubItems[i].ForeColor = Color.Red;
                        }
                    }
                    ListView_StreamData.Items.Add(listViewItem2);
                }
                while (enumerator2.MoveNext());
            }
            ListView_StreamData.EndUpdate();
        }

        private void UpdateStreamMarkerView(int SelectedStream)
        {
            EXSoundStream markerHeader = StreamFileDictionaryData[SelectedStream] as EXSoundStream;
            textStartMarkerCount.Text = markerHeader.m_MusicMarkerStartData.Count.ToString();
            ListView_StreamData_StartMarkers.Items.Clear();
            if (markerHeader.m_MusicMarkerStartData.Count >= 0 && markerHeader.m_MusicMarkerStartData.Count <= 20)
            {
                textStartMarkerCount.ForeColor = SystemColors.ControlText;
                if (markerHeader.m_MusicMarkerStartData != null)
                {
                    int num = 0;
                    if (0 < markerHeader.m_MusicMarkerStartData.Count)
                    {
                        do
                        {
                            ushort errors = 0;
                            string[] array = new string[7];
                            int streamLenght = markerHeader.SampleByteData.Length * 4;
                            if (num < markerHeader.m_MusicMarkerStartData.Count)
                            {
                                EXStreamStartMarker musicMarkerStartData = markerHeader.m_MusicMarkerStartData[num] as EXStreamStartMarker;
                                array[0] = num.ToString();
                                array[1] = musicMarkerStartData.Index.ToString();
                                array[2] = musicMarkerStartData.Position.ToString();
                                if (musicMarkerStartData.Position > streamLenght)
                                {
                                    errors |= (1 << 2);
                                }
                                switch (musicMarkerStartData.Type)
                                {
                                    case 10:
                                        array[3] = "Start";
                                        break;
                                    case 9:
                                        array[3] = "End";
                                        break;
                                    case 7:
                                        array[3] = "Goto";
                                        break;
                                    case 6:
                                        array[3] = "Loop";
                                        break;
                                    case 5:
                                        array[3] = "Pause";
                                        break;
                                    case 0:
                                        array[3] = "Jump";
                                        break;
                                    default:
                                        array[3] = "Error";
                                        break;
                                }
                                array[4] = musicMarkerStartData.LoopStart.ToString();
                                if (musicMarkerStartData.LoopStart > streamLenght)
                                {
                                    errors |= (1 << 4);
                                }
                                array[5] = musicMarkerStartData.LoopMarkerCount.ToString();
                                array[6] = musicMarkerStartData.MarkerPos.ToString();
                            }
                            else
                            {
                                int num2 = 0;
                                do
                                {
                                    array[num2++] = "N/A";
                                }
                                while (num2 < 7);
                            }
                            //Add item to list
                            ListViewItem listViewItem = new ListViewItem(array)
                            {
                                UseItemStyleForSubItems = false
                            };
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (Convert.ToBoolean((errors >> i) & 1))
                                {
                                    listViewItem.SubItems[i].ForeColor = Color.Red;
                                }
                            }
                            ListView_StreamData_StartMarkers.Items.Add(listViewItem);
                            num++;
                        }
                        while (num < markerHeader.m_MusicMarkerStartData.Count);
                    }
                }
            }
            else
            {
                textStartMarkerCount.ForeColor = Color.Red;
            }
            ListView_StreamData_StartMarkers.EndUpdate();
            textMarkerCount.Text = markerHeader.Markers.Count.ToString();
            ListView_StreamData_Markers.Items.Clear();
            if (markerHeader.Markers.Count >= 0 && markerHeader.Markers.Count <= 20)
            {
                textMarkerCount.ForeColor = Color.Black;
                if (markerHeader.Markers != null)
                {
                    int num3 = 0;
                    if (0 < markerHeader.Markers.Count)
                    {
                        do
                        {
                            ushort errors = 0;
                            string[] array2 = new string[6];
                            int musicLength = markerHeader.SampleByteData.Length * 4;
                            if (num3 < markerHeader.Markers.Count)
                            {
                                EXStreamMarker musicMarkerData = markerHeader.Markers[num3] as EXStreamMarker;
                                array2[0] = num3.ToString();
                                array2[1] = musicMarkerData.Index.ToString();
                                array2[2] = musicMarkerData.Position.ToString();
                                if (musicMarkerData.Position > musicLength)
                                {
                                    errors |= (1 << 2);
                                }
                                switch (musicMarkerData.Type)
                                {
                                    case 10:
                                        array2[3] = "Start";
                                        break;
                                    case 9:
                                        array2[3] = "End";
                                        break;
                                    case 7:
                                        array2[3] = "Goto";
                                        break;
                                    case 6:
                                        array2[3] = "Loop";
                                        break;
                                    case 5:
                                        array2[3] = "Pause";
                                        break;
                                    case 0:
                                        array2[3] = "Jump";
                                        break;
                                    default:
                                        array2[3] = "Error";
                                        break;
                                }
                                array2[4] = musicMarkerData.LoopStart.ToString();
                                if (musicMarkerData.LoopStart > musicLength)
                                {
                                    errors |= (1 << 4);
                                }
                                array2[5] = musicMarkerData.LoopMarkerCount.ToString();
                            }
                            else
                            {
                                int num4 = 0;
                                do
                                {
                                    array2[num4++] = "N/A";
                                }
                                while (num4 < 6);
                            }
                            //Add item to list
                            ListViewItem listViewItem = new ListViewItem(array2)
                            {
                                UseItemStyleForSubItems = false
                            };
                            for (int i = 0; i < array2.Length; i++)
                            {
                                if (Convert.ToBoolean((errors >> i) & 1))
                                {
                                    listViewItem.SubItems[i].ForeColor = Color.Red;
                                }
                            }
                            ListView_StreamData_Markers.Items.Add(listViewItem);
                            num3++;
                        }
                        while (num3 < markerHeader.Markers.Count);
                    }
                }
            }
            else
            {
                textMarkerCount.ForeColor = Color.Red;
            }
            ListView_StreamData_Markers.EndUpdate();
        }


    }
}
