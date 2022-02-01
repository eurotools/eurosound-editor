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
	void BuildStreamFile(char* binFilePath, char* lutFilePath, char* outputFilePath, bool bigEndian)
	{
		if ((outputFilePath != NULL) && (outputFilePath[0] != '\0'))
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
				fileHeader.Hashcode = 0xFFFF;
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
				//Streams lut Data
				if ((lutFilePath != NULL) && (lutFilePath[0] != '\0'))
				{
					FILE* streamLutFile = fopen(lutFilePath, "rb");
					if (streamLutFile != NULL)
					{
						//Read data
						fseek(streamLutFile, 0, SEEK_END);
						long streamLutDataSize = ftell(streamLutFile);
						rewind(streamLutFile);
						//Get file data
						char* streamLutFileData = (char*)malloc(streamLutDataSize + 1);
						fread(streamLutFileData, streamLutDataSize, 1, streamLutFile);
						fclose(streamLutFile);
						streamLutFileData[streamLutDataSize] = 0;

						//Write data
						u32 positionAligned = AlignNumber((u32)file.tellp(), 0x800);
						fileSections.FileStart1 = FlipUInt32(positionAligned, bigEndian);
						fileSections.FileStart1Length = FlipUInt32(streamLutDataSize, bigEndian);
						file.seekp(positionAligned);
						file.write(streamLutFileData, streamLutDataSize);

						//Clear and liberate memmory
						free(streamLutFileData);
						fclose(streamLutFile);
					}
				}

				//Streams Bin Data
				if ((binFilePath != NULL) && (binFilePath[0] != '\0'))
				{
					FILE* streamBinFile = fopen(binFilePath, "rb");
					if (streamBinFile != NULL)
					{
						//Read data
						fseek(streamBinFile, 0, SEEK_END);
						long streamBinFileSize = ftell(streamBinFile);
						rewind(streamBinFile);
						//Get file data
						char* streamBinFileData = (char*)malloc(streamBinFileSize + 1);
						fread(streamBinFileData, streamBinFileSize, 1, streamBinFile);
						fclose(streamBinFile);
						streamBinFileData[streamBinFileSize] = 0;

						//Write data
						u32 positionAligned = AlignNumber((u32)file.tellp(), 0x800);
						fileSections.FileStart2 = FlipUInt32(positionAligned, bigEndian);
						fileSections.FileStart2Length = FlipUInt32(streamBinFileSize, bigEndian);
						file.seekp(positionAligned);
						file.write(streamBinFileData, streamBinFileSize);

						//Clear and liberate memmory
						free(streamBinFileData);
						fclose(streamBinFile);
					}
				}

				//Get File Size
				fileHeader.FileSize = (u32)file.tellp();

				//--------------------------------------------------[Write Final offsets]--------------------------------------------------
				file.seekp(0, ios_base::beg);
				file.write((char*)&fileHeader, sizeof(fileHeader));
				file.write((char*)&fileSections, sizeof(fileSections));
				file.close();
			}
		}
	}
}
EUROSOUND_MUSX_END