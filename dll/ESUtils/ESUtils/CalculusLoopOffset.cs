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
            uint multiplyRounded = (uint)RoundNumber(ruleOfThree) * 4;
            return multiplyRounded;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetMusicLoopOffsetPlayStation2(uint baseLoopOffset)
        {
            uint pcResult = GetMusicLoopOffsetPCandGC(baseLoopOffset);
            uint ps2Value = (uint)(pcResult / 3.5);
            return ps2Value;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetMusicLoopOffsetXbox(uint loopOffset)
        {
            double division = loopOffset / 0.88888887;
            uint roundedDivision = (uint)Math.Floor(division);
            return roundedDivision;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetStreamLoopOffsetPlayStation2(uint baseLoopOffset)
        {
            uint PositionAligned = GetStreamLoopOffsetPCandGC(baseLoopOffset);
            uint division = (uint)(PositionAligned / 3.5);
            return division;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static uint GetStreamLoopOffsetXbox(uint baseLoopOffset)
        {
            uint PositionAligned = GetStreamLoopOffsetPCandGC(baseLoopOffset);
            uint parsedLoopOffset = PositionAligned / 2;
            uint division = (uint)(parsedLoopOffset / 1.7777777777);
            return division;
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
        public static int RuleOfThreeLoopOffset(int masterFreq, int ConvertedFreq, int masterLoopOffset)
        {
            decimal compressFactor = decimal.Divide(masterFreq, ConvertedFreq);
            decimal loopOffset = decimal.Divide(masterLoopOffset, compressFactor);
            return (int)loopOffset;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
