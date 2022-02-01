//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// STRUCTS FOR SFX FILES
//-------------------------------------------------------------------------------------------------------------------------------
#ifndef __MusXFunctions__
#define __MusXFunctions__

EUROSOUND_MUSX_START

struct MusXHeader
{
	char Magic[4];
	u32  Hashcode;
	u32  ConstantOffset;
	u32  FileSize;
};

struct MusicBanksHeader
{
	u32 FileStart1;
	u32 FileStart1Length;
	u32 FileStart2;
	u32 FileStart2Length;
	u32 FileStart3;
	u32 FileStart3Length;
};

//Functions to export
DLL_EXPORT(void) BuildMusicFile(char* soundMarkerFile, char* soundSampleData, char* filePath, u32 hashcode, bool bigEndian);
DLL_EXPORT(void) BuildStreamFile(char* binFilePath, char* lutFilePath, char* outputFilePath, bool bigEndian);

EUROSOUND_MUSX_END

#endif //__MusXFunctions__