using System.Collections.Generic;

namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SFX : FileHeader
    {
        public SfxParameters Parameters = new SfxParameters();
        public List<SfxSample> Samples = new List<SfxSample>();
        public SamplePoolControl SamplePool = new SamplePoolControl();
        public int HashCode;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    public class SfxParameters
    {
        public int ReverbSend;
        public byte TrackingType;
        public int InnerRadius;
        public int OuterRadius;
        public int MaxVoices;
        public byte Action1;
        public int Priority;
        public int Group;
        public byte Action2;
        public int Alertness;
        public bool IgnoreAge;
        public int Ducker;
        public int DuckerLength;
        public int MasterVolume;
        public bool Outdoors;
        public bool PauseInNis;
        public bool StealOnAge;
        public bool MusicType;
        public bool Doppler;

        //MusX V4
        public bool PauseInstant;
        public bool UnPausable;
        public bool OneInstancePerFrame;
        public bool KillMeOwnGroup;
        public bool IgnoreMasterVolume;
        public bool GroupStealReject;
        public int GroupMaxChannels;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    public class SfxSample
    {
        public string FilePath { get; set; }
        public sbyte BaseVolume;
        public decimal PitchOffset;
        public decimal RandomPitch;
        public sbyte RandomVolume;
        public sbyte Pan;
        public sbyte RandomPan;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    public class SamplePoolControl
    {
        public byte Action1;
        public bool RandomPick;
        public bool Shuffled;
        public bool isLooped;
        public bool Polyphonic;
        public int MinDelay;
        public int MaxDelay;
        public bool EnableSubSFX;
        public bool EnableStereo;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
