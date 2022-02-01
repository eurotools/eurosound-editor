using System;

namespace sb_explorer.AudioDecoders
{
    internal class DspAdpcm
    {
        private readonly int SamplesPerFrame = 14, NibblesPerFrame = 16;
        private readonly sbyte[] SignedNibbles = { 0, 1, 2, 3, 4, 5, 6, 7, -8, -7, -6, -5, -4, -3, -2, -1 };

        private class GcAdpcmParameters
        {
            public int SampleCount { get; set; } = -1;
            public short History1 { get; set; }
            public short History2 { get; set; }
        }

        public short[] Decode(byte[] adpcm, short[] coefficients)
        {
            GcAdpcmParameters config = new GcAdpcmParameters
            {
                SampleCount = NibbleCountToSampleCount(adpcm.Length * 2)
            };
            short[] pcm = new short[config.SampleCount];

            if (config.SampleCount > 0)
            {
                int frameCount = (int)Math.Ceiling((double)config.SampleCount / SamplesPerFrame);
                int currentSample = 0, outIndex = 0, inIndex = 0;
                short hist1 = config.History1;
                short hist2 = config.History2;

                for (int i = 0; i < frameCount; i++)
                {
                    byte predictorScale = adpcm[inIndex++];
                    int scale = (1 << (byte)(predictorScale & 0xF)) * 2048;
                    int predictor = (byte)((predictorScale >> 4) & 0xF);
                    short coef1 = coefficients[predictor * 2];
                    short coef2 = coefficients[predictor * 2 + 1];

                    int samplesToRead = Math.Min(SamplesPerFrame, config.SampleCount - currentSample);
                    for (int s = 0; s < samplesToRead; s++)
                    {
                        int adpcmSample;
                        if (s % 2 == 0)
                        {
                            adpcmSample = GetHighNibbleSigned(adpcm[inIndex]);
                        }
                        else
                        {
                            adpcmSample = GetLowNibbleSigned(adpcm[inIndex++]);
                        }

                        int distance = scale * adpcmSample;
                        int predictedSample = coef1 * hist1 + coef2 * hist2;
                        int correctedSample = predictedSample + distance;
                        int scaledSample = (correctedSample + 1024) >> 11;
                        short clampedSample = Clamp16(scaledSample);

                        hist2 = hist1;
                        hist1 = clampedSample;

                        pcm[outIndex++] = clampedSample;
                        currentSample++;
                    }
                }
            }
            return pcm;
        }

        private sbyte GetHighNibbleSigned(byte value)
        {
            return SignedNibbles[(value >> 4) & 0xF];
        }

        private sbyte GetLowNibbleSigned(byte value)
        {
            return SignedNibbles[value & 0xF];
        }

        private short Clamp16(int value)
        {
            int clampedVal = value;
            if (value > short.MaxValue)
            {
                clampedVal = short.MaxValue;
            }

            if (value < short.MinValue)
            {
                clampedVal = short.MinValue;
            }

            return (short)clampedVal;
        }

        private int NibbleCountToSampleCount(int nibbleCount)
        {
            int frames = nibbleCount / NibblesPerFrame;
            int extraNibbles = nibbleCount % NibblesPerFrame;
            int extraSamples = extraNibbles < 2 ? 0 : extraNibbles - 2;

            return SamplesPerFrame * frames + extraSamples;
        }
    }
}
