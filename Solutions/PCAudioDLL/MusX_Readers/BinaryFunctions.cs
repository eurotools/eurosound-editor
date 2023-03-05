using System;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal static class BinaryFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal static uint FlipData(uint valueToFlip, bool IsBigEndian)
        {
            uint finalData;

            if (IsBigEndian)
            {
                finalData = (valueToFlip & 0xFF0000 | valueToFlip >> 16) >> 8 | (valueToFlip & 0xFF00 | valueToFlip << 16) << 8;
            }
            else
            {
                finalData = valueToFlip;
            }
            return finalData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static int FlipData(int valueToFlip, bool IsBigEndian)
        {
            int finalData;

            if (IsBigEndian)
            {
                finalData = (valueToFlip & 0xFF00 | valueToFlip << 16) << 8 | valueToFlip >> 8 & 0xFF00 | valueToFlip >> 24 & byte.MaxValue;
            }
            else
            {
                finalData = valueToFlip;
            }
            return finalData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static short FlipData(short valueToFlip, bool IsBigEndian)
        {
            short finalData;

            if (IsBigEndian)
            {
                finalData = (short)(valueToFlip >> 8 & byte.MaxValue | (valueToFlip & byte.MaxValue) << 8);
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static ushort FlipData(ushort valueToFlip, bool IsBigEndian)
        {
            ushort finalData;

            if (IsBigEndian)
            {
                //Flip input value
                finalData = (ushort)((valueToFlip << 8) | valueToFlip >> 8);
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal static float FlipData(float valueToFlip, bool IsBigEndian)
        {
            float finalData;

            if (IsBigEndian)
            {
                byte[] floatData = BitConverter.GetBytes(valueToFlip);
                byte b1 = floatData[0];
                byte b2 = floatData[1];
                byte b3 = floatData[2];
                byte b4 = floatData[3];

                finalData = b1 << 24 | b2 << 16 | (b3 << 8) | b4;
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
