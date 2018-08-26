using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Media;
using System.Net;

namespace EarthQuakeLib
{
    #region Generic Support
    public static class Log
    {
        static Queue<String> dQueue = new Queue<String>(100);
        static Queue<String> eQueue = new Queue<String>(100);
        static Queue<String> sqlQueue = new Queue<String>(100);

        public static String debugLogName
        {
            get {
                return String.Format("Debug_{0:0000}-{1:00}.log", DateTime.Now.Year, DateTime.Now.Month);
            }
        }
        internal static StreamWriter OpenLogFile(string FileName)
        {
            if (!File.Exists(FileName))
            {
                return File.CreateText(FileName);
            }
            else
            {
                return File.AppendText(FileName);
            }
        }

        public static void d(string strMsg)
        {
            dQueue.Enqueue(strMsg);

            try //handle multiple write
            {
                StreamWriter LogFileStream = OpenLogFile(debugLogName);
                while (dQueue.Count > 0)
                {
                    LogFileStream.WriteLine("[" + DateTime.Now.ToString() + "] " + dQueue.Dequeue());
                }
                LogFileStream.Close();
            }
            catch
            {

            }
        }
        public static void e(string strMsg)
        {
            eQueue.Enqueue(strMsg);

            try //handle multiple write
            {
                StreamWriter LogFileStream = OpenLogFile(String.Format("Error_{0:0000}-{1:00}.log", DateTime.Now.Year, DateTime.Now.Month));
                while (eQueue.Count > 0)
                {
                    LogFileStream.WriteLine("[" + DateTime.Now.ToString() + "] " + eQueue.Dequeue());
                }
                LogFileStream.Close();
            }
            catch
            {

            }

            d(strMsg); //copy to debug file
        }

