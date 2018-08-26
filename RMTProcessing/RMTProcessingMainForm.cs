using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using EarthQuakeLib;
using System.Reflection;

/*
    2017/10/15  v0.0.0  Load RMT pipctures from http://rmt.earth.sinica.edu.tw.
    2017/10/17  v0.1.0  Define AOI of 54 signals.
    2017/10/18  v1.0.0  Calculate Warning value, add sound/diff/warn/config settings.
    2017/10/19  v1.0.1  Save pictures to the working directory.
    2017/10/22  v1.0.2  Improve processing speed.
    2017/10/26  v1.0.3  Detect NaN.
    2017/10/28  v1.0.4  Save processed data to CSV.
    2017/11/01  v1.0.5  Adjust interval to 5s.
    2017/11/06  v1.1.0  Add CWB web & Himawari8 pictures.
    2018/04/15  v1.2.0  OCR for date/time/depth/latitude/longtitude/.
    2018/04/16  v1.2.1  Save processed data to MySQL.
    2018/04/18  v1.2.2  OCR for Mr.
    2018/04/20  v1.3.0  Support web site analysis.
    2018/07/07  v2.0.0  Analysis of P-alert website.
    2018/07/10  v2.0.1  Get P-alert site list for web.
    2018/07/28  v2.0.2  Add P-alert site data to MySQL
    2018/08/16  v2.1.0  Save Himawari8 source pictures.
 */

namespace RMTProcessing
{
    public partial class RMTProcessingMainForm : Form
    {
        const String ConfigFileName = "RMTProcessing.cfg";
        const String webCwbDefaultUrl = "http://www.cwb.gov.tw/V7/earthquake/";
 
        bool isRmtProcessing = false, isPalertProcessing=false, isHimawari8Processing=false;
        //TabPage tabSystemStatus;
        int defaultFormWidth, defaultFormHeight;
        Himawari8.ImageType satImageType = Himawari8.ImageType.Visible;
        Himawari8.Area satImageArea = Himawari8.Area.Taiwan;
        List<TextBox> tbSiteInfoList = new List<TextBox>();
        List<Thread> threadPool = new List<Thread>();
        
        RmtCore rmtCore = new RmtCore(RmtCore.defaultRootPath);
        PalertCore palertCore = new PalertCore(RmtCore.defaultRootPath);
        Himawari8 satHimawari8 = new Himawari8();

        DataSetupForm dataSetupForm;

        //debug control
        Boolean isSaveRmtTemplate = true;
        Boolean isSaveRmtSourceImage = true;
        Boolean isSaveRmtDb = true;
        Boolean isSavePalertDb = true;
        Boolean isSavePalertSource = true;
        int RmtSignalDiff = 2;
        int RmtSampleTimeInterval = 5;

        #region Main Form
        public RMTProcessingMainForm()
        {
            InitializeComponent();
        }
        private void RMTProcessingMainForm_Load(object sender, EventArgs e)
        {
            LoadConfigFile();

            defaultFormWidth = Width;
            defaultFormHeight = Height;


            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //string assemblyVersion = Assembly.LoadFile('your assembly file').GetName().Version.ToString();
            string fileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            this.FindForm().Text = "RMT Processing " + assemblyVersion;

            //// Gets number of days since 1 day of Year 2000.
            const string TwoThousandYearDate = "01-01-2000";
            var startDate = DateTime.Parse(TwoThousandYearDate);
            var currentDate = DateTime.Now;
            var elapsedTimeSpan = currentDate.Subtract(startDate);
            var numberOfDaysSinceYearTwoThousand = elapsedTimeSpan.TotalDays;
            var timeSpanSinceMidnight = DateTime.Now - DateTime.Today;
            var totalNumberOfSecondsSinceMidnight = timeSpanSinceMidnight.TotalSeconds;
            var totalNumberOfSecondsSinceMidnightDivideByTwo = totalNumberOfSecondsSinceMidnight / 2;
            var currentExecutingAssemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.FindForm().Text = "RMT Processing " + currentExecutingAssemblyVersion;

            //tabSystemStatus = tabPagePalert;

            tbSiteInfoList.Add(tbSiteInfo1);
            tbSiteInfoList.Add(tbSiteInfo2);
            tbSiteInfoList.Add(tbSiteInfo3);
            tbSiteInfoList.Add(tbSiteInfo4);
            tbSiteInfoList.Add(tbSiteInfo5);
            tbSiteInfoList.Add(tbSiteInfo6);

            Font font = new Font("Arial", 9);
            this.FindForm().Font = font;
            foreach (Control control in this.Controls)
            {
                control.Font = font;
            }

            Font font2 = new Font(FontFamily.GenericMonospace, 9);
            foreach (TextBox tbBox in tbSiteInfoList) { 
                tbBox.Font = font2;
            }
            tbStat.Font = font2;

            //tbDiff.Text = RmtSignalDiff.ToString();
            tbWarn.Text = hScrollWarn.Value.ToString();
            tbNan.Text = hScrollNan.Value.ToString();
            tbMrLimit.Text = hScrollMrLimit.Value.ToString();
            tbMwLimit.Text = String.Format("{0:0.0}", (hScrollMwLimit.Value / 10.0));
            
            timerRMT.Interval = RmtSampleTimeInterval * 1000;

            picSource.Size = new Size((int)(picSource.Width * SourceImageScale), (int)(picSource.Height * SourceImageScale));
            picOutput.Size = new Size((int)(picOutput.Width * SourceImageScale), (int)(picOutput.Height * SourceImageScale));

            //tabPages.TabPages.Remove(tabSystemStatus);

            hScrollSatEnhenceLow.Maximum = hScrollSatEnhenceHigh.Maximum = satHistrogram.MaxGrayLevel;

            webCWB.Navigate(webCwbDefaultUrl);
            DownloadHimawari8Image(satImageArea,satImageType);

            toolStriptbRootPath.Text = rmtCore.fileRoot.pathname;

            // setup core runtime parameters
            rmtCore.isSaveTemplate = isSaveRmtTemplate;
            rmtCore.SignalDiffValue = RmtSignalDiff;
            rmtCore.isParseSiteDataOCR = false; //temperally disabled for speed up
            rmtCore.isShowOCRAOI = chkShowOcrAOI.Checked;
            
            //setup revertFrom default parameters
            dataSetupForm = new DataSetupForm(rmtCore.fileRoot.pathname);

            // 判斷是否已有程式在執行
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1)
            {
                MessageBox.Show("RMT Processing " + proc + " is runing"+Environment.NewLine+ "Auto Start not enabled", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusMessage.Text = "Auto Start not enabled";
            } else
            {
                if (menuAutoStart.Checked)
                    start();
            }
        }
        private void RMTProcessingMainForm_Shown(object sender, EventArgs e)
        {

        }

