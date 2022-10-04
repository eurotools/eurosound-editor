using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.IO;

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
                    foreach (TimedEvent eventToCheck in fileEvents)
                    {
                        MetricTimeSpan eventStartTime = eventToCheck.TimeAs<MetricTimeSpan>(tempoMap);
                        if (eventToCheck.Event.GetType() == typeof(NoteOnEvent))
                        {
                            NoteOnEvent noteOnEvent = ((NoteOnEvent)eventToCheck.Event);
                            string noteName = string.Format("{0}{1}", noteOnEvent.GetNoteName(), noteOnEvent.GetNoteOctave()).ToLower();
                            writter.WriteLine("{0}; /* {1}ms */ +{2}", noteOnEvent.DeltaTime, (int)eventStartTime.TotalMilliseconds, noteName);
                        }
                        else if (eventToCheck.Event.GetType() == typeof(TextEvent))
                        {
                            TextEvent textEvent = ((TextEvent)eventToCheck.Event);
                            writter.WriteLine("/* {0}ms */   text \"{1}\"", (int)eventStartTime.TotalMilliseconds, textEvent.Text);
                        }
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
