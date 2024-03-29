﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Audio Samples Manager
//-------------------------------------------------------------------------------------------------------------------------------
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
        public static string[] GetNewSamples(SamplePool samples, ProjProperties projectSettings)
        {
            // Create a list to store the missing files
            List<string> missingFiles = new List<string>();

            // Get the path to the "Master" folder
            string masterFolderPath = Path.Combine(projectSettings.SampleFilesFolder, "Master");

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
        public static string[] GetMissingSamples(SamplePool samples, ProjProperties projectSettings)
        {
            // Get the path of the master sample files folder
            string masterSampleFilesFolder = Path.Combine(projectSettings.SampleFilesFolder, "Master");

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
