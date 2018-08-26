using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EarthQuakeLib;
using System.IO;

namespace WebAnimation
{
    public partial class WebAnimationForm : Form
    {
        String defaultRootPath = @"E:\RMT Processing"; //default
        String urlRoot = "http://120.114.138.86/earthquake/";
        Himawari8 satHimawari8 = new Himawari8();
        Boolean autoSnap;
        int autoStep=0;
        DateTime timeTag;

        // new date format
        DateTime newVersionDate = DateTime.Now.AddDays(-1); // new DateTime(2018, 9, 1,0,0,0);

        public WebAnimationForm(Boolean snap)
        {
            InitializeComponent();
            autoSnap = snap;
        }

        private void WebAnimationForm_Load(object sender, EventArgs e)
        {
            setTimeInteval(autoSnap);
            tbMr.Text = "20";
            tbMw.Text = "3.5";
            tbWarning.Text = "12";
            toolStriptbRootPath.Text = defaultRootPath;

            /*
            if (DateTime.Compare(DateTime.Now,newVersionDate)>0)
            {
                // new
                picCloud.Location = new Point(678, 120);
                picCloud.Size = new Size(760, 675);
            }
            else
            {
                // old 
                picCloud.Location = new Point(678, 120);
                picCloud.Size = new Size(760, 675);
            }
            */

            if (autoSnap)
            {
                /*Thread thread = new Thread(snap);
                thread.Start();
                thread.Join();*/
                snap();
            }
        }

        #region UI update thread support
        delegate void updateTextBoxHandler(TextBox textBox, string text);
        delegate void appendTextBoxHandler(TextBox textBox, string text);
        delegate void updateToolStripLabelHandler(ToolStripLabel label, string text);
        delegate void taskHandler();
        private void updateTextBox(TextBox textBox, string str)
        {
            if (textBox == null) return;
            textBox.Text = str;
        }
        private void appendTextBox(TextBox textBox, string str)
        {
            if (textBox == null) return;
            textBox.Text += str;
        }
        private void updateToolStripLabel(ToolStripLabel label, string str)
        {
            if (label == null) return;
            label.Text = str;
        }
        void showStatusMessage(String msg)
        {
            DateTime now = DateTime.Now;
            try
            {
                this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusMessage, msg);
            }
            catch { }
        }
        #endregion
        void snap()
        {
            autoStart();
            autoAnalysis();
            timerSnap.Enabled = true;
        }

        private void setTimeInteval(Boolean snap)
        {
            DateTime time1 = DateTime.Now;
            if (snap) time1 = time1.AddHours(-1);
            time1 = new DateTime(time1.Year, time1.Month, time1.Day, time1.Hour, 0, 0);

            tbYear.Text = time1.Year.ToString();
            tbMonth.Text = time1.Month.ToString();
            tbDay1.Text = time1.Day.ToString();
            tbHour1.Text = time1.Hour.ToString();
            tbMinute1.Text = "0";

            DateTime time2 = time1.AddHours(1);
            tbDay2.Text = time2.Day.ToString();
            tbHour2.Text = time2.Hour.ToString();
            tbMinute2.Text = "0";

            timeTag = new DateTime(time1.Year, time1.Month, time1.Day, time1.Hour, 0, 0);
            tbTimeTag.Text = timeTag.ToString("yyyy/MM/dd HH:mm:ss");
        }

        delegate void downloadHimawari8Handler(Himawari8.Area area, Himawari8.ImageType type, DateTime picTime);
        private void savePicture()
        {
            String[] itemName = new String[] { "Mr", "Mw", "Warn" };
            String[] timeName = new String[] { "10m", "15m", "20m", "30m", "1h", "2h" };

            Rectangle rect=new Rectangle(0,0,0,0);
            switch (cbOutputType.Text)
            {
                case "Web":
                    rect = new Rectangle(
                            this.Bounds.Left + webBrowser.Location.X + 5,
                            this.Bounds.Top + menuStrip.Height + webBrowser.Location.Y + 10,
                              picCloud.Location.X - webBrowser.Location.X,
                              webBrowser.Height - 80);
                    break;
                case "Web+Sat":
                    rect = new Rectangle(
                          this.Bounds.Left + webBrowser.Location.X + 5,
                          this.Bounds.Top + menuStrip.Height + webBrowser.Location.Y + 10,
                            picCloud.Location.X-webBrowser.Location.X + picCloud.Width,
                            webBrowser.Height - 80);
                    break;
            }

            using (Bitmap bitmap = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(rect.Left, rect.Top), Point.Empty, rect.Size);
                }
                String filName = String.Format("{0}-{1}_{2:0000}-{3:00}-{4:00}_{5:00}-{6:00}.jpg",
                            itemName[cbAnalysisItem.SelectedIndex], timeName[cbTimeInterval.SelectedIndex],
                            timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, timeTag.Minute);
                String filePath = toolStriptbRootPath.Text + "\\Animation\\" + timeTag.ToString("yyyy-MM-dd");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                String pathName = filePath + "\\" + filName;
                bitmap.Save(pathName, ImageFormat.Jpeg);

