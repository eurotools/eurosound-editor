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
        private readonly Dictionary<uint, SoundBank> LoadedSoundBanks = new Dictionary<uint, SoundBank>();
        private readonly List<StreamSample> streamedFile = new List<StreamSample>();
        private readonly int hashCodePrefix;
        public readonly AudioVoices audioVoices = new AudioVoices();
        private string sbOutputPlatform;
        private int fileVersion;
        private SoundDetails soundDetails;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCAudio(int _hashcodePrefix)
        {
            hashCodePrefix = _hashcodePrefix;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public double LoadSoundBank(string soundBankPlatform, string soundBankPath)
        {
            sbOutputPlatform = soundBankPlatform;
            Stopwatch watch = Stopwatch.StartNew();

            //Add Debug Text
            PCAudioDebugConsole.WriteLine("");
            PCAudioDebugConsole.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            PCAudioDebugConsole.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            PCAudioDebugConsole.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            PCAudioDebugConsole.WriteLine("");

            //Get file type and load
            SfxFunctions.FileType fileType = Utils.GetFileType(soundBankPath);
            if (fileType == SfxFunctions.FileType.SoundDetailsFile)
            {
                PCAudioDebugConsole.WriteLine("CMD_SFXLOADDETAILSBANK");
                SoundDetailsReader soundDetailsReader = new SoundDetailsReader();
                SfxCommonHeader soundDetailsHeaderData = soundDetailsReader.ReadCommonHeader(soundBankPath, soundBankPlatform);
                soundDetails = soundDetailsReader.ReadSoundDetailsFile(soundBankPath, soundDetailsHeaderData);
                fileVersion = soundDetailsHeaderData.FileVersion;
            }
            if (fileType == SfxFunctions.FileType.StreamFile)
            {
                PCAudioDebugConsole.WriteLine("CMD_SFXLOADSTREAMBANK");
                streamedFile.Clear();
                StreamBankReader reader = new StreamBankReader();
                StreambankHeader soundBankHeaderData = reader.ReadStreamBankHeader(soundBankPath, soundBankPlatform);
                reader.ReadStreamBank(soundBankPath, soundBankHeaderData, streamedFile);
                fileVersion = soundBankHeaderData.FileVersion;
            }
            else if (fileType == SfxFunctions.FileType.SoundbankFile || fileType == SfxFunctions.FileType.TestSFX)
            {
                PCAudioDebugConsole.WriteLine("CMD_SFXLOADSOUNDBANK");
                //Initialize SoundBank
                SoundBank sbData = new SoundBank
                {
                    sfxSamples = new SortedDictionary<uint, Sample>(),
                    sfxStoredData = new List<SampleData>()
                };

                //Read File
                SoundBankReader reader = new SoundBankReader();
                SoundbankHeader soundBankHeaderData = reader.ReadSfxHeader(soundBankPath, soundBankPlatform);
                if (!LoadedSoundBanks.ContainsKey(soundBankHeaderData.FileHashCode))
                {
                    fileVersion = soundBankHeaderData.FileVersion;
                    reader.ReadSoundBank(soundBankPath, soundBankHeaderData, sbData.sfxSamples, sbData.sfxStoredData, null);
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
                            //Check if we need to get inner & outer radius from soundDetails
                            if (soundDetails != null)
                            {
                                foreach (SoundDetailsData sfxItem in soundDetails.sfxItems)
                                {
                                    if (sfxItem.HashCode == SoundHashcode)
                                    {
                                        sfxSample.InnerRadius = (short)sfxItem.InnerRadius;
                                        sfxSample.OuterRadius = (short)sfxItem.OuterRadius;
                                    }
                                }
                            }

                            //Play SFX depending on the sound type
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                            {
                                //If false it will pick and play randomly one of the samples in the list. 
                                if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.MultiSample) & 1) == 0)
                                {
                                    audioPlayer.PlaySingleSfx(fileVersion, sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                }
                                else
                                {
                                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                                    {
                                        audioPlayer.PlayPolyphonicSfx(fileVersion, sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                    }
                                    else
                                    {
                                        audioPlayer.PlayShuffledSfx(fileVersion, sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices);
                                    }
                                }
                            }
                            else
                            {
                                foreach (SampleInfo sample in sfxSample.samplesList)
                                {
                                    StartSound((uint)(hashCodePrefix + sample.FileRef), true);
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
                            //Check if we need to get inner & outer radius from soundDetails
                            if (soundDetails != null)
                            {
                                foreach (SoundDetailsData sfxItem in soundDetails.sfxItems)
                                {
                                    if (sfxItem.HashCode == SoundHashcode)
                                    {
                                        sfxSample.InnerRadius = (short)sfxItem.InnerRadius;
                                        sfxSample.OuterRadius = (short)sfxItem.OuterRadius;
                                    }
                                }
                            }

                            //Play SFX depending on the sound type
                            if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                            {
                                //If false it will pick and play randomly one of the samples in the list. 
                                if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.MultiSample) & 1) == 0)
                                {
                                    audioPlayer.PlaySingleSfx(fileVersion, sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                }
                                else
                                {
                                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.Polyphonic) & 1) == 1)
                                    {
                                        audioPlayer.PlayPolyphonicSfx(fileVersion, sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                    }
                                    else
                                    {
                                        audioPlayer.PlayShuffledSfx(fileVersion, sbOutputPlatform, streamedFile, sfxSample, soundBank.Value, audioVoices, audioPosition, enablePanning, volume);
                                    }
                                }
                            }
                            else
                            {
                                foreach (SampleInfo sample in sfxSample.samplesList)
                                {
                                    StartSound3D((uint)(hashCodePrefix + sample.FileRef), audioPosition, true, enablePanning, volume);
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
