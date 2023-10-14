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
        internal byte MixerTableIndex = 0;
        internal ExVoice[] MixerTable = new ExVoice[MAX_TOTAL_VOICES];

        //-------------------------------------------------------------------------------------------------------------------------------
        public void InitializeTable()
        {
            for (int i = 0; i < MixerTable.Length; i++)
            {
                MixerTable[i] = new ExVoice();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int RequestVoice(ExAudioSample audioSample)
        {
            if (MixerTableIndex >= (MixerTable.Length - 1))
            {
                MixerTableIndex = 0;
            }

            int index = MixerTableIndex++;
            MixerTable[index] = new ExVoice
            {
                BaseVoice = new WaveOut(),
                HashCode = (int)audioSample.HashCode
            };

            //Inform user
            DebugConsole.WriteLine(string.Format("ES-> ES_RequestVoiceHandle() = {0}", index));

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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
