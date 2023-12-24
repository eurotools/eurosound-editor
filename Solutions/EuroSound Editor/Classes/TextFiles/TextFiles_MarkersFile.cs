using ExMarkers;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static MarkerTextFile[] ReadMarkerFile(string filePath)
        {
            List<MarkerTextFile> fileMarkers = new List<MarkerTextFile>();

            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine().Trim();
                    //Skip empty or commented lines
                    if (string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//"))
                    {
                        continue;
                    }

                    //Main Block
                    if (currentLine.Equals("Markers", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("}", StringComparison.OrdinalIgnoreCase))
                        {
                            currentLine = sr.ReadLine().Trim();
                            if (currentLine.Contains("Marker"))
                            {
                                fileMarkers.Add(ReadMarkerBlock(sr));
                            }
                        }
                    }
                }
            }

            return fileMarkers.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static MarkerTextFile ReadMarkerBlock(StreamReader sr)
        {
            MarkerTextFile markerData = new MarkerTextFile();

            //Read Marker block
            string currentLine = sr.ReadLine().Trim();

            //Read marker properties
            while (!currentLine.Equals("}", StringComparison.OrdinalIgnoreCase))
            {
                currentLine = sr.ReadLine().Trim();
                string[] lineData = currentLine.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                switch (lineData[0].ToUpper())
                {
                    case "NAME":
                        markerData.Name = lineData[1].Trim();
                        break;
                    case "POS":
                        markerData.Position = uint.Parse(lineData[1].Trim());
                        break;
                    case "TYPE":
                        markerData.Type = int.Parse(lineData[1].Trim());
                        break;
                    case "FLAGS":
                        markerData.Flags = int.Parse(lineData[1].Trim());
                        break;
                    case "EXTRA":
                        markerData.Extra = int.Parse(lineData[1].Trim());
                        break;
                }
            }

            return markerData;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
