using System.Collections.Generic;
using System.IO;

namespace EngineXMarkersTool
{
    public class ExMarkersTool
    {
        //*===============================================================================================
        //* MAIN METHOD
        //*===============================================================================================
        public void CreateStreamMarkers(string AdpcmFile, string MarkersFilePath, string OutputFolder, string OutputPlatform, uint Volume)
        {
            if (File.Exists(MarkersFilePath) && Directory.Exists(OutputFolder))
            {
                StreamFunctions streamsLib = new StreamFunctions();
                streamsLib.CreateMarkerBinFile(AdpcmFile, MarkersFilePath, OutputFolder, OutputPlatform, Volume);
            }
        }

        public void CreateMusicMarkers(string AdpcmFileL, string AdpcmFileR, string MarkerFilePath, string outJumpFilePath, string soundMarkerFile, string OutputPlatform, uint Volume)
        {
            if (File.Exists(MarkerFilePath))
            {
                MusicsFunctions musicsLib = new MusicsFunctions();
                musicsLib.CreateMarkerBinFile(AdpcmFileL, AdpcmFileR, MarkerFilePath, outJumpFilePath, soundMarkerFile, OutputPlatform, Volume);
            }
        }

        public List<string> GetJumpMakersList(string MarkerFilesDir)
        {
            List<string> availableJumpMarkers = new List<string>();

            //Read Markers File
            MarkerFilesFunctions streamMarkersFunctions = new MarkerFilesFunctions();
            string[] filesList = Directory.GetFiles(MarkerFilesDir, "*.mrk", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < filesList.Length; i++)
            {
                List<MarkerInfo> fileData = streamMarkersFunctions.LoadFile(filesList[i], null, null, null);

                //Add markers to list
                for (int j = 0; j < fileData.Count; j++)
                {
                    string markerName = fileData[j].Name;
                    if (!markerName.Equals("*"))
                    {
                        availableJumpMarkers.Add("JMP_" + markerName);
                    }
                }
            }
            return availableJumpMarkers;
        }
    }
}
