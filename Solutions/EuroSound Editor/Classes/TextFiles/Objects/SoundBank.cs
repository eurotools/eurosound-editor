﻿namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SoundBank : FileHeader
    {
        public string[] DataBases = new string[0];
        public int HashCode;
        public uint PlayStationSize;
        public uint PCSize;
        public uint XboxSize;
        public uint GameCubeSize;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
