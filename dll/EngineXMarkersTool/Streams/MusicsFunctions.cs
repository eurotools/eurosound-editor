using EngineXMarkersTool.Classes;
using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace EngineXMarkersTool
{
    internal class MusicsFunctions
    {
        internal void CreateMarkerBinFile(string AdpcmFileL, string AdpcmFileR, string MarkerFilePath, string jumpFilePath, string smfFilePath, string outputPlatform, uint volume)
        {
            //List to store the text file markers
            List<EXStartMarker> startMarkersList = new List<EXStartMarker>();
            List<EXMarker> markersList = new List<EXMarker>();

            //Read Markers File
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();
            streamMarkersFunctions.LoadFile(MarkerFilePath, startMarkersList, markersList, jumpFilePath, true);

            //Calculate states -- PC & GameCube Platform
            if (outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
            {
                //Update positions Start Markers
                foreach (EXStartMarker startMarker in startMarkersList)
                {
                    //Calculate VAG offsets
                    if (startMarker.Position > 0)
                    {
                        startMarker.Position = UtilsFunctions.GetMusicLoopOffsetPCandGC(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = UtilsFunctions.GetMusicLoopOffsetPCandGC(startMarker.LoopStart);
                    }
                }

                //Update positions Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = UtilsFunctions.GetMusicLoopOffsetPCandGC(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = UtilsFunctions.GetMusicLoopOffsetPCandGC(marker.LoopStart);
                    }
                }

                //Read IMA Data LeftChannel
                if (File.Exists(AdpcmFileL) && File.Exists(AdpcmFileR))
                {
                    byte[][] imaData = new byte[2][];
                    imaData[0] = File.ReadAllBytes(AdpcmFileL);
                    imaData[1] = File.ReadAllBytes(AdpcmFileR);

                    //Get IMA Adpcm States
                    uint[][] pcImaDecodedStates = new uint[2][];
                    pcImaDecodedStates[0] = new uint[imaData[0].Length * 2];
                    pcImaDecodedStates[1] = new uint[imaData[1].Length * 2];
                    UtilsFunctions.ImaAdpcmState states = new UtilsFunctions.ImaAdpcmState();
                    UtilsFunctions.DecodeStatesIma(ref states, imaData[0], imaData[0].Length * 2, pcImaDecodedStates[0]);
                    states = new UtilsFunctions.ImaAdpcmState();
                    UtilsFunctions.DecodeStatesIma(ref states, imaData[1], imaData[1].Length * 2, pcImaDecodedStates[1]);

                    //Update Markers states
                    EXMarkersFunctions markersFunctions = new EXMarkersFunctions();
                    foreach (EXMarker marker in markersList)
                    {
                        if (marker.Position > 0)
                        {
                            //Update Ima States
                            foreach (EXStartMarker startMarker in startMarkersList)
                            {
                                if (startMarker.Index == marker.MarkerCount)
                                {
                                    uint[] IMA_States = markersFunctions.GetEngineXMarkerStates_Stereo(pcImaDecodedStates[0], pcImaDecodedStates[1], (int)marker.Position);
                                    startMarker.State[0] = IMA_States[0];
                                    startMarker.State[1] = IMA_States[1];
                                    break;
                                }
                            }
                        }
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
                        startMarker.Position = UtilsFunctions.GetMusicLoopOffsetPlayStation2(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = UtilsFunctions.GetMusicLoopOffsetPlayStation2(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = UtilsFunctions.GetMusicLoopOffsetPlayStation2(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = UtilsFunctions.GetMusicLoopOffsetPlayStation2(marker.LoopStart);
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
                        startMarker.Position = UtilsFunctions.GetMusicLoopOffsetXbox(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = UtilsFunctions.GetMusicLoopOffsetXbox(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = UtilsFunctions.GetMusicLoopOffsetXbox(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = UtilsFunctions.GetMusicLoopOffsetXbox(marker.LoopStart);
                    }
                }
            }

            //Write Sound Marker File
            FileWriters.WriteBinaryMarkerFile(smfFilePath, startMarkersList, markersList, volume);
        }
    }
}
