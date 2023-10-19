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
using PCAudioDLL.MusX_Objects;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class PCAudio
    {
        internal Dictionary<uint, SoundBank> LoadedSoundBanks = new Dictionary<uint, SoundBank>();
        private SoundDetails soundDetails;
        public readonly AudioVoices audioVoices = new AudioVoices();
        private readonly List<StreamSample> streamedFile = new List<StreamSample>();
        private string sbOutputPlatform;

        //-------------------------------------------------------------------------------------------------------------------------------
        public void LoadSoundDetails(string soundBankPlatform, string soundBankPath)
        {
            if (File.Exists(soundBankPath))
            {
                SoundDetailsReader soundDetailsReader = new SoundDetailsReader();
                SfxCommonHeader soundDetailsHeaderData = soundDetailsReader.ReadCommonHeader(soundBankPath, soundBankPlatform);
                soundDetails = soundDetailsReader.ReadSoundDetailsFile(soundBankPath, soundDetailsHeaderData);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public double LoadSoundBank(string soundBankPlatform, string soundBankPath, bool isStream = false)
        {
            sbOutputPlatform = soundBankPlatform;
            Stopwatch watch = Stopwatch.StartNew();

            //Add Debug Text
            DebugConsole.WriteLine("");
            DebugConsole.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            DebugConsole.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            DebugConsole.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            DebugConsole.WriteLine("");
            DebugConsole.WriteLine("CMD_SFXLOADSOUNDBANK");

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
                LoadedSoundBanks.Add(soundBankHeaderData.FileHashCode, sbData);
            }

            return watch.Elapsed.TotalMilliseconds;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void UnloadSoundbank(int sbHashCode = -1)
        {
            if (sbHashCode == -1)
            {
                LoadedSoundBanks.Clear();
                DebugConsole.WriteLine("ES-> ES_UnLoadAllSoundBanksReleaseFinished End");
                DebugConsole.WriteLine("pih");
            }
            else if (LoadedSoundBanks.ContainsKey((uint)sbHashCode))
            {
                LoadedSoundBanks.Remove((uint)sbHashCode);
                DebugConsole.WriteLine("ES-> ES_UnLoadSoundBankReleaseFinished End");
                DebugConsole.WriteLine("pih");
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

                        //Update inner and outer radius
                        foreach (var sfxItem in soundDetails.sfxItems)
                        {
                            if (sfxItem.HashCode == SoundHashcode)
                            {
                                sfxSample.InnerRadius = (short)sfxItem.InnerRadius;
                                sfxSample.OuterRadius = (short)sfxItem.OuterRadius;
                            }
                        }

                        //Ensure that is valid
                        if ((ContainStreams(sfxSample) && streamedFile.Count > 0) || !ContainStreams(sfxSample))
                        {
                            //Play SFX depending on the sound type
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                            {
                                if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                                {
                                    audioPlayer.PlayPolyphonicSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                }
                                else if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Shuffled) & 1) == 1)
                                {
                                    audioPlayer.PlayShuffledSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                }
                                else
                                {
                                    audioPlayer.PlaySingleSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                }
                            }
                            else
                            {
                                foreach (SampleInfo sample in sfxSample.samplesList)
                                {
                                    uint hashCode = (uint)(0x1AF00000 + sample.FileRef);
                                    if (hashCode == SoundHashcode)
                                    {
                                        break;
                                    }
                                    StartSound(hashCode, true);
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

                        //Update inner and outer radius
                        foreach (var sfxItem in soundDetails.sfxItems)
                        {
                            if (sfxItem.HashCode == SoundHashcode)
                            {
                                sfxSample.InnerRadius = (short)sfxItem.InnerRadius;
                                sfxSample.OuterRadius = (short)sfxItem.OuterRadius;
                            }
                        }

                        //Ensure that is valid
                        if ((ContainStreams(sfxSample) && streamedFile.Count > 0) || !ContainStreams(sfxSample))
                        {
                            //Play SFX depending on the sound type
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                            {
                                if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                                {
                                    audioPlayer.PlayPolyphonicSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                }
                                else if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Shuffled) & 1) == 1)
                                {
                                    audioPlayer.PlayShuffledSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                }
                                else
                                {
                                    audioPlayer.PlaySingleSfx(sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                }
                            }
                            else
                            {
                                foreach (SampleInfo sample in sfxSample.samplesList)
                                {
                                    uint hashCode = (uint)(0x1AF00000 + sample.FileRef);
                                    if (hashCode == SoundHashcode)
                                    {
                                        break;
                                    }
                                    StartSound3D(hashCode, audioPosition, true, enablePanning, volume);
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
                        audioVoices.MixerTable[i].Active = false;
                        audioVoices.MixerTable[i].Played = false;
                        audioVoices.MixerTable[i].Playing = false;
                        audioVoices.MixerTable[i].Looping = false;
                        audioVoices.MixerTable[i].Reverb = false;
                        audioVoices.MixerTable[i].Stop_ = false;
                        audioVoices.MixerTable[i].Stopped = false;
                        audioVoices.MixerTable[i].Locked = false;

                        //Inform User
                        DebugConsole.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", i));
                        DebugConsole.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", i));
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
                        audioVoices.MixerTable[i].Active = false;
                        audioVoices.MixerTable[i].Played = false;
                        audioVoices.MixerTable[i].Playing = false;
                        audioVoices.MixerTable[i].Looping = false;
                        audioVoices.MixerTable[i].Reverb = false;
                        audioVoices.MixerTable[i].Stop_ = false;
                        audioVoices.MixerTable[i].Stopped = false;
                        audioVoices.MixerTable[i].Locked = false;

                        //Inform User
                        DebugConsole.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", i));
                        DebugConsole.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", i));
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private bool ContainStreams(Sample sfxSample)
        {
            bool containStreams = false;
            foreach (SampleInfo sfxSampleData in sfxSample.samplesList)
            {
                if (sfxSampleData.FileRef < 0)
                {
                    containStreams = true;
                    break;
                }
            }

            return containStreams;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
