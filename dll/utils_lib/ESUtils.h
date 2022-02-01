//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// UTILS 
//-------------------------------------------------------------------------------------------------------------------------------

#ifndef __ESUtils__
#define __ESUtils__

//Export DLL Macro
#define DLL_EXPORT(_returntype) extern "C" __declspec(dllexport) _returntype __cdecl

//Namespaces
#define EUROSOUND_FUNCTIONS_START namespace ESFunctions {
#define EUROSOUND_FUNCTIONS_END   }

#define EUROSOUND_IMACODEC_START namespace ESImaCodec {
#define EUROSOUND_IMACODEC_END   }

#define EUROSOUND_MUSX_START namespace ESMusX {
#define EUROSOUND_MUSX_END   }

//Types
typedef unsigned __int8  u8;
typedef unsigned __int16 u16;
typedef unsigned __int32 u32;
typedef unsigned __int64 u64;
typedef   signed __int8  s8;
typedef   signed __int16 s16;
typedef   signed __int32 s32;
typedef   signed __int64 s64;

#endif //__ESUtils__
