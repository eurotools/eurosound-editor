//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Low Pass Filter
//-------------------------------------------------------------------------------------------------------------------------------
using NAudio.Dsp;
using NAudio.Wave;

namespace PCAudioDLL.Audio_Player.Effects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioLowPassFilter : ISampleProvider
    {
        private readonly ISampleProvider sourceProvider;
        private readonly float cutOffFreq;
        private readonly int channels;
        private readonly int sampleRate;
        private readonly BiQuadFilter[] filters;

        //-------------------------------------------------------------------------------------------------------------------------------
        internal AudioLowPassFilter(ISampleProvider sourceProvider, int cutOffFreq)
        {
            this.sourceProvider = sourceProvider;
            this.cutOffFreq = cutOffFreq;

            sampleRate = sourceProvider.WaveFormat.SampleRate;
            channels = sourceProvider.WaveFormat.Channels;
            filters = new BiQuadFilter[channels];
            CreateFilters();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void CreateFilters()
        {
            for (int n = 0; n < channels; n++)
            {
                if (filters[n] == null)
                {
                    filters[n] = BiQuadFilter.LowPassFilter(sampleRate, cutOffFreq, 1);
                }
                else
                {
                    filters[n].SetLowPassFilter(sampleRate, cutOffFreq, 1);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public WaveFormat WaveFormat { get { return sourceProvider.WaveFormat; } }

        //-------------------------------------------------------------------------------------------------------------------------------
        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);

            for (int i = 0; i < samplesRead; i++)
            {
                buffer[offset + i] = filters[(i % channels)].Transform(buffer[offset + i]);
            }

            return samplesRead;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
