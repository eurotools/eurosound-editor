//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Reverb Effect
//-------------------------------------------------------------------------------------------------------------------------------
using System;

namespace PCAudioDLL.Audio_Player
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioReverb // Schroeder
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal float[] ApplyEffect(float[] inputSamples, int sampleRate, float delayInMilliseconds, float decayFactor, float mixPercent)
        {
            int bufferSize = inputSamples.Length;

            //Method calls for the 4 Comb Filters in parallel. Defined at the bottom
            float[] combFilterSamples1 = CombFilter(inputSamples, bufferSize, delayInMilliseconds, decayFactor, sampleRate);
            float[] combFilterSamples2 = CombFilter(inputSamples, bufferSize, (delayInMilliseconds - 11.73f), (decayFactor - 0.1313f), sampleRate);
            float[] combFilterSamples3 = CombFilter(inputSamples, bufferSize, (delayInMilliseconds + 19.31f), (decayFactor - 0.2743f), sampleRate);
            float[] combFilterSamples4 = CombFilter(inputSamples, bufferSize, (delayInMilliseconds - 7.97f), (decayFactor - 0.31f), sampleRate);

            //Adding the 4 Comb Filters
            float[] outputComb = new float[bufferSize];
            for (int i = 0; i < outputComb.Length; i++)
            {
                outputComb[i] = ((combFilterSamples1[i] + combFilterSamples2[i] + combFilterSamples3[i] + combFilterSamples4[i]));
            }

            //Algorithm for Dry/Wet Mix in the output audio
            float[] mixAudio = new float[bufferSize];
            for (int i = 0; i < mixAudio.Length; i++)
            {
                mixAudio[i] = ((100 - mixPercent) * inputSamples[i]) + (mixPercent * outputComb[i]);
            }

            //Method calls for 2 All Pass Filters. Defined at the bottom
            float[] allPassFilterSamples1 = AllPassFilter(mixAudio, bufferSize, sampleRate);
            float[] allPassFilterSamples2 = AllPassFilter(allPassFilterSamples1, bufferSize, sampleRate);

            return allPassFilterSamples2;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private float[] CombFilter(float[] samples, int samplesLength, float delayinMilliSeconds, float decayFactor, float sampleRate)
        {
            //Calculating delay in samples from the delay in Milliseconds. Calculated from number of samples per millisecond
            int delaySamples = (int)((float)delayinMilliSeconds * (sampleRate / 1000));

            float[] combFilterSamples = new float[samples.Length];
            Array.Copy(samples, combFilterSamples, samples.Length);

            //Applying algorithm for Comb Filter
            for (int i = 0; i < samplesLength - delaySamples; i++)
            {
                combFilterSamples[i + delaySamples] += (combFilterSamples[i] * decayFactor);
            }
            return combFilterSamples;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private float[] AllPassFilter(float[] samples, int samplesLength, float sampleRate)
        {
            int delaySamples = (int)((float)89.27f * (sampleRate / 1000)); // Number of delay samples. Calculated from number of samples per millisecond
            float[] allPassFilterSamples = new float[samplesLength];
            float decayFactor = 0.131f;

            //Applying algorithm for All Pass Filter
            for (int i = 0; i < samplesLength; i++)
            {
                allPassFilterSamples[i] = samples[i];

                if (i - delaySamples >= 0)
                {
                    allPassFilterSamples[i] += -decayFactor * allPassFilterSamples[i - delaySamples];
                }

                if (i - delaySamples >= 1)
                {
                    allPassFilterSamples[i] += decayFactor * allPassFilterSamples[i + 20 - delaySamples];
                }
            }

            //This is for smoothing out the samples and normalizing the audio. Without implementing this, the samples overflow causing clipping of audio
            float value = allPassFilterSamples[0];
            float max = 0.0f;

            for (int i = 0; i < samplesLength; i++)
            {
                if (Math.Abs(allPassFilterSamples[i]) > max)
                {
                    max = Math.Abs(allPassFilterSamples[i]);
                }
            }

            for (int i = 0; i < allPassFilterSamples.Length; i++)
            {
                float currentValue = allPassFilterSamples[i];
                value = ((value + (currentValue - value)) / max);

                allPassFilterSamples[i] = value;
            }
            return allPassFilterSamples;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
