using System.Runtime.InteropServices;

namespace EngineXMarkersTool
{
    internal static class UtilsFunctions
    {
        internal struct ImaAdpcmState
        {
            public int valprev;
            public int index;
        }

        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DecodeStatesIma(ref ImaAdpcmState state, byte[] input, int numSamples, uint[] output);
        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetMusicLoopOffsetPCandGC(uint inputLoopOffset);
        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetMusicLoopOffsetPlayStation2(uint inputLoopOffset);
        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetMusicLoopOffsetXbox(uint inputLoopOffset);
        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetStreamLoopOffsetPlayStation2(uint inputLoopOffset);
        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetStreamLoopOffsetXbox(uint inputLoopOffset);
        [DllImport("SystemFiles\\EuroSound_Utils.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetStreamLoopOffsetPCandGC(uint inputLoopOffset);
    }
}
