using Microsoft.Win32;

namespace sb_explorer
{
    public class WinRegFunctions
    {
        public static void CreateSubKeyValue(string keyName, string valueKey, object valueName, RegistryValueKind typeOfData)
        {
            //Open default location, in this case the Software subkey
            using (RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true))
            {
                //Inside software, try to open the specified subkey, If not exists, we will create it
                if (SoftwareKey.OpenSubKey(keyName, true) == null)
                {
                    SoftwareKey.CreateSubKey(keyName);
                }

                //Create the value with the specified data
                using (RegistryKey keyPath = SoftwareKey.OpenSubKey(keyName, true))
                {
                    keyPath.SetValue(valueKey, valueName, typeOfData);
                    keyPath.Close();
                }

                //Finally close key
                SoftwareKey.Close();
            }
        }

        //*===============================================================================================
        //* FUNCTIONS TO GET VALUES FROM SUBKEYS
        //*===============================================================================================
        public static string GetSubkeyStringValue(string keyName, string valueKeyName, string defValue = "")
        {
            string keyValue = defValue;

            //Open default location, in this case the Software subkey
            using (RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true))
            {
                //Inside software, try to open the specified subkey if exists
                if (SoftwareKey.OpenSubKey(keyName, true) != null)
                {
                    //Retrieve the value of the specified key
                    using (RegistryKey specifiedKey = SoftwareKey.OpenSubKey(keyName, false))
                    {
                        keyValue = specifiedKey.GetValue(valueKeyName, defValue).ToString();
                        specifiedKey.Close();
                    }
                }

                //Finally close key
                SoftwareKey.Close();
            }

            return keyValue;
        }

        public static int GetSubkeyIntValue(string keyName, string valueKeyName, int defValue = 0)
        {
            int keyValue = defValue;

            //Open default location, in this case the Software subkey
            using (RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true))
            {
                //Inside software, try to open the specified subkey if exists
                if (SoftwareKey.OpenSubKey(keyName, true) != null)
                {
                    //Retrieve the value of the specified key
                    using (RegistryKey specifiedKey = SoftwareKey.OpenSubKey(keyName, false))
                    {
                        keyValue = (int)specifiedKey.GetValue(valueKeyName, defValue);
                        specifiedKey.Close();
                    }
                }

                //Finally close key
                SoftwareKey.Close();
            }

            return keyValue;
        }
    }
}
