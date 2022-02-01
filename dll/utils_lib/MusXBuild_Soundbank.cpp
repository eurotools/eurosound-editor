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
	void BuildSoundbankFile(char* sfxFilePath, char* sifFilePath, char* sbFilePath, char* ssFilePath, char* outputFilePath, u32 fileHashCode, bool bigEndian)
	{
		//Create a new binary writer
		if ((outputFilePath != NULL) && (outputFilePath[0] != '\0'))
		{
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
				fileHeader.Hashcode = fileHashCode;
				//--offst[Constant offset to the next section,]--
				fileHeader.ConstantOffset = 0xC9;
				//--fulls[Size of the whole file, in bytes. Unused. ]--
				fileHeader.FileSize = 0;
				//--Write Data to file
				file.write((char*)&fileHeader, sizeof(fileHeader));

				//--------------------------------------------------[File Sections]--------------------------------------------------
				SoundBanksHeader fileSections;
				//--sfxstart[an offset that points to the section where soundbanks are stored, always 0x800]--
				fileSections.SFXStart = 0;
				//--sfxlength[size of the first section, in bytes]--
				fileSections.SFXLength = 0;

				//--sampleinfostart[offset to the second section where the sample properties are stored]--
				fileSections.SampleInfoStart = 0;
				//--sampleinfolen[size of the second section, in bytes]--
				fileSections.SampleInfoLength = 0;

				//--specialsampleinfostart[used for gamecube adpcm struct info]--
				fileSections.SpecialSampleInfoStart = 0;
				//--specialsampleinfolen[Size of the block, in bytes]--
				fileSections.SpecialSampleInfoLength = 0;

				//--sampledatastart[Offset that points to the beginning of the PCM data, where sound Is actually stored]--
				fileSections.SampleDataStart = 0;
				//--sampledatalen[Size of the block, in bytes]--
				fileSections.SampleDataLength = 0;

				//--------------------------------------------------[Read and Write Files Content]--------------------------------------------------
				//Write Hashcodes SFX Section
				if ((sfxFilePath != NULL) && (sfxFilePath[0] != '\0'))
				{
					FILE* sfxFile = fopen(sfxFilePath, "rb");
					if (sfxFile != NULL)
					{
						//Read data
						fseek(sfxFile, 0, SEEK_END);
						long sfxFileSize = ftell(sfxFile);
						rewind(sfxFile);
						//Get file data
						char* sfxFileData = (char*)malloc(sfxFileSize + 1);
						fread(sfxFileData, sfxFileSize, 1, sfxFile);
						fclose(sfxFile);
						sfxFileData[sfxFileSize] = 0;

						//Write data
						u32 positionAligned = AlignNumber((u32)file.tellp(), 0x800);
						fileSections.SFXStart = FlipUInt32(positionAligned, bigEndian);
						fileSections.SFXLength = FlipUInt32(sfxFileSize, bigEndian);
						file.seekp(positionAligned);
						file.write(sfxFileData, sfxFileSize);

						//Clear and liberate memmory
						free(sfxFileData);
						fclose(sfxFile);
					}
				}

				//Write SampleInfo SFX Section
				if ((sifFilePath != NULL) && (sifFilePath[0] != '\0'))
				{
					FILE* sifFile = fopen(sifFilePath, "rb");
					if (sifFile != NULL)
					{
						//Read data
						fseek(sifFile, 0, SEEK_END);
						long sifFileSize = ftell(sifFile);
						rewind(sifFile);
						//Get file data
						char* sifFiledata = (char*)malloc(sifFileSize + 1);
						fread(sifFiledata, sifFileSize, 1, sifFile);
						fclose(sifFile);
						sifFiledata[sifFileSize] = 0;

						//Write data
						u32 positionAligned = AlignNumber((u32)file.tellp(), 0x800);
						fileSections.SampleInfoStart = FlipUInt32(positionAligned, bigEndian);
						fileSections.SampleInfoLength = FlipUInt32(sifFileSize, bigEndian);
						file.seekp(positionAligned);
						file.write(sifFiledata, sifFileSize);

						//Clear and liberate memmory
						free(sifFiledata);
						fclose(sifFile);
					}
				}

				//Write special section
				u32 positionAligned = AlignNumber((u32)file.tellp(), 0x800);
				fileSections.SpecialSampleInfoStart = FlipUInt32(positionAligned, bigEndian);
				long ssFileSize = 0;
				if ((ssFilePath != NULL) && (ssFilePath[0] != '\0'))
				{
					FILE* ssFile = fopen(ssFilePath, "rb");
					if (ssFile != NULL)
					{
						//Read data
						fseek(ssFile, 0, SEEK_END);
						ssFileSize = ftell(ssFile);
						rewind(ssFile);
						//Get file data
						char* ssFiledata = (char*)malloc(ssFileSize + 1);
						fread(ssFiledata, ssFileSize, 1, ssFile);
						fclose(ssFile);
						ssFiledata[ssFileSize] = 0;

						//Write data
						fileSections.SpecialSampleInfoLength = FlipUInt32(ssFileSize, bigEndian);
						file.seekp(positionAligned);
						file.write(ssFiledata, ssFileSize);

						//Clear and liberate memmory
						free(ssFiledata);
						fclose(ssFile);
					}
				}


				//Write Sample Data SFX Section
				if ((sbFilePath != NULL) && (sbFilePath[0] != '\0'))
				{
					FILE* sbFile = fopen(sbFilePath, "rb");
					if (sbFile != NULL)
					{
						//Read data
						fseek(sbFile, 0, SEEK_END);
						long sbFileSize = ftell(sbFile);
						rewind(sbFile);
						//Get file data
						char* ssFiledata = (char*)malloc(sbFileSize + 1);
						fread(ssFiledata, sbFileSize, 1, sbFile);
						fclose(sbFile);
						ssFiledata[sbFileSize] = 0;

						//Write data
						if (ssFileSize > 0)
						{
							positionAligned = AlignNumber((u32)file.tellp(), 0x800);
						}
						fileSections.SampleDataStart = FlipUInt32(positionAligned, bigEndian);
						fileSections.SampleDataLength = FlipUInt32(sbFileSize, bigEndian);
						file.seekp(positionAligned);
						file.write(ssFiledata, sbFileSize);

						//Clear and liberate memmory
						free(ssFiledata);
						fclose(sbFile);
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