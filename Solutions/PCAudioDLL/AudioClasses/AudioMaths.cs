using PCAudioDLL.MusXStuff.Objects;
using System;

namespace PCAudioDLL.AudioClasses
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
            float[] rndPitch = new float[] { 0, sampleInfo.PitchOffset, sampleInfo.PitchOffset * -1 };
            float _pitch = sampleInfo.Pitch + rndPitch[Utils.random.Next(0, rndPitch.Length)];

            return _pitch;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float GetPan(SampleInfo sampleInfo)
        {
            float[] rndPan = new float[] { 0, sampleInfo.PanOffset, sampleInfo.PanOffset * -1 };
            float _pan = sampleInfo.Pan + rndPan[Utils.random.Next(0, rndPan.Length)];

            return _pan;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float GetVolume(SampleInfo sampleInfo)
        {
            float[] rndVolume = new float[] { 0, sampleInfo.VolumeOffset, sampleInfo.VolumeOffset * -1 };
            float _volume = sampleInfo.Volume + rndVolume[Utils.random.Next(0, rndVolume.Length)];

            return _volume;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
