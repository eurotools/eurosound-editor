using NAudio.Wave;
using System;
using System.IO;

namespace XboxAdpcm
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
                    Console.WriteLine("Usage: <Encode> <InputFile> <OutputFile>");
                }
                else
                {
                    //Get input params
                    if (args.Length > 2)
                    {
                        string modeApp = args[0].ToLower(), inputFile = args[1].ToLower(), outputFile = args[2].ToLower();

                        //Read wave file
                        if (File.Exists(inputFile) && modeApp.Equals("Encode", StringComparison.OrdinalIgnoreCase))
                        {
                            //Read data
                            WaveFileReader waveReader = new WaveFileReader(inputFile);
                            byte[] byteData = new byte[waveReader.Length];
                            waveReader.Read(byteData, 0, byteData.Length);

                            //Convert byte array to short array
                            short[] pcmData = ConvertByteArrayToShortArray(byteData);

                            //Start encoding
                            byte[] encodedData = XboxAdpcm.Encode(pcmData, pcmData.Length);

                            //Write encoded data
                            File.WriteAllBytes(outputFile, encodedData);
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
