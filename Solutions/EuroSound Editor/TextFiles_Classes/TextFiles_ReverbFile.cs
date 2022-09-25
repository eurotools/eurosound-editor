using sb_editor.Objects;
using System;
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
        public static ReverbFile ReadReverbFile(string filePath)
        {
            ReverbFile rvbData = new ReverbFile
            {
                TextFileName = Path.GetFileNameWithoutExtension(filePath)
            };

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

                    //HashCodes Block
                    if (currentLine.Equals("#MiscData", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string hashcodeNumber = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            rvbData.HashCode = Convert.ToInt32(hashcodeNumber);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Read Data
                    ReadReverbData(sr, "#PCReverb", ref currentLine, rvbData.PCReverb);
                    ReadReverbData(sr, "#XBReverb", ref currentLine, rvbData.XBReverb);
                    ReadReverbData(sr, "#GCReverb", ref currentLine, rvbData.GCReverb);
                }
            }

            return rvbData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static void ReadReverbData(StreamReader sr, string name, ref string currentLine, ReverbData platformData)
        {
            //PC Data Block
            if (currentLine.Equals("#PCReverb", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < 6; i++)
                {
                    currentLine = sr.ReadLine().Trim();
                    if (currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                platformData.RoomSize = Convert.ToInt32(currentLine);
                                break;
                            case 1:
                                platformData.Width = Convert.ToInt32(currentLine);
                                break;
                            case 2:
                                platformData.Damp = Convert.ToInt32(currentLine);
                                break;
                            case 3:
                                platformData.LowPassFilter = Convert.ToInt32(currentLine);
                                break;
                            case 4:
                                platformData.Filter1 = Convert.ToInt32(currentLine);
                                break;
                            case 5:
                                platformData.Filter2 = Convert.ToInt32(currentLine);
                                break;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteReverbFile(ReverbFile fileData, string filePath)
        {
            using (StreamWriter outputFile = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.UTF8))
            {
                outputFile.WriteLine("#MiscData");
                outputFile.WriteLine("HashCode  {0}", fileData.HashCode);
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#PCReverb");
                outputFile.WriteLine(fileData.PCReverb.RoomSize);
                outputFile.WriteLine(fileData.PCReverb.Width);
                outputFile.WriteLine(fileData.PCReverb.Damp);
                outputFile.WriteLine(fileData.PCReverb.LowPassFilter);
                outputFile.WriteLine(fileData.PCReverb.Filter1);
                outputFile.WriteLine(fileData.PCReverb.Filter2);
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#XBReverb");
                outputFile.WriteLine(fileData.XBReverb.RoomSize);
                outputFile.WriteLine(fileData.XBReverb.Width);
                outputFile.WriteLine(fileData.XBReverb.Damp);
                outputFile.WriteLine(fileData.XBReverb.LowPassFilter);
                outputFile.WriteLine(fileData.XBReverb.Filter1);
                outputFile.WriteLine(fileData.XBReverb.Filter2);
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#GCReverb");
                outputFile.WriteLine(fileData.GCReverb.RoomSize);
                outputFile.WriteLine(fileData.GCReverb.Width);
                outputFile.WriteLine(fileData.GCReverb.Damp);
                outputFile.WriteLine(fileData.GCReverb.LowPassFilter);
                outputFile.WriteLine(fileData.GCReverb.Filter1);
                outputFile.WriteLine(fileData.GCReverb.Filter2);
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}

