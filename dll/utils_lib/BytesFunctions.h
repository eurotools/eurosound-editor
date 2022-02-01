//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// BYTES FUNCTIONS
//-------------------------------------------------------------------------------------------------------------------------------

#ifndef __BytesFunctions__
#define __BytesFunctions__

EUROSOUND_FUNCTIONS_START

//Functions declarations
DLL_EXPORT(char*) FormatBytes(long bytes);
DLL_EXPORT(u32) FlipUInt32(u32 valueToFlip, bool isBigEndian);
DLL_EXPORT(s32) FlipInt32(s32 valueToFlip, bool isBigEndian);
DLL_EXPORT(s16) FlipShort(s16 valueToFlip, bool isBigEndian);
DLL_EXPORT(u16) FlipUShort(u16 valueToFlip, bool isBigEndian);
DLL_EXPORT(u32) AlignNumber(u32 valueToAlign, u32 blockSize);

EUROSOUND_FUNCTIONS_END

#endif //__BytesFunctions__
