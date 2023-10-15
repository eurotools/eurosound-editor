using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PCAudioDLL.Codecs
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SonyAdpcm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private static readonly double[,] VAGLut = new double[,]
        {
            {0.0, 0.0},
            {60.0 / 64.0, 0.0},
            {115.0 / 64.0, -52.0 / 64.0},
            {98.0 / 64.0, -55.0 / 64.0},
            {122.0 / 64.0, -60.0 / 64.0}
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
        private readonly int VAG_SAMPLE_NIBBL = VAG_SAMPLE_BYTES * 2;

        //-------------------------------------------------------------------------------------------------------------------------------
        public byte[] Decode(byte[] vagData, ref int loopOffset)
        {
            byte[] pcmData;

            using (BinaryReader VagReader = new BinaryReader(new MemoryStream(vagData, false)))
            using (MemoryStream PCMStream = new MemoryStream())
            using (BinaryWriter PCMWriter = new BinaryWriter(PCMStream))
            {
                double hist_1 = 0.0, hist_2 = 0.0;

                //Skip header
                VagReader.BaseStream.Seek(16, SeekOrigin.Begin);

                //Start decoding
                while (VagReader.BaseStream.Position < VagReader.BaseStream.Length)
                {
                    //Read chunk data
                    byte DecodingCoefficent = VagReader.ReadByte();
                    VAGChunk vc = new VAGChunk
                    {
                        shift = (sbyte)(DecodingCoefficent & 0xF),
                        predict = (sbyte)((DecodingCoefficent & 0xF0) >> 4),
                        flags = VagReader.ReadByte(),
                        sample = VagReader.ReadBytes(14)
                    };

                    if (vc.flags == (byte)VAGFlag.VAGF_PLAYBACK_END)
                    {
                        break;
                    }
                    else if (vc.flags == (byte)VAGFlag.VAGF_LOOP_START)
                    {
                        loopOffset = (int)PCMStream.Length;
                    }
                    else
                    {
                        int[] samples = new int[VAG_SAMPLE_NIBBL];

                        // expand 4bit -> 8bit
                        for (int j = 0; j < VAG_SAMPLE_BYTES; j++)
                        {
                            samples[j * 2] = vc.sample[j] & 0xF;
                            samples[j * 2 + 1] = (vc.sample[j] & 0xF0) >> 4;
                        }

                        //Decode samples
                        for (int j = 0; j < VAG_SAMPLE_NIBBL; j++)
                        {
                            // shift 4 bits to top range of int16_t
                            int s = samples[j] << 12;
                            if ((s & 0x8000) != 0)
                            {
                                s = (int)(s | 0xFFFF0000);
                            }

                            /* swy: don't overflow the LUT array access; limit the max allowed index */
                            sbyte predict = Math.Min(vc.predict, (sbyte)(VAGLut.GetLength(0) - 1));

                            double sample = (s >> vc.shift) + hist_1 * VAGLut[predict, 0] + hist_2 * VAGLut[predict, 1];
                            hist_2 = hist_1;
                            hist_1 = sample;

                            PCMWriter.Write((short)(Math.Min(short.MaxValue, Math.Max(sample, short.MinValue))));
                        }
                    }
                }
                pcmData = PCMStream.ToArray();

                PCMWriter.Close();
                PCMStream.Close();
                VagReader.Close();
            }

            return pcmData;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
