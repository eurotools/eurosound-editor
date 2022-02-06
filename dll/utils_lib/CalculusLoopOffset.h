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

#ifndef __CalculusLoopOffset__
#define __CalculusLoopOffset__

EUROSOUND_FUNCTIONS_START

//Functions Declarations
DLL_EXPORT(u32) GetMusicLoopOffsetPCandGC(u32 baseLoopOffset);
DLL_EXPORT(u32) GetMusicLoopOffsetPlayStation2(u32 baseLoopOffset);
DLL_EXPORT(u32) GetMusicLoopOffsetXbox(u32 loopOffset);
DLL_EXPORT(u32) GetStreamLoopOffsetPlayStation2(u32 baseLoopOffset);
DLL_EXPORT(u32) GetStreamLoopOffsetXbox(u32 baseLoopOffset);
DLL_EXPORT(u32) GetStreamLoopOffsetPCandGC(u32 markerPosition);
DLL_EXPORT(double) roundNum(double value);

//-------------------------------------------------------------------------------------------------------------------------------
EUROSOUND_FUNCTIONS_END

#endif //__CalculusLoopOffset__