//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// IMA ADPCM CODEC
//-------------------------------------------------------------------------------------------------------------------------------
using System;

namespace ESUtils
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class ImaAdpcmState
    {
        public int valprev;
        public int index;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class ImaCodec
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private static readonly int[] StepSizeTable = {
            7,    8,    9,   10,   11,   12,   13,   14,   16,   17,
           19,   21,   23,   25,   28,   31,   34,   37,   41,   45,
           50,   55,   60,   66,   73,   80,   88,   97,  107,  118,
          130,  143,  157,  173,  190,  209,  230,  253,  279,  307,
          337,  371,  408,  449,  494,  544,  598,  658,  724,  796,
          876,  963, 1060, 1166, 1282, 1411, 1552, 1707, 1878, 2066,
         2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358,
         5894, 6484, 7132, 7845, 8630, 9493,10442,11487,12635,13899,
        15289,16818,18500,20350,22385,24623,27086,29794,32767 };

        //-------------------------------------------------------------------------------------------------------------------------------
        /* Intel ADPCM step variation table */
        private static readonly int[] IndexTable = { -1, -1, -1, -1, 2, 4, 6, 8, -1, -1, -1, -1, 2, 4, 6, 8 };

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint[] DecodeStatesIma(byte[] ImaFileData, int numSamples)
        {
            uint[] outBuff = new uint[numSamples];
            int inp;            /* Input buffer pointer */
            int outIndex = 0;   /* output buffer pointer */
            int sign;           /* Current adpcm sign bit */
            int delta;          /* Current adpcm output value */
            int step;           /* Stepsize */
            int valpred;        /* Predicted value */
            int vpdiff;         /* Current change to valpred */
            int index;          /* Current step change index */
            int inputbuffer;    /* place to keep next 4-bit value */
            bool bufferstep;    /* toggle between inputbuffer/input */
            uint EngineXState;   /* EngineX StateA and StateB field */

            ImaAdpcmState state = new ImaAdpcmState();
            inp = 0;
            inputbuffer = 0;

            valpred = state.valprev;
            index = state.index;
            step = StepSizeTable[index];

            bufferstep = false;

            for (; numSamples > 0; numSamples--)
            {
                /* Step 1 - get the delta value */
                if (bufferstep)
                {
                    delta = inputbuffer & 0xf;
                }
                else
                {
                    inputbuffer = ImaFileData[inp++];
                    delta = (inputbuffer >> 4) & 0xf;
                }
                bufferstep = !bufferstep;

                /* Step 2 - Find new index value (for later) */
                index += IndexTable[delta];
                if (index < 0) index = 0;
                if (index > 88) index = 88;

                /* Step 3 - Separate sign and magnitude */
                sign = delta & 8;
                delta = delta & 7;

                /* Step 4 - Compute difference and new predicted value */
                /*
                ** Computes 'vpdiff = (delta+0.5)*step/4', but see comment
                ** in adpcm_coder.
                */
                vpdiff = step >> 3;
                if ((delta & 4) != 0) vpdiff += step;
                if ((delta & 2) != 0) vpdiff += step >> 1;
                if ((delta & 1) != 0) vpdiff += step >> 2;

                if (sign != 0)
                    valpred -= vpdiff;
                else
                    valpred += vpdiff;

                /* Step 5 - clamp output value */
                if (valpred > short.MaxValue)
                    valpred = short.MaxValue;
                else if (valpred < short.MinValue)
                    valpred = short.MinValue;

                /* Step 6 - Update step value */
                step = StepSizeTable[index];

                /* Step 7 - Calculate States */
                byte bufferstepInt = Convert.ToByte(bufferstep);
                EngineXState = ((uint)(
                    (((short)valpred & 0xffff) << 0) | /* 0xPPPP....   swy: signed 16-bit predictor */
                    ((inputbuffer & 0xff) << 16) |     /* 0x....II..   swy: full byte that contains a staging buffer */
                    (((bufferstepInt & 0x1) << 7) |    /* 0x......OO   swy: boolean, top bit of the last byte */
                    ((index & 0x7f) << 0))             /* 0x......OO   swy: remaining seven bits of the last byte */
                    << 24));

                /* Step 8 - Output value */
                outBuff[outIndex++] = EngineXState;
            }
            state.valprev = valpred;
            state.index = index;

            return outBuff;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
