using System.Collections.Generic;
using System.IO;

namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal string[] GetHashtableLabels(string filePath)
        {
            List<string> hashTableLabels = new List<string>();

            using (StreamReader sw = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                while (!sw.EndOfStream)
                {
                    string[] currentLine = sw.ReadLine().Split(null);
                    if (currentLine.Length > 1)
                    {
                        hashTableLabels.Add(currentLine[1].Trim());
                    }
                }
            }

            return hashTableLabels.ToArray();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
