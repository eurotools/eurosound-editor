namespace PCAudioDLL.MusX_Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SoundbankHeader : SfxCommonHeader
    {
        public uint SFXStart;
        public uint SFXLenght;

        public uint SampleInfoStart;
        public uint SampleInfoLenght;

        public uint SpecialSampleInfoStart;
        public uint SpecialSampleInfoLength;

        public uint SampleDataStart;
        public uint SampleDataLength;

        //-------------------------------------------------------------------------------------------------------------------------------
        public SoundbankHeader(SfxCommonHeader commonHeader = null)
        {
            if (commonHeader != null)
            {
                IsBigEndian = commonHeader.IsBigEndian;
                FileHashCode = commonHeader.FileHashCode;
                FileVersion = commonHeader.FileVersion;
                FileSize = commonHeader.FileSize;
                Platform = commonHeader.Platform;
                Timespan = commonHeader.Timespan;
                UsesAdpcm = commonHeader.UsesAdpcm;
                EndOffset = commonHeader.EndOffset;
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
