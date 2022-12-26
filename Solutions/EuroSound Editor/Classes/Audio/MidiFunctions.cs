using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace sb_editor.Audio_Classes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class MidiFunctions
    {
        private bool endMarkerAdded = false;
        private readonly Dictionary<int, List<string>> MergedDict = new Dictionary<int, List<string>>();
        internal List<string> errorsList = new List<string>();

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void WriteMarkerFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                int markerIndex = 0;
                sw.WriteLine("Markers");
                sw.WriteLine("{");
                foreach (KeyValuePair<int, List<string>> markerToPrint in MergedDict)
                {
                    if (markerIndex > 0)
                    {
                        sw.WriteLine("\tMarker{0}", markerIndex);
                    }
                    else
                    {
                        sw.WriteLine("\tMarker");
                    }
                    sw.WriteLine("\t{");
                    switch (markerToPrint.Value[2])
                    {
                        case "c3":
                            WriteMakerBlock(sw, markerToPrint.Value[4], markerToPrint.Key, 9, ref markerIndex);
                            break;
                        case "c5":
                            WriteMakerBlock(sw, markerToPrint.Value[4], markerToPrint.Key, 6, ref markerIndex);
                            break;
                        case "c6":
                            WriteMakerBlock(sw, markerToPrint.Value[4], markerToPrint.Key, 10, ref markerIndex);
                            break;
                        case "f4":
                            WriteMakerBlock(sw, "PAUSE_STREAM_HERE", markerToPrint.Key, 7, ref markerIndex);
                            break;
                        case "f5":
                            WriteMakerBlock(sw, markerToPrint.Value[4], markerToPrint.Key, 7, ref markerIndex);
                            break;
                    }
                    sw.WriteLine("\t}");
                }
                sw.WriteLine("}");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void WriteMakerBlock(StreamWriter sw, string Name, int mPos, int mType, ref int markerIndex)
        {
            sw.WriteLine("\t\tName={0}", Name);
            sw.WriteLine("\t\tPos={0}", (int)Math.Round(mPos * 44.1));
            sw.WriteLine("\t\tType={0}", mType);
            sw.WriteLine("\t\tFlags={0}", 0);
            sw.WriteLine("\t\tExtra={0}", 0);
            markerIndex++;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetNotes(string midiTextFilePath)
        {
            List<string> NotesArray = new List<string>();

            string[] fileData = File.ReadAllLines(midiTextFilePath);
            for (int i = 0; i < fileData.Length; i++)
            {
                if (fileData[i].Contains(";") && !fileData[i].Contains("ms */ +c4"))
                {
                    NotesArray.Add(fileData[i]);
                }
            }

            return NotesArray.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetTexts(string midiTextFilePath)
        {
            List<string> TextsArray = new List<string>();

            string[] fileData = File.ReadAllLines(midiTextFilePath);
            for (int i = 0; i < fileData.Length; i++)
            {
                if (fileData[i].Trim().StartsWith("/*"))
                {
                    TextsArray.Add(fileData[i]);
                }
            }

            return TextsArray.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal bool CheckMarkersFatalErrors(string[] notesArray, string[] textArray, StreamWriter sw)
        {
            bool hasErrors = false;
            int occurences = 0;

            for (int i = 0; i < notesArray.Length; i++)
            {
                //Marker Line
                string textLine = "-";
                string markerName = "*";
                int MarkerMilliseconds = 0;
                if (i < textArray.Length)
                {
                    textLine = string.Format(" {0}", textArray[i]);
                    markerName = GetText(textLine);
                    MarkerMilliseconds = GetMilliseconds(textLine);
                }

                //Notes data
                int milliseconds = GetMilliseconds(notesArray[i]);
                string noteLine = string.Format(" {0}", notesArray[i]);
                string noteName = GetNote(noteLine);
                if (MergedDict.ContainsKey(milliseconds))
                {
                    string errorMessage = "MS value matches previous marker";
                    errorsList.Add(errorMessage);
                    sw.WriteLine(errorMessage);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(string.Format("------------------------------------Occurance Number  {0}", ++occurences));
                    sw.WriteLine("------------------------------------Note Found");
                    sw.WriteLine(noteLine);
                    sw.WriteLine(noteName);
                    sw.WriteLine(string.Format("{0:00}:{1:00}", milliseconds / 1000 / 60, Math.Round(decimal.Divide(milliseconds, 1000) % 60)));
                    sw.WriteLine("------------------------------------Text Found");
                    sw.WriteLine(textLine);
                    sw.WriteLine(markerName);
                    sw.WriteLine(string.Format("{0:00}:{1:00}", MarkerMilliseconds / 1000 / 60, Math.Round(decimal.Divide(MarkerMilliseconds, 1000) % 60)));
                    hasErrors = true;
                    break;
                }
                else
                {
                    MergedDict.Add(milliseconds, new List<string> { textLine, MarkerMilliseconds.ToString(), noteName, noteLine, markerName });
                }

                //Check if we have to quit
                if (noteName.Equals("c3", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            //Continue checking
            if (!hasErrors)
            {
                //Notes Count
                int notesCount = 0;
                foreach (KeyValuePair<int, List<string>> noteToCheck in MergedDict)
                {
                    if (noteToCheck.Value[2].Equals("c3", StringComparison.OrdinalIgnoreCase))
                    {
                        endMarkerAdded = true;
                        break;
                    }
                    if (!noteToCheck.Value[2].Equals("f4", StringComparison.OrdinalIgnoreCase))
                    {
                        notesCount++;
                    }
                }

                //Notes &  Text Missmatch!!
                if (notesCount != textArray.Length)
                {
                    hasErrors = true;
                    occurences = 0;

                    //Inform User
                    string error = string.Format("c5+c6+f5 Note and Text Mismatch error\nText Found =  {0}\nNote Found =  {1}", textArray.Length, notesCount);
                    errorsList.Add(error);
                    sw.WriteLine(error);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("Milliseconds value mis-matched at:");
                    sw.WriteLine(string.Empty);
                    foreach (KeyValuePair<int, List<string>> noteToCheck in MergedDict)
                    {
                        if (noteToCheck.Value[2].Equals("c5", StringComparison.OrdinalIgnoreCase) || noteToCheck.Value[2].Equals("c6", StringComparison.OrdinalIgnoreCase) || noteToCheck.Value[2].Equals("f5", StringComparison.OrdinalIgnoreCase))
                        {
                            if (string.IsNullOrEmpty(noteToCheck.Value[0]) || string.IsNullOrWhiteSpace(noteToCheck.Value[0]) || !noteToCheck.Value[1].Equals(noteToCheck.Key.ToString()))
                            {
                                sw.WriteLine(string.Empty);
                                sw.WriteLine(string.Empty);
                                sw.WriteLine(string.Format("------------------------------------Occurance Number  {0}", ++occurences));
                                sw.WriteLine("------------------------------------Note Found");
                                sw.WriteLine(noteToCheck.Value[3]);
                                sw.WriteLine(noteToCheck.Value[2]);
                                sw.WriteLine(string.Format("{0:00}:{1:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60)));
                                sw.WriteLine("------------------------------------Text Found");
                                sw.WriteLine(noteToCheck.Value[0]);
                                sw.WriteLine(noteToCheck.Value[4]);
                                int milliseconds = Convert.ToInt32(noteToCheck.Value[1]);
                                sw.WriteLine(string.Format("{0:00}:{1:00}", milliseconds / 1000 / 60, Math.Round(decimal.Divide(milliseconds, 1000) % 60)));
                            }
                        }
                    }
                }
            }

            return hasErrors;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal bool CheckMarkersWarnings(StreamWriter sw)
        {
            bool hasErrors = false;
            foreach (KeyValuePair<int, List<string>> noteToCheck in MergedDict)
            {
                string noteName = noteToCheck.Value[2].ToLower();
                string markerName = noteToCheck.Value[4];

                //Check Spaces
                for (int i = 0; i < markerName.Length; i++)
                {
                    if (char.IsWhiteSpace(markerName[i]))
                    {
                        string toPrint = string.Format("Marker Name has spaces init!!!\n{0}", markerName);
                        errorsList.Add(toPrint);
                        sw.WriteLine(toPrint);
                        sw.WriteLine(string.Empty);
                        hasErrors = true;
                        break;
                    }
                }

                //Do Other Checks
                switch (noteName)
                {
                    case "c4":
                        foreach (KeyValuePair<int, List<string>> markerToCheck in MergedDict)
                        {
                            int markerToCheckPos = Convert.ToInt32(markerToCheck.Value[1]);
                            int textPos = Convert.ToInt32(noteToCheck.Value[1]);
                            if (textPos == markerToCheckPos)
                            {
                                string errorMessage = string.Format("Jump Marker MS matches previous\nPos = {0:#0}:{1:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60));
                                errorsList.Add(errorMessage);
                                sw.WriteLine(errorMessage);
                                sw.WriteLine(string.Empty);
                                hasErrors = true;
                                break;
                            }
                        }
                        break;
                    case "c5":
                        if (string.IsNullOrEmpty(markerName) || markerName.Equals("*"))
                        {
                            string errorMessage = string.Format("c5 Note error - Loop start has no Label\nPos = {0:#0}:{1:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        if (markerName.IndexOf("LOOP", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            string errorMessage = string.Format("c5 Note error - Not InStr(MarkerName, 'LOOP')\nMarkerName = {0}\nPos = {1:#0}:{2:00}", markerName, noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        if (!noteToCheck.Key.ToString().Equals(noteToCheck.Value[1]))
                        {
                            int textPos = Convert.ToInt32(noteToCheck.Value[1]);
                            string errorMessage = string.Format("c5 Note error - Not (Note MS = Text MS) value\nNote Pos = {0:#0}:{1:00}\nText Pos = {2:#0}:{3:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60), textPos / 1000 / 60, Math.Round(decimal.Divide(textPos, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        break;
                    case "c6":
                        if (string.IsNullOrEmpty(markerName) || markerName.Equals("*"))
                        {
                            string errorMessage = string.Format("c6 Note error - Playback start has no Label\nPos = {0:#0}:{1:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        if (!noteToCheck.Key.ToString().Equals(noteToCheck.Value[1]))
                        {
                            int textPos = Convert.ToInt32(noteToCheck.Value[1]);
                            string errorMessage = string.Format("c6 Note error - Not (Note MS = Text MS) value\nNote Pos = {0:#0}:{1:00}\nText Pos = {2:#0}:{3:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60), textPos / 1000 / 60, Math.Round(decimal.Divide(textPos, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        break;
                    case "f5":
                        if (string.IsNullOrEmpty(markerName) || markerName.Equals("*"))
                        {
                            string errorMessage = string.Format("f5 Note error - Goto Marker has no Label\nPos = {0:#0}:{1:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        if (markerName.IndexOf("GOTO_", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            string errorMessage = string.Format("f5 Note error - Not InStr(MarkerName, 'GOTO_')\nMarkerName = {0}\nPos = {1:#0}:{2:00}", markerName, noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        if (!noteToCheck.Key.ToString().Equals(noteToCheck.Value[1]))
                        {
                            int textPos = Convert.ToInt32(noteToCheck.Value[1]);
                            string errorMessage = string.Format("f5 Note error - Not (Note MS = Text MS) value\nNote Pos = {0:#0}:{1:00}\nText Pos = {2:#0}:{3:00}", noteToCheck.Key / 1000 / 60, Math.Round(decimal.Divide(noteToCheck.Key, 1000) % 60), textPos / 1000 / 60, Math.Round(decimal.Divide(textPos, 1000) % 60));
                            errorsList.Add(errorMessage);
                            sw.WriteLine(errorMessage);
                            sw.WriteLine(string.Empty);
                            hasErrors = true;
                        }
                        break;
                }
            }

            //Print Error Log File
            if (!endMarkerAdded)
            {
                string errorMessage = "c3 Note error - TotalEndNotesFound = 0";
                sw.WriteLine(errorMessage);
                hasErrors = true;
                errorsList.Add(errorMessage);
            }

            return hasErrors;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int GetMilliseconds(string lineToInspect)
        {
            //Try get milliseconds
            int start = lineToInspect.IndexOf("/*") + 2;
            int end = lineToInspect.IndexOf("ms") - start;

            //Try to get the milliseconds value
            string data = lineToInspect.Substring(start, end).Trim();
            if (int.TryParse(data, out int result))
            {
                return result;
            }
            return -1;
        }

        //-------------------------------------------------------------------------------------------------------------------------------<
        internal string GetText(string lineToInspect)
        {
            string result = "*";
            Match matches = Regex.Match(lineToInspect, @"""(.*?)""");
            if (matches.Success)
            {
                result = matches.Groups[1].Value;
            }
            return result;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string GetNote(string lineToInspect)
        {
            //Try get milliseconds
            int start = lineToInspect.IndexOf("+") + 1;

            //Try to get the milliseconds value
            string data = lineToInspect.Substring(start).Trim();
            return data;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
