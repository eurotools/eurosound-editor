﻿namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class GroupFile : FileHeader
    {
        public string[] Dependencies = new string[] { };
        public byte Action1;
        public int MaxVoices;
        public int Priority;
        public bool UseDistCheck;
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
