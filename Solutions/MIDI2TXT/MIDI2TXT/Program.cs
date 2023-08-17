using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MIDI2TXT
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class Program
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private static readonly Dictionary<int, string> csharpMidiDict = new Dictionary<int, string>()
        {
            { 0, "c-1" }, { 1, "d-1" }, { 2, "d#-1" }, { 3, "e-1" }, { 4, "f-1" }, { 5, "f-1" }, { 6, "g-1" }, { 7, "g-1" }, { 8, "a-1" }, { 9, "a-1" }, { 10, "b-1" },
            { 11, "c0" }, { 12, "c0" }, { 13, "d0" }, { 14, "d#0" }, { 15, "e0" }, { 16, "f0" }, { 17, "f0" }, { 18, "g0" }, { 19, "g0" }, { 20, "a0" }, { 21, "a0" }, { 22, "b0" },
            { 23, "c1" }, { 24, "c1" }, { 25, "d1" }, { 26, "d#1" }, { 27, "e1" }, { 28, "f1" }, { 29, "f1" }, { 30, "g1" }, { 31, "g1" }, { 32, "a1" }, { 33, "a1" }, { 34, "b1" },
            { 35, "c2" }, { 36, "c2" }, { 37, "d2" }, { 38, "d#2" }, { 39, "e2" }, { 40, "f2" }, { 41, "f2" }, { 42, "g2" }, { 43, "g2" }, { 44, "a2" }, { 45, "a2" }, { 46, "b2" },
            { 47, "c3" }, { 48, "c3" }, { 49, "d3" }, { 50, "d#3" }, { 51, "e3" }, { 52, "f3" }, { 53, "f3" }, { 54, "g3" }, { 55, "g3" }, { 56, "a3" }, { 57, "a3" }, { 58, "b3" },
            { 59, "c4" }, { 60, "c4" }, { 61, "d4" }, { 62, "d#4" }, { 63, "e4" }, { 64, "f4" }, { 65, "f4" }, { 66, "g4" }, { 67, "g4" }, { 68, "a4" }, { 69, "a4" }, { 70, "b4" },
            { 71, "c5" }, { 72, "c5" }, { 73, "d5" }, { 74, "d#5" }, { 75, "e5" }, { 76, "f5" }, { 77, "f5" }, { 78, "g5" }, { 79, "g5" }, { 80, "a5" }, { 81, "a5" }, { 82, "b5" },
            { 83, "c6" }, { 84, "c6" }, { 85, "d6" }, { 86, "d#6" }, { 87, "e6" }, { 88, "f6" }, { 89, "f6" }, { 90, "g6" }, { 91, "g6" }, { 92, "a6" }, { 93, "a6" }, { 94, "b6" },
            { 95, "c7" }, { 96, "c7" }, { 97, "d7" }, { 98, "d#7" }, { 99, "e7" }, { 100, "f7" }, { 101, "f7" }, { 102, "g7" }, { 103, "g7" }, { 104, "a7" }, { 105, "a7" }, { 106, "b7" },
            { 107, "c8" }, { 108, "c8" }, { 109, "d8" }, { 110, "d#8" }, { 111, "e8" }, { 112, "f8" }, { 113, "f8" }, { 114, "g8" }, { 115, "g8" }, { 116, "a8" }, { 117, "a8" }, { 118, "b8" },
            { 119, "c9" }, { 120, "c9" }, { 121, "d9" }, { 122, "d#9" }, { 123, "e9" }, { 124, "f9" }, { 125, "f9" }, { 126, "g9" }
        };

        //-------------------------------------------------------------------------------------------------------------------------------
        private static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                //Start Reading file
                float accumulatedTimeMs = 0;
                MidiFile midiFileData = new MidiFile();

                using (BinaryReader reader = new BinaryReader(new FileStream(args[1], FileMode.Open)))
                {
                    // Leer la cabecera del archivo MIDI
                    string Magic = Encoding.ASCII.GetString(reader.ReadBytes(4));
                    if (Magic.Equals("MThd", StringComparison.OrdinalIgnoreCase))
                    {
                        // Leer la longitud del header (siempre debe ser 6)
                        midiFileData.HeaderLength = BinaryFunctions.FlipInt32(reader.ReadInt32(), true);
                        midiFileData.FormatType = BinaryFunctions.FlipShort(reader.ReadInt16(), true);
                        midiFileData.NumTracks = BinaryFunctions.FlipShort(reader.ReadInt16(), true);
                        midiFileData.PulsesPerQuarterNote = BinaryFunctions.FlipShort(reader.ReadInt16(), true);

                        // Variable para llevar el tiempo acumulado en milisegundos
                        float eventTimeMs = 0;

                        // Iterar a través de las pistas
                        for (int trackIndex = 0; trackIndex < midiFileData.NumTracks; trackIndex++)
                        {
                            string trackMagic = Encoding.ASCII.GetString(reader.ReadBytes(4));
                            if (trackMagic.Equals("MTrk", StringComparison.OrdinalIgnoreCase))
                            {
                                long trackLength = BinaryFunctions.FlipInt32(reader.ReadInt32(), true) + reader.BaseStream.Position;

                                // Leer eventos en la pista
                                while (reader.BaseStream.Position < trackLength)
                                {
                                    int deltaTime = ReadVariableLengthValue(reader);
                                    byte eventType = reader.ReadByte();

                                    if (eventType == 0xFF) // Meta-event
                                    {
                                        byte metaType = reader.ReadByte();
                                        int dataLength = ReadVariableLengthValue(reader);

                                        if (metaType == 0x01) // Text event
                                        {
                                            byte[] textBytes = reader.ReadBytes(dataLength);
                                            string text = Encoding.UTF8.GetString(textBytes);

                                            midiFileData.Events.Add(string.Format(" /* {0}ms */   text \"{1}\"", eventTimeMs, text));
                                        }
                                        else if (metaType == 0x51) // Tempo Event
                                        {
                                            byte[] tempoBytes = reader.ReadBytes(3);
                                            uint tempoValue = (uint)((tempoBytes[0] << 16) | (tempoBytes[1] << 8) | tempoBytes[2]);
                                            midiFileData.TempoPerQuarterNote = tempoValue;
                                            midiFileData.BPM = 60000000.0f / tempoValue;
                                        }
                                        else if (metaType == 0x58) // Time Signature event
                                        {
                                            midiFileData.TimeSignature.DeltaTime = deltaTime;
                                            midiFileData.TimeSignature.Numerator = reader.ReadByte();
                                            midiFileData.TimeSignature.Denominator = (int)Math.Pow(2, reader.ReadByte());
                                            midiFileData.TimeSignature.ClocksPerMetronomeClick = reader.ReadByte();
                                            midiFileData.TimeSignature.ThirtySecondNotesPerBeat = reader.ReadByte();
                                        }
                                        else
                                        {
                                            // Otras manipulaciones de meta-eventos si es necesario
                                            reader.BaseStream.Seek(dataLength, SeekOrigin.Current);
                                        }
                                    }
                                    else if (eventType == 0x90) // Note On event
                                    {
                                        // Calcula los milisegundos para el evento actual
                                        eventTimeMs = (float)Math.Round(DeltaToMilliseconds(deltaTime, midiFileData.PulsesPerQuarterNote, midiFileData.TempoPerQuarterNote));
                                        accumulatedTimeMs = eventTimeMs;

                                        //Leer tipo de nota y número
                                        byte channelAndType = reader.ReadByte();
                                        byte noteNumber = reader.ReadByte();

                                        //Añadir evento a la lista
                                        if (deltaTime > 0)
                                        {
                                            midiFileData.Events.Add(string.Format("  {0}; /* {1}ms */ +{2} ${3:X2};", deltaTime, eventTimeMs, csharpMidiDict[channelAndType], noteNumber));
                                        }
                                        else
                                        {
                                            midiFileData.Events.Add(string.Format(" /* {0}ms */   +{1} ${2:X2};", eventTimeMs, csharpMidiDict[channelAndType], noteNumber));
                                        }
                                    }
                                    else if (eventType == 0x80) // Note Off event
                                    {
                                        // Calcula los milisegundos para el evento actual
                                        eventTimeMs = (float)Math.Round(DeltaToMilliseconds(deltaTime, midiFileData.PulsesPerQuarterNote, midiFileData.TempoPerQuarterNote));


                                        //Leer tipo de nota y número
                                        byte channelAndType = reader.ReadByte();
                                        byte noteNumber = reader.ReadByte();

                                        //Añadir evento a la lista
                                        midiFileData.Events.Add(string.Format("  {0}/{1} /* {2}ms */ -{3} ${4:X2};", midiFileData.TimeSignature.Numerator, midiFileData.TimeSignature.Denominator, eventTimeMs, csharpMidiDict[channelAndType], noteNumber));
                                        accumulatedTimeMs = eventTimeMs;
                                    }
                                }
                            }
                        }
                    }
                }


                //Write debug file
                using (StreamWriter writter = new StreamWriter(File.Open(args[2], FileMode.Create, FileAccess.Write), Encoding.UTF8))
                {
                    writter.WriteLine("// {0}", Path.GetFileName(args[1]));
                    writter.WriteLine("mthd");
                    if (midiFileData.FormatType == 0)
                    {
                        writter.WriteLine("  version {0} // {1}", midiFileData.FormatType, "single multichanneltrack");
                    }
                    else if (midiFileData.FormatType == 1)
                    {
                        writter.WriteLine("  version {0} // {1}", midiFileData.FormatType, "multiple multichanneltrack");
                    }
                    else
                    {
                        writter.WriteLine("  version {0} // {1}", midiFileData.FormatType, "pattern multichanneltrack");
                    }
                    writter.WriteLine("  // {0} track", 1);
                    writter.WriteLine("  unit {0} // is {1}/{2}", midiFileData.PulsesPerQuarterNote, midiFileData.TimeSignature.Numerator, midiFileData.TimeSignature.Denominator);
                    writter.WriteLine("end mthd");
                    writter.WriteLine(string.Empty);
                    writter.WriteLine("mtrk(1)  // track 1");
                    writter.WriteLine(" /* {0}ms */   beats {1:0.00000} /* {2} microsec/beat */", midiFileData.TimeSignature.DeltaTime, midiFileData.BPM, midiFileData.TempoPerQuarterNote);
                    writter.WriteLine(" /* {0}ms */   tact {1} / {2} {3} {4}", midiFileData.TimeSignature.DeltaTime, midiFileData.TimeSignature.Numerator, midiFileData.TimeSignature.Denominator, midiFileData.TimeSignature.ClocksPerMetronomeClick, midiFileData.TimeSignature.ThirtySecondNotesPerBeat);
                    for (int i = 0; i < midiFileData.Events.Count; i++)
                    {
                        writter.WriteLine(midiFileData.Events[i]);
                    }
                    writter.WriteLine(" /* {0}ms */", accumulatedTimeMs);
                    writter.WriteLine("end mtrk");
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static int ReadVariableLengthValue(BinaryReader reader)
        {
            int value = 0;
            byte currentByte;

            do
            {
                currentByte = reader.ReadByte();
                value = (value << 7) | (currentByte & 0x7F);
            } while ((currentByte & 0x80) != 0);

            return value;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static float DeltaToMilliseconds(int deltaTime, int Division, uint TempoPerQuarterNote)
        {
            float microSeconds_per_tick = TempoPerQuarterNote / Division;
            float seconds_per_tick = microSeconds_per_tick / 1000000.0f;
            float milliseconds = (deltaTime * seconds_per_tick) * 1000.0f;

            return milliseconds;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
