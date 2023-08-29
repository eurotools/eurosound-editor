//-------------------------------------------------------------------------------------------------------------------------------
//  __  __            _                   ______    _ _ _             
// |  \/  |          | |                 |  ____|  | (_) |            
// | \  / | __ _ _ __| | _____ _ __ ___  | |__   __| |_| |_ ___  _ __ 
// | |\/| |/ _` | '__| |/ / _ \ '__/ __| |  __| / _` | | __/ _ \| '__|
// | |  | | (_| | |  |   <  __/ |  \__ \ | |___| (_| | | || (_) | |   
// |_|  |_|\__,_|_|  |_|\_\___|_|  |___/ |______\__,_|_|\__\___/|_|   
//
//-------------------------------------------------------------------------------------------------------------------------------
// Main Form
//-------------------------------------------------------------------------------------------------------------------------------
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarkersEditor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Frm_Main : Form
    {
        private readonly List<TrackChunk> markers = new List<TrackChunk>();
        private MidiFile midiFile = new MidiFile();
        private TempoMap tempoMap;

        //-------------------------------------------------------------------------------------------------------------------------------
        public Frm_Main()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(110), new TimeSignature(1, 4));
            midiFile.ReplaceTempoMap(tempoMap);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_AddMarker_End_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputBox = new Frm_InputBox())
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    //Get values
                    string markerText = inputBox.Textbox_MarkerName.Text;
                    int milliseconds = (int)inputBox.Numeric_Milliseconds.Value;

                    //Create Note
                    Pattern endMarker = new PatternBuilder()
                    .MoveToTime(new MetricTimeSpan(0, 0, 0, milliseconds))
                    .Note(Octave.Get(3).C, MusicalTimeSpan.Quarter)
                    .Build();

                    //Add Marker to list
                    lvwMarkers.Items.Add(new ListViewItem(new string[] { milliseconds.ToString(), "END", markerText }));
                    markers.Add(endMarker.ToTrackChunk(tempoMap, (FourBitNumber)0));
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_AddMarker_Jump_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputBox = new Frm_InputBox())
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    //Get values
                    string markerText = inputBox.Textbox_MarkerName.Text;
                    int milliseconds = (int)inputBox.Numeric_Milliseconds.Value;

                    //Create Note
                    Pattern jumpMarker = new PatternBuilder()
                    .MoveToTime(new MetricTimeSpan(0, 0, 0, milliseconds))
                    .Note(Octave.Get(4).C, MusicalTimeSpan.Quarter)
                    .Build();
                    TrackChunk jumpMarkerChunk = jumpMarker.ToTrackChunk(tempoMap, (FourBitNumber)0);

                    //Create Marker
                    if (!string.IsNullOrWhiteSpace(markerText))
                    {
                        TextEvent textMarkerEvent = new TextEvent(markerText);
                        jumpMarkerChunk.Events.Insert(1, textMarkerEvent);
                    }

                    //Add Marker to list
                    lvwMarkers.Items.Add(new ListViewItem(new string[] { milliseconds.ToString(), "JUMP", markerText }));
                    markers.Add(jumpMarkerChunk);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_AddMarker_Loop_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputBox = new Frm_InputBox())
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    //Get values
                    string markerText = inputBox.Textbox_MarkerName.Text;
                    int milliseconds = (int)inputBox.Numeric_Milliseconds.Value;

                    //Create Note
                    Pattern loopMarker = new PatternBuilder()
                    .MoveToTime(new MetricTimeSpan(0, 0, 0, milliseconds))
                    .Note(Octave.Get(5).C, MusicalTimeSpan.Quarter)
                    .Build();
                    TrackChunk loopMarkerChunk = loopMarker.ToTrackChunk(tempoMap, (FourBitNumber)0);

                    //Create Marker
                    if (!string.IsNullOrWhiteSpace(markerText))
                    {
                        TextEvent textMarkerEvent = new TextEvent(markerText);
                        loopMarkerChunk.Events.Insert(1, textMarkerEvent);
                    }

                    //Add Marker to list
                    lvwMarkers.Items.Add(new ListViewItem(new string[] { milliseconds.ToString(), "LOOP", markerText }));
                    markers.Add(loopMarkerChunk);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_AddMarker_Start_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputBox = new Frm_InputBox())
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    //Get values
                    string markerText = inputBox.Textbox_MarkerName.Text;
                    int milliseconds = (int)inputBox.Numeric_Milliseconds.Value;

                    //Create Note
                    Pattern startPattern = new PatternBuilder()
                    .MoveToTime(new MetricTimeSpan(0, 0, 0, milliseconds))
                    .Note(Octave.Get(6).C, MusicalTimeSpan.Quarter)
                    .Build();
                    TrackChunk startMarkerChunk = startPattern.ToTrackChunk(tempoMap, (FourBitNumber)0);

                    //Create Marker
                    if (!string.IsNullOrWhiteSpace(markerText))
                    {
                        TextEvent textMarkerEvent = new TextEvent(markerText);
                        startMarkerChunk.Events.Insert(1, textMarkerEvent);
                    }

                    //Add Marker to list
                    lvwMarkers.Items.Add(new ListViewItem(new string[] { milliseconds.ToString(), "START", markerText }));
                    markers.Add(startMarkerChunk);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_AddMarker_Pause_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputBox = new Frm_InputBox())
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    //Get values
                    string markerText = inputBox.Textbox_MarkerName.Text;
                    int milliseconds = (int)inputBox.Numeric_Milliseconds.Value;

                    //Create Note
                    Pattern pauseMarker = new PatternBuilder()
                    .MoveToTime(new MetricTimeSpan(0, 0, 0, milliseconds))
                    .Note(Octave.Get(4).F, MusicalTimeSpan.Quarter)
                    .Build();
                    TrackChunk jumpMarkerChunk = pauseMarker.ToTrackChunk(tempoMap, (FourBitNumber)0);

                    //Create Marker
                    if (!string.IsNullOrWhiteSpace(markerText))
                    {
                        TextEvent textMarkerEvent = new TextEvent(markerText);
                        jumpMarkerChunk.Events.Insert(1, textMarkerEvent);
                    }

                    //Add Marker to list
                    lvwMarkers.Items.Add(new ListViewItem(new string[] { milliseconds.ToString(), "PAUSE", markerText }));
                    markers.Add(jumpMarkerChunk);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_AddMarker_Goto_Click(object sender, EventArgs e)
        {
            using (Frm_InputBox inputBox = new Frm_InputBox())
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    //Get values
                    string markerText = inputBox.Textbox_MarkerName.Text;
                    int milliseconds = (int)inputBox.Numeric_Milliseconds.Value;

                    //Create Note
                    Pattern gotoMarker = new PatternBuilder()
                    .MoveToTime(new MetricTimeSpan(0, 0, 0, milliseconds))
                    .Note(Octave.Get(5).F, MusicalTimeSpan.Quarter)
                    .Build();
                    TrackChunk gotoMarkerChunk = gotoMarker.ToTrackChunk(tempoMap, (FourBitNumber)0);

                    //Create Marker
                    if (!string.IsNullOrWhiteSpace(markerText))
                    {
                        TextEvent textMarkerEvent = new TextEvent(markerText);
                        gotoMarkerChunk.Events.Insert(1, textMarkerEvent);
                    }

                    //Add Marker to list
                    lvwMarkers.Items.Add(new ListViewItem(new string[] { milliseconds.ToString(), "GOTO", markerText }));
                    markers.Add(gotoMarkerChunk);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (lvwMarkers.SelectedItems.Count > 0)
            {
                markers.RemoveAt(lvwMarkers.SelectedIndices[0]);
                lvwMarkers.SelectedItems[0].Remove();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_ClearList_Click(object sender, EventArgs e)
        {
            //Clear ListView and clear list
            lvwMarkers.Items.Clear();
            markers.Clear();
            //New Midi File
            midiFile = new MidiFile();
            tempoMap = midiFile.GetTempoMap();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_SaveMidiFile_Click(object sender, EventArgs e)
        {
            //Write file
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Add all chunks to file
                for (int i = 0; i < markers.Count; i++)
                {
                    midiFile.Chunks.Add(markers[i]);
                }
                midiFile.Write(SaveFileDialog.FileName, true, MidiFileFormat.MultiTrack);

                //Inform User
                MessageBox.Show("File Saved Successfully!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
