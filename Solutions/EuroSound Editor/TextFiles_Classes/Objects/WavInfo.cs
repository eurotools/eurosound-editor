using System;

namespace EuroSound_Editor.Audio_Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class WavInfo
    {
        public int Channels;
        public int SampleRate;
        public int BitsPerSample;
        public int AverageBytesPerSecond;
        public int LoopEnd;
        public int MidiNote;
        public int LoopStart;
        public long Length;
        public long SampleCount;
        public bool HasLoop;
        public TimeSpan TotalTime;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
