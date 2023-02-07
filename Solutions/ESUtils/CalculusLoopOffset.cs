//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// CALCULUS LOOP OFFSETS (MARKER FILES)
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using static ESUtils.BytesFunctions;

namespace ESUtils
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class CalculusLoopOffset
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetMusicLoopOffsetPCandGC(uint baseLoopOffset)
        {
            // 1.378125 comes from 44100/32000
            double ruleOfThree = baseLoopOffset / 1.378125;
            return (uint)RoundNumber(ruleOfThree) * 4;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetMusicLoopOffsetPlayStation2(uint baseLoopOffset)
        {
            uint pcResult = GetMusicLoopOffsetPCandGC(baseLoopOffset);
            return (uint)(pcResult / 3.5);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetMusicLoopOffsetXbox(uint loopOffset)
        {
            double division = loopOffset / 0.875;
            return (uint)Math.Floor(division);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetStreamLoopOffsetPlayStation2(uint baseLoopOffset)
        {
            uint PositionAligned = GetStreamLoopOffsetPCandGC(baseLoopOffset);
            return (uint)(PositionAligned / 3.5);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetStreamLoopOffsetXbox(uint baseLoopOffset)
        {
            uint PositionAligned = GetStreamLoopOffsetPCandGC(baseLoopOffset);
            uint parsedLoopOffset = PositionAligned / 2;
            return (uint)(parsedLoopOffset / 1.7777777777);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetStreamLoopOffsetPCandGC(uint markerPosition)
        {
            uint PositionAligned;

            //Align position
            if ((markerPosition & 1) == 1) //Odd number
            {
                PositionAligned = ((markerPosition + 1) / 4 * 4);
            }
            else //Even number
            {
                PositionAligned = AlignNumber(markerPosition, 4);
            }
            return PositionAligned;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static double RoundNumber(double value)
        {
            return value < 0 ? -Math.Floor(0.5 - value) : Math.Floor(0.5 + value);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static decimal RuleOfThreeLoopOffset(int masterFreq, int ConvertedFreq, int masterLoopOffset)
        {
            decimal compressFactor = decimal.Divide(masterFreq, ConvertedFreq);
            return Math.Round(decimal.Divide(masterLoopOffset, compressFactor));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetXboxAlignedNumber(uint inputValue)
        {
            uint alignedNumber = 0;
            if (inputValue > 31)
            {
                alignedNumber = (((inputValue - 32) / 64) + 1) * 36;
            }
            return alignedNumber;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetEurocomImaLoopOffset(uint baseLoopOffset)
        {
            double result = RoundNumber((double)decimal.Divide(baseLoopOffset, (decimal)3.4568));
            uint PositionAligned = GetStreamLoopOffsetPCandGC((uint)result);
            return (((PositionAligned - 28) / 32) + 1) * 32;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
