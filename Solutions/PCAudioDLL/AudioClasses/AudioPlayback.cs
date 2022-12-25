using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PCAudioDLL.MusXStuff.Objects;
using System.IO;

namespace PCAudioDLL.AudioClasses
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioPlayback
    {
        private readonly AudioMaths audioMaths = new AudioMaths();

        //-------------------------------------------------------------------------------------------------------------------------------
        internal IWaveProvider GetWaveProvider(SampleData sampleData, SampleInfo sampleInfo, int minDelay, int maxDelay)
        {
            //Create Provider
            RawSourceWaveStream provider = new RawSourceWaveStream(new MemoryStream(sampleData.EncodedData), new WaveFormat(audioMaths.SemitonesToFreq(sampleData.Frequency, audioMaths.GetPitch(sampleInfo)), 16, 1));
            LoopStream loop = new LoopStream(provider, sampleData.LoopStartOffset)
            {
                EnableLooping = sampleData.Flags == 1,
                Position = 0
            };
            PanningSampleProvider panProvider = new PanningSampleProvider(loop.ToSampleProvider()) { Pan = audioMaths.GetPan(sampleInfo) };
            VolumeSampleProvider volumeProvider = new VolumeSampleProvider(panProvider) { Volume = audioMaths.GetVolume(sampleInfo) };

            return volumeProvider.ToWaveProvider();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
