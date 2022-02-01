using System;

namespace sb_explorer
{
    internal static class AudioFunctions
    {
        internal static byte[] ShortArrayToByteArray(short[] inputArray)
        {
            byte[] byteArray = new byte[inputArray.Length * 2];
            Buffer.BlockCopy(inputArray, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }
    }
}
