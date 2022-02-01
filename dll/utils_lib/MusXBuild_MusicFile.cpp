#include "ESUtils.h"
#include "BytesFunctions.h"
#include "MusX.h"
#include <iostream>
#include <fstream>

EUROSOUND_MUSX_START

using namespace std;
using namespace ESFunctions;

//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
extern "C"
{
	void BuildMusicFile(char* mkrFilePath, char* ssdFilePath, char* outputFilePath, u32 fileHashCode, bool bigEndian)
	{
		//Create a new binary writer
		ofstream file(outputFilePath, ios::binary);
		if (file.is_open())
		{
			//--------------------------------------------------[File Header]--------------------------------------------------
			//Declaration
			MusXHeader fileHeader;
			const char* magic = "MUSX";
			//--magic[magic value]--
			strcpy(fileHeader.Magic, magic);
			//--hashc[Hashcode for the current soundbank without the section prefix]--
			fileHeader.Hashcode = fileHashCode | 0xE00000;
			//--offst[Constant offset to the next section,]--
			fileHeader.ConstantOffset = 0xC9;
			//--fulls[Size of the whole file, in bytes. Unused. ]--
			fileHeader.FileSize = 0;
			//--Write Data to file
			file.write((char*)&fileHeader, sizeof(fileHeader));

			//--------------------------------------------------[File Sections]--------------------------------------------------
			MusicBanksHeader fileSections;
			//--File start 1; an offset that points to the stream look-up file details. Set to 0x800 in the original software. --
			fileSections.FileStart1 = 0;
			//--File length 1; size of the first section, in bytes. --
			fileSections.FileStart1Length = 0;
			//--File start 2; offset to the second section with the sample data. Set to 0x1000 in the original software. --
			fileSections.FileStart2 = 0;
			//--File length 2; size of the second section, in bytes. --
			fileSections.FileStart2Length = 0;
			//--File start 3; unused offset. Set to zero.--
			fileSections.FileStart3 = 0;
			//--File length 3; unused. Set to zero.--
			fileSections.FileStart3Length = 0;
			//--Write Data to file
			file.write((char*)&fileSections, sizeof(fileSections));

			//--------------------------------------------------[Read and Write Files Content]--------------------------------------------------
			//Sound Marker File
			FILE* markersFile = fopen(mkrFilePath, "rb");
			if (markersFile != NULL)
			{
				//Read data
				fseek(markersFile, 0, SEEK_END);
				long markersFileSize = ftell(markersFile);
				rewind(markersFile);
				//Get file data
				char* markersFileData = (char*)malloc(markersFileSize + 1);
				fread(markersFileData, markersFileSize, 1, markersFile);
				fclose(markersFile);
				markersFileData[markersFileSize] = 0;

				//Write data
				u32 positionAligned = AlignNumber(file.tellp(), 0x800);
				fileSections.FileStart1 = FlipUInt32(positionAligned, bigEndian);
				fileSections.FileStart1Length = FlipUInt32(markersFileSize, bigEndian);
				file.seekp(positionAligned);
				file.write(markersFileData, markersFileSize);
			}

			//Sound Sample Data
			FILE* soundSampleData = fopen(ssdFilePath, "rb");
			if (soundSampleData != NULL)
			{
				//Read data
				fseek(soundSampleData, 0, SEEK_END);
				long soundSampleDataSize = ftell(soundSampleData);
				rewind(soundSampleData);
				//Get file data
				char* soundSampleDataData = (char*)malloc(soundSampleDataSize + 1);
				fread(soundSampleDataData, soundSampleDataSize, 1, soundSampleData);
				fclose(soundSampleData);
				soundSampleDataData[soundSampleDataSize] = 0;

				//Write data
				u32 positionAligned = AlignNumber(file.tellp(), 0x800);
				fileSections.FileStart2 = FlipUInt32(positionAligned, bigEndian);
				fileSections.FileStart2Length = FlipUInt32(soundSampleDataSize, bigEndian);
				file.seekp(positionAligned);
				file.write(soundSampleDataData, soundSampleDataSize);
				fileHeader.FileSize = file.tellp();
			}

			//--------------------------------------------------[Write Final offsets]--------------------------------------------------
			file.seekp(0, ios_base::beg);
			file.write((char*)&fileHeader, sizeof(fileHeader));
			file.write((char*)&fileSections, sizeof(fileSections));
			file.close();
		}
	}
}
EUROSOUND_MUSX_END