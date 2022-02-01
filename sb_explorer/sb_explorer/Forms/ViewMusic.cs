using Microsoft.Win32;
using sb_explorer.AudioDecoders;
using sb_explorer.Classes;
using sb_explorer.EXObjects.Musicbanks;
using sb_explorer.MediaPlayer;
using sb_explorer.ReadSFXFiles;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace sb_explorer
{
    public partial class ViewMusic : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES
        //*===============================================================================================
        private EXMusic musicObject;
        private readonly string MusicFilePath;

        public ViewMusic(string FilePath)
        {
            InitializeComponent();
            MusicFilePath = FilePath;
        }

        //*===============================================================================================
        //* MAIN FORM EVENTS
        //*===============================================================================================
        private void ViewMusic_Shown(object sender, System.EventArgs e)
        {
            //Read File
            LoadMusicBank(MusicFilePath);

            //Show markers
            UpdateStreamMarkerView();

            //Validate ADPCM
            ValidateMusicAdpcm();
        }

        private void Button_MediaPlayer_Click(object sender, EventArgs e)
        {
            MediaPlayerStereo musicPlayer = new MediaPlayerStereo(musicObject.SampleParsedData_LeftChannel, musicObject.SampleParsedData_RightChannel, (int)musicObject.Frequency);
            musicPlayer.ShowDialog();
        }

        //*===============================================================================================
        //* BUTTONS CONTROLS
        //*===============================================================================================
        private void Button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        //*===============================================================================================
        //* FUNCTIONS
        //*===============================================================================================
        private void LoadMusicBank(string FileName)
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
            int interleave_block_size = 1;
            switch (filePlatform)
            {
                case 0:
                    LoadFile = true;
                    interleave_block_size = 1;
                    GlobalVariables.MusicFilePlatform = (byte)GenericFunctions.CurrentPlatform.PC;
                    break;
                case 1:
                    LoadFile = true;
                    interleave_block_size = 128;
                    GlobalVariables.MusicFilePlatform = (byte)GenericFunctions.CurrentPlatform.PS2;
                    break;
                case 2:
                    LoadFile = true;
                    interleave_block_size = 1;
                    GlobalVariables.MusicFilePlatform = (byte)GenericFunctions.CurrentPlatform.GC;
                    break;
                case 3:
                    LoadFile = true;
                    interleave_block_size = 4;
                    GlobalVariables.MusicFilePlatform = (byte)GenericFunctions.CurrentPlatform.XBX;
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
                    SFX_ReadMusicBank SfxMusicReader = new SFX_ReadMusicBank();
                    musicObject = SfxMusicReader.LoadMusicFile(binaryReader, interleave_block_size);

                    //Add file to recent list
                    ((Frm_MainFrame)Application.OpenForms["Frm_MainFrame"]).RecentFilesMenu.AddFile(FileName);
                }
                catch (Exception ex)
                {
                    binaryReader.Close();
                    fileStream.Close();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Show object info
                Textbox_BaseVolume.Text = musicObject.BaseVolume.ToString();
                Textbox_MusicLength.Text = ((musicObject.SampleByteData_LeftChannel.Length + musicObject.SampleByteData_RightChannel.Length) * 4).ToString();
                Textbox_FilePath.Text = MusicFilePath;
                Textbox_AdpcmStatus.Text = "No validated";
            }
        }

        private void UpdateStreamMarkerView()
        {
            textStartMarkerCount.Text = musicObject.m_MusicMarkerStartData.Count.ToString();
            ListView_StreamData_StartMarkers.Items.Clear();
            if (musicObject.m_MusicMarkerStartData.Count >= 0 && musicObject.m_MusicMarkerStartData.Count <= 20)
            {
                textStartMarkerCount.ForeColor = SystemColors.ControlText;
                if (musicObject.m_MusicMarkerStartData != null)
                {
                    int num = 0;
                    if (0 < musicObject.m_MusicMarkerStartData.Count)
                    {
                        do
                        {
                            ushort errors = 0;
                            string[] array = new string[7];
                            int musicLength = (musicObject.SampleByteData_LeftChannel.Length + musicObject.SampleByteData_RightChannel.Length) * 4;
                            if (num < musicObject.m_MusicMarkerStartData.Count)
                            {
                                EXStreamStartMarker musicMarkerStartData = musicObject.m_MusicMarkerStartData[num] as EXStreamStartMarker;
                                array[0] = num.ToString();
                                array[1] = musicMarkerStartData.Index.ToString();
                                array[2] = musicMarkerStartData.Position.ToString();
                                if (musicMarkerStartData.Position > musicLength)
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
                                if (musicMarkerStartData.LoopStart > musicLength)
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
                                while (num2 < array.Length);
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
                        while (num < musicObject.m_MusicMarkerStartData.Count);
                    }
                }
            }
            else
            {
                textStartMarkerCount.ForeColor = Color.Red;
            }
            ListView_StreamData_StartMarkers.EndUpdate();
            textMarkerCount.Text = musicObject.Markers.Count.ToString();
            ListView_StreamData_Markers.Items.Clear();
            if (musicObject.Markers.Count >= 0 && musicObject.Markers.Count <= 20)
            {
                textMarkerCount.ForeColor = Color.Black;
                if (musicObject.Markers != null)
                {
                    int num3 = 0;
                    if (0 < musicObject.Markers.Count)
                    {
                        do
                        {
                            ushort errors = 0;
                            string[] array2 = new string[6];
                            int musicLength = (musicObject.SampleByteData_LeftChannel.Length + musicObject.SampleByteData_RightChannel.Length) * 4; ;
                            if (num3 < musicObject.Markers.Count)
                            {
                                EXStreamMarker musicMarkerData = musicObject.Markers[num3] as EXStreamMarker;
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
                        while (num3 < musicObject.Markers.Count);
                    }
                }
            }
            else
            {
                textMarkerCount.ForeColor = Color.Red;
            }
            ListView_StreamData_Markers.EndUpdate();
        }

        private void ValidateMusicAdpcm()
        {
            //Decoders
            ImaAdpcm imaFunctions = new ImaAdpcm();
            SonyAdpcm sonyAdpcmFunctions = new SonyAdpcm();
            XboxAdpcm xboxAdpcmFunctions = new XboxAdpcm();

            try
            {
                //Decode audio
                if (GlobalVariables.MusicFilePlatform == (byte)GenericFunctions.CurrentPlatform.PC || GlobalVariables.MusicFilePlatform == (byte)GenericFunctions.CurrentPlatform.GC)
                {
                    musicObject.SampleParsedData_LeftChannel = AudioFunctions.ShortArrayToByteArray(imaFunctions.Decode(musicObject.SampleByteData_LeftChannel, musicObject.SampleByteData_LeftChannel.Length * 2));
                    musicObject.SampleParsedData_RightChannel = AudioFunctions.ShortArrayToByteArray(imaFunctions.Decode(musicObject.SampleByteData_RightChannel, musicObject.SampleByteData_RightChannel.Length * 2));
                    Textbox_AdpcmStatus.Text = "ADPCM data is Valid";
                    Textbox_AdpcmStatus.ForeColor = SystemColors.ControlText;
                    Button_MediaPlayer.Enabled = true;
                }
                else if (GlobalVariables.MusicFilePlatform == (byte)GenericFunctions.CurrentPlatform.PS2)
                {
                    musicObject.SampleParsedData_LeftChannel = sonyAdpcmFunctions.Decode(musicObject.SampleByteData_LeftChannel);
                    musicObject.SampleParsedData_RightChannel = sonyAdpcmFunctions.Decode(musicObject.SampleByteData_RightChannel);
                    Textbox_AdpcmStatus.Text = "ADPCM data is Valid";
                    Textbox_AdpcmStatus.ForeColor = SystemColors.ControlText;
                    Button_MediaPlayer.Enabled = true;
                }
                else if (GlobalVariables.MusicFilePlatform == (byte)GenericFunctions.CurrentPlatform.XBX)
                {
                    musicObject.SampleParsedData_LeftChannel = xboxAdpcmFunctions.Decode(musicObject.SampleByteData_LeftChannel);
                    musicObject.SampleParsedData_RightChannel = xboxAdpcmFunctions.Decode(musicObject.SampleByteData_RightChannel);
                    Textbox_AdpcmStatus.Text = "ADPCM data is Valid";
                    Textbox_AdpcmStatus.ForeColor = SystemColors.ControlText;
                    Button_MediaPlayer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                //Update listview item
                Textbox_AdpcmStatus.Text = "ADPCM data is *INVALID*";
                Textbox_AdpcmStatus.ForeColor = Color.Red;

                //Disable button
                Button_MediaPlayer.Enabled = false;

                //Inform user
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_ExportMarkers_Click(object sender, EventArgs e)
        {
            //Get markers Array
            EXStreamMarker[] markersArray = (EXStreamMarker[])musicObject.Markers.ToArray(typeof(EXStreamMarker));

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
            SaveFileDlg_SaveFile.FileName = "MusicMarkers" + Path.GetFileNameWithoutExtension(MusicFilePath) + ".mrk";

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
}
