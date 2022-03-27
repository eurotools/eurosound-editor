using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using static ESUtils.CalculusLoopOffset;

namespace EngineXMarkersTool
{
    internal class StreamFunctions
    {
        //*===============================================================================================
        //* STREAM MARKER BINARY FILE
        //*===============================================================================================
        internal void CreateMarkerBinFile(string smdFilePath, string markerFilePath, string outputFilePath, string outputPlatform, uint baseVolume)
        {
            //List to store the text file markers
            List<EXStartMarker> startMarkersList = new List<EXStartMarker>();
            List<EXMarker> markersList = new List<EXMarker>();
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();

            //Read Markers File
            streamMarkersFunctions.LoadFile(markerFilePath, startMarkersList, markersList);

            //Calculate states -- PC & GameCube Platform
            if (outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
            {
                //Update Markers states
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        foreach (EXStartMarker startMarker in startMarkersList)
                        {
                            if (startMarker.Index == marker.MarkerCount)
                            {
                                using (BinaryReader breader = new BinaryReader(File.Open(smdFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                                {
                                    breader.BaseStream.Seek((marker.Position / 256) * 256, SeekOrigin.Begin);
                                    uint state = breader.ReadUInt32();
                                    startMarker.State[0] = state;
                                    startMarker.State[1] = state;
                                }
                                break;
                            }
                        }
                    }
                }

                //Start markers
                foreach (EXStartMarker startMarker in startMarkersList)
                {
                    //Calculate VAG offsets
                    if (startMarker.Position > 0)
                    {
                        startMarker.Position = GetStreamLoopOffsetPCandGC(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = GetStreamLoopOffsetPCandGC(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = GetStreamLoopOffsetPCandGC(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = GetStreamLoopOffsetPCandGC(marker.LoopStart);
                    }
                }
            }

            //Update Positions PS2 Platform
            if (outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (EXStartMarker startMarker in startMarkersList)
                {
                    //Calculate VAG offsets
                    if (startMarker.Position > 0)
                    {
                        startMarker.Position = GetStreamLoopOffsetPlayStation2(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = GetStreamLoopOffsetPlayStation2(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = GetStreamLoopOffsetPlayStation2(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = GetStreamLoopOffsetPlayStation2(marker.LoopStart);
                    }
                }
            }

            //Update positions for Xbox
            if (outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (EXStartMarker startMarker in startMarkersList)
                {
                    //Calculate VAG offsets
                    if (startMarker.Position > 0)
                    {
                        startMarker.Position = GetStreamLoopOffsetXbox(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = GetStreamLoopOffsetXbox(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = GetStreamLoopOffsetXbox(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = GetStreamLoopOffsetXbox(marker.LoopStart);
                    }
                }
            }

            //Write Sound Marker File
            FileWriters.WriteBinaryMarkerFile(outputFilePath, startMarkersList, markersList, baseVolume, outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase));
        }
    }
}
