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
            int stringLength = 19;
            if (hashCodeLabel.Length > stringLength)
            {
                stringLength = ((((hashCodeLabel.Length - 20) / 14) + 1) * 14) + 19;
            }
            string formattedString = string.Format("#define {0,-" + stringLength + "} {1,8}", hashCodeLabel, hashCodeNumber);

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
        internal string WriteNumber(string hashCodeLabel, int hashCodeNumber)
        {
            int stringLength = 19;
            if (hashCodeLabel.Length > stringLength)
            {
                stringLength = ((((hashCodeLabel.Length - 20) / 14) + 1) * 14) + 19;
            }
            string formattedString = string.Format("#define {0,-" + stringLength + "} {1,1}", hashCodeLabel, hashCodeNumber);

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
