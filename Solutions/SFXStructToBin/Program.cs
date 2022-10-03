using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace SFXStructToBin
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class Program
    {
        // *===============================================================================================
        // * GLOBAL VARIABLES 
        // *===============================================================================================
        private static readonly NumberFormatInfo numericProvider = new NumberFormatInfo()
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
                using (BinaryWriter BinWriter = new BinaryWriter(File.Open(args[1], FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.ASCII))
                {
                    //Start Reading Text File
                    using (StreamReader sr = new StreamReader(File.Open(args[0], FileMode.Open, FileAccess.Read, FileShare.Read)))
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
                                float[] valuesToWrite = GetArrayValues(currentLine);
                                // HashCode
                                BinWriter.Write((uint)valuesToWrite[5]);
                                // Inner Radius
                                BinWriter.Write(valuesToWrite[1]);
                                // Outer Radius
                                BinWriter.Write(valuesToWrite[2]);
                                // Alertness
                                BinWriter.Write(valuesToWrite[3]);
                                // Duration
                                BinWriter.Write(valuesToWrite[4]);
                                // Looping
                                BinWriter.Write((sbyte)valuesToWrite[5]);
                                // Tracking 3D
                                BinWriter.Write((sbyte)valuesToWrite[6]);
                                // SampleStreamed
                                BinWriter.Write((sbyte)valuesToWrite[7]);
                                // Padding
                                BinWriter.Write((sbyte)0);
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static float[] GetArrayValues(string currentLine)
        {
            string[] SplitedLine = currentLine.Split(new char[] { '{', ',', '}' }, StringSplitOptions.RemoveEmptyEntries);
            float[] ArrayOfValues = new float[8];
            for (int index = 0; index < ArrayOfValues.Length; index++)
            {
                // Parse text data to floats and add items to the list
                if (!string.IsNullOrEmpty(SplitedLine[index]))
                {
                    ArrayOfValues[index] = StringFloatToDouble(SplitedLine[index]);
                }
            }
            return ArrayOfValues;
        }

        // *===============================================================================================
        // * FORMAT NUMBERS FUNCTIONS
        // *===============================================================================================
        private static float StringFloatToDouble(string number)
        {
            string num = number.Trim().Replace("f", string.Empty);
            return float.Parse(num, numericProvider);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}