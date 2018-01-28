using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HllmUp
{
    static class RegistryHdr
    {
        public static bool setValue(string name, string value)
        {
            try
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\HllmUp", name, value);
                return true;
            }//make shure we don't crash but close
            catch (Exception e)
            {
                //MessageBox.Show("An Error ucorred while trying to writing to the registry!\n" + e.Message);
                return false;
            }
        }

        public static bool setValueGlobal(string path, string name, string value)
        {
            try
            {
                Registry.SetValue(path, name, value);
                return true;
            }//make shure we don't crash but close
            catch (Exception e)
            {
                //MessageBox.Show("An Error ucorred while trying to writing to the registry!\n" + e.Message);
                return false;
            }
        }

        //function for getting the regvalue
        public static string getValue(string name)
        {
            try
            {
                return (Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\HllmUp", name, "").ToString());
            }
            catch (Exception e)
            {
                //MessageBox.Show("An Error ucorred while trying to read from the registry!\n" + e.Message + "\n" + e.HelpLink);
                //Application.Exit();
                return "";
            }
        }

        public static void DelKey(string path,string name)
        {
            try
            {
                using (RegistryKey explorerKey =
                Registry.CurrentUser.OpenSubKey(path, writable: true))
                {
                    if (explorerKey != null)
                    {
                        explorerKey.DeleteSubKeyTree(name);
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("An Error ucorred while trying to read from the registry!\n" + e.Message + "\n" + e.HelpLink);
                //Application.Exit();
            }
        }

        public static void DelValue(string path,string name)
        {
            try
            {
                using (RegistryKey explorerKey =
                Registry.CurrentUser.OpenSubKey(path, writable: true))
                {
                    if (explorerKey != null)
                    {
                        explorerKey.DeleteValue(name,false);
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("An Error ucorred while trying to read from the registry!\n" + e.Message + "\n" + e.HelpLink);
                //Application.Exit();
            }
        }

        public static void DelRootKey(string path, string name)
        {
            try
            {
                using (RegistryKey explorerKey =
                Registry.ClassesRoot.OpenSubKey(path, writable: true))
                {
                    if (explorerKey != null)
                    {
                        explorerKey.DeleteSubKeyTree(name);
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("An Error ucorred while trying to read from the registry!\n" + e.Message + "\n" + e.HelpLink);
                //Application.Exit();
            }
        }
    }
}
