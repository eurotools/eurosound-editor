namespace sb_editor.Classes
{
    using System;
    using System.Runtime.InteropServices;

    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class FlashWindow
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static bool FlashWindowAPI(IntPtr handleToWindow)
        {
            FLASHWINFO flashwinfo1 = new FLASHWINFO();
            flashwinfo1.cbSize = Convert.ToUInt32(Marshal.SizeOf(flashwinfo1));
            flashwinfo1.hwnd = handleToWindow;
            flashwinfo1.dwFlags = 15;
            flashwinfo1.uCount = 7;
            flashwinfo1.dwTimeout = 0;
            return FlashWindowEx(ref flashwinfo1) == 0;
        }

        [DllImport("user32.dll")]
        private static extern short FlashWindowEx(ref FLASHWINFO pwfi);

        // Fields
        public static uint FLASHW_ALL = 3;
        public static uint FLASHW_CAPTION = 1;
        public static uint FLASHW_STOP = 0;
        public static uint FLASHW_TIMER = 4;
        public static uint FLASHW_TIMERNOFG = 12;
        public static uint FLASHW_TRAY = 2;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
