using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using static ESUtils.CalculusLoopOffset;

namespace EngineXMarkersTool
{
    internal class MusicsFunctions
    {
        internal void CreateMarkerBinFile(string smdFilePathL, string smdFilePathR, string MarkerFilePath, string jumpFilePath, string smfFilePath, string outputPlatform, uint volume)
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
                        startMarker.Position = GetMusicLoopOffsetPCandGC(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = GetMusicLoopOffsetPCandGC(startMarker.LoopStart);
                    }
                }

                //Update positions Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = GetMusicLoopOffsetPCandGC(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = GetMusicLoopOffsetPCandGC(marker.LoopStart);
                    }
                }

                //Read IMA ADPCM States
                if (File.Exists(smdFilePathL) && File.Exists(smdFilePathR))
                {
                    //Update Markers states
                    foreach (EXMarker marker in markersList)
                    {
                        if (marker.Position > 0)
                        {
                            //Update Ima States
                            foreach (EXStartMarker startMarker in startMarkersList)
                            {
                                if (startMarker.Index == marker.MarkerCount)
                                {
                                    uint state = 0;
                                    using (BinaryReader breader = new BinaryReader(File.Open(smdFilePathL, FileMode.Open, FileAccess.Read, FileShare.Read)))
                                    {
                                        long offset = ((marker.Position / 256) * 256) / 2;
                                        if (offset <= breader.BaseStream.Length)
                                        {
                                            breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                            state = breader.ReadUInt32();
                                        }
                                        startMarker.State[0] = state;
                                    }

                                    using (BinaryReader breader = new BinaryReader(File.Open(smdFilePathR, FileMode.Open, FileAccess.Read, FileShare.Read)))
                                    {
                                        long offset = ((marker.Position / 256) * 256) / 2;
                                        if (offset <= breader.BaseStream.Length)
                                        {
                                            breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                            state = breader.ReadUInt32();
                                        }
                                        startMarker.State[1] = state;
                                    }
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
                        startMarker.Position = GetMusicLoopOffsetPlayStation2(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = GetMusicLoopOffsetPlayStation2(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = GetMusicLoopOffsetPlayStation2(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = GetMusicLoopOffsetPlayStation2(marker.LoopStart);
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
                        startMarker.Position = GetMusicLoopOffsetXbox(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = GetMusicLoopOffsetXbox(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = GetMusicLoopOffsetXbox(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = GetMusicLoopOffsetXbox(marker.LoopStart);
                    }
                }
            }

            //Write Sound Marker File
            FileWriters.WriteBinaryMarkerFile(smfFilePath, startMarkersList, markersList, volume, outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase));
        }
    }
}
