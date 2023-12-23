//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Audio Maths
//-------------------------------------------------------------------------------------------------------------------------------
using System;

namespace PCAudioDLL.Audio_Player
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
        internal float GetEffectValue(float effectValue, float effectRandomValue)
        {
            switch (RandomInt(0, 2))
            {
                case 0:
                    return effectValue + effectRandomValue;
                case 1:
                    return effectValue + (effectRandomValue * -1);
                default:
                    return effectValue;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal uint RandomUint()
        {
            /*uint r1 = 0x12345678;
            uint r2 = 0x87654321;*/
            uint r1 = (uint)DateTime.Now.Ticks; // Utiliza el reloj del sistema para la semilla
            uint r2 = (uint)Guid.NewGuid().GetHashCode(); // Utiliza un valor aleatorio

            r1 += r2;
            r2 += r1 + (r1 >> 31);

            return r2;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int RandomInt(int min, int max)
        {
            int range = (max + 1) - min;
            if (range <= 0)
            {
                return min;
            }
            return ((int)(RandomUint() & 0x7fffffff) % range) + min;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private float NormaliseDistance(float dist, float min, float max)
        {
            if (dist >= max)
            {
                return 1;
            }
            if (dist <= min)
            {
                return 0;
            }
            float t = dist - min;
            return t / (max - min);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float CalcVolumeFromDist(float dist, float innerRadius, float outerRadius)
        {
            return 1.0f - NormaliseDistance(dist, innerRadius, outerRadius);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float CalcDistFromListener(float[] ListenerPos, float[] AudioPos)
        {
            /*
            float diffX = ListenerPos[0] - AudioPos[0];
            float diffY = ListenerPos[1] - AudioPos[1];
            float diffZ = ListenerPos[2] - AudioPos[2];

            float distance = (float)Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2) + Math.Pow(diffZ, 2));
            */

            //Listener will be always at the position 0,0,0
            float distance = (float)Math.Sqrt(Math.Pow(AudioPos[0], 2.0f) + Math.Pow(AudioPos[1], 2.0f) + Math.Pow(AudioPos[2], 2.0f));
            return distance;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float CalcPanFromPos(float dist, float[] AudioPos)
        {
            // avoid divisions by zero
            float pan = AudioPos[0] / Math.Max(dist, 1.0f);

            // clamp
            pan = Math.Min(Math.Max(pan, -1.0f), 1.0f);

            return pan;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int CalculateInterSample(int minDelay, int maxDelay, int Frequency, bool exitBefore = false)
        {
            int numofBytes = 0;

            if (exitBefore && (minDelay >= 0 || maxDelay >= 0))
            {
                return numofBytes;
            }
            else
            {
                int minDelayAbs = Math.Min(Math.Abs(minDelay), Math.Abs(maxDelay));
                int maxDelayAbs = Math.Max(Math.Abs(minDelay), Math.Abs(maxDelay));

                float delayInMilliseconds = RandomInt(minDelayAbs, maxDelayAbs) / 1000.0f;
                if (delayInMilliseconds > 0)
                {
                    int numSilenceSamples = (int)(Frequency * delayInMilliseconds);
                    numofBytes = numSilenceSamples * 2;
                }

                return numofBytes;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal float[] ConvertPcmToFloat(short[] pInPcm16, int SampleCount)
        {
            float[] pOutPCMFloat = new float[SampleCount];

            for (int i = 0; i < SampleCount; i++)
            {
                pOutPCMFloat[i] = pInPcm16[i] * (1.0f / 32768.0f);
            }

            return pOutPCMFloat;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal short[] ConvertFloatToPcm(float[] pInFloat, int SampleCount)
        {
            short[] pOutPCMShort = new short[SampleCount];

            for (int i = 0; i < SampleCount; i++)
            {
                int sample = (int)(pInFloat[i] * 32768.0f);
                sample = Math.Max(-32768, Math.Min(sample, 32767));

                pOutPCMShort[i] = (short)sample;
            }

            return pOutPCMShort;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal byte[] ShortArrayToByteArray(short[] inputArray)
        {
            byte[] byteArray = new byte[inputArray.Length * 2];
            Buffer.BlockCopy(inputArray, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
