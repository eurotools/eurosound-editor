using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class MultipleFilesFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetNextAvailableFilename(string folderPath, string fileNamePattern)
        {
            // Set the initial file number to 0
            int fileNumber = 0;

            // Construct the full file path using the given folder path and file name pattern
            string fullFilePath = Path.Combine(folderPath, fileNamePattern + fileNumber + ".txt");

            // Check if the file exists
            while (File.Exists(fullFilePath))
            {
                // If the file exists, increment the file number and update the full file path
                fileNumber++;
                fullFilePath = Path.Combine(folderPath, fileNamePattern + fileNumber + ".txt");
            }

            // Return the constructed file name
            return string.Join(string.Empty, fileNamePattern, fileNumber);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetFullFileName(string fileName)
        {
            // Check if the file name is an absolute path (i.e., if it is rooted)
            if (!Path.IsPathRooted(fileName))
            {
                // If the file name is not an absolute path, add the directory separator character to the beginning of the file name
                fileName = Path.DirectorySeparatorChar + fileName;
            }

            // Return the modified file name
            return fileName;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetFilesRemovingMessage(string questionText, string[] DataBasesToDelete)
        {
            // Initialize the message string
            string message = string.Format("{0}\n\n", questionText);

            // Add the names of the first 33 files to the message
            for (int i = 0; i < Math.Min(33, DataBasesToDelete.Length); i++)
            {
                message += string.Format("'{0}'\n", DataBasesToDelete[i]);
            }

            // If there are more than 33 files, add a note to the message
            if (DataBasesToDelete.Length > 33)
            {
                message += "Plus Some More .....\n";
                message += "............\n";
            }

            // Add the total number of files to the message
            message += string.Format("\nTotal Files:  {0}", DataBasesToDelete.Length);

            // Return the completed message
            return message;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void RemoveFilesAndUpdateDependencies(string[] filesToRemove, string folderName, string dependenciesFolder)
        {
            //Trash Folder
            string trashFolder = Path.Combine(GlobalPrefs.ProjectFolder, folderName + "_Trash");
            Directory.CreateDirectory(trashFolder);

            //Iterate over all SoundBanks 
            IEnumerable<string> dependencies = Directory.EnumerateFiles(Path.Combine(GlobalPrefs.ProjectFolder, dependenciesFolder), "*.txt", SearchOption.TopDirectoryOnly);
            foreach (string dependency in dependencies)
            {
                List<string> fileData = File.ReadAllLines(dependency).ToList();
                bool fileHasChanges = false;
                for (int i = 0; i < filesToRemove.Length; i++)
                {
                    //Remove DataBase If exists
                    int itemIndex = fileData.IndexOf(filesToRemove[i]);
                    if (itemIndex > 0)
                    {
                        fileHasChanges = true;
                        fileData.RemoveAt(itemIndex);
                    }
                }

                //Write file again
                if (fileHasChanges)
                {
                    File.WriteAllLines(dependency, fileData.ToArray());
                }
            }

            //Delete files
            for (int i = 0; i < filesToRemove.Length; i++)
            {
                string trashFilePath = Path.Combine(trashFolder, filesToRemove[i] + ".txt");
                File.Delete(trashFilePath);
                File.Move(Path.Combine(GlobalPrefs.ProjectFolder, folderName, filesToRemove[i] + ".txt"), trashFilePath);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
