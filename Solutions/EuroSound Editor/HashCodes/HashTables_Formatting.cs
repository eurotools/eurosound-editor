namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCode(string hashCodeLabel, int hashCodeNumber)
        {
            int stringLength = GetColumnWidth(hashCodeLabel.Length);
            string formattedString = string.Format("#define {0,-" + stringLength + "} 0x{1,8}", hashCodeLabel, hashCodeNumber.ToString("X8"));

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCodeComment(string hashCodeLabel, int hashCodeNumber)
        {
            int stringLength = GetColumnWidth(hashCodeLabel.Length + 3) - 3; // 3 for the "// "
            string formattedString = string.Format("// #define {0,-" + stringLength + "} 0x{1,8}", hashCodeLabel, hashCodeNumber.ToString("X8"));

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteNumber(string hashCodeLabel, int hashCodeNumber)
        {
            int stringLength = GetColumnWidth(hashCodeLabel.Length);
            string formattedString = string.Format("#define {0,-" + stringLength + "} {1,1}", hashCodeLabel, hashCodeNumber);

            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteNoAlign(string hashCodeLabel, int hashCodeNumber)
        {
            return string.Format("#define {0} 0x{1}", hashCodeLabel, hashCodeNumber.ToString("X8"));
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private int GetColumnWidth(int stringLength)
        {
            int colLength = 19;
            if (stringLength > colLength)
            {
                colLength = ((((stringLength - 20) / 14) + 1) * 14) + 19;
            }

            return colLength;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
