using ESUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExMarkers
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class StreamMarkerFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public void CreateMarkerBinFile(string smdFilePath, string markerFilePath, string outputFilePath, string outputPlatform)
        {
            //List to store the text file markers
            List<EXStartMarker> startMarkersList = new List<EXStartMarker>();
            List<EXMarker> markersList = new List<EXMarker>();
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();

            //Read Markers File
            streamMarkersFunctions.LoadTextMarkerFile(markerFilePath, startMarkersList, markersList);

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
                                uint state = 0;
                                using (BinaryReader breader = new BinaryReader(File.Open(smdFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                                {
                                    long offset = (marker.Position / 256) * 256;
                                    if (offset <= breader.BaseStream.Length)
                                    {
                                        breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                        state = breader.ReadUInt32();
                                    }
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
                        startMarker.Position = CalculusLoopOffset.GetStreamLoopOffsetPCandGC(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = CalculusLoopOffset.GetStreamLoopOffsetPCandGC(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetStreamLoopOffsetPCandGC(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = CalculusLoopOffset.GetStreamLoopOffsetPCandGC(marker.LoopStart);
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
                        startMarker.Position = CalculusLoopOffset.GetStreamLoopOffsetPlayStation2(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = CalculusLoopOffset.GetStreamLoopOffsetPlayStation2(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetStreamLoopOffsetPlayStation2(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = CalculusLoopOffset.GetStreamLoopOffsetPlayStation2(marker.LoopStart);
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
                        startMarker.Position = CalculusLoopOffset.GetStreamLoopOffsetXbox(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = CalculusLoopOffset.GetStreamLoopOffsetXbox(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetStreamLoopOffsetXbox(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = CalculusLoopOffset.GetStreamLoopOffsetXbox(marker.LoopStart);
                    }
                }
            }

            //Write Sound Marker File
            streamMarkersFunctions.WriteBinaryMarkerFile(outputFilePath, startMarkersList, markersList, 100, outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase));
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
