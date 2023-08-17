namespace MIDI2TXT
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal static class BinaryFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal static uint FlipUInt32(uint valueToFlip, bool IsBigEndian)
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
        internal static int FlipInt32(int valueToFlip, bool IsBigEndian)
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
        internal static short FlipShort(short valueToFlip, bool IsBigEndian)
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
        internal static ushort FlipUShort(ushort valueToFlip, bool IsBigEndian)
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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
