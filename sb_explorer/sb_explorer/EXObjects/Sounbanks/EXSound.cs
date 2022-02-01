using System.Collections.Generic;

namespace sb_explorer.EXObjects
{
    public class EXSound
    {
        public short DuckerLenght;
        public short MinDelay;
        public short MaxDelay;
        public short InnerRadiusReal;
        public short OuterRadiusReal;
        public sbyte ReverbSend;
        public sbyte TrackingType;
        public sbyte MaxVoices;
        public sbyte Priority;
        public sbyte Ducker;
        public sbyte MasterVolume;
        public ushort Flags;
        public uint Hashcode;
        public uint FlagsOffset;
        public uint SamplePoolOffset;
        public uint BinaryLength;
        public byte[] BinaryData;

        public List<EXSample> Samples = new List<EXSample>();
    }
}
