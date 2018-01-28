using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HllmUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Update.Start();
        }

        private void UpdateAll()
        {
            if(UploadHdr.LoggedIn == true)
            {
                LogoutButt.Enabled = true;

                Status.ForeColor = Color.Black;
                Status.Text = "Logged in as:";
                UsernameLabel.Text = UploadHdr.Username;
            }
            else
            {
                LogoutButt.Enabled = false;

                Status.ForeColor = Color.Red;
                Status.Text = "Not logged in!";
                UsernameLabel.Text = "";
            }

            AutostartLabel.Text = "Autostart: " + UploadHdr.Autostart;
            ContextLabel.Text = "Contextmenu: " + UploadHdr.Context;


            //ContextEnable.Checked = UploadHdr.Context;
            //AutostartEnable.Checked = UploadHdr.Autostart;
        }

        private void LoginButt_Click(object sender, EventArgs e)
        {
            Login();

        }

        private void Update_Tick(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void LogoutButt_Click(object sender, EventArgs e)
        {
            UploadHdr.Logout();
            UpdateAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ContextEnable.Checked = true;
            AutostartEnable.Checked = true;
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Password.Focus();
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            LoginButt.Enabled = false;

            bool worked = UploadHdr.Login(Username.Text, Password.Text);

            if (RegistryHdr.getValue("context") != ContextEnable.Checked.ToString() & !(ContextEnable.Checked == false & RegistryHdr.getValue("context") == ""))
            {
                //todo: E/Disable content thingy thing
                MessageBox.Show("An elevated Process needs to be started to enable explorer right-clicking.\nPlease click yes.");

                var exeName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).Replace('/', '\\');
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";

                if (ContextEnable.Checked == true)
                {
                    startInfo.Arguments = "RegisterRC";
                }
                else
                {
                    startInfo.Arguments = "DeRegisterRC";
                }

                Process p = Process.Start(startInfo);
                p.WaitForExit();

                RegistryHdr.setValue("context", ContextEnable.Checked.ToString());
                UploadHdr.Context = ContextEnable.Checked;
            }

            if (RegistryHdr.getValue("autostart") != AutostartEnable.Checked.ToString())
            {
                StartupHdr.Autostart(AutostartEnable.Checked);
            }

            UpdateAll();

            if (worked == true)
            {

                Username.Text = "";
                Password.Text = "";

                ErrorText.Text = "";

                System.Media.SystemSounds.Hand.Play();

            }
            else
            {
                ErrorText.Text = "Couldn't log in!";
                Password.Text = "";

                System.Media.SystemSounds.Asterisk.Play();
            }

            LoginButt.Enabled = true;
        }
    }
}
