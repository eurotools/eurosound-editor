using System;
using System.Collections.Generic;
using System.IO;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] ReadJumpHashCodes(string filePath)
        {
            List<string> jumpHashCodes = new List<string>();

            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
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
