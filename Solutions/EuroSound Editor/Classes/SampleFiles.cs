using sb_editor.Objects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class SampleFiles
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetNewSamples(SamplePool samples)
        {
            // Create a list to store the missing files
            List<string> missingFiles = new List<string>();

            // Get the path to the "Master" folder
            string masterFolderPath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");

            // Get the paths to all wave files in the "Master" folder and its subfolders
            string[] waveFilePaths = Directory.GetFiles(masterFolderPath, "*.wav", SearchOption.AllDirectories);

            // Iterate over the wave file paths
            foreach (string waveFilePath in waveFilePaths)
            {
                // Get the relative path of the wave file
                string relativeFilePath = waveFilePath.Substring(masterFolderPath.Length);

                // Check if the relative path of the wave file exists in the sample pool
                if (!samples.SamplePoolItems.ContainsKey(relativeFilePath))
                {
                    // If the relative path does not exist in the sample pool, add it to the list of missing files
                    missingFiles.Add(relativeFilePath);
                }
            }

            // Return the list of missing files as an array
            return missingFiles.ToArray();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string[] GetMissingSamples(SamplePool samples)
        {
            // Get the path of the master sample files folder
            string masterSampleFilesFolder = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master");

            // Get a list of all available sample file paths
            string[] availableSampleFilePaths = Directory.GetFiles(masterSampleFilesFolder, "*.wav", SearchOption.AllDirectories)
                .Select(filePath => filePath.Substring(masterSampleFilesFolder.Length))
                .ToArray();

            // Get a list of all used sample file paths
            string[] usedSampleFilePaths = samples.SamplePoolItems.Keys.ToArray();

            // Return the list of used sample file paths that are not available
            return usedSampleFilePaths.Except(availableSampleFilePaths).ToArray();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
