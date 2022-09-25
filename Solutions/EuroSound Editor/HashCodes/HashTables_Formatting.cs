namespace sb_editor.HashCodes
{
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------
    internal partial class HashTables
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCode(string hashCodeLabel, string hashCodeNumber)
        {
            string formattedString;
            int stringLength = 20;
            if (hashCodeLabel.Length < stringLength)
            {
                formattedString = string.Format("#define {0,-19} {1,8}", hashCodeLabel, hashCodeNumber);
            }
            else
            {
                while (stringLength <= hashCodeLabel.Length)
                {
                    stringLength += 14;
                }
                formattedString = string.Format("#define {0,-" + (stringLength - 1) + "} {1,8}", hashCodeLabel, hashCodeNumber);
            }
            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteHashCodeComment(string hashCodeLabel, string hashCodeNumber)
        {
            string formattedString;
            if (hashCodeLabel.Length < 19)
            {
                formattedString = string.Format("// #define {0,-16} {1,8}", hashCodeLabel, hashCodeNumber);
            }
            else
            {
                formattedString = string.Format("// #define {0,-30} {1,8}", hashCodeLabel, hashCodeNumber);
            }
            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteNumber(string hashCodeLabel, string hashCodeNumber)
        {
            int stringLength = 20;
            string formattedString;
            if (hashCodeLabel.Length < stringLength)
            {
                formattedString = string.Format("#define {0,-19} {1,1}", hashCodeLabel, hashCodeNumber);
            }
            else
            {
                while (stringLength <= hashCodeLabel.Length)
                {
                    stringLength += 14;
                }
                formattedString = string.Format("#define {0,-" + (stringLength - 1) + "} {1,1}", hashCodeLabel, hashCodeNumber);
            }
            return formattedString;
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        internal string WriteNoAlign(string hashCodeLabel, string hashCodeNumber)
        {
            return string.Format("#define {0} {1}", hashCodeLabel, hashCodeNumber);
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------
}
