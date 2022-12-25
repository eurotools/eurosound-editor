using NAudio.Wave;
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
    public class PCAudioDLL
    {
        //Local variables and classes
        private int sbHashCode = -1;
        private readonly WaveOut _waveOut = new WaveOut();

        //Sb Data
        private readonly SortedDictionary<uint, Sample> sfxSamples = new SortedDictionary<uint, Sample>();
        private readonly List<SampleData> sfxStoredData = new List<SampleData>();

        //Main Thread
        public readonly PCVoices pcOutVoices = new PCVoices();
        public DebugConsole outputConsole = new DebugConsole();
        private Thread _thread;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCAudioDLL()
        {
            //Initialize game reserved voices
            for (int i = 0; i < pcOutVoices.VoicesArray.Length; i++)
            {
                if (i < 10)
                {
                    pcOutVoices.VoicesArray[i] = new ExWaveOut
                    {
                        Active = true,
                        Looping = true,
                        Locked = true
                    };
                }
                else
                {
                    pcOutVoices.VoicesArray[i] = new ExWaveOut();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public double LoadSoundBank(string soundBankPath)
        {
            Stopwatch watch = Stopwatch.StartNew();

            //Add Debug Text
            outputConsole.WriteLine("");
            outputConsole.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            outputConsole.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            outputConsole.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            outputConsole.WriteLine("");
            outputConsole.WriteLine("CMD_SFXLOADSOUNDBANK");

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
            sfxStoredData.TrimExcess();

            sbHashCode = -1;
            outputConsole.WriteLine("ES-> ES_UnLoadSoundBankReleaseFinished End");
            outputConsole.WriteLine("pih");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void PlaySound()
        {
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(PlayHashCode)
                {
                    IsBackground = true
                };
                _thread.Start();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void PlayHashCode()
        {
            AudioPlayer audioPlayer = new AudioPlayer();

            //Look hashcodes
            foreach (KeyValuePair<uint, Sample> soundToCheck in sfxSamples)
            {
                if (soundToCheck.Key == 0 && ((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.HasSubSfx) & 1) == 0 && soundToCheck.Value.samplesList.Count > 0)
                {
                    //If false it will pick and play randomly one of the samples in the list. 
                    if (((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.MultiSample) & 1) == 0 || sfxStoredData[0].Flags == 1)
                    {
                        audioPlayer.PlaySingleSfx(outputConsole, _waveOut, pcOutVoices, soundToCheck.Value, sfxStoredData);
                    }
                    else
                    {

                        //It will interpret the list of samples
                        if (((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.MultiSample) & 1) == 1)
                        {
                            if (((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.Polyphonic) & 1) == 1)
                            {
                                audioPlayer.PlayPolyphonic(outputConsole, _waveOut, pcOutVoices, soundToCheck.Value, sfxStoredData);
                            }
                            else
                            {
                                bool shuffled = ((soundToCheck.Value.Flags >> (int)SoundBanksReader.Flags.Shuffled) & 1) == 1;
                                audioPlayer.PlayList(outputConsole, _waveOut, shuffled, pcOutVoices, soundToCheck.Value, sfxStoredData);
                            }
                        }
                    }
                }
                else
                {
                    outputConsole.WriteLine("Sub SFXs not supported!");
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void StopSounds()
        {
            //Stop Output Player
            if (_waveOut != null)
            {
                _waveOut.Stop();
            }

            //Close Thread
            if (_thread != null)
            {
                _thread.Abort();
            }

            //Stop all voices
            pcOutVoices.CloseAllVoices(outputConsole);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void InitializeConsole(TextBox outputControl)
        {
            outputConsole.TxtConsole = outputControl;
            outputConsole.WriteLine("Debug Console Initialised!");
            outputConsole.WriteLine("5.1 Mixer Initialise");
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
