using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EngineXMarkersTool
{
    internal static class FileWriters
    {
        //*===============================================================================================
        //* STREAM MARKER BINARY WRITER
        //*===============================================================================================
        internal static void WriteBinaryMarkerFile(string outputFilePath, List<EXStartMarker> StartMarkers, List<EXMarker> Markers, uint volume, bool isBigEndian)
        {
            using (BinaryWriter BWriter = new BinaryWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII))
            {
                //Start marker count
                BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers.Count, isBigEndian));
                //Marker count
                BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers.Count, isBigEndian));
                //Start marker offset
                BWriter.Write(0);
                //Marker offset
                BWriter.Write(0);
                //Base volume
                BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(volume, isBigEndian));

                //Start Markers Data
                long startMarkersOffset = BWriter.BaseStream.Position;
                for (int i = 0; i < StartMarkers.Count; i++)
                {
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].Index, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(StartMarkers[i].Position, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].Type, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].Flags, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].Extra, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(StartMarkers[i].LoopStart, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].MarkerCount, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].LoopMarkerIndex, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(StartMarkers[i].MarkerPosition, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Convert.ToInt32(StartMarkers[i].IsInstant), isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Convert.ToInt32(StartMarkers[i].InstantBuffer), isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(StartMarkers[i].State[0], isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(StartMarkers[i].State[1], isBigEndian));
                }

                //Markers
                long markersOffset = BWriter.BaseStream.Position;
                for (int j = 0; j < Markers.Count; j++)
                {
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers[j].Index, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(Markers[j].Position, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers[j].Type, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers[j].Flags, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers[j].Extra, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipUInt32(Markers[j].LoopStart, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers[j].MarkerCount, isBigEndian));
                    BWriter.Write(ESUtils.BytesFunctions.FlipInt32(Markers[j].LoopMarkerIndex, isBigEndian));
                }

                //Write start offsets
                BWriter.BaseStream.Seek(8, SeekOrigin.Begin);
                BWriter.Write(ESUtils.BytesFunctions.FlipUInt32((uint)startMarkersOffset, isBigEndian));
                BWriter.Write(ESUtils.BytesFunctions.FlipUInt32((uint)markersOffset, isBigEndian));

                //Close file
                BWriter.Close();
            }
        }
    }
}
