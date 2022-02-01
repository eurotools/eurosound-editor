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
        internal static void WriteBinaryMarkerFile(string outputFilePath, List<EXStartMarker> StartMarkers, List<EXMarker> Markers, uint volume)
        {
            using (BinaryWriter BWriter = new BinaryWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII))
            {
                //Start marker count
                BWriter.Write((uint)StartMarkers.Count);
                //Marker count
                BWriter.Write((uint)Markers.Count);
                //Start marker offset
                BWriter.Write(0);
                //Marker offset
                BWriter.Write(0);
                //Base volume
                BWriter.Write(volume);

                //Start Markers Data
                long startMarkersOffset = BWriter.BaseStream.Position;
                for (int i = 0; i < StartMarkers.Count; i++)
                {
                    BWriter.Write(StartMarkers[i].Index);
                    BWriter.Write(StartMarkers[i].Position);
                    BWriter.Write(StartMarkers[i].Type);
                    BWriter.Write(StartMarkers[i].Flags);
                    BWriter.Write(StartMarkers[i].Extra);
                    BWriter.Write(StartMarkers[i].LoopStart);
                    BWriter.Write(StartMarkers[i].MarkerCount);
                    BWriter.Write(StartMarkers[i].LoopMarkerIndex);
                    BWriter.Write(StartMarkers[i].MarkerPosition);
                    BWriter.Write(Convert.ToInt32(StartMarkers[i].IsInstant));
                    BWriter.Write(Convert.ToInt32(StartMarkers[i].InstantBuffer));
                    BWriter.Write(StartMarkers[i].State[0]);
                    BWriter.Write(StartMarkers[i].State[1]);
                }

                //Markers
                long markersOffset = BWriter.BaseStream.Position;
                for (int j = 0; j < Markers.Count; j++)
                {
                    BWriter.Write(Markers[j].Index);
                    BWriter.Write(Markers[j].Position);
                    BWriter.Write(Markers[j].Type);
                    BWriter.Write(Markers[j].Flags);
                    BWriter.Write(Markers[j].Extra);
                    BWriter.Write(Markers[j].LoopStart);
                    BWriter.Write(Markers[j].MarkerCount);
                    BWriter.Write(Markers[j].LoopMarkerIndex);
                }

                //Write start offsets
                BWriter.BaseStream.Seek(8, SeekOrigin.Begin);
                BWriter.Write((uint)startMarkersOffset);
                BWriter.Write((uint)markersOffset);

                //Close file
                BWriter.Close();
            }
        }
    }
}
