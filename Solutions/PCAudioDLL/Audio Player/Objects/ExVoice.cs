﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// ExVoice
//-------------------------------------------------------------------------------------------------------------------------------
using NAudio.Wave;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class ExVoice
    {
        public WaveOut BaseVoice { get; set; }
        public bool Active { get; set; }
        public bool Played { get; set; }
        public bool Playing { get; set; }
        public bool Looping { get; set; }
        public bool Reverb { get; set; }
        public bool Stop_ { get; set; }
        public bool Stopped { get; set; }
        public bool Locked { get; set; }
        public int HashCode { get; set; }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
