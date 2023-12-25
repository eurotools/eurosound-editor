using ESUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ExMarkers.Enumerations;

namespace ExMarkers
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class StreamMarkerFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public void BuildBinaryFile(MarkerTextFile[] MarkersData, uint baseVolume, string outputFilePath, bool isBigEndian)
        {
            //Write binary file
            using (BinaryWriter BWriter = new BinaryWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.ReadWrite), Encoding.ASCII))
            {
                //Start marker count
                BWriter.Write(0);
                //Marker count
                BWriter.Write(0);
                //Start marker offset
                BWriter.Write(0);
                //Marker offset
                BWriter.Write(0);
                //Base volume
                BWriter.Write(BytesFunctions.FlipUInt32(baseVolume, isBigEndian));

                //Local vars
                int startMarkersCount = 0;
                int markersCount = 0;

                //Start Markers Data
                int MarkerPosition = 0;
                long startMarkersOffset = BWriter.BaseStream.Position;
                for (int i = 0; i < MarkersData.Length; i++)
                {
                    if (MarkersData[i].Type == (int)EXMarkerType.End)
                    {
                        break;
                    }
                    else
                    {
                        startMarkersCount++;
                        BWriter.Write(BytesFunctions.FlipInt32(i, isBigEndian)); //Name
                        BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[i].Position, isBigEndian)); //Position
                        if (MarkersData[i].Type == (int)EXMarkerType.Goto)
                        {
                            BWriter.Write(BytesFunctions.FlipInt32((int)EXMarkerType.Goto, isBigEndian)); //Type
                        }
                        else
                        {
                            BWriter.Write(BytesFunctions.FlipInt32((int)EXMarkerType.Start, isBigEndian)); //Type
                        }
                        BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Flags, isBigEndian)); //Flags
                        BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Extra, isBigEndian)); //Extra
                        if (MarkersData[i].Type == (int)EXMarkerType.Goto)
                        {
                            for (int j = 0; j < MarkersData.Length; j++)
                            {
                                if (MarkersData[j].Name.Equals(MarkersData[i].Name.Replace("GOTO_", ""), System.StringComparison.OrdinalIgnoreCase))
                                {
                                    BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[j].Position, isBigEndian)); //Loop Start
                                    break;
                                }
                            }
                        }
                        else
                        {
                            BWriter.Write(BytesFunctions.FlipInt32(0, isBigEndian)); //Loop Start
                        }
                        BWriter.Write(BytesFunctions.FlipInt32(i, isBigEndian)); //Marker Count
                        if (MarkersData[i].Type == (int)EXMarkerType.Goto)
                        {
                            BWriter.Write(BytesFunctions.FlipInt32(1, isBigEndian)); //Loop Marker Count
                        }
                        else
                        {
                            BWriter.Write(BytesFunctions.FlipInt32(0, isBigEndian)); //Loop Marker Count
                        }
                        BWriter.Write(BytesFunctions.FlipInt32(MarkerPosition, isBigEndian)); //Marker Position
                        BWriter.Write(BytesFunctions.FlipInt32(0, isBigEndian)); //Is Instant
                        BWriter.Write(BytesFunctions.FlipInt32(0, isBigEndian)); //Instant Buffer
                        BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[i].ImaStateA, isBigEndian)); //State A
                        BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[i].ImaStateB, isBigEndian)); //State B

                        //Update counter var
                        MarkerPosition++;
                        if (MarkersData[i].Type == (int)EXMarkerType.Loop)
                        {
                            MarkerPosition++;
                        }
                    }
                }

                //Markers Data
                long markersOffset = BWriter.BaseStream.Position;
                for (int i = 0; i < MarkersData.Length; i++)
                {
                    markersCount++;
                    if (MarkersData[i].Type == (int)EXMarkerType.End)
                    {
                        if (MarkersData[i].Name.Equals("*"))
                        {
                            BWriter.Write(BytesFunctions.FlipInt32(i - 1, isBigEndian)); //Name
                        }
                        else
                        {
                            BWriter.Write(BytesFunctions.FlipInt32(-1, isBigEndian)); //Name
                        }
                    }
                    else
                    {
                        BWriter.Write(BytesFunctions.FlipInt32(i, isBigEndian)); //Name
                    }
                    BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[i].Position, isBigEndian)); //Position
                    if (MarkersData[i].Type == (int)EXMarkerType.Loop)
                    {
                        BWriter.Write(BytesFunctions.FlipInt32(10, isBigEndian)); //Type
                    }
                    else
                    {
                        BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Type, isBigEndian)); //Type
                    }
                    BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Flags, isBigEndian)); //Flags
                    BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Extra, isBigEndian)); //Extra
                    if (MarkersData[i].Type == (int)EXMarkerType.Goto)
                    {
                        for (int j = 0; j < MarkersData.Length; j++)
                        {
                            if (MarkersData[j].Name.Equals(MarkersData[i].Name.Replace("GOTO_", ""), System.StringComparison.OrdinalIgnoreCase))
                            {
                                BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[j].Position, isBigEndian)); //Loop Start
                                break;
                            }
                        }
                    }
                    else
                    {
                        BWriter.Write(BytesFunctions.FlipInt32(0, isBigEndian)); //Loop Start
                    }
                    BWriter.Write(BytesFunctions.FlipInt32(i, isBigEndian)); //Marker Count
                    if (MarkersData[i].Type == (int)EXMarkerType.Goto)
                    {
                        BWriter.Write(BytesFunctions.FlipInt32(1, isBigEndian)); //Loop Marker Count
                    }
                    else
                    {
                        BWriter.Write(BytesFunctions.FlipInt32(0, isBigEndian)); //Loop Marker Count
                    }

                    //Add extra marker
                    if (MarkersData[i].Type == (int)EXMarkerType.Loop)
                    {
                        markersCount++;
                        BWriter.Write(BytesFunctions.FlipInt32(i, isBigEndian)); //Name
                        BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[i + 1].Position, isBigEndian)); //Position
                        BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Type, isBigEndian)); //Type
                        BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Flags, isBigEndian)); //Flags
                        BWriter.Write(BytesFunctions.FlipInt32(MarkersData[i].Extra, isBigEndian)); //Extra
                        BWriter.Write(BytesFunctions.FlipUInt32(MarkersData[i].Position, isBigEndian)); //Loop Start
                        BWriter.Write(BytesFunctions.FlipInt32(i + 1, isBigEndian)); //Marker Count
                        BWriter.Write(BytesFunctions.FlipInt32(1, isBigEndian)); //Loop Marker Count
                    }
                }

                //Write start offsets
                BWriter.BaseStream.Seek(0, SeekOrigin.Begin);
                BWriter.Write(BytesFunctions.FlipUInt32((uint)startMarkersCount, isBigEndian));
                BWriter.Write(BytesFunctions.FlipUInt32((uint)markersCount, isBigEndian));
                BWriter.Write(BytesFunctions.FlipUInt32((uint)startMarkersOffset, isBigEndian));
                BWriter.Write(BytesFunctions.FlipUInt32((uint)markersOffset, isBigEndian));

                //Close file
                BWriter.Close();
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
