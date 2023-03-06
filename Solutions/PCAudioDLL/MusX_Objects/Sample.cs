using System.Collections.Generic;

namespace PCAudioDLL.MusX_Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class Sample
    {
        //Parameters
        public short DuckerLenght;
        public short MinDelay;
        public short MaxDelay;
        public sbyte ReverbSend;
        public sbyte TrackingType;
        public sbyte MaxVoices;
        public sbyte Priority;
        public sbyte Ducker;
        public float MasterVolume;
        public short GroupHashCode;
        public sbyte GroupMaxChannels;
        public sbyte DopplerValue;
        public sbyte UserValue;
        public sbyte SFXDucker;
        public sbyte Spare;
        public short InnerRadius;
        public short OuterRadius;

        //Flags
        public ushort Flags;
        public ushort UserFlags;

        //Samples
        public List<SampleInfo> samplesList = new List<SampleInfo>();
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
