using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                            midiFileData.Division = BinaryFunctions.FlipShort(reader.ReadInt16(), true);

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
                                            eventTimeMs = (float)Math.Round(deltaTime * (60000.0f / (midiFileData.BPM * midiFileData.Division)));
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
                                            eventTimeMs = (float)Math.Round(accumulatedTimeMs + (deltaTime * (60000.0f / (midiFileData.BPM * midiFileData.Division))));

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
                    writter.WriteLine("  unit {0} // is {1}/{2}", midiFileData.Division, midiFileData.TimeSignature.Numerator, midiFileData.TimeSignature.Denominator);
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
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
