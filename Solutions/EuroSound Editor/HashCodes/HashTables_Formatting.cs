namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCodeWithUsage(string hashCodeLabel, int hashCodeNumber, string sfxUsage, string tabs = "\t\t")
        {
            string formattedString = string.Format("#define {0}{1}0x{2,8}\t// {3}", hashCodeLabel, tabs, hashCodeNumber.ToString("X8"), sfxUsage);

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCode(string hashCodeLabel, int hashCodeNumber, string tabs = "\t\t")
        {
            string formattedString = string.Format("#define {0}{1}0x{2,8}", hashCodeLabel, tabs, hashCodeNumber.ToString("X8"));

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCodeComment(string hashCodeLabel, int hashCodeNumber, string tabs = "\t\t")
        {
            string formattedString = string.Format("// #define {0}{1}0x{2,8}", hashCodeLabel, tabs, hashCodeNumber.ToString("X8"));

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteNumber(string hashCodeLabel, int hashCodeNumber, string tabs = "\t\t")
        {
            string formattedString = string.Format("#define {0}{1}{2,1}", hashCodeLabel, tabs, hashCodeNumber);

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteNoAlign(string hashCodeLabel, int hashCodeNumber)
        {
            return string.Format("#define {0} 0x{1}", hashCodeLabel, hashCodeNumber.ToString("X8"));
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
