using Microsoft.VisualBasic;
using sb_editor.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace sb_editor.Forms
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public partial class Frm_RefineList : TimerForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        public Frm_RefineList()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_RefineList_Shown(object sender, EventArgs e)
        {
            if (!BackgroundWorker.IsBusy)
            {
                BackgroundWorker.RunWorkerAsync();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void Frm_RefineList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorker.IsBusy)
            {
                e.Cancel = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Directory.Exists(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs")))
            {
                string[] sfxFilesToCheck = Directory.GetFiles(Path.Combine(GlobalPrefs.ProjectFolder, "SFXs"), "*.txt", SearchOption.TopDirectoryOnly);

                if (sfxFilesToCheck.Length > 0)
                {
                    List<ComboItemData> cboTemporal = new List<ComboItemData>();

                    //Split only six words
                    for (int wordIndexCount = 0; wordIndexCount < 6; wordIndexCount++)
                    {
                        //Iterate items
                        int listboxItemsCount = sfxFilesToCheck.Length;
                        for (int sfxItemIndex = 0; sfxItemIndex < listboxItemsCount; sfxItemIndex++)
                        {
                            //Calculate and report progress
                            int totalItems = listboxItemsCount * 6;
                            int progFromPrevIterations = listboxItemsCount * wordIndexCount;
                            BackgroundWorker.ReportProgress((int)(decimal.Divide(sfxItemIndex + progFromPrevIterations, totalItems) * (decimal)100.0));

                            //Find matches
                            for (int sfxItemIndexSub = 0; sfxItemIndexSub < listboxItemsCount; sfxItemIndexSub++)
                            {
                                //Skip the line that we are checking in the previus loop
                                if (sfxItemIndex == sfxItemIndexSub)
                                {
                                    continue;
                                }
                                //Get item to check
                                string currentSFX = Path.GetFileNameWithoutExtension(sfxFilesToCheck[sfxItemIndex]);
                                string wordToCheck = currentSFX;
                                //Split words
                                if (wordIndexCount > 0)
                                {
                                    for (int wordIndex = 0; wordIndex < wordIndexCount; wordIndex++)
                                    {
                                        if (wordToCheck.IndexOf("_") > 0)
                                        {
                                            int wordLength = wordToCheck.Length - (wordToCheck.IndexOf("_") + 1);
                                            wordToCheck = Strings.Right(wordToCheck, wordLength);
                                        }
                                    }
                                }
                                if (wordToCheck.IndexOf("_") > 0)
                                {
                                    int wordLength = wordToCheck.IndexOf("_");
                                    wordToCheck = Strings.Left(wordToCheck, wordLength);
                                }
                                //Find Matches
                                if (!wordToCheck.Equals("SFX") && wordToCheck.Length > 2)
                                {
                                    currentSFX = Path.GetFileNameWithoutExtension(sfxFilesToCheck[sfxItemIndexSub]);
                                    if (currentSFX.IndexOf(wordToCheck) >= 0)
                                    {
                                        //Get combo items count
                                        bool addNewItem = true;
                                        for (int comboboxIndex = 0; comboboxIndex < cboTemporal.Count; comboboxIndex++)
                                        {
                                            string comboWordItem = cboTemporal[comboboxIndex].Name;
                                            //Check for duplicated
                                            if (comboWordItem.IndexOf(wordToCheck) == -1)
                                            {
                                                continue;
                                            }
                                            //Add match to the list
                                            if (comboWordItem.Equals(wordToCheck))
                                            {
                                                cboTemporal[comboboxIndex].ItemData += 1;
                                            }
                                            addNewItem = false;
                                            break;
                                        }
                                        //Check if we have to add the new item
                                        if (addNewItem)
                                        {
                                            cboTemporal.Add(new ComboItemData(wordToCheck, 0));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Get final keywords
                    List<string> finalWords = new List<string>
                {
                    "All",
                    "HighLighted"
                };
                    int maxWordAppearances = 6;
                    while (maxWordAppearances > 5 && cboTemporal.Count > 0)
                    {
                        int itemToRemove = -1;
                        //Get max value from the remaining words
                        maxWordAppearances = 0;
                        for (int i = 0; i < cboTemporal.Count; i++)
                        {
                            int itemData = cboTemporal[i].ItemData;
                            maxWordAppearances = Math.Max(maxWordAppearances, itemData);
                        }
                        //Get the item with the max value
                        for (int i = 0; i < cboTemporal.Count; i++)
                        {
                            int itemData = cboTemporal[i].ItemData;
                            if (itemData == maxWordAppearances && itemToRemove == -1)
                            {
                                itemToRemove = i;
                            }
                        }
                        //Remove and add items
                        finalWords.Add(cboTemporal[itemToRemove].Name);
                        cboTemporal.RemoveAt(itemToRemove);
                    }

                    //Write file
                    TextFiles.WriteRefine(Path.Combine(GlobalPrefs.ProjectFolder, "System", "RefineSearch.txt"), finalWords.ToArray());
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar1.Value = e.ProgressPercentage;
            if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
            {
                TaskbarProgress.SetValue(Handle, e.ProgressPercentage, ProgressBar1.Maximum);
                TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (!IsDisposed && Environment.OSVersion.Version >= new Version(6, 1))
                {
                    TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
                }
                MessageBox.Show(e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
