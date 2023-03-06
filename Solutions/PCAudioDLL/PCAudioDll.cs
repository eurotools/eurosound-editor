using NAudio.Wave;
using PCAudioDLL.Audio_Stuff;
using PCAudioDLL.MusX_Objects;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PCAudioDLL
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class PCAudioDll
    {
        public static SoundbankHeader soundBankHeaderData = new SoundbankHeader();
        public static SortedDictionary<uint, Sample> sfxSamples;
        public static List<SampleData> sfxStoredData;
        public static DebugConsole outputConsole = new DebugConsole();
        public static readonly PCVoices pcOutVoices = new PCVoices();
        private static readonly WaveOut _waveOut = new WaveOut();
        private static int sbHashCode = -1;
        internal static bool StopSfx = false;

        //-------------------------------------------------------------------------------------------------------------------------------
        public static double LoadSoundBank(string soundBankPath)
        {
            Stopwatch watch = Stopwatch.StartNew();

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

            //Add Debug Text
            outputConsole.WriteLine("");
            outputConsole.WriteLine(string.Format("CMD_SFX_INITIALISE : {0}", Path.GetDirectoryName(soundBankPath)));
            outputConsole.WriteLine(string.Format("CMD_SFX_INITIALISE2 : {0}", Path.GetFileName(soundBankPath)));
            outputConsole.WriteLine(string.Format("AsyncOpenFile : {0}", soundBankPath));
            outputConsole.WriteLine("");
            outputConsole.WriteLine("CMD_SFXLOADSOUNDBANK");

            //Read SoundBank
            SoundBankReader reader = new SoundBankReader();
            sfxSamples = new SortedDictionary<uint, Sample>();
            sfxStoredData = new List<SampleData>();

            //Load data
            soundBankHeaderData = reader.ReadSfxHeader(soundBankPath, "PC");
            reader.ReadSoundBank(soundBankPath, soundBankHeaderData, sfxSamples, sfxStoredData, null);

            //Update global var.
            sbHashCode = (int)soundBankHeaderData.FileHashCode;

            return watch.Elapsed.TotalMilliseconds;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void UnloadSoundbank()
        {
            StopSfxTest();
            if (sfxSamples != null)
            {
                sfxSamples.Clear();
            }
            if (sfxStoredData != null)
            {
                sfxStoredData.Clear();
            }

            sbHashCode = -1;
            outputConsole.WriteLine("ES-> ES_UnLoadSoundBankReleaseFinished End");
            outputConsole.WriteLine("pih");
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static bool IsSoundBankLoaded(int hashcode)
        {
            return sbHashCode == hashcode;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void PlaySfx(uint hashCode, int lowPassFilter = -1)
        {
            StopSfx = false;
            if (_waveOut.PlaybackState != PlaybackState.Playing)
            {
                AudioPlayer audioPlayer = new AudioPlayer();
                if (sfxSamples.ContainsKey(hashCode))
                {
                    Sample sfxSample = sfxSamples[hashCode];
                    if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.HasSubSfx) & 1) == 0 && sfxSample.samplesList.Count > 0)
                    {
                        //If false it will pick and play randomly one of the samples in the list. 
                        if (((sfxSample.Flags >> (int)SoundBankReader.OldFlags.MultiSample) & 1) == 0)
                        {
                            audioPlayer.PlaySingleSfx(_waveOut, sfxSample, sfxStoredData, lowPassFilter);
                        }
                        else
                        {
                            audioPlayer.PlayMultiSampleSFX(_waveOut, sfxSample, sfxStoredData, lowPassFilter);
                        }
                    }
                    else
                    {
                        outputConsole.WriteLine("Sub SFXs not supported!");
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void StopSfxTest()
        {
            StopSfx = true;
            if (_waveOut != null)
            {
                _waveOut.Stop();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void InitializeConsole(TextBox outputControl)
        {
            outputConsole.TxtConsole = outputControl;
            outputConsole.WriteLine("Debug Console Initialised!");
            outputConsole.WriteLine("5.1 Mixer Initialise");
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
