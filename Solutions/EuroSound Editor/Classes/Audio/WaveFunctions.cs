using NAudio.Wave;
using System;
using System.Linq;

namespace sb_editor.Audio_Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class WaveFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal WavInfo ReadWaveProperties(string waveFilePath)
        {
            WavInfo waveFileData;
            using (WaveFileReader wReader = new WaveFileReader(waveFilePath))
            {
                waveFileData = new WavInfo
                {
                    Channels = wReader.WaveFormat.Channels,
                    BitsPerSample = wReader.WaveFormat.BitsPerSample,
                    SampleRate = wReader.WaveFormat.SampleRate,
                    AverageBytesPerSecond = wReader.WaveFormat.AverageBytesPerSecond,
                    SampleCount = wReader.SampleCount,
                    Length = wReader.Length,
                    TotalTime = wReader.TotalTime
                };

                // Read Sample Chunck
                RiffChunk smp = wReader.ExtraChunks.FirstOrDefault(ec => ec.IdentifierAsString.Equals("smpl", StringComparison.OrdinalIgnoreCase));
                if (smp != null)
                {
                    byte[] chunkData = wReader.GetChunkData(smp);
                    int midiNote = BitConverter.ToInt32(chunkData, 12);
                    int numberOfSamples = BitConverter.ToInt32(chunkData, 28);
                    int offset = 36;

                    for (int n = 0; n < numberOfSamples; n++)
                    {
                        // Read Chunck info
                        int cuePointId = BitConverter.ToInt32(chunkData, offset);
                        int type = BitConverter.ToInt32(chunkData, offset + 4); // 0 = loop forward, 1 = alternating loop, 2 = reverse
                        int loopStart = BitConverter.ToInt32(chunkData, offset + 8);
                        int loopEnd = BitConverter.ToInt32(chunkData, offset + 12);
                        int fraction = BitConverter.ToInt32(chunkData, offset + 16);
                        int playCount = BitConverter.ToInt32(chunkData, offset + 20);
                        offset += 24;

                        // Save Data
                        waveFileData.HasLoop = true;
                        waveFileData.LoopStart = loopStart;
                        waveFileData.LoopEnd = loopEnd;
                        waveFileData.MidiNote = midiNote;
                    }
                }
            }

            return waveFileData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal byte[] GetByteWaveData(string waveReader)
        {
            byte[] fileData;
            using (WaveFileReader wReader = new WaveFileReader(waveReader))
            {
                fileData = new byte[wReader.Length];
                wReader.Read(fileData, 0, fileData.Length);
            }
            return fileData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal short[] GetWaveSamples(string waveReader)
        {
            short[] samples = ConvertByteArrayToShortArray(GetByteWaveData(waveReader));
            return samples;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal short[] ConvertByteArrayToShortArray(byte[] PCMData)
        {
            short[] samplesShort = new short[PCMData.Length / 2];
            WaveBuffer sourceWaveBuffer = new WaveBuffer(PCMData);
            for (int i = 0; i < samplesShort.Length; i++)
            {
                samplesShort[i] = sourceWaveBuffer.ShortBuffer[i];
            }
            return samplesShort;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
