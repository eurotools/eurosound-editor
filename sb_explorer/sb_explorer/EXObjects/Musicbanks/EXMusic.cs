using System.Collections;

namespace sb_explorer.EXObjects.Musicbanks
{
    public class EXMusic
    {
        public ArrayList m_MusicMarkerStartData = new ArrayList();
        public ArrayList Markers = new ArrayList();

        //*===============================================================================================
        //* LEFT CHANNEL
        //*===============================================================================================
        public uint Frequency;
        public byte Channels;
        public uint Bits;
        public string Encoding_LeftChannel = string.Empty;
        public byte[] SampleByteData_LeftChannel = new byte[0];
        public byte[] SampleParsedData_LeftChannel = new byte[0];

        //*===============================================================================================
        //* RIGHT CHANNEL
        //*===============================================================================================
        public byte[] SampleByteData_RightChannel = new byte[0];
        public byte[] SampleParsedData_RightChannel = new byte[0];

        //*===============================================================================================
        //* EXTRA INFO
        //*===============================================================================================
        public uint MarkerSize;
        public uint AudioOffset;
        public uint AudioSize;
        public uint StartMarkerOffset;
        public uint MarkerOffset;
        public uint BaseVolume = 100;
        public uint HashCode = 0;
    }
}
