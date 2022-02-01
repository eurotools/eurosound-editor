#include "ESUtils.h"
#include "BytesFunctions.h"
#include <stdio.h>

EUROSOUND_FUNCTIONS_START

//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------
extern "C"
{
    //-------------------------------------------------------------------------------------------------------------------------------
    char* FormatBytes(long bytes)
    {
        const char* suffix[] = { "bytes", "KB", "MB", "GB", "TB" };
        char length = sizeof(suffix) / sizeof(suffix[0]);

        int i = 0;
        double dblBytes = bytes;
        if (bytes > 1024)
        {
            for (i = 0; (bytes / 1024) > 0 && i < length - 1; i++, bytes /= 1024)
            {
                dblBytes = bytes / 1024.0;
            }
        }

        static char output[200];
        sprintf(output, "%.02lf %s", dblBytes, suffix[i]);

        return output;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    u32 FlipUInt32(u32 valueToFlip, bool isBigEndian)
    {
        u32 finalData;
        if (isBigEndian)
        {
            finalData = (valueToFlip & 0xFF000000) >> (8 * 3) | /* 0x11______ -> 0x______11 */
                (valueToFlip & 0x00FF0000) >> (8 * 1) | /* 0x__22____ -> 0x____22__ */
                (valueToFlip & 0x0000FF00) << (8 * 1) | /* 0x____33__ -> 0x__33____ */
                (valueToFlip & 0x000000FF) << (8 * 3);  /* 0x______44 -> 0x44______ */
        }
        else
        {
            finalData = valueToFlip;
        }
        return finalData;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    s32 FlipInt32(s32 valueToFlip, bool isBigEndian)
    {
        s32 finalData;

        if (isBigEndian)
        {
            finalData = (valueToFlip & 0x7F000000) >> (8 * 3) | /* 0x11______ -> 0x______11 */
                (valueToFlip & 0x00FF0000) >> (8 * 1) | /* 0x__22____ -> 0x____22__ */
                (valueToFlip & 0x0000FF00) << (8 * 1) | /* 0x____33__ -> 0x__33____ */
                (valueToFlip & 0x000000FF) << (8 * 3);  /* 0x______44 -> 0x44______ */
        }
        else
        {
            finalData = valueToFlip;
        }
        return finalData;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    s16 FlipShort(s16 valueToFlip, bool isBigEndian)
    {
        s16 finalData;
        if (isBigEndian)
        {
            finalData = (valueToFlip & 0xFF00) >> (8 * 1) | /* 0x11__ -> 0x__11 */
                (valueToFlip & 0x00FF) << (8 * 1);  /* 0x__22 -> 0x22__ */
        }
        else
        {
            finalData = valueToFlip;
        }

        return finalData;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    u16 FlipUShort(u16 valueToFlip, bool isBigEndian)
    {
        u16 finalData;

        if (isBigEndian)
        {
            //Flip input value
            finalData = (valueToFlip & 0xFF00) >> (8 * 1) | /* 0x11__ -> 0x__11 */
                (valueToFlip & 0x00FF) << (8 * 1);  /* 0x__22 -> 0x22__ */
        }
        else
        {
            finalData = valueToFlip;
        }

        return finalData;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    u32 AlignNumber(u32 valueToAlign, u32 blockSize)
    {
        u32 PositionAligned = ((valueToAlign + (blockSize - 1)) & ~(blockSize - 1));
        return PositionAligned;
    }
}

//-------------------------------------------------------------------------------------------------------------------------------
EUROSOUND_FUNCTIONS_END