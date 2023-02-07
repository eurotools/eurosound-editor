﻿using ESUtils;
using NUnit.Framework;
using System;
using static ESUtils.CalculusLoopOffset;

namespace LoopOffsetUnitTests
{
    [TestFixture]
    public class CalculusLoopOffsetTest
    {
        //Aby_Council_Int1
        [TestCase((uint)76554, ExpectedResult = 222196)]
        [TestCase((uint)110589, ExpectedResult = 320984)]
        [TestCase((uint)2095108, ExpectedResult = 6081040)]
        [TestCase((uint)2182957, ExpectedResult = 6336020)]
        //Ambient1
        [TestCase((uint)83290, ExpectedResult = 241748)]
        [TestCase((uint)394496, ExpectedResult = 1145024)]
        [TestCase((uint)1124374, ExpectedResult = 3263488)]
        [TestCase((uint)2035748, ExpectedResult = 5908748)]
        [TestCase((uint)3295246, ExpectedResult = 9564432)]
        [TestCase((uint)3657639, ExpectedResult = 10616276)]
        [TestCase((uint)3697809, ExpectedResult = 10732868)]
        //Random Tests
        [TestCase((uint)56174, ExpectedResult = 163044)]
        [TestCase((uint)14, ExpectedResult = 40)]
        [TestCase((uint)24, ExpectedResult = 68)]
        [TestCase((uint)30, ExpectedResult = 88)]
        [TestCase((uint)44444, ExpectedResult = 129000)]
        [TestCase((uint)777777, ExpectedResult = 2257492)]
        [TestCase((uint)2095108, ExpectedResult = 6081040)]
        public uint Test_GetMusicLoopOffsetPCandGC(uint input)
        {
            uint result = GetMusicLoopOffsetPCandGC(input);
            return result;
        }

        //Aby_Council_Int1
        [TestCase((uint)76554, ExpectedResult = 63484)]
        [TestCase((uint)110589, ExpectedResult = 91709)]
        [TestCase((uint)2095108, ExpectedResult = 1737440)]
        [TestCase((uint)2182957, ExpectedResult = 1810291)]
        //Ambient1
        [TestCase((uint)83290, ExpectedResult = 69070)]
        [TestCase((uint)394496, ExpectedResult = 327149)]
        [TestCase((uint)1124374, ExpectedResult = 932425)]
        [TestCase((uint)2035748, ExpectedResult = 1688213)]
        [TestCase((uint)3295246, ExpectedResult = 2732694)]
        [TestCase((uint)3657639, ExpectedResult = 3033221)]
        [TestCase((uint)3697809, ExpectedResult = 3066533)]
        //Random Tests
        [TestCase((uint)56174, ExpectedResult = 46584)]
        [TestCase((uint)14, ExpectedResult = 11)]
        [TestCase((uint)24, ExpectedResult = 19)]
        [TestCase((uint)30, ExpectedResult = 25)]
        [TestCase((uint)44444, ExpectedResult = 36857)]
        [TestCase((uint)777777, ExpectedResult = 644997)]
        [TestCase((uint)2095108, ExpectedResult = 1737440)]
        public uint Test_GetMusicLoopOffsetPlayStation2(uint input)
        {
            uint result = GetMusicLoopOffsetPlayStation2(input);
            return result;
        }

        //Aby_Council_Int1
        [TestCase((uint)76554, ExpectedResult = 86123)]
        [TestCase((uint)110589, ExpectedResult = 124412)]
        [TestCase((uint)2095108, ExpectedResult = 2356996)]
        [TestCase((uint)2182957, ExpectedResult = 2455826)]
        //Ambient1
        [TestCase((uint)83290, ExpectedResult = 93701)]
        [TestCase((uint)394496, ExpectedResult = 443808)]
        [TestCase((uint)1124374, ExpectedResult = 1264920)]
        [TestCase((uint)2035748, ExpectedResult = 2290216)]
        [TestCase((uint)3295246, ExpectedResult = 3707151)]
        [TestCase((uint)3657639, ExpectedResult = 4114843)]
        [TestCase((uint)3697809, ExpectedResult = 4160035)]
        //Random Tests
        [TestCase((uint)56174, ExpectedResult = 63195)]
        [TestCase((uint)14, ExpectedResult = 15)]
        [TestCase((uint)24, ExpectedResult = 27)]
        [TestCase((uint)30, ExpectedResult = 33)]
        [TestCase((uint)44444, ExpectedResult = 49999)]
        [TestCase((uint)777777, ExpectedResult = 874999)]
        [TestCase((uint)2095108, ExpectedResult = 2356996)]
        public uint Test_GetMusicLoopOffsetXbox(uint input)
        {
            uint result = GetMusicLoopOffsetXbox(input);
            return result;
        }

        [TestCase((uint)380928, ExpectedResult = 108836)]
        [TestCase((uint)242882, ExpectedResult = 69395)]
        [TestCase((uint)262183, ExpectedResult = 74909)]
        [TestCase((uint)501759, ExpectedResult = 143360)]
        [TestCase((uint)923161, ExpectedResult = 263760)]
        public uint Test_GetStreamLoopOffsetPlayStation2(uint input)
        {
            uint result = GetStreamLoopOffsetPlayStation2(input);
            return result;
        }

        [TestCase((uint)380928, ExpectedResult = 107136)]
        [TestCase((uint)242882, ExpectedResult = 68311)]
        [TestCase((uint)262183, ExpectedResult = 73739)]
        [TestCase((uint)501759, ExpectedResult = 141120)]
        [TestCase((uint)923161, ExpectedResult = 259638)]
        public uint Test_GetStreamLoopOffsetXbox(uint input)
        {
            uint result = GetStreamLoopOffsetXbox(input);
            return result;
        }

        [TestCase((uint)7624, ExpectedResult = 2208)]
        [TestCase((uint)444, ExpectedResult = 128)]
        [TestCase((uint)3102, ExpectedResult = 896)]
        [TestCase((uint)3094, ExpectedResult = 896)]
        [TestCase((uint)7305, ExpectedResult = 2112)]
        [TestCase((uint)3209, ExpectedResult = 928)]
        [TestCase((uint)9657, ExpectedResult = 2784)]
        [TestCase((uint)9748, ExpectedResult = 2816)]
        [TestCase((uint)6763, ExpectedResult = 1952)]
        [TestCase((uint)12445, ExpectedResult = 3584)]
        [TestCase((uint)5291, ExpectedResult = 1536)]
        [TestCase((uint)12301, ExpectedResult = 3520)]
        public uint Test_GetEurocomImaLoopOffset(uint baseLoopOffset)
        {
            double result = RoundNumber((double)decimal.Divide(baseLoopOffset, (decimal)3.4568));
            uint PositionAligned = GetStreamLoopOffsetPCandGC((uint)result);
            return (((PositionAligned - 28) / 32) + 1) * 32;
        }
    }
}
