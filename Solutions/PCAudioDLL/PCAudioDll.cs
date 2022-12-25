using PCAudioDLL.AudioClasses;
using PCAudioDLL.MusXStuff;
using PCAudioDLL.MusXStuff.Objects;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class PCAudioDll
    {
        //PC Voices
        public static VoiceItem[] pcAudioVoices = new VoiceItem[61];
        private readonly PCVoices audioPlayer = new PCVoices(pcAudioVoices);
        private bool canPlayAgain = true;
        private int sbHashCode = -1;
        private Thread playThread;

        //Sb Data
        private readonly SortedDictionary<uint, Sample> sfxSamples = new SortedDictionary<uint, Sample>();
        private readonly List<SampleData> sfxStoredData = new List<SampleData>();

        //Debug List
        public TextBoxTraceListener tbl;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCAudioDll()
        {
            for (int i = 0; i < pcAudioVoices.Length; i++)
            {
                pcAudioVoices[i] = new VoiceItem();
            }

            for (int i = 0; i < 10; i++)
            {
                pcAudioVoices[i].Active = true;
                pcAudioVoices[i].Looping = true;
                pcAudioVoices[i].Locked = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public double LoadSoundBank(string soundBankPath)
        {
            Stopwatch watch = Stopwatch.StartNew();

            //Add Debug Text
#if DEBUG
            Debug.WriteLine("");
            Debug.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            Debug.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            Debug.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            Debug.WriteLine("");
            Debug.WriteLine("CMD_SFXLOADSOUNDBANK");
#elif TRACE
            Trace.WriteLine("");
            Trace.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            Trace.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            Trace.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            Trace.WriteLine("");
            Trace.WriteLine("CMD_SFXLOADSOUNDBANK");
#endif

            //Call reader
            SoundBanksReader reader = new SoundBanksReader();

            //Read SoundBank
            SfxHeaderData soundBankHeaderData = reader.ReadSfxHeader(soundBankPath);
            reader.ReadSoundbank(soundBankPath, soundBankHeaderData, sfxSamples, sfxStoredData);

            //Update global var.
            sbHashCode = (int)soundBankHeaderData.FileHashCode;

            return watch.Elapsed.TotalMilliseconds;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public bool IsSoundBankLoaded(int hashcode)
        {
            return sbHashCode == hashcode;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void UnloadSoundbank()
        {
            sfxSamples.Clear();
            sfxStoredData.Clear();
            sbHashCode = -1;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void PlaySound()
        {
            if (canPlayAgain)
            {
                playThread = new Thread(new ThreadStart(PlaySfx))
                {
                    IsBackground = true
                };
                playThread.Start();
                canPlayAgain = false;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void PlaySfx()
        {
            //Look hashcodes
            foreach (KeyValuePair<uint, Sample> soundToCheck in sfxSamples)
            {
                if (soundToCheck.Key == 0 && ((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.HasSubSfx) & 1) == 0 && soundToCheck.Value.samplesList.Count > 0)
                {
                    bool loopFlag = ((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.Loop) & 1) == 1;

                    //If false it will pick and play randomly one of the samples in the list. 
                    if (((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.MultiSample) & 1) == 0 || sfxStoredData[0].Flags == 1)
                    {
                        SampleInfo sampleInfo = soundToCheck.Value.samplesList[Utils.GetRandomInt(soundToCheck.Value.samplesList.Count)];
                        SampleData sampleData = sfxStoredData[sampleInfo.FileRef];

                        audioPlayer.PlaySingleSfx(sampleInfo, sampleData, soundToCheck.Value.MinDelay, soundToCheck.Value.MaxDelay);
                    }
                    else
                    {
                        //It will interpret the list of samples
                        if (((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.MultiSample) & 1) == 1)
                        {
                            if (((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.Polyphonic) & 1) == 1)
                            {
                                audioPlayer.PlayPolyphonic(soundToCheck.Value, sfxStoredData, soundToCheck.Value.MinDelay, soundToCheck.Value.MaxDelay);
                            }
                            else
                            {
                                bool shuffled = ((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.Shuffled) & 1) == 1;
                                audioPlayer.PlayList(soundToCheck.Value, sfxStoredData, shuffled, soundToCheck.Value.MinDelay, soundToCheck.Value.MaxDelay);
                            }
                        }

                        //Call again if loops
                        if (loopFlag)
                        {
                            PlaySfx();
                        }
                        else
                        {
                            canPlayAgain = true;
                        }
                    }
                }
                else
                {
#if DEBUG
                    Debug.Write("Sub SFXs not supported!");
#elif TRACE
                    Trace.Write("Sub SFXs not supported!");
#endif
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void StopAudio()
        {
            if (playThread != null)
            {
                audioPlayer.StopAudioPlayer();
                playThread.Abort();
                canPlayAgain = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void InitializeConsole(TextBox outputControl)
        {
            tbl = new TextBoxTraceListener(outputControl);
#if DEBUG
            Debug.Listeners.Add(tbl);
            Debug.WriteLine("Debug Console Initialised!");
            Debug.WriteLine("5.1 Mixer Initialise");
#elif TRACE
            Trace.Listeners.Add(tbl);
            Trace.WriteLine("Debug Console Initialised!");
            Trace.WriteLine("5.1 Mixer Initialise");
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void DebugConsoleState(bool pauseDebugOutput)
        {
            tbl.PauseOutput = pauseDebugOutput;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
