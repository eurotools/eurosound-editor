//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// PCAudio DLL
//-------------------------------------------------------------------------------------------------------------------------------
using MusX.Objects;
using MusX.Readers;
using NAudio.Wave;
using PCAudioDLL.Audio_Player;
using PCAudioDLL.MusX_Objects;
using PCAudioDLL.Objects;
using System.Collections.Generic;
using System.Threading;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioPlayer
    {
        private readonly AudioMaths audioMaths = new AudioMaths();
        private readonly AudioMixer audioPlayer = new AudioMixer();

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlaySingleSfx(string sbPlatform, List<StreamSample> streamedFile, Sample sfxSample, SoundBank soundBank, AudioVoices pcVoices, float[] audioPosition = null, bool enablePanning = false, int fixedVolume = -1)
        {
            pcVoices.ExitSound = false;
            bool LoopFlag = ((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Loop) & 1) == 1;

            //Calculate volume
            float volume = sfxSample.MasterVolume / 100.0f;
            if (fixedVolume != -1)
            {
                volume = fixedVolume / 100.0f;
            }

            //Calculate Panning
            float panning = 0;
            if (audioPosition != null && enablePanning)
            {
                float dist = audioMaths.CalcDistFromListener(null, audioPosition);
                panning = audioMaths.CalcPanFromPos(dist, audioPosition);
                volume = audioMaths.CalcVolumeFromDist(dist, sfxSample.InnerRadius, sfxSample.OuterRadius);
            }

            //Start work
            new Thread(() =>
            {
                do
                {
                    SampleInfo sampleInfo = sfxSample.samplesList[audioMaths.RandomInt(0, sfxSample.samplesList.Count - 1)];
                    if (sampleInfo.FileRef < soundBank.sfxStoredData.Count)
                    {
                        //Get all data
                        ExAudioSample sampleAudio = null;
                        if (sampleInfo.FileRef >= 0)
                        {
                            sampleAudio = audioPlayer.GetAudioSample(sbPlatform, soundBank, sfxSample.HashCodeNumber, sfxSample, sampleInfo);
                        }
                        else
                        {
                            sampleAudio = audioPlayer.GetStreamAudioSample(sbPlatform, streamedFile, sfxSample.HashCodeNumber, sfxSample, sampleInfo);
                        }

                        //Get a voice
                        if (audioPosition == null)
                        {
                            panning = audioMaths.GetEffectValue(sampleAudio.Pan, sampleAudio.RandomPan) / 100.0f;
                        }
                        RawSourceWaveStream audioStr = audioPlayer.BuildWaveStream(sampleAudio);
                        IWaveProvider audioSample = audioPlayer.PlayAudioSample(audioStr, sampleAudio, panning);

                        //Request voice and play
                        int index = pcVoices.RequestVoice(sampleAudio);
                        pcVoices.MixerTable[index].BaseVoice.Init(audioSample);
                        pcVoices.MixerTable[index].BaseVoice.Volume = volume;
                        pcVoices.MixerTable[index].BaseVoice.Play();
                        DebugConsole.WriteLine("Voice::Play");

                        //InterSample delay
                        int exitAt = audioMaths.CalculateInterSample(sampleAudio.MinDelay, sampleAudio.MaxDelay, sampleAudio.Frequency, true);
                        while (audioStr.Position < (audioStr.Length - exitAt) && !pcVoices.ExitSound)
                        {
                            if (pcVoices.ExitSound)
                            {
                                break;
                            }
                            Thread.Sleep(1);
                        }
                    }
                } while (LoopFlag && !pcVoices.ExitSound);
            })
            {
                IsBackground = true
            }.Start();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayShuffledSfx(string sbPlatform, List<StreamSample> streamedFile, Sample sfxSample, SoundBank soundBank, AudioVoices pcVoices, float[] audioPosition = null, bool enablePanning = false, int fixedVolume = -1)
        {
            pcVoices.ExitSound = false;
            bool LoopFlag = ((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Loop) & 1) == 1;

            //Calculate volume
            float volume = sfxSample.MasterVolume / 100.0f;
            if (fixedVolume != -1)
            {
                volume = fixedVolume / 100.0f;
            }

            //Calculate Panning
            float panning = 0;
            if (audioPosition != null && enablePanning)
            {
                float dist = audioMaths.CalcDistFromListener(null, audioPosition);
                panning = audioMaths.CalcPanFromPos(dist, audioPosition);
                volume = audioMaths.CalcVolumeFromDist(dist, sfxSample.InnerRadius, sfxSample.OuterRadius);
            }

            //Start work
            new Thread(() =>
            {
                do
                {
                    //Randomize list
                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Shuffled) & 1) == 1)
                    {
                        sfxSample.samplesList.Shuffle();
                    }

                    foreach (SampleInfo sampleInfo in sfxSample.samplesList)
                    {
                        if (pcVoices.ExitSound)
                        {
                            break;
                        }
                        else
                        {
                            //Get all data
                            ExAudioSample sampleAudio = null;
                            if (sampleInfo.FileRef >= 0)
                            {
                                sampleAudio = audioPlayer.GetAudioSample(sbPlatform, soundBank, sfxSample.HashCodeNumber, sfxSample, sampleInfo);
                            }
                            else
                            {
                                sampleAudio = audioPlayer.GetStreamAudioSample(sbPlatform, streamedFile, sfxSample.HashCodeNumber, sfxSample, sampleInfo);
                            }

                            //Get a voice
                            if (audioPosition == null)
                            {
                                panning = audioMaths.GetEffectValue(sampleAudio.Pan, sampleAudio.RandomPan) / 100.0f;
                            }
                            RawSourceWaveStream audioStr = audioPlayer.BuildWaveStream(sampleAudio);
                            IWaveProvider audioSample = audioPlayer.PlayAudioSample(audioStr, sampleAudio, panning);

                            //Request voice and play
                            int index = pcVoices.RequestVoice(sampleAudio);
                            pcVoices.MixerTable[index].BaseVoice.Init(audioSample);
                            pcVoices.MixerTable[index].BaseVoice.Volume = volume;
                            pcVoices.MixerTable[index].BaseVoice.Play();
                            DebugConsole.WriteLine("Voice::Play");

                            //InterSample delay
                            int exitAt = audioMaths.CalculateInterSample(sampleAudio.MinDelay, sampleAudio.MaxDelay, sampleAudio.Frequency, true);
                            while (audioStr.Position < (audioStr.Length - exitAt) && !pcVoices.ExitSound)
                            {
                                if (pcVoices.ExitSound)
                                {
                                    break;
                                }
                                Thread.Sleep(1);
                            }
                        }
                    }
                } while (LoopFlag && !pcVoices.ExitSound);
            })
            {
                IsBackground = true
            }.Start();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayPolyphonicSfx(string sbPlatform, List<StreamSample> streamedFile, Sample sfxSample, SoundBank soundBank, AudioVoices pcVoices, float[] audioPosition = null, bool enablePanning = false, int fixedVolume = -1)
        {
            pcVoices.ExitSound = false;
            bool LoopFlag = ((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Loop) & 1) == 1;

            //Calculate volume
            float volume = sfxSample.MasterVolume / 100.0f;
            if (fixedVolume != -1)
            {
                volume = fixedVolume / 100.0f;
            }

            //Calculate Panning
            float panning = 0;
            if (audioPosition != null && enablePanning)
            {
                float dist = audioMaths.CalcDistFromListener(null, audioPosition);
                panning = audioMaths.CalcPanFromPos(dist, audioPosition);
                volume = audioMaths.CalcVolumeFromDist(dist, sfxSample.InnerRadius, sfxSample.OuterRadius);
            }

            //Start work
            new Thread(() =>
            {
                do
                {
                    List<int> PolyphonicAudios = new List<int>();

                    //Randomize list
                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Shuffled) & 1) == 1)
                    {
                        sfxSample.samplesList.Shuffle();
                    }

                    foreach (SampleInfo sampleInfo in sfxSample.samplesList)
                    {
                        if (pcVoices.ExitSound)
                        {
                            break;
                        }
                        else
                        {
                            //Get all data
                            ExAudioSample sampleAudio = null;
                            if (sampleInfo.FileRef >= 0)
                            {
                                sampleAudio = audioPlayer.GetAudioSample(sbPlatform, soundBank, sfxSample.HashCodeNumber, sfxSample, sampleInfo);
                            }
                            else
                            {
                                sampleAudio = audioPlayer.GetStreamAudioSample(sbPlatform, streamedFile, sfxSample.HashCodeNumber, sfxSample, sampleInfo);
                            }

                            //Get a voice
                            if (audioPosition == null)
                            {
                                panning = audioMaths.GetEffectValue(sampleAudio.Pan, sampleAudio.RandomPan) / 100.0f;
                            }
                            RawSourceWaveStream audioStr = audioPlayer.BuildWaveStream(sampleAudio);
                            IWaveProvider audioSample = audioPlayer.PlayAudioSample(audioStr, sampleAudio, panning);

                            //Request voice and play
                            int index = pcVoices.RequestVoice(sampleAudio);
                            pcVoices.MixerTable[index].BaseVoice.Init(audioSample);
                            pcVoices.MixerTable[index].BaseVoice.Volume = volume;
                            PolyphonicAudios.Add(index);
                        }
                    }

                    //Play all together
                    for (int i = 0; i < PolyphonicAudios.Count; i++)
                    {
                        pcVoices.MixerTable[PolyphonicAudios[i]].BaseVoice.Play();
                        DebugConsole.WriteLine("Voice::Play");
                    }

                    //Check if there are playing
                    bool voicesArePlaying = true;
                    while (voicesArePlaying && !pcVoices.ExitSound)
                    {
                        for (int i = 0; i < PolyphonicAudios.Count; i++)
                        {
                            if (pcVoices.MixerTable[PolyphonicAudios[i]].BaseVoice.PlaybackState == PlaybackState.Stopped)
                            {
                                voicesArePlaying = false;
                            }
                            else
                            {
                                voicesArePlaying = true;
                                break;
                            }
                        }
                    };
                } while (LoopFlag && !pcVoices.ExitSound);
            })
            {
                IsBackground = true
            }.Start();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
