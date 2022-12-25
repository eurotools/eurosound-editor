using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PCAudioDLL.MusXStuff.Objects;
using System.IO;

namespace PCAudioDLL.AudioClasses
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioMixer
    {
        private readonly AudioMaths audioMaths = new AudioMaths();

        //-------------------------------------------------------------------------------------------------------------------------------
        internal IWaveProvider GetWaveProviderLoop(SampleData sampleData, SampleInfo sampleInfo)
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

        //-------------------------------------------------------------------------------------------------------------------------------
        internal Stream ProviderToStream(IWaveProvider wavProv)
        {
            IWaveProvider sampleProv = wavProv;

            //Write wave to a stream
            MemoryStream outBuff = new MemoryStream();

            //Start reading
            WaveFileWriter.WriteWavFileToStream(outBuff, sampleProv);

            return outBuff;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal BufferedWaveProvider StreamToWaveBuffer(Stream streamData, WaveFormat wavHeadFormat)
        {
            //Initialize buffered Stream
            BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(wavHeadFormat)
            {
                ReadFully = false
            };
            bufferedWaveProvider.ClearBuffer();

            //Get Pcm Data
            streamData.Position = 0;
            using (WaveFileReader wReader = new WaveFileReader(streamData))
            {
                byte[] pcmData = new byte[wReader.Length];
                wReader.Read(pcmData, 0, pcmData.Length);

                //Add data to stream
                bufferedWaveProvider.BufferLength = pcmData.Length;
                bufferedWaveProvider.AddSamples(pcmData, 0, pcmData.Length);
            }

            return bufferedWaveProvider;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
