using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MIDI2TXT
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class Program
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                MidiFile file = MidiFile.Read(args[1]);
                TempoMap tempoMap = file.GetTempoMap();
                ICollection<TimedEvent> fileEvents = file.GetTimedEvents();

                //Read File Data
                using (StreamWriter writter = new StreamWriter(File.Open(args[2], FileMode.Create, FileAccess.Write)))
                {
                    writter.WriteLine("// xvxv.mid");
                    writter.WriteLine("mthd");
                    switch (file.OriginalFormat)
                    {
                        case MidiFileFormat.SingleTrack:
                            writter.WriteLine("  version {0} // {1}", 0, "single multichanneltrack");
                            break;
                        case MidiFileFormat.MultiTrack:
                            writter.WriteLine("  version {0} // {1}", 1, "multiple multichanneltrack");
                            break;
                        default:
                            writter.WriteLine("  version {0} // {1}", 2, "multiple multichannelsong");
                            break;
                    }
                    writter.WriteLine("  // {0} track", file.GetTrackChunks().Count());
                    int ticksPerQuarterNote = ((TicksPerQuarterNoteTimeDivision)file.TimeDivision).TicksPerQuarterNote;
                    TimeSignatureEvent[] timeSignatureTimedEvents = file.GetTrackChunks().SelectMany(chunk => chunk.Events).OfType<TimeSignatureEvent>().ToArray();
                    if (timeSignatureTimedEvents.Length > 0)
                    {
                        writter.WriteLine("  uint {0} // is {1}/{2}", ticksPerQuarterNote, timeSignatureTimedEvents[0].Numerator, timeSignatureTimedEvents[0].Denominator);
                    }
                    else
                    {
                        writter.WriteLine("  uint {0} // is 1/4", ticksPerQuarterNote);
                    }
                    writter.WriteLine("end mthd");
                    writter.WriteLine(string.Empty);
                    writter.WriteLine("mtrk(1)  // track 1");
                    foreach (TimedEvent eventToCheck in fileEvents)
                    {
                        MetricTimeSpan eventStartTime = eventToCheck.TimeAs<MetricTimeSpan>(tempoMap);
                        if (eventToCheck.Event.GetType() == typeof(SetTempoEvent))
                        {
                            SetTempoEvent tempoEvent = ((SetTempoEvent)eventToCheck.Event);
                            double beats = eventToCheck.TimeAs<MetricTimeSpan>(tempoMap).TotalMicroseconds / tempoEvent.MicrosecondsPerQuarterNote;
                            writter.WriteLine(" /* {0}ms */   beats {1} /* {2} microsec/beat */", (int)Math.Round(eventStartTime.TotalMilliseconds), beats, tempoEvent.MicrosecondsPerQuarterNote);
                        }
                        else if (eventToCheck.Event.GetType() == typeof(NoteOnEvent))
                        {
                            NoteOnEvent noteOnEvent = ((NoteOnEvent)eventToCheck.Event);
                            string noteName = string.Format("{0}{1}", noteOnEvent.GetNoteName(), noteOnEvent.GetNoteOctave()).ToLower();
                            writter.WriteLine("{0}; /* {1}ms */ +{2}", noteOnEvent.DeltaTime, (int)Math.Round(eventStartTime.TotalMilliseconds), noteName);
                        }
                        else if (eventToCheck.Event.GetType() == typeof(TextEvent))
                        {
                            TextEvent textEvent = ((TextEvent)eventToCheck.Event);
                            writter.WriteLine("/* {0}ms */   text \"{1}\"", (int)Math.Round(eventStartTime.TotalMilliseconds), textEvent.Text);
                        }
                    }
                    writter.WriteLine("end mtrk");
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
