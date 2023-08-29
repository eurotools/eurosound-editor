//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Text Files - Generic Utils
//-------------------------------------------------------------------------------------------------------------------------------
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private static void ReadHeaderData(FileHeader HeaderData, string currentLine)
        {
            //Split content
            string[] lineData = currentLine.Split(new string[] { "..." }, StringSplitOptions.RemoveEmptyEntries);

            //Get header info
            if (lineData[0].Trim().Equals("## First Created", StringComparison.OrdinalIgnoreCase))
            {
                if (lineData.Length > 1)
                {
                    HeaderData.bankInfo1 = lineData[0].Trim();
                }
                if (lineData.Length > 1)
                {
                    HeaderData.FirstCreated = DateTime.ParseExact(lineData[1].Trim(), GlobalPrefs.FilesDateFormat, CultureInfo.InvariantCulture);
                }
            }
            if (lineData[0].Trim().Equals("## Created By", StringComparison.OrdinalIgnoreCase))
            {
                if (lineData.Length > 0)
                {
                    HeaderData.bankInfo2 = lineData[0].Trim();
                }
                if (lineData.Length > 1)
                {
                    HeaderData.CreatedBy = lineData[1].Trim();
                }
            }
            if (lineData[0].Trim().Equals("## Last Modified", StringComparison.OrdinalIgnoreCase))
            {
                if (lineData.Length > 0)
                {
                    HeaderData.bankInfo3 = lineData[0].Trim();
                }
                if (lineData.Length > 1)
                {
                    HeaderData.LastModified = DateTime.ParseExact(lineData[1].Trim(), GlobalPrefs.FilesDateFormat, CultureInfo.InvariantCulture);
                }
            }
            if (lineData[0].Trim().Equals("## Last Modified By", StringComparison.OrdinalIgnoreCase))
            {
                if (lineData.Length > 0)
                {
                    HeaderData.bankInfo4 = lineData[0].Trim();
                }
                if (lineData.Length > 1)
                {
                    HeaderData.ModifiedBy = lineData[1].Trim();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private static string GetKeyWordValue(string keyword, string currentLine)
        {
            int keyWordLength = keyword.Length;
            string lineData = currentLine.Substring(keyWordLength).Trim();
            return lineData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] ReadListBlock(string filePath, string blockName)
        {
            HashSet<string> SFXs = new HashSet<string>();

            string[] fileData = File.ReadAllLines(filePath);

            int index = Array.IndexOf(fileData, blockName) + 1;
            if (index > 0)
            {
                string currentLine = fileData[index];
                while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                {
                    SFXs.Add(currentLine);
                    currentLine = fileData[index++];
                }
            }

            return SFXs.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string ReadFileVersion(string filePath)
        {
            string fileVersion = "0";
            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
            {
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine().Trim();
                    //HashCodes Block
                    if (currentLine.Equals("#VERSION", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string versionNumber = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            fileVersion = versionNumber.Replace(".", string.Empty);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return fileVersion;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteHeader(StreamWriter outputFile, string fileID, FileHeader fileHeader)
        {
            outputFile.WriteLine("## EuroSound {0} File", fileID);
            outputFile.WriteLine("## First Created ... {0}", fileHeader.FirstCreated.ToString(GlobalPrefs.FilesDateFormat));
            outputFile.WriteLine("## Created By ... {0}", fileHeader.CreatedBy);
            outputFile.WriteLine("## Last Modified ... {0}", fileHeader.LastModified.ToString(GlobalPrefs.FilesDateFormat));
            outputFile.WriteLine("## Last Modified By ... {0}", fileHeader.ModifiedBy);
            outputFile.WriteLine(string.Empty);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
