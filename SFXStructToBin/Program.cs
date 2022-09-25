using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SFXStructToBin
{
    internal class Program
    {
        // *===============================================================================================
        // * GLOBAL VARIABLES 
        // *===============================================================================================
        // Use the dot instead of comma
        private readonly static NumberFormatInfo numericProvider = new NumberFormatInfo()
        {
            NumberDecimalSeparator = "."
        };

        // *===============================================================================================
        // * MAIN METHOD
        // *===============================================================================================
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                // Get data to print
                List<float[]> itemsList = ReadTextFile(args[0]);

                // Generate Binary File
                CreateBinaryFile(args[1], itemsList);
            }
        }

        // *===============================================================================================
        // * FILES FUNCTIONS
        // *===============================================================================================
        private static void CreateBinaryFile(string outputFilePath, List<float[]> listOfItems)
        {
            using (BinaryWriter BinWriter = new BinaryWriter(File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.ASCII))
            {
                for (int itemIndex = 0; itemIndex <= listOfItems.Count - 1; itemIndex++)
                {
                    float[] currentItem = listOfItems[itemIndex];
                    // HashCode
                    BinWriter.Write((uint)currentItem[5]);
                    // Inner Radius
                    BinWriter.Write(currentItem[1]);
                    // Outer Radius
                    BinWriter.Write(currentItem[2]);
                    // Alertness
                    BinWriter.Write(currentItem[3]);
                    // Duration
                    BinWriter.Write(currentItem[4]);
                    // Looping
                    BinWriter.Write((sbyte)currentItem[5]);
                    // Tracking 3D
                    BinWriter.Write((sbyte)currentItem[6]);
                    // SampleStreamed
                    BinWriter.Write((sbyte)currentItem[7]);
                    // Padding
                    BinWriter.Write((sbyte)0);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static List<float[]> ReadTextFile(string inputFilePath)
        {
            List<float[]> itemsList = new List<float[]>();
            using (StreamReader sr = new StreamReader(File.Open(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine().Trim();
                    //Skip empty or commented lines
                    if (string.IsNullOrEmpty(currentLine) || currentLine.StartsWith("//"))
                    {
                        continue;
                    }

                    // Check if the currentLine is valid
                    if (currentLine.StartsWith("{"))
                    {
                        string[] SplitedLine = currentLine.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);

                        // Parse text data to floats and add items to the list
                        float[] ArrayOfValues = new float[8];
                        for (int index = 0; index <= ArrayOfValues.Length - 1; index++)
                        {
                            ArrayOfValues[index] = StringFloatToDouble(SplitedLine[index].Trim());
                        }
                        itemsList.Add(ArrayOfValues);
                    }
                }
            }

            return itemsList;
        }

        // *===============================================================================================
        // * FORMAT NUMBERS FUNCTIONS
        // *===============================================================================================
        private static float StringFloatToDouble(string number)
        {
            float FinalNumber = 0;

            // Ensure that the string is not null
            if (!string.IsNullOrEmpty(number))
            {
                string num = number.Replace("f", string.Empty);
                FinalNumber = float.Parse(num, numericProvider);
            }

            return FinalNumber;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
