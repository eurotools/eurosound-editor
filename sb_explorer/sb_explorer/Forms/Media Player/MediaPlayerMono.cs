using Microsoft.Win32;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace sb_explorer.MediaPlayer
{
    public partial class MediaPlayerMono : Form
    {
        //*===============================================================================================
        //* GLOBAL VARS
        //*===============================================================================================
        private readonly WaveOut AudioPlayer = new WaveOut();
        private readonly byte[] pcmDataToPlay;
        private readonly int Frequency;

        public MediaPlayerMono(byte[] vPcmDataToPlay, int vFrequency)
        {
            InitializeComponent();
            pcmDataToPlay = vPcmDataToPlay;
            Frequency = vFrequency;
        }

        //*===============================================================================================
        //* Form Events
        //*===============================================================================================
        private void MediaPlayerMono_Shown(object sender, System.EventArgs e)
        {
            WavesViewer.WaveStream = new RawSourceWaveStream(new MemoryStream(pcmDataToPlay), new WaveFormat(Frequency, 16, 1));
            WavesViewer.InitControl();
        }

        private void MediaPlayerMono_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AudioPlayer.PlaybackState == PlaybackState.Playing)
            {
                AudioPlayer.Stop();
                AudioPlayer.Dispose();
            }
        }

        //*===============================================================================================
        //* Button Events
        //*===============================================================================================
        private void Button_Play_Click(object sender, System.EventArgs e)
        {
            //Check if we have an output device
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            if (enumerator.HasDefaultAudioEndpoint(DataFlow.Render, Role.Console))
            {
                //Play audio
                if (Frequency != 0)
                {
                    if (AudioPlayer.PlaybackState == PlaybackState.Stopped)
                    {
                        IWaveProvider provider = new RawSourceWaveStream(new MemoryStream(pcmDataToPlay), new WaveFormat(Frequency, 16, 1));
                        AudioPlayer.Init(provider);
                        AudioPlayer.Play();
                    }
                }
            }
            else
            {
                MessageBox.Show("The selected audio could not been played because it has not been possible to find an output device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Stop_Click(object sender, System.EventArgs e)
        {
            if (AudioPlayer.PlaybackState == PlaybackState.Playing)
            {
                AudioPlayer.Stop();
            }
        }

        private void Button_SaveWav_Click(object sender, System.EventArgs e)
        {
            //Restore the last selected path
            string filesExplorerLastPath = WinRegFunctions.GetSubkeyStringValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "SaveWaveDiag");
            if (Directory.Exists(filesExplorerLastPath))
            {
                SaveFileDlg_SaveFile.FileName = string.Empty;
                SaveFileDlg_SaveFile.InitialDirectory = filesExplorerLastPath;
            }

            //Set file name and extension
            SaveFileDlg_SaveFile.Filter = "Wave Audio File (*.wav)|*.wav";
            SaveFileDlg_SaveFile.FileName = "output.wav";

            //Show dialog
            DialogResult saveFileDialog = SaveFileDlg_SaveFile.ShowDialog();
            if (saveFileDialog == DialogResult.OK)
            {
                string filePath = SaveFileDlg_SaveFile.FileName;
                try
                {
                    //Save file
                    IWaveProvider provider = new RawSourceWaveStream(new MemoryStream(pcmDataToPlay), new WaveFormat(Frequency, 16, 1));
                    WaveFileWriter.CreateWaveFile(filePath, provider);

                    //Inform user
                    MessageBox.Show("File saved successfully!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Save the selected path
                WinRegFunctions.CreateSubKeyValue("Eurocomm\\EuroSound Explorer\\DialogBrowsers", "SaveWaveDiag", Path.GetDirectoryName(SaveFileDlg_SaveFile.FileName), RegistryValueKind.String);
            }
        }

        private void Button_Close_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
