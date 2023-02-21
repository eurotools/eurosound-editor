using System.Globalization;
using System.IO;

namespace sb_editor
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    public static class GlobalPrefs
    {
        public static NumberFormatInfo NumericProvider = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        public static string ProjectFolder = string.Empty;
        public static string EuroSoundUser = string.Empty;

        public static string SoxEffect = "resample -qs 0.97";

        //Formats
        public static string FilesDateFormat = "MM-dd-yyyy HH:mm:ss";
        public static string DateFormat = "yyyy/dd/MM HH:mm:ss";

        //HashCodes counters 
        private static int _SoundBankHashCodeNumber, _SFXHashCodeNumber, _MFXHashCodeNumber, _ReverbHashCodeNumber;
        private static bool _ReSampleStreams;

        //-------------------------------------------------------------------------------------------------------------------------------
        public static int SFXHashCodeNumber
        {
            get { return _SFXHashCodeNumber; }
            set { _SFXHashCodeNumber = value; TextFiles.WriteMiscFile(Path.Combine(ProjectFolder, "System", "Misc.txt")); }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static int SoundBankHashCodeNumber
        {
            get { return _SoundBankHashCodeNumber; }
            set { _SoundBankHashCodeNumber = value; TextFiles.WriteMiscFile(Path.Combine(ProjectFolder, "System", "Misc.txt")); }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static int MFXHashCodeNumber
        {
            get { return _MFXHashCodeNumber; }
            set { _MFXHashCodeNumber = value; TextFiles.WriteMiscFile(Path.Combine(ProjectFolder, "System", "Misc.txt")); }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static int ReverbHashCodeNumber
        {
            get { return _ReverbHashCodeNumber; }
            set { _ReverbHashCodeNumber = value; TextFiles.WriteMiscFile(Path.Combine(ProjectFolder, "System", "Misc.txt")); }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public static bool ReSampleStreams
        {
            get { return _ReSampleStreams; }
            set { _ReSampleStreams = value; TextFiles.WriteMiscFile(Path.Combine(ProjectFolder, "System", "Misc.txt")); }
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
