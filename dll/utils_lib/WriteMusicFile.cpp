#include "MusX.h"
#include "MusXFilesFunctions.h"
#include <fstream>
#include <iostream>

//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
extern "C"
{
	using namespace std;

	void BuildMusicFile(char* soundMarkerFile, char* soundSampleData, char* filePath, unsigned int hashCode, bool bigEndian)
	{
		//Create a new binary writer
		ofstream file("myFirstBinaryFile.SFX", ios::binary);

		//--------------------------------------------------[Header]--------------------------------------------------
		const char* magic = "MUSX";
		MusXHeader testHeader;
		//--magic[magic value]--
		strcpy(testHeader.Magic, magic);
		//--hashc[Hashcode for the current soundbank without the section prefix]--
		testHeader.Hashcode = 2 | 0xE00000;
		//--offst[Constant offset to the next section,]--
		testHeader.ConstantOffset = 0xC9;
		//--fulls[Size of the whole file, in bytes. Unused. ]--
		testHeader.FileSize = 0;

		//--------------------------------------------------[File Sections]--------------------------------------------------
		//--File start 1; an offset that points to the stream look-up file details. Set to 0x800 in the original software. --
		MusicBanksHeader musicFileSections;
		musicFileSections.FileStart1 = 0;
		//--File length 1; size of the first section, in bytes. --
		musicFileSections.FileStart1Length = 0;
		//--File start 2; offset to the second section with the sample data. Set to 0x1000 in the original software. --
		musicFileSections.FileStart2 = 0;
		//--File length 2; size of the second section, in bytes. --
		musicFileSections.FileStart2Length = 0;
		//--File start 3; unused offset. Set to zero.--
		musicFileSections.FileStart3 = 0;
		//--File length 3; unused. Set to zero.--
		musicFileSections.FileStart3Length = 0;

		//--------------------------------------------------[Files Content]--------------------------------------------------
		//Sound Marker File
		FILE* markersFile = fopen("MFX_0.smf", "rb");
		fseek(markersFile, 0, SEEK_END);
		long markersFileSize = ftell(markersFile);
		rewind(markersFile);
		char* markersFileData = ReturnFileArray(markersFile, markersFileSize);

		FILE* soundSampleData = fopen("MFX_0.ssd", "rb");
		fseek(soundSampleData, 0, SEEK_END);
		long soundSampleDataSize = ftell(soundSampleData);
		rewind(soundSampleData);
		char* soundSampleDataData = ReturnFileArray(soundSampleData, soundSampleDataSize);

		//--------------------------------------------------[Final offsets]--------------------------------------------------
		file.write((char*)&testHeader, sizeof(testHeader));
		file.write((char*)&musicFileSections, sizeof(musicFileSections));
		//------------------------------
		musicFileSections.FileStart1 = AlignNumber(file.tellp(), 0x800);
		musicFileSections.FileStart1Length = markersFileSize;
		file.seekp(musicFileSections.FileStart1);
		file.write(markersFileData, markersFileSize);

		musicFileSections.FileStart2 = AlignNumber(file.tellp(), 0x800);
		musicFileSections.FileStart2Length = soundSampleDataSize;
		file.seekp(musicFileSections.FileStart2);
		file.write(soundSampleDataData, soundSampleDataSize);
		testHeader.FileSize = file.tellp();

		//--------------------------------------------------[Final offsets]--------------------------------------------------
		file.seekp(0, ios_base::beg);
		file.write((char*)&testHeader, sizeof(testHeader));
		file.write((char*)&musicFileSections, sizeof(musicFileSections));
		file.close();
	}
}