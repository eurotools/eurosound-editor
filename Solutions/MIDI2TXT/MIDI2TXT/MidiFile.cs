using System.Collections.Generic;

namespace MIDI2TXT
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class MidiFile
    {
        internal int HeaderLength;
        internal int FormatType;
        internal int NumTracks;
        internal int PulsesPerQuarterNote;
        internal uint TempoPerQuarterNote;
        internal float BPM;
        internal TimeSignatureEvent TimeSignature = new TimeSignatureEvent();
        internal List<string> Events = new List<string>();
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    internal class TimeSignatureEvent
    {
        public int DeltaTime;
        public int Numerator;
        public int Denominator;
        public int ClocksPerMetronomeClick;
        public int ThirtySecondNotesPerBeat;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
