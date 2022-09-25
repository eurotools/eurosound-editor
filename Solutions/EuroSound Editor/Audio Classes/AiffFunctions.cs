using ESUtils;
using NAudio.Wave;
using System.IO;
using System.Text;

namespace EuroSound_Editor.Audio_Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class AiffFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal WavInfo ReadWaveProperties(string waveFilePath)
        {
            WavInfo waveFileData;
            using (AiffFileReader wReader = new AiffFileReader(waveFilePath))
            {
                waveFileData = new WavInfo
                {
                    Channels = wReader.WaveFormat.Channels,
                    BitsPerSample = wReader.WaveFormat.BitsPerSample,
                    SampleRate = wReader.WaveFormat.SampleRate,
                    AverageBytesPerSecond = wReader.WaveFormat.AverageBytesPerSecond,
                    SampleCount = wReader.SampleCount,
                    Length = wReader.Length,
                    TotalTime = wReader.TotalTime
                };
            }

            return waveFileData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void AddLoopPoints(string filePath, int startPos, long endPos, int midiNote)
        {
            long fileLength;

            //Append Chunck at the end of file
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                //Add Instrument chunk
                binWriter.Write(Encoding.ASCII.GetBytes("INST"));
                binWriter.Write(BytesFunctions.FlipInt32(20, true));
                binWriter.Write((byte)midiNote);
                binWriter.Write((byte)0);
                binWriter.Write((byte)0);
                binWriter.Write((byte)127);
                binWriter.Write((byte)0);
                binWriter.Write((byte)127);
                binWriter.Write((short)0);
                binWriter.Write(BytesFunctions.FlipShort(1, true));
                binWriter.Write((short)0);
                binWriter.Write(BytesFunctions.FlipShort(1, true));
                binWriter.Write((short)0);
                binWriter.Write((short)0);
                binWriter.Write((short)0);

                //Add Markers chunk
                binWriter.Write(Encoding.ASCII.GetBytes("MARK"));
                binWriter.Write(BytesFunctions.FlipInt32(34, true));
                binWriter.Write(BytesFunctions.FlipShort(2, true));

                //Start
                binWriter.Write(BytesFunctions.FlipShort(0, true));
                binWriter.Write(BytesFunctions.FlipInt32(startPos, true));
                binWriter.Write((byte)(8));
                binWriter.Write(Encoding.ASCII.GetBytes("beg loop"));
                binWriter.Write((byte)(0));

                //End
                binWriter.Write(BytesFunctions.FlipShort(1, true));
                binWriter.Write(BytesFunctions.FlipInt32((int)endPos, true));
                binWriter.Write((byte)(8));
                binWriter.Write(Encoding.ASCII.GetBytes("end loop"));
                binWriter.Write((byte)(0));

                //Get file length
                fileLength = binWriter.BaseStream.Position - 8;

                //Add some padding bytes
                binWriter.Write(new byte[18]);
            }

            //Update Form Chunk size
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.Read)))
            {
                binWriter.BaseStream.Seek(4, SeekOrigin.Begin);
                binWriter.Write(BytesFunctions.FlipUInt32((uint)fileLength, true));
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
