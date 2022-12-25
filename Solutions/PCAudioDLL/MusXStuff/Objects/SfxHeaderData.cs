namespace PCAudioDLL.MusXStuff.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SfxHeaderData
    {
        public uint FileHashCode;
        public uint FileVersion;
        public uint FileSize;

        //Soundbanks
        public uint SFXStart;
        public uint SFXLenght;

        public uint SampleInfoStart;
        public uint SampleInfoLenght;

        public uint SpecialSampleInfoStart;
        public uint SpecialSampleInfoLength;

        public uint SampleDataStart;
        public uint SampleDataLength;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
