using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;    //MemoryStream
using System.Net;   //WebRequest
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.CV.Text;
using Emgu.CV.Util;
using HtmlAgilityPack;
using EarthQuakeLib;

namespace RMTProcessing
{
    public partial class RMTProcessingMainForm : Form
    {
        #region System Management
        enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }
        private int SC_MONITORPOWER = 0xF170;
        private uint WM_SYSCOMMAND = 0x0112;
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        #endregion

        #region data type & class
 
        float SourceImageScale
        {
            get
            {
                return (float)(tabPageSourceImage.Width - picSource.Left * 2) / (float)RmtCore.sizeRmtImage.Width;
            }
        }
        float OutputImageScale
        {
            get
            {
                return (float)(tabPageOutputImage.Width - picOutput.Left * 2) / (float)RmtCore.sizeRmtImage.Width;
            }
        }
        #endregion

        #region variables

        Histrogram satHistrogram = new Histrogram();
        bool isTrace = true;

        #endregion

        #region common functions
        delegate void monitorStateHandler(MonitorState state);
        private void setMonitorStateThread(MonitorState state)
        {
            SendMessage(this.FindForm().Handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)state);
        }
        private void SetMonitorState(MonitorState state)
        {
            this.Invoke(new monitorStateHandler(setMonitorStateThread), state);
        } //SetMonitorState()    
        private void ScreenCapture()
        {
            int MonitorIndex = 0; //default monitor number

            // Turn on screen before capture to enable buffer refresh
            SetMonitorState(MonitorState.ON);
            Thread.Sleep(100); //wait monitor on

            using (Bitmap bmpScreenCapture = new Bitmap(Screen.AllScreens[MonitorIndex].Bounds.Width,
                                            Screen.AllScreens[MonitorIndex].Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.AllScreens[MonitorIndex].Bounds.X,
                                     Screen.AllScreens[MonitorIndex].Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);
                }
                String FilePath = rmtCore.fileRoot.CreateSubPath("PrintScreen", FileRoot.Type.Daily);
                String FileName = "PrintScreen" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
                String BmpFileName = FilePath + "\\" + FileName;
                bmpScreenCapture.Save(BmpFileName);

                if (chkRmtWarnSound.Checked)
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"shutter.wav");
                    simpleSound.Play();
                }

                /*if (chkBalloonTips.Checked && this.WindowState == FormWindowState.Minimized)
                {
                    notifyIcon.BalloonTipText = FileName;
                    notifyIcon.ShowBalloonTip(200);
                }
                */
            }
        } //ScreenCapture()
 
        #endregion

        #region file management
        String saveSourceImage(DateTime timeTag)
        {
            showRmtMessage("Save Source Image");
            String FilePath = rmtCore.fileRoot.CreateSubPath("Source", FileRoot.Type.Hourly);
            String FileName = String.Format("RMT{0}.png", timeTag.ToString("yyyy-MM-dd_HH-mm-ss"));
            String BmpFileName = FilePath + FileName;

            if (picSource.Image != null)
            {
                try
                {
                    picSource.Image.Save(BmpFileName);
                } catch
                {
                    BmpFileName = null;
                }
            }
            showRmtMessage("");
            return BmpFileName;
        }
        String saveCaptureSourceImage(DateTime timeTag)
        {
            if (rmtCore.sourceImage == null) //no image
            {
                return null;
            }
            showRmtMessage("save Capture Source Image");
            String FilePath = rmtCore.fileRoot.CreateSubPath("Capture", FileRoot.Type.Daily);
            String FileName = String.Format("RMT{0}_Source.png", timeTag.ToString("yyyy-MM-dd_HH-mm-ss"));

            String BmpFileName = rmtCore.SaveSourceImage(FilePath, FileName);

            showRmtMessage("");
            return BmpFileName;
        }
        String saveCaptureOutputImage(DateTime timeTag)
        {
            if (rmtCore.outputImage == null) //no image
            {
                return null;
            }
            showRmtMessage("save Capture Output Image");
            String FilePath = rmtCore.fileRoot.CreateSubPath("Capture", FileRoot.Type.Daily);
            String FileName = String.Format("RMT{0}_Output.png", timeTag.ToString("yyyy-MM-dd_HH-mm-ss"));

            String BmpFileName = rmtCore.SaveOutputImage(FilePath, FileName);

            showRmtMessage("");
            return BmpFileName;
        }
        String saveWarningImage(DateTime timeTag)
        {
            if (rmtCore.outputImage == null) //no image
            {
                return null;
            }
            showRmtMessage("Save Warning Image");
            String FilePath = rmtCore.fileRoot.CreateSubPath("Warning", FileRoot.Type.Daily);
            String FileName = String.Format("RMT_{0}_d{1}w{2}.png", timeTag.ToString("yyyy-MM-dd_HH-mm-ss"), RmtSignalDiff, rmtCore.signalWarningCount);

            String BmpFileName = rmtCore.SaveOutputImage(FilePath, FileName);

            showRmtMessage("");
            return BmpFileName;
        }
        String saveNanImage(DateTime timeTag)
        {
            if (rmtCore.outputImage == null) //no image
            {
                return null;
            }
            showRmtMessage("save NaN Image");
            String FilePath = rmtCore.fileRoot.CreateSubPath("NaN", FileRoot.Type.Daily);
            String FileName = String.Format("RMT_{0}_NaN{1:D2}.png", timeTag.ToString("yyyy-MM-dd_HH-mm-ss"), rmtCore.nanLabelList.Count);

            String BmpFileName = rmtCore.SaveOutputImage(FilePath, FileName);

            showRmtMessage("");
            return BmpFileName;
        }
        String saveHimawari8SourceImage(Himawari8.Area area, Himawari8.ImageType type, DateTime pictureTime)
        {
            if (satHimawari8.sourceImage==null) //no image
            {
                return null; 
            }
            DateTime fileTag = new DateTime(pictureTime.Year, pictureTime.Month, pictureTime.Day, pictureTime.Hour, (pictureTime.Minute / 10) * 10, 0);
            showHimawari8Message("save CWB Image");
            String subPath = String.Format("CWB\\{0}\\{1}", Himawari8.ImageAreaName[(int)area], Himawari8.ImageTypeName[(int)type]);
            String FilePath = rmtCore.fileRoot.CreateSubPath(subPath, FileRoot.Type.Daily);
            String FileName = String.Format("{0}_{1}_{2}_{3}.png", (type == Himawari8.ImageType.RadarCompositeReflect) ? "Radar" : "Himawari8",
                                fileTag.ToString("yyyy-MM-dd_HH-mm-ss"), Himawari8.ImageAreaName[(int)area], Himawari8.ImageTypeName[(int)type]);

            String BmpFileName = satHimawari8.SaveSourceImage(FilePath, FileName);

            showHimawari8Message("");
            return BmpFileName;
        }
        #endregion

        #region download manager
        delegate void downloadHimawari8Handler(Himawari8.Area area, Himawari8.ImageType type, DateTime pictureTime);
        bool DownloadRmtImage()
        {
            showRmtMessage("Download RMT Image");
            bool result = rmtCore.DownloadRmtImage();
            if (rmtCore.sourceImage != null)
            {
                picSource.Image = (Image)rmtCore.sourceImage.Clone();
            }
            showRmtMessage(rmtCore.status);
            return result;
        }
        void DownloadHimawari8Image(Himawari8.Area area, Himawari8.ImageType type)
        {
            DateTime timeTag = DateTime.Now;
            //update picHimawari8.Image 
            showHimawari8Message("Download Himawari8 Image");
            this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), area, type, timeTag);
            if (satHimawari8.sourceImage != null)
            {
                picHimawari8.Image = satHimawari8.sourceImage;
            } 

            showHimawari8Message(satHimawari8.status);
        }
        #endregion

        #region misc functions 
        #endregion
    }
}
