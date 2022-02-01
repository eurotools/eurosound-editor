using System.IO;

namespace sb_explorer.Classes
{
    internal class MarkerFiles_Exporter
    {
        internal void ExportMarkers(string FilePath, EXStreamMarker[] Markers)
        {
            using (StreamWriter TextFileWriter = File.CreateText(FilePath))
            {
                TextFileWriter.WriteLine("Markers");
                TextFileWriter.WriteLine("{");

                for (int i = 0; i < Markers.Length; i++)
                {
                    TextFileWriter.WriteLine("\tMarker");
                    TextFileWriter.WriteLine("\t{");

                    //Check for loop marker!!!
                    if (i + 1 < Markers.Length && Markers[i + 1].Type == 6)
                    {
                        //Skip loop start marker and go to loop marker
                        i += 1;
                        TextFileWriter.WriteLine(string.Join("", "\t\tName=Stream Start Loop"));
                        TextFileWriter.WriteLine(string.Join("", "\t\tPos=", Markers[i].LoopStart));
                        TextFileWriter.WriteLine(string.Join("", "\t\tType=", Markers[i].Type));
                        TextFileWriter.WriteLine(string.Join("", "\t\tFlags=", Markers[i].Flags));
                        TextFileWriter.WriteLine(string.Join("", "\t\tExtra=", Markers[i].Extra));
                        TextFileWriter.WriteLine("\t}");

                        //Go to start loop end marker
                        i += 1;
                        TextFileWriter.WriteLine("\tMarker");
                        TextFileWriter.WriteLine("\t{");
                        TextFileWriter.WriteLine(string.Join("", "\t\tName=Stream End Loop"));
                    }
                    else
                    {
                        TextFileWriter.WriteLine(string.Join("", "\t\tName=Stream ", GetMarkerType(Markers[i].Type), " ", "Marker"));
                    }
                    TextFileWriter.WriteLine(string.Join("", "\t\tPos=", Markers[i].Position));
                    TextFileWriter.WriteLine(string.Join("", "\t\tType=", Markers[i].Type));
                    TextFileWriter.WriteLine(string.Join("", "\t\tFlags=", Markers[i].Flags));
                    TextFileWriter.WriteLine(string.Join("", "\t\tExtra=", Markers[i].Extra));
                    TextFileWriter.WriteLine("\t}");
                }
                TextFileWriter.WriteLine("}");
            }
        }

        private string GetMarkerType(uint MarkerValue)
        {
            string markerType;

            switch (MarkerValue)
            {
                case 10:
                    markerType = "Start";
                    break;
                case 9:
                    markerType = "End";
                    break;
                case 7:
                    markerType = "Goto";
                    break;
                case 6:
                    markerType = "Loop";
                    break;
                case 5:
                    markerType = "Pause";
                    break;
                default:
                    markerType = "Jump";
                    break;
            }

            return markerType;
        }
    }
}
