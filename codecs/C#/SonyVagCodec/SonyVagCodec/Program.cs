using NAudio.Wave;
using System;
using System.IO;

namespace SonyVagCodec
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ensure that we have arguments
            if (args.Length > 0)
            {
                //Usage
                if (args[0].ToLower().Contains("help") || args[0].Contains("?"))
                {
                    Console.WriteLine("Usage: <InputFile> <OutputFile> <LoopOffset> ");
                }
                else
                {
                    string inputFile = string.Empty, outputFile = string.Empty;
                    int loopOffset = -1;
                    if (args.Length > 1)
                    {
                        inputFile = args[0];
                        outputFile = args[1];
                    }
                    if (args.Length > 2)
                    {
                        loopOffset = Convert.ToInt32(args[2]);
                    }

                    if (File.Exists(inputFile))
                    {
                        string fileExtension = Path.GetExtension(inputFile);
                        if (fileExtension.Equals(".aif", StringComparison.OrdinalIgnoreCase))
                        {
                            using (AiffFileReader reader = new AiffFileReader(inputFile))
                            {
                                //Get pcm short array
                                byte[] pcmByteData = new byte[reader.Length];
                                reader.Read(pcmByteData, 0, pcmByteData.Length);
                                short[] pcmData = ConvertByteArrayToShortArray(pcmByteData);

                                //Start encoding!
                                byte[] vagData = PS2_VAG_Functions.Encode(pcmData, loopOffset, loopOffset > -1);
                                File.WriteAllBytes(outputFile, vagData);
                            }
                        }
                        else if (fileExtension.Equals(".wav", StringComparison.OrdinalIgnoreCase))
                        {
                            using (WaveFileReader reader = new WaveFileReader(inputFile))
                            {
                                //Get pcm short array
                                byte[] pcmByteData = new byte[reader.Length];
                                reader.Read(pcmByteData, 0, pcmByteData.Length);
                                short[] pcmData = ConvertByteArrayToShortArray(pcmByteData);

                                //Start encoding!
                                byte[] vagData = PS2_VAG_Functions.Encode(pcmData, loopOffset, loopOffset > -1);
                                File.WriteAllBytes(outputFile, vagData);
                            }
                        }
                    }
                }
            }
        }

        private static short[] ConvertByteArrayToShortArray(byte[] PCMData)
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
}
