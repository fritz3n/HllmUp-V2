using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HllmUp
{
    static class StartupHdr
    {
        static public Form1 Form;
        static public void Startup(Form1 form)
        {
            Form = form;

            if (UploadHdr.Recover() != true)
            {
                form.ShowDialog();
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        static public void ShowLogin()
        {

            System.Media.SystemSounds.Asterisk.Play();
            Form.ShowDialog();
        }

        public static void RegisterRC()
        {
            string curPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).Replace('/', '\\');
            string newFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp";
            string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp\HllmUp.exe";

            if (!System.IO.Directory.Exists(newFolder))
            {
                Directory.CreateDirectory(newFolder);
            }

            System.IO.File.Copy(curPath, newPath, true);

            try
            {
                Registry.SetValue(@"HKEY_CLASSES_ROOT\*\shell\Upload\command", null, "\"" + newPath + "\" \"%1\"");
                Registry.SetValue(@"HKEY_CLASSES_ROOT\Directory\shell\Upload\command", null, "\"" + newPath + "\" \"%1\"");
            }//make shure we don't crash but close
            catch (Exception e)
            {
                MessageBox.Show("An Error ucorred while trying to writing to the registry!\n" + e.Message);
            }
        }

        public static void DeRegisterRC()
        {
            string Folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp";

            Directory.Delete(Folder, true);

            RegistryHdr.DelRootKey(@"*\shell","Upload");
            RegistryHdr.DelRootKey(@"Directory\shell","Upload");
            

        }

        public static void Autostart(bool enable)
        {
            string curPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).Replace('/', '\\');
            string newFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp\Autostart";
            string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp\Autostart\HllmUp.exe";

            if (enable)
            {

                if (!System.IO.Directory.Exists(newFolder))
                {
                    Directory.CreateDirectory(newFolder);
                }

                System.IO.File.Copy(curPath, newPath, true);

                RegistryHdr.setValueGlobal(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run","HllmUp",newPath);

                

            }
            else
            {

                RegistryHdr.DelValue(@"Software\Microsoft\Windows\CurrentVersion\Run","HllmUp");

            }

            RegistryHdr.setValue("autostart", enable.ToString());
            UploadHdr.Autostart = enable;
        }

        public static void UpdateExes(bool Auto, bool Context)
        {
            if(Auto == true)
            {
                string curPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).Replace('/', '\\');
                string newFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp\Autostart";
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp\Autostart\HllmUp.exe";

                if (curPath != newPath)
                {

                    if (!System.IO.Directory.Exists(newFolder))
                    {
                        Directory.CreateDirectory(newFolder);
                    }

                    System.IO.File.Copy(curPath, newPath, true);
                }
            }

            if(Context == true)
            {
                string curPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).Replace('/', '\\');
                string newFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp";
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\HllmUp\HllmUp.exe";

                if (curPath != newPath)
                {

                    if (!System.IO.Directory.Exists(newFolder))
                    {
                        Directory.CreateDirectory(newFolder);
                    }

                    System.IO.File.Copy(curPath, newPath, true);
                }
            }
        }
    }
}
