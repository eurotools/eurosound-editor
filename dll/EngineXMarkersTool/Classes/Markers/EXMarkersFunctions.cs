using EngineXMarkersTool.Objects;
using System;
using System.Collections.Generic;

namespace EngineXMarkersTool.Classes
{
    class EXMarkersFunctions
    {
        //*===============================================================================================
        //* MARKERS CREATION FUNCTIONS
        //*===============================================================================================
        internal void AddMarker(uint loopStart, uint loopEndPos, Enumerations.EXMarkerType markerType, byte flags, byte extra, List<EXStartMarker> startMarkersList, List<EXMarker> markersList, bool MusicFile = false)
        {
            EXMarker marker;
            EXStartMarker startMarker;
            switch (markerType)
            {
                case Enumerations.EXMarkerType.Start:
                    //Create marker object and add it to list
                    marker = new EXMarker()
                    {
                        Index = startMarkersList.Count,
                        Position = loopEndPos,
                        LoopStart = loopStart,
                        Type = (byte)Enumerations.EXMarkerType.Start,
                        Flags = flags,
                        Extra = extra,
                        MarkerCount = startMarkersList.Count
                    };
                    markersList.Add(marker);

                    //Create start marker object and add it to list
                    startMarker = new EXStartMarker(marker, markersList.Count - 1);
                    startMarkersList.Add(startMarker);
                    break;
                case Enumerations.EXMarkerType.End:
                    /*NOTE: when we are creating a SFX that contains a music, the Index must be replaced by the last start marker index*/
                    if (MusicFile)
                    {
                        marker = new EXMarker()
                        {
                            Index = GetLastStartMarkerIndex(startMarkersList),
                            Position = loopEndPos,
                            LoopStart = loopStart,
                            Type = (byte)Enumerations.EXMarkerType.End,
                            Flags = flags,
                            Extra = extra,
                            MarkerCount = startMarkersList.Count
                        };
                        markersList.Add(marker);
                    }
                    else
                    {
                        marker = new EXMarker()
                        {
                            Index = -1,
                            Position = loopEndPos,
                            LoopStart = loopStart,
                            Type = (byte)Enumerations.EXMarkerType.End,
                            Flags = flags,
                            Extra = extra,
                            MarkerCount = startMarkersList.Count
                        };
                        markersList.Add(marker);
                    }
                    break;
                case Enumerations.EXMarkerType.Jump:
                    marker = new EXMarker()
                    {
                        Index = -1,
                        Position = loopEndPos,
                        LoopStart = loopStart,
                        Type = (byte)Enumerations.EXMarkerType.Jump,
                        Flags = flags,
                        Extra = extra,
                        MarkerCount = startMarkersList.Count
                    };
                    markersList.Add(marker);
                    break;
                case Enumerations.EXMarkerType.Loop:
                    //Create start loop marker and add it to list
                    EXMarker markerStartLoop = new EXMarker()
                    {
                        Index = startMarkersList.Count,
                        Position = loopStart,
                        Type = (int)Enumerations.EXMarkerType.Start,
                        Flags = flags,
                        Extra = extra,
                        MarkerCount = startMarkersList.Count
                    };
                    markersList.Add(markerStartLoop);

                    //Create start loop start marker and add it to list
                    EXStartMarker startMarkerStartLoop = new EXStartMarker(markerStartLoop, markersList.Count - 1);
                    startMarkersList.Add(startMarkerStartLoop);

                    //Create loop marker and add it to list
                    EXMarker markerLoop = new EXMarker()
                    {
                        Index = GetLastStartMarkerIndex(startMarkersList),
                        Position = loopEndPos,
                        LoopStart = loopStart,
                        Type = (int)Enumerations.EXMarkerType.Loop,
                        Flags = flags,
                        Extra = extra,
                        MarkerCount = startMarkersList.Count,
                        LoopMarkerIndex = GetLastStartMarkerIndex(startMarkersList),
                    };
                    markersList.Add(markerLoop);

                    //Create end loop marker and add it to list
                    EXMarker markerEndLoop = new EXMarker()
                    {
                        Index = startMarkersList.Count,
                        Position = loopEndPos,
                        Type = (int)Enumerations.EXMarkerType.Start,
                        Flags = flags,
                        Extra = extra,
                        MarkerCount = startMarkersList.Count
                    };
                    markersList.Add(markerEndLoop);

                    //Create end loop start marker and add it to list
                    EXStartMarker startMarkerEndLoop = new EXStartMarker(markerEndLoop, markersList.Count - 1);
                    startMarkersList.Add(startMarkerEndLoop);
                    break;
                case Enumerations.EXMarkerType.Goto:
                    //Create object and add it to list
                    marker = new EXMarker()
                    {
                        Index = startMarkersList.Count,
                        Position = loopEndPos,
                        LoopStart = loopStart,
                        Type = (int)Enumerations.EXMarkerType.Goto,
                        Flags = flags,
                        Extra = extra,
                        MarkerCount = startMarkersList.Count,
                        LoopMarkerIndex = GetGotoLoopIndex(markersList, loopStart),
                    };
                    markersList.Add(marker);

                    //Create object and add it to list
                    startMarker = new EXStartMarker(marker, markersList.Count - 1);
                    startMarkersList.Add(startMarker);
                    break;
            }
        }

        //*===============================================================================================
        //* OTHER FUNCTIONS
        //*===============================================================================================
        private int GetGotoLoopIndex(List<EXMarker> markersList, uint LoopPos)
        {
            int prevValue = -1;
            foreach (EXMarker marker in markersList)
            {
                if (marker.Position == LoopPos)
                {
                    prevValue = marker.Index;
                    break;
                }
            }
            return prevValue;
        }

        internal int GetLastStartMarkerIndex(List<EXStartMarker> startMarkers)
        {
            int prevValue = 0;
            foreach (EXStartMarker startMarker in startMarkers)
            {
                prevValue = Math.Max(prevValue, startMarker.Index);
            }
            return prevValue;
        }
    }
}
