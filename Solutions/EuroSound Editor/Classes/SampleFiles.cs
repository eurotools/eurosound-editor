using EuroSound_Editor.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EuroSound_Editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class SampleFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetNewSamples(SamplePool samples)
        {
            List<string> missingFiles = new List<string>();

            string masterFiles = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");
            string[] waveFiles = Directory.GetFiles(masterFiles, "*.wav", SearchOption.AllDirectories);
            for (int i = 0; i < waveFiles.Length; i++)
            {
                string filePath = waveFiles[i].Substring(masterFiles.Length);
                if (!samples.SamplePoolItems.ContainsKey(filePath))
                {
                    missingFiles.Add(filePath);
                }
            }

            return missingFiles.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetMissingSamples(SamplePool samples)
        {
            //Get All Available Samples
            List<string> availableSamples = new List<string>();
            string masterFiles = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");
            string[] waveFiles = Directory.GetFiles(masterFiles, "*.wav", SearchOption.AllDirectories);
            for (int i = 0; i < waveFiles.Length; i++)
            {
                availableSamples.Add(waveFiles[i].Substring(masterFiles.Length));
            }

            //Get Used Samples
            List<string> usedSamples = new List<string>();
            foreach (KeyValuePair<string, SamplePoolItem> itemData in samples.SamplePoolItems)
            {
                usedSamples.Add(itemData.Key);
            }

            return usedSamples.Except(availableSamples).ToArray();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
