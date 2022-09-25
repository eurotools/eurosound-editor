using System;
using System.Collections.Generic;

namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SamplePool
    {
        public FileHeader HeaderData = new FileHeader();
        public Dictionary<string, SamplePoolItem> SamplePoolItems = new Dictionary<string, SamplePoolItem>(StringComparer.OrdinalIgnoreCase);
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}