namespace sb_explorer.EXObjects
{
    public class EXAudio
    {
        public ushort Flags;
        public byte[] SampleByteData = new byte[0];
        public short[] DspCoefs = new short[0];
        public uint SampleSize;
        public uint Frequency;
        public uint MemorySize;
        public uint Channels;
        public uint Bits;
        public uint RealBits;
        public uint PSIsample;
        public uint LoopStartOffset;
        public uint DurationInMilliseconds;
        public uint Address;
    }
}
