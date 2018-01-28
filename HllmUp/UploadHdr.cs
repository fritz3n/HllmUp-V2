using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace HllmUp
{
    static public class UploadHdr
    {
        static public string[] Filedata;
        static public string Username = "";
        static public bool LoggedIn = false;
        static public bool Context = false;
        static public bool Autostart = false;


        public static bool Recover()
        {
            Filedata = new string[]{ RegistryHdr.getValue("uid"),RegistryHdr.getValue("utk")};

            Boolean.TryParse(RegistryHdr.getValue("context"), out Context);
            Boolean.TryParse(RegistryHdr.getValue("autostart"), out Autostart);

            GetName();

            StartupHdr.UpdateExes(Autostart,Context);

            if(Username != "false" & Username != "")
            {
                LoggedIn = true;
                return true;
            }
            else
            {
                return false;
            }

        }


        public static bool Login(string name,string pass)
        {
            string result = postRequest("https://hllm.ddns.net/php/upload/login", new NameValueCollection()
                        {
                {"Username", name },
                {"Password", pass }
                        });
            if(result != "false" & result != "")
            {
                LoggedIn = true;
                try
                {
                    Filedata = result.Split(',');

                    RegistryHdr.setValue("uid", Filedata[0]);
                    RegistryHdr.setValue("utk", Filedata[1]);
                    RegistryHdr.setValue("active", "true");
                    GetName();
                }
                catch(Exception e)
                {
                    LoggedIn = false;
                }
                

                

                if(Username == "false" || Username == "")
                {
                    return false;
                }

                Console.WriteLine("-"+Username+"-");

                return true;
            }
            else
            {
                return false;
            }

        }

        public static void Logout()
        {
            //RegistryHdr.DelKey(@"HKEY_CLASSES_ROOT\*\shell\Upload");
            //RegistryHdr.DelKey(@"HKEY_CLASSES_ROOT\Directory\shell\Upload");

            RegistryHdr.DelKey(@"SOFTWARE","HllmUp");

            LoggedIn = false;
        }

        static public string GetName()
        {
            Username = postRequest("https://hllm.ddns.net/php/upload/confirm", new NameValueCollection()
                        {
                {"uid", Filedata[0] },
                {"utk", Filedata[1] }
                        });

            if(Username == "false" || Username == "")
            {
                LoggedIn = false;
                StartupHdr.ShowLogin();
                return "";
            }

            try
            {
                return Username;
            }catch(Exception e)
            {
                return "";
            }
        }

        private static string postRequest(string url, NameValueCollection dta)
        {
            string result = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] response = client.UploadValues(url, dta);

                    result = System.Text.Encoding.UTF8.GetString(response);

                }
                catch (Exception e)
                {
                    MessageBox.Show("There is an error with:\n"+url);
                }
            }
            return result;
        }

        static public void Auto()
        {
            if (Clipboard.ContainsFileDropList())
            {
                StringCollection List = Clipboard.GetFileDropList();

                foreach (string path in List)
                {
                    Upload(path);
                }
                System.Media.SystemSounds.Hand.Play();

            }
            else if (Clipboard.ContainsImage())
            {
                Cursor.Current = Cursors.WaitCursor;

                string path = Path.GetTempPath() + "img_" + DateTime.Now.ToString("dd-MM-yy_HH.mm.ss") + ".png";
                Image img = Clipboard.GetImage();
                img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                Upload(path, "images/", true);
                File.Delete(path);
                Cursor.Current = Cursors.Default;
                System.Media.SystemSounds.Hand.Play();
                //System.Media.SystemSounds.Hand.Play();
                //Environment.Exit(0);
            }
            else
            {
                /*OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;
                //dialog.Filter = "Audio Files";
               

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (String path in dialog.FileNames)
                    {
                        Upload(path);
                    }
                    System.Media.SystemSounds.Hand.Play();
                }
                else
                {
                    System.Media.SystemSounds.Asterisk.Play();
                }*/

                Process proc = Process.Start("snippingtool", "/clip");

                proc.WaitForExit();

                if (Clipboard.ContainsImage())
                {
                    string path = Path.GetTempPath() + "screenshot_" + DateTime.Now.ToString("dd-MM-yy_HH.mm.ss") + ".png";
                    Image img = Clipboard.GetImage();
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    Upload(path, "screenshots/", true);
                    File.Delete(path);
                    System.Media.SystemSounds.Hand.Play();
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }

            }
        }

        static public void Upload(string path, string subdir = "", bool getToken = false)
        {
            if (LoggedIn)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (File.Exists(path))
                {

                    string dataStr = Convert.ToBase64String(File.ReadAllBytes(path));
                    string filename = Path.GetFileName(path);

                    Console.WriteLine("Uploading " + filename + (subdir != "" ? " under: " + subdir : ""));

                    string response = postRequest("https://hllm.ddns.net/php/upload/upload", new NameValueCollection()
                            {
                            {"uid", Filedata[0] },
                            {"utk", Filedata[1] },
                            {"data", dataStr },
                            {"name", filename },
                            {"dir", subdir }
                            });
                    if (response != "false")
                    {
                        if (subdir == "" || getToken)
                        {
                            Clipboard.SetText(response);
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("OK");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {

                        ConsoleHdr.writeE("ERROR UPLOADING");
                        //Console.ReadKey();
                        //System.Threading.Thread.Sleep(2000);
                    }
                }
                else if (Directory.Exists(path))
                {


                    bool token = false;
                    if (subdir == "" || getToken)
                    {
                        token = true;
                    }

                    Console.WriteLine("Encountered /" + new DirectoryInfo(path).Name + " ...");
                    subdir = subdir + new DirectoryInfo(path).Name + "/";
                    string[] paths = Directory.GetFileSystemEntries(path);
                    foreach (string newpath in paths)
                    {
                        Upload(newpath, subdir);
                    }

                    if (token)
                    {
                        string response = postRequest("https://hllm.ddns.net/php/upload/token", new NameValueCollection()
                        {
                        {"uid", Filedata[0] },
                        {"utk", Filedata[1] },
                        {"name", new DirectoryInfo(path).Name },
                        {"dir", "" }
                        });

                        Clipboard.SetText(response);
                    }


                }
                else
                {
                    ConsoleHdr.writeE(Path.GetFileName(path) + " not found!");
                }

                
            }
            else
            {
                StartupHdr.ShowLogin();
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
