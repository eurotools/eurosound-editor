using ESUtils;
using System;
using System.IO;
using static ESUtils.Enumerations;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class SfxOutputForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void BindStreams(string[] filesToBind, Language outputLanguage, string Platform)
        {
            //Get Output Path
            string outputFolder = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", Platform, outputLanguage.ToString(), "Streams");
            Directory.CreateDirectory(outputFolder);

            //Ensure that the output directory exists. 
            string debugfileFolder = Path.Combine(GlobalPrefs.ProjectFolder, "Debug_Report");
            Directory.CreateDirectory(debugfileFolder);

            //MusX Output Path
            string sfxOutputFolder = string.Empty;
            if (Directory.Exists(projectSettings.EngineXProjectPath))
            {
                sfxOutputFolder = Path.Combine(projectSettings.EngineXProjectPath, "Binary", CommonFunctions.GetEnginexFolder(Platform), "audio");
                Directory.CreateDirectory(sfxOutputFolder);
            }

            bool isBigEndian = Platform.Equals("GameCube", StringComparison.OrdinalIgnoreCase);

            //Create Files
            string binaryFile = Path.Combine(outputFolder, "STREAMS.bin");
            string lutFile = Path.Combine(outputFolder, "STREAMS.lut");
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(debugfileFolder, string.Format("StreamList_{0}_{1}.txt", outputLanguage, Platform)), FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                using (BinaryWriter streamsWritter = new BinaryWriter(File.Open(binaryFile, FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    using (BinaryWriter lutWritter = new BinaryWriter(File.Open(lutFile, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        int index = 0;
                        while (index < filesToBind.Length)
                        {
                            //Report progress
                            decimal progress = decimal.Divide(index, filesToBind.Length) * 100;
                            backgroundWorker1.ReportProgress((int)progress, string.Format("Binding {0} Audio Stream Data {1} For {2}", outputLanguage, filesToBind[index], Platform));

                            byte[] markerFileData = File.ReadAllBytes(filesToBind[index++]);
                            byte[] audioFileData = File.ReadAllBytes(filesToBind[index++]);

                            //Align
                            AlignFile(streamsWritter, 0x800);

                            //Offsets and Sizes
                            uint headerStart = (uint)streamsWritter.BaseStream.Position;
                            lutWritter.Write(BytesFunctions.FlipUInt32(headerStart, isBigEndian));
                            streamsWritter.Write(BytesFunctions.FlipInt32(markerFileData.Length, isBigEndian));
                            long audioOffsetPos = streamsWritter.BaseStream.Position;
                            streamsWritter.Write(0);
                            streamsWritter.Write(BytesFunctions.FlipInt32(audioFileData.Length, isBigEndian));

                            //Write Marker File
                            streamsWritter.Write(markerFileData);

                            //Align
                            AlignFile(streamsWritter, 0x800);

                            //Write Audio Start Offset
                            long sampleDataStart = streamsWritter.BaseStream.Position;
                            streamsWritter.BaseStream.Seek(audioOffsetPos, SeekOrigin.Begin);
                            streamsWritter.Write(BytesFunctions.FlipUInt32((uint)sampleDataStart, isBigEndian));
                            streamsWritter.BaseStream.Seek(sampleDataStart, SeekOrigin.Begin);

                            //Write Audio File
                            streamsWritter.Write(audioFileData);

                            //Debug File
                            sw.WriteLine("------------------Stream {0}------------------", (index / 2) - 1);
                            sw.WriteLine("HeaderStart = {0}", headerStart);
                            sw.WriteLine("DataStart = {0}", sampleDataStart);
                            sw.WriteLine(string.Empty);
                            sw.WriteLine("MarkerSize = {0}", markerFileData.Length);
                            sw.WriteLine("SampleDataStart = {0}", sampleDataStart);
                            sw.WriteLine("SampleSize = {0}", audioFileData.Length);
                            sw.WriteLine(string.Empty);
                        }
                    }
                }
            }

            //Create MusX File
            if (!string.IsNullOrEmpty(sfxOutputFolder) && Directory.Exists(sfxOutputFolder))
            {
                string fileName = CommonFunctions.GetSfxName(outputLanguage, "_streamdata");
                MusXBuild_StreamFile.BuildStreamFile(binaryFile, lutFile, Path.Combine(sfxOutputFolder, fileName), CommonFunctions.GetFileHashCode(FileType.StreamFile, outputLanguage, 0), CommonFunctions.GetPlatformLabel(Platform), isBigEndian);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void AlignFile(BinaryWriter bWritter, int blockSize)
        {
            if (bWritter.BaseStream.Position > 0)
            {
                bWritter.Seek(0x7FB, SeekOrigin.Current);
                bWritter.Seek((int)((-bWritter.BaseStream.Position % blockSize + blockSize) % blockSize), SeekOrigin.Current);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
