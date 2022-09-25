using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PcAudioTest
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class SfxTestMethods
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private static Process GameProc;

        //*===============================================================================================
        //* GAME FUNCTIONS
        //*===============================================================================================
        public static void OpenGame(string gameFilePath)
        {
            if (File.Exists(gameFilePath))
            {
                //Start Game
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = gameFilePath,
                    Arguments = string.Join(" ", "-dev", "-mod", string.Format("\"{0}\"", Path.Combine(Application.StartupPath, "SystemFiles", "testAudioMod")), "-level", "0x0100030D")
                };
                GameProc = new Process
                {
                    StartInfo = info,
                    EnableRaisingEvents = true
                };
                GameProc.Exited += (se, ev) => { RestoreTestIni(); };
                GameProc.OutputDataReceived += (se, ev) => { MessageBox.Show(ev.Data); };
                GameProc.Start();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void CloseGame()
        {
            if (GameProc != null && !GameProc.HasExited)
            {
                GameProc.Kill();
                GameProc.WaitForExit();
                GameProc.Dispose();
                GameProc = null;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static bool GameIsOpen()
        {
            bool gameIsOpen = false;

            //Check all opened processes
            if (GameProc != null && !GameProc.HasExited)
            {
                Process[] process = Process.GetProcesses();
                foreach (Process prs in process)
                {
                    if (prs.Id == GameProc.Id)
                    {
                        gameIsOpen = true;
                        break;
                    }
                }
            }

            return gameIsOpen;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static int GetGamePID()
        {
            int pid = -1;
            if (GameProc != null && !GameProc.HasExited)
            {
                pid = GameProc.Id;
            }

            return pid;
        }

        //*===============================================================================================
        //* INI FILE FUNCTIONS
        //*===============================================================================================
        public static void SetTestIni()
        {
            //Replace INI
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string originalIniPath = Path.Combine(appDataPath, "Sphinx", "Sphinx.ini");
            string backupIniPath = Path.ChangeExtension(originalIniPath, ".bak");
            if (!File.Exists(backupIniPath))
            {
                File.Move(originalIniPath, backupIniPath);
                File.Copy(Path.Combine(Application.StartupPath, "SystemFiles", "testAudioMod", "Sphinx.ini"), originalIniPath);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static void RestoreTestIni()
        {
            //Replace INI
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string originalIniPath = Path.Combine(appDataPath, "Sphinx", "Sphinx.ini");
            string backupIniPath = Path.ChangeExtension(originalIniPath, ".bak");
            if (File.Exists(backupIniPath))
            {
                File.Delete(originalIniPath);
                File.Move(backupIniPath, originalIniPath);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
