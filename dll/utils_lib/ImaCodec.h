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
#ifndef __ImaCodec__
#define __ImaCodec__


EUROSOUND_IMACODEC_START

//Types
typedef unsigned int uint;
typedef unsigned char u8;
typedef unsigned short u16;
typedef unsigned int u32;
typedef signed char s8;
typedef signed short s16;
typedef signed int s32;

//States struct
struct CodecState
{
	int valprev;
	int index;
};

//Functions declaration
DLL_EXPORT(void) DecodeStatesIma(CodecState* state, u8* input, int numSamples, u32* output);

EUROSOUND_IMACODEC_END

#endif //__ImaCodec__