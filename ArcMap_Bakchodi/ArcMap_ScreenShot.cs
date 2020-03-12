using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using calculator;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ArcMap_ScreenShot
{
    public class ArcMap_ScreenShot : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        int i;

        int random;

        IntPtr hWnd;
        public ArcMap_ScreenShot()
        {
            Random random1 = new Random();

            i = 1;

            random = random1.Next();

           
        }

        protected override void OnClick()
        {
            
                try

                {
                string screenDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ArcGIS\ScreenShots";

                if(!Directory.Exists(screenDir))
                {
                    Directory.CreateDirectory(screenDir);
                }
                
                    Bitmap captureBitmap = new Bitmap(1920, 1020, PixelFormat.Format32bppArgb);

                //Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                Rectangle captureRectangle = Screen.FromHandle(hWnd).WorkingArea;

                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 330, 0, captureRectangle.Size);

                    string path = screenDir + @"\ScreenShot_"+ random +"_" + i++ + ".jpg";

                    captureBitmap.Save(path, ImageFormat.Jpeg);

                    //MessageBox.Show("Screen Captured");

                    Process.Start(path);
                
                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message);

                }
            
            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
