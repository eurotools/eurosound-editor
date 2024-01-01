//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Base Audio Voice
//-------------------------------------------------------------------------------------------------------------------------------
using NAudio.Wave;
using PCAudioDLL.Objects;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class AudioVoices
    {
        internal bool ExitSound = false;
        internal const int MAX_TOTAL_VOICES = 60;
        internal const int MAX_TOTAL_STREAMS = 10;
        internal byte MixerTableIndex = MAX_TOTAL_STREAMS;
        public ExVoice[] MixerTable = new ExVoice[MAX_TOTAL_VOICES];

        //-------------------------------------------------------------------------------------------------------------------------------
        public AudioVoices()
        {
            for(int i = 0; i < MAX_TOTAL_STREAMS; i++)
            {
                MixerTable[i] = new ExVoice
                {
                    Active = true,
                    Looping = true,
                    Locked = true
                };
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int RequestVoice(ExAudioSample audioSample)
        {
            if (MixerTableIndex >= (MixerTable.Length - 1))
            {
                MixerTableIndex = MAX_TOTAL_STREAMS;
            }

            int index = MixerTableIndex++;
            MixerTable[index] = new ExVoice
            {
                BaseVoice = new WaveOut(),
                HashCode = (int)audioSample.HashCode
            };

            PCAudioDebugConsole.WriteLine(string.Format("ES-> ES_RequestVoiceHandle() = {0}", index));

            return index;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal bool CanPlay()
        {
            bool audioCanPlay = true;
            for (int i = 0; i < MixerTable.Length; i++)
            {
                if (MixerTable[i] != null && MixerTable[i].BaseVoice != null)
                {
                    if (MixerTable[i].BaseVoice.PlaybackState == PlaybackState.Playing)
                    {
                        audioCanPlay = false;
                        break;
                    }
                }
            }

            return audioCanPlay;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void InitialiseVoice(int index, float volume, bool LoopFlag, IWaveProvider audioSample)
        {
            MixerTable[index].BaseVoice.Init(audioSample);
            MixerTable[index].BaseVoice.Volume = volume;
            MixerTable[index].Active = true;
            MixerTable[index].Playing = false;
            MixerTable[index].Looping = LoopFlag;
            MixerTable[index].Locked = true;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayVoice(int index)
        {
            MixerTable[index].BaseVoice.Play();
            MixerTable[index].Played = true;
            MixerTable[index].Playing = true;
            MixerTable[index].Reverb = true;
            PCAudioDebugConsole.WriteLine("Voice::Play");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void StopVoice(int index)
        {
            MixerTable[index].Active = false;
            MixerTable[index].Playing = false;
            MixerTable[index].Played = true;
            MixerTable[index].Reverb = false;
            MixerTable[index].Looping = false;
            MixerTable[index].Stopped = true;
            PCAudioDebugConsole.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", index));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CloseVoice(int index)
        {
            MixerTable[index].Active = false;
            MixerTable[index].Playing = false;
            MixerTable[index].Played = false;
            MixerTable[index].Reverb = false;
            MixerTable[index].Looping = false;
            MixerTable[index].Stopped = false;
            MixerTable[index].Locked = false;
            PCAudioDebugConsole.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", index));
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
