using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace HllmUp
{
    static class ConsoleHdr
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        static bool ConsoleOpen = false;
        static private bool CommandLineRunning = false;

        public static void writeE(string txt)
        {
            AllocCons();
            System.Media.SystemSounds.Asterisk.Play();
            Console.ForegroundColor = ConsoleColor.Yellow;
            write(txt);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void write(string Str)
        {
            Console.WriteLine(Str);
        }

        static public void AllocCons()
        {
            if (!ConsoleOpen)
            {
                AllocConsole();
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
                ConsoleOpen = true;

                Console.WriteLine("Use exit to close!");
            }
            else
            {
                ShowWindow(GetConsoleWindow(), 9);
            }
        }

        static public void StartConsole()
        {
            AllocCons();

            //CommandlineMode();
            if (!CommandLineRunning)
            {
                CommandLineRunning = true;
                Thread tid1 = new Thread(new ThreadStart(CommandlineMode));
                tid1.Start();
            }
        }

        static private bool commandLineStep()
        {
            Console.Write(">");
            string raw = Console.ReadLine();
            int x = 2;
            string[] rawArray = raw.Split(new char[] { ' ' }, x);
            string command = rawArray[0];

            List<string> args = new List<string>();

            if (rawArray.Length > 1)
            {

                string pattern = "[\"'](.*?)[\"']|([^ \"'\\s]+)";

                Regex rgx = new Regex(pattern);
                int[] groupNumbers = rgx.GetGroupNumbers();

                Match m = rgx.Match(rawArray[1]);

                while (m.Success)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        Group g = m.Groups[i];
                        CaptureCollection cc = g.Captures;
                        if (cc.Count > 0)
                        {
                            args.Add(cc[0].Value);
                        }
                    }
                    m = m.NextMatch();
                }
            }

            switch (command)
            {
                case "damn":
                case "bye":
                case "kys":
                case "quit":
                case "close":
                case "exit":
                    write("Bye!");
                    System.Threading.Thread.Sleep(500);
                    return false;
                    //FreeCons();
                    break;

                case "upload":
                    if (args.Count == 0)
                    {
                        OpenFileDialog opf = new OpenFileDialog();
                        DialogResult result = opf.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            UploadHdr.Upload(opf.FileName);
                        }
                        else
                        {
                            writeE("There was an problem with:\n" + opf.FileName);
                        }
                    }
                    else
                    {
                        foreach (string path in args)
                        {
                            UploadHdr.Upload(path);
                        }
                    }
                    break;

                case "logout":
                    UploadHdr.Logout();
                    Console.WriteLine("Please log back in");
                    StartupHdr.ShowLogin();
                    break;

                case "screenshot":

                    Process proc = Process.Start("snippingtool", "/clip");

                    proc.WaitForExit();

                    if (Clipboard.ContainsImage())
                    {
                        string path = Path.GetTempPath() + "screenshot_" + DateTime.Now.ToString("dd-MM-yy_HH.mm.ss") + ".png";
                        Image img = Clipboard.GetImage();
                        img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                        UploadHdr.Upload(path, "screenshots/", true);
                        File.Delete(path);
                    }
                    else
                    {
                        writeE("Theres no picture!");
                    }
                    break;

                case "fullscreenshot":

                    string ImgPath = Path.GetTempPath() + "screenshot_" + DateTime.Now.ToString("dd-MM-yy_HH.mm.ss") + ".png";
                    Bitmap image = HotKeyHdr.screenshot();
                    image.Save(ImgPath, System.Drawing.Imaging.ImageFormat.Png);
                    UploadHdr.Upload(ImgPath, "screenshots/", true);
                    File.Delete(ImgPath);

                    break;


                default:
                    writeE("Unrecognized command: " + command);
                    break;
            }

            return true;

        }

        static public void CommandlineMode()
        {
            Console.WriteLine("Command line mode!");
            while (commandLineStep());
            FreeCons();
            CommandLineRunning = false;
        }

        static public void FreeCons()
        {
            if (ConsoleOpen)
            {
                FreeConsole();
                ConsoleOpen = false;
            }
        }
    }
}
