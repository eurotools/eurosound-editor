namespace EngineXMarkersTool.Objects
{
    public class EXStartMarker : EXMarker
    {
        public int MarkerPosition = 0;
        public bool IsInstant = false;
        public bool InstantBuffer = false;
        public uint[] State = new uint[2];

        public EXStartMarker(EXMarker markerInfo, int markerPosition)
        {
            Index = markerInfo.Index;
            Position = markerInfo.Position;
            Type = markerInfo.Type;
            Flags = markerInfo.Flags;
            Extra = markerInfo.Extra;
            LoopStart = markerInfo.LoopStart;
            LoopMarkerIndex = markerInfo.LoopMarkerIndex;
            MarkerCount = markerInfo.MarkerCount;
            MarkerPosition = markerPosition;
        }
    }
}
