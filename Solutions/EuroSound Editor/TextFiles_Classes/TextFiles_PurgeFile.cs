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
        public static string[] ReadPurgeFiles(string filePath)
        {
            List<string> dependencies = new List<string>();

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
                    if (currentLine.Equals("#PurgedFileList", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            dependencies.Add(currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return dependencies.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WritePurgeFilesList(string filePath, string[] fileslist)
        {
            //Update text file
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("Purged File List Created:\t {0:dd/MM/yyyy}\t\t{0:HH:mm:ss}", DateTime.Now);
                sw.WriteLine(string.Empty);
                sw.WriteLine("#PurgedFileList");
                for (int i = 0; i < fileslist.Length; i++)
                {
                    sw.WriteLine(fileslist[i]);
                }
                sw.WriteLine("#END");
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
