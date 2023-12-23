﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Music Tester APP
//-------------------------------------------------------------------------------------------------------------------------------
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PCAudioDLL.Audio_Player;
using sb_editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class MusicApp : Form
    {
        private WaveOut musicPlayer = new WaveOut();
        private MarkerTextFile[] markerData;
        private WaveStream wReader;

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnRunTarget_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (musicPlayer.PlaybackState == PlaybackState.Paused)
            {
                musicPlayer.Play();
            }
            else if (musicPlayer.PlaybackState == PlaybackState.Stopped)
            {
                string waveFile = Path.Combine(GlobalPrefs.ProjectFolder, "Music", lvwMusicFiles.SelectedItems[0].Text + ".wav");

                if (File.Exists(waveFile))
                {
                    int startPos = GetStartPosition(markerData) * 4;
                    int loopStart = GetStartLoopPos(markerData) * 4;
                    int loopEnd = GetEndLoopPos(markerData) * 4;

                    //Read data
                    musicPlayer = new WaveOut();
                    wReader = new WaveFileReader(waveFile);

                    //Cut audio to loop end
                    byte[] pcmData = new byte[loopEnd];
                    wReader.Read(pcmData, 0, pcmData.Length);
                    wReader = new RawSourceWaveStream(new MemoryStream(pcmData), wReader.WaveFormat);

                    //build object
                    AudioLoop musicLooped = new AudioLoop(wReader, loopStart) { Position = startPos, EnableLooping = true };
                    VolumeSampleProvider musicProvider = new VolumeSampleProvider(musicLooped.ToSampleProvider()) { Volume = trackBar1.Value / 100.0f };
                    musicPlayer.Init(musicProvider);
                    musicPlayer.Play();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (musicPlayer != null)
            {
                musicPlayer.Stop();
                musicPlayer.Dispose();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (musicPlayer != null)
            {
                musicPlayer.Pause();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BtnJump_Click(object sender, EventArgs e)
        {
            if (musicPlayer != null && musicPlayer.PlaybackState == PlaybackState.Playing)
            {
                int samples = markerData[lstbx_JumpMakers.SelectedIndex].Position;
                TimeSpan streamPos = TimeSpan.FromMilliseconds((double)decimal.Divide(samples, (wReader.WaveFormat.SampleRate / 1000)));
                wReader.CurrentTime = streamPos;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int GetStartLoopPos(MarkerTextFile[] startMarkers)
        {
            int startPosition = 0;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 6)
                {
                    startPosition = startMarkers[i].Position;
                    break;
                }
                if (startMarkers[i].Type == 7)
                {
                    string nameMarker = startMarkers[i].Name.Replace("GOTO_", "");
                    for (int j = 0; j < startMarkers.Length; j++)
                    {
                        if (startMarkers[j].Name.Equals(nameMarker))
                        {
                            startPosition = startMarkers[j].Position;
                            break;
                        }
                    }
                }
            }

            return startPosition;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int GetEndLoopPos(MarkerTextFile[] startMarkers)
        {
            int startPosition = 0;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 7 || startMarkers[i].Type == 6)
                {
                    startPosition = startMarkers[i].Position;
                    break;
                }
            }

            return startPosition;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int GetStartPosition(MarkerTextFile[] startMarkers)
        {
            int startPosition = 0;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 10)
                {
                    startPosition = startMarkers[i].Position;
                    break;
                }
            }

            return startPosition;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
