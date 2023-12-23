//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// PC Audio
//-------------------------------------------------------------------------------------------------------------------------------
using MusX;
using MusX.Objects;
using MusX.Readers;
using NAudio.Wave;
using PCAudioDLL.Audio_Player;
using PCAudioDLL.Codecs;
using PCAudioDLL.MusX_Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class PCAudio
    {
        //Sb Data
        private readonly List<StreamSample> streamedFile = new List<StreamSample>();
        private string sbOutputPlatform;
        public readonly AudioVoices audioVoices = new AudioVoices();
        internal Dictionary<uint, SoundBank> LoadedSoundBanks = new Dictionary<uint, SoundBank>();

        //-------------------------------------------------------------------------------------------------------------------------------
        public double LoadSoundBank(string soundBankPlatform, string soundBankPath, bool isStream = false)
        {
            sbOutputPlatform = soundBankPlatform;
            Stopwatch watch = Stopwatch.StartNew();

            //Add Debug Text
            PCAudioDebugConsole.WriteLine("");
            PCAudioDebugConsole.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            PCAudioDebugConsole.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            PCAudioDebugConsole.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            PCAudioDebugConsole.WriteLine("");
            PCAudioDebugConsole.WriteLine("CMD_SFXLOADSOUNDBANK");

            //Load data
            if (isStream)
            {
                streamedFile.Clear();
                StreamBankReader reader = new StreamBankReader();
                StreambankHeader soundBankHeaderData = reader.ReadStreamBankHeader(soundBankPath, soundBankPlatform);
                reader.ReadStreamBank(soundBankPath, soundBankHeaderData, streamedFile);
            }
            else
            {
                //Initialize SoundBank
                SoundBank sbData = new SoundBank
                {
                    sfxSamples = new SortedDictionary<uint, Sample>(),
                    sfxStoredData = new List<SampleData>()
                };

                //Read File
                SoundBankReader reader = new SoundBankReader();
                SoundbankHeader soundBankHeaderData = reader.ReadSfxHeader(soundBankPath, soundBankPlatform);
                reader.ReadSoundBank(soundBankPath, soundBankHeaderData, sbData.sfxSamples, sbData.sfxStoredData, null);
                if (!LoadedSoundBanks.ContainsKey(soundBankHeaderData.FileHashCode))
                {
                    LoadedSoundBanks.Add(soundBankHeaderData.FileHashCode, sbData);
                }
            }

            return watch.Elapsed.TotalMilliseconds;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void UnloadSoundbank(int sbHashCode = -1)
        {
            if (sbHashCode == -1)
            {
                LoadedSoundBanks.Clear();
                PCAudioDebugConsole.WriteLine("ES-> ES_UnLoadAllSoundBanksReleaseFinished End");
                PCAudioDebugConsole.WriteLine("pih");
            }
            else if (LoadedSoundBanks.ContainsKey((uint)sbHashCode))
            {
                LoadedSoundBanks.Remove((uint)sbHashCode);
                PCAudioDebugConsole.WriteLine("ES-> ES_UnLoadSoundBankReleaseFinished End");
                PCAudioDebugConsole.WriteLine("pih");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public bool IsSoundBankLoaded(int sbHashCode)
        {
            return LoadedSoundBanks.ContainsKey((uint)sbHashCode);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void StartSound(uint SoundHashcode, bool isSubSFX = false)
        {
            AudioPlayer audioPlayer = new AudioPlayer();

            if (audioVoices.CanPlay() || isSubSFX)
            {
                //Iterate over all SoundBanks
                foreach (KeyValuePair<uint, SoundBank> soundBank in LoadedSoundBanks)
                {
                    //Check SoundBanks SFXs
                    if (soundBank.Value.sfxSamples.ContainsKey(SoundHashcode))
                    {
                        Sample sfxSample = soundBank.Value.sfxSamples[SoundHashcode];
                        if ((ContainStreams(sfxSample) && streamedFile.Count > 0)|| !ContainStreams(sfxSample))
                        {
                            //Play SFX depending on the sound type
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                            {
                                //If false it will pick and play randomly one of the samples in the list. 
                                if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.MultiSample) & 1) == 0)
                                {
                                    audioPlayer.PlaySingleSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                }
                                else
                                {
                                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                                    {
                                        audioPlayer.PlayPolyphonicSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                    }
                                    else
                                    {
                                        audioPlayer.PlayShuffledSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                    }
                                }
                            }
                            else
                            {
                                foreach (SampleInfo sample in sfxSample.samplesList)
                                {
                                    StartSound((uint)(0x1A000000 + sample.FileRef), true);
                                }
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void StartSound3D(uint SoundHashcode, float[] audioPosition, bool isSubSFX = false, bool enablePanning = false, int volume = -1)
        {
            AudioPlayer audioPlayer = new AudioPlayer();

            if (audioVoices.CanPlay() || isSubSFX)
            {
                //Iterate over all SoundBanks
                foreach (KeyValuePair<uint, SoundBank> soundBank in LoadedSoundBanks)
                {
                    //Check SoundBanks SFXs
                    if (soundBank.Value.sfxSamples.ContainsKey(SoundHashcode))
                    {
                        Sample sfxSample = soundBank.Value.sfxSamples[SoundHashcode];
                        if ((ContainStreams(sfxSample) && streamedFile.Count > 0) || !ContainStreams(sfxSample))
                        {
                            //Play SFX depending on the sound type
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                            {
                                //If false it will pick and play randomly one of the samples in the list. 
                                if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.MultiSample) & 1) == 0)
                                {
                                    audioPlayer.PlaySingleSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                }
                                else
                                {
                                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                                    {
                                        audioPlayer.PlayPolyphonicSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                    }
                                    else
                                    {
                                        audioPlayer.PlayShuffledSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                    }
                                }
                            }
                            else
                            {
                                foreach (SampleInfo sample in sfxSample.samplesList)
                                {
                                    StartSound3D((uint)(0x1A000000 + sample.FileRef), audioPosition, true, enablePanning, volume);
                                }
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void StopPlayer()
        {
            audioVoices.ExitSound = true;
            for (int i = 0; i < audioVoices.MixerTable.Length; i++)
            {
                if (audioVoices.MixerTable[i] != null)
                {
                    if (audioVoices.MixerTable[i].BaseVoice != null && audioVoices.MixerTable[i].BaseVoice.PlaybackState == PlaybackState.Playing)
                    {
                        audioVoices.MixerTable[i].BaseVoice.Stop();
                        audioVoices.MixerTable[i].BaseVoice.Dispose();

                        //Update Object
                        audioVoices.StopVoice(i);
                        audioVoices.CloseVoice(i);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void StopHashCode(uint hashCode)
        {
            audioVoices.ExitSound = true;
            for (int i = 0; i < audioVoices.MixerTable.Length; i++)
            {
                if (audioVoices.MixerTable[i] != null && audioVoices.MixerTable[i].HashCode == hashCode)
                {
                    if (audioVoices.MixerTable[i].BaseVoice != null && audioVoices.MixerTable[i].BaseVoice.PlaybackState == PlaybackState.Playing)
                    {
                        audioVoices.MixerTable[i].BaseVoice.Stop();
                        audioVoices.MixerTable[i].BaseVoice.Dispose();

                        //Update Object
                        audioVoices.StopVoice(i);
                        audioVoices.CloseVoice(i);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private bool ContainStreams(Sample sfxSample)
        {
            bool containStreams = false;
            foreach(SampleInfo sfxSampleData in sfxSample.samplesList)
            {
                if (sfxSampleData.FileRef < 0)
                {
                    containStreams = true;
                    break;
                }
            }

            return containStreams;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void InitializeConsole(TextBox outputControl)
        {
            PCAudioDebugConsole.TxtConsole = outputControl;
            PCAudioDebugConsole.WriteLine("Debug Console Initialised!");
            PCAudioDebugConsole.WriteLine("5.1 Mixer Initialise");
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
