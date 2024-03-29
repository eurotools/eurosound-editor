﻿//-------------------------------------------------------------------------------------------------------------------------------
//  ______                                           _ 
// |  ____|                                         | |
// | |__   _   _ _ __ ___  ___  ___  _   _ _ __   __| |
// |  __| | | | | '__/ _ \/ __|/ _ \| | | | '_ \ / _` |
// | |____| |_| | | | (_) \__ \ (_) | |_| | | | | (_| |
// |______|\__,_|_|  \___/|___/\___/ \__,_|_| |_|\__,_|
//
//-------------------------------------------------------------------------------------------------------------------------------
// Advanced Settings Form - Reports
//-------------------------------------------------------------------------------------------------------------------------------
using ESUtils;
using sb_editor.Classes;
using sb_editor.Objects;
using System;
using System.IO;
using System.Windows.Forms;
using static ESUtils.Enumerations;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Advanced
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        private void CreateReport(string reportFilePath, string soundBankPath, string outputFormat, Language outputLanguage)
        {
            string projectPropertiesFile = Path.Combine(GlobalPrefs.ProjectFolder, "System", "Properties.txt");
            if (File.Exists(projectPropertiesFile))
            {
                ProjProperties projectSettings = TextFiles.ReadPropertiesFile(projectPropertiesFile);

                // Create a new SoundBankFunctions instance
                SoundBankFunctions sbFunctions = new SoundBankFunctions();

                // Read the samples file and sound bank file
                SamplePool samplePool = TextFiles.ReadSamplesFile(Path.Combine(GlobalPrefs.ProjectFolder, "System", "Samples.txt"));
                SoundBank soundBankData = TextFiles.ReadSoundbankFile(soundBankPath);

                // Get the list of SFXs and samples
                string[] SFXs = sbFunctions.GetSFXs(soundBankData.DataBases, outputFormat);
                string[] samples = sbFunctions.GetSampleList(SFXs, outputLanguage);

                // Open the report file for writing
                using (StreamWriter sw = new StreamWriter(File.Open(reportFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    // Write the report data to the file
                    sw.WriteLine("SoundBank Report Created:\t {0:MM/dd/yyyy} {0:HH:mm:ss}", DateTime.Now);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("SoundBank Name: {0}", Path.GetFileNameWithoutExtension(soundBankPath));
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("{0} : \t\t {1}", soundBankData.bankInfo1.TrimStart('#').Trim(), soundBankData.FirstCreated.ToString(GlobalPrefs.FilesDateFormat));
                    sw.WriteLine("{0} : \t\t {1}", soundBankData.bankInfo2.TrimStart('#').Trim(), soundBankData.CreatedBy);
                    sw.WriteLine("{0} : \t\t {1}", soundBankData.bankInfo3.TrimStart('#').Trim(), soundBankData.LastModified.ToString(GlobalPrefs.FilesDateFormat));
                    sw.WriteLine("{0} : \t\t {1}", soundBankData.bankInfo4.TrimStart('#').Trim(), soundBankData.ModifiedBy);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("Database Count:\t\t{0}", soundBankData.DataBases.Length);
                    sw.WriteLine("SFX Count:\t\t{0}", SFXs.Length);
                    sw.WriteLine("Sample Count:\t\t{0}", samples.Length);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("Total Sample Size:\t\t{0}", BytesFunctions.FormatBytes(sbFunctions.GetSampleSize(Path.Combine(GlobalPrefs.ProjectFolder, "Master"), samplePool, samples)));
                    sw.WriteLine(string.Empty);
                    foreach (string currentFormat in projectSettings.platformData.Keys)
                    {
                        string temporalFiles = Path.Combine(GlobalPrefs.ProjectFolder, "TempOutputFolder", currentFormat, "SoundBanks", outputLanguage.ToString(), soundBankData.HashCode + ".sbf");
                        string fileSize;
                        if (File.Exists(temporalFiles))
                        {
                            fileSize = BytesFunctions.FormatBytes(new FileInfo(temporalFiles).Length);
                        }
                        else
                        {
                            fileSize = string.Format("{0} - ESTIMATED", BytesFunctions.FormatBytes(sbFunctions.GetEstimatedOutputFileSize(projectSettings, samples, samplePool, currentFormat)));
                        }

                        //Show value
                        sw.WriteLine("{0}:\t\t{1}", currentFormat, fileSize);
                    }
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine("DataBases:  {0}", soundBankData.DataBases.Length);
                    sw.WriteLine("SFXs:  {0}", SFXs.Length);
                    sw.WriteLine("Samples:  {0}", samples.Length);
                    sw.WriteLine(string.Empty);
                    sw.WriteLine(string.Empty);
                    foreach (string dataBase in soundBankData.DataBases)
                    {
                        sw.WriteLine("DataBase: 	{0}", dataBase);
                        DataBase data = TextFiles.ReadDataBaseFile(Path.Combine(GlobalPrefs.ProjectFolder, "DataBases", dataBase + ".txt"));
                        foreach (string sfxItem in data.SFXs)
                        {
                            sw.WriteLine("\tSFX:\t {0}", sfxItem);
                            SFX sfxData = TextFiles.ReadSfxFile(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs", sfxItem + ".txt"));
                            foreach (SfxSample sampleData in sfxData.Samples)
                            {
                                sw.WriteLine("\t\tSample: \t{0}{1}", @"\MASTER\", sampleData.FilePath.ToUpper());
                            }
                            sw.WriteLine("\tEnd SFX");
                        }
                        sw.WriteLine("End DataBase");
                        sw.WriteLine(string.Empty);
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("Project Properties File Not Found {0}", projectPropertiesFile), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
