using NAudio.Wave;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void WriteSampleChunk(string outputFilePath, int startLoop, int endLoop)
        {
            using (FileStream inputFileStream = File.Open(outputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                uint dataChunkSize;
                int frequency;
                byte[] samples;

                //Read Wave Without any metadata
                using (BinaryReader bReader = new BinaryReader(inputFileStream))
                {
                    //Read Sample Rate
                    bReader.BaseStream.Seek(0x18, SeekOrigin.Begin);
                    frequency = bReader.ReadInt32();

                    //Read Data Chunk Size
                    bReader.BaseStream.Seek(0x28, SeekOrigin.Begin);
                    dataChunkSize = bReader.ReadUInt32();

                    //Read Samples
                    bReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    samples = bReader.ReadBytes((int)dataChunkSize + 44);

                    //Close all
                    inputFileStream.Close();
                }

                //Write Wav with new metadata
                using (FileStream outputFileStream = File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (BinaryWriter outputFileWriter = new BinaryWriter(outputFileStream))
                    {
                        //Header and Data Chunk
                        outputFileWriter.Write(samples);
                        //LIST Chunk
                        using (MemoryStream listChunkStream = new MemoryStream())
                        {
                            using (BinaryWriter binWriter = new BinaryWriter(listChunkStream))
                            {
                                //Header
                                binWriter.Write(Encoding.ASCII.GetBytes("LIST"));
                                binWriter.Write(0);
                                binWriter.Write(Encoding.ASCII.GetBytes("INFO"));

                                //Software
                                binWriter.Write(Encoding.ASCII.GetBytes("ISFT"));
                                Version euroSoundVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor);
                                byte[] software = Encoding.ASCII.GetBytes(string.Format("{0} {1}", Application.ProductName, euroSoundVersion));
                                binWriter.Write(software.Length + 1);
                                binWriter.Write(software);
                                binWriter.Write((byte)0);
                                AlignNumber(binWriter, 2);

                                //Engineer
                                binWriter.Write(Encoding.ASCII.GetBytes("IENG"));
                                byte[] engineer = Encoding.ASCII.GetBytes(GlobalPrefs.EuroSoundUser);
                                binWriter.Write(engineer.Length + 1);
                                binWriter.Write(engineer);
                                binWriter.Write((byte)0);
                                AlignNumber(binWriter, 2);

                                //Date
                                binWriter.Write(Encoding.ASCII.GetBytes("ICRD"));
                                byte[] creationDate = Encoding.ASCII.GetBytes(string.Format("{0:yyyy}-{0:MM}-{0:dd}", DateTime.Now));
                                binWriter.Write(creationDate.Length + 1);
                                binWriter.Write(creationDate);
                                binWriter.Write((byte)0);
                                AlignNumber(binWriter, 2);

                                //Write lenght
                                listChunkStream.Position = 4;
                                binWriter.Write(listChunkStream.Length - 8);

                                //Write to the output file
                                listChunkStream.WriteTo(outputFileStream);
                            }
                        }

                        //SMPL Chunk
                        using (MemoryStream smpChunkStream = new MemoryStream())
                        {
                            using (BinaryWriter binWriter = new BinaryWriter(smpChunkStream))
                            {
                                //Write SMPL Chunk
                                binWriter.Write(Encoding.ASCII.GetBytes("smpl"));
                                //size
                                binWriter.Write(0);
                                //manufacturer (MIDI Manufacturers Association manufacturer code)
                                binWriter.Write(0);
                                //product (product / model ID of the target device)
                                binWriter.Write(0);
                                //sample period
                                float samplePeriod = (1.0f / frequency) * 1000000000.0f;
                                binWriter.Write((uint)samplePeriod);
                                //MIDI unity note
                                binWriter.Write(60);
                                //MIDI pitch fraction
                                binWriter.Write(0);
                                //SMPTE format
                                binWriter.Write(0);
                                //SMPTE offset
                                binWriter.Write(0);
                                //number of sample loops
                                binWriter.Write(1);
                                //number sample data
                                binWriter.Write(0);
                                //------------------------------------Sample Loop Struct
                                //ID
                                binWriter.Write(0);
                                //Type: 0 means normal forward looping type. A value of 1 means alternating (forward and backward) looping type. A value of 2 means backward looping type
                                binWriter.Write(0);
                                //Start
                                binWriter.Write(startLoop);
                                //End
                                if (endLoop == -1)
                                {
                                    binWriter.Write(dataChunkSize / 2);
                                }
                                else
                                {
                                    binWriter.Write(endLoop);
                                }
                                //Fraction - A value of zero means current resolution. A value of 50 cents (0x80) means ½ sample
                                binWriter.Write(0);
                                //Number of times to play the loop
                                binWriter.Write(0);

                                //Write Chunk length
                                smpChunkStream.Position = 4;
                                binWriter.Write(smpChunkStream.Length - 8);

                                //Write to the output file
                                smpChunkStream.WriteTo(outputFileStream);
                            }
                        }

                        //Update new length
                        outputFileStream.Seek(4, SeekOrigin.Begin);
                        outputFileWriter.Write((int)outputFileStream.Length - 8);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void AlignNumber(BinaryWriter bw, uint blockSize)
        {
            uint PositionAligned = ((uint)bw.BaseStream.Position + (blockSize - 1)) & ~(blockSize - 1);
            while (bw.BaseStream.Position != PositionAligned)
            {
                bw.Write((byte)0x00);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
