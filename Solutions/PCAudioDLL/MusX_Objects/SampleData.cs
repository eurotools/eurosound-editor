namespace PCAudioDLL.MusX_Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SampleData
    {
        public int Flags;
        public int Address;
        public int MemorySize;
        public int Frequency;
        public int SampleSize;
        public int PsiSampleHeader;
        public int Channels;
        public int Bits;
        public int LoopStartOffset;
        public int TotalSamples;
        public int Duration;
        public byte[] EncodedData;
        public short[] DspCoeffs;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
