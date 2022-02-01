using sb_explorer.AudioDecoders;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace sb_explorer
{
    public partial class ADPCMValidator : Form
    {
        //*===============================================================================================
        //* GLOBAL VARIABLES 
        //*===============================================================================================
        private readonly ArrayList StreamFileDictionaryData;

        public ADPCMValidator(ArrayList streamDictionary)
        {
            InitializeComponent();
            StreamFileDictionaryData = streamDictionary;
        }

        //*===============================================================================================
        //* FORM EVENTS
        //*===============================================================================================
        private void ADPCMValidator_Shown(object sender, EventArgs e)
        {
            //Set status bar params
            ProgressBar_Validation.Maximum = StreamFileDictionaryData.Count;
            ProgressBar_Validation.Step = 10;

            //Decoders
            ImaAdpcm imaFunctions = new ImaAdpcm();
            SonyAdpcm sonyAdpcmFunctions = new SonyAdpcm();
            XboxAdpcm xboxAdpcmFunctions = new XboxAdpcm();

            //Loop through items
            try
            {
                for (int i = 0; i < StreamFileDictionaryData.Count; i++)
                {
                    //Update progress
                    ProgressBar_Validation.Value = i;

                    //Get item from list
                    ListViewItem listViewItem = ((Frm_MainFrame)Application.OpenForms["Frm_MainFrame"]).ListView_StreamData.Items[i];
                    ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems[1];
                    listViewItem.UseItemStyleForSubItems = false;

                    //Get item from dictionary
                    EXSoundStream soundToCheck = StreamFileDictionaryData[i] as EXSoundStream;
                    try
                    {
                        //Decode audio
                        if (GlobalVariables.StreamFilePlatform == (byte)GenericFunctions.CurrentPlatform.PC || GlobalVariables.StreamFilePlatform == (byte)GenericFunctions.CurrentPlatform.GC)
                        {
                            soundToCheck.SampleParsedData = AudioFunctions.ShortArrayToByteArray(imaFunctions.Decode(soundToCheck.SampleByteData, soundToCheck.SampleByteData.Length * 2));
                            listViewSubItem.Text = "OK";
                            listViewSubItem.ForeColor = SystemColors.ControlText;
                        }
                        else if (GlobalVariables.StreamFilePlatform == (byte)GenericFunctions.CurrentPlatform.PS2)
                        {
                            soundToCheck.SampleParsedData = sonyAdpcmFunctions.Decode(soundToCheck.SampleByteData);
                            listViewSubItem.Text = "OK";
                            listViewSubItem.ForeColor = SystemColors.ControlText;
                        }
                        else if (GlobalVariables.StreamFilePlatform == (byte)GenericFunctions.CurrentPlatform.XBX)
                        {
                            soundToCheck.SampleParsedData = xboxAdpcmFunctions.Decode(soundToCheck.SampleByteData);
                            listViewSubItem.Text = "OK";
                            listViewSubItem.ForeColor = SystemColors.ControlText;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Update listview item
                        listViewSubItem.Text = "INVALID";
                        listViewSubItem.ForeColor = Color.Red;

                        //Throw exception
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                //Inform user
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }
}
