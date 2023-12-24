//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// SFX Form Output Create Streams
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using ExMarkers;
using sb_editor.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static ESUtils.Enumerations;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        private readonly StreamMarkerFiles streamMarkers = new StreamMarkerFiles();

        //-------------------------------------------------------------------------------------------------------------------------------
        private void OutputStreams(SamplePool samplesList, string[] languages, string debugfileFolder)
        {
            foreach (KeyValuePair<string, PlatformData> platform in projectSettings.platformData)
            {
                int StreamListEnglishCount = -1;
                int StreamListCount;

                for (int i = 0; i < languages.Length; i++)
                {
                    Language outputLanguage = (Language)Enum.Parse(typeof(Language), languages[i], true);

                    //Get Streams List in the current Language
                    string[] streamsList = GetStreamSamples(samplesList, outputLanguage);
                    if (streamsList.Length > 0)
                    {
                        //Ensure that the output directory exists. 
                        string outputFolder = Path.Combine(GlobalPrefs.ProjectFolder, platform.Key + "_Streams", languages[i]);
                        Directory.CreateDirectory(outputFolder);

                        //Copy files
                        HashSet<string> itemsToBind = new HashSet<string>();

                        //Create Debug File
                        using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(debugfileFolder, string.Format("StreamsConverted_{0}_{1}.txt", languages[i], platform.Key)), FileMode.Create, FileAccess.Write, FileShare.Read)))
                        {
                            for (int j = 0; j < streamsList.Length; j++)
                            {
                                //Inform user about the progress
                                double progress = (double)decimal.Divide(j, streamsList.Length) * 100;
                                backgroundWorker1.ReportProgress((int)progress, string.Format("{0} Stream {1} For {2}", languages[i], streamsList[j], platform.Key));

                                //Get Sample Data File And it's header size
                                string audioDataFilePath, statesFilePath;
                                int headerSize;
                                switch (platform.Key.ToLower())
                                {
                                    case "playstation2":
                                        audioDataFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "PlayStation2_VAG", Path.ChangeExtension(streamsList[j], ".vag"));
                                        statesFilePath = string.Empty;
                                        headerSize = 48;
                                        break;
                                    case "gamecube":
                                        audioDataFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "GameCube_Software_adpcm", Path.ChangeExtension(streamsList[j], ".ssp"));
                                        statesFilePath = Path.ChangeExtension(audioDataFilePath, ".smd");
                                        headerSize = 0;
                                        break;
                                    case "pc":
                                        audioDataFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "PC_Software_adpcm", Path.ChangeExtension(streamsList[j], ".ssp"));
                                        statesFilePath = Path.ChangeExtension(audioDataFilePath, ".smd");
                                        headerSize = 0;
                                        break;
                                    default:
                                        audioDataFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "XBox_adpcm", streamsList[j]);
                                        statesFilePath = string.Empty;
                                        headerSize = 48;
                                        break;
                                }

                                //Create Stream Sample Data File
                                if (File.Exists(audioDataFilePath))
                                {
                                    string markerFile = Path.Combine(projectSettings.SampleFilesFolder, "Master", Path.ChangeExtension(streamsList[j], ".mrk"));
                                    if (File.Exists(markerFile))
                                    {
                                        //Ensure that the output folder exists
                                        Directory.CreateDirectory(outputFolder);

                                        //Add item to list
                                        string sampleDataPath = Path.Combine(outputFolder, j + ".ssd");
                                        File.WriteAllBytes(sampleDataPath, CommonFunctions.RemoveFileHeader(audioDataFilePath, headerSize));

                                        //Read Marker File
                                        MarkerTextFile[] markersData = TextFiles.ReadMarkerFile(markerFile);
                                        UpdateMarkerPositions(platform.Key, markersData, statesFilePath);

                                        //Write Marker File
                                        string markerDataPath = Path.ChangeExtension(sampleDataPath, ".smf");
                                        streamMarkers.BuildBinaryFile(markersData, 100, markerDataPath, platform.Key.Equals("GameCube"));

                                        //Add items to list
                                        itemsToBind.Add(markerDataPath);
                                        itemsToBind.Add(sampleDataPath);

                                        //Log
                                        sw.WriteLine("InputFile = {0}", audioDataFilePath);
                                        sw.WriteLine("OutputFileName = {0}", sampleDataPath);
                                    }
                                    else
                                    {
                                        //Should never happen, just in case ¯\_(ツ)_/¯
                                        Invoke(method: new Action(() => { MessageBox.Show(string.Format("OutputPlatformMakerFile(). File Not Here: {0} Try Full Output!", audioDataFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                                    }
                                }
                                else
                                {
                                    //Should never happen, just in case ¯\_(ツ)_/¯
                                    Invoke(method: new Action(() => { MessageBox.Show(string.Format("ReSampleStreams. File Not Here: {0}", audioDataFilePath), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                                }
                            }
                        }

                        //Get count of the current list
                        StreamListCount = itemsToBind.Count;

                        //Save English Streams List Count
                        if (outputLanguage == Language.English)
                        {
                            StreamListEnglishCount = itemsToBind.Count;
                        }
                        if (StreamListEnglishCount > 0 && StreamListEnglishCount != StreamListCount)
                        {
                            Invoke(method: new Action(() => { MessageBox.Show("Not StreamList.ListCount = StreamListEnglish.ListCount", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                        }

                        //Bind Streams into one single file
                        backgroundWorker1.ReportProgress(100, string.Format("Binding {0} Audio Stream Data For {1}", languages[i], platform.Key));
                        BindStreams(itemsToBind.ToArray(), outputLanguage, platform.Key);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private string[] GetStreamSamples(SamplePool samplesList, Language Language = Language.English)
        {
            HashSet<string> streamSamples = new HashSet<string>();
            foreach (KeyValuePair<string, SamplePoolItem> sample in samplesList.SamplePoolItems)
            {
                if (sample.Value.StreamMe)
                {
                    string filePath = CommonFunctions.GetSampleFromSpeechFolder(sample.Key.TrimStart(Path.DirectorySeparatorChar), Language);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        streamSamples.Add(filePath);
                    }
                }
            }

            string[] streamArray = streamSamples.ToArray();
            Array.Sort(streamArray);

            return streamArray;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void UpdateMarkerPositions(string outputPlatform, MarkerTextFile[] markersList, string smdFilePath)
        {
            //Calculate states -- PC & GameCube Platform
            if (outputPlatform.Equals("PC", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("GameCube", StringComparison.OrdinalIgnoreCase))
            {
                //Update Markers states
                foreach (MarkerTextFile marker in markersList)
                {
                    if (marker.Position > 0)
                    {
                        uint state = 0;
                        using (BinaryReader breader = new BinaryReader(File.Open(smdFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                        {
                            long offset = (marker.Position / 256) * 256;
                            if (offset <= breader.BaseStream.Length)
                            {
                                breader.BaseStream.Seek(offset, SeekOrigin.Begin);
                                state = breader.ReadUInt32();
                            }
                            marker.ImaStateA = state;
                            marker.ImaStateB = state;
                        }
                        break;
                    }
                }

                //Start markers
                foreach (MarkerTextFile marker in markersList)
                {
                    //Calculate VAG offsets
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetStreamLoopOffsetPCandGC(marker.Position);
                    }
                }
            }

            //Update Positions PS2 Platform
            if (outputPlatform.Equals("PlayStation2", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (MarkerTextFile marker in markersList)
                {
                    //Calculate VAG offsets
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetStreamLoopOffsetPlayStation2(marker.Position);
                    }
                }
            }

            //Update positions for Xbox
            if (outputPlatform.Equals("Xbox", StringComparison.OrdinalIgnoreCase) || outputPlatform.Equals("X Box", StringComparison.OrdinalIgnoreCase))
            {
                //Start markers
                foreach (MarkerTextFile marker in markersList)
                {
                    //Calculate VAG offsets
                    if (marker.Position > 0)
                    {
                        marker.Position = CalculusLoopOffset.GetStreamLoopOffsetXbox(marker.Position);
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
