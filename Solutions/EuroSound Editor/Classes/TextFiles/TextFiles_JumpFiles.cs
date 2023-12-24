//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Text Files - Audio Jump Files
//-------------------------------------------------------------------------------------------------------------------------------
using ExMarkers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static int CreateJumpMarker(string markerFilePath, string outputFilePath)
        {
            //Read Markers File
            MarkerTextFile[] fileData = TextFiles.ReadMarkerFile(markerFilePath);

            //Write Jump Markers
            int writedLines = 0;
            using (StreamWriter sw = new StreamWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("#JUMPMARKERS");
                for (int i = 0; i < fileData.Length; i++)
                {
                    if (fileData[i].Type == 9 && fileData[i].Name.Equals("*"))
                    {
                        continue;
                    }
                    sw.WriteLine(fileData[i].Name);
                    writedLines = i + 1;
                }
                sw.WriteLine("#END");
            }

            return writedLines;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] ReadJumpHashCodes(string filePath)
        {
            List<string> jumpHashCodes = new List<string>();

            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine().Trim();
                    //Skip empty or commented lines
                    if (string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//"))
                    {
                        continue;
                    }

                    //Dependencies Block
                    if (currentLine.Equals("#JUMPMARKERS", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            jumpHashCodes.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return jumpHashCodes.ToArray();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
