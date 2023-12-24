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
using PCAudioDLL.Codecs;
using PCAudioDLL.MusX_Objects;
using PCAudioDLL.Objects;
using System;
using System.Collections.Generic;
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
            //Cut if Loop End
            if (audioSample.LoopEnd > 0)
            {
                Array.Resize(ref audioSample.PCMData, Math.Min(audioSample.LoopEnd * 2, audioSample.PCMData.Length));
            }

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
            AudioLoop loop = new AudioLoop(waveStream, audioSample.LoopStart * 2) { Position = audioSample.StartPos, EnableLooping = audioSample.isLooped };
            PanningSampleProvider panProvider = new PanningSampleProvider(loop.ToSampleProvider()) { Pan = panning };
            VolumeSampleProvider volumeProvider = new VolumeSampleProvider(panProvider) { Volume = audioMaths.GetEffectValue(audioSample.Volume, audioSample.RandomVolume) / 100.0f };

            return volumeProvider.ToWaveProvider();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal ExAudioSample GetAudioSample(string outputPlatform, SoundBank soundBank, uint hashcode, Sample sfxSample, SampleInfo sampleInfo, bool testing)
        {
            SampleData sampleData = soundBank.sfxStoredData[sampleInfo.FileRef];

            //Decode 
            byte[] decodedData = null;
            if (testing)
            {
                decodedData = soundBank.sfxStoredData[sampleInfo.FileRef].EncodedData;
                sampleData.LoopStartOffset = sampleData.OriginalLoopOffset / 2;
            }
            else if (outputPlatform.IndexOf("XB", StringComparison.OrdinalIgnoreCase) >= 0 || outputPlatform.IndexOf("PC", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Eurocom_ImaAdpcm eurocomImaDecoder = new Eurocom_ImaAdpcm();
                decodedData = Utils.ShortArrayToByteArray(eurocomImaDecoder.Decode(soundBank.sfxStoredData[sampleInfo.FileRef].EncodedData));
            }
            else if (outputPlatform.IndexOf("PS2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                SonyAdpcm vagDecoder = new SonyAdpcm();
                decodedData = vagDecoder.Decode(soundBank.sfxStoredData[sampleInfo.FileRef].EncodedData, ref soundBank.sfxStoredData[sampleInfo.FileRef].LoopStartOffset);
                sampleData.LoopStartOffset /= 2;
            }
            else if (outputPlatform.IndexOf("GC", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DspAdpcm gcDecoder = new DspAdpcm();
                decodedData = Utils.ShortArrayToByteArray(gcDecoder.Decode(soundBank.sfxStoredData[sampleInfo.FileRef].EncodedData, soundBank.sfxStoredData[sampleInfo.FileRef].DspCoeffs));
            }

            //Set settings
            ExAudioSample audioSample = new ExAudioSample
            {
                HashCode = hashcode,
                PCMData = decodedData,
                isLooped = sampleData.Flags == 1,
                LoopStart = sampleData.LoopStartOffset,
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

        //-------------------------------------------------------------------------------------------------------------------------------
        internal ExAudioSample GetStreamAudioSample(string outputPlatform, List<StreamSample> streamedFile, uint hashcode, Sample sfxSample, SampleInfo sampleInfo)
        {
            int streamIndex = Math.Abs(sampleInfo.FileRef) - 1;

            //Decode Data
            byte[] decodedData = null;
            if (outputPlatform.IndexOf("PC", StringComparison.OrdinalIgnoreCase) >= 0 || outputPlatform.IndexOf("GC", StringComparison.OrdinalIgnoreCase) >= 0 || outputPlatform.IndexOf("XB", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Eurocom_ImaAdpcm decoder = new Eurocom_ImaAdpcm();
                decodedData = Utils.ShortArrayToByteArray(decoder.Decode(streamedFile[streamIndex].EncodedData));
            }
            else if (outputPlatform.IndexOf("PS2", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int test = 0;
                SonyAdpcm vagDecoder = new SonyAdpcm();
                decodedData = vagDecoder.Decode(streamedFile[streamIndex].EncodedData, ref test);
            }

            //Set settings
            ExAudioSample audioSample = new ExAudioSample
            {
                HashCode = hashcode,
                PCMData = decodedData,
                isLooped = SoundIsLooped(streamedFile[streamIndex].Markers),
                LoopStart = (int)GetStartLoopPos(streamedFile[streamIndex].Markers),
                LoopEnd = (int)GetEndLoopPos(streamedFile[streamIndex].Markers),
                StartPos = (int)GetStartPosition(streamedFile[streamIndex].Markers),
                Frequency = audioMaths.SemitonesToFreq(22050, audioMaths.GetEffectValue(sampleInfo.Pitch, sampleInfo.PitchOffset)),
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

        //-------------------------------------------------------------------------------------------------------------------------------
        internal IWaveProvider CreateStereoLoopWav(ref RawSourceWaveStream providerLeft, ref RawSourceWaveStream providerRight, byte[][] _pcmData, MusicBank _soundToPlay)
        {
            if (_soundToPlay.loopEndPoint > 0)
            {
                Array.Resize(ref _pcmData[0], Math.Min(_soundToPlay.loopEndPoint * 4, _pcmData[0].Length));
                Array.Resize(ref _pcmData[1], Math.Min(_soundToPlay.loopEndPoint * 4, _pcmData[0].Length));
            }

            providerLeft = new RawSourceWaveStream(new MemoryStream(_pcmData[0]), new WaveFormat(_soundToPlay.sampleRate, 16, 1));
            AudioLoop loopLeft = new AudioLoop(providerLeft, _soundToPlay.loopStartPoint * 4) { Position = _soundToPlay.startPos, EnableLooping = _soundToPlay.isLooped };
            providerRight = new RawSourceWaveStream(new MemoryStream(_pcmData[1]), new WaveFormat(_soundToPlay.sampleRate, 16, 1));
            AudioLoop loopRight = new AudioLoop(providerRight, _soundToPlay.loopStartPoint * 4) { Position = _soundToPlay.startPos, EnableLooping = _soundToPlay.isLooped };
            MultiplexingWaveProvider waveProvider = new MultiplexingWaveProvider(new IWaveProvider[] { loopLeft, loopRight }, 2);
            VolumeSampleProvider volumeProvider = new VolumeSampleProvider(waveProvider.ToSampleProvider()) { Volume =  _soundToPlay.volume };

            return volumeProvider.ToWaveProvider();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal uint GetStartLoopPos(Marker[] startMarkers)
        {
            uint startPosition = 0;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 7 || startMarkers[i].Type == 6)
                {
                    startPosition = startMarkers[i].LoopStart;
                    break;
                }
            }

            return startPosition;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal uint GetEndLoopPos(Marker[] startMarkers)
        {
            uint startPosition = 0;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 7 || startMarkers[i].Type == 6)
                {
                    startPosition = startMarkers[i].Position;
                    break;
                }
            }

            return startPosition;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal bool SoundIsLooped(Marker[] startMarkers)
        {
            bool isLooped = true;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 9)
                {
                    isLooped = false;
                    break;
                }
            }

            return isLooped;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal uint GetStartPosition(Marker[] startMarkers)
        {
            uint startPosition = 0;
            for (int i = 0; i < startMarkers.Length; i++)
            {
                if (startMarkers[i].Type == 10)
                {
                    startPosition = startMarkers[i].Position;
                    break;
                }
            }

            return startPosition;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
