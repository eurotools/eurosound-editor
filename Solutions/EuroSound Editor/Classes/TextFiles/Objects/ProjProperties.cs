using System.Collections.Generic;
using System.Linq;

namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class ProjProperties : FileHeader
    {
        //Misc
        public int DefaultRate;
        public int DefaultMemMap;
        public string SampleFilesFolder;
        public string HashCodeFileDirectory;
        public string EngineXProjectPath;
        public string EuroLandHashCodeServerPath;

        //Version
        public uint CurrentVersion;

        //Available ReSampleRates 
        public List<string> ResampleRates = new List<string>();
        public List<string> MemoryMaps = new List<string>();

        //Platform Data
        public Dictionary<string, PlatformData> platformData = new Dictionary<string, PlatformData>();

        //Clone object
        public ProjProperties Clone()
        {
            ProjProperties newObj = new ProjProperties
            {
                DefaultRate = DefaultRate,
                DefaultMemMap = DefaultMemMap,
                CurrentVersion = CurrentVersion,
                SampleFilesFolder = SampleFilesFolder,
                EngineXProjectPath = EngineXProjectPath,
                HashCodeFileDirectory = HashCodeFileDirectory,
                ResampleRates = new List<string>(ResampleRates),
                MemoryMaps = new List<string>(MemoryMaps),
                EuroLandHashCodeServerPath = EuroLandHashCodeServerPath,
                platformData = platformData.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()),
                CreatedBy = CreatedBy,
                ModifiedBy = ModifiedBy,
                LastModified = LastModified,
                FirstCreated = FirstCreated
            };

            //Return
            return newObj;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    public class PlatformData
    {
        public string OutputFolder;
        public bool AutoReSample;
        public List<int> ReSampleRates = new List<int>();
        public List<int> MemoryMapsSize = new List<int>();

        //Clone object
        public PlatformData Clone()
        {
            PlatformData clonedObj = new PlatformData()
            {
                OutputFolder = OutputFolder,
                AutoReSample = AutoReSample,
                ReSampleRates = new List<int>(ReSampleRates),
                MemoryMapsSize = new List<int>(MemoryMapsSize)
            };
            return clonedObj;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
