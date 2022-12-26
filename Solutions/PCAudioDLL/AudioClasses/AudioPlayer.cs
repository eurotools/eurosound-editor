using NAudio.Wave;
using PCAudioDLL.MusXStuff;
using PCAudioDLL.MusXStuff.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PCAudioDLL.AudioClasses
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioPlayer
    {
        private readonly AudioMixer mixer = new AudioMixer();

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlaySingleSfx(DebugConsole outputConsole, WaveOut _waveOut, PCVoices pcVoices, Sample sfxSample, SampleData[] sfxStoredData)
        {
            bool loopFlag = ((sfxSample.Flags >> (int)SoundBanksReader.Flags.Loop) & 1) == 1;

            do
            {
                SampleInfo sampleInfo = sfxSample.samplesList[Utils.random.Next(sfxSample.samplesList.Count)];
                SampleData sampleData = sfxStoredData[sampleInfo.FileRef];

                IWaveProvider waveProv = mixer.GetWaveProviderLoop(sampleData, sampleInfo);

                //Wait time that the user has put
                ApplyPositiveMinAndMaxDelay(sfxSample.MinDelay, sfxSample.MaxDelay);

                //Init new voice
                int vIndex = pcVoices.RequestVoice(sampleData.Flags == 1, outputConsole);

                //Start playing
                _waveOut.Init(waveProv);
                _waveOut.Play();
                while (_waveOut.PlaybackState == PlaybackState.Playing)
                {

                };

                //Stop and remove
                _waveOut.Stop();

                //Close Voice
                pcVoices.PreCloseVoice(vIndex, outputConsole);
                pcVoices.CloseVoice(vIndex);
            }
            while (loopFlag);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayPolyphonic(DebugConsole outputConsole, WaveOut _waveOut, PCVoices pcVoices, Sample sfxSample, SampleData[] sfxStoredData)
        {
            bool loopFlag = ((sfxSample.Flags >> (int)SoundBanksReader.Flags.Loop) & 1) == 1;

            do
            {
                //PreLoad all
                Dictionary<int, BufferedWaveProvider> indexBuff = new Dictionary<int, BufferedWaveProvider>();
                foreach (SampleInfo info in sfxSample.samplesList)
                {
                    SampleData sampleData = sfxStoredData[info.FileRef];

                    //Get Provider and convert it to a stream
                    IWaveProvider waveProv = mixer.GetWaveProviderLoop(sampleData, info);
                    Stream waveStreamData = mixer.ProviderToStream(waveProv);

                    //Start Playing
                    BufferedWaveProvider waveBuffProv = mixer.StreamToWaveBuffer(waveStreamData, waveProv.WaveFormat);
                    indexBuff.Add(pcVoices.RequestVoice(false, outputConsole), waveBuffProv);
                    _waveOut.Init(waveBuffProv);
                    _waveOut.Play();
                };

                //Check if there are playing
                bool voicesArePlaying = true;
                while (voicesArePlaying)
                {
                    foreach (KeyValuePair<int, BufferedWaveProvider> outVoice in indexBuff)
                    {
                        if (outVoice.Value.BufferedBytes == 0)
                        {
                            voicesArePlaying = false;
                            pcVoices.PreCloseVoice(outVoice.Key, outputConsole);
                            pcVoices.CloseVoice(outVoice.Key);
                        }
                        else
                        {
                            voicesArePlaying = true;
                            break;
                        }
                    }
                };

                //Stop and remove
                _waveOut.Stop();
            }
            while (loopFlag);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayList(DebugConsole outputConsole, WaveOut _waveOut, bool shuffled, PCVoices pcVoices, Sample sfxSample, SampleData[] sfxStoredData)
        {
            bool loopFlag = ((sfxSample.Flags >> (int)SoundBanksReader.Flags.Loop) & 1) == 1;
            do
            {
                //Randomize list
                if (shuffled)
                {
                    sfxSample.samplesList.Shuffle();
                }

                foreach (SampleInfo info in sfxSample.samplesList)
                {
                    SampleData sampleData = sfxStoredData[info.FileRef];

                    //Get Provider and convert it to a stream
                    IWaveProvider waveProv = mixer.GetWaveProviderLoop(sampleData, info);
                    Stream waveStreamData = mixer.ProviderToStream(waveProv);

                    BufferedWaveProvider sampleProv = mixer.StreamToWaveBuffer(waveStreamData, waveProv.WaveFormat);
                    _waveOut.Init(sampleProv);

                    //Wait the time that the user has put
                    ApplyPositiveMinAndMaxDelay(sfxSample.MinDelay, sfxSample.MaxDelay);

                    //Add Voices
                    int vIndex = pcVoices.RequestVoice(false, outputConsole);

                    //Start playing
                    _waveOut.Play();
                    int exitAt = ApplyNegativeMinAndMaxDelay(sfxSample.MinDelay, sfxSample.MaxDelay);
                    while (sampleProv.BufferedBytes > exitAt)
                    {

                    }

                    //Preclose Voice
                    pcVoices.PreCloseVoice(vIndex, outputConsole);
                    pcVoices.CloseVoice(vIndex);
                }
            }
            while (loopFlag);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ApplyPositiveMinAndMaxDelay(int minDelay, int maxDelay)
        {
            if (minDelay > 0 && maxDelay > 0)
            {
                Thread.Sleep(Utils.random.Next(minDelay, maxDelay));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private int ApplyNegativeMinAndMaxDelay(int minDelay, int maxDelay)
        {
            int sampleToQuit = 0;

            if (minDelay < 0 && maxDelay < 0)
            {
                sampleToQuit = Utils.random.Next(Math.Abs(minDelay), Math.Abs(maxDelay)) * 16;
            }

            return sampleToQuit;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
