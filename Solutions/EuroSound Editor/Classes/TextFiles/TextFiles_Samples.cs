using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static SamplePool ReadSamplesFile(string filePath)
        {
            SamplePool samplePool = new SamplePool();

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

                    //Header info
                    if (currentLine.StartsWith("##"))
                    {
                        ReadHeaderData(samplePool, currentLine);
                    }

                    //Available formats section
                    if (currentLine.Equals("#AvailableSamples", StringComparison.OrdinalIgnoreCase))
                    {
                        uint numberOfItems = Convert.ToUInt32(sr.ReadLine().Trim());
                        for (int colIndex = 0; colIndex < 10; colIndex++)
                        {
                            for (int i = 0; i < numberOfItems; i++)
                            {
                                currentLine = sr.ReadLine();
                                switch (colIndex)
                                {
                                    case 0:
                                        samplePool.SamplePoolItems.Add(currentLine.Trim(), new SamplePoolItem());
                                        break;
                                    case 1:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.ReSampleRate = currentLine.Trim();
                                        break;
                                    case 2:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.Size = currentLine;
                                        break;
                                    case 3:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.Date = currentLine.Trim();
                                        break;
                                    case 4:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.ReSample = currentLine.Trim().Equals("True", StringComparison.OrdinalIgnoreCase);
                                        break;
                                    case 5:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.StreamMe = currentLine.Trim().Equals("True", StringComparison.OrdinalIgnoreCase);
                                        break;
                                    case 6:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.ReSmp1 = currentLine.Trim();
                                        break;
                                    case 7:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.ReSmp2 = currentLine.Trim();
                                        break;
                                    case 8:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.ReSmp3 = currentLine.Trim();
                                        break;
                                    case 9:
                                        samplePool.SamplePoolItems.ElementAt(i).Value.ReSmp4 = currentLine.Trim();
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            return samplePool;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteSamplesFile(string filePath, SamplePool samplesFile)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(filePath))
            {
                samplesFile.FirstCreated = currentData;
                samplesFile.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            samplesFile.LastModified = currentData;
            samplesFile.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            using (StreamWriter outputFile = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                WriteHeader(outputFile, "Samples", samplesFile);
                outputFile.WriteLine("#AvailableSamples");
                outputFile.WriteLine(" {0} ", samplesFile.SamplePoolItems.Count);
                for (int i = 0; i < 10; i++)
                {
                    foreach (KeyValuePair<string, SamplePoolItem> sampleData in samplesFile.SamplePoolItems)
                    {
                        switch (i)
                        {
                            case 0:
                                outputFile.WriteLine(sampleData.Key);
                                break;
                            case 1:
                                outputFile.WriteLine(sampleData.Value.ReSampleRate);
                                break;
                            case 2:
                                outputFile.WriteLine(sampleData.Value.Size);
                                break;
                            case 3:
                                outputFile.WriteLine(sampleData.Value.Date);
                                break;
                            case 4:
                                outputFile.WriteLine(sampleData.Value.ReSample.ToString());
                                break;
                            case 5:
                                outputFile.WriteLine(sampleData.Value.StreamMe.ToString());
                                break;
                            case 6:
                                outputFile.WriteLine(sampleData.Value.ReSmp1);
                                break;
                            case 7:
                                outputFile.WriteLine(sampleData.Value.ReSmp2);
                                break;
                            case 8:
                                outputFile.WriteLine(sampleData.Value.ReSmp3);
                                break;
                            case 9:
                                outputFile.WriteLine(sampleData.Value.ReSmp4);
                                break;
                        }
                    }
                }
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
