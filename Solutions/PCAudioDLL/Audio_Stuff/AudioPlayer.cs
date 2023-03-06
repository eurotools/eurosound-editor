using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PCAudioDLL.MusX_Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PCAudioDLL.Audio_Stuff
{
    public class AudioPlayer
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlaySingleSfx(WaveOut _waveOut, Sample sfxSample, List<SampleData> sfxStoredData)
        {
            AudioMaths audioMaths = new AudioMaths();

            //Play sample list that loops
            new Thread(() =>
            {
                bool LoopFlag = ((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Loop) & 1) == 1;
                do
                {
                    Random random = new Random();
                    SampleInfo sampleInfo = sfxSample.samplesList[random.Next(sfxSample.samplesList.Count)];
                    SampleData sampleData = sfxStoredData[sampleInfo.FileRef];
                    WaveFormat waveFormat = new WaveFormat(audioMaths.SemitonesToFreq(sampleData.Frequency, audioMaths.GetPitch(sampleInfo)), 16, 1);

                    if (LoopFlag && sampleData.Flags == 0)
                    {
                        //Set data to the audio buffer
                        BufferedWaveProvider soundToPlay = new BufferedWaveProvider(waveFormat)
                        {
                            BufferLength = sampleData.EncodedData.Length,
                            DiscardOnBufferOverflow = true
                        };

                        //Check Inter-Sample Delay
                        if (sfxSample.MinDelay >= 0 && sfxSample.MaxDelay > 0)
                        {
                            float delay = random.Next(sfxSample.MinDelay, sfxSample.MaxDelay) / 1000.0f;
                            if (delay > 0)
                            {
                                int numSilenceSamples = (int)(waveFormat.SampleRate * delay);
                                byte[] silenceBytes = new byte[numSilenceSamples * 2];

                                //Add silence samples to the buffer
                                soundToPlay.BufferLength = sampleData.EncodedData.Length + silenceBytes.Length;
                                soundToPlay.AddSamples(silenceBytes, 0, silenceBytes.Length);
                            }
                        }

                        //Add SFX samples to the buffer
                        soundToPlay.AddSamples(sampleData.EncodedData, 0, sampleData.EncodedData.Length);
                        VolumeSampleProvider volumeProvider = new VolumeSampleProvider(soundToPlay.ToSampleProvider()) { Volume = audioMaths.GetVolume(sampleInfo) };

                        //Init new voice
                        int vIndex = PCAudioDll.pcOutVoices.RequestVoice(sampleData.Flags == 1, PCAudioDll.outputConsole);

                        //Play Sample
                        _waveOut.Init(volumeProvider);
                        _waveOut.Play();
                        _waveOut.Volume = sfxSample.MasterVolume;
                        while (soundToPlay.BufferedBytes > 0 && !PCAudioDll.StopSfx)
                        {
                            Thread.Sleep(1);
                        }

                        //Stop and remove
                        soundToPlay.ClearBuffer();

                        //Close Voice
                        PCAudioDll.pcOutVoices.PreCloseVoice(vIndex, PCAudioDll.outputConsole);
                        PCAudioDll.pcOutVoices.CloseVoice(vIndex);
                    }
                    else
                    {
                        //Check Inter-Sample Delay
                        int numSilenceSamples = 0;
                        if (sfxSample.MinDelay >= 0 && sfxSample.MaxDelay > 0)
                        {
                            float delay = random.Next(sfxSample.MinDelay, sfxSample.MaxDelay) / 1000.0f;
                            if (delay > 0)
                            {
                                numSilenceSamples = (int)(waveFormat.SampleRate * delay) * 2;
                                byte[] pcmData = new byte[sampleData.EncodedData.Length + numSilenceSamples];
                                Buffer.BlockCopy(sampleData.EncodedData, 0, pcmData, numSilenceSamples, sampleData.EncodedData.Length);
                                sampleData.EncodedData = pcmData;
                            }
                        }

                        //Create Provider
                        RawSourceWaveStream provider = new RawSourceWaveStream(new MemoryStream(sampleData.EncodedData), waveFormat);
                        LoopStream loop = new LoopStream(provider, sampleData.LoopStartOffset)
                        {
                            EnableLooping = sampleData.Flags == 1,
                            Position = numSilenceSamples
                        };
                        VolumeSampleProvider volumeProvider = new VolumeSampleProvider(loop.ToSampleProvider()) { Volume = audioMaths.GetVolume(sampleInfo) };

                        //Init new voice
                        int vIndex = PCAudioDll.pcOutVoices.RequestVoice(sampleData.Flags == 1, PCAudioDll.outputConsole);

                        //Play Sample
                        _waveOut.Init(volumeProvider);
                        _waveOut.Play();
                        _waveOut.Volume = sfxSample.MasterVolume;
                        while (_waveOut.PlaybackState == PlaybackState.Playing && !PCAudioDll.StopSfx)
                        {
                            Thread.Sleep(1);
                        }

                        //Stop and remove
                        _waveOut.Stop();
                        provider.Close();

                        //Close Voice
                        PCAudioDll.pcOutVoices.PreCloseVoice(vIndex, PCAudioDll.outputConsole);
                        PCAudioDll.pcOutVoices.CloseVoice(vIndex);
                    }
                } while (LoopFlag && !PCAudioDll.StopSfx);
            })
            {
                IsBackground = true
            }.Start();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayMultiSampleSFX(WaveOut _waveOut, Sample sfxSample, List<SampleData> sfxStoredData)
        {
            AudioMaths audioMaths = new AudioMaths();
            new Thread(() =>
            {
                do
                {
                    //Randomize list
                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Shuffled) & 1) == 1)
                    {
                        sfxSample.samplesList.Shuffle();
                    }

                    Dictionary<int, BufferedWaveProvider> indexBuff = new Dictionary<int, BufferedWaveProvider>();
                    foreach (SampleInfo sampleInfo in sfxSample.samplesList)
                    {
                        if (PCAudioDll.StopSfx)
                        {
                            break;
                        }
                        else
                        {
                            Random random = new Random();
                            SampleData sampleData = sfxStoredData[sampleInfo.FileRef];
                            WaveFormat waveFormat = new WaveFormat(audioMaths.SemitonesToFreq(sampleData.Frequency, audioMaths.GetPitch(sampleInfo)), 16, 1);

                            //Set data to the audio buffer
                            BufferedWaveProvider soundToPlay = new BufferedWaveProvider(waveFormat)
                            {
                                BufferLength = sampleData.EncodedData.Length,
                                DiscardOnBufferOverflow = true
                            };

                            //Check Inter-Sample Delay - Positive
                            if (sfxSample.MinDelay >= 0 && sfxSample.MaxDelay > 0)
                            {
                                float delay = random.Next(sfxSample.MinDelay, sfxSample.MaxDelay) / 1000.0f;
                                if (delay > 0)
                                {
                                    int numSilenceSamples = (int)(waveFormat.SampleRate * delay);
                                    byte[] silenceBytes = new byte[numSilenceSamples * 2];

                                    //Add silence samples to the buffer
                                    soundToPlay = new BufferedWaveProvider(waveFormat)
                                    {
                                        BufferLength = sampleData.EncodedData.Length + silenceBytes.Length,
                                        DiscardOnBufferOverflow = true
                                    };
                                    soundToPlay.AddSamples(silenceBytes, 0, silenceBytes.Length);
                                }
                            }

                            int exitAt = 0;
                            //Check Inter-Sample Delay - Negative
                            if (sfxSample.MinDelay <= 0 && sfxSample.MaxDelay < 0)
                            {
                                float delay = random.Next(Math.Abs(sfxSample.MinDelay), Math.Abs(sfxSample.MaxDelay)) / 1000.0f;
                                if (delay > 0)
                                {
                                    int numSilenceSamples = (int)(waveFormat.SampleRate * delay);
                                    exitAt = numSilenceSamples * 2;
                                }
                            }

                            //Add SFX samples to the buffer
                            soundToPlay.AddSamples(sampleData.EncodedData, 0, sampleData.EncodedData.Length);
                            VolumeSampleProvider volumeProvider = new VolumeSampleProvider(soundToPlay.ToSampleProvider()) { Volume = audioMaths.GetVolume(sampleInfo) };

                            //Init new voice
                            int vIndex = PCAudioDll.pcOutVoices.RequestVoice(sampleData.Flags == 1, PCAudioDll.outputConsole);

                            //Play Sample
                            indexBuff.Add(vIndex, soundToPlay);
                            _waveOut.Init(volumeProvider);
                            _waveOut.Play();
                            _waveOut.Volume = sfxSample.MasterVolume;
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 0)
                            {
                                while (soundToPlay.BufferedBytes > exitAt && !PCAudioDll.StopSfx)
                                {
                                    Thread.Sleep(1);
                                }

                                //Stop and remove
                                soundToPlay.ClearBuffer();

                                //Close Voice
                                PCAudioDll.pcOutVoices.PreCloseVoice(vIndex, PCAudioDll.outputConsole);
                                PCAudioDll.pcOutVoices.CloseVoice(vIndex);
                            }
                        }
                    }

                    //Check if there are playing
                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                    {
                        bool voicesArePlaying = true;
                        while (voicesArePlaying && !PCAudioDll.StopSfx)
                        {
                            foreach (KeyValuePair<int, BufferedWaveProvider> outVoice in indexBuff)
                            {
                                if (outVoice.Value.BufferedBytes == 0)
                                {
                                    voicesArePlaying = false;

                                    //Close Voice
                                    PCAudioDll.pcOutVoices.PreCloseVoice(outVoice.Key, PCAudioDll.outputConsole);
                                    PCAudioDll.pcOutVoices.CloseVoice(outVoice.Key);
                                    outVoice.Value.ClearBuffer();
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
                        indexBuff.Clear();
                    }

                } while (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Loop) & 1) == 1 && !PCAudioDll.StopSfx);
            })
            {
                IsBackground = true
            }.Start();
        }
    }
}
