using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PCAudioDLL.MusXStuff.Objects;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PCAudioDLL.AudioClasses
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class PCVoices
    {
        private readonly VoiceItem[] pcAudioVoices;
        private readonly WaveOut waveout = new WaveOut();
        private readonly AudioPlayback audioPlayFunctions = new AudioPlayback();
        private int pcAudioVoicesIndex = 9;

        //-------------------------------------------------------------------------------------------------------------------------------
        public PCVoices(VoiceItem[] pcVoicesMatrix)
        {
            pcAudioVoices = pcVoicesMatrix;
            waveout.PlaybackStopped += WaveOutStop;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void WaveOutStop(object sender, StoppedEventArgs e)
        {
            CloseAllVoices();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void StopAudioPlayer()
        {
            //Stop Player
            if (waveout != null && waveout.PlaybackState == PlaybackState.Playing)
            {
                waveout.Stop();

                //Close all voices
                CloseAllVoices();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlaySingleSfx(SampleInfo sampleInfo, SampleData sampleData, int minDelay, int maxDelay)
        {
            //Stop previous audios that could be playing
            StopAudioPlayer();

            //Get Voice
            ActivateVoice(GetVoiceIndex(), sampleData.Flags == 1);

            //Build Wav file
            waveout.Init(audioPlayFunctions.GetWaveProvider(sampleData, sampleInfo, minDelay, maxDelay));
            waveout.Play();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayList(Sample sfxSample, List<SampleData> sfxStoredData, bool shuffled, int minDelay, int maxDelay)
        {
            //Stop previous audios that could be playing
            StopAudioPlayer();

            //Shuffle list if required
            if (shuffled)
            {
                sfxSample.samplesList.Shuffle();
            }

            //Create a wave format
            WaveFormat waveFormat = new WaveFormat(44100, 16, 1);

            //Parse each item of the list
            foreach (SampleInfo sampleInfo in sfxSample.samplesList)
            {
                SampleData sampleData = sfxStoredData[sampleInfo.FileRef];

                //Create Sample Provider
                IWaveProvider sampleProv = audioPlayFunctions.GetWaveProvider(sampleData, sampleInfo, minDelay, maxDelay);

                //Ensure that all has the same format
                if (sampleProv.WaveFormat.SampleRate != 44100 || sampleProv.WaveFormat.Channels != 1)
                {
                    sampleProv = new MediaFoundationResampler(sampleProv, waveFormat) { ResamplerQuality = 60 };
                }

                //Write wave to a stream
                using (MemoryStream outBuff = new MemoryStream())
                {
                    //Get Voice
                    ActivateVoice(GetVoiceIndex(), false);

                    //Start reading
                    WaveFileWriter.WriteWavFileToStream(outBuff, sampleProv);
                    outBuff.Position = 0;

                    //Initialize buffered Stream
                    BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(waveFormat)
                    {
                        ReadFully = false
                    };
                    bufferedWaveProvider.ClearBuffer();
                    waveout.Init(bufferedWaveProvider);

                    //Get Pcm Data
                    using (WaveFileReader wReader = new WaveFileReader(outBuff))
                    {
                        byte[] pcmData = new byte[wReader.Length];
                        wReader.Read(pcmData, 0, pcmData.Length);

                        //Add data to stream
                        bufferedWaveProvider.BufferLength = pcmData.Length;
                        bufferedWaveProvider.AddSamples(pcmData, 0, pcmData.Length);
                    }

                    //Start playing
                    waveout.Play();
                    while (bufferedWaveProvider.BufferedBytes > 0)
                    {

                    }
                    PreCloseVoice();
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PlayPolyphonic(Sample sfxSample, List<SampleData> sfxStoredData, int minDelay, int maxDelay)
        {
            //Stop previous audios that could be playing
            StopAudioPlayer();

            //Create a wave format
            WaveFormat waveFormat = new WaveFormat(44100, 16, 1);

            ISampleProvider[] sampleProvider = new ISampleProvider[sfxSample.samplesList.Count];
            for (int i = 0; i < sfxSample.samplesList.Count; i++)
            {
                SampleInfo sampleInfo = sfxSample.samplesList[i];
                SampleData sampleData = sfxStoredData[sampleInfo.FileRef];

                //Create Sample Provider
                IWaveProvider sampleProv = audioPlayFunctions.GetWaveProvider(sampleData, sampleInfo, minDelay, maxDelay);

                //Ensure that all has the same format
                if (sampleProv.WaveFormat.SampleRate != 44100 || sampleProv.WaveFormat.Channels != 1)
                {
                    sampleProv = new MediaFoundationResampler(sampleProv, waveFormat) { ResamplerQuality = 60 };
                }
                sampleProvider[i] = sampleProv.ToSampleProvider();
            }
            IWaveProvider mixedWave = new MixingSampleProvider(sampleProvider).ToWaveProvider();

            //Write wave to a stream
            using (MemoryStream outBuff = new MemoryStream())
            {
                //Get Voice
                ActivateVoice(GetVoiceIndex(), false);

                //Start reading
                WaveFileWriter.WriteWavFileToStream(outBuff, mixedWave.ToSampleProvider().ToWaveProvider16());
                outBuff.Position = 0;

                //Initialize buffered Stream
                BufferedWaveProvider bufferedWaveProvider = new BufferedWaveProvider(waveFormat)
                {
                    ReadFully = false
                };
                bufferedWaveProvider.ClearBuffer();
                waveout.Init(bufferedWaveProvider);

                //Get Pcm Data
                using (WaveFileReader wReader = new WaveFileReader(outBuff))
                {
                    byte[] pcmData = new byte[wReader.Length];
                    wReader.Read(pcmData, 0, pcmData.Length);

                    //Add data to stream
                    bufferedWaveProvider.BufferLength = pcmData.Length;
                    bufferedWaveProvider.AddSamples(pcmData, 0, pcmData.Length);
                }

                //Start playing
                waveout.Play();
                while (bufferedWaveProvider.BufferedBytes > 0)
                {

                }
                PreCloseVoice();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void ActivateVoice(int index, bool looping)
        {
            pcAudioVoices[index].Active = true;
            pcAudioVoices[index].Played = true;
            pcAudioVoices[index].Playing = true;
            pcAudioVoices[index].Looping = looping;
            pcAudioVoices[index].Reverb = true;
            pcAudioVoices[index].Locked = true;

#if DEBUG
            Debug.WriteLine(string.Format("ES-> ES_RequestVoiceHandle() = {0}", index));
            Debug.WriteLine("Voice::Play");
#elif TRACE
            Trace.WriteLine(string.Format("ES-> ES_RequestVoiceHandle() = {0}", index));
            Trace.WriteLine("Voice::Play");
#endif
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void PreCloseVoice()
        {
            for (int i = 10; i < pcAudioVoices.Length; i++)
            {
                if (pcAudioVoices[i].Active)
                {
                    pcAudioVoices[i].Played = false;
                    pcAudioVoices[i].Playing = false;
                    pcAudioVoices[i].Looping = false;
                    pcAudioVoices[i].Reverb = false;
                    pcAudioVoices[i].Stop = true;
                    pcAudioVoices[i].Locked = true;
#if DEBUG
                    Debug.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", i));
                    Debug.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", i));
#elif TRACE
                    Trace.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", i));
                    Trace.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", i));
#endif
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal void CloseAllVoices()
        {
            for (int i = 10; i < pcAudioVoices.Length; i++)
            {
                if (pcAudioVoices[i].Active)
                {
                    pcAudioVoices[i].Active = false;
                    pcAudioVoices[i].Played = false;
                    pcAudioVoices[i].Playing = false;
                    pcAudioVoices[i].Looping = false;
                    pcAudioVoices[i].Reverb = false;
                    pcAudioVoices[i].Stop = false;
                    pcAudioVoices[i].Stopped = false;
                    pcAudioVoices[i].Locked = false;
#if DEBUG
                    Debug.WriteLine("ES-> ES_SFXRemove()");
                    Debug.WriteLine(string.Format("ES-> psiSampleKeyOff( {0} )", pcAudioVoicesIndex));
                    Debug.WriteLine("Voice::Stop");
                    Debug.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", pcAudioVoicesIndex));
                    Debug.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", pcAudioVoicesIndex));
                    Debug.WriteLine("ES-> Sfx->KeyOffWait OK.");
#elif TRACE
                    Trace.WriteLine("ES-> ES_SFXRemove()");
                    Trace.WriteLine(string.Format("ES-> psiSampleKeyOff( {0} )", pcAudioVoicesIndex));
                    Trace.WriteLine("Voice::Stop");
                    Trace.WriteLine(string.Format("ES-> ES_AudioHasEnded() = {0} Ok.", pcAudioVoicesIndex));
                    Trace.WriteLine(string.Format("ES-> ES_UnLockVoiceHandle() = {0}", pcAudioVoicesIndex));
                    Trace.WriteLine("ES-> Sfx->KeyOffWait OK.");
#endif
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal int GetVoiceIndex()
        {
            if (pcAudioVoicesIndex < pcAudioVoices.Length)
            {
                pcAudioVoicesIndex++;
            }
            if (pcAudioVoicesIndex >= pcAudioVoices.Length)
            {
                pcAudioVoicesIndex = 10;
            }

            return pcAudioVoicesIndex;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
