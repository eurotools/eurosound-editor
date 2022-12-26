using sb_editor.Objects;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static partial class TextFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static Misc ReadMiscFile(string miscFilePath)
        {
            Misc MiscFileData = new Misc();

            using (StreamReader sr = new StreamReader(File.Open(miscFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), new UTF8Encoding(false)))
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
                        ReadHeaderData(MiscFileData, currentLine);
                    }

                    //Read project version
                    if (currentLine.Equals("#VERSION", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            MiscFileData.Version = GetKeyWordValue("VersionNumber", currentLine);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Streams
                    if (currentLine.Equals("#STREAMS", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            if (lineData[0].Equals("RESAMPLESTREAMS", StringComparison.OrdinalIgnoreCase))
                            {
                                MiscFileData.ReSampleStreams = Convert.ToBoolean(Convert.ToUInt32(lineData[1]));
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //HashCodes Block
                    if (currentLine.Equals("#HASHCODES", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            //Check KeyWord
                            switch (lineData[0].ToUpper())
                            {
                                case "SFXHASHCODENUMBER":
                                    MiscFileData.SFXHashCodeNumber = Convert.ToInt32(lineData[1]);
                                    break;
                                case "SOUNDBANKHASHCODENUMBER":
                                    MiscFileData.SoundBankHashCodeNumber = Convert.ToInt32(lineData[1]);
                                    break;
                                case "MFXHASHCODENUMBER":
                                    MiscFileData.MFXHashCodeNumber = Convert.ToInt32(lineData[1]);
                                    break;
                                case "REVERBHASHCODENUMBER":
                                    MiscFileData.ReverbHashCodeNumber = Convert.ToInt32(lineData[1]);
                                    break;
                                case "SFXGROUPNUMBER":
                                    MiscFileData.SFXGroupNumber = Convert.ToInt32(lineData[1]);
                                    break;
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }
                }
            }

            return MiscFileData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteMiscFile(string miscFilePath, bool newProject = false)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(miscFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), new UTF8Encoding(false)))
            {
                if (newProject)
                {
                    sw.WriteLine("#HASHCODES");
                    sw.WriteLine("SFXHashCodeNumber {0}", 0);
                    sw.WriteLine("SoundBankHashCodeNumber {0}", 0);
                    sw.WriteLine("#END");
                    sw.WriteLine(string.Empty);
                }
                sw.WriteLine("#VERSION");
                sw.WriteLine("VersionNumber {0}.{1}", Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor);
                sw.WriteLine("#END");
                sw.WriteLine(string.Empty);
                if (!newProject)
                {
                    sw.WriteLine("#STREAMS");
                    sw.WriteLine("ReSampleStreams {0}", GlobalPrefs.ReSampleStreams ? "1" : "0");
                    sw.WriteLine("#END");
                    sw.WriteLine(string.Empty);

                    sw.WriteLine("#HASHCODES");
                    sw.WriteLine("SFXHashCodeNumber {0}", GlobalPrefs.SFXHashCodeNumber);
                    sw.WriteLine("SoundBankHashCodeNumber {0}", GlobalPrefs.SoundBankHashCodeNumber);
                    if (GlobalPrefs.MFXHashCodeNumber > 0)
                    {
                        sw.WriteLine("MFXHashCodeNumber {0}", GlobalPrefs.MFXHashCodeNumber);
                    }
                    if (GlobalPrefs.ReverbHashCodeNumber > 0)
                    {
                        sw.WriteLine("ReverbHashCodeNumber {0}", GlobalPrefs.ReverbHashCodeNumber);
                    }
                    sw.WriteLine("#END");
                    sw.WriteLine(string.Empty);
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
