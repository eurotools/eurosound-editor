using NAudio.Wave;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DspAdpcmTool
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class Program
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal struct ADPCMINFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            internal short[] coef;
            internal ushort gain;
            internal ushort pred_scale;
            internal short yn1;
            internal short yn2;

            internal ushort loop_pred_scale;
            internal short loop_yn1;
            internal short loop_yn2;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        [DllImport("dsptool.dll")]
        private static extern void encode(short[] src, byte[] dst, ref ADPCMINFO cxt, uint samples);
        [DllImport("dsptool.dll")]
        private static extern uint getBytesForAdpcmBuffer(uint samples);
        [DllImport("dsptool.dll")]
        private static extern void getLoopContext(short[] src, ref ADPCMINFO cxt, uint samples);
        [DllImport("dsptool.dll")]
        private static extern uint getNibbleAddress(uint samples);
        [DllImport("dsptool.dll")]
        private static extern uint getBytesForAdpcmInfo(uint samples);

        //-------------------------------------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            //Ensure that we have arguments
            if (args.Length > 0)
            {
                //Usage
                if (args[0].ToLower().Contains("help") || args[0].Contains("?"))
                {
                    Console.WriteLine("Usage: <Encode> <InputFile> <OutputFile> <-L> ");
                }
                else
                {
                    //Get input params
                    if (args.Length > 2)
                    {
                        bool Looped = false;
                        uint samplesToLoopStart = 0;
                        string modeApp = args[0].ToLower(), inputFile = args[1].ToLower(), outputFile = args[2].ToLower();
                        if (args.Length > 3 && args[3].Equals("-L", StringComparison.OrdinalIgnoreCase))
                        {
                            Looped = true;
                            samplesToLoopStart = Convert.ToUInt32(args[4]);
                        }

                        //Read wave file
                        if (File.Exists(inputFile) && modeApp.Equals("Encode", StringComparison.OrdinalIgnoreCase))
                        {
                            //Read data
                            WaveFileReader waveReader = new WaveFileReader(inputFile);
                            byte[] byteData = new byte[waveReader.Length];
                            waveReader.Read(byteData, 0, byteData.Length);

                            //Convert byte array to short array
                            short[] pcmData = ConvertByteArrayToShortArray(byteData);
                            uint samplesToEncode = (uint)pcmData.Length;

                            //... put some PCM buffer in memory, reverse the endian if you have to
                            uint nibblesCount = getBytesForAdpcmBuffer(samplesToEncode);
                            byte[] encodedData = new byte[nibblesCount];

                            if (encodedData.Length > 0)
                            {
                                //ok.. lets encode it!
                                ADPCMINFO dspData = new ADPCMINFO();
                                encode(pcmData, encodedData, ref dspData, samplesToEncode);

                                // get ADPCM loop context if sample is looped
                                if (Looped)
                                {
                                    //Get loop info
                                    getLoopContext(pcmData, ref dspData, samplesToLoopStart);
                                }

                                //store ADPCM context to file
                                uint nibbleStartOffset = getNibbleAddress(0);
                                uint nibbleLoopStartOffset = getNibbleAddress(samplesToLoopStart);
                                uint nibbleEndAddress = getNibbleAddress(samplesToEncode);

                                //Write encoded data
                                File.WriteAllBytes(outputFile, encodedData);

                                //Write text file
                                WriteTextFile(outputFile, inputFile, Looped, (uint)waveReader.WaveFormat.SampleRate, samplesToEncode, dspData, nibbleStartOffset, nibbleLoopStartOffset, nibbleEndAddress);

                                //Write binary file
                                WriteBinaryFile(outputFile, Looped, (uint)waveReader.WaveFormat.SampleRate, samplesToEncode, dspData, nibbleStartOffset, nibbleLoopStartOffset, nibbleEndAddress);
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static void WriteTextFile(string outputFile, string inputFile, bool looped, uint sampleRate, uint samplesToEncode, ADPCMINFO dspData, uint nibbleStartOffset, uint nibbleLoopStartOffset, uint nibbleEndAddress)
        {
            //Text file
            string textOutFile = Path.Combine(Path.GetDirectoryName(outputFile), Path.GetFileNameWithoutExtension(outputFile) + ".txt");
            using (StreamWriter writer = new StreamWriter(textOutFile))
            {
                writer.WriteLine("");
                writer.WriteLine(string.Format("Header size: {0} bytes", 96));
                writer.WriteLine("");
                writer.WriteLine(string.Format("Sample     : '{0}'", inputFile));
                writer.WriteLine(string.Format("Length     : {0} samples", samplesToEncode));
                writer.WriteLine(string.Format("Num nibbles: {0} ADPCM nibbles", nibbleEndAddress));
                writer.WriteLine(string.Format("Sample Rate: {0} Hz", sampleRate));
                writer.WriteLine(string.Format("Loop Flag  : {0}", looped ? "LOOPED" : "NOT LOOPED"));
                writer.WriteLine("");
                writer.WriteLine(string.Format("Start Addr : 0x{0} + ARAM_offset (ADPCM nibble mode)", nibbleLoopStartOffset.ToString("X8")));
                writer.WriteLine(string.Format("End Addr   : 0x{0} + ARAM_offset (ADPCM nibble mode)", nibbleEndAddress.ToString("X8")));
                writer.WriteLine(string.Format("Curr Addr  : 0x{0} + ARAM_offset (ADPCM nibble mode)", nibbleStartOffset.ToString("X8")));
                writer.WriteLine("");
                for (int i = 0, index = 0; i < dspData.coef.Length; i += 2, index++)
                {
                    writer.WriteLine(string.Format("a1[{0}]: 0x{1} a2[{0}]: 0x{2} ", index, dspData.coef[i].ToString("X4"), dspData.coef[i + 1].ToString("X4")));
                }
                writer.WriteLine("");
                writer.WriteLine(string.Format("Gain      : 0x{0}", dspData.gain.ToString("X4")));
                writer.WriteLine(string.Format("Pred/Scale: 0x{0}", dspData.pred_scale.ToString("X4")));
                writer.WriteLine(string.Format("y[n-1]    : 0x{0}", dspData.yn1.ToString("X4")));
                writer.WriteLine(string.Format("y[n-2]    : 0x{0}", dspData.yn2.ToString("X4")));
                writer.WriteLine("");
                writer.WriteLine(string.Format("Loop Pred/Scale: 0x{0}", dspData.loop_pred_scale.ToString("X4")));
                writer.WriteLine(string.Format("Loop y[n-1]    : 0x{0}", dspData.loop_yn1.ToString("X4")));
                writer.WriteLine(string.Format("Loop y[n-2]    : 0x{0}", dspData.loop_yn2.ToString("X4")));
                writer.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static void WriteBinaryFile(string outputFile, bool looped, uint sampleRate, uint samplesToEncode, ADPCMINFO dspData, uint nibbleStartOffset, uint nibbleLoopStartOffset, uint nibbleEndAddress)
        {
            string textOutFile = Path.Combine(Path.GetDirectoryName(outputFile), Path.GetFileNameWithoutExtension(outputFile) + ".dsph");
            using (BinaryWriter binWrite = new BinaryWriter(File.Open(textOutFile, FileMode.Create, FileAccess.Write, FileShare.Read), System.Text.Encoding.ASCII))
            {
                binWrite.Write(FlipUInt32(samplesToEncode, true));
                binWrite.Write(FlipUInt32(nibbleEndAddress, true));
                binWrite.Write(FlipUInt32(sampleRate, true));
                binWrite.Write(FlipUShort(Convert.ToUInt16(looped), true));
                binWrite.Write((ushort)0);
                binWrite.Write(FlipUInt32(nibbleLoopStartOffset, true));
                binWrite.Write(FlipUInt32(nibbleEndAddress, true));
                binWrite.Write(FlipUInt32(nibbleStartOffset, true));
                for (int i = 0; i < dspData.coef.Length; i++)
                {
                    binWrite.Write(FlipUShort((ushort)dspData.coef[i], true));
                }
                binWrite.Write(FlipUShort(dspData.gain, true));
                binWrite.Write(FlipUShort(dspData.pred_scale, true));
                binWrite.Write(FlipShort(dspData.yn1, true));
                binWrite.Write(FlipShort(dspData.yn2, true));
                binWrite.Write(FlipUShort(dspData.loop_pred_scale, true));
                binWrite.Write(FlipShort(dspData.loop_yn1, true));
                binWrite.Write(FlipShort(dspData.loop_yn2, true));
                for (int i = 0; i < 10; i++)
                {
                    binWrite.Write((ushort)0);
                }
                binWrite.Write((ushort)0);
                binWrite.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
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

        //-------------------------------------------------------------------------------------------------------------------------------
        private static uint FlipUInt32(uint valueToFlip, bool isBigEndian)
        {
            uint finalData;
            if (isBigEndian)
            {
                finalData = (valueToFlip & 0xFF000000) >> (8 * 3) | /* 0x11______ -> 0x______11 */
                            (valueToFlip & 0x00FF0000) >> (8 * 1) | /* 0x__22____ -> 0x____22__ */
                            (valueToFlip & 0x0000FF00) << (8 * 1) | /* 0x____33__ -> 0x__33____ */
                            (valueToFlip & 0x000000FF) << (8 * 3);  /* 0x______44 -> 0x44______ */
            }
            else
            {
                finalData = valueToFlip;
            }
            return finalData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static ushort FlipUShort(ushort valueToFlip, bool isBigEndian)
        {
            ushort finalData;

            if (isBigEndian)
            {
                //Flip input value
                finalData = (ushort)((valueToFlip & 0xFF00) >> (8 * 1) | /* 0x11__ -> 0x__11 */
                                     (valueToFlip & 0x00FF) << (8 * 1)); /* 0x__22 -> 0x22__ */
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static short FlipShort(short valueToFlip, bool isBigEndian)
        {
            short finalData;
            if (isBigEndian)
            {
                finalData = (short)((valueToFlip & 0xFF00) >> (8 * 1) | /* 0x11__ -> 0x__11 */
                                    (valueToFlip & 0x00FF) << (8 * 1)); /* 0x__22 -> 0x22__ */
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