        private void RMTProcessingMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRMT.Enabled = false;
            timerClock.Enabled = false;
            foreach (Thread thread in threadPool)
            {
                if (thread.IsAlive) thread.Abort();
            }
        }
        private void RMTProcessingMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfigFile();
        }
        private void RMTProcessingMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //set 'Form.KeyPreview' Property to true, or the event handler is not responsing
            if (e.KeyCode == Keys.F5 && e.Control) //Enable Debug
            {
                /*
                if (tabPages.TabPages.Contains(tabSystemStatus) == false)
                {
                    tabPages.TabPages.Add(tabSystemStatus);
                } else
                {
                    tabPages.TabPages.Remove(tabSystemStatus);
                }
                */
                testToolStripMenuItem.Enabled = true;
            }
        }
        private void RMTProcessingMainForm_Resize(object sender, EventArgs e)
        {
            this.Width = defaultFormWidth;
            this.Height = defaultFormHeight;
        }
        #endregion

        #region UI update thread support
        delegate void updateTextBoxHandler(TextBox textBox, string text);
        delegate void appendTextBoxHandler(TextBox textBox, string text);
        delegate void updateToolStripLabelHandler(ToolStripLabel label, string text);
        delegate void refreshPictureHandler(PictureBox pic);
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
        private void refreshImage(PictureBox pic)
        {
            if (pic == null) return;
            pic.Refresh();
        }
        #endregion

        #region Processing Support/Management Functions
        void start()
        {
            showStatusMessage("Start");
            downloadToolStripMenuItem.Enabled = false;
            loadToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            toolStripBtnStart.Enabled = false;
            toolstripBtnStop.Enabled = true;
            
            runRmtProcessingTask();
            runPalertTask();

            timerRMT.Enabled = true;
            timerPalert.Enabled = true;

            showStatusMessage("");
        }
        void stop()
        {
            showStatusMessage("Stop");
            downloadToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = true;
            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            toolStripBtnStart.Enabled = true;
            toolstripBtnStop.Enabled = false;

            timerRMT.Enabled = false;
            timerPalert.Enabled = false;

            showStatusMessage("");
        }
        void updateThreadPool()
        {
            for (int i=0; i<threadPool.Count; i++)
            {
                if (threadPool[i].ThreadState== System.Threading.ThreadState.Stopped)
                {
                    threadPool.RemoveAt(i);
                    //this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "revmove thread" + Environment.NewLine);
                }
            }
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

        #region RMT Processing Support/Management Functions
        void showRmtMessage(String msg)
        {
            if (isTrace) {
                try
                {
                    this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusRmtCore, msg);
                }
                catch
                {

                }
            } 
        }
        void RmtRevertSourceData(String rootPath, DateTime startDate, DateTime endDate)
        {
            showStatusMessage("start to revert history data");

            RmtCore core = new RmtCore(rootPath);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1)) 
            {
                String sourcePath = String.Format("{0}\\Source\\{1}", core.fileRoot.pathname, date.ToString("yyyy-MM-dd"));
                String[] sourceFiles = null;
                try
                {
                    sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
                }
                catch
                {
                    Log.e("Revert source path not found : " + sourcePath);
                }

                if (sourceFiles != null)
                {
                    foreach (String pathname in sourceFiles)
                    {
                            String filename = Path.GetFileName(pathname);
                            String mark = filename.Substring(0, 3);
                            if (mark == "RMT") //valid source image
                            {
                                showStatusMessage("revert: " + filename);
                                runRmtRevertSourceTask(core, pathname, 0); //source mode
                            }
                            else
                            {
                                //not a source image
                            }
                            Thread.Sleep(10); //release CPU to run rmtCore
                    }
                }
                else
                {
                    Log.e("No revert error files");
                }
            }
            showStatusMessage("revert history data complete");
        }
        void RmtRevertErrorData(String rootPath)
        {
            showStatusMessage("start to revert error data");
            RmtCore core = new RmtCore(rootPath);
            {
                String sourcePath =  String.Format("{0}\\Error", core.fileRoot.pathname);
                showStatusMessage("Revert error is scaning : " + sourcePath);

                String[] sourceFiles = null;
                try
                {
                    sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
                }
                catch
                {
                    Log.e("Revert source path not found : " + sourcePath);
                }
                if (sourceFiles != null)
                {
                    foreach (String pathname in sourceFiles)
                    {
                            String filename = Path.GetFileName(pathname);
                            String mark = filename.Substring(0, 3);
                            if (mark == "RMT") //valid source image
                            {
                                showStatusMessage("revert: " + filename);
                                runRmtRevertSourceTask(core, pathname, 1); //error mode
                            }
                            else
                            {
                                //not a source image
                            }
                            Thread.Sleep(10); //release CPU to run rmtCore
                    }
                }
                else
                {
                    Log.e("No revert sourceFiles ");
                }
            }
            rmtRevertSourceDataToolStripMenuItem.Checked = false;
            rmtRevertSourceDataToolStripMenuItem.Text = "Revert History Data";
            showStatusMessage("revert history data complete");
        }
        void runRmtRevertSourceTask(RmtCore core, String sourcePathName, int mode)
        {
            String filename = Path.GetFileName(sourcePathName);
            String date = filename.Substring(3, 10);
            String time = filename.Substring(14, 2) + ":" + filename.Substring(17, 2) + ":" + filename.Substring(20, 2);
            DateTime dataTime = Convert.ToDateTime(date + " " + time);

            try
            {
                Image<Rgb, Byte> SourceImage = new Image<Rgb, byte>(sourcePathName);
                core.sourceImage = SourceImage.ToBitmap();
                core.run(dataTime, mode);
            }
            catch (Exception e)
            {
                Log.e("revert image failed (" + e.Message + "): " + sourcePathName);
            }
        }
        void runRmtRenameSourceTask(String sourcePathName)
        {
            char[] trim = { '.' };               
            String dir = Path.GetDirectoryName(sourcePathName);
            String sourceFileName = Path.GetFileName(sourcePathName);
            String ext = Path.GetExtension(sourcePathName);
            String year = sourceFileName.Substring(3, 4);
            String month = sourceFileName.Substring(8, 2);
            String day = sourceFileName.Substring(11, 2);
            String hour = sourceFileName.Substring(14, 2);
            String min = sourceFileName.Substring(17, 2);
            String sec = sourceFileName.Substring(20, 2);

            String destFileName = String.Format("RMT{0}-{1}-{2}_{3}-{4}-{5}{6}", year, month, day, hour, min, sec, ext);
            String destPathName = Path.Combine(dir, destFileName);
            File.Move(sourcePathName, destPathName);
        }
        void RmtRenameSourceData(String rootPath, DateTime startDate, DateTime endDate)
        {
            showStatusMessage("start to rename source data");

            //RmtCore core = new RmtCore(rootPath);
            DateTime dataDate = startDate.Date;
            while (dataDate <= endDate.Date)
            {
                String sourcePath =rootPath + String.Format("\\Source\\{0}", dataDate.ToString("yyyy-MM-dd"));
                showStatusMessage("Rename is scanning : " + dataDate.ToString("yyyy-MM-dd"));

                String[] sourceFiles = null;
                try
                {
                    sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
                }
                catch
                {
                    Log.e("Rename Source path not found : " + sourcePath);
                }
                if (sourceFiles != null)
                {
                    foreach (String pathname in sourceFiles)
                    {
                        String filename = Path.GetFileName(pathname);
                        String mark = filename.Substring(0, 7);
                        String ext = Path.GetExtension(filename);
                        if ((mark == "RMT2017") && ext.Substring(0,1)==".") //rename source image
                        {
                            showStatusMessage("rename: " + filename);
                            runRmtRenameSourceTask(pathname);
                        }
                        else
                        {
                            showStatusMessage("rename stop: " + filename);
                            return;
                        }
                        Thread.Sleep(10); //release CPU to run rmtCore
                    }
                }
                else
                {
                    Log.e("No rename sourceFiles : " + dataDate.ToShortDateString());
                }
                dataDate = dataDate.AddDays(1);
            }
            showStatusMessage("rename history data complete");
        }
        void RmtProcessing()
        {
            rmtCore.errCode = 0; // init processing error code
            //RMT Processing            showRmtMessage("Processing");

            // Assume Image is loaded in picSourceImage
            if (rmtCore.sourceImage == null)
            {
                showStatusMessage("Source Image is not Loaded");
                return;
            }

            // prepare
            rmtCore.startProcessingTime = DateTime.Now; //start processing time

            // processing
            //step.1
            showRmtMessage("Calculate Site Signal ");
            rmtCore.calcSignal(RmtSignalDiff);

            //step.2
            showRmtMessage("Search NaN");
            rmtCore.SearchNan();

            //step 3

            showRmtMessage("Parse OCR");
            rmtCore.parse_OCR();
            //step 3

            showRmtMessage("Show AOI");
            rmtCore.showAOI();

            // save results
            picOutput.Image = rmtCore.outputImage;
            rmtCore.saveUndefinedFont(isSaveRmtTemplate);


            // finished
            rmtCore.endProcessingTime = DateTime.Now; //end processing time
            showRmtMessage("");

            RmtShowStatistics();
            RmtShowResult();
        }
        void runRmtProcessingTask()
        {
            DateTime now = DateTime.Now;

            isRmtProcessing = true;

            if (rmtCore.SamplePeriod < 0)
            {
                rmtCore.SamplePeriod = timerRMT.Interval / 1000;
                rmtCore.lastProcessingTimer = now;
            }
            // Download RMT Image
            if (!DownloadRmtImage())
            {
                isRmtProcessing = false;
                return;
            }

            RmtProcessing();

            //ShowSystemStatus();

            // Save Result Image and Play Sound

            //run result
            Double Mw = rmtCore.OCR_ToDouble(rmtCore.OCR_Mw.text, -10) / 10;
            Double Mr = rmtCore.OCR_ToDouble(rmtCore.OCR_Mr.text, -10) / 10;
            if ((Mr>=hScrollMrLimit.Value) && (Mw> (hScrollMwLimit.Value/10.0)))  {
                Sound.RmtAlert(chkRmtMrSound.Checked);
            }
            else if (rmtCore.signalWarningCount >= hScrollWarn.Value ||
                rmtCore.nanLabelList.Count >= hScrollNan.Value)
            {
                //SetMonitorState(MonitorState.ON);
                Sound.Alert(chkRmtWarnSound.Checked);
            }

            if (isSaveRmtSourceImage)
            {
                if (saveSourceImage(now) == null)
                {
                    showStatusMessage(String.Format("Save Source Image Failed ({0})", now.ToShortTimeString()));
                }
            }
            if ((chkSaveRmtWarnImage.Checked) && (rmtCore.signalWarningCount >= hScrollWarn.Value))
            {
                if (saveWarningImage(now) == null)
                {
                    showStatusMessage(String.Format("Save Warning Image Failed ({0})", now.ToShortTimeString()));
                }
            }
            if ((chkSaveRmtWarnImage.Checked) && (rmtCore.nanLabelList.Count >= hScrollNan.Value))
            {
                if (saveNanImage(now) == null)
                {
                    showStatusMessage(String.Format("Save Nan Image Failed ({0})", now.ToShortTimeString()));
                }
            }
            if (chkSaveRmtCsv.Checked)
            {
                rmtCore.WriteCsvData(now);
            }
            if (isSaveRmtDb)
            {
                rmtCore.InsertDbData(now);
            }

            if (rmtCore.errCode > 0)
            {
                String FilePath = rmtCore.fileRoot.CreateSubPath("Error", FileRoot.Type.Monthly);
                String FileName = String.Format("RMT{0}.jpg", now.ToString("yyyy-MM-dd_HH-mm-ss"));
                rmtCore.SaveSourceImage(FilePath, FileName);
            }

            GC.Collect();
            isRmtProcessing = false;
        }
        void RmtShowStatistics()
        {
            showRmtMessage("Show Statistics");

            int diff = RmtSignalDiff;

            foreach (TextBox tbBox in tbSiteInfoList)
            {
                this.Invoke(new updateTextBoxHandler(updateTextBox), tbBox, "");
            }
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbStat, ""); //clear 
            // Show result in text

            // Site AOI
            for (int i = 0; i < rmtCore.siteENZ.Length; i++)
            {
                TextBox infoTextBox = tbSiteInfoList[i / 3];

                this.Invoke(new appendTextBoxHandler(appendTextBox), infoTextBox,
                        String.Format("{0:D2}.E min={1:D2} max={2:D2} {3}",
                        i + 1, rmtCore.siteENZ[i].minE, rmtCore.siteENZ[i].maxE, (rmtCore.siteENZ[i].diffE) <= diff ? "*" : " ") + Environment.NewLine);
                this.Invoke(new appendTextBoxHandler(appendTextBox), infoTextBox,
                        String.Format("{0:D2}.N min={1:D2} max={2:D2} {3}",
                        i + 1, rmtCore.siteENZ[i].minN, rmtCore.siteENZ[i].maxN, (rmtCore.siteENZ[i].diffN) <= diff ? "*" : " ") + Environment.NewLine);
                this.Invoke(new appendTextBoxHandler(appendTextBox), infoTextBox,
                        String.Format("{0:D2}.Z min={1:D2} max={2:D2} {3}",
                        i + 1, rmtCore.siteENZ[i].minZ, rmtCore.siteENZ[i].maxZ, (rmtCore.siteENZ[i].diffZ) <= diff ? "*" : " ") + Environment.NewLine);
            }

            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
                String.Format("     download: {0:F6} s", rmtCore.downloadTime) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format(" max download: {0:F6} s", rmtCore.maxDownloadTime) + Environment.NewLine + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("   calcSignal: {0:F6} s", rmtCore.calcTime) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("   search Nan: {0:F6} s", rmtCore.matchTime) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("   total proc: {0:F6} s", rmtCore.processingTime) + Environment.NewLine + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("   total time: {0:F6} s", rmtCore.downloadTime + rmtCore.processingTime) + Environment.NewLine + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("      warning: {0}/{1}", rmtCore.signalWarningCount, rmtCore.maxSignalWarningCount) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("          Nan: {0}/{1}", rmtCore.NanCount, rmtCore.maxNanCount) + Environment.NewLine + Environment.NewLine);
            long totalBytesOfMemoryUsed = Process.GetCurrentProcess().WorkingSet64;
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbStat,
               String.Format("  Used Memory: {0:F3} MB", (float)totalBytesOfMemoryUsed / 1000000.0f) + Environment.NewLine);

            showRmtMessage("");
        }
        void RmtShowResult()
        { 
            // show Processing result
            this.Invoke(new updateTextBoxHandler(updateTextBox), (tbDebug),
                String.Format("{0}Date# = {1} ({2})", (rmtCore.OCR_Date.charBox.Count == 8 ? " " : "*"), rmtCore.OCR_Date.text, rmtCore.OCR_Date.totalMSE)+Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), (tbDebug),
                String.Format("{0}Time# = {1} ({2})", (rmtCore.OCR_Time.charBox.Count == 6 ? " " : "*"), rmtCore.OCR_Time.text, rmtCore.OCR_Time.totalMSE) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), (tbDebug),
                String.Format("{0}Longitude# = {1} ({2})", (rmtCore.OCR_Longitude.charBox.Count == 5 ? " " : "*"), rmtCore.OCR_Longitude.text, rmtCore.OCR_Longitude.totalMSE) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), (tbDebug), 
                String.Format("{0}Latitude# = {1} ({2})", (rmtCore.OCR_Latitude.charBox.Count == 4 ? " " : "*"), rmtCore.OCR_Latitude.text, rmtCore.OCR_Latitude.totalMSE) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), (tbDebug),
                String.Format("{0}Depth# = {1} ({2})", (rmtCore.OCR_Depth.charBox.Count == 3 ? " " : "*"), rmtCore.OCR_Depth.text, rmtCore.OCR_Depth.totalMSE) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), (tbDebug),
                String.Format("{0}Mw# = {1} ({2})", (rmtCore.OCR_Mw.charBox.Count == 2 ? " " : "*"), rmtCore.OCR_Mw.text, rmtCore.OCR_Mw.totalMSE) + Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), (tbDebug),
                String.Format("{0}Mr# = {1} ({2})", (rmtCore.OCR_Mr.validOcrText() ? " " : "*"), rmtCore.OCR_Mr.text, rmtCore.OCR_Mr.totalMSE) + Environment.NewLine);

            bool soundError = false;
            if ((rmtCore.OCR_Date.charBox.Count != 8) || (!rmtCore.OCR_Date.validOcrText()))
            {
                this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "OCR_Date error"+Environment.NewLine);
                Log.d(String.Format("rmtCore.OCR_Date.charBox.Count = {0}, totalMSE = {1}", rmtCore.OCR_Date.charBox.Count, rmtCore.OCR_Date.totalMSE));
                soundError = true;
            }

            if ((rmtCore.OCR_Time.charBox.Count != 6) || (!rmtCore.OCR_Time.validOcrText()))
            {
                this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "OCR_Time error" + Environment.NewLine);
                Log.d(String.Format("rmtCore.OCR_Time.charBox.Count = {0}, totalMSE = {1}", rmtCore.OCR_Time.charBox.Count, rmtCore.OCR_Time.totalMSE));
                soundError = true;
            }
            if ((rmtCore.OCR_Longitude.charBox.Count != 5) || (!rmtCore.OCR_Longitude.validOcrText()))
            { 
                this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "OCR_Longitude error" + Environment.NewLine);
                Log.d(String.Format("rmtCore.OCR_Longitude.charBox.Count = {0}, totalMSE = {1}", rmtCore.OCR_Longitude.charBox.Count, rmtCore.OCR_Longitude.totalMSE));
                soundError = true;
            }
            if ((rmtCore.OCR_Latitude.charBox.Count != 4)|| (!rmtCore.OCR_Latitude.validOcrText()))
            { 
                this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "OCR_Latitude error" + Environment.NewLine);
                Log.d(String.Format("rmtCore.OCR_Latitude.charBox.Count = {0}, totalMSE = {1}", rmtCore.OCR_Latitude.charBox.Count, rmtCore.OCR_Latitude.totalMSE));
                soundError = true;
            }
            if ((rmtCore.OCR_Depth.charBox.Count != 3) || (!rmtCore.OCR_Depth.validOcrText()))
            {
                this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "OCR_Depth error" + Environment.NewLine);
                Log.d(String.Format("rmtCore.OCR_Depth.charBox.Count = {0}, totalMSE = {1}", rmtCore.OCR_Depth.charBox.Count, rmtCore.OCR_Depth.totalMSE));
                soundError = true;
            }
            if ((rmtCore.OCR_Mw.charBox.Count != 2) || (!rmtCore.OCR_Mw.validOcrText()))
            {
                this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog), "OCR_Mw error" + Environment.NewLine);
                Log.d(String.Format("rmtCore.OCR_Mw.charBox.Count = {0}, totalMSE = {1}", rmtCore.OCR_Mw.charBox.Count, rmtCore.OCR_Mw.totalMSE));
                soundError = true;
            }
            for (int i = 0; i < rmtCore.OCR_Mr.charBox.Count(); i++)
            {
                CharBox box = rmtCore.OCR_Mr.charBox[i];
                if (!box.validOcrText())
                {
                    this.Invoke(new appendTextBoxHandler(appendTextBox), (tbLog),((box.text=="?") ? "OCR_Mr fail" :"OCR_Mr error") + Environment.NewLine);
                    Log.d(String.Format("rmtCore.OCR_Mr.charBox[{0} MSE = {1} result='{2}'", i, box.MSE, box.text));
                    soundError = true;
                    break;
                }
            }
            //this.Invoke(new refreshPictureHandler(refreshImage), picOutput);

            if (soundError) Sound.Error(chkRmtWarnSound.Checked);
            showRmtMessage("");
        }

        #endregion

        #region Palert Support Functions
        void showPalertMessage(String msg)
        {
            if (isTrace)
            {
                try
                {
                    this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusPalert, msg);
                }
                catch { }
            }
        }
        void runPalertTask()
        {
            if (isPalertProcessing) return;

            isPalertProcessing = true;

            Thread thread = new Thread(syncPalert);
            threadPool.Add(thread);
            thread.Start();
        }

        void syncPalert()
        {
            showPalertMessage("Palert Sync");
            DateTime now = DateTime.Now;

            palertCore.syncPga(chkPalertForceSync.Checked);

            this.Invoke(new updateTextBoxHandler(updateTextBox), tbPalert, ""); //clear text box
            for (int i = 0; i < PalertCore.NumberOfPgaPages; i++)
            {
                try
                {
                    this.Invoke(new appendTextBoxHandler(appendTextBox), tbPalert,
                        String.Format("[{0:00}] {1} ({2:000}/{3:000}) {4}{5} {6}", i,
                            palertCore.pga[i].timeTag,
                            palertCore.pga[i].dataCount,
                            palertCore.pga[i].rawData.Count,
                            ((palertCore.pga[i].newDataReceived) ? "^" : " "),
                            ((i == palertCore.lastPage) ? "*" : " "),
                            ((i == palertCore.lastPage) ? "td = "+ palertCore.timeDiff.ToString() : " ")
                        + Environment.NewLine)); //show timetag only      
                }
                catch (Exception e)
                {
                    this.Invoke(new appendTextBoxHandler(appendTextBox), tbPalert,
                        String.Format("[{0:00}] {1}", i, e.Message)
                        + Environment.NewLine); //show timetag only      
                }
            }
            // show results
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbPalert, Environment.NewLine);
            this.Invoke(new appendTextBoxHandler(appendTextBox), tbPalert,
                    String.Format("     Max download time = {0}", palertCore.maxDownloadTime) + Environment.NewLine + Environment.NewLine);
            if (palertCore.isSyncAllPages)
            {
                Sound.MessageNudge(chkPalertSyncSound.Checked);
            }
            if (palertCore.lastPage >= 0)
            {
                if (palertCore.pga[palertCore.lastPage].dataCount >= 3)
                {
                    Sound.PAlert(chkPalertQuakeSound.Checked);
                }
            }
            if (palertCore.status.Length > 0)
            {
                this.Invoke(new appendTextBoxHandler(appendTextBox), tbPalert, palertCore.status + Environment.NewLine);
                showStatusMessage(palertCore.status);
            }
            if (palertCore.reason.Length>0) this.Invoke(new appendTextBoxHandler(appendTextBox), tbPalert, palertCore.reason + Environment.NewLine);
                
            if (isSavePalertDb)
            {
                this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusPalert, "write Palert DB");
                palertCore.InsertDbData(DateTime.Now, isSavePalertSource);
            }

            showPalertMessage("");


            isPalertProcessing = false;
        }
        void runPalertRenameSourceTask(String sourcePathName)
        {
            char[] trim = { '.' };
            String dir = Path.GetDirectoryName(sourcePathName);
            String sourceFileName = Path.GetFileName(sourcePathName);
            String ext = Path.GetExtension(sourcePathName);

            String year = sourceFileName.Substring(7, 4);
            String month = sourceFileName.Substring(12, 2);
            String day = sourceFileName.Substring(15, 2);
            String hour = sourceFileName.Substring(18, 2);
            String min = sourceFileName.Substring(20, 2);
            String sec = sourceFileName.Substring(22, 2);

            String destFileName = String.Format("PGA_{0} {1} {2} {3}{4}{5}{6}", year, month, day, hour, min, sec, ext);
            String destPathName = Path.Combine(dir, destFileName);
            File.Move(sourcePathName, destPathName);
        }
        void PalertRevertSourceData(String rootPath, DateTime startDate, DateTime endDate)
        {
            showStatusMessage("start to revert palert data");

            PalertCore core = new PalertCore(rootPath);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                String sourcePath = String.Format("{0}\\Palert\\{1}", core.fileRoot.pathname, date.ToString("yyyy-MM-dd"));
                String[] sourceFiles = null;
                try
                {
                    sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
                }
                catch
                {
                    Log.e("Revert source path not found : " + sourcePath);
                }

                if (sourceFiles != null)
                {
                    foreach (String pathname in sourceFiles)
                    {
                        String filename = Path.GetFileName(pathname);
                        String mark = filename.Substring(0, 3);
                        if (mark == "PGA") //valid source image
                        {
                            showStatusMessage("revert: " + filename);
                            runPalertRevertSourceTask(core, pathname); //source mode
                        }
                        else
                        {
                            //not a source image
                        }
                        Thread.Sleep(10); //release CPU to run rmtCore
                    }
                }
                else
                {
                    Log.e("No revert error files");
                }
            }
            showStatusMessage("revert history data complete");
        }
        void runPalertRevertSourceTask(PalertCore core, String sourcePathName)
        {

            try
            {
                core.ImportDbDataFromFile(sourcePathName);
            }
            catch (Exception e)
            {
                Log.e("revert image failed (" + e.Message + "): " + sourcePathName);
            }
        }
        void PalertRenameSourceData(String rootPath, DateTime startDate, DateTime endDate)
        {
            showStatusMessage("start to rename source data");
            
            DateTime dataDate = startDate.Date;
            while (dataDate <= endDate.Date)
            {
                String sourcePath = rootPath + String.Format("\\Palert\\{0}", dataDate.ToString("yyyy-MM-dd"));
                showStatusMessage("Rename is scanning : " + dataDate.ToString("yyyy-MM-dd"));

                String[] sourceFiles = null;
                try
                {
                    sourceFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();
                }
                catch
                {
                    Log.e("Rename Source path not found : " + sourcePath);
                }
                if (sourceFiles != null)
                {
                    foreach (String pathname in sourceFiles)
                    {
                        String filename = Path.GetFileName(pathname);
                        String mark = filename.Substring(0, 7);
                        String ext = Path.GetExtension(filename);
                        if (mark == "Palert_") //rename source image
                        {
                            showStatusMessage("rename: " + filename);
                            runPalertRenameSourceTask(pathname);
                        }
                        else
                        {
                            showStatusMessage("rename stop: " + filename);
                            //return;
                        }
                        Thread.Sleep(10); //release CPU to run rmtCore
                    }
                }
                else
                {
                    Log.e("No rename sourceFiles : " + dataDate.ToShortDateString());
                }
                dataDate = dataDate.AddDays(1);
            }
            showStatusMessage("rename history data complete");
        }

        #endregion

        #region Himawari8 Support Functions
        void showHimawari8Message(String msg)
        {
            if (isTrace)
            {
                try
                {
                    this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusHimawari8, msg);
                }
                catch { }
            }
        }

        Boolean validVisiblePictureTime(DateTime pictureTime)
        {
            const int m1= 5 * 60 + 0; //05:00
            const int m2 = 18 * 60 + 30;    //18:30
            int m = pictureTime.Hour * 60 + pictureTime.Minute;
            return (m >= m1 && m<=m2);
        }
        void runHimawari8ProcessingTask()
        {
            webCWB.Refresh();
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();


            // Save Himawari8 Source Picture
            DateTime now = DateTime.Now;
            DateTime downloadTime = now.AddMinutes(-21); //assigned time
            DateTime himawari8Time = now.AddMinutes(-21); //assigned time at 30 minute before
            DateTime radarTime = now.AddMinutes(-21); //assigned time at 30 minute before
            if (chkSaveHimawari8SourceImages.Checked)
            {
                for (Himawari8.Area area = Himawari8.Area.Taiwan; area <= Himawari8.Area.Asia; area++)
                {
                    this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), area, Himawari8.ImageType.InfraridColor, downloadTime);
                    saveHimawari8SourceImage(area, Himawari8.ImageType.InfraridColor, himawari8Time);

                    this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), area, Himawari8.ImageType.InfraridMono, downloadTime);
                    saveHimawari8SourceImage(area, Himawari8.ImageType.InfraridMono, himawari8Time);

                    this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), area, Himawari8.ImageType.InfraridEnhence, downloadTime);
                    saveHimawari8SourceImage(area, Himawari8.ImageType.InfraridEnhence, himawari8Time);

                    if (validVisiblePictureTime(himawari8Time))
                    {
                        this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), area, Himawari8.ImageType.Visible, downloadTime);
                        saveHimawari8SourceImage(area, Himawari8.ImageType.Visible, himawari8Time);
                    }

                    this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), area, Himawari8.ImageType.TrueColor, downloadTime);
                    saveHimawari8SourceImage(area, Himawari8.ImageType.TrueColor, himawari8Time);
                }

                this.Invoke(new downloadHimawari8Handler(satHimawari8.DownloadHimawari8Image), Himawari8.Area.Taiwan, Himawari8.ImageType.RadarCompositeReflect, downloadTime);
                saveHimawari8SourceImage(Himawari8.Area.Taiwan, Himawari8.ImageType.RadarCompositeReflect, radarTime);
            }
        }

        #endregion

        #region Config File
        public bool LoadConfigFile()
        {
            StreamReader ConfigFileStream;

            showStatusMessage("Load Config File");
        
            if (!File.Exists(ConfigFileName))
            {
                SaveConfigFile();
                return false;
            }
            ConfigFileStream = new StreamReader(ConfigFileName);

            String Line;
            while ((Line = ConfigFileStream.ReadLine()) != null)
            {
                if (Line.Contains("chkRmtWarnSound="))
                {
                    chkRmtWarnSound.Checked = Convert.ToBoolean(Line.Substring("chkRmtWarnSound=".Length));
                }
                else if (Line.Contains("chkRmtMrSound="))
                {
                    chkRmtMrSound.Checked = Convert.ToBoolean(Line.Substring("chkRmtMrSound=".Length));
                }
                else if (Line.Contains("chkShowOcrAOI="))
                {
                    chkShowOcrAOI.Checked = Convert.ToBoolean(Line.Substring("chkShowOcrAOI=".Length));
                }
                else if (Line.Contains("isSaveRmtSourceImage="))
                {
                    isSaveRmtSourceImage = Convert.ToBoolean(Line.Substring("isSaveRmtSourceImage=".Length));
                }
                else if (Line.Contains("chkSaveRmtCsv="))
                {
                    chkSaveRmtCsv.Checked = Convert.ToBoolean(Line.Substring("chkSaveRmtCsv=".Length));
                }
                else if (Line.Contains("isSaveRmtDb="))
                {
                    isSaveRmtDb = Convert.ToBoolean(Line.Substring("isSaveRmtDb=".Length));
                }
                else if (Line.Contains("isSaveRmtTemplate="))
                {
                    isSaveRmtTemplate = Convert.ToBoolean(Line.Substring("isSaveRmtTemplate=".Length));
                }
                else if (Line.Contains("chkSaveRmtNanImage="))
                {
                    chkSaveRmtNanImage.Checked = Convert.ToBoolean(Line.Substring("chkSaveRmtNanImage=".Length));
                }
                else if (Line.Contains("chkSaveRmtWarnImage="))
                {
                    chkSaveRmtWarnImage.Checked = Convert.ToBoolean(Line.Substring("chkSaveRmtWarnImage=".Length));
                }
                else if (Line.Contains("chkPalertForceSync="))
                {
                    chkPalertForceSync.Checked = Convert.ToBoolean(Line.Substring("chkPalertForceSync=".Length));
                }
                else if (Line.Contains("chkPalertQuakeSound="))
                {
                    chkPalertQuakeSound.Checked = Convert.ToBoolean(Line.Substring("chkPalertQuakeSound=".Length));
                }
                else if (Line.Contains("chkPalertSyncSound="))
                {
                    chkPalertSyncSound.Checked = Convert.ToBoolean(Line.Substring("chkPalertSyncSound=".Length));
                }
                else if (Line.Contains("isSavePalertDb="))
                {
                    isSavePalertDb = Convert.ToBoolean(Line.Substring("isSavePalertDb=".Length));
                }
                else if (Line.Contains("isSavePalertSource="))
                {
                    isSavePalertSource = Convert.ToBoolean(Line.Substring("isSavePalertSource=".Length));
                }
                else if (Line.Contains("chkSaveHimawari8SourceImages="))
                {
                    chkSaveHimawari8SourceImages.Checked = Convert.ToBoolean(Line.Substring("chkSaveHimawari8SourceImages=".Length));
                }
                
                else if (Line.Contains("menuAutoStart="))
                {
                    menuAutoStart.Checked = Convert.ToBoolean(Line.Substring("menuAutoStart=".Length));
                }
                else if (Line.Contains("RmtSignalDiff="))
                {
                    RmtSignalDiff = Convert.ToInt16(Line.Substring("RmtSignalDiff=".Length));
                    //tbDiff.Text = RmtSignalDiff.ToString();
                }
                else if (Line.Contains("Warn="))
                {
                    hScrollWarn.Value = Convert.ToInt16(Line.Substring("Warn=".Length));
                    tbWarn.Text = hScrollWarn.Value.ToString();
                }
                else if (Line.Contains("Nan="))
                {
                    hScrollNan.Value = Convert.ToInt16(Line.Substring("Nan=".Length));
                    tbNan.Text = hScrollNan.Value.ToString();
                }
                else if (Line.Contains("MrLimit="))
                {
                    hScrollMrLimit.Value = Convert.ToInt16(Line.Substring("MrLimit=".Length));
                    tbMrLimit.Text = hScrollMrLimit.Value.ToString();
                }
                else if (Line.Contains("MwLimit="))
                {
                    hScrollMwLimit.Value = Convert.ToInt16(Line.Substring("MwLimit=".Length));
                    tbMwLimit.Text = String.Format("{0:0.0}", (hScrollMwLimit.Value / 10.0));
                }
                else if (Line.Contains("RmtSampleTimeInterval="))
                {
                    RmtSampleTimeInterval = Convert.ToInt16(Line.Substring("RmtSampleTimeInterval=".Length));
                    //tbInterval.Text = RmtSampleTimeInterval.ToString();
                }
                else if (Line.Contains("RootPath="))
                {
                    rmtCore.fileRoot.setRoot(Line.Substring("RootPath=".Length));
                    palertCore.fileRoot.setRoot(rmtCore.fileRoot.pathname);
                }
                else
                {
                    MessageBox.Show("Config 設定錯誤:\n" + Line);
                    return false;
                }
            }
            ConfigFileStream.Close();

            showStatusMessage("");
            return true;
        }
        public void SaveConfigFile()
        {
            showStatusMessage("Save Config File");
            StreamWriter ConfigFileStream = new StreamWriter(ConfigFileName);
            ConfigFileStream.WriteLine(String.Format("chkRmtWarnSound={0}", chkRmtWarnSound.Checked));
            ConfigFileStream.WriteLine(String.Format("chkRmtMrSound={0}", chkRmtMrSound.Checked));
            ConfigFileStream.WriteLine(String.Format("isSaveRmtSourceImage={0}", isSaveRmtSourceImage));
            ConfigFileStream.WriteLine(String.Format("chkShowOcrAOI={0}", chkShowOcrAOI.Checked));
            ConfigFileStream.WriteLine(String.Format("chkSaveRmtCsv={0}", chkSaveRmtCsv.Checked));
            ConfigFileStream.WriteLine(String.Format("isSaveRmtDb={0}", isSaveRmtDb));
            ConfigFileStream.WriteLine(String.Format("isSaveRmtTemplate={0}", isSaveRmtTemplate));
            ConfigFileStream.WriteLine(String.Format("chkSaveRmtNanImage={0}", chkSaveRmtNanImage.Checked));
            ConfigFileStream.WriteLine(String.Format("chkSaveRmtWarnImage={0}", chkSaveRmtWarnImage.Checked));
            ConfigFileStream.WriteLine(String.Format("chkPalertForceSync={0}", chkPalertForceSync.Checked)); 
            ConfigFileStream.WriteLine(String.Format("chkPalertSyncSound={0}", chkPalertSyncSound.Checked));
            ConfigFileStream.WriteLine(String.Format("chkPalertQuakeSound={0}", chkPalertQuakeSound.Checked));
            ConfigFileStream.WriteLine(String.Format("isSavePalertDb={0}", isSavePalertDb));
            ConfigFileStream.WriteLine(String.Format("isSavePalertSource={0}", isSavePalertSource));
            ConfigFileStream.WriteLine(String.Format("chkSaveHimawari8SourceImages={0}", chkSaveHimawari8SourceImages.Checked));
            ConfigFileStream.WriteLine(String.Format("menuAutoStart={0}", menuAutoStart.Checked));
            ConfigFileStream.WriteLine(String.Format("RmtSignalDiff={0}", RmtSignalDiff));
            ConfigFileStream.WriteLine(String.Format("Warn={0}", hScrollWarn.Value));
            ConfigFileStream.WriteLine(String.Format("Nan={0}", hScrollNan.Value));
            ConfigFileStream.WriteLine(String.Format("MrLimit={0}", hScrollMrLimit.Value));
            ConfigFileStream.WriteLine(String.Format("MwLimit={0}", hScrollMwLimit.Value));
            ConfigFileStream.WriteLine(String.Format("RmtSampleTimeInterval={0}", RmtSampleTimeInterval));
            ConfigFileStream.WriteLine(String.Format("RootPath={0}", rmtCore.fileRoot.pathname));

            ConfigFileStream.Close();

            showStatusMessage("");
        }
        #endregion

        #region Timer
        private void timerClock_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusTime, 
                String.Format("{0} {1:D2}:{2:D2}:{3:D2}", now.ToShortDateString(), now.Hour, now.Minute, now.Second));

            updateThreadPool();
            this.Invoke(new updateToolStripLabelHandler(updateToolStripLabel), statusThread,
               String.Format("{0} Tasks", threadPool.Count));

            if ((now.Minute % 10 == 1) && (now.Second<5)) //update every 10 minute
            {
                if (!isHimawari8Processing) //start thread once
                {
                    Thread thread = new Thread(runHimawari8ProcessingTask);
                    threadPool.Add(thread);
                    thread.Start();
                    isHimawari8Processing = true;
                }
            }
            else
            {
                isHimawari8Processing = false;
            }
        }
        private void timerRMT_Tick(object sender, EventArgs e)
        {
            if (isRmtProcessing) return;  //prevent re-entry
            
            if (rmtCore.SamplePeriod <0) //1st init
            {
                rmtCore.SamplePeriod = timerRMT.Interval / 1000;
                rmtCore.lastProcessingTimer = DateTime.Now;
            }
            else
            {
                rmtCore.SamplePeriod = (DateTime.Now - rmtCore.lastProcessingTimer).TotalSeconds;
            }
            showRmtMessage("timer RMT start");
            rmtCore.lastProcessingTimer = DateTime.Now;

            Thread thread = new Thread(runRmtProcessingTask);
            threadPool.Add(thread);
            thread.Start();

            showRmtMessage("");
        }
        private void timerPalert_Tick(object sender, EventArgs e)
        {
            if (isPalertProcessing) return;  //prevent re-entry

            showPalertMessage("timer Palert start");

            runPalertTask();

            showPalertMessage("");
        }
        #endregion

        #region Picture Information
        private void picSourceImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (picSource.Image == null) return;

            Point loc = new Point((int)((float)e.X / SourceImageScale), (int)((float)e.Y / SourceImageScale));
            if (loc.X >= 0 && loc.X < picSource.Width && loc.Y >= 0 && loc.Y < picSource.Height)
            {
                statusMouse.Text = String.Format("( {0} . {1} )", loc.X, loc.Y);
                //Color pixelColor = GetColorAt(picSource, loc);
                Color pixelColor = ((Bitmap)rmtCore.sourceImage).GetPixel(loc.X, loc.Y);
                statusColor.Text = String.Format(" R={0}, G={1}, B={2}", pixelColor.R, pixelColor.G, pixelColor.B);
            }
        }
        private void picSourceImage_MouseLeave(object sender, EventArgs e)
        {
            statusMouse.Text = "";
            statusColor.Text = "";
        }
        private void picOutputImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (picOutput.Image == null) return;

            Point loc = new Point((int)((float)e.X / OutputImageScale), (int)((float)e.Y / OutputImageScale));
            if (loc.X >= 0 && loc.X < picOutput.Width && loc.Y >= 0 && loc.Y < picOutput.Height)
            {
                statusMouse.Text = String.Format("( {0} . {1} )", loc.X, loc.Y);
                //Color pixelColor = GetColorAt(picOutput, loc);
                Color pixelColor = ((Bitmap)picOutput.Image).GetPixel(loc.X, loc.Y);
                statusColor.Text = String.Format(" R={0}, G={1}, B={2}", pixelColor.R, pixelColor.G, pixelColor.B);
            }
        }
        private void picOutputImage_MouseLeave(object sender, EventArgs e)
        {
            statusMouse.Text = "";
            statusColor.Text = "";
        }

        private void picHimawari8_MouseMove(object sender, MouseEventArgs e)
        {
            if (picHimawari8 == null) return;

            if (e.X >= 0 && e.X < picSource.Width && e.Y >= 0 && e.Y < picSource.Height)
            {
                statusMouse.Text = String.Format("( {0} . {1} )", e.X, e.Y);
                //Color pixelColor = GetColorAt(picHimawari8, e.Location);
                Color pixelColor = ((Bitmap)picHimawari8.Image).GetPixel(e.X, e.Y);
                statusColor.Text = String.Format(" R={0}, G={1}, B={2}", pixelColor.R, pixelColor.G, pixelColor.B);
            }
        }
        private void picHimawari8_MouseLeave(object sender, EventArgs e)
        {
            statusMouse.Text = "";
            statusColor.Text = "";
        }

        #endregion

        #region Menu
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Openfile = new OpenFileDialog();
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                Image<Rgb, Byte> SourceImage = new Image<Rgb, byte>(Openfile.FileName);
                picSource.Image = SourceImage.ToBitmap();
                rmtCore.sourceImage = SourceImage.ToBitmap();
            }
        }
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadRmtImage();
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start();
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stop(); 
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void menuAutoStart_Click(object sender, EventArgs e)
        {
            menuAutoStart.Checked = !menuAutoStart.Checked;
            SaveConfigFile();
        }
        private void createRMTTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rmtCore.CreateDbTabless(DateTime.Now))
            {
                tbDebug.Text = "Create database successfully";
            }
            else
            {
                tbDebug.Text = "Create database fail : " + rmtCore.status;
            }
        }
        private void processingCurrentDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RmtProcessing();
        }
        private void openTemplatePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", @"template");
        }
        private void openDebugLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", Log.debugLogName);
        }
        private void openConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", ConfigFileName);
        }
        private void openRootPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", toolStriptbRootPath.Text);
        }
        private void openProjectPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath);
        }
        private void createTemplateDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rmtCore.createTemplatePath();
            showStatusMessage("Template directories are created");
        }

        private void alertDBTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime tableDate = new DateTime(2017, 10, 1);

            for (int i = 0; i <= 10; i++)
            {
                rmtCore.AlertDbTabless(tableDate);
                tableDate = tableDate.AddMonths(1);
            }
        }
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runHimawari8ProcessingTask();
        }

        private void palertRevertSourceDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSetupForm.setMode("Revert Source Data");
            DialogResult result = dataSetupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Thread thread = new Thread(() => PalertRevertSourceData(DataSetupForm.rootPath, DataSetupForm.startDate, DataSetupForm.endDate));
                threadPool.Add(thread);
                thread.Start();
            }

        }

        private void renamePalertSourceDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataSetupForm.setMode("Rename Source Data");
            DialogResult result = dataSetupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Thread thread = new Thread(() => PalertRenameSourceData(DataSetupForm.rootPath, DataSetupForm.startDate, DataSetupForm.endDate));
                threadPool.Add(thread);
                thread.Start();
            }
        }
        private void revertSourceDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSetupForm.setMode("Revert Source Data");
            DialogResult result = dataSetupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Thread thread = new Thread(() => RmtRevertSourceData(DataSetupForm.rootPath, DataSetupForm.startDate, DataSetupForm.endDate));
                threadPool.Add(thread);
                thread.Start();
            }
        }

        private void updateErrorDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSetupForm.setMode("Update Error Data");
            DialogResult result = dataSetupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Thread thread = new Thread(() => RmtRevertErrorData(DataSetupForm.rootPath));
                threadPool.Add(thread);
                thread.Start();
            }
        }
         
        private void renameSourceDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSetupForm.setMode("Rename Source Data");
            DialogResult result = dataSetupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Thread thread = new Thread(() => RmtRenameSourceData(DataSetupForm.rootPath, DataSetupForm.startDate, DataSetupForm.endDate));
                threadPool.Add(thread);
                thread.Start();
            }
        }
        private void searchErrorCopyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataSetupForm.setMode("Search & Copy Error");

            DialogResult result = dataSetupForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                RmtCore core = new RmtCore(DataSetupForm.rootPath);
                for (DateTime date= DataSetupForm.startDate; date<= DataSetupForm.endDate; date=date.AddDays(1)) {
                    showStatusMessage("Search " + date.ToShortDateString());
                    Thread thread = new Thread(() => core.searchError(date));
                    threadPool.Add(thread);
                    thread.Start();                        
                    thread.Join();
               }
               showStatusMessage("Search Complete");
           }

        }
        #endregion

        #region Setting
        /*
        private void RmtSignalDiffChanged(object sender, EventArgs e)
        {
            rmtCore.SignalDiffValue = RmtSignalDiff;  // setup core runtime parameters
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbDiff, RmtSignalDiff.ToString());
        }
        */
        private void hScrollWarn_ValueChanged(object sender, EventArgs e)
        {
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbWarn, hScrollWarn.Value.ToString());
        }
        private void hScrollNan_ValueChanged(object sender, EventArgs e)
        {
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbNan, hScrollNan.Value.ToString());
        }
        private void hScrollMrLimit_ValueChanged(object sender, EventArgs e)
        {
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbMrLimit, hScrollMrLimit.Value.ToString());
        }

        private void hScrollMwLimit_Scroll(object sender, ScrollEventArgs e)
        {
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbMwLimit, String.Format("{0:0.0}", (hScrollMwLimit.Value / 10.0)));
        }
        /*
        private void RmtSampleTimeIntervalChanged(object sender, EventArgs e)
        {
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbInterval, RmtSampleTimeInterval.ToString());
            if (timerRMT.Enabled)
            {
                // reset timer counter
                timerRMT.Enabled = false;
                timerRMT.Enabled = true;
            }
            timerRMT.Interval = RmtSampleTimeInterval*1000;
        }
        */
        private void chkSaveTemplate_CheckedChanged(object sender, EventArgs e)
        {
            rmtCore.isSaveTemplate = isSaveRmtTemplate;
        }
        private void chkShowOcrAOI_CheckedChanged(object sender, EventArgs e)
        {
            rmtCore.isShowOCRAOI = chkShowOcrAOI.Checked;
        }
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            chkRmtWarnSound.Checked = true;
            chkRmtMrSound.Checked = true;
            chkShowOcrAOI.Checked = false;
            isSaveRmtSourceImage = true;
            chkSaveRmtCsv.Checked = false;
            isSaveRmtDb = true;
            chkSaveRmtWarnImage.Checked = true;
            chkSaveRmtNanImage.Checked = true;
            isSaveRmtTemplate = true;
            isSavePalertSource = true;
            chkPalertForceSync.Checked = false;
            chkPalertQuakeSound.Checked = true;
            chkPalertSyncSound.Checked = false;
            chkSaveHimawari8SourceImages.Checked = true;

            //RmtSignalDiff = 2;
            //this.Invoke(new updateTextBoxHandler(updateTextBox), tbDiff, RmtSignalDiff.ToString());

            hScrollWarn.Value = 39;
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbWarn, hScrollWarn.Value.ToString());

            hScrollNan.Value = 12;
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbNan, hScrollNan.Value.ToString());

            hScrollMrLimit.Value = 40;
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbMrLimit, hScrollMrLimit.Value.ToString());

            hScrollMwLimit.Value = 40;
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbMwLimit, String.Format("{0:0.0}", (hScrollMwLimit.Value/10.0)));

            //RmtSampleTimeInterval = 5;
            //this.Invoke(new updateTextBoxHandler(updateTextBox), tbInterval,RmtSampleTimeInterval.ToString());
        }
        private void btnResetStatistic_Click(object sender, EventArgs e)
        {
            rmtCore.clear();
            palertCore.Clear();
            tbLog.Text = "";
        }


        #endregion

        #region Tool Strip
        private void toolStripBtnStart_Click(object sender, EventArgs e)
        {
            showStatusMessage("Start");

            start();
        }
        private void toolstripBtnStop_Click(object sender, EventArgs e)
        {
            showStatusMessage("Stop");

            stop();
        }
        private void toolStripBtnCamera_Click(object sender, EventArgs e)
        {
            if (tabPages.SelectedTab == tabPages.TabPages["tabPageSourceImage"])
            {
                if (rmtCore.sourceImage == null)
                {
                    showStatusMessage("No source image to save");
                    return;
                }

                showStatusMessage("Save source image to " + saveCaptureSourceImage(DateTime.Now));
                Sound.Shutter();
            }
            else if (tabPages.SelectedTab == tabPages.TabPages["tabPageOutputImage"])
            {
                if (picOutput.Image == null)
                {
                    showStatusMessage("No output image to save");
                    return;
                }

                showStatusMessage("Save output image to " + saveCaptureOutputImage(DateTime.Now));
                Sound.Shutter();
            } else
            {
                showStatusMessage("Only source and output image can be saved");
            }
        }
        private void toolStripBtnRootPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                toolStriptbRootPath.Text = dlg.SelectedPath;
                rmtCore.fileRoot.setRoot(dlg.SelectedPath);
                palertCore.fileRoot.setRoot(rmtCore.fileRoot.pathname);
                statusMessage.Text = "RootPath is set to " + rmtCore.fileRoot.pathname;

            }
        }
        private void toolStripBtnRefresh_Click(object sender, EventArgs e)
        {
            webCWB.Refresh();
        }
        private void toolStripBtnBack_Click(object sender, EventArgs e)
        {
            webCWB.GoBack();
        }
        private void toolStripBtnForward_Click(object sender, EventArgs e)
        {
            webCWB.GoForward();
        }

        private void toolStripBtnHome_Click(object sender, EventArgs e)
        {
            webCWB.Navigate(webCwbDefaultUrl);
        }

        #endregion

        #region Himawari8 Tab Page

        private void radioSatAreaGlobal_CheckedChanged(object sender, EventArgs e)
        {
            satImageArea = Himawari8.Area.Global;
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }

        private void radioSatAreaAsia_CheckedChanged(object sender, EventArgs e)
        {
            satImageArea = Himawari8.Area.Asia;
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }

        private void radioSatAreaTaiwan_CheckedChanged(object sender, EventArgs e)
        {
            satImageArea = Himawari8.Area.Taiwan;
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }
        private void btnHimawari8Rfresh_Click(object sender, EventArgs e)
        {
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }        
        
        private void radioSatImageVisible_CheckedChanged(object sender, EventArgs e)
        {
            satImageType = Himawari8.ImageType.Visible;
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }

        private void radioSatImageIrColor_CheckedChanged(object sender, EventArgs e)
        {
            satImageType = Himawari8.ImageType.InfraridColor;
            DownloadHimawari8Image(satImageArea, satImageType);
        }
        private void radioSatImageIrEnhence_CheckedChanged(object sender, EventArgs e)
        {
            satImageType = Himawari8.ImageType.InfraridEnhence;
            DownloadHimawari8Image(satImageArea, satImageType);
        }
        private void radioSatImageMono_CheckedChanged(object sender, EventArgs e)
        {
            satImageType = Himawari8.ImageType.InfraridMono;
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }
        private void radioSatImageTrueColor_CheckedChanged(object sender, EventArgs e)
        {
            satImageType = Himawari8.ImageType.TrueColor;
            DownloadHimawari8Image(satImageArea, satImageType);
            picHimawari8Histrogram.Invalidate();
        }
        private void btnHimawari8Equalize_Click(object sender, EventArgs e)
        {
            Bitmap sourceBMP = new Bitmap(picHimawari8.Image);
            Image<Gray, Byte> InputImage = new Image<Gray, Byte>(sourceBMP);
            
            int diff = hScrollSatEnhenceHigh.Value - hScrollSatEnhenceLow.Value;
             for (int y = 0; y < InputImage.Height; y++)
            {
                for (int x = 0; x < InputImage.Width; x++)
                {
                    int level = (int)InputImage[y, x].Intensity;
                    InputImage[y, x] = new Gray((level-hScrollSatEnhenceLow.Value)*256/diff);
                }
            }
            picHimawari8.Image = new Bitmap(InputImage.Bitmap);
            satHistrogram.Calc(picHimawari8.Image);
            picHimawari8Histrogram.Invalidate();

        }

        private void hScrollHimarwari8Enhence_ValueChanged(object sender, EventArgs e)
        {
            tbHimarwari8Enhence.Text = String.Format("{0:F1}",hScrollHimarwari8Enhence.Value / 10.0);
        }

        private void picHimawari8Histrogram_MouseMove(object sender, MouseEventArgs e)
        {
            statusMouse.Text = String.Format("Level {0}", e.X);
            statusColor.Text = String.Format("{0} / {1}", satHistrogram.pdf[e.X], satHistrogram.pdf.Max());
        }

        private void picHimawari8Histrogram_MouseLeave(object sender, EventArgs e)
        {
            statusMouse.Text = "";
            statusColor.Text = "";
        }
        private void hScrollSatEnhenceLow_ValueChanged(object sender, EventArgs e)
        {
            if (hScrollSatEnhenceLow.Value >= hScrollSatEnhenceHigh.Value)
            {
                if (hScrollSatEnhenceLow.Value > hScrollSatEnhenceLow.Maximum-1)
                {
                    hScrollSatEnhenceLow.Value = hScrollSatEnhenceLow.Maximum - 1;
                }
                hScrollSatEnhenceHigh.Value = hScrollSatEnhenceLow.Value + 1;
                hScrollSatEnhenceHigh.Invalidate();
                this.Invoke(new updateTextBoxHandler(updateTextBox), tbSatEnhenceHigh, hScrollSatEnhenceHigh.Value.ToString());
            }
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbSatEnhenceLow, hScrollSatEnhenceLow.Value.ToString());
            hScrollSatEnhenceLow.Invalidate();
            picHimawari8Histrogram.Invalidate();
        }
        private void hScrollSatEnhenceHigh_Scroll(object sender, ScrollEventArgs e)
        {
            if (hScrollSatEnhenceHigh.Value <= hScrollSatEnhenceLow.Value)
            {
                if (hScrollSatEnhenceHigh.Value < 1)
                {
                    hScrollSatEnhenceHigh.Value = 1;
                }
                hScrollSatEnhenceLow.Value = hScrollSatEnhenceHigh.Value - 1;
                hScrollSatEnhenceLow.Invalidate();
                this.Invoke(new updateTextBoxHandler(updateTextBox), tbSatEnhenceLow, hScrollSatEnhenceLow.Value.ToString());
            }
            this.Invoke(new updateTextBoxHandler(updateTextBox), tbSatEnhenceHigh, hScrollSatEnhenceHigh.Value.ToString());
            hScrollSatEnhenceHigh.Invalidate();
            picHimawari8Histrogram.Invalidate();
        }

        #endregion

        private void tabPages_Selected(object sender, TabControlEventArgs e)
        {
            toolStripBtnRefresh.Visible
                = toolStripBtnBack.Visible
                = toolStripSeparatorCWB.Visible
                = toolStripBtnHome.Visible 
                = toolStripBtnForward.Visible
                = (e.TabPage == tabPageCwbReport) ? true :false;
        }


        private void picHimawari8Histrogram_Paint(object sender, PaintEventArgs e)
        {
            /*
            if (satImageType==Himawari8.ImageType.InfraridColor || satImageType==Himawari8.ImageType.InfraridEnhence)
            {
                groupHimawari8Histrogram.Visible = false;
                return;
            }
            groupHimawari8Histrogram.Visible = true;
            */
            satHistrogram.Calc(picHimawari8.Image);
            int maxHeight = satHistrogram.pdf.Max();
            if (maxHeight == 0) return; //no data

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);
            for (int x=0; x<satHistrogram.MaxGrayLevel; x++)
            {
                g.DrawLine(pen, new Point(x, picHimawari8Histrogram.Height), new Point(x, picHimawari8Histrogram.Height-satHistrogram.pdf[x]*picHimawari8Histrogram.Height/maxHeight));
            }
            Pen pen2 = new Pen(Color.Blue);
            g.DrawLine(pen2, new Point(hScrollSatEnhenceLow.Value, picHimawari8Histrogram.Height), new Point(hScrollSatEnhenceLow.Value, 0));
            g.DrawLine(pen2, new Point(hScrollSatEnhenceHigh.Value, picHimawari8Histrogram.Height), new Point(hScrollSatEnhenceHigh.Value, 0));
        }


        private void picOutputImage_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
