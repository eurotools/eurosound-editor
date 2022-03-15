using System;
using System.IO;

namespace SonyVagCodec
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class PS2_VAG_Functions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private static readonly double[,] VAGLut = new double[,]
        {
            { 0.0, 0.0 },
            {  -60.0 / 64.0, 0.0 },
            { -115.0 / 64.0, 52.0 / 64.0 },
            {  -98.0 / 64.0, 55.0 / 64.0 },
            { -122.0 / 64.0, 60.0 / 64.0 }
        };

        //-------------------------------------------------------------------------------------------------------------------------------
        private struct VAGChunk
        {
            public sbyte shift;
            public sbyte predict; /* swy: reversed nibbles due to little-endian */
            public byte flags;
            public byte[] sample;
        };

        //-------------------------------------------------------------------------------------------------------------------------------
        private enum VAGFlag
        {
            VAGF_NOTHING = 0,         /* Nothing*/
            VAGF_LOOP_LAST_BLOCK = 1, /* Last block to loop */
            VAGF_LOOP_REGION = 2,     /* Loop region*/
            VAGF_LOOP_END = 3,        /* Ending block of the loop */
            VAGF_LOOP_FIRST_BLOCK = 4,/* First block of looped data */
            VAGF_UNK = 5,             /* ?*/
            VAGF_LOOP_START = 6,      /* Starting block of the loop*/
            VAGF_PLAYBACK_END = 7     /* Playback ending position */
        };

        //-------------------------------------------------------------------------------------------------------------------------------
        private static readonly int VAG_SAMPLE_BYTES = 14;
        private static readonly int VAG_SAMPLE_NIBBL = VAG_SAMPLE_BYTES * 2;

        //-------------------------------------------------------------------------------------------------------------------------------
        public static byte[] Encode(short[] pcmData, int loopOffset, bool loopFlag)
        {
            byte[] vagDat;
            double _hist_1 = 0.0, _hist_2 = 0.0;
            double hist_1 = 0.0, hist_2 = 0.0;
            double[] d_samples = new double[28];

            int fullChunks = pcmData.Length / VAG_SAMPLE_NIBBL;
            byte lastPredictAndShift = 0;

            using (MemoryStream VagFile = new MemoryStream())
            {
                using (BinaryWriter vagWriter = new BinaryWriter(VagFile))
                {
                    //Write Empty line
                    vagWriter.Write(new byte[16]);

                    //Start packing
                    for (int pos = 0, iteration = 0; pos < pcmData.Length; pos += VAG_SAMPLE_NIBBL, iteration++)
                    {
                        // [STEP 1] --- Get chunk
                        short[] wavBuf = new short[VAG_SAMPLE_NIBBL];
                        if (iteration < fullChunks)
                        {
                            Buffer.BlockCopy(pcmData, sizeof(short) * pos, wavBuf, 0, sizeof(short) * VAG_SAMPLE_NIBBL);
                        }
                        else
                        {
                            int remainingData = pcmData.Length - pos;
                            Buffer.BlockCopy(pcmData, sizeof(short) * pos, wavBuf, 0, sizeof(short) * remainingData);
                        }

                        //Initialize struct where we will store the data
                        VAGChunk VAGstruct = new VAGChunk
                        {
                            sample = new byte[VAG_SAMPLE_BYTES]
                        };

                        // [STEP 2] --- Find predict
                        int predict = 0, shift;
                        double min = 1e10;
                        double s_1 = 0.0, s_2 = 0.0;
                        double[,] predictBuf = new double[28, 5];

                        for (int j = 0; j < 5; j++)
                        {
                            double max = 0.0;

                            s_1 = _hist_1;
                            s_2 = _hist_2;

                            for (int k = 0; k < VAG_SAMPLE_NIBBL; k++)
                            {
                                double sample = wavBuf[k];
                                if (sample > 30719.0)
                                {
                                    sample = 30719.0;
                                }
                                if (sample < -30720.0)
                                {
                                    sample = -30720.0;
                                }

                                double ds = sample + s_1 * VAGLut[j, 0] + s_2 * VAGLut[j, 1];
                                predictBuf[k, j] = ds;
                                if (Math.Abs(ds) > max)
                                {
                                    max = Math.Abs(ds);
                                }

                                s_2 = s_1;
                                s_1 = sample;
                            }
                            if (max < min)
                            {
                                min = max;
                                predict = j;
                            }
                            if (min <= 7)
                            {
                                predict = 0;
                                break;
                            }
                        }

                        // store s[t-2] and s[t-1] in a static variable
                        // these than used in the next function call
                        _hist_1 = s_1;
                        _hist_2 = s_2;

                        for (int i = 0; i < 28; i++)
                        {
                            d_samples[i] = predictBuf[i, predict];
                        }

                        // [STEP 3] --- Find shift
                        int min2 = (int)min;
                        int shift_mask = 0x4000;
                        shift = 0;

                        while (shift < 12)
                        {
                            if (Convert.ToBoolean(shift_mask & (min2 + (shift_mask >> 3))))
                            {
                                break;
                            }
                            shift++;
                            shift_mask >>= 1;
                        }

                        //So shift==12 if none found...
                        VAGstruct.predict = (sbyte)predict;
                        VAGstruct.shift = (sbyte)shift;
                        //Flags
                        if (pcmData.Length - pos >= 28)
                        {
                            VAGstruct.flags = (byte)VAGFlag.VAGF_NOTHING;
                            if (loopFlag)
                            {
                                VAGstruct.flags = (byte)VAGFlag.VAGF_LOOP_REGION;
                                if (iteration == loopOffset)
                                {
                                    VAGstruct.flags = (byte)VAGFlag.VAGF_LOOP_START;
                                }
                            }
                        }
                        else
                        {
                            VAGstruct.flags = (byte)VAGFlag.VAGF_LOOP_LAST_BLOCK;
                            if (loopFlag)
                            {
                                VAGstruct.flags = (byte)VAGFlag.VAGF_LOOP_END;
                            }
                        }

                        // [STEP 4] --- Pack
                        short[] outBuf = new short[VAG_SAMPLE_NIBBL];
                        for (int k = 0; k < VAG_SAMPLE_NIBBL; k++)
                        {
                            double s_double_trans = d_samples[k] + hist_1 * VAGLut[predict, 0] + hist_2 * VAGLut[predict, 1];
                            double s_double = s_double_trans * (1 << shift);
                            int sample = (int)(((int)s_double + 0x800) & 0xFFFFF000);

                            if (sample > short.MaxValue)
                            {
                                sample = short.MaxValue;
                            }
                            if (sample < short.MinValue)
                            {
                                sample = short.MinValue;
                            }

                            outBuf[k] = (short)sample;

                            sample = sample >> shift;
                            hist_2 = hist_1;
                            hist_1 = sample - s_double_trans;
                        }

                        for (int k = 0; k < VAG_SAMPLE_BYTES; k++)
                        {
                            VAGstruct.sample[k] = (byte)(((outBuf[(k * 2) + 1] >> 8) & 0xf0) | ((outBuf[k * 2] >> 12) & 0xf));
                        }

                        //write chunck data
                        lastPredictAndShift = (byte)(((VAGstruct.predict << 4) & 0xF0) | (VAGstruct.shift & 0x0F));
                        vagWriter.Write(lastPredictAndShift);
                        vagWriter.Write(VAGstruct.flags);
                        vagWriter.Write(VAGstruct.sample);
                    }

                    // put terminating chunk
                    if (!loopFlag)
                    {
                        vagWriter.Write(lastPredictAndShift);
                        vagWriter.Write((byte)VAGFlag.VAGF_PLAYBACK_END);
                        vagWriter.Write(new byte[VAG_SAMPLE_BYTES]);
                    }
                    //Close
                    vagWriter.Close();
                }
                vagDat = VagFile.ToArray();
                VagFile.Close();
            }
            return vagDat;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
    }
}
