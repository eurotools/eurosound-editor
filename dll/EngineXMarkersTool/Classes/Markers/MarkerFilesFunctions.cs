using EngineXMarkersTool.Classes;
using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace EngineXMarkersTool
{
    internal struct MarkerInfo
    {
        internal string Name;
        internal uint Position;
        internal byte Type;
        internal byte Flags;
        internal byte Extra;
    }

    internal class MarkerFilesFunctions
    {
        internal List<MarkerInfo> LoadFile(string filePath, List<EXStartMarker> startMarkersList, List<EXMarker> markersList, string jumpMarkers = "", bool IsMusic = false)
        {
            //List to store mrk file data
            List<MarkerInfo> fileData = new List<MarkerInfo>();

            //Read file
            using (FileStream markersReader = File.OpenRead(filePath))
            {
                using (StreamReader reader = new StreamReader(markersReader))
                {
                    if (reader.ReadLine().Equals("Markers", StringComparison.OrdinalIgnoreCase))
                    {
                        //Start reading
                        string CurrentLine;
                        while ((CurrentLine = reader.ReadLine()) != null)
                        {
                            //Trim string to avoid unexpected results.
                            CurrentLine = CurrentLine.Trim();
                            //We don't need to read comments, also skip empty lines.
                            if (string.IsNullOrEmpty(CurrentLine))
                            {
                                //Ignore and continue
                                continue;
                            }
                            else
                            {
                                if (CurrentLine.Equals("Marker", StringComparison.OrdinalIgnoreCase) || CurrentLine.StartsWith("Marker", StringComparison.OrdinalIgnoreCase))
                                {
                                    ReadMarkerBlock(reader, fileData);
                                }
                            }
                        }
                    }
                }
            }

            //Add items
            EXMarkersFunctions markersFunctions = new EXMarkersFunctions();
            if (startMarkersList != null && markersList != null)
            {
                //Clear lists
                startMarkersList.Clear();
                markersList.Clear();

                //Add new markers to list
                for (int i = 0; i < fileData.Count; i++)
                {
                    switch (fileData[i].Type)
                    {
                        case (byte)Enumerations.EXMarkerType.Start:
                            markersFunctions.AddMarker(0, fileData[i].Position, Enumerations.EXMarkerType.Start, fileData[i].Flags, fileData[i].Extra, startMarkersList, markersList, IsMusic);
                            break;
                        case (byte)Enumerations.EXMarkerType.End:
                            markersFunctions.AddMarker(0, fileData[i].Position, Enumerations.EXMarkerType.End, fileData[i].Flags, fileData[i].Extra, startMarkersList, markersList, IsMusic);
                            break;
                        case (byte)Enumerations.EXMarkerType.Jump:
                            markersFunctions.AddMarker(0, fileData[i].Position, Enumerations.EXMarkerType.Jump, fileData[i].Flags, fileData[i].Extra, startMarkersList, markersList, IsMusic);
                            break;
                        case (byte)Enumerations.EXMarkerType.Goto:
                            //Get start position
                            string startMarkerName = fileData[i].Name.Substring("GOTO_".Length);
                            uint startPos = 0;
                            for (int j = 0; j < fileData.Count; j++)
                            {
                                if (fileData[j].Name.Equals(startMarkerName))
                                {
                                    startPos = fileData[j].Position;
                                    break;
                                }
                            }
                            //Create Marker
                            markersFunctions.AddMarker(startPos, fileData[i].Position, Enumerations.EXMarkerType.Goto, fileData[i].Flags, fileData[i].Extra, startMarkersList, markersList, IsMusic);
                            break;
                        case (byte)Enumerations.EXMarkerType.Loop:
                            if (i + 1 < fileData.Count && fileData[i + 1].Type == (byte)Enumerations.EXMarkerType.Start)
                            {
                                markersFunctions.AddMarker(fileData[i].Position, fileData[i + 1].Position, Enumerations.EXMarkerType.Loop, fileData[i].Flags, fileData[i].Extra, startMarkersList, markersList, IsMusic);
                                i += 1;
                            }
                            break;
                    }
                }
            }

            //Create Jump Markers file
            if (!string.IsNullOrEmpty(jumpMarkers))
            {
                using (StreamWriter sw = File.CreateText(jumpMarkers))
                {
                    sw.WriteLine("#JUMPMARKERS");
                    for (int i = 0; i < fileData.Count; i++)
                    {
                        string markerName = fileData[i].Name;
                        if (!markerName.Equals("*"))
                        {
                            sw.WriteLine(markerName);
                        }
                    }
                    sw.WriteLine("#END");
                    sw.Close();
                }
            }

            return fileData;
        }

        private void ReadMarkerBlock(StreamReader reader, List<MarkerInfo> markerInfoList)
        {
            string CurrentLine;
            if (reader.ReadLine().Trim().Equals("{"))
            {
                MarkerInfo markerInfo = new MarkerInfo();
                while ((CurrentLine = reader.ReadLine()) != null)
                {
                    //Trim string to avoid unexpected results.
                    CurrentLine = CurrentLine.Trim();

                    //Exit when found the end marker
                    if (CurrentLine.Equals("}"))
                    {
                        //Add item to list
                        markerInfoList.Add(markerInfo);
                        break;
                    }
                    else
                    {
                        //Get Keyword
                        string[] lineData = CurrentLine.Split('=');
                        if (lineData.Length > 1)
                        {
                            switch (lineData[0].ToUpper())
                            {
                                case "NAME":
                                    markerInfo.Name = lineData[1];
                                    break;
                                case "POS":
                                    if (uint.TryParse(lineData[1], out uint PositionParsed))
                                    {
                                        markerInfo.Position = PositionParsed;
                                    }
                                    break;
                                case "TYPE":
                                    if (byte.TryParse(lineData[1], out byte MarkerType))
                                    {
                                        markerInfo.Type = MarkerType;
                                    }
                                    break;
                                case "FLAGS":
                                    if (byte.TryParse(lineData[1], out byte MarkerFlags))
                                    {
                                        markerInfo.Flags = MarkerFlags;
                                    }
                                    break;
                                case "EXTRA":
                                    if (byte.TryParse(lineData[1], out byte ExtraData))
                                    {
                                        markerInfo.Extra = ExtraData;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
