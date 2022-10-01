using sb_editor.Objects;
using System;
using System.IO;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class ProjectFileFunctions
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static void UpdateSoundBanks(MainForm mainForm)
        {
            string projectFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt");
            if (File.Exists(projectFilePath))
            {
                ProjectFile projectFile = TextFiles.ReadProjectFile(projectFilePath);
                TextFiles.WriteProjectFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt"), projectFile);

                //Reload Data & Sort
                projectFile.SoundBanks = mainForm.UserControl_SoundBanks_CheckBox.LoadSoundBanks();
                mainForm.UserControl_SoundBanks.LoadSoundBanks(mainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks);
                Array.Sort(projectFile.SoundBanks);
                Array.Sort(projectFile.DataBases);
                Array.Sort(projectFile.SFXs);

                //Update files
                TextFiles.WriteProjectFile(projectFilePath, projectFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void UpdateDataBases(MainForm mainForm)
        {
            string projectFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt");
            if (File.Exists(projectFilePath))
            {
                ProjectFile projectFile = TextFiles.ReadProjectFile(projectFilePath);
                TextFiles.WriteProjectFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt"), projectFile);

                //Reload Data & Sort
                projectFile.DataBases = mainForm.UserControl_Available_Databases.LoadDataBases();
                Array.Sort(projectFile.SoundBanks);
                Array.Sort(projectFile.DataBases);
                Array.Sort(projectFile.SFXs);

                //Update files
                TextFiles.WriteProjectFile(projectFilePath, projectFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void UpdateSFXs(MainForm mainForm)
        {
            string projectFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt");
            if (File.Exists(projectFilePath))
            {
                ProjectFile projectFile = TextFiles.ReadProjectFile(projectFilePath);
                TextFiles.WriteProjectFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt"), projectFile);

                //Reload Data & Sort
                projectFile.SFXs = mainForm.UserControl_Available_SFXs.LoadSFXs("All");
                if (projectFile.SoundBanks != null)
                {
                    Array.Sort(projectFile.SoundBanks);
                }
                if (projectFile.DataBases != null)
                {
                    Array.Sort(projectFile.DataBases);
                }

                //Update files
                TextFiles.WriteProjectFile(projectFilePath, projectFile);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void UpdateAll(MainForm mainForm)
        {
            //Copy File
            string projectFilePath = Path.Combine(GlobalPrefs.ProjectFolder, "Project.txt");
            if (File.Exists(projectFilePath))
            {
                ProjectFile projectFile = TextFiles.ReadProjectFile(projectFilePath);

                //Reload Data & Sort
                projectFile.SoundBanks = mainForm.UserControl_SoundBanks_CheckBox.LoadSoundBanks();
                mainForm.UserControl_SoundBanks.LoadSoundBanks(mainForm.UserControl_SoundBanks_CheckBox.cbllstSoundbanks);
                projectFile.DataBases = mainForm.UserControl_Available_Databases.LoadDataBases();
                projectFile.SFXs = mainForm.UserControl_Available_SFXs.LoadSFXs("All");
                Array.Sort(projectFile.SoundBanks);
                Array.Sort(projectFile.DataBases);

                //Update files
                TextFiles.WriteProjectFile(projectFilePath, projectFile);

                //Update Temporal File
                Array.Sort(projectFile.SFXs);
                TextFiles.WriteProjectFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "TempFileName.txt"), projectFile);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
