//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Utils
//-------------------------------------------------------------------------------------------------------------------------------
using MusX.Readers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using static MusX.Readers.SfxFunctions;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal static class Utils
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static byte[] ShortArrayToByteArray(short[] inputArray)
        {
            byte[] byteArray = new byte[inputArray.Length * 2];
            Buffer.BlockCopy(inputArray, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static FileType GetFileType(string filePath)
        {
            SfxFunctions readingFunctions = new SfxFunctions();

            int hashCode = readingFunctions.GetFileHashCode(filePath);
            int selectedVersion = readingFunctions.GetFileVersion(filePath);

            if (hashCode == 0xFFFE)
            {
                return FileType.TestSFX;
            }
            else if (selectedVersion == 201)
            {
                int sectionHashCode = (hashCode & 0x00F00000) >> 20;
                if (sectionHashCode == 0xE)
                {
                    return FileType.MusicFile;
                }
                else if (hashCode == 0x0000FFFF)
                {
                    return FileType.StreamFile;
                }
                else if (hashCode == 0x00FFFFFF)
                {
                    return FileType.SBI;
                }
                else
                {
                    return FileType.SoundbankFile;
                }
            }
            else if (selectedVersion == 4 || selectedVersion == 5)
            {
                int sectionHashCode = (hashCode & 0x0000F000) >> 12;
                switch (sectionHashCode)
                {
                    case 0xA:
                        return FileType.MusicDetails;
                    case 0xB:
                        return FileType.SoundDetailsFile;
                    case 0xC:
                        return FileType.ProjectDetails;
                    case 0xD:
                        return FileType.StreamFile;
                    case 0xE:
                        return FileType.SoundbankFile;
                    case 0xF:
                        return FileType.MusicFile;
                }
            }
            return FileType.Unknown;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
