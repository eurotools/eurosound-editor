using EngineXMarkersTool.Classes;
using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using static ESUtils.CalculusLoopOffset;
using static ESUtils.ImaCodec;

namespace EngineXMarkersTool
{
    internal class StreamFunctions
    {
        //*===============================================================================================
        //* STREAM MARKER BINARY FILE
        //*===============================================================================================
        internal void CreateMarkerBinFile(string imaFilePath, string markerFilePath, string outputFilePath, string outputPlatform, uint baseVolume)
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
                //Read IMA Data
                byte[] imaData = File.ReadAllBytes(imaFilePath);
                uint[] pcImaDecodedStates = DecodeStatesIma(imaData, imaData.Length * 2);

                //Update Markers states
                EXMarkersFunctions markersFunctions = new EXMarkersFunctions();
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        foreach (EXStartMarker startMarker in startMarkersList)
                        {
                            if (startMarker.Index == marker.MarkerCount)
                            {
                                uint[] IMA_States = markersFunctions.GetEngineXMarkerStates_Mono(pcImaDecodedStates, (int)marker.Position);
                                startMarker.State[0] = IMA_States[0];
                                startMarker.State[1] = IMA_States[1];
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
            FileWriters.WriteBinaryMarkerFile(outputFilePath, startMarkersList, markersList, baseVolume);
        }
    }
}
