using System.Collections;

namespace sb_explorer
{
    internal class EXSoundStream
    {
        public ArrayList m_MusicMarkerStartData = new ArrayList();
        public ArrayList Markers = new ArrayList();

        //WAV File
        public byte[] SampleByteData = new byte[0];
        public byte[] SampleParsedData = new byte[0];
        public uint Frequency;
        public byte Channels;
        public uint Bits;
        public uint RealBits = 16;

        //Extra Info
        public uint MarkerSize;
        public uint AudioOffset;
        public uint AudioSize;
        public uint StartMarkerOffset;
        public uint MarkerOffset;
        public uint BaseVolume = 100;
    }
}
