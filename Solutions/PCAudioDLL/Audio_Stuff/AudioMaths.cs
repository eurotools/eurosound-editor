using PCAudioDLL.MusX_Objects;
using System;

namespace PCAudioDLL.Audio_Stuff
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioMaths
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal int SemitonesToFreq(int Frequency, float Semitone)
        {
            float mult = 1.0f;
            if (Semitone != 0)
            {
                //In terms of frequencies, a semitone is equal to a frequency ratio of 2^(1/12)
                mult = (float)Math.Pow(2.0f, Semitone * (1.0f / 12.0f));
            }
            return (int)(Frequency * mult);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float GetPitch(SampleInfo sampleInfo)
        {
            Random random = new Random();
            switch (random.Next(0, 3))
            {
                case 0:
                    return sampleInfo.Pitch + sampleInfo.PitchOffset;
                case 1:
                    return sampleInfo.Pitch + (sampleInfo.PitchOffset * -1);
                default:
                    return sampleInfo.Pitch;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float GetPan(SampleInfo sampleInfo)
        {
            Random random = new Random();
            switch (random.Next(0, 3))
            {
                case 0:
                    return sampleInfo.Pan + sampleInfo.PanOffset;
                case 1:
                    return sampleInfo.Pan + (sampleInfo.PanOffset * -1);
                default:
                    return sampleInfo.Pan;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float GetVolume(SampleInfo sampleInfo)
        {
            Random random = new Random();
            switch (random.Next(0, 3))
            {
                case 0:
                    return sampleInfo.Volume + sampleInfo.VolumeOffset;
                case 1:
                    return sampleInfo.Volume + (sampleInfo.VolumeOffset * -1);
                default:
                    return sampleInfo.Volume;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
