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
        public void CreateMarkerFile(string markerFilePath, uint volume, string outputPlatform, string outputPath)
        {
            //List to store the text file markers
            List<EXStartMarker> startMarkersList = new List<EXStartMarker>();
            List<EXMarker> markersList = new List<EXMarker>();

            //Read Markers File
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();
            streamMarkersFunctions.LoadFile(markerFilePath, startMarkersList, markersList, true);

            //Calculate states -- PC & GameCube Platform
            if (outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase)|| outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
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
