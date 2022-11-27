using System;
using System.Collections.Generic;
using System.IO;

namespace sb_editor.Objects
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public class SamplePool
    {
        public FileHeader HeaderData = new FileHeader();
        public Dictionary<string, SamplePoolItem> SamplePoolItems = new Dictionary<string, SamplePoolItem>(StringComparer.OrdinalIgnoreCase);

        //-------------------------------------------------------------------------------------------------------------------------------
        public void CheckForUpdates()
        {
            //Check For Extra ReSample
            foreach (KeyValuePair<string, SamplePoolItem> itemToCheck in SamplePoolItems)
            {
                string fullpath = Path.Combine(GlobalPrefs.CurrentProject.SampleFilesFolder, "Master", itemToCheck.Key.TrimStart(Path.DirectorySeparatorChar));
                if (File.Exists(fullpath))
                {
                    string lastDate = CommonFunctions.GetSampleDate(fullpath);
                    if (!itemToCheck.Value.Date.Trim().Equals(lastDate))
                    {
                        itemToCheck.Value.Date = lastDate;
                        itemToCheck.Value.ReSample = true;
                    }
                    if (!itemToCheck.Value.Size.Trim().Equals(new FileInfo(fullpath).Length.ToString()))
                    {
                        itemToCheck.Value.Size = CommonFunctions.GetSampleSize(fullpath);
                        itemToCheck.Value.ReSample = true;
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}