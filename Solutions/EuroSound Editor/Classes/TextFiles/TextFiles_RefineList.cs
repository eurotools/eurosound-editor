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
        public static string[] ReadRefineList(string filePath)
        {
            List<string> refineList = new List<string>();

            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine().Trim();
                    //Available formats section
                    if (currentLine.Equals("#RefineSearch", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            refineList.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return refineList.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteRefine(string filePath, string[] refineList)
        {
            using (StreamWriter sr = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                sr.WriteLine("#RefineSearch");
                for (int i = 0; i < refineList.Length; i++)
                {
                    sr.WriteLine(refineList[i]);
                }
                sr.WriteLine("#END");
                sr.WriteLine(string.Empty);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
