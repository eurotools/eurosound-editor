using System.Collections.Generic;
using System.Linq;

namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class ProjProperties
    {
        public FileHeader HeaderData = new FileHeader();

        //Misc
        public int DefaultRate;
        public string SampleFilesFolder;
        public string HashCodeFileDirectory;
        public string EngineXProjectPath;
        public string EuroLandHashCodeServerPath;

        //Version
        public uint CurrentVersion;

        //Available ReSampleRates 
        public List<string> ResampleRates = new List<string>();

        //Platform Data
        public Dictionary<string, PlatformData> platformData = new Dictionary<string, PlatformData>();

        //Clone object
        public ProjProperties Clone()
        {
            ProjProperties newObj = new ProjProperties
            {
                DefaultRate = DefaultRate,
                CurrentVersion = CurrentVersion,
                SampleFilesFolder = SampleFilesFolder,
                EngineXProjectPath = EngineXProjectPath,
                HashCodeFileDirectory = HashCodeFileDirectory,
                ResampleRates = new List<string>(ResampleRates),
                EuroLandHashCodeServerPath = EuroLandHashCodeServerPath,
                platformData = platformData.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()),
                HeaderData = new FileHeader
                {
                    CreatedBy = HeaderData.CreatedBy,
                    ModifiedBy = HeaderData.ModifiedBy,
                    LastModified = HeaderData.LastModified,
                    FirstCreated = HeaderData.FirstCreated,
                }
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

        //Clone object
        public PlatformData Clone()
        {
            PlatformData clonedObj = new PlatformData()
            {
                OutputFolder = OutputFolder,
                AutoReSample = AutoReSample,
                ReSampleRates = new List<int>(ReSampleRates)
            };
            return clonedObj;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
