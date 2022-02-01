using EngineXMarkersTool.Classes;
using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace EngineXMarkersTool
{
    internal class StreamFunctions
    {
        //*===============================================================================================
        //* STREAM MARKER BINARY FILE
        //*===============================================================================================
        internal void CreateMarkerBinFile(string imaFilePath, string markerFilePath, string outputDir, string outputPlatform, uint baseVolume)
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

                uint[] pcImaDecodedStates = new uint[imaData.Length * 2];
                UtilsFunctions.ImaAdpcmState states = new UtilsFunctions.ImaAdpcmState();
                UtilsFunctions.DecodeStatesIma(ref states, imaData, imaData.Length * 2, pcImaDecodedStates);

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
                        startMarker.Position = UtilsFunctions.GetStreamLoopOffsetPCandGC(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = UtilsFunctions.GetStreamLoopOffsetPCandGC(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = UtilsFunctions.GetStreamLoopOffsetPCandGC(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = UtilsFunctions.GetStreamLoopOffsetPCandGC(marker.LoopStart);
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
                        startMarker.Position = UtilsFunctions.GetStreamLoopOffsetPlayStation2(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = UtilsFunctions.GetStreamLoopOffsetPlayStation2(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = UtilsFunctions.GetStreamLoopOffsetPlayStation2(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = UtilsFunctions.GetStreamLoopOffsetPlayStation2(marker.LoopStart);
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
                        startMarker.Position = UtilsFunctions.GetStreamLoopOffsetXbox(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = UtilsFunctions.GetStreamLoopOffsetXbox(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = UtilsFunctions.GetStreamLoopOffsetXbox(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = UtilsFunctions.GetStreamLoopOffsetXbox(marker.LoopStart);
                    }
                }
            }

            //Write Sound Marker File
            string outputFilePath = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(imaFilePath) + ".smf");
            FileWriters.WriteBinaryMarkerFile(outputFilePath, startMarkersList, markersList, baseVolume);
        }
    }
}
