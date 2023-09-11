//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Audio Player
//-------------------------------------------------------------------------------------------------------------------------------
using MusX.Objects;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using PCAudioDLL.MusX_Objects;
using PCAudioDLL.Objects;
using System;
using System.IO;

namespace PCAudioDLL.Audio_Player
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal class AudioMixer
    {
        private readonly AudioMaths audioMaths = new AudioMaths();

        //-------------------------------------------------------------------------------------------------------------------------------
        internal RawSourceWaveStream BuildWaveStream(ExAudioSample audioSample)
        {
            //Get Wave Format
            WaveFormat waveFormat = new WaveFormat(audioMaths.SemitonesToFreq(audioSample.Frequency, audioMaths.GetEffectValue(audioSample.Pitch, audioSample.RandomPitch)), 16, 1);

            //Calculate Inter-Sample Delay
            byte[] pcmData = audioSample.PCMData;
            int numSilenceSamples = audioMaths.CalculateInterSample(audioSample.MinDelay, audioSample.MaxDelay, audioSample.Frequency);
            if (numSilenceSamples > 0)
            {
                pcmData = new byte[audioSample.PCMData.Length + numSilenceSamples];
                Array.Copy(audioSample.PCMData, 0, pcmData, numSilenceSamples, audioSample.PCMData.Length);
            }

            //Set wave data
            RawSourceWaveStream waveStream = new RawSourceWaveStream(new MemoryStream(pcmData), waveFormat)
            {
                Position = 0
            };

            return waveStream;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal IWaveProvider PlayAudioSample(RawSourceWaveStream waveStream, ExAudioSample audioSample, float panning)
        {
            //Set wave data
            AudioLoop loop = new AudioLoop(waveStream, audioSample.LoopStart, audioSample.isLooped);
            PanningSampleProvider panProvider = new PanningSampleProvider(loop.ToSampleProvider()) { Pan = panning };
            VolumeSampleProvider volumeProvider = new VolumeSampleProvider(panProvider) { Volume = audioMaths.GetEffectValue(audioSample.Volume, audioSample.RandomVolume) / 100.0f };

            return volumeProvider.ToWaveProvider();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal ExAudioSample GetAudioSample(SoundBank soundBank, uint hashcode, Sample sfxSample, SampleInfo sampleInfo)
        {
            SampleData sampleData = soundBank.sfxStoredData[sampleInfo.FileRef];

            //Set settings
            ExAudioSample audioSample = new ExAudioSample
            {
                HashCode = hashcode,
                PCMData = soundBank.sfxStoredData[sampleInfo.FileRef].EncodedData,
                isLooped = sampleData.Flags == 1,
                LoopStart = sampleData.OriginalLoopOffset,
                Frequency = audioMaths.SemitonesToFreq(sampleData.Frequency, audioMaths.GetEffectValue(sampleInfo.Pitch, sampleInfo.PitchOffset)),
                Pitch = sampleInfo.Pitch,
                RandomPitch = sampleInfo.PitchOffset,
                Pan = sampleInfo.Pan,
                RandomPan = sampleInfo.PanOffset,
                Volume = sampleInfo.Volume,
                RandomVolume = sampleInfo.VolumeOffset,
                MinDelay = sfxSample.MinDelay,
                MaxDelay = sfxSample.MaxDelay
            };

            return audioSample;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