        public static void ddump(int[] x)
        {
            String strMsg = "";
            for (int i = 0; i < x.Length-1; i++)
                strMsg += String.Format("x[{0}]={1}, ",i,x[i]);
            strMsg += String.Format("x[{0}]={1},", x.Length - 1, x[x.Length - 1]);
            d(strMsg); //copy to debug file
        }
        public static void sql(string strMsg)
        {
            sqlQueue.Enqueue(strMsg);

            try //handle multiple write
            {
                StreamWriter LogFileStream = OpenLogFile(String.Format("SQL_{0:0000}-{1:00}.log", DateTime.Now.Year, DateTime.Now.Month));
                while (eQueue.Count > 0)
                {
                    LogFileStream.WriteLine("[" + DateTime.Now.ToString() + "] " + eQueue.Dequeue());
                }
                LogFileStream.Close();
            }
            catch
            {

            }

            d(strMsg); //copy to debug file
        }
    }
    public static class Sound
    {
        public static void Alert(Boolean play=true)
        {
            if (play) {
                SoundPlayer simpleSound = new SoundPlayer(@"Notify.wav");
                simpleSound.Play();
            }
        }
        public static void PAlert(Boolean play = true)
        {
            if (play)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"PAlert.wav");
                simpleSound.Play();
            }
        }
        public static void RmtAlert(Boolean play = true)
        {
            if (play)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"RmtAlert.wav");
                simpleSound.Play();
            }
        }
        public static void Shutter(Boolean play=true)
        {
            if (play) { 
                SoundPlayer simpleSound = new SoundPlayer(@"shutter.wav");
                simpleSound.Play();
            }
        }
        public static void Error(Boolean play = true)
        {
            if (play)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"Windows Error.wav");
                simpleSound.Play();
            }
        }
        public static void Ding(Boolean play = true)
        {
            if (play)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"Windows Ding.wav");
                simpleSound.Play();
            }
        }
        public static void MessageNudge(Boolean play = true)
        {
            if (play)
            {
                SoundPlayer simpleSound = new SoundPlayer(@"Windows Message Nudge.wav");
                simpleSound.Play();
            }
        }
    }
    public class FileRoot
    {
        public enum Type { Monthly, Daily, Hourly};
        public String pathname;
        static String message;

        public FileRoot(String pathname)
        {
            this.pathname = pathname;
        }

        public void setRoot(String pathname)
        {
            this.pathname = pathname;
        }
    public String CreateSubPath(String subPath, Type type) //=FileRoot.Type.Normal)
        {
            String FilePath="";
            // Create the file directory
            switch (type)
            {
                case Type.Monthly:
                                    FilePath = String.Format("{0}\\{1}\\", pathname, subPath);
                                    break;
                case Type.Daily:
                                    FilePath = String.Format("{0}\\{1}\\{2}\\", pathname, subPath, DateTime.Now.ToString("yyyy-MM-dd"));
                                    break;
                case Type.Hourly:
                                    FilePath = String.Format("{0}\\{1}\\{2}\\{3:D2}\\", pathname, subPath, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.Hour);
                                    break;

            }
            try
            {
                if (!System.IO.Directory.Exists(pathname))
                {
                    System.IO.Directory.CreateDirectory(pathname);
                }
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
            }
            catch (Exception e)
            {
               message = String.Format("Cannot Create directory : {0}", e.ToString());
                return "";
            }
            return FilePath;
        } //CreateSubPath()

        public StreamWriter AppendFileStream(String FileName)
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    return File.CreateText(FileName);
                }
                else
                {
                    return File.AppendText(FileName);
                }
            }
            catch (Exception e)
            {
                Log.e("FileRoot.AppendFileStream(): " + e.Message);
                return null;
            }
        }
        public StreamWriter CreateFileStream(String FileName)
        {
            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
                return File.CreateText(FileName);
            }
            catch (Exception e)
            {
                Log.e("FileRoot.CreateFileStream(): " + e.Message);
                return null;
            }
        }

    }
    #endregion

    #region Generic Data Class
    public class FontImage
    {
        public String text;
        public Bitmap bmp;
        public FontImage( String text, String fileName)
        {
            this.text = text;
            bmp = new Bitmap(fileName);
        }
    }

    public class FontImageList
    {
        public String name;
        public List<FontImage> list;
        public FontImageList(String name)
        {
            list = new List<FontImage>();
            this.name = name;
        }
    }
    public class Histrogram
    {
        int _MaxGrayLevel = 255;
        public int MaxGrayLevel
        {
            get
            {
                return _MaxGrayLevel;
            }
        }
        public int[] pdf;

        public Histrogram(int maxLevel = 255)
        {
            _MaxGrayLevel = maxLevel;
            pdf = new int[MaxGrayLevel + 1];
            Clear();
        }
        public void Clear()
        {
            for (int i = 0; i <= MaxGrayLevel; i++)
                pdf[i] = 0;
        }
        public void Calc(Image img)
        {
            if (img == null) return;

            Image<Gray, Byte> sourceGrayImage;
            try
            {
                Bitmap sourceBmp = new Bitmap(img);
                sourceGrayImage = new Image<Gray, Byte>(sourceBmp);
            }
            catch
            {
                return;
            }
            Clear();
            for (int y = 0; y < sourceGrayImage.Height; y++)
            {
                for (int x = 0; x < sourceGrayImage.Width; x++)
                {
                    pdf[(int)sourceGrayImage[y, x].Intensity]++;
                }
            }
        }
    }
    #endregion

    #region RMT Data Access & processing
    public class SiteENZ
    {
        public const int OCR_Site_Black_Ratio = 180;
        public const float OCR_Site_Red_Ratio = 1.5f;
        public const float OCR_Site_Blue_Ratio = 1.5f;
        public static Size sizeAOI = new Size(86, 20); //max H = 24

        public Point E, N, Z, Title;
        public int diffE, diffN, diffZ;
        public float scaleE, scaleN, scaleZ;
        public int minE, minN, minZ;
        public int maxE, maxN, maxZ;
        public String id, location, city;
        public OCR_AOI OCR_E_K, OCR_N_K, OCR_Z_K, OCR_E_R, OCR_N_R, OCR_Z_R, OCR_E_B, OCR_N_B, OCR_Z_B;
        public OCR_AOI OCR_E_Nan, OCR_N_Nan, OCR_Z_Nan;
        RmtCore core;
        public SiteENZ(RmtCore core, Point Title, Point E, Point N, Point Z, String id, String location, String city)
        {
            this.core = core;
            this.Title = Title;
            this.E = E;
            this.N = N;
            this.Z = Z;
            this.id = id;
            this.location = location;
            this.city = city;
            OCR_E_K = new OCR_AOI(core, core.Font_SiteK);
            OCR_N_K = new OCR_AOI(core, core.Font_SiteK);
            OCR_Z_K = new OCR_AOI(core, core.Font_SiteK);
            OCR_E_R = new OCR_AOI(core, core.Font_SiteR);
            OCR_N_R = new OCR_AOI(core, core.Font_SiteR);
            OCR_Z_R = new OCR_AOI(core, core.Font_SiteR);
            OCR_E_B = new OCR_AOI(core, core.Font_SiteB);
            OCR_N_B = new OCR_AOI(core, core.Font_SiteB);
            OCR_Z_B = new OCR_AOI(core, core.Font_SiteB);
            OCR_E_Nan = new OCR_AOI(core, core.Font_Nan);
            OCR_N_Nan = new OCR_AOI(core, core.Font_Nan);
            OCR_Z_Nan = new OCR_AOI(core, core.Font_Nan);
        }
        public void parseSite(Bitmap sourceBMP)
        {
            Size sizeDataAOI = new Size(50, 10);
            //Signal E
            {
                int cx = E.X + SiteENZ.sizeAOI.Width + 1;
                int cy = E.Y;
                Rectangle rectK = new Rectangle(new Point(cx, cy - 11), sizeDataAOI);
                Rectangle rectR = new Rectangle(new Point(cx, cy - 1), sizeDataAOI);
                Rectangle rectB = new Rectangle(new Point(cx + 23, cy - 1), sizeDataAOI);

                OCR_E_K.parseSiteSignal(sourceBMP, rectK, OCR_AOI.createColorTemplate, OCR_AOI.isBlackDot, OCR_Site_Black_Ratio, id + "_EK");
                OCR_E_R.parseSiteSignal(sourceBMP, rectR, OCR_AOI.createRedTemplate, OCR_AOI.isRedDot, OCR_Site_Red_Ratio, id + "_ER");
                OCR_E_B.parseSiteSignal(sourceBMP, rectB, OCR_AOI.createBlueTemplate, OCR_AOI.isBlueDot, OCR_Site_Blue_Ratio, id + "_EB");
            }
            // Signal N
            {
                int cx = N.X + +SiteENZ.sizeAOI.Width + 1;
                int cy = N.Y;

                Rectangle rectK = new Rectangle(new Point(cx, cy - 11), sizeDataAOI);
                Rectangle rectR = new Rectangle(new Point(cx, cy - 1), sizeDataAOI);
                Rectangle rectB = new Rectangle(new Point(cx + 23, cy - 1), sizeDataAOI);

                OCR_N_K.parseSiteSignal(sourceBMP, rectK, OCR_AOI.createColorTemplate, OCR_AOI.isBlackDot, OCR_Site_Black_Ratio, id + "_NK");
                OCR_N_R.parseSiteSignal(sourceBMP, rectR, OCR_AOI.createRedTemplate, OCR_AOI.isRedDot, OCR_Site_Red_Ratio, id + "_NR");
                OCR_N_B.parseSiteSignal(sourceBMP, rectB, OCR_AOI.createBlueTemplate, OCR_AOI.isBlueDot, OCR_Site_Blue_Ratio, id + "_NB");
            }

            // Signal Z 
            {
                int cx = Z.X + +SiteENZ.sizeAOI.Width + 1;
                int cy = Z.Y;

                Rectangle rectK = new Rectangle(new Point(cx, cy - 11), sizeDataAOI);
                Rectangle rectR = new Rectangle(new Point(cx, cy - 1), sizeDataAOI);
                Rectangle rectB = new Rectangle(new Point(cx + 23, cy - 1), sizeDataAOI);

                OCR_Z_K.parseSiteSignal(sourceBMP, rectK, OCR_AOI.createColorTemplate, OCR_AOI.isBlackDot, OCR_Site_Black_Ratio, id + "_ZK");
                OCR_Z_R.parseSiteSignal(sourceBMP, rectR, OCR_AOI.createRedTemplate, OCR_AOI.isRedDot, OCR_Site_Red_Ratio, id + "_ZR");
                OCR_Z_B.parseSiteSignal(sourceBMP, rectB, OCR_AOI.createBlueTemplate, OCR_AOI.isBlueDot, OCR_Site_Blue_Ratio, id + "_ZB");
            }
        }
    }
    public class CharBox
    {
        public Rectangle rect;
        public UInt32 MSE;
        private UInt32 _validError;
        public String text;
        public CharBox(Rectangle rect, UInt32 validError)
        {
            this.rect = rect;
            _validError = validError;
            MSE = 0;
            text = "?"; //default char for debug
        }
        public bool validOcrText()
        {
            return (MSE <= validError) && (!text.Contains("?"));
        }
        public UInt32 validError
        {
            get
            {
                return _validError;
            }
        }
    }
    public class OCR_AOI
    {
        public const int OCR_Black_Ratio = 128;
        public const float OCR_Red_Ratio = 1.5f;
        public const float OCR_Blue_Ratio = 1.5f;

        public List<CharBox> charBox;
        public FontImageList fontImage;
        public String text;
        public UInt32 totalMSE = 0;
        UInt32 validError=0;
        RmtCore core;
        public OCR_AOI(RmtCore core, FontImageList fontImage)
        {
            charBox = new List<CharBox>();
            this.fontImage = fontImage;
            this.core = core;
        }

        public bool validOcrText()
        {
            return (totalMSE <= validError) && (!text.Contains("?"));
        }

         public static bool isBlackDot(Color color, float threshold = 128)
        {
            if ((color.R < threshold) && (color.G < threshold) && (color.B < threshold))
            {
                return true;
            }
            return false;
        }
        public static bool isRedDot(Color color, float colorRate = 1.5f)
        {
            if ((color.G == 0) && (color.B == 0) && (color.R > 50))
            {
                return true;
            }
            if ((color.G == 0) && (color.B != 0) && ((float)color.R / (float)color.B > colorRate))
            {
                return true;
            }
            if ((color.G != 0) && (color.B == 0) && ((float)color.R / (float)color.G > colorRate))
            {
                return true;
            }
            if ((color.G != 0) && (color.B != 0) && ((float)color.R / (float)color.G > colorRate) && ((float)color.R / (float)color.B > colorRate))
            {
                return true;
            }
            return false;
        }
        public static bool isBlueDot(Color color, float colorRate = 1.5f)
        {
            if ((color.R == 0) && (color.G == 0) && (color.B > 50))
            {
                return true;
            }
            if ((color.R == 0) && (color.G != 0) && ((float)color.B / (float)color.G > colorRate))
            {
                return true;
            }
            if ((color.R != 0) && (color.G == 0) && ((float)color.B / (float)color.R > colorRate))
            {
                return true;
            }
            if ((color.R != 0) && (color.G != 0) && ((float)color.B / (float)color.R > colorRate) && ((float)color.B / (float)color.G > colorRate))
            {
                return true;
            }
            return false;
        }
        public static Bitmap createColorTemplate(Bitmap srcTemplateImage)
        {
            Bitmap bmp = new Bitmap(srcTemplateImage.Width, srcTemplateImage.Height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, srcTemplateImage.GetPixel(x, y));
                }
            }
            return bmp;
        }
        public static Bitmap createRedTemplate(Bitmap srcTemplateImage)
        {
            Bitmap bmp = new Bitmap(srcTemplateImage.Width, srcTemplateImage.Height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (isRedDot(srcTemplateImage.GetPixel(x, y)))
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(255, 0, 0));
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            return bmp;
        }
        public static Bitmap createBlueTemplate(Bitmap srcTemplateImage)
        {
            Bitmap bmp = new Bitmap(srcTemplateImage.Width, srcTemplateImage.Height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (isBlueDot(srcTemplateImage.GetPixel(x, y)))
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(0, 0, 255));
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            return bmp;
        }
        public static bool isNullTarget(Bitmap target, Func<Color, float, bool> isDot, float ratio)
        {
            for (int x = 0; x < target.Width; x++)
            {
                for (int y = 0; y < target.Height; y++)
                {
                    if (isDot(target.GetPixel(x, y), ratio))
                    {
                        return false; // a dot is found
                    }
                }
            }
            return true;
        }
        private UInt32 MSE(Bitmap target, Bitmap font)
        {

            if ((target.Width != font.Width) || (target.Height != font.Height))
                return UInt32.MaxValue;

            UInt32 MSE = 0;
            for (int x = 0; x < target.Width; x++)
            {
                for (int y = 0; y < target.Height; y++)
                {
                    Int32 dR = target.GetPixel(x, y).R - font.GetPixel(x, y).R;
                    Int32 dG = target.GetPixel(x, y).G - font.GetPixel(x, y).G;
                    Int32 dB = target.GetPixel(x, y).B - font.GetPixel(x, y).B;
                    MSE += (UInt32)(dR * dR + dG * dG + dB * dB);
                }
            }
            return MSE;
        }
        void locate_OCR_Mr(Bitmap sourceBMP)
        {
            const int errThreshold = 393300;

            const int start_x = 667, start_y = 58, stop_x = 720, stop_y = 180;
            const int x_size = stop_x - start_x, y_size = stop_y - start_y;

            int y1=-1, y2=-1; //box rect
            
            // find 1st y line
            for (int y = 0; y < y_size; y++)
            {
                for (int x = 0; x < x_size; x++)
                {                   
                    if (isRedDot(sourceBMP.GetPixel(start_x + x, start_y + y))) 
                    {
                        // found a red pixel
                        y1 = y; break;
                    }
                }
                if (y1 >= 0) break;
            }

            // find last y line
            for (int y = y1+1; y < y_size; y++)
            {
                bool inCharLine = false;
                for (int x = 0; x < x_size; x++)
                {
                    if (isRedDot(sourceBMP.GetPixel(start_x + x, start_y + y)))
                    {
                        inCharLine = true;  //We are in the character font line
                        break;
                    }
                }
                if (!inCharLine) //we have exit character font line
                {
                    y2 = y;
                    break; 
                }
            }

            int maxCharHeight = y2 - y1;
            if (maxCharHeight > 12) maxCharHeight = 12; //height limit

            // search for char position
            int[] x_hist = new int[x_size];
            for (int x = 0; x < x_size; x++)
            {
                x_hist[x] = 0;
                for (int y = 0; y < maxCharHeight; y++)
                {
                    if (isRedDot(sourceBMP.GetPixel(start_x + x, start_y + y1 + y)))
                    {
                        x_hist[x]++;
                    }
                }
            }

            int x1 = 0;
            bool inBox = false;
            core.OCR_Mr.clearBox();
            for (int x = 0; x < x_size; x++)
            {
                if (!inBox)
                {
                    if (x_hist[x] >= 3) //enter char box, min x_hist is 2 for '1'
                    {
                        inBox = true;
                        x1 = x; //remember the start x position
                    }
                }
                else
                {
                    if (x_hist[x] < 3) //exit char box
                    {
                        inBox = false;

                        if (x - x1 >= 2) // valid character width
                        {
                            int[] y_hist = new int[maxCharHeight];

                            // find  box
                            for (int y = 0; y < maxCharHeight; y++)
                            {
                                y_hist[y] = 0;
                                for (int x2 = x1; x2 < x; x2++)
                                {
                                    if (isRedDot(sourceBMP.GetPixel(start_x + x2, start_y + y1 + y))) 
                                    {
                                        y_hist[y]++;
                                    }
                                }
                            }

                            int chy1, chy2;

                            for (chy1 = 0; chy1 < maxCharHeight; chy1++)
                            {
                                if (y_hist[chy1] > 0) break;
                            }
                            for (chy2 = chy1; chy2 < maxCharHeight; chy2++)
                            {
                                if (y_hist[chy2] == 0) break;
                            }

                            int dy = chy2 - chy1 + 1, sx = start_x + x1 - 1, sy = start_y + y1 + chy1 - 1;
                            core.OCR_Mr.addBox(new Rectangle(new Point(sx, sy), new Size(x - x1 + 1, dy)), errThreshold);
                        }
                    }
                }
            }
        }
        public String parseMr(Bitmap sourceBMP)
        {
            locate_OCR_Mr(sourceBMP);

            totalMSE = 0;
            validError = 0;
            text = "";
            for (int i = 0; i < charBox.Count(); i++) //search for each char box
            {
                Bitmap target = (Bitmap)OCR_AOI.createRedTemplate(sourceBMP.Clone(charBox[i].rect, sourceBMP.PixelFormat));

                if (isNullTarget(target, isRedDot, 1.5f))
                {
                    continue; //next charBox
                }
                UInt32 minMSE = UInt32.MaxValue;
                int minId = -1;
                for (int id = 0; id < fontImage.list.Count(); id++)
                {                    
                    UInt32 mse = MSE(target, fontImage.list[id].bmp);
                    if (mse < minMSE)
                    {
                        minMSE = mse;
                        minId = id;
                    }
                    if (mse == 0) break; //exactly match
                }
                if (minId >= 0) //find target
                {
                    charBox[i].MSE = minMSE;
                    if (charBox[i].MSE > charBox[i].validError)
                    {
                        core.errCode |= RmtCore.ErrorCode.OCR_CharError;
                    }
                    totalMSE += charBox[i].MSE;
                    validError += charBox[i].validError;
                    charBox[i].text = fontImage.list[minId].text;
                    text += charBox[i].text;
                }
                else
                {
                    charBox[i].text = "?";
                    text += charBox[i].text;
                    core.errCode |= RmtCore.ErrorCode.OCR_DecodeFail;
                }
            }
            if (totalMSE>validError)
            {
                core.errCode |= RmtCore.ErrorCode.OCR_DecodeError;
            }
            return text;
        }

        void locate_OCR_SiteSignal(Bitmap sourceBMP, Rectangle rect, Func<Color, float, bool> isDot, float ratio, String siteID)
        {
            const int errThreshold = 393300;

            // find 1st y line
            int y1 = -1; // y2=rect.Height;
            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if (isDot(sourceBMP.GetPixel(rect.X + x, rect.Y + y),ratio))
                    {
                        // found a valid pixel
                        y1 = y; break;
                    }
                }
                if (y1 >= 0) break;
            }

            // find last y line
            for (int y = y1 + 1; y < rect.Height; y++)
            {
                bool inCharLine = false;
                for (int x = 0; x < rect.Width; x++)
                {
                    if (isDot(sourceBMP.GetPixel(rect.X + x, rect.Y + y),ratio))
                    {
                        inCharLine = true;  //We are in the character font line
                        break;
                    }
                }
                if (!inCharLine) //we have exit character font line
                {
                    //y2 = y;
                    break;
                }
            }

            int charHeight = 8; //fixed

            // search for char position
            int[] x_hist = new int[rect.Width];
            int x1 =0, x2=0;
            for (int x = 0; x < rect.Width; x++)
            {
                x_hist[x] = 0;
                for (int y = 0; y < charHeight; y++)
                {
                    if (isDot(sourceBMP.GetPixel(rect.X + x, rect.Y + y1 + y),ratio))
                    {
                        x_hist[x]++;
                    }
                }

                if (x_hist[x]>0)
                {
                    if (x1 == 0) x1 = x; //1st position
                    x2 = x; //last position
                }
            }

            clearBox();

            bool inBox = false;
            int char_x1 = 0;
            for (int x = x1; x <= x2 + 1; x++) //+1 to exit char box
            {
                if (!inBox)
                {
                    if (x_hist[x] > 0) //enter char box, min x_hist is 2 for '1'
                    {
                        inBox = true;
                        char_x1 = x; //remember the start x position
                    }
                }
                else
                {
                    if (x_hist[x] == 0) //exit char box
                    {
                        inBox = false;

                        if (x - char_x1 > 5) // width too long
                        {
                            if (x_hist[char_x1] == 1) //leading dot?
                            {
                                bool isLeadingDot = true;
                                for (int y = 0; y < charHeight - 2; y++)
                                {
                                    if (isDot(sourceBMP.GetPixel(rect.X + char_x1, rect.Y + y1 + y), ratio))
                                    {
                                        isLeadingDot = false;
                                        break;
                                    }
                                }
                                if (isLeadingDot)
                                {
                                    addBox(new Rectangle(new Point(rect.X + char_x1, rect.Y + y1 - 1), new Size(1, charHeight)), errThreshold);
                                    addBox(new Rectangle(new Point(rect.X + char_x1 + 1, rect.Y + y1 - 1), new Size(x - char_x1 - 1, charHeight)), errThreshold);
                                } else
                                {
                                    addBox(new Rectangle(new Point(rect.X + char_x1, rect.Y + y1 - 1), new Size(x - char_x1, charHeight)), errThreshold);
                                }
                            }
                            else //all number
                            {
                                addBox(new Rectangle(new Point(rect.X + char_x1, rect.Y + y1 - 1), new Size(x - char_x1, charHeight)), errThreshold);
                            }
                        }
                        else if (x - char_x1 > 0) // valid character width
                        {
                            addBox(new Rectangle(new Point(rect.X + char_x1, rect.Y + y1 - 1), new Size(x - char_x1, charHeight)), errThreshold);
                        }
                    }
                }
            }
            if (charBox.Count == 0)
                Log.e(String.Format("{0} chrBox.Count=0", siteID));
        }

        public String parseSiteSignal(Bitmap sourceBMP, Rectangle rect, Func<Bitmap, Bitmap> createTemplate, Func<Color, float, bool> isDot, float ratio, String siteID)
        {
            locate_OCR_SiteSignal(sourceBMP, rect, isDot, ratio, siteID);

            totalMSE = 0;
            validError = 0;
            text = "";
            for (int i = 0; i < charBox.Count(); i++) //search for each char box
            {
                Bitmap target = createTemplate(sourceBMP.Clone(charBox[i].rect, sourceBMP.PixelFormat));

                if (isNullTarget(target, isDot, ratio))
                {
                    continue; //next charBox
                }
                UInt32 minMSE = UInt32.MaxValue;
                int minId = -1;
                for (int id = 0; id < fontImage.list.Count(); id++)
                {
                    UInt32 mse = MSE(target,fontImage.list[id].bmp);
                    if (mse < minMSE)
                    {
                        minMSE = mse;
                        minId = id;
                    }
                    if (mse == 0) break; //exactly match
                }
                if (minId >= 0) //find target
                {
                    charBox[i].MSE = minMSE;
                    if (charBox[i].MSE > charBox[i].validError)
                    {
                        core.errCode |= RmtCore.ErrorCode.OCR_CharError;
                    }
                    totalMSE += charBox[i].MSE;
                    validError += charBox[i].validError;
                    charBox[i].text = fontImage.list[minId].text;
                    text += charBox[i].text;
                }
                else
                {
                    charBox[i].text = "?";
                    text += charBox[i].text;
                    core.errCode |= RmtCore.ErrorCode.OCR_DecodeFail;
                }
            }
            if (totalMSE > validError)
            {
                core.errCode |= RmtCore.ErrorCode.OCR_DecodeError;
            }
            return text;
        }

        public String parse(Bitmap sourceBMP)
        {
            totalMSE = 0;
            text = "";
            for (int i=0; i<charBox.Count(); i++) //search for each char box
            {
                Bitmap target = OCR_AOI.createColorTemplate(sourceBMP.Clone(charBox[i].rect, sourceBMP.PixelFormat));

                if (isNullTarget(target, isBlackDot, 128)) {
                    continue; //next charBox
                }
                UInt32 minMSE = UInt32.MaxValue;
                int minId = -1;
                for (int id=0; id<fontImage.list.Count(); id++)
                {
                    UInt32 mse = MSE(target,fontImage.list[id].bmp);
                    if (mse<minMSE)
                    {
                        minMSE = mse;
                        minId = id;
                    }
                    if (mse == 0) break; //exactly match
                }
                if (minId>=0) //find target
                {
                    charBox[i].MSE = minMSE;
                    if (charBox[i].MSE > charBox[i].validError)
                    {
                        core.errCode |= RmtCore.ErrorCode.OCR_CharError;
                    }
                    totalMSE += charBox[i].MSE;
                    validError += charBox[i].validError;
                    charBox[i].text = fontImage.list[minId].text;
                    text += charBox[i].text;

                } else
                {
                    charBox[i].text = "?";
                    text += charBox[i].text;
                    core.errCode |= RmtCore.ErrorCode.OCR_DecodeFail;
                }
            }
            if (totalMSE > validError)
            {
                core.errCode |= RmtCore.ErrorCode.OCR_DecodeError;
            }
            return text;
        }
        public void addBox(Rectangle rect, UInt32 validError)
        {
            charBox.Add(new CharBox(rect, validError));
        }
        public void clearBox()
        {
            charBox.Clear();
        }
    }
    public class RmtCore
    {
        // common constants
        public static Size sizeRmtImage = new Size(902, 541);

        //runtime parameters
        public int SignalDiffValue = 2;
        public bool isSaveTemplate = true;
        public bool isShowOCRAOI = true;
        public bool isParseSiteDataOCR = false;
        public FileRoot fileRoot;
        public enum ErrorCode : UInt32
        {
            OCR_DecodeFail = 0x01,
            OCR_CharError = 0x02,
            OCR_DecodeError = 0x4
        }
        public ErrorCode errCode;

        // Processing Time
        public DateTime lastProcessingTimer;
        private DateTime _startProcessingTime, _endProcessingTime;
        private DateTime _startMatchTime, _endMatchTime;
        private DateTime _startCalcTime, _endCalcTime;
        private DateTime _startDownloadTime, _endDownloadTime;

        // Results
        private int _signalWarningCount;
        private int _NanCount;
        private static List<Bitmap>[] undefinedFont; //unrecognized pattern, store to file to added

        // Public data
        public String[] templatePath = new String[] {
                                    "template\\Date\\", "template\\Time\\", "template\\Long\\", "template\\Lati\\",
                                    "template\\Depth\\", "template\\Mw\\", "template\\Mr\\", "template\\SiteK\\",
                                    "template\\SiteR\\", "template\\SiteB\\" };


        public static String defaultRootPath = Environment.GetEnvironmentVariable("HOMEDRIVE") + Environment.GetEnvironmentVariable("HOMEPATH") + @"\RMTProcessing";
        public Image sourceImage, outputImage;

        public double downloadTime;
        public double maxDownloadTime=0;
        public double matchTime;
        public double calcTime;
        public double processingTime;
        public List<Rectangle> nanLabelList = new List<Rectangle>();
        public int maxSignalWarningCount;
        public int maxNanCount;
        public OCR_AOI OCR_Date, OCR_Time, OCR_Longitude, OCR_Latitude, OCR_Depth, OCR_Mw, OCR_Mr;
        public FontImageList Font_Date, Font_Time, Font_Longitude, Font_Latitude, Font_Depth, Font_Mw, Font_Mr;
        public FontImageList Font_SiteK, Font_SiteR, Font_SiteB, Font_Nan;

        public double SamplePeriod = -1; //undefined
        public Boolean fail = false;
        public SiteENZ[] siteENZ;
        public String status;
        public RmtCore(String rootpath) //Constructor
        {
            fileRoot = new FileRoot(rootpath);
            initFontLists();
            initundefinedFont();
            initOCR();
            initSiteENZ();
        }

        #region init & clear
        void initundefinedFont()
        {
            // geterate template image List
            undefinedFont = new List<Bitmap>[templatePath.Length];
            for (int i = 0; i < templatePath.Length; i++)
            {
                undefinedFont[i] = new List<Bitmap>();
            }
        }
        void initOCR()
        {
            const int errThreshold = 390300;

            OCR_Date = new OCR_AOI(this, Font_Date);
            OCR_Time = new OCR_AOI(this, Font_Time);
            OCR_Longitude = new OCR_AOI(this, Font_Longitude);
            OCR_Latitude = new OCR_AOI(this, Font_Latitude);
            OCR_Depth = new OCR_AOI(this, Font_Depth);
            OCR_Mw = new OCR_AOI(this, Font_Mw);
            OCR_Mr = new OCR_AOI(this, Font_Mr);

            for (int i = 0; i < 4; i++) //Year 
            {
                OCR_Date.addBox(new Rectangle(new Point(729 + i * 9, 19), new Size(9, 13)), errThreshold);
            }
            for (int i = 0; i < 2; i++) //Month
            {
                OCR_Date.addBox(new Rectangle(new Point(729 + 4 * 9 + 6 + i * 9, 19), new Size(9, 13)), errThreshold);
            }
            for (int i = 0; i < 2; i++) //Day
            {
                OCR_Date.addBox(new Rectangle(new Point(729 + 6 * 9 + 12 + i * 9, 19), new Size(9, 13)), errThreshold);
            }
            for (int i = 0; i < 2; i++) //Hour
            {
                OCR_Time.addBox(new Rectangle(new Point(716 + i * 9, 38), new Size(9, 13)), 0);
            }
            for (int i = 0; i < 2; i++) //Minute
            {
                OCR_Time.addBox(new Rectangle(new Point(716 + 2 * 9 + 7 + i * 9, 38), new Size(9, 13)), errThreshold);
            }
            for (int i = 0; i < 2; i++) //Second
            {
                OCR_Time.addBox(new Rectangle(new Point(716 + 4 * 9 + 14 + i * 9, 38), new Size(9, 13)), errThreshold);
            }
            for (int i = 0; i < 3; i++) //Longitude integer part
            {
                OCR_Longitude.addBox(new Rectangle(new Point(539 + i * 6, 41), new Size(6, 8)), 0);
            }
            for (int i = 0; i < 2; i++) //Longitude fractional part
            {
                OCR_Longitude.addBox(new Rectangle(new Point(539 + 3 * 6 + 4 + i * 6, 41), new Size(6, 8)), errThreshold);
            }
            for (int i = 0; i < 2; i++) //Latitude integer part
            {
                OCR_Latitude.addBox(new Rectangle(new Point(581 + i * 6, 41), new Size(6, 8)), errThreshold);
            }
            for (int i = 0; i < 2; i++) //Latitude fractional part
            {
                OCR_Latitude.addBox(new Rectangle(new Point(581 + 2 * 6 + 4 + i * 6, 41), new Size(6, 8)), errThreshold);
            }
            for (int i = 0; i < 3; i++) //Depth
            {
                OCR_Depth.addBox(new Rectangle(new Point(541 + i * 6, 58), new Size(6, 10)), errThreshold);
            }
            //Mw
            {
                //OCR_Mw.addBox(new Rectangle(new Point(531, 72), new Size(9, 13)));
                OCR_Mw.addBox(new Rectangle(new Point(540, 72), new Size(9, 13)), errThreshold); // Mw integer part
                OCR_Mw.addBox(new Rectangle(new Point(554, 72), new Size(9, 13)), errThreshold); // Mw fractional part
            }
            // Mr
            {
                //OCR_Mr is dynamically calculated before OCR parser
            }
            // SiteData
            {
                //OCR_SiteData is dynamically calculated before OCR parser
            }
        }
        void initSiteENZ()
        {
            siteENZ = new SiteENZ[18];
            siteENZ[0] = new SiteENZ(this, new Point(74, 25), new Point(39, 54), new Point(39, 78), new Point(39, 102), "VWUC", "烏坵", "金門");
            siteENZ[1] = new SiteENZ(this, new Point(72, 119), new Point(39, 148), new Point(39, 172), new Point(39, 196), "SBCB", "十八尖山", "新竹");
            siteENZ[2] = new SiteENZ(this, new Point(72, 214), new Point(39, 244), new Point(39, 268), new Point(39, 292), "RLNB", "二林", "彰化");
            siteENZ[3] = new SiteENZ(this, new Point(72, 308), new Point(39, 338), new Point(39, 362), new Point(39, 386), "TPUB", "大埔", "嘉義");
            siteENZ[4] = new SiteENZ(this, new Point(74, 403), new Point(39, 432), new Point(39, 456), new Point(39, 480), "PHUB", "澎湖", "澎湖");

            siteENZ[5] = new SiteENZ(this, new Point(222, 25), new Point(192, 54), new Point(192, 78), new Point(192, 102), "YD07", "大坪", "新北");
            siteENZ[6] = new SiteENZ(this, new Point(225, 119),new Point(192, 148), new Point(192, 172), new Point(192, 196), "YHNB", "耶亨", "桃園");
            siteENZ[7] = new SiteENZ(this, new Point(225, 214), new Point(192, 244), new Point(192, 268), new Point(192, 292), "TDCB", "達見", "台中");
            siteENZ[8] = new SiteENZ(this, new Point(225, 308), new Point(192, 338), new Point(192, 362), new Point(192, 386), "SSLB", "雙龍", "南投");
            siteENZ[9] = new SiteENZ(this, new Point(230, 403), new Point(192, 432), new Point(192, 456), new Point(192, 480), "MASB", "馬仕", "屏東");

            siteENZ[10] = new SiteENZ(this, new Point(375, 25), new Point(346, 54), new Point(346, 78), new Point(346, 102), "SXI1", "雙溪", "新北");
            siteENZ[11] = new SiteENZ(this, new Point(383, 119), new Point(346, 148), new Point(346, 172), new Point(346, 196), "NACB", "寧安橋", "花蓮");
            siteENZ[12] = new SiteENZ(this, new Point(381, 214), new Point(346, 244), new Point(346, 268), new Point(346, 292), "YULB", "玉里", "花蓮");
            siteENZ[13] = new SiteENZ(this, new Point(384, 308), new  Point(346, 338), new Point(346, 362), new Point(346, 386), "TWGB", "台東", "台東");
            siteENZ[14] = new SiteENZ(this, new Point(383, 403), new Point(346, 432), new Point(346, 456), new Point(346, 480), "TWKB", "墾丁", "屏東");

            siteENZ[15] = new SiteENZ(this, new Point(535, 214), new Point(500, 244), new Point(500, 268), new Point(500, 292), "PCYB", "彭佳嶼", "基隆");
            siteENZ[16] = new SiteENZ(this, new Point(535, 308), new Point(500, 338), new Point(500, 362), new Point(500, 386), "YNGF", "與那國島", "沖繩");
            siteENZ[17] = new SiteENZ(this, new Point(535, 403), new Point(500, 432), new Point(500, 456), new Point(500, 480), "LYUB", "蘭嶼", "台東");
        }
        void initFontLists()
        {
            Font_Date = new FontImageList("Date");
            Font_Time = new FontImageList("Time");
            Font_Longitude = new FontImageList("Longitude");
            Font_Latitude = new FontImageList("Latitude");
            Font_Depth = new FontImageList("Depth");
            Font_Mw = new FontImageList("Mw");
            Font_Mr = new FontImageList("Mr");
            Font_SiteK = new FontImageList("SiteK");
            Font_SiteR = new FontImageList("SiteR");
            Font_SiteB = new FontImageList("SiteB");
            Font_Nan = new FontImageList("Nan");

            addFont(Font_Date, "0", @"template\Date-0-t{0:00}.bmp");
            addFont(Font_Date, "1", @"template\Date-1-t{0:00}.bmp");
            addFont(Font_Date, "2", @"template\Date-2-t{0:00}.bmp");
            addFont(Font_Date, "3", @"template\Date-3-t{0:00}.bmp");
            addFont(Font_Date, "4", @"template\Date-4-t{0:00}.bmp");
            addFont(Font_Date, "5", @"template\Date-5-t{0:00}.bmp");
            addFont(Font_Date, "6", @"template\Date-6-t{0:00}.bmp");
            addFont(Font_Date, "7", @"template\Date-7-t{0:00}.bmp");
            addFont(Font_Date, "8", @"template\Date-8-t{0:00}.bmp");
            addFont(Font_Date, "9", @"template\Date-9-t{0:00}.bmp");

            addFont(Font_Time, "0", @"template\Time-0-t{0:00}.bmp");
            addFont(Font_Time, "1", @"template\Time-1-t{0:00}.bmp");
            addFont(Font_Time, "2", @"template\Time-2-t{0:00}.bmp");
            addFont(Font_Time, "3", @"template\Time-3-t{0:00}.bmp");
            addFont(Font_Time, "4", @"template\Time-4-t{0:00}.bmp");
            addFont(Font_Time, "5", @"template\Time-5-t{0:00}.bmp");
            addFont(Font_Time, "6", @"template\Time-6-t{0:00}.bmp");
            addFont(Font_Time, "7", @"template\Time-7-t{0:00}.bmp");
            addFont(Font_Time, "8", @"template\Time-8-t{0:00}.bmp");
            addFont(Font_Time, "9", @"template\Time-9-t{0:00}.bmp");

            addFont(Font_Longitude, "0", @"template\Long-0-t{0:00}.bmp");
            addFont(Font_Longitude, "1", @"template\Long-1-t{0:00}.bmp");
            addFont(Font_Longitude, "2", @"template\Long-2-t{0:00}.bmp");
            addFont(Font_Longitude, "3", @"template\Long-3-t{0:00}.bmp");
            addFont(Font_Longitude, "4", @"template\Long-4-t{0:00}.bmp");
            addFont(Font_Longitude, "5", @"template\Long-5-t{0:00}.bmp");
            addFont(Font_Longitude, "6", @"template\Long-6-t{0:00}.bmp");
            addFont(Font_Longitude, "7", @"template\Long-7-t{0:00}.bmp");
            addFont(Font_Longitude, "8", @"template\Long-8-t{0:00}.bmp");
            addFont(Font_Longitude, "9", @"template\Long-9-t{0:00}.bmp");

            addFont(Font_Latitude, "0", @"template\Lati-0-t{0:00}.bmp");
            addFont(Font_Latitude, "1", @"template\Lati-1-t{0:00}.bmp");
            addFont(Font_Latitude, "2", @"template\Lati-2-t{0:00}.bmp");
            addFont(Font_Latitude, "3", @"template\Lati-3-t{0:00}.bmp");
            addFont(Font_Latitude, "4", @"template\Lati-4-t{0:00}.bmp");
            addFont(Font_Latitude, "5", @"template\Lati-5-t{0:00}.bmp");
            addFont(Font_Latitude, "6", @"template\Lati-6-t{0:00}.bmp");
            addFont(Font_Latitude, "7", @"template\Lati-7-t{0:00}.bmp");
            addFont(Font_Latitude, "8", @"template\Lati-8-t{0:00}.bmp");
            addFont(Font_Latitude, "9", @"template\Lati-9-t{0:00}.bmp");

            addFont(Font_Depth, "0", @"template\Depth-0-t{0:00}.bmp");
            addFont(Font_Depth, "1", @"template\Depth-1-t{0:00}.bmp");
            addFont(Font_Depth, "2", @"template\Depth-2-t{0:00}.bmp");
            addFont(Font_Depth, "3", @"template\Depth-3-t{0:00}.bmp");
            addFont(Font_Depth, "4", @"template\Depth-4-t{0:00}.bmp");
            addFont(Font_Depth, "5", @"template\Depth-5-t{0:00}.bmp");
            addFont(Font_Depth, "6", @"template\Depth-6-t{0:00}.bmp");
            addFont(Font_Depth, "7", @"template\Depth-7-t{0:00}.bmp");
            addFont(Font_Depth, "8", @"template\Depth-8-t{0:00}.bmp");
            addFont(Font_Depth, "9", @"template\Depth-9-t{0:00}.bmp");

            addFont(Font_Mw, "0", @"template\Mw-0-t{0:00}.bmp");
            addFont(Font_Mw, "1", @"template\Mw-1-t{0:00}.bmp");
            addFont(Font_Mw, "2", @"template\Mw-2-t{0:00}.bmp");
            addFont(Font_Mw, "3", @"template\Mw-3-t{0:00}.bmp");
            addFont(Font_Mw, "4", @"template\Mw-4-t{0:00}.bmp");
            addFont(Font_Mw, "5", @"template\Mw-5-t{0:00}.bmp");
            addFont(Font_Mw, "6", @"template\Mw-6-t{0:00}.bmp");
            addFont(Font_Mw, "7", @"template\Mw-7-t{0:00}.bmp");
            addFont(Font_Mw, "8", @"template\Mw-8-t{0:00}.bmp");
            addFont(Font_Mw, "9", @"template\Mw-9-t{0:00}.bmp");

            addFont(Font_Mr, "0", @"template\Mr-0-t{0:00}.bmp");
            addFont(Font_Mr, "1", @"template\Mr-1-t{0:00}.bmp");
            addFont(Font_Mr, "2", @"template\Mr-2-t{0:00}.bmp");
            addFont(Font_Mr, "3", @"template\Mr-3-t{0:00}.bmp");
            addFont(Font_Mr, "4", @"template\Mr-4-t{0:00}.bmp");
            addFont(Font_Mr, "5", @"template\Mr-5-t{0:00}.bmp");
            addFont(Font_Mr, "6", @"template\Mr-6-t{0:00}.bmp");
            addFont(Font_Mr, "7", @"template\Mr-7-t{0:00}.bmp");
            addFont(Font_Mr, "8", @"template\Mr-8-t{0:00}.bmp");
            addFont(Font_Mr, "9", @"template\Mr-9-t{0:00}.bmp");

            // the following is not OK
            addFont(Font_SiteK, "9", @"template\Date-9-t{0:00}.bmp");
            addFont(Font_SiteR, "9", @"template\Date-9-t{0:00}.bmp");
            addFont(Font_SiteB, "9", @"template\Date-9-t{0:00}.bmp");
            // Nan
            addFont(Font_Nan, "Nan", @"template\Nan-t{0:00}.bmp");
        }
        public void clear()
        {
            errCode = 0;
            maxDownloadTime = 0;
            maxSignalWarningCount = 0;
            maxNanCount = 0;
        }

        void addFont(FontImageList fontImage, String text, String format)
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    fontImage.list.Add(new FontImage(text, String.Format(format, i)));
                }
                catch
                {
                    Log.d(String.Format("Load Font_{0}[{1}] count={2}", fontImage.name, text, i));
                    break;
                }
            }
        }
        #endregion
        
        #region data field
        //processing time
        public DateTime startProcessingTime
        {
            get
            {
                return _startProcessingTime;
            }
            set
            {
                _startProcessingTime = value;
            }
        }
        public DateTime endProcessingTime
        {
            get
            {
                return _endProcessingTime;
            }
            set
            {
                _endProcessingTime = value;
                processingTime = (_endProcessingTime - _startProcessingTime).Ticks / 1E7;
            }
        }

        // Match Time
        public DateTime startMatchTime
        {
            get
            {
                return _startMatchTime;
            }
            set
            {
                _startMatchTime = value;
            }
        }
        public DateTime endMatchTime
        {
            get
            {
                return _endMatchTime;
            }
            set
            {
                _endMatchTime = value;
                matchTime = (_endMatchTime - _startMatchTime).Ticks / 1E7;
            }
        }

        //Calc Time
        public DateTime startCalcTime
        {
            get
            {
                return _startCalcTime;
            }
            set
            {
                _startCalcTime = value;
            }
        }
        public DateTime endCalcTime
        {
            get
            {
                return _endCalcTime;
            }
            set
            {
                _endCalcTime = value;
                calcTime = (_endCalcTime - _startCalcTime).Ticks / 1E7;
            }
        }

        // Download Time
        public DateTime startDownloadTime
        {
            get
            {
                return _startDownloadTime;
            }
            set
            {
                _startDownloadTime = value;
            }
        }
        public DateTime endDownloadTime
        {
            get
            {
                return _endDownloadTime;
            }
            set
            {
                _endDownloadTime = value;
                downloadTime = (_endDownloadTime - _startDownloadTime).Ticks / 1E7;
                if (downloadTime > maxDownloadTime)
                {
                    maxDownloadTime = downloadTime;
                }
            }
        }

        // Site Warning Count
        public int signalWarningCount
        {
            get
            {
                return _signalWarningCount;
            }
            set
            {
                _signalWarningCount = value;
                if (_signalWarningCount > maxSignalWarningCount)
                {
                    maxSignalWarningCount = _signalWarningCount;
                }
            }
        }

        // Nan Count
        public int NanCount
        {
            get
            {
                return _NanCount;
            }
            set
            {
                _NanCount = value;
                if (_NanCount > maxNanCount)
                {
                    maxNanCount = _NanCount;
                }
            }
        }
        #endregion

        #region template suport
        public void createTemplatePath()
        { 
            for (int i = 0; i<templatePath.Length; i++)
            {
                //for (int n = 0; n < 10; n++)
                {
                    String path = String.Format("{0}\\{1}",fileRoot.pathname, templatePath[i]); // + n.ToString(); //add number tail
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
        bool isNullBlackTemplate(Bitmap templateImage, float ratio)
        {
            return OCR_AOI.isNullTarget(templateImage, OCR_AOI.isBlackDot, ratio);
        }
        bool isNullRedTemplate(Bitmap templateImage, float ratio)
        {
            return OCR_AOI.isNullTarget(templateImage, OCR_AOI.isRedDot, ratio);
        }
        bool isNullBlueTemplate(Bitmap templateImage, float ratio)
        {
            return OCR_AOI.isNullTarget(templateImage, OCR_AOI.isBlueDot, ratio);
        }
        bool isNewTemplate(Bitmap templateImage, List<Bitmap> undefinedFont, Func<Bitmap, float, bool> isNullTemplate, float ratio)
        {
            /* template class = 0 for generic B/W dot, 1 for Red dot */
            if (isNullTemplate(templateImage, ratio))
            {
                return false;
            }
            if (undefinedFont.Count() == 0)
            {
                //1st template
                undefinedFont.Add(templateImage);
                return true;
            }
            for (int id = 0; id < undefinedFont.Count(); id++)
            {
                // check size
                if ((undefinedFont[id].Width != templateImage.Width) ||
                    (undefinedFont[id].Height != templateImage.Height))
                    continue;

                bool isDiff = false;
                for (int x = 0; x < undefinedFont[id].Width; x++)
                {
                    for (int y = 0; y < undefinedFont[id].Height; y++)
                    {
                        if ((templateImage.GetPixel(x, y).R != undefinedFont[id].GetPixel(x, y).R) ||
                            (templateImage.GetPixel(x, y).G != undefinedFont[id].GetPixel(x, y).G) ||
                            (templateImage.GetPixel(x, y).B != undefinedFont[id].GetPixel(x, y).B))
                        {
                            isDiff = true;
                            break;
                        }
                    }
                    if (isDiff) break;
                }
                if (isDiff)
                    continue;
                else
                    return false;
            }

            undefinedFont.Add(templateImage);
            return true;
        }
        public void saveUndefinedFont(Boolean saveTemplate = true)
        {
            if (!saveTemplate) return;
            {
                createTemplatePath();
                Bitmap sourceBMP = new Bitmap(sourceImage);

                foreach (CharBox box in OCR_Date.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[0], isNullBlackTemplate, OCR_AOI.OCR_Black_Ratio) || box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Date-t{2}.bmp", fileRoot.pathname, templatePath[0], undefinedFont[0].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }

                foreach (CharBox box in OCR_Time.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[1], isNullBlackTemplate, OCR_AOI.OCR_Black_Ratio) || box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Time-t{2}.bmp", fileRoot.pathname, templatePath[1], undefinedFont[1].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }

                foreach (CharBox box in OCR_Longitude.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[2], isNullBlackTemplate, OCR_AOI.OCR_Black_Ratio) || box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Long-t{2}.bmp", fileRoot.pathname, templatePath[2], undefinedFont[2].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }
                foreach (CharBox box in OCR_Latitude.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[3], isNullBlackTemplate, OCR_AOI.OCR_Black_Ratio) || box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Lati-t{2}.bmp", fileRoot.pathname, templatePath[3], undefinedFont[3].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }
                foreach (CharBox box in OCR_Depth.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[4], isNullBlackTemplate, OCR_AOI.OCR_Black_Ratio) || box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Depth-t{2}.bmp", fileRoot.pathname, templatePath[4], undefinedFont[4].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }

                foreach (CharBox box in OCR_Mw.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[5], isNullBlackTemplate, OCR_AOI.OCR_Black_Ratio) || box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Mw-t{2}.bmp", fileRoot.pathname, templatePath[5], undefinedFont[5].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }

                foreach (CharBox box in OCR_Mr.charBox)
                {
                    if (!box.validOcrText())
                    {
                        Bitmap templateImage = OCR_AOI.createRedTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                        if (isNewTemplate(templateImage, undefinedFont[6], isNullRedTemplate, OCR_AOI.OCR_Red_Ratio))
                            templateImage.Save(String.Format("{0}\\{1}\\Mr-t{2}.bmp", fileRoot.pathname, templatePath[6], undefinedFont[6].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        if (box.text == "?")
                            templateImage.Save(String.Format("{0}\\{1}\\Mr-err{2}.bmp", fileRoot.pathname, templatePath[6], undefinedFont[6].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }

                foreach (SiteENZ site in siteENZ)
                {
                    foreach (CharBox box in site.OCR_E_K.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[7], isNullBlackTemplate, SiteENZ.OCR_Site_Black_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteK-t{2}.bmp", fileRoot.pathname, templatePath[7], undefinedFont[7].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_N_K.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[7], isNullBlackTemplate, SiteENZ.OCR_Site_Black_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteK-t{2}.bmp", fileRoot.pathname, templatePath[7], undefinedFont[7].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_Z_K.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createColorTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[7], isNullBlackTemplate, SiteENZ.OCR_Site_Black_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteK-t{2}.bmp", fileRoot.pathname, templatePath[7], undefinedFont[7].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_E_R.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createRedTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[8], isNullRedTemplate, SiteENZ.OCR_Site_Red_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteR-t{2}.bmp", fileRoot.pathname, templatePath[8], undefinedFont[8].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_N_R.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createRedTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[8], isNullRedTemplate, SiteENZ.OCR_Site_Red_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteR-t{2}.bmp", fileRoot.pathname, templatePath[8], undefinedFont[8].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_Z_R.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createRedTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[8], isNullRedTemplate, SiteENZ.OCR_Site_Red_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteR-t{2}.bmp", fileRoot.pathname, templatePath[8], undefinedFont[8].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_E_B.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createBlueTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[9], isNullBlueTemplate, SiteENZ.OCR_Site_Blue_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteB-t{2}.bmp", fileRoot.pathname, templatePath[9], undefinedFont[9].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_N_B.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createBlueTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[9], isNullBlueTemplate, SiteENZ.OCR_Site_Blue_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteB-t{2}.bmp", fileRoot.pathname, templatePath[9], undefinedFont[9].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    foreach (CharBox box in site.OCR_Z_B.charBox)
                    {
                        if (!box.validOcrText())
                        {
                            Bitmap templateImage = OCR_AOI.createBlueTemplate(sourceBMP.Clone(box.rect, sourceBMP.PixelFormat));
                            if (isNewTemplate(templateImage, undefinedFont[9], isNullBlueTemplate, SiteENZ.OCR_Site_Blue_Ratio) || box.text == "?")
                                templateImage.Save(String.Format("{0}\\{1}\\SiteB-t{2}.bmp", fileRoot.pathname, templatePath[9], undefinedFont[9].Count()), System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                }
            }
        }
        #endregion

        #region processing functions
        public void calcSignal(int diffThreshold)
        {
            Image<Gray, Byte> sourceGrayImage;
            try
            {
                Bitmap sourceBmp = new Bitmap(sourceImage);
                sourceGrayImage = new Image<Gray, Byte>(sourceBmp);

            }
            catch (Exception e)
            {
                Log.e("Invalid source image handle: " + e.Message);
                return;
            }

            startCalcTime = DateTime.Now;

            signalWarningCount = 0;
            for (int site = 0; site < siteENZ.Length; site++)
            {
                calcOneSignal(sourceGrayImage, site, 0, diffThreshold); //E
                calcOneSignal(sourceGrayImage, site, 1, diffThreshold); //N
                calcOneSignal(sourceGrayImage, site, 2, diffThreshold); //Z
            }
            endCalcTime = DateTime.Now;

        }
        void calcOneSignal(Image<Gray, Byte> InputImage, int site, int axis, int diffThreshold)
        {
            int[] histrogram = new int[SiteENZ.sizeAOI.Height];
            int cx = 0, cy = 0;
            switch (axis)
            {
                case 0:
                    cx = siteENZ[site].E.X;
                    cy = siteENZ[site].E.Y - SiteENZ.sizeAOI.Height / 2;
                    break;
                case 1:
                    cx = siteENZ[site].N.X;
                    cy = siteENZ[site].N.Y - SiteENZ.sizeAOI.Height / 2;
                    break;
                case 2:
                    cx = siteENZ[site].Z.X;
                    cy = siteENZ[site].Z.Y - SiteENZ.sizeAOI.Height / 2;
                    break;
            }
            for (int y = 0; y < SiteENZ.sizeAOI.Height; y++)
            {
                for (int x = 0; x < SiteENZ.sizeAOI.Width; x++)
                {
                    double intensity = InputImage[y + cy, x + cx].Intensity;
                    if (intensity < 200)
                        histrogram[y]++;
                }
            }
            int min = 0, max = histrogram.Length;
            for (int i = 0; i < histrogram.Length; i++)
            {
                if (histrogram[i] > 0)
                {
                    min = i;
                    break;
                }
            }
            for (int i = histrogram.Length - 1; i >= 0; i--)
            {
                if (histrogram[i] > 0)
                {
                    max = i;
                    break;
                }
            }
            if ((max - min) <= diffThreshold)
            {
                signalWarningCount++;
            }
            //Signal difference
            switch (axis)
            {
                case 0:
                    siteENZ[site].maxE = max;
                    siteENZ[site].minE = min;
                    siteENZ[site].diffE = max - min;
                    break;
                case 1:
                    siteENZ[site].maxN = max;
                    siteENZ[site].minN = min;
                    siteENZ[site].diffN = max - min;
                    break;
                case 2:
                    siteENZ[site].maxZ = max;
                    siteENZ[site].minZ = min;
                    siteENZ[site].diffZ = max - min;
                    break;
            }
            //Signal scale, not implemented yet
            switch (axis)
            {
                case 0:
                    siteENZ[site].scaleE = 0;
                    break;
                case 1:
                    siteENZ[site].scaleN = 0;
                    break;
                case 2:
                    siteENZ[site].scaleZ = 0;
                    break;
            }
        }
        public void SearchNan()
        {
            Image<Gray, Byte> sourceRgbImage;
            Image<Gray, Byte> TemplateImage;
            try
            {
                sourceRgbImage = new Image<Gray, Byte>(new Bitmap(sourceImage));
                TemplateImage = new Image<Gray, Byte>("template\\Nan.jpg");
            }
            catch
            {
                return;
            }

            startMatchTime = DateTime.Now;

            nanLabelList.Clear();
            using (Image<Gray, float> result = sourceRgbImage.MatchTemplate(TemplateImage, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                int borderWidth = TemplateImage.Size.Width;
                int borderHeight = TemplateImage.Size.Height;
                int deltaWidth = 3;
                int deltaHeight = 3;
                for (int y = borderHeight; y < result.Size.Height - borderHeight; y++)
                {
                    for (int x = borderWidth; x < result.Size.Width - borderWidth; x++)
                    {
                        if (result[y, x].Intensity > 0.6)
                        {
                            //search for near points to remove duplicate match
                            bool isRecorded = false;
                            foreach (Rectangle p in nanLabelList)
                            {
                                if ((Math.Abs(p.X - x) < deltaWidth) && (Math.Abs(p.Y - y) < deltaHeight))
                                {
                                    // This point is already recorded
                                    isRecorded = true;
                                    break;
                                }
                            }
                            if (!isRecorded)
                            {
                                nanLabelList.Add(new Rectangle(new Point(x, y), TemplateImage.Size));
                            }
                        }
                    }
                }
            }
            NanCount = nanLabelList.Count;
            endMatchTime = DateTime.Now;
        }
        public void parse_OCR()
        {
            if (sourceImage == null)
            {
                return;
            }

            Bitmap sourceBMP = new Bitmap(sourceImage);

            OCR_Date.parse(sourceBMP);
            OCR_Time.parse(sourceBMP);
            OCR_Longitude.parse(sourceBMP);
            OCR_Latitude.parse(sourceBMP);
            OCR_Depth.parse(sourceBMP);
            OCR_Mw.parse(sourceBMP);
            OCR_Mr.parseMr(sourceBMP);

            if (isParseSiteDataOCR)
            { 
                foreach (SiteENZ site in siteENZ)
                {
                    site.parseSite(sourceBMP);
                }
            }
        }
        public void showAOI()
        {
            if (sourceImage == null)
            {
                // no Output Image
                status = "No Source Image";
                return;
            }

            Graphics g;
            try
            {
                outputImage = (Image)sourceImage.Clone();
                g = Graphics.FromImage(outputImage);
            }
            catch (Exception e)
            {
                String msg = "Invalid AOI graphic handler: " + e.Message;
                Log.e(msg);
                status = msg;
                return;
            }

            Brush siteBrush = new SolidBrush(Color.FromArgb(128, 160, 160, 160));
            Brush nanBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 0));
            Brush dataBrush = new SolidBrush(Color.FromArgb(60, 160, 160, 160));
            Brush misfit1Brush = new SolidBrush(Color.FromArgb(60, 0, 160, 160));
            Brush misfit2Brush = new SolidBrush(Color.FromArgb(60, 160, 160, 0));
            Pen dataPen = new Pen(Color.Red), dataPen2 = new Pen(Color.Blue), dataPen3 = new Pen(Color.Red);            

            // Site AOI
            for (int i = 0; i < siteENZ.Length; i++)
            {
                RectangleF rect = new RectangleF(siteENZ[i].Title.X, siteENZ[i].Title.Y, 90, 50);
                g.DrawString(String.Format("{0}{1}",siteENZ[i].city, siteENZ[i].location), new Font("Tahoma", 8), Brushes.Black, rect);
                // 
                if (siteENZ[i].diffE <= SignalDiffValue)
                {
                    Rectangle box = new Rectangle(new Point(siteENZ[i].E.X, siteENZ[i].E.Y - SiteENZ.sizeAOI.Height / 2), SiteENZ.sizeAOI);
                    g.FillRectangle(siteBrush, box);
                }

                if (siteENZ[i].diffN <= SignalDiffValue)
                {
                    Rectangle box = new Rectangle(new Point(siteENZ[i].N.X, siteENZ[i].N.Y - SiteENZ.sizeAOI.Height / 2), SiteENZ.sizeAOI);
                    g.FillRectangle(siteBrush, box);
                }

                if (siteENZ[i].diffN <= SignalDiffValue)
                {
                    Rectangle box = new Rectangle(new Point(siteENZ[i].Z.X, siteENZ[i].Z.Y - SiteENZ.sizeAOI.Height / 2), SiteENZ.sizeAOI);
                    g.FillRectangle(siteBrush, box);
                }
            }

            // Nan AOI
            foreach (Rectangle nanLabel in nanLabelList)
            {
                g.FillRectangle(nanBrush, nanLabel);
            }

            // OCR AOI
            if (isShowOCRAOI)
            {
                foreach (CharBox box in OCR_Date.charBox)
                {
                    g.DrawRectangle(dataPen, box.rect);
                }

                foreach (CharBox box in OCR_Time.charBox)
                {
                    g.DrawRectangle(dataPen, box.rect);
                }

                foreach (CharBox box in OCR_Longitude.charBox)
                {
                    g.DrawRectangle(dataPen, box.rect);
                }
                foreach (CharBox box in OCR_Latitude.charBox)
                {
                    g.DrawRectangle(dataPen, box.rect);
                }
                foreach (CharBox box in OCR_Depth.charBox)
                {
                    g.DrawRectangle(dataPen, box.rect);
                }

                foreach (CharBox box in OCR_Mw.charBox)
                {
                    g.DrawRectangle(dataPen, box.rect);
                }
                foreach (CharBox box in OCR_Mr.charBox)
                {
                    g.DrawRectangle(dataPen2, box.rect);
                }

                // Site data AOI
                if (isParseSiteDataOCR)
                {
                    Size sizeDataAOI = new Size(50, 10);
                    foreach (SiteENZ site in siteENZ)
                    {
                        //Date E
                        {
                            int cx = site.E.X + SiteENZ.sizeAOI.Width + 1;
                            int cy = site.E.Y;
                            Rectangle rectK = new Rectangle(new Point(cx, cy - 11), sizeDataAOI);
                            Rectangle rectR = new Rectangle(new Point(cx, cy - 1), sizeDataAOI);
                            Rectangle rectB = new Rectangle(new Point(cx + 23, cy - 1), sizeDataAOI);

                            //g.FillRectangle(dataBrush, rectK);
                            for (int i = 0; i < site.OCR_E_K.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen, site.OCR_E_K.charBox[i].rect);
                            }
                            //g.FillRectangle(misfit1Brush, rectR);
                            for (int i = 0; i < site.OCR_E_R.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen2, site.OCR_E_R.charBox[i].rect);
                            }
                            //g.FillRectangle(misfit2Brush, rectB);
                            for (int i = 0; i < site.OCR_E_B.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen3, site.OCR_E_B.charBox[i].rect);
                            }
                        }

                        //Data N
                        {
                            int cx = site.N.X + +SiteENZ.sizeAOI.Width + 1;
                            int cy = site.N.Y;
                            Rectangle rectK = new Rectangle(new Point(cx, cy - 11), sizeDataAOI);
                            Rectangle rectR = new Rectangle(new Point(cx, cy - 1), sizeDataAOI);
                            Rectangle rectB = new Rectangle(new Point(cx + 23, cy - 1), sizeDataAOI);

                            //g.FillRectangle(dataBrush, rectK);
                            for (int i = 0; i < site.OCR_N_K.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen, site.OCR_N_K.charBox[i].rect);
                            }
                            //g.FillRectangle(misfit1Brush, rectR);
                            for (int i = 0; i < site.OCR_N_R.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen2, site.OCR_N_R.charBox[i].rect);
                            }
                            //g.FillRectangle(misfit2Brush, rectB);
                            for (int i = 0; i < site.OCR_N_B.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen3, site.OCR_N_B.charBox[i].rect);
                            }
                        }

                        //Data Z
                        {
                            int cx = site.Z.X + +SiteENZ.sizeAOI.Width + 1;
                            int cy = site.Z.Y;
                            Rectangle rectK = new Rectangle(new Point(cx, cy - 11), sizeDataAOI);
                            Rectangle rectR = new Rectangle(new Point(cx, cy - 1), sizeDataAOI);
                            Rectangle rectB = new Rectangle(new Point(cx + 23, cy - 1), sizeDataAOI);

                            //g.FillRectangle(dataBrush, rectK);
                            for (int i = 0; i < site.OCR_Z_K.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen, site.OCR_Z_K.charBox[i].rect);
                            }
                            //g.FillRectangle(misfit1Brush, rectR);
                            for (int i = 0; i < site.OCR_Z_R.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen2, site.OCR_Z_R.charBox[i].rect);
                            }
                            //g.FillRectangle(misfit2Brush, rectB);
                            for (int i = 0; i < site.OCR_Z_B.charBox.Count; i++)
                            {
                                g.DrawRectangle(dataPen3, site.OCR_Z_B.charBox[i].rect);
                            }
                        }
                    } //foreach site
                }
            }
        }
        public void searchError(DateTime date)
        {
            SqlManager SqlMgr = new SqlManager();
            String strSQL = ""; ;
            status = "";
            try
            {
                if (!SqlMgr.OpenDB())
                {
                    status = "searchError(): Cannot Open DataDB()";
                    Log.e(status);
                    return;
                }

                MySqlDataReader myReader;
                int count = 0;

                strSQL = "SELECT COUNT(*) FROM `" + dbTableName(date) + "` WHERE `Date`=\"" + date.ToString("yyyy/MM/dd") + "\" AND `ErrorCode`>0;";
                if ((myReader = SqlMgr.ExecuteReader(strSQL)) == null) return;
                if (myReader.Read())
                {
                    count = myReader.GetInt32(0);
                }
                myReader.Close();
                if (count > 0)
                {
                    strSQL = "SELECT  `Time` FROM `" + dbTableName(date) + "` WHERE `Date`=\"" + date.ToString("yyyy/MM/dd") + "\" AND `ErrorCode`>0;";
                    if ((myReader = SqlMgr.ExecuteReader(strSQL)) == null) return;

                    while (myReader.Read())
                    {
                        String errDate = date.ToString("yyyy-MM-dd");

                        DateTime time = Convert.ToDateTime(myReader.GetString(0));
                        DateTime errTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
                        String dir = String.Format("Source\\{0}\\{1}", errTime.ToString("yyyy-MM-dd"), errTime.ToString("HH"));
                        String fileName = String.Format("RMT{0}_{1}.*", errTime.ToString("yyyy-MM-dd"), errTime.ToString("HH-mm-ss"));
                        Log.e(dir + "\\" + fileName);
                    }
                }
                myReader.Close();
            }
            catch (Exception e)
            {
                status = String.Format("{0}: {1}", e.Message, strSQL);
                Log.sql(status);
            }
            SqlMgr.CloseDB();
        }
        public void run(DateTime dataTime, int mode) 
        {
            // Assume Image is loaded in picSourceImage
            if (sourceImage == null)
            {
                status = "Source Image is not Loaded";
                return;
            }
            errCode = 0;
            // prepare
            startProcessingTime = DateTime.Now; //start processing time

            // processing
            //step.1
            calcSignal(SignalDiffValue);

            //step.2
            SearchNan();

            //step 3            
            parse_OCR();

            //step 4
            showAOI();

            // save results
            saveUndefinedFont(isSaveTemplate);
            WriteCsvData(dataTime);

            //mode = 0; //debug, force to new record
            switch (mode)
            {
                case 0: // insert mode
                    InsertDbData(dataTime);
                    break;
                case 1: // update mode
                    UpdateDbData(dataTime);
                    break;
            }

            if (mode == 0 && errCode==ErrorCode.OCR_DecodeFail) //fail to decode
            {
                String FilePath = fileRoot.CreateSubPath("Error", FileRoot.Type.Monthly);
                String FileName = String.Format("RMT{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                SaveSourceImage(FilePath, FileName);
            }

            // finished
            endProcessingTime = DateTime.Now; //end processing time
        }
        public void test()
        {
            if (sourceImage == null)
            {
                // no Output Image
                status = "No Source Image";
                return;
            }

            Graphics g;
            try
            {
                outputImage = (Image)sourceImage.Clone();
                g = Graphics.FromImage(outputImage);
            }
            catch (Exception e)
            {
                String msg = "Invalid AOI graphic handler: " + e.Message;
                Log.e(msg);
                status = msg;
                return;
            }

            Brush siteBrush = new SolidBrush(Color.FromArgb(128, 160, 160, 160));
            Brush nanBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 0));
            Pen nanPen = new Pen(Color.Red), dataPen2 = new Pen(Color.Blue), dataPen3 = new Pen(Color.Red);


            Size sizeNanAOI = new Size(45, SiteENZ.sizeAOI.Height-1);
            Bitmap sourceBMP = new Bitmap(sourceImage);

            foreach (SiteENZ site in siteENZ)
            {
                Point nanLocE = new Point(site.E.X + SiteENZ.sizeAOI.Width + 1, site.E.Y - SiteENZ.sizeAOI.Height / 2 - 1);
                Point nanLocN = new Point(site.N.X + SiteENZ.sizeAOI.Width + 1, site.N.Y - SiteENZ.sizeAOI.Height / 2 - 1);
                Point nanLocZ = new Point(site.Z.X + SiteENZ.sizeAOI.Width + 1, site.Z.Y - SiteENZ.sizeAOI.Height / 2 - 1);

                Rectangle rectE = new Rectangle(nanLocE, sizeNanAOI);
                Rectangle rectN = new Rectangle(nanLocN, sizeNanAOI);
                Rectangle rectZ = new Rectangle(nanLocZ, sizeNanAOI);
                
                g.DrawRectangle(nanPen, rectE);
                g.DrawRectangle(nanPen, rectN);
                g.DrawRectangle(nanPen, rectZ);

                /*
                Bitmap templateE = OCR_AOI.createColorTemplate(sourceBMP.Clone(rectE, sourceBMP.PixelFormat));
                Bitmap templateN = OCR_AOI.createColorTemplate(sourceBMP.Clone(rectN, sourceBMP.PixelFormat));
                Bitmap templateZ = OCR_AOI.createColorTemplate(sourceBMP.Clone(rectZ, sourceBMP.PixelFormat));

                templateE.Save(String.Format("template\\Nan-t{0:00}.bmp",id++));
                templateN.Save(String.Format("template\\Nan-t{0:00}.bmp", id++));
                templateZ.Save(String.Format("template\\Nan-t{0:00}.bmp", id++));
                */

            }

        }
        #endregion

        #region save result
        public double OCR_ToDouble(String str, double defaultValue)
        {
            return (str.Contains("?") ? (defaultValue) : Convert.ToDouble(str));
        }
        String tableTailName(DateTime dataTime)
        {
            return String.Format("{0:0000}{1:00}", dataTime.Year, dataTime.Month); //每個月使用一個資料表
        }

        String dbTableName(DateTime dataTime)
        {
            return "RMT" + tableTailName(dataTime);
        }

        public bool CreateDbTabless(DateTime dataTime)
        {
            SqlManager MySQL = new SqlManager();
            status = "";
            try
            {
                if (!MySQL.OpenDB())
                {
                     status = MySQL.Status;
                    return false;
                }
                String strSQL = "CREATE TABLE IF NOT EXISTS `" + dbTableName(dataTime) + "` ("
                                    + "`Date` DATE NOT NULL ,"
                                    + "`Time` TIME NOT NULL ,"
                                    + "`RMT_Date` DATE NOT NULL ,"
                                    + "`RMT_Time` TIME NOT NULL ,"
                                    + "`Longitude` Float NOT NULL ,"
                                    + "`Latitude` Float NOT NULL ,"
                                    + "`Depth`  Float NOT NULL ,"
                                    + "`Mw` Float NOT NULL ,"
                                    + "`Mr` Float NOT NULL ,";

                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += "`" + siteENZ[site].id + "_SE` Float NOT NULL,"; //S=Signal Scale
                    strSQL += "`" + siteENZ[site].id + "_SN` Float NOT NULL,";
                    strSQL += "`" + siteENZ[site].id + "_SZ` Float NOT NULL,";
                }
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += "`" + siteENZ[site].id + "_DE` Float NOT NULL,"; //D=Signal Amplitude Difference
                    strSQL += "`" + siteENZ[site].id + "_DN` Float NOT NULL,";
                    strSQL += "`" + siteENZ[site].id + "_DZ` Float NOT NULL,";
                }
                strSQL += "`WarnCount` TINYINT NOT NULL ,";
                strSQL += "`NanCount` TINYINT NOT NULL ,";
                strSQL += "`SamplePeriod` Float NOT NULL ,";
                strSQL += "`ErrorCode` INT NOT NULL ,";
                strSQL += "INDEX (Date, Time) " + ") ENGINE = MYISAM ;";
                MySQL.RunSQL(strSQL);
                MySQL.CloseDB();
                return true;
            }
            catch
            {
                status = "Cannoe create " + dbTableName(dataTime);
                Log.sql(status);
            }
            MySQL.CloseDB();
            return false;
        }
        public bool AlertDbTabless(DateTime dataTime)
        {
            SqlManager MySQL = new SqlManager();
            status = "";
            try
            {
                if (!MySQL.OpenDB())
                {
                    status = MySQL.Status;
                    return false;
                }
                String strSQL = "ALTER TABLE `" + dbTableName(dataTime) + "`";

                /*
                // 1st site
                strSQL += "  ADD `" + siteENZ[0].id + "_SE` Double NOT NULL AFTER `Mr`";
                strSQL += ", ADD `" + siteENZ[0].id + "_SN` Double NOT NULL AFTER `" + siteENZ[0].id + "_SE`";
                strSQL += ", ADD `" + siteENZ[0].id + "_SZ` Double NOT NULL AFTER `" + siteENZ[0].id + "_SN`";
                for (int site = 1; site < siteENZ.Length; site++)
                {
                    strSQL += ", ADD `" + siteENZ[site].id + "_SE` Double NOT NULL AFTER `" + siteENZ[site - 1].id + "_SZ`";
                    strSQL += ", ADD `" + siteENZ[site].id + "_SN` Double NOT NULL AFTER `" + siteENZ[site].id + "_SE`";
                    strSQL += ", ADD `" + siteENZ[site].id + "_SZ` Double NOT NULL AFTER `" + siteENZ[site].id + "_SN`";
                }
                */

                strSQL += "  CHANGE `Longitude` `Longitude` FLOAT NOT NULL";
                strSQL += ", CHANGE `Latitude` `Latitude` FLOAT NOT NULL";
                strSQL += ", CHANGE `Depth` `Depth` FLOAT NOT NULL";
                strSQL += ", CHANGE `Mw` `Mw` FLOAT NOT NULL";
                strSQL += ", CHANGE `Mr` `Mr` FLOAT NOT NULL";
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    //CHANGE `Latitude` `Latitude` FLOAT NOT NULL;
                    strSQL += ", CHANGE `" + siteENZ[site].id + "_SE` `" + siteENZ[site].id + "_SE` FLOAT NOT NULL";
                    strSQL += ", CHANGE `" + siteENZ[site].id + "_SN` `" + siteENZ[site].id + "_SN` FLOAT NOT NULL";
                    strSQL += ", CHANGE `" + siteENZ[site].id + "_SZ` `" + siteENZ[site].id + "_SZ` FLOAT NOT NULL";
                }
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    //CHANGE `Latitude` `Latitude` FLOAT NOT NULL;
                    strSQL += ", CHANGE `" + siteENZ[site].id + "_DE` `" + siteENZ[site].id + "_DE` FLOAT NOT NULL";
                    strSQL += ", CHANGE `" + siteENZ[site].id + "_DN` `" + siteENZ[site].id + "_DN` FLOAT NOT NULL";
                    strSQL += ", CHANGE `" + siteENZ[site].id + "_DZ` `" + siteENZ[site].id + "_DZ` FLOAT NOT NULL";
                }
                strSQL += ", CHANGE `SamplePeriod` `SamplePeriod` FLOAT NOT NULL;";
                MySQL.RunSQL(strSQL);
                MySQL.CloseDB();
                return true;
            }
            catch
            {
                status = "Cannoe alert " + dbTableName(dataTime);
                Log.sql(status);
            }
            MySQL.CloseDB();
            return false;
        }
        public void InsertDbData(DateTime dataTime)
        {
            SqlManager SqlMgr = new SqlManager();
            String strSQL = ""; ;
            status = "";
            try
            {
                if (!SqlMgr.OpenDB())
                {
                    status = "UpdateDbData(): Cannot Open DataDB()";
                    Log.e(status);
                    return;
                }

                CreateDbTabless(dataTime);

                strSQL = "INSERT INTO " + dbTableName(dataTime) + " (`Date`, `Time`,`RMT_Date`,`RMT_Time`,`Longitude`,`Latitude`,`Depth`,`Mw`,`Mr`,";
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += "`" + siteENZ[site].id + "_SE`,";
                    strSQL += "`" + siteENZ[site].id + "_SN`,";
                    strSQL += "`" + siteENZ[site].id + "_SZ`,";
                }
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += "`" + siteENZ[site].id + "_DE`,";
                    strSQL += "`" + siteENZ[site].id + "_DN`,";
                    strSQL += "`" + siteENZ[site].id + "_DZ`,";
                }
                strSQL += "`WarnCount`,`NanCount`,`SamplePeriod`,`ErrorCode`)"
                       + String.Format("VALUES (\"{0}\",\"{1}\",", dataTime.ToString("yyyy/MM/dd"), dataTime.ToString("HH:mm:ss"))
                       + String.Format("\"{0}/{1}/{2}\",", OCR_Date.text.Substring(0, 4),OCR_Date.text.Substring(4, 2), OCR_Date.text.Substring(6, 2))
                       + String.Format("\"{0}:{1}:{2}\",", OCR_Time.text.Substring(0, 2), OCR_Time.text.Substring(2, 2), OCR_Time.text.Substring(4, 2))
                       + String.Format("{0:0.00},", OCR_ToDouble(OCR_Longitude.text, -100) / 100)
                       + String.Format("{0:0.00},", OCR_ToDouble(OCR_Latitude.text, -100) / 100)
                       + String.Format("{0},", OCR_ToDouble(OCR_Depth.text, -1))
                       + String.Format("{0:0.0},", OCR_ToDouble(OCR_Mw.text, -10) / 10)
                       + String.Format("{0:0.0},", OCR_ToDouble(OCR_Mr.text, -10) / 10);
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += String.Format("{0},", siteENZ[site].scaleE);
                    strSQL += String.Format("{0},", siteENZ[site].scaleN);
                    strSQL += String.Format("{0},", siteENZ[site].scaleZ);
                }
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += String.Format("{0},", siteENZ[site].diffE);
                    strSQL += String.Format("{0},", siteENZ[site].diffN);
                    strSQL += String.Format("{0},", siteENZ[site].diffZ);
                }
                strSQL += String.Format("{0},", signalWarningCount);
                strSQL += String.Format("{0},", NanCount);
                strSQL += String.Format("{0:0.0},", SamplePeriod);  //SamplePeriod
                strSQL += String.Format("{0});", (int)errCode);  // errCode, must do type case
                SqlMgr.RunSQL(strSQL);
            }
            catch (Exception e)
            {
                status = String.Format("{0}: {1}", e.Message, strSQL);
                Log.sql(status);
            }
            SqlMgr.CloseDB();
        }

        public void UpdateDbData(DateTime dataTime)
        {
            SqlManager SqlMgr = new SqlManager();
            String strSQL = ""; ;
            status = "";
            try
            {
                if (!SqlMgr.OpenDB())
                {
                    status = "UpdateDbData(): Cannot Open DataDB()";
                    Log.e(status);
                    return;
                }

                CreateDbTabless(dataTime);

                //UPDATE `rmt201710` SET `Date`=[value - 1],`Time`=[value - 2],`RMT_Date`=[value - 3],`RMT_Time`=[value - 4],
                //`Longitude`=[value - 5],`Latitude`=[value - 6],`Depth`=[value - 7],`Mw`=[value - 8],`Mr`=[value - 9],
                //`VWUC_DE`=[value - 10],`VWUC_DN`=[value - 11] ......
                //`WarnCount`=[value-64],`NanCount`=[value-65],`SamplePeriod`=[value-66],`ErrorCode`=[value-67] WHERE 1

                strSQL = "UPDATE " + dbTableName(dataTime) + " SET "
                    + String.Format("`RMT_Date`=\"{0}/{1}/{2}\",", OCR_Date.text.Substring(0, 4), OCR_Date.text.Substring(4, 2), OCR_Date.text.Substring(6, 2))
                    + String.Format("`RMT_Time`=\"{0}:{1}:{2}\",", OCR_Time.text.Substring(0, 2), OCR_Time.text.Substring(2, 2), OCR_Time.text.Substring(4, 2))
                    + String.Format("`Longitude`={0:0.00},", OCR_ToDouble(OCR_Longitude.text, -100) / 100)
                    + String.Format("`Latitude`={0:0.00},", OCR_ToDouble(OCR_Latitude.text, -100) / 100)
                    + String.Format("`Depth`={0},", OCR_ToDouble(OCR_Depth.text, -1))
                    + String.Format("`Mw`={0:0.0},", OCR_ToDouble(OCR_Mw.text, -10) / 10)
                    + String.Format("`Mr`={0:0.0},", OCR_ToDouble(OCR_Mr.text, -10) / 10);

                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += String.Format("`{0}_SE`={1},", siteENZ[site].id, siteENZ[site].scaleE);
                    strSQL += String.Format("`{0}_SN`={1},", siteENZ[site].id, siteENZ[site].scaleN);
                    strSQL += String.Format("`{0}_SZ`={1},", siteENZ[site].id, siteENZ[site].scaleZ);
                }
                for (int site = 0; site < siteENZ.Length; site++)
                {
                    strSQL += String.Format("`{0}_DE`={1},", siteENZ[site].id, siteENZ[site].diffE);
                    strSQL += String.Format("`{0}_DN`={1},", siteENZ[site].id, siteENZ[site].diffN);
                    strSQL += String.Format("`{0}_DZ`={1},", siteENZ[site].id, siteENZ[site].diffZ);
                }
                strSQL += String.Format("`WarnCount`={0},", signalWarningCount);
                strSQL += String.Format("`NanCount`={0},", NanCount);
                strSQL += String.Format("`SamplePeriod`={0},", SamplePeriod);
                strSQL += String.Format("`ErrorCode`={0} ", (int)errCode);

                strSQL += String.Format("WHERE `Date`=\"{0}\" AND `Time`=\"{1}\";", dataTime.ToString("yyyy/MM/dd"), dataTime.ToString("HH:mm:ss"));
 
                SqlMgr.RunSQL(strSQL);
            }
            catch (Exception e)
            {
                status = String.Format("{0}: {1}", e.Message, strSQL);
                Log.sql(status);
            }
            SqlMgr.CloseDB();
        }
        public bool WriteCsvData(DateTime dataTime)
        {
            String FilePath = fileRoot.CreateSubPath("Data", FileRoot.Type.Monthly);
            String FileName = String.Format("RMT_{0}.csv", dataTime.ToString("yyyy-MM-dd"));
            String dataFileName = FilePath + FileName;
            StreamWriter dataFileStream = fileRoot.AppendFileStream(dataFileName);
            status = "";

            if (dataFileStream != null)
            {
                //begin to write processing data
                try
                {
                    dataFileStream.Write(dataTime.ToString("yyyy-MM-dd HH:mm:ss") + ",#,");
                    dataFileStream.Write(String.Format("{0}-{1}-{2} ", OCR_Date.text.Substring(0, 4), OCR_Date.text.Substring(4, 2), OCR_Date.text.Substring(6, 2)));
                    dataFileStream.Write(String.Format("{0}:{1}:{2},", OCR_Time.text.Substring(0, 2), OCR_Time.text.Substring(2, 2), OCR_Time.text.Substring(4, 2)));
                    dataFileStream.Write(String.Format("{0:0.00},", OCR_ToDouble(OCR_Longitude.text, -100) / 100));
                    dataFileStream.Write(String.Format("{0:0.00},", OCR_ToDouble(OCR_Latitude.text, -100) / 100));
                    dataFileStream.Write(String.Format("{0},", OCR_ToDouble(OCR_Depth.text, -1)));
                    dataFileStream.Write(String.Format("{0:0.0},",OCR_ToDouble(OCR_Mw.text, -10) / 10));
                    dataFileStream.Write(String.Format("{0:0.0},#,", OCR_ToDouble(OCR_Mr.text, -1) / 10));
                    for (int i = 0; i < siteENZ.Length; i++)
                    {
                        dataFileStream.Write(String.Format("{0},", siteENZ[i].scaleE));
                        dataFileStream.Write(String.Format("{0},", siteENZ[i].scaleN));
                        dataFileStream.Write(String.Format("{0},", siteENZ[i].scaleZ));
                    }
                    dataFileStream.Write("#");
                    for (int i = 0; i < siteENZ.Length; i++)
                    {
                        dataFileStream.Write(String.Format("{0},", siteENZ[i].diffE));
                        dataFileStream.Write(String.Format("{0},", siteENZ[i].diffN));
                        dataFileStream.Write(String.Format("{0},", siteENZ[i].diffZ));
                    }
                    dataFileStream.Write(String.Format("#,{0},", signalWarningCount));
                    dataFileStream.Write(String.Format("{0},", NanCount));
                    dataFileStream.Write(String.Format("#,{0:0.0},", SamplePeriod));  //SamplePeriod
                    dataFileStream.Write(String.Format("{0}", errCode));  // errCode
                    dataFileStream.Write(Environment.NewLine);
                    // processing data finished

                    dataFileStream.Close();
                    return true;
                }
                catch (Exception e)
                {
                    status = " WriteCsvData() : " + e.Message;
                    Log.e(status);
                }
            }
            return false;
        }
        public String SaveOutputImage(String FilePath, String FileName)
        {
            String BmpFileName = FilePath + "\\" + FileName;
            if (outputImage != null)
            {
                try
                {
                    outputImage.Save(BmpFileName);
                }
                catch
                {
                    BmpFileName = null;
                }
            }
            return BmpFileName;
        }
        public String SaveSourceImage(String FilePath, String FileName)
        {
            String BmpFileName = FilePath + "\\" + FileName;
            if (sourceImage != null)
            {
                try
                {
                    sourceImage.Save(BmpFileName);
                }
                catch
                {
                    BmpFileName = null;
                }
            }
            return BmpFileName;
        }
        public bool DownloadRmtImage()
        {
            bool result = false;
            status = "";
            sourceImage = null; //clear

            startDownloadTime = DateTime.Now;
            try
            {
                WebRequest req = WebRequest.Create("http://rmt.earth.sinica.edu.tw/rmt.png");
                req.Timeout = 3000; //3000ms
                WebResponse response = req.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                sourceImage = Image.FromStream(stream);
                stream.Close();
                result = true;
            }
            catch (Exception)
            {
                status = String.Format("Download from RMT failed ({0})", DateTime.Now.ToLongTimeString());
                Log.e(status);
            }
            endDownloadTime = DateTime.Now;

            return result;
        }
        #endregion
    }

    #endregion

    #region PAlert Data Access & processing
    public class PalertPga
    {
        public List<String> rawData=new List<String>();
        public DateTime dataTime;
        public String timeTag = "";
        public Boolean newDataReceived = false;

        public int dataCount = 0; // count of non-zero data

        public PalertPga()
        {
        }

        public void clear()
        {
            rawData.Clear();
            timeTag = "";
            newDataReceived = false;
            dataCount = 0;
        }
    }
    public class PalertCore
    {
        public const int NumberOfPgaPages = 20;
        //public float MwThreahold = 0.1f;  /not used because the source data is always >1 if any suake is occurred 

        public FileRoot fileRoot;
        DateTime _startDownloadTime, _endDownloadTime;
        public double downloadTime, maxDownloadTime = 0;
        public String status, reason;

        public PalertPga[] pga =new PalertPga[NumberOfPgaPages];
        public int lastPage = -1, timeDiff=0;
        public Boolean isSyncAllPages = false;

        public PalertCore(String rootpath) //Constructor
        {
            fileRoot = new FileRoot(rootpath);
            for (int i = 0; i<NumberOfPgaPages; i++) {
                pga[i] = new PalertPga();
            }
        }

        public void Clear()
        {
            maxDownloadTime = 0;
        }
        // Download Time
        public DateTime startDownloadTime
        {
            get
            {
                return _startDownloadTime;
            }
            set
            {
                _startDownloadTime = value;
            }
        }
        public DateTime endDownloadTime
        {
            get
            {
                return _endDownloadTime;
            }
            set
            {
                _endDownloadTime = value;
                downloadTime = (_endDownloadTime - _startDownloadTime).Ticks / 1E7;
                if (downloadTime > maxDownloadTime)
                {
                    maxDownloadTime = downloadTime;
                }
            }
        }        

        private void downloadPGA(int page)
        {
            status = "";
            //Boolean trace = true; //debug

            String pgaUrl = String.Format("http://palert.earth.sinica.edu.tw/pga/pga.{0}",page+1);
            String timeTag = "";
            String webString = null;

            try
            {
                WebRequest req = WebRequest.Create(pgaUrl);
                req.Timeout = 5000;
                req.Method = "GET";
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader webReader = new StreamReader(stream);
                webString = webReader.ReadToEnd();
                stream.Close();
            }
            catch (Exception e)
            {
                pga[page].clear(); //error or lost sync
                status += String.Format("Palert D/L fail({0})' {1}", DateTime.Now.ToLongTimeString(), e.Message);
                Log.e(status);
            }
            
            if (webString==null) //data not read, maybe caused by web access fail
            {
                pga[page].clear(); //lost sync
                Log.e("Palert: null web String string");
                return;
            }

            StringReader dataReader = new StringReader(webString);
            if (dataReader == null)
            {
                Log.e("Palert: null data reader");
                return;
            }
            try
            {
                //if (trace) Log.e("Palert: trace 0");

                timeTag = dataReader.ReadLine(); //1st line
                // Save raw data string for debug
                String FilePath = fileRoot.CreateSubPath("Palert", FileRoot.Type.Hourly);
                String FileName = String.Format("PGA_{0}.{1:00}", timeTag, page);
                //String FileName = String.Format("Palert_{0}.PGA{1}", datetime.ToString("yyyy-MM-dd"), page);
                String dataFileName = FilePath + FileName;
                if (!File.Exists(dataFileName))
                {
                    // new file
                    StreamWriter dataFileStream = fileRoot.CreateFileStream(dataFileName); //must be a new file
                    dataFileStream.WriteLine(webString); // debug file
                    dataFileStream.Close();
                }
                else
                {
                    //file already exists, all task is normally done in previous download
                    return;
                }

                if (timeTag != pga[page].timeTag) //new data is coming
                {
                    //if (trace) Log.e("Palert: trace 1");
                    pga[page].timeTag = timeTag;
                    String strDate = timeTag.Substring(0, 10).Replace(' ', '-');
                    String strTime = timeTag.Substring(11, 2) + ":" + timeTag.Substring(13, 2) + ":" + timeTag.Substring(15, 2);
                    pga[page].dataTime = Convert.ToDateTime(strDate + " " + strTime).AddHours(8); //to UTC+8

                    //if (trace) Log.e("Palert: trace 2");
                    pga[page].rawData.Clear();
                    pga[page].dataCount = 0;


                    //if (trace) Log.e("Palert: trace 3");
                    String row = dataReader.ReadLine();

                    while (row!=null)
                    {
                        pga[page].rawData.Add(row); //ReadToEnd(); //
                        String[] item = row.Split(' ');
                        float Mw = (float)Convert.ToDouble(item[4]);
                        if (Mw>0)
                        {
                            pga[page].dataCount++;
                        }
                        row = dataReader.ReadLine(); //next row
                    }

                    //if (trace) Log.e("Palert: trace 4");
                    pga[page].newDataReceived = true;                    
                }
            }
            catch (Exception e)
            {
                pga[page].clear(); //lost sync

                status += String.Format("Palert parse fail: TimeTag='{0}', {1}", timeTag, e.Message);
                Log.e(status);
            }
        }
        private void syncAllPgaPages()
        {
            startDownloadTime = DateTime.Now;
            
            //download all pages
            for (int page = 0; page < PalertCore.NumberOfPgaPages; page++) //max 20 search             {
            {
                downloadPGA(page);
            }

            timeDiff = int.MaxValue;
            //search time tag
            for (int page = 0; page < PalertCore.NumberOfPgaPages; page++) //max 20 search             {
            {
                try
                {
                    int diff = (int)(DateTime.Now - pga[page].dataTime).TotalSeconds; // to UTC+8
                    if ((diff>0) && (diff <= timeDiff))
                    {
                        timeDiff = diff;
                        lastPage = page;
                    }
                }
                catch (Exception e)
                {
                    status = String.Format("Palert search time tag failed ({0}): {1}", DateTime.Now.ToLongTimeString(), e.Message);
                    Log.e(status);
                }
            }
            isSyncAllPages = true;
            endDownloadTime = DateTime.Now;
        }
        public void syncPga(Boolean syncAll=false)
        {
            reason = "";
            startDownloadTime = DateTime.Now;
            isSyncAllPages = false;
            
            if (syncAll)
            {
                // force to sync all
                reason = "Sync all : forced";
                syncAllPgaPages();
                return;
            }

            for (int page=0; page<NumberOfPgaPages; page++) {
                if (pga[page].rawData.Count == 0)
                {
                    // 1st sync, or lost sync
                    reason = String.Format("zeroPage Sync, timeDiff = {0}, zeroPage = {1}", timeDiff, page);
                    syncAllPgaPages();
                    return;
                }
            }

            lastPage = (lastPage + 1) % 20;
            //pga[lastPage].newDataReceived = false;
            downloadPGA(lastPage);
            timeDiff = (int)(DateTime.Now - pga[lastPage].dataTime).TotalSeconds; // to UTC+8
            if ((timeDiff<0) || (timeDiff > 60)) // lost sync
            {
                reason = String.Format("timeDiff Sync, timeDiff = {0}, lastPage = {1}", timeDiff, lastPage);
                syncAllPgaPages();
            }
            endDownloadTime = DateTime.Now;
        }
        String tableTailName(DateTime dataTime)
        {
            return String.Format("{0:0000}{1:00}", dataTime.Year, dataTime.Month); //每個月使用一個資料表
        }
        String dbTableName(DateTime dataTime)
        {
            return "Palert" + tableTailName(dataTime);
        }
        public bool CreateDbTabless(DateTime dataTime)
        {
            SqlManager MySQL = new SqlManager();
            status = "";
            try
            {
                if (!MySQL.OpenDB())
                {
                    status = MySQL.Status;
                    return false;
                }
                // W14C 21.9704 120.7345 0.179 0.00 0.189 0.169
                String strSQL = "CREATE TABLE IF NOT EXISTS `" + dbTableName(dataTime) + "` ("
                                    + "`Date` DATE NOT NULL ,"
                                    + "`Time` TIME NOT NULL ,"
                                    + "`Palert_Date` DATE NOT NULL ,"
                                    + "`Palert_Time` TIME NOT NULL ,"
                                    + "`TimeTag` VARCHAR(24) NOT NULL,"
                                    + "`SiteID` VARCHAR(8) NOT NULL ,"
                                    + "`Longitude` Float NOT NULL ,"
                                    + "`Latitude` Float NOT NULL ,"
                                    + "`PGA1`  Float NOT NULL ,"
                                    + "`Intensity`  Float NOT NULL ,"
                                    + "`PGA2` Float NOT NULL ,"
                                    + "`PGA3` Float NOT NULL ,";
                strSQL += "UNIQUE INDEX `TimeTag_SiteID` (`TimeTag`, `SiteID`) " + ") ENGINE = MYISAM ;";
                MySQL.RunSQL(strSQL);
                MySQL.CloseDB();
                return true;
            }
            catch
            {
                status = "Cannoe create " + dbTableName(dataTime);
                Log.sql(status);
            }
            MySQL.CloseDB();
            return false;
        }
        public void InsertDbRecord(DateTime dataTime, DateTime palertTime, String timeTag, String siteID, float Longitude, float Latitude, float PGA1, float Intensity, float PGA2, float PGA3)
        {
            SqlManager SqlMgr = new SqlManager();
            String strSQL = ""; ;
            status = "";
            try
            {
                if (!SqlMgr.OpenDB())
                {
                    status = "UpdateDbData(): Cannot Open DataDB()";
                    Log.e(status);
                    return;
                }

                CreateDbTabless(dataTime);

                strSQL = "INSERT INTO " + dbTableName(dataTime) + " (`Date`, `Time`,`Palert_Date`,`Palert_Time`,`TimeTag`,";
                strSQL += "`SiteID`,`Longitude`,`Latitude`,`PGA1`,`Intensity`,`PGA2`,`PGA3`) VALUES";
                strSQL += String.Format("(\"{0}\",\"{1}\",", dataTime.ToString("yyyy/MM/dd"), dataTime.ToString("HH:mm:ss"))
                       + String.Format("\"{0}\",\"{1}\",", palertTime.ToString("yyyy/MM/dd"), palertTime.ToString("HH:mm:ss"))
                       + String.Format("\"{0}\",", timeTag)
                       + String.Format("\"{0}\",", siteID)
                       + String.Format("{0:0.000000},", Longitude)
                       + String.Format("{0:0.000000},", Latitude)
                       + String.Format("{0:0.000000},", PGA1)
                       + String.Format("{0:0.00},", Intensity)
                       + String.Format("{0:0.0000},", PGA2)
                       + String.Format("{0:0.000000});", PGA3);
                SqlMgr.RunSQL(strSQL);
            }
            catch (Exception e)
            {
                status = String.Format("{0}: {1}", e.Message, strSQL);
                Log.sql(status);
            }
            SqlMgr.CloseDB();
        }
        public void InsertDbData(DateTime sampleTime, Boolean writeCSV) //sampleTime is UTC+8
        {
            //String FilePath = fileRoot.CreateSubPath("Palert", FileRoot.Type.Hourly);

            for (int page=0; page<NumberOfPgaPages; page++)
            {
                if (!pga[page].newDataReceived) continue; //skip to next page

               // StreamWriter dataFileStream = null;

                try
                {
                    /*
                    if (writeCSV)
                    {
                        String FileName = String.Format("Palert_{0}.txt", pga[page].timeTag);
                        String dataFileName = FilePath + FileName;
                        dataFileStream = fileRoot.CreateFileStream(dataFileName); //must be a new file

                        dataFileStream.WriteLine(pga[page].timeTag); //1st line
                    }
                    */

                    foreach (String row in pga[page].rawData) //skip 1st time tag line
                    {
                        /*
                        if (writeCSV)
                        {
                            dataFileStream.WriteLine(row); //must write to csv
                        }
                        */

                        if (pga[page].dataCount > 0) // has non-zero data to save db
                        {
                            String[] item = row.Split(' ');

                            float Intensity = (float)Convert.ToDouble(item[4]);

                            if (Intensity > 0) // >=MwThreahold)
                            {
                                String siteID = item[0];
                                float Latitude = (float)Convert.ToDouble(item[1]);
                                float Longitude = (float)Convert.ToDouble(item[2]);
                                float PGA1 = (float)Convert.ToDouble(item[3]);
                                //float Intensity = (float)Convert.ToDouble(item[4]); //already converted
                                float PGA2 = (float)Convert.ToDouble(item[5]);
                                float PGA3 = (float)Convert.ToDouble(item[6]);
                                InsertDbRecord(sampleTime, pga[page].dataTime, pga[page].timeTag, siteID, Longitude, Latitude, PGA1, Intensity, PGA2, PGA3);
                            }
                        }
                    }
                    //if (writeCSV) dataFileStream.Close();
                }
                catch (Exception e)
                {
                    status = String.Format("Palert Write DB : {0}", e.Message);
                    Log.e(status);
                }

                pga[page].newDataReceived = false; //data is saved, no new data in pga[page].rowData[] list
            }
        }
        public void ImportDbDataFromFile(String sourcePathName) //sampleTime is UTC+8
        {
            String filename = Path.GetFileName(sourcePathName);
            String date = filename.Substring(4, 10).Replace(' ','-');
            String time = filename.Substring(15, 2) + ":" + filename.Substring(17, 2) + ":" + filename.Substring(19, 2);
            DateTime dataTime = Convert.ToDateTime(date + " " + time).AddHours(8); //


            StreamReader dataReader = new StreamReader(sourcePathName);
            if (dataReader == null)
            {
                Log.e("Palert: ImportDbData null data reader");
                return;
            }
            try
            {
                String timeTag = dataReader.ReadLine(); //1st line

                String row = dataReader.ReadLine();
                while (row != null)
                {
                    String[] item = row.Split(' ');
                    float Intensity = (float)Convert.ToDouble(item[4]);
                    if (Intensity > 0) // >=MwThreahold)
                    {
                        String strDate = timeTag.Substring(0, 10).Replace(' ', '-');
                        String strTime = timeTag.Substring(11, 2) + ":" + timeTag.Substring(13, 2) + ":" + timeTag.Substring(15, 2);

                        String siteID = item[0];
                        float Latitude = (float)Convert.ToDouble(item[1]);
                        float Longitude = (float)Convert.ToDouble(item[2]);
                        float PGA1 = (float)Convert.ToDouble(item[3]);
                        //float Intensity = (float)Convert.ToDouble(item[4]); //already converted
                        float PGA2 = (float)Convert.ToDouble(item[5]);
                        float PGA3 = (float)Convert.ToDouble(item[6]);
                        InsertDbRecord(dataTime, dataTime, timeTag, siteID, Longitude, Latitude, PGA1, Intensity, PGA2, PGA3);
                    }
                    row = dataReader.ReadLine(); //next row
                }
            }
            catch (Exception e)
            {
                status = String.Format("Palert ImportDbData() : {0}", e.Message);
                Log.e(status);
            }
        }
    }
    #endregion

    #region Himawari8 Data Access & processing
    public class Himawari8
    {
        DateTime _startDownloadTime, _endDownloadTime;

        public String status;
        public Image sourceImage;
        public double downloadTime, maxDownloadTime=0;
        // Download Time
        public DateTime startDownloadTime
        {
            get
            {
                return _startDownloadTime;
            }
            set
            {
                _startDownloadTime = value;
            }
        }
        public DateTime endDownloadTime
        {
            get
            {
                return _endDownloadTime;
            }
            set
            {
                _endDownloadTime = value;
                downloadTime = (_endDownloadTime - _startDownloadTime).Ticks / 1E7;
                if (downloadTime > maxDownloadTime)
                {
                    maxDownloadTime = downloadTime;
                }
            }
        }
        public  enum ImageType { Visible, InfraridColor, InfraridMono, InfraridEnhence, TrueColor, RadarCompositeReflect };
        public static String[] ImageTypeName = {"Visible", "IRC", "IRM", "IRE",  "TrueColor", "RCR" };
        public enum Area { Taiwan, Asia, Global };
        public static String[] ImageAreaName = { "Taiwan", "Asia", "Global" };

        public static String[,] satImageUrlFormat = new String[,]
        {
                {   
                    //Global
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/sco/sco-{0}-{1:D2}.jpg", //Visible
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0p/s0p-{0}-{1:D2}.jpg", //IR Color
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0q/s0q-{0}-{1:D2}.jpg", //IR Enhence
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0o/s0o-{0}-{1:D2}.jpg" //IR Mono
                },
                {   
                    //Asia
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/sao/sao-{0}-{1:D2}.jpg", //Visible
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1p/s1p-{0}-{1:D2}.jpg", //IR Color
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1q/s1q-{0}-{1:D2}.jpg", //IR Enhence
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1o/s1o-{0}-{1:D2}.jpg" //IR Mono
                },
                {   //Taiwan
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/sbo/sbo-{0}-{1:D2}.jpg", //Visible
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3p/s3p-{0}-{1:D2}.jpg", //IR Color
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3q/s3q-{0}-{1:D2}.jpg", //IR Enhence
                    "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3o/s3o-{0}-{1:D2}.jpg" //IR Mono
                }
        };
        
        static public String ImageUrl(Himawari8.Area area, Himawari8.ImageType type)
        {
            String imageUrl = "";
            if (type == Himawari8.ImageType.RadarCompositeReflect)
            {
                imageUrl = "https://www.cwb.gov.tw/V7/observe/radar/Data/HD_Radar/CV1_3600.png";
            }
            else
            {
                if (area == Himawari8.Area.Taiwan)
                {
                    switch (type)
                    {
                        case Himawari8.ImageType.Visible:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/sbo/sbo.jpg";
                            break;
                        case Himawari8.ImageType.InfraridColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3p/s3p.jpg";
                            break;
                        case Himawari8.ImageType.InfraridEnhence:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3q/s3q.jpg";
                            break;
                        case Himawari8.ImageType.InfraridMono:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3o/s3o.jpg";
                            break;
                        case Himawari8.ImageType.TrueColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/ts3p/ts3p.jpg";
                            break;
                    }
                }
                else if (area == Himawari8.Area.Asia)
                {
                    switch (type)
                    {
                        case Himawari8.ImageType.Visible:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/sao/sao.jpg";
                            break;
                        case Himawari8.ImageType.InfraridColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1p/s1p.jpg";
                            break;
                        case Himawari8.ImageType.InfraridEnhence:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1q/s1q.jpg";
                            break;
                        case Himawari8.ImageType.InfraridMono:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1o/s1o.jpg";
                            break;
                        case Himawari8.ImageType.TrueColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/ts1p/ts1p.jpg";
                            break;
                    }
                }
                else if (area == Himawari8.Area.Global)
                {
                    switch (type)
                    {
                        case Himawari8.ImageType.Visible:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/sco/sco.jpg";
                            break;
                        case Himawari8.ImageType.InfraridColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0p/s0p.jpg";
                            break;
                        case Himawari8.ImageType.InfraridEnhence:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0q/s0q.jpg";
                            break;
                        case Himawari8.ImageType.InfraridMono:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0o/s0o.jpg";
                            break;
                        case Himawari8.ImageType.TrueColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/ts0p/ts0p.jpg";
                            break;
                    }
                }
            }
            return imageUrl;
        }

        static public String ImageUrl(Himawari8.Area area, Himawari8.ImageType type,DateTime pictureTime)
        {
            String imageUrl = "";
            String fileTail = "";

            if (type == Himawari8.ImageType.RadarCompositeReflect)
            {
                imageUrl = "https://www.cwb.gov.tw/V7/observe/radar/Data/HD_Radar/CV1_3600_";
                fileTail = String.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}.png",
                                    pictureTime.Year, pictureTime.Month, pictureTime.Day, pictureTime.Hour, (pictureTime.Minute / 10) * 10);
            }
            else
            {
                fileTail = String.Format("{0:0000}-{1:00}-{2:00}-{3:00}-{4:00}.jpg",
                                   pictureTime.Year, pictureTime.Month, pictureTime.Day, pictureTime.Hour, (pictureTime.Minute / 10) * 10);
                if (area == Himawari8.Area.Taiwan)
                {
                    switch (type)
                    {
                        case Himawari8.ImageType.Visible:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/sbo/sbo-";
                            break;
                        case Himawari8.ImageType.InfraridColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3p/s3p-";
                            break;
                        case Himawari8.ImageType.InfraridEnhence:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3q/s3q-";
                            break;
                        case Himawari8.ImageType.InfraridMono:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s3o/s3o-";
                            break;
                        case Himawari8.ImageType.TrueColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/ts3p/ts3p-";
                            break;
                    }
                }
                else if (area == Himawari8.Area.Asia)
                {
                    switch (type)
                    {
                        case Himawari8.ImageType.Visible:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/sao/sao-";
                            break;
                        case Himawari8.ImageType.InfraridColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1p/s1p-";
                            break;
                        case Himawari8.ImageType.InfraridEnhence:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1q/s1q-";
                            break;
                        case Himawari8.ImageType.InfraridMono:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s1o/s1o-";
                            break;
                        case Himawari8.ImageType.TrueColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/ts1p/ts1p-";
                            break;
                    }
                }
                else if (area == Himawari8.Area.Global)
                {
                    switch (type)
                    {
                        case Himawari8.ImageType.Visible:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/sco/sco-";
                            break;
                        case Himawari8.ImageType.InfraridColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0p/s0p-";
                            break;
                        case Himawari8.ImageType.InfraridEnhence:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0q/s0q-";
                            break;
                        case Himawari8.ImageType.InfraridMono:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/s0o/s0o-";
                            break;
                        case Himawari8.ImageType.TrueColor:
                            imageUrl = "http://www.cwb.gov.tw/V7/observe/satellite/Data/ts0p/ts0p-";
                            break;
                    }
                }
            }
            return imageUrl+fileTail;
        }
        public void DownloadHimawari8Image(Himawari8.Area area, Himawari8.ImageType type, DateTime pictureTime)
        {
            startDownloadTime = DateTime.Now;
            status = "";
            sourceImage = null; //clear

            String imageUrl;
            DateTime now = DateTime.Now;
            if (DateTime.Compare(now.AddMinutes(-20), pictureTime)>0) 
            {
                imageUrl = Himawari8.ImageUrl(area, type, pictureTime); // old picture 
            } else
            {
                imageUrl = Himawari8.ImageUrl(area, type); //current picture
            }
            try
            {
                WebRequest req = WebRequest.Create(imageUrl);
                req.Timeout = 5000; //5000ms
                WebResponse response = req.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                sourceImage = Image.FromStream(stream);
                stream.Close();
            }
            catch (Exception e)
            {
                status = String.Format("Download from Himawari8 failed ({0}): {1}", DateTime.Now.ToLongTimeString(), e.Message);
                Log.e(status);
            }

            endDownloadTime = DateTime.Now;
        }
        public String SaveSourceImage(String FilePath, String FileName)
        {
            String BmpFileName = FilePath + "\\" + FileName;
            if (sourceImage != null)
            {
                try
                {
                    sourceImage.Save(BmpFileName);
                }
                catch
                {
                    BmpFileName = null;
                }
            }
            return BmpFileName;
        }
    }
    #endregion

    #region Database Management
    public class SqlManager
    {
        public const String Address = "localhost";
        public const String UserID = "root";
        public const String Password = "";
        public const String Database = "EarthQuake";
        MySqlConnection dbConnection = null;

        String sqlStatus = "";

        public String Status
        {
            get
            {
                return sqlStatus;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------
        public SqlManager()
        {
        }

        ~SqlManager()
        {
        }
        internal static StreamWriter OpenLogFile(String FileName)
        {
            if (!File.Exists(FileName))
            {
                return File.CreateText(FileName);
            }
            else
            {
                return File.AppendText(FileName);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------
        public bool OpenDB()
        {
            String dbConnStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",
                            Address, UserID, Password, Database);

            sqlStatus = "OpenDataDB()";

            if (dbConnection == null)
            {
                dbConnection = new MySqlConnection(dbConnStr);
            }
            else
            {
                dbConnection.Close();
            }

            try
            {
                dbConnection.Open();
                sqlStatus += Environment.NewLine;
                RunSQL("SET NAMES 'utf8'");
                RunSQL("SET CHARACTER_SET_RESULTS=utf8");
                return true;
            }
            catch (MySqlException ex)
            {
                sqlStatus += ex.Message + Environment.NewLine;
                Log.e("OpenDataDB(): " + ex.Message);
            }
            return false;
        } //OpenDataDB

        //-----------------------------------------------------------------------------------------------------------------
        public void CloseDB()
        {
            sqlStatus = "CloseDB()";
            try
            {
                if (dbConnection == null)
                {
                    Log.sql("Try to CloseDB() when dbConnection is null");
                    return;
                }
                RunSQL("FLUSH TABLES");
                dbConnection.Close();
                sqlStatus += Environment.NewLine;
            }
            catch (MySqlException ex)
            {
                sqlStatus += ex.Message + Environment.NewLine;
                Log.e(" CloseDB(): " + ex.Message);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------
        public void RunSQL(String strSQL)
        {
            MySqlCommand dbSqlCmd;

            sqlStatus = "RunSQL(" + strSQL + ")";

            try
            {
                dbSqlCmd = new MySqlCommand(strSQL, dbConnection);
                dbSqlCmd.ExecuteNonQuery();
                sqlStatus += Environment.NewLine;
            }
            catch (MySqlException ex)
            {
                sqlStatus += ex.Message + Environment.NewLine;
                Log.e("SQL : " + ex.Message + "[" + strSQL + "]");
                Log.sql(strSQL);
            }
        } //RunSQL

        public MySqlDataReader ExecuteReader(String strSQL)
        {
            MySqlCommand dbSqlCmd;
            MySqlDataReader myReader = null;

            sqlStatus = "SQLReader(" + strSQL + ")";
            try
            {
                dbSqlCmd = new MySqlCommand(strSQL, dbConnection);
                myReader = dbSqlCmd.ExecuteReader();
                sqlStatus += Environment.NewLine;
            }
            catch (MySqlException ex)
            {
                sqlStatus += ex.Message + Environment.NewLine;
            }
            return myReader;
        } //ExecuteReader

        public object ExecuteScalar(String strSQL)
        {
            object result = 0;
            MySqlCommand dbSqlCmd;


            sqlStatus = "SQLReader(" + strSQL + ")";
            try
            {
                dbSqlCmd = new MySqlCommand(strSQL, dbConnection);
                result = dbSqlCmd.ExecuteScalar();
                sqlStatus += Environment.NewLine;
            }
            catch (MySqlException ex)
            {
                sqlStatus += ex.Message + Environment.NewLine;
            }
            return result;
        } //ExecuteReader        
    }
    
    #endregion

}
