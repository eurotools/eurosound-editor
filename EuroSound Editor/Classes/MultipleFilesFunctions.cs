using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EuroSound_Editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class MultipleFilesFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetNextAvailableFilename(string folderToCheck, string filenamePattern)
        {
            int fileNumber = 0;
            while (File.Exists(Path.Combine(folderToCheck, filenamePattern + fileNumber + ".txt")))
            {
                fileNumber++;
            }
            return string.Join(string.Empty, filenamePattern, fileNumber);
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetFullFileName(string fileName)
        {
            string finalName = fileName;
            if (!Path.IsPathRooted(fileName))
            {
                finalName = Path.DirectorySeparatorChar + fileName;
            }
            return finalName;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetFilesRemovingMessage(string questionText, string[] DataBasesToDelete)
        {
            string message = string.Format("{0}\n\n", questionText);
            for (int i = 0; i < Math.Min(33, DataBasesToDelete.Length); i++)
            {
                message += string.Format("'{0}'\n", DataBasesToDelete[i]);
            }
            if (DataBasesToDelete.Length > 33)
            {
                message += "Plus Some More .....\n";
                message += "............\n";
            }
            message += string.Format("\nTotal Files:  {0}", DataBasesToDelete.Length);
            return message;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void RemoveFilesAndUpdateDependencies(string[] filesToRemove, string filesFolderName, string dependenciesFolder)
        {
            //Trash Folder
            string trashFolder = Path.Combine(GlobalPrefs.ProjectFolder, filesFolderName + "_Trash");
            Directory.CreateDirectory(trashFolder);

            //Iterate over all SoundBanks 
            string[] dependencies = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, dependenciesFolder), "*.txt", SearchOption.TopDirectoryOnly);
            for (int j = 0; j < dependencies.Length; j++)
            {
                List<string> fileData = File.ReadAllLines(dependencies[j]).ToList();
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
                    File.WriteAllLines(dependencies[j], fileData.ToArray());
                }
            }

            //Delete files
            for (int i = 0; i < filesToRemove.Length; i++)
            {
                string trashFilePath = Path.Combine(trashFolder, filesToRemove[i] + ".txt");
                File.Delete(trashFilePath);
                File.Move(Path.Combine(GlobalPrefs.ProjectFolder, filesFolderName, filesToRemove[i] + ".txt"), trashFilePath);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
