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
        public static SFX ReadSfxFile(string filePath)
        {
            SFX sfxData = new SFX();
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

                    //Header info
                    if (currentLine.StartsWith("##"))
                    {
                        ReadHeaderData(sfxData.HeaderData, currentLine);
                    }

                    //Read parameters block
                    if (currentLine.Equals("#SFXParameters", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "REVERBSEND":
                                    sfxData.Parameters.ReverbSend = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "TRACKINGTYPE":
                                    sfxData.Parameters.TrackingType = Convert.ToByte(lineData[1].Trim());
                                    break;
                                case "INNERRADIUS":
                                    sfxData.Parameters.InnerRadius = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "OUTERRADIUS":
                                    sfxData.Parameters.OuterRadius = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "MAXVOICES":
                                    sfxData.Parameters.MaxVoices = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "ACTION1":
                                    sfxData.Parameters.Action1 = Convert.ToByte(lineData[1].Trim());
                                    break;
                                case "PRIORITY":
                                    sfxData.Parameters.Priority = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "GROUP":
                                    sfxData.Parameters.Group = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "ACTION2":
                                    sfxData.Parameters.Action2 = Convert.ToByte(lineData[1].Trim());
                                    break;
                                case "ALERTNESS":
                                    sfxData.Parameters.Alertness = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "IGNOREAGE":
                                    sfxData.Parameters.IgnoreAge = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "DUCKER":
                                    sfxData.Parameters.Ducker = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "DUCKERLENGHT":
                                    sfxData.Parameters.DuckerLength = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "DUCKERLENGTH":
                                    sfxData.Parameters.DuckerLength = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "MASTERVOLUME":
                                    sfxData.Parameters.MasterVolume = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "OUTDOORS":
                                    sfxData.Parameters.Outdoors = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "PAUSEINNIS":
                                    sfxData.Parameters.PauseInNis = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "STEALONAGE":
                                    sfxData.Parameters.StealOnAge = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "MUSICTYPE":
                                    sfxData.Parameters.MusicType = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "DOPPLER":
                                    sfxData.Parameters.Doppler = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Sample Pool Files
                    if (currentLine.Equals("#SFXSamplePoolFiles", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            SfxSample sampleData = new SfxSample
                            {
                                FilePath = currentLine.Trim()
                            };
                            sfxData.Samples.Add(sampleData);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //Sample Pool Modes
                    if (currentLine.Equals("#SFXSamplePoolModes", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            for (int i = 0; i < sfxData.Samples.Count; i++)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                    switch (lineData[0].ToUpper())
                                    {
                                        case "BASEVOLUME":
                                            sfxData.Samples[i].BaseVolume = Convert.ToSByte(lineData[1].Trim(), GlobalPrefs.NumericProvider);
                                            break;
                                        case "PITCHOFFSET":
                                            sfxData.Samples[i].PitchOffset = Convert.ToDecimal(lineData[1].Trim(), GlobalPrefs.NumericProvider);
                                            break;
                                        case "RANDOMPITCHOFFSET":
                                            sfxData.Samples[i].RandomPitch = Convert.ToDecimal(lineData[1].Trim(), GlobalPrefs.NumericProvider);
                                            break;
                                        case "RANDOMVOLUMEOFFSET":
                                            sfxData.Samples[i].RandomVolume = Convert.ToSByte(lineData[1].Trim(), GlobalPrefs.NumericProvider);
                                            break;
                                        case "PAN":
                                            sfxData.Samples[i].Pan = Convert.ToSByte(lineData[1].Trim(), GlobalPrefs.NumericProvider);
                                            break;
                                        case "RANDOMPAN":
                                            sfxData.Samples[i].RandomPan = Convert.ToSByte(lineData[1].Trim(), GlobalPrefs.NumericProvider);
                                            break;
                                    }
                                    currentLine = sr.ReadLine().Trim();
                                }
                            }
                        }
                    }

                    if (currentLine.Equals("#SFXSamplePoolControl", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] lineData = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            switch (lineData[0].ToUpper())
                            {
                                case "ACTION1":
                                    sfxData.SamplePool.Action1 = Convert.ToByte(lineData[1].Trim());
                                    break;
                                case "RANDOMPICK":
                                    sfxData.SamplePool.RandomPick = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "SHUFFLED":
                                    sfxData.SamplePool.Shuffled = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "LOOP":
                                    sfxData.SamplePool.isLooped = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "POLYPHONIC":
                                    sfxData.SamplePool.Polyphonic = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "MINDELAY":
                                    sfxData.SamplePool.MinDelay = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "MAXDELAY":
                                    sfxData.SamplePool.MaxDelay = Convert.ToInt32(lineData[1].Trim());
                                    break;
                                case "ENABLESUBSFX":
                                    sfxData.SamplePool.EnableSubSFX = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                                case "ENABLESTEREO":
                                    sfxData.SamplePool.EnableStereo = Convert.ToBoolean(byte.Parse(lineData[1].Trim()));
                                    break;
                            }
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                    //HashCodes Block
                    if (currentLine.Equals("#HASHCODE", StringComparison.OrdinalIgnoreCase))
                    {
                        currentLine = sr.ReadLine().Trim();
                        while (!currentLine.Equals("#END", StringComparison.OrdinalIgnoreCase))
                        {
                            string hashcodeNumber = currentLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            sfxData.HashCode = Convert.ToInt32(hashcodeNumber);
                            currentLine = sr.ReadLine().Trim();
                        }
                    }

                }
            }

            return sfxData;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void WriteSfxFile(string outputFilePath, SFX sfxFile, bool defaultsFile = false)
        {
            //Get creation time if file exists
            DateTime currentData = DateTime.Now;
            if (!File.Exists(outputFilePath))
            {
                sfxFile.HeaderData.FirstCreated = currentData;
                sfxFile.HeaderData.CreatedBy = GlobalPrefs.EuroSoundUser;
            }
            sfxFile.HeaderData.LastModified = currentData;
            sfxFile.HeaderData.ModifiedBy = GlobalPrefs.EuroSoundUser;

            //Update text file
            string tmpFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt");
            using (StreamWriter outputFile = new StreamWriter(File.Open(tmpFilePath, FileMode.Create, FileAccess.Write, FileShare.Read), Encoding.UTF8))
            {
                if (defaultsFile)
                {
                    WriteHeader(outputFile, "SFX Defaults File", sfxFile.HeaderData);
                }
                else
                {
                    WriteHeader(outputFile, "SFX", sfxFile.HeaderData);
                }
                outputFile.WriteLine("#SFXParameters");
                outputFile.WriteLine("ReverbSend  {0}", sfxFile.Parameters.ReverbSend);
                outputFile.WriteLine("TrackingType  {0}", sfxFile.Parameters.TrackingType);
                outputFile.WriteLine("InnerRadius  {0}", sfxFile.Parameters.InnerRadius);
                outputFile.WriteLine("OuterRadius  {0}", sfxFile.Parameters.OuterRadius);
                outputFile.WriteLine("MaxVoices  {0}", sfxFile.Parameters.MaxVoices);
                outputFile.WriteLine("Action1  {0}", sfxFile.Parameters.Action1);
                outputFile.WriteLine("Priority  {0}", sfxFile.Parameters.Priority);
                outputFile.WriteLine("Group  {0}", sfxFile.Parameters.Group);
                outputFile.WriteLine("Action2  {0}", sfxFile.Parameters.Action2);
                outputFile.WriteLine("Alertness  {0}", sfxFile.Parameters.Alertness);
                outputFile.WriteLine("IgnoreAge  {0}", Convert.ToByte(sfxFile.Parameters.IgnoreAge));
                outputFile.WriteLine("Ducker  {0}", sfxFile.Parameters.Ducker);
                outputFile.WriteLine("DuckerLenght  {0}", sfxFile.Parameters.DuckerLength);
                outputFile.WriteLine("MasterVolume  {0}", sfxFile.Parameters.MasterVolume);
                outputFile.WriteLine("Outdoors  {0}", Convert.ToByte(sfxFile.Parameters.Outdoors));
                outputFile.WriteLine("PauseInNis  {0}", Convert.ToByte(sfxFile.Parameters.PauseInNis));
                outputFile.WriteLine("StealOnAge  {0}", Convert.ToByte(sfxFile.Parameters.StealOnAge));
                outputFile.WriteLine("MusicType  {0}", Convert.ToByte(sfxFile.Parameters.MusicType));
                outputFile.WriteLine("Doppler  {0}", Convert.ToByte(sfxFile.Parameters.Doppler));
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                if (!defaultsFile)
                {
                    outputFile.WriteLine("#SFXSamplePoolFiles");
                    for (int i = 0; i < sfxFile.Samples.Count; i++)
                    {
                        outputFile.WriteLine(sfxFile.Samples[i].FilePath);
                    }
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                    outputFile.WriteLine("#SFXSamplePoolModes");
                    for (int i = 0; i < sfxFile.Samples.Count; i++)
                    {
                        if (sfxFile.Samples[i].BaseVolume != 0)
                        {
                            outputFile.WriteLine(string.Format(GlobalPrefs.NumericProvider, "BaseVolume  {0:#.#}", sfxFile.Samples[i].BaseVolume));
                        }
                        else
                        {
                            outputFile.WriteLine("BaseVolume  {0:0}", sfxFile.Samples[i].BaseVolume);
                        }
                        if (sfxFile.Samples[i].PitchOffset != 0)
                        {
                            outputFile.WriteLine(string.Format(GlobalPrefs.NumericProvider, "PitchOffset {0:#.#}", sfxFile.Samples[i].PitchOffset));
                        }
                        else
                        {
                            outputFile.WriteLine("PitchOffset {0:0}", sfxFile.Samples[i].PitchOffset);
                        }
                        if (sfxFile.Samples[i].RandomPitch != 0)
                        {
                            outputFile.WriteLine(string.Format(GlobalPrefs.NumericProvider, "RandomPitchOffset  {0:#.#}", sfxFile.Samples[i].RandomPitch));
                        }
                        else
                        {
                            outputFile.WriteLine("RandomPitchOffset  {0:0}", sfxFile.Samples[i].RandomPitch);
                        }
                        if (sfxFile.Samples[i].RandomVolume != 0)
                        {
                            outputFile.WriteLine(string.Format(GlobalPrefs.NumericProvider, "RandomVolumeOffset  {0:#.#}", sfxFile.Samples[i].RandomVolume));
                        }
                        else
                        {
                            outputFile.WriteLine("RandomVolumeOffset  {0:0}", sfxFile.Samples[i].RandomVolume);
                        }
                        if (sfxFile.Samples[i].Pan != 0)
                        {
                            outputFile.WriteLine(string.Format(GlobalPrefs.NumericProvider, "Pan  {0:#.#}", sfxFile.Samples[i].Pan));
                        }
                        else
                        {
                            outputFile.WriteLine("Pan  {0:0}", sfxFile.Samples[i].Pan);
                        }
                        if (sfxFile.Samples[i].RandomPan != 0)
                        {
                            outputFile.WriteLine(string.Format(GlobalPrefs.NumericProvider, "RandomPan  {0:#.#}", sfxFile.Samples[i].RandomPan));
                        }
                        else
                        {
                            outputFile.WriteLine("RandomPan  {0:0}", sfxFile.Samples[i].RandomPan);
                        }
                    }
                    outputFile.WriteLine("#END");
                    outputFile.WriteLine(string.Empty);
                }
                outputFile.WriteLine("#SFXSamplePoolControl");
                outputFile.WriteLine("Action1  {0}", sfxFile.SamplePool.Action1);
                outputFile.WriteLine("RandomPick  {0}", Convert.ToByte(sfxFile.SamplePool.RandomPick));
                outputFile.WriteLine("Shuffled  {0}", Convert.ToByte(sfxFile.SamplePool.Shuffled));
                outputFile.WriteLine("Loop  {0}", Convert.ToByte(sfxFile.SamplePool.isLooped));
                outputFile.WriteLine("Polyphonic  {0}", Convert.ToByte(sfxFile.SamplePool.Polyphonic));
                outputFile.WriteLine("MinDelay  {0}", sfxFile.SamplePool.MinDelay);
                outputFile.WriteLine("MaxDelay  {0}", sfxFile.SamplePool.MaxDelay);
                outputFile.WriteLine("EnableSubSFX  {0}", Convert.ToByte(sfxFile.SamplePool.EnableSubSFX));
                outputFile.WriteLine("EnableStereo  {0}", Convert.ToByte(sfxFile.SamplePool.EnableStereo));
                outputFile.WriteLine("#END");
                outputFile.WriteLine(string.Empty);
                outputFile.WriteLine("#HASHCODE");
                outputFile.WriteLine("HashCodeNumber {0}", sfxFile.HashCode);
                outputFile.WriteLine("#END");
            }

            //Copy file to the final folder
            File.Delete(outputFilePath);
            File.Copy(tmpFilePath, outputFilePath);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
