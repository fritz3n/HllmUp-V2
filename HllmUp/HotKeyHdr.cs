using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HllmUp
{
    class HotKeyHdr
    {
        KeyboardHook Upload;
        KeyboardHook Screenshot;
        KeyboardHook ScreenClip;

        public HotKeyHdr()
        {
            Upload = new KeyboardHook();
            Screenshot = new KeyboardHook();
            ScreenClip = new KeyboardHook();

            Upload.KeyPressed += new EventHandler<KeyPressedEventArgs>(Upload_KeyPressed);
            Screenshot.KeyPressed += new EventHandler<KeyPressedEventArgs>(Screenshot_KeyPressed);
            ScreenClip.KeyPressed += new EventHandler<KeyPressedEventArgs>(ScreenClip_KeyPressed);

            Upload.RegisterHotKey(ModifierKeys2.Control | ModifierKeys2.Alt | ModifierKeys2.NoRepeat,
                Keys.C);

            Screenshot.RegisterHotKey(ModifierKeys2.Control | ModifierKeys2.Shift | ModifierKeys2.NoRepeat,
                Keys.D);

            ScreenClip.RegisterHotKey(ModifierKeys2.Control | ModifierKeys2.Shift | ModifierKeys2.NoRepeat,
                Keys.S);
        }

        public void Upload_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            UploadHdr.Auto();
        }

        public void Screenshot_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string ImgPath = Path.GetTempPath() + "screenshot_" + DateTime.Now.ToString("dd-MM-yy_HH.mm.ss") + ".png";
            Bitmap image = screenshot();
            image.Save(ImgPath, System.Drawing.Imaging.ImageFormat.Png);
            UploadHdr.Upload(ImgPath, "screenshots/", true);
            File.Delete(ImgPath);
            System.Media.SystemSounds.Hand.Play();
        }

        public void ScreenClip_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            Process proc = Process.Start("snippingtool", "/clip");

            proc.WaitForExit();

            if (Clipboard.ContainsImage())
            {
                string path = Path.GetTempPath() + "screenshot_" + DateTime.Now.ToString("dd-MM-yy_HH.mm.ss") + ".png";
                Image img = Clipboard.GetImage();
                img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                UploadHdr.Upload(path, "screenshots/", true);
                File.Delete(path);
                System.Media.SystemSounds.Hand.Play();
            }
            else
            {
                MessageBox.Show("ERROR!");
            }
        }

        public static Bitmap screenshot()
        {//Create a new bitmap.

            int screenLeft = SystemInformation.VirtualScreen.Left;
            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;

            var bmpScreenshot = new Bitmap(screenWidth,
                                           screenHeight,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(screenLeft,
                                        screenTop,
                                        0,
                                        0,
                                        bmpScreenshot.Size,
                                        CopyPixelOperation.SourceCopy);
            return bmpScreenshot;
        }
    }
}
