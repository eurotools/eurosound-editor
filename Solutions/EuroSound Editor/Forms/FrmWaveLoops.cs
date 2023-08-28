using sb_editor.Audio_Classes;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class FrmWaveLoops : Form
    {
        private readonly string outputFilePath;

        //-------------------------------------------------------------------------------------------------------------------------------
        public FrmWaveLoops(string waveFilePath)
        {
            InitializeComponent();
            outputFilePath = waveFilePath;
            Text = String.Format("{0} - Loop Settings", Path.GetFileNameWithoutExtension(waveFilePath));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void FrmWaveLoops_Load(object sender, EventArgs e)
        {
            //Read wave settings
            WaveFunctions wFunctions = new WaveFunctions();
            WavInfo smplChunk = wFunctions.ReadWaveProperties(outputFilePath);

            //Set sample chunk data (if there are)
            chkLoopSettings.Checked = smplChunk.HasLoop;
            nudStartLoop.Value = smplChunk.LoopStart;
            nudEndLoop.Value = smplChunk.LoopEnd;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void FrmWaveLoops_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Ask for confirmation
            if (DialogResult == DialogResult.Cancel)
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to close without saving?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (chkLoopSettings.Checked)
                {
                    try
                    {
                        using (FileStream inputFileStream = File.Open(outputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            uint dataChunkSize;
                            int frequency;
                            byte[] samples;

                            //Read Wave Without any metadata
                            using (BinaryReader bReader = new BinaryReader(inputFileStream))
                            {
                                //Read Sample Rate
                                bReader.BaseStream.Seek(0x18, SeekOrigin.Begin);
                                frequency = bReader.ReadInt32();

                                //Read Data Chunk Size
                                bReader.BaseStream.Seek(0x28, SeekOrigin.Begin);
                                dataChunkSize = bReader.ReadUInt32();

                                //Read Samples
                                bReader.BaseStream.Seek(0, SeekOrigin.Begin);
                                samples = bReader.ReadBytes((int)dataChunkSize + 44);

                                //Close all
                                inputFileStream.Close();
                            }

                            //Write Wav with new metadata
                            using (FileStream outputFileStream = File.Open(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                            {
                                using (BinaryWriter outputFileWriter = new BinaryWriter(outputFileStream))
                                {
                                    //Header and Data Chunk
                                    outputFileWriter.Write(samples);
                                    //LIST Chunk
                                    using (MemoryStream listChunkStream = new MemoryStream())
                                    {
                                        using (BinaryWriter binWriter = new BinaryWriter(listChunkStream))
                                        {
                                            //Header
                                            binWriter.Write(Encoding.ASCII.GetBytes("LIST"));
                                            binWriter.Write(0);
                                            binWriter.Write(Encoding.ASCII.GetBytes("INFO"));

                                            //Software
                                            binWriter.Write(Encoding.ASCII.GetBytes("ISFT"));
                                            Version euroSoundVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor);
                                            byte[] software = Encoding.ASCII.GetBytes(string.Format("{0} {1}", Application.ProductName, euroSoundVersion));
                                            binWriter.Write(software.Length + 1);
                                            binWriter.Write(software);
                                            binWriter.Write((byte)0);
                                            AlignNumber(binWriter, 2);

                                            //Engineer
                                            binWriter.Write(Encoding.ASCII.GetBytes("IENG"));
                                            byte[] engineer = Encoding.ASCII.GetBytes(GlobalPrefs.EuroSoundUser);
                                            binWriter.Write(engineer.Length + 1);
                                            binWriter.Write(engineer);
                                            binWriter.Write((byte)0);
                                            AlignNumber(binWriter, 2);

                                            //Date
                                            binWriter.Write(Encoding.ASCII.GetBytes("ICRD"));
                                            byte[] creationDate = Encoding.ASCII.GetBytes(string.Format("{0:yyyy}-{0:MM}-{0:dd}", DateTime.Now));
                                            binWriter.Write(creationDate.Length + 1);
                                            binWriter.Write(creationDate);
                                            binWriter.Write((byte)0);
                                            AlignNumber(binWriter, 2);

                                            //Write lenght
                                            listChunkStream.Position = 4;
                                            binWriter.Write(listChunkStream.Length - 8);

                                            //Write to the output file
                                            listChunkStream.WriteTo(outputFileStream);
                                        }
                                    }

                                    //SMPL Chunk
                                    using (MemoryStream smpChunkStream = new MemoryStream())
                                    {
                                        using (BinaryWriter binWriter = new BinaryWriter(smpChunkStream))
                                        {
                                            //Write SMPL Chunk
                                            binWriter.Write(Encoding.ASCII.GetBytes("smpl"));
                                            //size
                                            binWriter.Write(0);
                                            //manufacturer (MIDI Manufacturers Association manufacturer code)
                                            binWriter.Write(0);
                                            //product (product / model ID of the target device)
                                            binWriter.Write(0);
                                            //sample period
                                            float samplePeriod = (1.0f / frequency) * 1000000000.0f;
                                            binWriter.Write((uint)samplePeriod);
                                            //MIDI unity note
                                            binWriter.Write(60);
                                            //MIDI pitch fraction
                                            binWriter.Write(0);
                                            //SMPTE format
                                            binWriter.Write(0);
                                            //SMPTE offset
                                            binWriter.Write(0);
                                            //number of sample loops
                                            binWriter.Write(1);
                                            //number sample data
                                            binWriter.Write(0);
                                            //------------------------------------Sample Loop Struct
                                            //ID
                                            binWriter.Write(0);
                                            //Type: 0 means normal forward looping type. A value of 1 means alternating (forward and backward) looping type. A value of 2 means backward looping type
                                            binWriter.Write(0);
                                            //Start
                                            binWriter.Write((uint)nudStartLoop.Value);
                                            //End
                                            if (chkToLastSample.Checked)
                                            {
                                                binWriter.Write(dataChunkSize / 2);
                                            }
                                            else
                                            {
                                                binWriter.Write((uint)nudEndLoop.Value);
                                            }
                                            //Fraction - A value of zero means current resolution. A value of 50 cents (0x80) means ½ sample
                                            binWriter.Write(0);
                                            //Number of times to play the loop
                                            binWriter.Write(0);

                                            //Write Chunk length
                                            smpChunkStream.Position = 4;
                                            binWriter.Write(smpChunkStream.Length - 8);

                                            //Write to the output file
                                            smpChunkStream.WriteTo(outputFileStream);
                                        }
                                    }

                                    //Update new length
                                    outputFileStream.Seek(4, SeekOrigin.Begin);
                                    outputFileWriter.Write((int)outputFileStream.Length - 8);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkLoopSettings_CheckedChanged(object sender, EventArgs e)
        {
            nudStartLoop.Enabled = chkLoopSettings.Checked;
            nudEndLoop.Enabled = chkLoopSettings.Checked;
            chkToLastSample.Enabled = chkLoopSettings.Checked;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void ChkToLastSample_CheckedChanged(object sender, EventArgs e)
        {
            nudEndLoop.Enabled = !chkToLastSample.Checked;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void AlignNumber(BinaryWriter bw, uint blockSize)
        {
            uint PositionAligned = ((uint)bw.BaseStream.Position + (blockSize - 1)) & ~(blockSize - 1);
            while (bw.BaseStream.Position != PositionAligned)
            {
                bw.Write((byte)0x00);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
