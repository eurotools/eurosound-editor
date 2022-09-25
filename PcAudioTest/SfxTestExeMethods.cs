using Microsoft.Win32;
using System;
using System.IO;

namespace PcAudioTest
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class SfxTestExeMethods
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetExeGamePath()
        {
            string gamePath = string.Empty;

            //Check Gog - Local User
            RegistryKey baseReg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            RegistryKey GogKey = baseReg.OpenSubKey(@"SOFTWARE\GOG.com\Games\1118073204");
            if (GogKey == null)
            {
                //Check Gog - Local Machine
                baseReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                GogKey = baseReg.OpenSubKey(@"SOFTWARE\GOG.com\Games\1118073204");
                if (GogKey != null)
                {
                    gamePath = GogKey.GetValue("Exe", string.Empty).ToString();
                }
            }
            else
            {
                gamePath = GogKey.GetValue("Exe", string.Empty).ToString();
            }

            //Game is not installed via gog check Steam...
            if (string.IsNullOrEmpty(gamePath))
            {
                //Check Steam - Local User
                baseReg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                RegistryKey SteamKey = baseReg.OpenSubKey(@"SOFTWARE\Valve\Steam");
                if (SteamKey == null)
                {
                    //Check Steam - Local Machine
                    baseReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                    SteamKey = baseReg.OpenSubKey(@"SOFTWARE\Valve\Steam");
                    if (SteamKey != null)
                    {
                        gamePath = GetSteamSphinx(SteamKey.GetValue("SteamPath", string.Empty).ToString());
                    }
                }
                else
                {
                    gamePath = GetSteamSphinx(SteamKey.GetValue("SteamPath", string.Empty).ToString());
                }
            }

            return gamePath;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static string GetSteamSphinx(string steamPath)
        {
            string GamePath = string.Empty;

            string libraryFilePath = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
            if (File.Exists(libraryFilePath))
            {
                //Iterate over all libraries
                string[] libraryFileData = File.ReadAllLines(libraryFilePath);
                for (int i = 0; i < libraryFileData.Length; i++)
                {
                    string currentLine = libraryFileData[i].Trim();
                    if (currentLine.StartsWith("\"path\"", StringComparison.OrdinalIgnoreCase))
                    {
                        string libraryFolder = currentLine.Substring("\"path\"".Length).Trim().Replace("\"", string.Empty);

                        //Check App Manifest File
                        string appManifest = Path.Combine(libraryFolder, "steamapps", "appmanifest_606710.acf");
                        if (File.Exists(appManifest))
                        {
                            string[] manifestFileData = File.ReadAllLines(appManifest);
                            for (int j = 0; j < manifestFileData.Length; j++)
                            {
                                currentLine = manifestFileData[j].Trim();
                                if (currentLine.StartsWith("\"installdir\"", StringComparison.OrdinalIgnoreCase))
                                {
                                    string gameFolder = currentLine.Substring("\"installdir\"".Length).Trim().Replace("\"", string.Empty);
                                    GamePath = Path.Combine(libraryFolder, "steamapps", "common", gameFolder, "SphinxD_GL.exe");
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }

            return GamePath;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
