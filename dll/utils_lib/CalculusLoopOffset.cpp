#include "ESUtils.h"
#include "BytesFunctions.h"
#include "CalculusLoopOffset.h"
#include <math.h>  

EUROSOUND_FUNCTIONS_START
//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
extern "C"
{
	//-------------------------------------------------------------------------------------------------------------------------------
	u32 GetMusicLoopOffsetPCandGC(u32 baseLoopOffset)
	{
		// 1.378125 comes from 44100/32000
		double ruleOfThree = baseLoopOffset / 1.378125;
		u32 multiplyRounded = (u32)round(ruleOfThree) * 4;
		return multiplyRounded;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	u32 GetMusicLoopOffsetPlayStation2(u32 baseLoopOffset)
	{
		u32 pcResult = GetMusicLoopOffsetPCandGC(baseLoopOffset);
		u32 ps2Value = (u32)(pcResult / 3.5);
		return ps2Value;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	u32 GetMusicLoopOffsetXbox(u32 loopOffset)
	{
		double division = loopOffset / 0.88888887;
		u32 roundedDivision = (u32)floor(division);
		return roundedDivision;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	u32 GetStreamLoopOffsetPlayStation2(u32 baseLoopOffset)
	{
		u32 PositionAligned = GetStreamLoopOffsetPCandGC(baseLoopOffset);
		u32 division = (u32)(PositionAligned / 3.5);
		return division;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	u32 GetStreamLoopOffsetXbox(u32 baseLoopOffset)
	{
		u32 PositionAligned = GetStreamLoopOffsetPCandGC(baseLoopOffset);
		u32 parsedLoopOffset = PositionAligned / 2;
		u32 division = (u32)(parsedLoopOffset / 1.7777777777);
		return division;
	}

	//-------------------------------------------------------------------------------------------------------------------------------
	u32 GetStreamLoopOffsetPCandGC(u32 markerPosition)
	{
		u32 PositionAligned;

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
}

//-------------------------------------------------------------------------------------------------------------------------------
EUROSOUND_FUNCTIONS_END