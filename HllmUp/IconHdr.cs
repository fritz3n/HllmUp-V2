using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace HllmUp
{
    class IconHdr : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        Form1 configWindow = new Form1();

        public IconHdr()
        {
            string[] arguments = Environment.GetCommandLineArgs();

            //MessageBox.Show(arguments.Concat);

            if (arguments.Length > 1)
            {
                if (arguments[1] == "RegisterRC")
                {
                    StartupHdr.RegisterRC();
                    notifyIcon.Visible = false;
                    //Application.Exit();
                    //MessageBox.Show("closing");
                    Environment.Exit(0);
                }
                else if (arguments[1] == "DeRegisterRC")
                {
                    StartupHdr.DeRegisterRC();
                    notifyIcon.Visible = false;
                    //Application.Exit();
                    //MessageBox.Show("closing");
                    Environment.Exit(0);
                }
                else
                {
                    StartupHdr.Startup(configWindow);

                    for (int i = 1; i < arguments.Count(); i++)
                    {
                        string path = arguments[i];
                        UploadHdr.Upload(path);

                    }
                    System.Media.SystemSounds.Hand.Play();
                    Environment.Exit(0);
                }
            }
            else
            {

                HotKeyHdr Hotkeys = new HotKeyHdr();


                MenuItem consoleMenuItem = new MenuItem("Console", new EventHandler(ShowConsole));
                MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
                MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));


                notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location); ;
                notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                    { consoleMenuItem, configMenuItem, exitMenuItem });
                notifyIcon.MouseClick += new MouseEventHandler(LeftClick);
                notifyIcon.Visible = true;

                StartupHdr.Startup(configWindow);
            }
        }

        void ShowConfig(object sender, EventArgs e)
        {
            // If we are already showing the window, merely focus it.
            if (configWindow.Visible)
            {
                configWindow.Activate();
            }
            else
            {
                configWindow.ShowDialog();
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;
            //MessageBox.Show("closing");
            Application.Exit();
        }

        void ShowConsole(object sender, EventArgs e)
        {
            ConsoleHdr.StartConsole();
        }

        void LeftClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //MessageBox.Show(sender.ToString() + "\n" + e.ToString());
                UploadHdr.Auto();
            }
        }
    }
}

