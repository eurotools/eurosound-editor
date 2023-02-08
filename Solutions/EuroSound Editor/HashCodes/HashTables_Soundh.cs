using sb_editor.Objects;
using System.IO;

namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal void BuildSoundHhFile(string filePath, ProjProperties projectSettings)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                sw.WriteLine("/* HT_Sound */");
                string sfxDefinesFilePath = Path.Combine(projectSettings.HashCodeFileDirectory, "SFX_Defines.h");
                if (File.Exists(sfxDefinesFilePath))
                {
                    string[] fileData = File.ReadAllLines(sfxDefinesFilePath);
                    for (int i = 0; i < fileData.Length; i++)
                    {
                        sw.WriteLine(fileData[i]);
                    }
                }
                string mfxDefinesFilePath = Path.Combine(projectSettings.HashCodeFileDirectory, "MFX_Defines.h");
                if (File.Exists(mfxDefinesFilePath))
                {
                    sw.WriteLine(string.Empty);
                    string[] fileData = File.ReadAllLines(mfxDefinesFilePath);
                    for (int i = 0; i < fileData.Length; i++)
                    {
                        sw.WriteLine(fileData[i]);
                    }
                }
                string reverbsFilePath = Path.Combine(projectSettings.HashCodeFileDirectory, "SFX_Reverbs.h");
                if (File.Exists(reverbsFilePath))
                {
                    sw.WriteLine(string.Empty);
                    string[] fileData = File.ReadAllLines(reverbsFilePath);
                    for (int i = 0; i < fileData.Length; i++)
                    {
                        sw.WriteLine(fileData[i]);
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
