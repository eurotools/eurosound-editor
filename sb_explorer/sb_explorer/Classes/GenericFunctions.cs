using System.IO;

namespace sb_explorer
{
    class GenericFunctions
    {
        //*===============================================================================================
        //* ENUMERATIONS
        //*===============================================================================================
        internal enum CurrentFileType : uint
        {
            SoundBanks = 1,
            StreamSounds = 2,
            MusicBanks = 3
        }

        internal enum CurrentPlatform : byte
        {
            PC = 0,
            PS2 = 1,
            GC = 2,
            XBX = 3
        }

        //*===============================================================================================
        //* FUNCTIONS TO IDENTIFY FILES
        //*===============================================================================================
        internal static byte FindPlatform(string filePath)
        {
            string absPath = Path.GetFullPath(filePath);
            byte platform = byte.MaxValue;

            //Check for platform folder
            string pathConverted = absPath.ToUpper();
            if (pathConverted.Contains("XB"))
            {
                platform = (byte)CurrentPlatform.XBX;
            }
            else if (pathConverted.Contains("PS2"))
            {
                platform = (byte)CurrentPlatform.PS2;
            }
            else if (pathConverted.Contains("GC"))
            {
                platform = (byte)CurrentPlatform.GC;
            }
            else if (pathConverted.Contains("PC"))
            {
                platform = (byte)CurrentPlatform.PC;
            }
            return platform;
        }

        //*===============================================================================================
        //* FLIP DATA
        //*===============================================================================================
        internal static uint FlipUInt32(uint valueToFlip, bool IsBigEndian)
        {
            uint finalData;

            if (IsBigEndian)
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

        internal static int FlipInt32(int valueToFlip, bool IsBigEndian)
        {
            int finalData;

            if (IsBigEndian)
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

        internal static short FlipShort(short valueToFlip, bool IsBigEndian)
        {
            short finalData;

            if (IsBigEndian)
            {
                finalData = (short)((valueToFlip & 0xFF00) >> (8 * 1) | /* 0x11__ -> 0x__11 */
                                    (valueToFlip & 0x00FF) << (8 * 1)); /* 0x__22 -> 0x22__ */
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }

        internal static ushort FlipUShort(ushort valueToFlip, bool IsBigEndian)
        {
            ushort finalData;

            if (IsBigEndian)
            {
                //Flip input value
                finalData = (ushort)((valueToFlip & 0xFF00) >> (8 * 1) | /* 0x11__ -> 0x__11 */
                                     (valueToFlip & 0x00FF) << (8 * 1)); /* 0x__22 -> 0x22__ */
            }
            else
            {
                finalData = valueToFlip;
            }

            return finalData;
        }
    }
}