                String ffmepgPath = toolStriptbRootPath.Text + "\\FFMpeg\\Animation";
                if (!Directory.Exists(ffmepgPath))
                {
                    Directory.CreateDirectory(ffmepgPath);
                }
                String ffmpegName= ffmepgPath + "\\" + filName;
                bitmap.Save(ffmpegName, ImageFormat.Jpeg);

                statusMessage.Text = "存檔: " + pathName;
            }            
        }

        private void snapPicture()
        {
            String[] itemName = new String[] { "Mr", "Mw", "Warn" };
            String[] timeName = new String[] { "10m", "15m", "20m", "30m", "1h", "2h" };
            
            int browerWidth = picCloud.Location.X - webBrowser.Location.X;
            int bmpHeight = picCloud.Location.Y - webBrowser.Location.Y + picCloud.Height - 20;
            int bmpWidth = 0;

            switch (cbOutputType.Text)
            {
                case "Web":
                    bmpWidth = browerWidth+ 5; //border 10
                    break;
                case "Web+Sat":
                    bmpWidth = browerWidth * 2 + 10; //border 10
                    break;
            }

            using (Bitmap bitmap = new Bitmap(bmpWidth, bmpHeight))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    g.Clear(Color.White);
                }

                int cloudLocationY = 66;
                webBrowser.DrawToBitmap(bitmap, new Rectangle(0, 0, browerWidth, bmpHeight));

                String typeName = "RMT";
                if (cbOutputType.Text == "Web+Sat") {
                    picCloud.DrawToBitmap(bitmap, new Rectangle(browerWidth + 5, cloudLocationY, browerWidth, picCloud.Height - 20));
                    typeName = Himawari8.ImageTypeName[cbCloudTyoe.SelectedIndex];
                }
                String filName = String.Format("{0}-{1}-{2}_{3:0000}-{4:00}-{5:00}_{6:00}-{7:00}.jpg",
                            typeName, itemName[cbAnalysisItem.SelectedIndex], timeName[cbTimeInterval.SelectedIndex],
                            timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, timeTag.Minute);
                String filePath = toolStriptbRootPath.Text + "\\Animation\\" + timeTag.ToString("yyyy-MM-dd")+"\\"+typeName;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                String pathName = filePath + "\\" + filName;
                bitmap.Save(pathName, ImageFormat.Jpeg);

                String ffmepgPath = toolStriptbRootPath.Text + "\\FFMpeg\\Animation" + "\\" + typeName; 
                if (!Directory.Exists(ffmepgPath))
                {
                    Directory.CreateDirectory(ffmepgPath);
                }
                String ffmpegName = ffmepgPath + "\\" + filName;
                bitmap.Save(ffmpegName, ImageFormat.Jpeg);

                statusMessage.Text = "存檔: " + pathName;
            }
        }
        private void webAnalysis(String urlPage, int year, int month, int day1, int hour1, int min1, int day2, int hour2, int min2, int mr, float mw, int warn)
        {
            String postData;

            postData = "year=" + year.ToString();
            postData += "&month=" + month.ToString();
            postData += "&day1=" + day1.ToString();
            postData += "&hour1=" + hour1.ToString();
            postData += "&min1=" + min1.ToString();
            postData += "&day2=" + day2.ToString();
            postData += "&hour2=" + hour2.ToString();
            postData += "&min2=" + min2.ToString();
            postData += "&mr=" + mr.ToString();
            postData += "&mw=" + mw.ToString();
            postData += "&warn=" + warn.ToString();
            postData += "&analysis=true";
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(postData);
            string url = urlRoot+urlPage;
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");           
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser.Document.Body.Style = "zoom:90%";
        }

        private void autoStart()
        {
            int year = Convert.ToInt32(tbYear.Text);
            int month = Convert.ToInt32(tbMonth.Text);
            int day1 = Convert.ToInt32(tbDay1.Text);
            int hour1 = Convert.ToInt32(tbHour1.Text);
            int min1 = Convert.ToInt32(tbMinute1.Text);

            timeTag = new DateTime(year, month, day1, hour1, min1, 0);
            tbTimeTag.Text = timeTag.ToString("yyyy/MM/dd HH:mm:ss");

            //if (!autoSnap)
            {
                showStatusMessage("準備:" + tbTimeTag.Text);
            }
        }

        private void autoNext()
        {
            switch (cbTimeInterval.Text)
            {
                case "10 Minutes":
                        timeTag = timeTag.AddMinutes(10);
                        timeTag = new DateTime(timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, (timeTag.Minute/10)*10, 0); //aligned to 10 minutes
                        break;
                case "15 Minutes":
                        timeTag = timeTag.AddMinutes(15);
                        timeTag = new DateTime(timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, (timeTag.Minute / 15) * 15, 0); //aligned to 15 minutes
                        break;
                case "20 Minutes":
                        timeTag = timeTag.AddMinutes(20);
                        timeTag = new DateTime(timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, (timeTag.Minute / 20) * 20, 0); //aligned 20 minutes
                        break;
                case "30 Minutes":
                        timeTag = timeTag.AddMinutes(30);
                        timeTag = new DateTime(timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, (timeTag.Minute / 30) * 30, 0); //aligned to 30 minutes
                        break;
                case "1 Hour":
                        timeTag = timeTag.AddHours(1);
                        timeTag = new DateTime(timeTag.Year, timeTag.Month, timeTag.Day, timeTag.Hour, 0, 0); //aligned to  hour
                        break;
                case "2 Hours":
                        timeTag = timeTag.AddHours(2);
                        timeTag = new DateTime(timeTag.Year, timeTag.Month, timeTag.Day, (timeTag.Hour/2)*2, 0, 0); //aligned to 2 hour
                        break;
                default: MessageBox.Show("Invalid Time Interval", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            tbTimeTag.Text = timeTag.ToString("yyyy/MM/dd HH:mm:ss");
            statusMessage.Text = "下一個:"+tbTimeTag.Text;
        }        

        private void autoAnalysis()
        {
            DateTime time1 = timeTag;
            DateTime time2;

            switch (cbTimeInterval.Text)
            {
                case "10 Minutes":
                    time2 = time1.AddMinutes(10);
                    time2 = new DateTime(time2.Year, time2.Month, time2.Day, time2.Hour, (time2.Minute / 10) * 10, 0); //aligned to 10 minutes
                    break;
                case "15 Minutes":
                    time2 = time1.AddMinutes(15);
                    time2 = new DateTime(time2.Year, time2.Month, time2.Day, time2.Hour, (time2.Minute / 15) * 15, 0); //aligned to 15 minutes
                    break;
                case "20 Minutes":
                    time2 = time1.AddMinutes(20);
                    time2 = new DateTime(time2.Year, time2.Month, time2.Day, time2.Hour, (time2.Minute / 20) * 20, 0); //aligned to 20 minutes
                    break;
                case "30 Minutes":
                    time2 = time1.AddMinutes(30);
                    time2 = new DateTime(time2.Year, time2.Month, time2.Day, time2.Hour, (time2.Minute / 30) * 30, 0); //aligned to 30 minutes
                    break;
                case "1 Hour":
                    time2 = time1.AddHours(1);
                    time2 = new DateTime(time2.Year, time2.Month, time2.Day, (time2.Hour / 1) * 1, 0, 0); //aligned to 1 hour
                    break;
                case "2 Hours":
                    time2 = time1.AddHours(2);
                    time2 = new DateTime(time2.Year, time2.Month, time2.Day, (time2.Hour / 2) * 2, 0, 0); //aligned to 2 hour
                    break;
                default: MessageBox.Show("Invalid Time Interval", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            int year = timeTag.Year;
            int month = timeTag.Month;
            int day1 = time1.Day;
            int hour1 = time1.Hour;
            int min1 = time1.Minute;
            int day2 = time2.Day;
            int hour2 = time2.Hour;
            int min2 = time2.Minute;
            int mr = Convert.ToInt32(tbMr.Text);
            float mw = (float)Convert.ToDouble(tbMw.Text); ;
            int warn = Convert.ToInt32(tbWarning.Text);

            switch (cbAnalysisItem.Text)
            {
                case "Mr最大強度圖":
                    webAnalysis("map_mr.php", year, month, day1, hour1, min1, day2, hour2, min2, mr, mw, warn);
                    statusMessage.Text = "分析：Mr最大強度圖";
                    break;
                case "Mw最大強度圖":
                    webAnalysis("map_mw.php", year, month, day1, hour1, min1, day2, hour2, min2, mr, mw, warn);
                    statusMessage.Text = "分析：Mw最大強度圖";
                    break;
                case "林志勇猜想分布圖":
                    webAnalysis("map_warning.php", year, month, day1, hour1, min1, day2, hour2, min2, mr, mw, warn);
                    statusMessage.Text = "分析：林志勇猜想分布圖";
                    break;
                //case "樣本熱區圖": webAnalysis_mr("map_hot.php", year, month, day1, hour1, min1, day2, hour2, min2, mr, mw); break;                        
                //case "Mw*Mr累積強度圖": webAnalysis_mr("map_mwmr-sum.php", year, month, day1, hour1, min1, day2, hour2, min2, mr, mw); break;
                //case "Mw累積強度圖": webAnalysis_mr("map_mw-sum.php", year, month, day1, hour1, min1, day2, hour2, min2, mr, mw); break;
                default: MessageBox.Show("Invalid Analysis Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            DateTime picTime = new DateTime(year, month, day1, hour1, min1, 0);
            Himawari8.ImageType cloudType=Himawari8.ImageType.InfraridColor;

            switch (cbCloudTyoe.Text)
            {
                case "可見光": cloudType = Himawari8.ImageType.Visible; break;
                case "紅外線彩色": cloudType = Himawari8.ImageType.InfraridColor; break;
                case "紅外線黑白": cloudType = Himawari8.ImageType.InfraridMono; break;
                case "色調強化": cloudType = Himawari8.ImageType.InfraridEnhence; break;
                case "真實色彩": cloudType = Himawari8.ImageType.TrueColor; break;
                case "雷達回波": cloudType = Himawari8.ImageType.RadarCompositeReflect; break;
            }
            this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), Himawari8.Area.Taiwan, cloudType, picTime);
            picCloud.Image = satHimawari8.sourceImage;
        }
        private void toolStripBtnRootPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                toolStriptbRootPath.Text = dlg.SelectedPath;
            }
        }

        private void toolStripBtnCamera_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            autoAnalysis();
        }

        private void btnSavePicture_Click(object sender, EventArgs e)
        {
            savePicture();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            autoStart();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            autoNext();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            autoStep = 0;
        }

        private void openRootPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", toolStriptbRootPath.Text);
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            int year= Convert.ToInt32(tbYear.Text);
            int month = Convert.ToInt32(tbMonth.Text);
            int day2 = Convert.ToInt32(tbDay2.Text);
            int hour2 = Convert.ToInt32(tbHour2.Text);
            int min2 = Convert.ToInt32(tbMinute2.Text);

            DateTime stop = new DateTime(year, month, day2, hour2, min2, 0);
            
            if (timeTag>=stop)
            {
                statusMessage.Text = "處理完畢";
                return;
            }

            Color backColor=Color.Gray, flashColor=Color.Red;
            switch (autoStep)
            {
                case 0:
                    backColor = btnStart.BackColor;
                    btnStart.BackColor = flashColor;
                    autoStart();
                    btnStart.BackColor = backColor;
                    autoStep = 1;
                    break;
                case 1:
                    backColor = btnAnalysis.BackColor;
                    btnAnalysis.BackColor = flashColor;
                    autoAnalysis();
                    btnAnalysis.BackColor = backColor;
                    autoStep = 2;
                    break;
                case 2:
                    backColor = btnSavePicture.BackColor;
                    btnSavePicture.BackColor = flashColor;
                    savePicture();
                    btnSavePicture.BackColor = backColor;
                    autoStep = 3;
                    break;
                case 3:
                    backColor = btnNext.BackColor;
                    btnNext.BackColor = flashColor;
                    autoNext();
                    btnNext.BackColor = backColor;
                    autoStep = 1;
                    break;
            }
        }

        private void timerSnap_Tick(object sender, EventArgs e)
        {
            timerSnap.Enabled = false;
            snapPicture();
            Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            snapPicture();
        }
    }
}
