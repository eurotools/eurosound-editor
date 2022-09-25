using ESUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExMarkers
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class MusicMarkerFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public int CreateJumpMarker(string markerFilePath, string outputFilePath)
        {
            //Read Markers File
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();
            List<MarkerInfo> fileData = streamMarkersFunctions.LoadFile(markerFilePath, null, null, true);

            //Write Jump Markers
            using (StreamWriter sw = new StreamWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("#JUMPMARKERS");
                for (int i = 0; i < fileData.Count - 1; i++)
                {
                    sw.WriteLine(fileData[i].Name);
                }
                sw.WriteLine("#END");
            }

            return fileData.Count - 1;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void CreateMarkerFile(string imaFileLeft, string imaFileRight, string markerFilePath, uint volume, string outputPlatform, string outputPath)
        {
            //List to store the text file markers
            List<EXStartMarker> startMarkersList = new List<EXStartMarker>();
            List<EXMarker> markersList = new List<EXMarker>();

            //Read Markers File
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();
            streamMarkersFunctions.LoadFile(markerFilePath, startMarkersList, markersList, true);

            //Calculate states -- PC & GameCube Platform
            if (outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
            {
                //Update positions Start Markers
                foreach (EXStartMarker startMarker in startMarkersList)
                {
                    //Calculate offsets for IMA Adpcm
                    if (startMarker.Position > 0)
                    {
                        startMarker.Position = CalculusLoopOffset.GetMusicLoopOffsetPCandGC(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = CalculusLoopOffset.GetMusicLoopOffsetPCandGC(startMarker.LoopStart);
                    }
                }

                //Update positions Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetMusicLoopOffsetPCandGC(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = CalculusLoopOffset.GetMusicLoopOffsetPCandGC(marker.LoopStart);
                    }
                }

                //Update STATES
                if (File.Exists(imaFileLeft) && File.Exists(imaFileRight))
                {
                    List<string> stl = new List<string>();
                    List<string> str = new List<string>();

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
                                    using (BinaryReader breader = new BinaryReader(File.Open(imaFileLeft, FileMode.Open, FileAccess.Read, FileShare.Read)))
                                    {
                                        long offset = ((marker.Position / 256) * 256) / 2;
                                        if (offset <= breader.BaseStream.Length)
                                        {
                                            breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                            state = breader.ReadUInt32();
                                        }
                                        startMarker.State[0] = state;

                                        //Add items to list
                                        stl.Add(state.ToString());
                                        stl.Add(startMarker.Position.ToString());
                                    }
                                    using (BinaryReader breader = new BinaryReader(File.Open(imaFileRight, FileMode.Open, FileAccess.Read, FileShare.Read)))
                                    {
                                        long offset = ((marker.Position / 256) * 256) / 2;
                                        if (offset <= breader.BaseStream.Length)
                                        {
                                            breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                            state = breader.ReadUInt32();
                                        }
                                        startMarker.State[1] = state;

                                        //Add items to list
                                        str.Add(state.ToString());
                                        str.Add(startMarker.Position.ToString());
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    //Write file
                    File.WriteAllLines(Path.Combine(Path.ChangeExtension(imaFileLeft, ".str")), str);
                    File.WriteAllLines(Path.Combine(Path.ChangeExtension(imaFileLeft, ".stl")), stl);
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
                        startMarker.Position = CalculusLoopOffset.GetMusicLoopOffsetPlayStation2(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = CalculusLoopOffset.GetMusicLoopOffsetPlayStation2(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetMusicLoopOffsetPlayStation2(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = CalculusLoopOffset.GetMusicLoopOffsetPlayStation2(marker.LoopStart);
                    }
                }
            }

            //Update positions for Xbox
            if (outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (EXStartMarker startMarker in startMarkersList)
                {
                    //Calculate Xbox Adpcm offsets
                    if (startMarker.Position > 0)
                    {
                        startMarker.Position = CalculusLoopOffset.GetMusicLoopOffsetXbox(startMarker.Position);
                    }
                    if (startMarker.LoopStart > 0)
                    {
                        startMarker.LoopStart = CalculusLoopOffset.GetMusicLoopOffsetXbox(startMarker.LoopStart);
                    }
                }

                //Markers
                foreach (EXMarker marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetMusicLoopOffsetXbox(marker.Position);
                    }
                    if (marker.LoopStart > 0)
                    {
                        marker.LoopStart = CalculusLoopOffset.GetMusicLoopOffsetXbox(marker.LoopStart);
                    }
                }
            }

            //Write Sound Marker File
            streamMarkersFunctions.WriteBinaryMarkerFile(outputPath, startMarkersList, markersList, volume, outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase));
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
