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
        public void CreateMarkerBinFile(string markerFilePath, string outputFilePath, string outputPlatform)
        {
            //List to store the text file markers
            List<EXStartMarker> startMarkersList = new List<EXStartMarker>();
            List<EXMarker> markersList = new List<EXMarker>();
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();

            //Read Markers File
            streamMarkersFunctions.LoadTextMarkerFile(markerFilePath, startMarkersList, markersList);

            //Start markers
            foreach (EXStartMarker startMarker in startMarkersList)
            {
                //Calculate offsets
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

            //Write Sound Marker File
            streamMarkersFunctions.WriteBinaryMarkerFile(outputFilePath, startMarkersList, markersList, 100, outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase));
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
