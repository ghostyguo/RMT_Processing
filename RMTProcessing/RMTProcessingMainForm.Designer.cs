namespace RMTProcessing
{
    partial class RMTProcessingMainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RMTProcessingMainForm));
            this.picSource = new System.Windows.Forms.PictureBox();
            this.timerRMT = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRmtCore = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPalert = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusHimawari8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMouse = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusThread = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPages = new System.Windows.Forms.TabControl();
            this.tabPageSourceImage = new System.Windows.Forms.TabPage();
            this.tabPageOutputImage = new System.Windows.Forms.TabPage();
            this.tbSiteInfo5 = new System.Windows.Forms.TextBox();
            this.tbSiteInfo6 = new System.Windows.Forms.TextBox();
            this.tbSiteInfo4 = new System.Windows.Forms.TextBox();
            this.tbSiteInfo3 = new System.Windows.Forms.TextBox();
            this.tbSiteInfo1 = new System.Windows.Forms.TextBox();
            this.tbSiteInfo2 = new System.Windows.Forms.TextBox();
            this.picOutput = new System.Windows.Forms.PictureBox();
            this.tabPageAnalysis = new System.Windows.Forms.TabPage();
            this.tabPageCwbReport = new System.Windows.Forms.TabPage();
            this.webCWB = new System.Windows.Forms.WebBrowser();
            this.tabPageHimawari8 = new System.Windows.Forms.TabPage();
            this.groupHimawari8Histrogram = new System.Windows.Forms.GroupBox();
            this.picHimawari8Histrogram = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.hScrollSatEnhenceLow = new System.Windows.Forms.HScrollBar();
            this.tbSatEnhenceHigh = new System.Windows.Forms.TextBox();
            this.btnHiimawari8Equalize = new System.Windows.Forms.Button();
            this.hScrollSatEnhenceHigh = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSatEnhenceLow = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioSatAreaTaiwan = new System.Windows.Forms.RadioButton();
            this.radioSatAreaAsia = new System.Windows.Forms.RadioButton();
            this.radioSatAreaGlobal = new System.Windows.Forms.RadioButton();
            this.tbHimarwari8Enhence = new System.Windows.Forms.TextBox();
            this.hScrollHimarwari8Enhence = new System.Windows.Forms.HScrollBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioSatImageTrueColor = new System.Windows.Forms.RadioButton();
            this.radioSatImageMono = new System.Windows.Forms.RadioButton();
            this.radioSatImageIrEnhence = new System.Windows.Forms.RadioButton();
            this.radioSatImageVisible = new System.Windows.Forms.RadioButton();
            this.radioSatImageIrColor = new System.Windows.Forms.RadioButton();
            this.btnHimawari8Rfresh = new System.Windows.Forms.Button();
            this.picHimawari8 = new System.Windows.Forms.PictureBox();
            this.tabPagePalert = new System.Windows.Forms.TabPage();
            this.tbPalert = new System.Windows.Forms.TextBox();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRMTTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alertDBTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTemplateDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTemplatePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRootPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDebugLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runRmtTaskProcessingCurrentDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rmtRevertSourceDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rmtUpdateErrorDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameSourceDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchErrorCopyImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runPalertTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.palertRevertSourceDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renamePalertSourceDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.systemStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tbStat = new System.Windows.Forms.TextBox();
            this.chkRmtWarnSound = new System.Windows.Forms.CheckBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.chkSaveHimawari8SourceImages = new System.Windows.Forms.CheckBox();
            this.tbMwLimit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hScrollMwLimit = new System.Windows.Forms.HScrollBar();
            this.tbMrLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.hScrollMrLimit = new System.Windows.Forms.HScrollBar();
            this.chkPalertQuakeSound = new System.Windows.Forms.CheckBox();
            this.chkRmtMrSound = new System.Windows.Forms.CheckBox();
            this.chkPalertForceSync = new System.Windows.Forms.CheckBox();
            this.chkPalertSyncSound = new System.Windows.Forms.CheckBox();
            this.chkShowOcrAOI = new System.Windows.Forms.CheckBox();
            this.chkSaveRmtCsv = new System.Windows.Forms.CheckBox();
            this.btnDefaultSetting = new System.Windows.Forms.Button();
            this.chkSaveRmtNanImage = new System.Windows.Forms.CheckBox();
            this.tbNan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hScrollNan = new System.Windows.Forms.HScrollBar();
            this.chkSaveRmtWarnImage = new System.Windows.Forms.CheckBox();
            this.tbWarn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hScrollWarn = new System.Windows.Forms.HScrollBar();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.toolStripBtnStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnCamera = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStriptbRootPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripBtnRootPath = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolstripBtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripBtnHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorCWB = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnResetStatistic = new System.Windows.Forms.Button();
            this.tbDebug = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.timerPalert = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picSource)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.tabPageSourceImage.SuspendLayout();
            this.tabPageOutputImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).BeginInit();
            this.tabPageCwbReport.SuspendLayout();
            this.tabPageHimawari8.SuspendLayout();
            this.groupHimawari8Histrogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHimawari8Histrogram)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHimawari8)).BeginInit();
            this.tabPagePalert.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picSource
            // 
            this.picSource.BackColor = System.Drawing.Color.Black;
            this.picSource.Location = new System.Drawing.Point(16, 16);
            this.picSource.Name = "picSource";
            this.picSource.Size = new System.Drawing.Size(902, 541);
            this.picSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSource.TabIndex = 0;
            this.picSource.TabStop = false;
            this.picSource.MouseLeave += new System.EventHandler(this.picSourceImage_MouseLeave);
            this.picSource.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picSourceImage_MouseMove);
            // 
            // timerRMT
            // 
            this.timerRMT.Interval = 2000;
            this.timerRMT.Tick += new System.EventHandler(this.timerRMT_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage,
            this.statusRmtCore,
            this.statusPalert,
            this.statusHimawari8,
            this.statusMouse,
            this.statusColor,
            this.statusThread,
            this.statusTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 917);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1420, 24);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.BackColor = System.Drawing.SystemColors.Control;
            this.statusMessage.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(400, 19);
            this.statusMessage.Spring = true;
            this.statusMessage.Text = "Ready";
            this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusRmtCore
            // 
            this.statusRmtCore.AutoSize = false;
            this.statusRmtCore.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusRmtCore.Name = "statusRmtCore";
            this.statusRmtCore.Size = new System.Drawing.Size(220, 19);
            // 
            // statusPalert
            // 
            this.statusPalert.AutoSize = false;
            this.statusPalert.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusPalert.Name = "statusPalert";
            this.statusPalert.Size = new System.Drawing.Size(120, 19);
            // 
            // statusHimawari8
            // 
            this.statusHimawari8.AutoSize = false;
            this.statusHimawari8.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusHimawari8.Name = "statusHimawari8";
            this.statusHimawari8.Size = new System.Drawing.Size(200, 19);
            // 
            // statusMouse
            // 
            this.statusMouse.AutoSize = false;
            this.statusMouse.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusMouse.Name = "statusMouse";
            this.statusMouse.Size = new System.Drawing.Size(90, 19);
            // 
            // statusColor
            // 
            this.statusColor.AutoSize = false;
            this.statusColor.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusColor.Name = "statusColor";
            this.statusColor.Size = new System.Drawing.Size(135, 19);
            // 
            // statusThread
            // 
            this.statusThread.AutoSize = false;
            this.statusThread.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusThread.Name = "statusThread";
            this.statusThread.Size = new System.Drawing.Size(90, 19);
            // 
            // statusTime
            // 
            this.statusTime.AutoSize = false;
            this.statusTime.Name = "statusTime";
            this.statusTime.Size = new System.Drawing.Size(150, 19);
            this.statusTime.Text = "00:00:00";
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.tabPageSourceImage);
            this.tabPages.Controls.Add(this.tabPageOutputImage);
            this.tabPages.Controls.Add(this.tabPageAnalysis);
            this.tabPages.Controls.Add(this.tabPageCwbReport);
            this.tabPages.Controls.Add(this.tabPageHimawari8);
            this.tabPages.Controls.Add(this.tabPagePalert);
            this.tabPages.Location = new System.Drawing.Point(0, 49);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1150, 865);
            this.tabPages.TabIndex = 5;
            this.tabPages.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabPages_Selected);
            // 
            // tabPageSourceImage
            // 
            this.tabPageSourceImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPageSourceImage.Controls.Add(this.picSource);
            this.tabPageSourceImage.Location = new System.Drawing.Point(4, 24);
            this.tabPageSourceImage.Name = "tabPageSourceImage";
            this.tabPageSourceImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSourceImage.Size = new System.Drawing.Size(1142, 837);
            this.tabPageSourceImage.TabIndex = 0;
            this.tabPageSourceImage.Text = "Source Image";
            // 
            // tabPageOutputImage
            // 
            this.tabPageOutputImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPageOutputImage.Controls.Add(this.tbSiteInfo5);
            this.tabPageOutputImage.Controls.Add(this.tbSiteInfo6);
            this.tabPageOutputImage.Controls.Add(this.tbSiteInfo4);
            this.tabPageOutputImage.Controls.Add(this.tbSiteInfo3);
            this.tabPageOutputImage.Controls.Add(this.tbSiteInfo1);
            this.tabPageOutputImage.Controls.Add(this.tbSiteInfo2);
            this.tabPageOutputImage.Controls.Add(this.picOutput);
            this.tabPageOutputImage.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutputImage.Name = "tabPageOutputImage";
            this.tabPageOutputImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutputImage.Size = new System.Drawing.Size(1142, 839);
            this.tabPageOutputImage.TabIndex = 1;
            this.tabPageOutputImage.Text = "Output Image";
            // 
            // tbSiteInfo5
            // 
            this.tbSiteInfo5.BackColor = System.Drawing.SystemColors.Control;
            this.tbSiteInfo5.Location = new System.Drawing.Point(758, 689);
            this.tbSiteInfo5.Multiline = true;
            this.tbSiteInfo5.Name = "tbSiteInfo5";
            this.tbSiteInfo5.Size = new System.Drawing.Size(181, 142);
            this.tbSiteInfo5.TabIndex = 13;
            // 
            // tbSiteInfo6
            // 
            this.tbSiteInfo6.BackColor = System.Drawing.SystemColors.Control;
            this.tbSiteInfo6.Location = new System.Drawing.Point(945, 689);
            this.tbSiteInfo6.Multiline = true;
            this.tbSiteInfo6.Name = "tbSiteInfo6";
            this.tbSiteInfo6.Size = new System.Drawing.Size(181, 142);
            this.tbSiteInfo6.TabIndex = 14;
            // 
            // tbSiteInfo4
            // 
            this.tbSiteInfo4.BackColor = System.Drawing.SystemColors.Control;
            this.tbSiteInfo4.Location = new System.Drawing.Point(572, 689);
            this.tbSiteInfo4.Multiline = true;
            this.tbSiteInfo4.Name = "tbSiteInfo4";
            this.tbSiteInfo4.Size = new System.Drawing.Size(180, 142);
            this.tbSiteInfo4.TabIndex = 12;
            // 
            // tbSiteInfo3
            // 
            this.tbSiteInfo3.BackColor = System.Drawing.SystemColors.Control;
            this.tbSiteInfo3.Location = new System.Drawing.Point(386, 689);
            this.tbSiteInfo3.Multiline = true;
            this.tbSiteInfo3.Name = "tbSiteInfo3";
            this.tbSiteInfo3.Size = new System.Drawing.Size(180, 142);
            this.tbSiteInfo3.TabIndex = 11;
            // 
            // tbSiteInfo1
            // 
            this.tbSiteInfo1.BackColor = System.Drawing.SystemColors.Control;
            this.tbSiteInfo1.Location = new System.Drawing.Point(14, 689);
            this.tbSiteInfo1.Multiline = true;
            this.tbSiteInfo1.Name = "tbSiteInfo1";
            this.tbSiteInfo1.Size = new System.Drawing.Size(180, 142);
            this.tbSiteInfo1.TabIndex = 9;
            // 
            // tbSiteInfo2
            // 
            this.tbSiteInfo2.BackColor = System.Drawing.SystemColors.Control;
            this.tbSiteInfo2.Location = new System.Drawing.Point(200, 689);
            this.tbSiteInfo2.Multiline = true;
            this.tbSiteInfo2.Name = "tbSiteInfo2";
            this.tbSiteInfo2.Size = new System.Drawing.Size(180, 142);
            this.tbSiteInfo2.TabIndex = 10;
            // 
            // picOutput
            // 
            this.picOutput.BackColor = System.Drawing.Color.Black;
            this.picOutput.Location = new System.Drawing.Point(16, 16);
            this.picOutput.Name = "picOutput";
            this.picOutput.Size = new System.Drawing.Size(902, 541);
            this.picOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOutput.TabIndex = 1;
            this.picOutput.TabStop = false;
            this.picOutput.Paint += new System.Windows.Forms.PaintEventHandler(this.picOutputImage_Paint);
            this.picOutput.MouseLeave += new System.EventHandler(this.picOutputImage_MouseLeave);
            this.picOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picOutputImage_MouseMove);
            // 
            // tabPageAnalysis
            // 
            this.tabPageAnalysis.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPageAnalysis.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnalysis.Name = "tabPageAnalysis";
            this.tabPageAnalysis.Size = new System.Drawing.Size(1142, 839);
            this.tabPageAnalysis.TabIndex = 5;
            this.tabPageAnalysis.Text = "Analysis";
            // 
            // tabPageCwbReport
            // 
            this.tabPageCwbReport.Controls.Add(this.webCWB);
            this.tabPageCwbReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageCwbReport.Name = "tabPageCwbReport";
            this.tabPageCwbReport.Size = new System.Drawing.Size(1142, 839);
            this.tabPageCwbReport.TabIndex = 4;
            this.tabPageCwbReport.Text = "CWB Report";
            this.tabPageCwbReport.UseVisualStyleBackColor = true;
            // 
            // webCWB
            // 
            this.webCWB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webCWB.Location = new System.Drawing.Point(0, 0);
            this.webCWB.MinimumSize = new System.Drawing.Size(20, 20);
            this.webCWB.Name = "webCWB";
            this.webCWB.ScriptErrorsSuppressed = true;
            this.webCWB.Size = new System.Drawing.Size(1142, 839);
            this.webCWB.TabIndex = 0;
            this.webCWB.Url = new System.Uri("http://www.cwb.gov.tw/V7/earthquake/", System.UriKind.Absolute);
            // 
            // tabPageHimawari8
            // 
            this.tabPageHimawari8.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPageHimawari8.Controls.Add(this.groupHimawari8Histrogram);
            this.tabPageHimawari8.Controls.Add(this.groupBox3);
            this.tabPageHimawari8.Controls.Add(this.tbHimarwari8Enhence);
            this.tabPageHimawari8.Controls.Add(this.hScrollHimarwari8Enhence);
            this.tabPageHimawari8.Controls.Add(this.groupBox2);
            this.tabPageHimawari8.Controls.Add(this.btnHimawari8Rfresh);
            this.tabPageHimawari8.Controls.Add(this.picHimawari8);
            this.tabPageHimawari8.Location = new System.Drawing.Point(4, 22);
            this.tabPageHimawari8.Name = "tabPageHimawari8";
            this.tabPageHimawari8.Size = new System.Drawing.Size(1142, 839);
            this.tabPageHimawari8.TabIndex = 3;
            this.tabPageHimawari8.Text = "Himawari8";
            // 
            // groupHimawari8Histrogram
            // 
            this.groupHimawari8Histrogram.BackColor = System.Drawing.SystemColors.Window;
            this.groupHimawari8Histrogram.Controls.Add(this.picHimawari8Histrogram);
            this.groupHimawari8Histrogram.Controls.Add(this.label6);
            this.groupHimawari8Histrogram.Controls.Add(this.hScrollSatEnhenceLow);
            this.groupHimawari8Histrogram.Controls.Add(this.tbSatEnhenceHigh);
            this.groupHimawari8Histrogram.Controls.Add(this.btnHiimawari8Equalize);
            this.groupHimawari8Histrogram.Controls.Add(this.hScrollSatEnhenceHigh);
            this.groupHimawari8Histrogram.Controls.Add(this.label5);
            this.groupHimawari8Histrogram.Controls.Add(this.tbSatEnhenceLow);
            this.groupHimawari8Histrogram.Location = new System.Drawing.Point(833, 277);
            this.groupHimawari8Histrogram.Name = "groupHimawari8Histrogram";
            this.groupHimawari8Histrogram.Size = new System.Drawing.Size(294, 293);
            this.groupHimawari8Histrogram.TabIndex = 65;
            this.groupHimawari8Histrogram.TabStop = false;
            this.groupHimawari8Histrogram.Text = "Histrogram";
            // 
            // picHimawari8Histrogram
            // 
            this.picHimawari8Histrogram.BackColor = System.Drawing.Color.Black;
            this.picHimawari8Histrogram.Location = new System.Drawing.Point(22, 35);
            this.picHimawari8Histrogram.Name = "picHimawari8Histrogram";
            this.picHimawari8Histrogram.Size = new System.Drawing.Size(256, 120);
            this.picHimawari8Histrogram.TabIndex = 59;
            this.picHimawari8Histrogram.TabStop = false;
            this.picHimawari8Histrogram.Paint += new System.Windows.Forms.PaintEventHandler(this.picHimawari8Histrogram_Paint);
            this.picHimawari8Histrogram.MouseLeave += new System.EventHandler(this.picHimawari8Histrogram_MouseLeave);
            this.picHimawari8Histrogram.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHimawari8Histrogram_MouseMove);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 62;
            this.label6.Text = "High :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hScrollSatEnhenceLow
            // 
            this.hScrollSatEnhenceLow.LargeChange = 1;
            this.hScrollSatEnhenceLow.Location = new System.Drawing.Point(96, 171);
            this.hScrollSatEnhenceLow.Maximum = 255;
            this.hScrollSatEnhenceLow.Name = "hScrollSatEnhenceLow";
            this.hScrollSatEnhenceLow.Size = new System.Drawing.Size(182, 21);
            this.hScrollSatEnhenceLow.TabIndex = 60;
            this.hScrollSatEnhenceLow.ValueChanged += new System.EventHandler(this.hScrollSatEnhenceLow_ValueChanged);
            // 
            // tbSatEnhenceHigh
            // 
            this.tbSatEnhenceHigh.Location = new System.Drawing.Point(59, 212);
            this.tbSatEnhenceHigh.Name = "tbSatEnhenceHigh";
            this.tbSatEnhenceHigh.ReadOnly = true;
            this.tbSatEnhenceHigh.Size = new System.Drawing.Size(34, 21);
            this.tbSatEnhenceHigh.TabIndex = 64;
            this.tbSatEnhenceHigh.Text = "0";
            this.tbSatEnhenceHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnHiimawari8Equalize
            // 
            this.btnHiimawari8Equalize.Location = new System.Drawing.Point(101, 245);
            this.btnHiimawari8Equalize.Name = "btnHiimawari8Equalize";
            this.btnHiimawari8Equalize.Size = new System.Drawing.Size(98, 32);
            this.btnHiimawari8Equalize.TabIndex = 54;
            this.btnHiimawari8Equalize.Text = "Equalize";
            this.btnHiimawari8Equalize.UseVisualStyleBackColor = true;
            this.btnHiimawari8Equalize.Click += new System.EventHandler(this.btnHimawari8Equalize_Click);
            // 
            // hScrollSatEnhenceHigh
            // 
            this.hScrollSatEnhenceHigh.LargeChange = 1;
            this.hScrollSatEnhenceHigh.Location = new System.Drawing.Point(101, 212);
            this.hScrollSatEnhenceHigh.Maximum = 255;
            this.hScrollSatEnhenceHigh.Name = "hScrollSatEnhenceHigh";
            this.hScrollSatEnhenceHigh.Size = new System.Drawing.Size(182, 21);
            this.hScrollSatEnhenceHigh.TabIndex = 63;
            this.hScrollSatEnhenceHigh.Value = 255;
            this.hScrollSatEnhenceHigh.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollSatEnhenceHigh_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 48;
            this.label5.Text = "Low :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSatEnhenceLow
            // 
            this.tbSatEnhenceLow.Location = new System.Drawing.Point(54, 171);
            this.tbSatEnhenceLow.Name = "tbSatEnhenceLow";
            this.tbSatEnhenceLow.ReadOnly = true;
            this.tbSatEnhenceLow.Size = new System.Drawing.Size(34, 21);
            this.tbSatEnhenceLow.TabIndex = 61;
            this.tbSatEnhenceLow.Text = "0";
            this.tbSatEnhenceLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox3.Controls.Add(this.radioSatAreaTaiwan);
            this.groupBox3.Controls.Add(this.radioSatAreaAsia);
            this.groupBox3.Controls.Add(this.radioSatAreaGlobal);
            this.groupBox3.Location = new System.Drawing.Point(855, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 136);
            this.groupBox3.TabIndex = 58;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Area";
            // 
            // radioSatAreaTaiwan
            // 
            this.radioSatAreaTaiwan.AutoSize = true;
            this.radioSatAreaTaiwan.Checked = true;
            this.radioSatAreaTaiwan.Location = new System.Drawing.Point(17, 80);
            this.radioSatAreaTaiwan.Name = "radioSatAreaTaiwan";
            this.radioSatAreaTaiwan.Size = new System.Drawing.Size(64, 19);
            this.radioSatAreaTaiwan.TabIndex = 2;
            this.radioSatAreaTaiwan.TabStop = true;
            this.radioSatAreaTaiwan.Text = "Tawian";
            this.radioSatAreaTaiwan.UseVisualStyleBackColor = true;
            this.radioSatAreaTaiwan.CheckedChanged += new System.EventHandler(this.radioSatAreaTaiwan_CheckedChanged);
            // 
            // radioSatAreaAsia
            // 
            this.radioSatAreaAsia.AutoSize = true;
            this.radioSatAreaAsia.Location = new System.Drawing.Point(17, 53);
            this.radioSatAreaAsia.Name = "radioSatAreaAsia";
            this.radioSatAreaAsia.Size = new System.Drawing.Size(49, 19);
            this.radioSatAreaAsia.TabIndex = 1;
            this.radioSatAreaAsia.Text = "Asia";
            this.radioSatAreaAsia.UseVisualStyleBackColor = true;
            this.radioSatAreaAsia.CheckedChanged += new System.EventHandler(this.radioSatAreaAsia_CheckedChanged);
            // 
            // radioSatAreaGlobal
            // 
            this.radioSatAreaGlobal.AutoSize = true;
            this.radioSatAreaGlobal.Location = new System.Drawing.Point(17, 25);
            this.radioSatAreaGlobal.Name = "radioSatAreaGlobal";
            this.radioSatAreaGlobal.Size = new System.Drawing.Size(61, 19);
            this.radioSatAreaGlobal.TabIndex = 0;
            this.radioSatAreaGlobal.Text = "Global";
            this.radioSatAreaGlobal.UseVisualStyleBackColor = true;
            this.radioSatAreaGlobal.CheckedChanged += new System.EventHandler(this.radioSatAreaGlobal_CheckedChanged);
            // 
            // tbHimarwari8Enhence
            // 
            this.tbHimarwari8Enhence.Location = new System.Drawing.Point(904, 589);
            this.tbHimarwari8Enhence.Name = "tbHimarwari8Enhence";
            this.tbHimarwari8Enhence.ReadOnly = true;
            this.tbHimarwari8Enhence.Size = new System.Drawing.Size(31, 21);
            this.tbHimarwari8Enhence.TabIndex = 48;
            this.tbHimarwari8Enhence.Text = "1.0";
            this.tbHimarwari8Enhence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbHimarwari8Enhence.Visible = false;
            // 
            // hScrollHimarwari8Enhence
            // 
            this.hScrollHimarwari8Enhence.LargeChange = 1;
            this.hScrollHimarwari8Enhence.Location = new System.Drawing.Point(938, 589);
            this.hScrollHimarwari8Enhence.Maximum = 30;
            this.hScrollHimarwari8Enhence.Minimum = 10;
            this.hScrollHimarwari8Enhence.Name = "hScrollHimarwari8Enhence";
            this.hScrollHimarwari8Enhence.Size = new System.Drawing.Size(107, 21);
            this.hScrollHimarwari8Enhence.TabIndex = 48;
            this.hScrollHimarwari8Enhence.Value = 10;
            this.hScrollHimarwari8Enhence.Visible = false;
            this.hScrollHimarwari8Enhence.ValueChanged += new System.EventHandler(this.hScrollHimarwari8Enhence_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.radioSatImageTrueColor);
            this.groupBox2.Controls.Add(this.radioSatImageMono);
            this.groupBox2.Controls.Add(this.radioSatImageIrEnhence);
            this.groupBox2.Controls.Add(this.radioSatImageVisible);
            this.groupBox2.Controls.Add(this.radioSatImageIrColor);
            this.groupBox2.Location = new System.Drawing.Point(994, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 173);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // radioSatImageTrueColor
            // 
            this.radioSatImageTrueColor.AutoSize = true;
            this.radioSatImageTrueColor.Location = new System.Drawing.Point(14, 133);
            this.radioSatImageTrueColor.Name = "radioSatImageTrueColor";
            this.radioSatImageTrueColor.Size = new System.Drawing.Size(83, 19);
            this.radioSatImageTrueColor.TabIndex = 58;
            this.radioSatImageTrueColor.Text = "True Color";
            this.radioSatImageTrueColor.UseVisualStyleBackColor = true;
            this.radioSatImageTrueColor.CheckedChanged += new System.EventHandler(this.radioSatImageTrueColor_CheckedChanged);
            // 
            // radioSatImageMono
            // 
            this.radioSatImageMono.AutoSize = true;
            this.radioSatImageMono.Location = new System.Drawing.Point(14, 108);
            this.radioSatImageMono.Name = "radioSatImageMono";
            this.radioSatImageMono.Size = new System.Drawing.Size(55, 19);
            this.radioSatImageMono.TabIndex = 57;
            this.radioSatImageMono.Text = "Mono";
            this.radioSatImageMono.UseVisualStyleBackColor = true;
            this.radioSatImageMono.CheckedChanged += new System.EventHandler(this.radioSatImageMono_CheckedChanged);
            // 
            // radioSatImageIrEnhence
            // 
            this.radioSatImageIrEnhence.AutoSize = true;
            this.radioSatImageIrEnhence.Location = new System.Drawing.Point(14, 81);
            this.radioSatImageIrEnhence.Name = "radioSatImageIrEnhence";
            this.radioSatImageIrEnhence.Size = new System.Drawing.Size(89, 19);
            this.radioSatImageIrEnhence.TabIndex = 56;
            this.radioSatImageIrEnhence.Text = "IR Enhence";
            this.radioSatImageIrEnhence.UseVisualStyleBackColor = true;
            this.radioSatImageIrEnhence.CheckedChanged += new System.EventHandler(this.radioSatImageIrEnhence_CheckedChanged);
            // 
            // radioSatImageVisible
            // 
            this.radioSatImageVisible.AutoSize = true;
            this.radioSatImageVisible.Checked = true;
            this.radioSatImageVisible.Location = new System.Drawing.Point(14, 25);
            this.radioSatImageVisible.Name = "radioSatImageVisible";
            this.radioSatImageVisible.Size = new System.Drawing.Size(62, 19);
            this.radioSatImageVisible.TabIndex = 54;
            this.radioSatImageVisible.TabStop = true;
            this.radioSatImageVisible.Text = "Visible";
            this.radioSatImageVisible.UseVisualStyleBackColor = true;
            this.radioSatImageVisible.CheckedChanged += new System.EventHandler(this.radioSatImageVisible_CheckedChanged);
            // 
            // radioSatImageIrColor
            // 
            this.radioSatImageIrColor.AutoSize = true;
            this.radioSatImageIrColor.Location = new System.Drawing.Point(14, 52);
            this.radioSatImageIrColor.Name = "radioSatImageIrColor";
            this.radioSatImageIrColor.Size = new System.Drawing.Size(70, 19);
            this.radioSatImageIrColor.TabIndex = 55;
            this.radioSatImageIrColor.Text = "IR Color";
            this.radioSatImageIrColor.UseVisualStyleBackColor = true;
            this.radioSatImageIrColor.CheckedChanged += new System.EventHandler(this.radioSatImageIrColor_CheckedChanged);
            // 
            // btnHimawari8Rfresh
            // 
            this.btnHimawari8Rfresh.Location = new System.Drawing.Point(938, 224);
            this.btnHimawari8Rfresh.Name = "btnHimawari8Rfresh";
            this.btnHimawari8Rfresh.Size = new System.Drawing.Size(98, 32);
            this.btnHimawari8Rfresh.TabIndex = 48;
            this.btnHimawari8Rfresh.Text = "Refresh";
            this.btnHimawari8Rfresh.UseVisualStyleBackColor = true;
            this.btnHimawari8Rfresh.Click += new System.EventHandler(this.btnHimawari8Rfresh_Click);
            // 
            // picHimawari8
            // 
            this.picHimawari8.BackColor = System.Drawing.Color.Black;
            this.picHimawari8.Location = new System.Drawing.Point(16, 16);
            this.picHimawari8.Name = "picHimawari8";
            this.picHimawari8.Size = new System.Drawing.Size(800, 800);
            this.picHimawari8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHimawari8.TabIndex = 0;
            this.picHimawari8.TabStop = false;
            this.picHimawari8.MouseLeave += new System.EventHandler(this.picHimawari8_MouseLeave);
            this.picHimawari8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHimawari8_MouseMove);
            // 
            // tabPagePalert
            // 
            this.tabPagePalert.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPagePalert.Controls.Add(this.tbPalert);
            this.tabPagePalert.Location = new System.Drawing.Point(4, 22);
            this.tabPagePalert.Name = "tabPagePalert";
            this.tabPagePalert.Size = new System.Drawing.Size(1142, 839);
            this.tabPagePalert.TabIndex = 2;
            this.tabPagePalert.Text = "Palert";
            // 
            // tbPalert
            // 
            this.tbPalert.Font = new System.Drawing.Font("細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbPalert.Location = new System.Drawing.Point(8, 10);
            this.tbPalert.Multiline = true;
            this.tbPalert.Name = "tbPalert";
            this.tbPalert.ReadOnly = true;
            this.tbPalert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPalert.Size = new System.Drawing.Size(832, 803);
            this.tbPalert.TabIndex = 0;
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processingToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1420, 24);
            this.menuStripMain.TabIndex = 6;
            this.menuStripMain.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.downloadToolStripMenuItem.Text = "&Download RMT";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // processingToolStripMenuItem
            // 
            this.processingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.databaseToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.runPalertTaskToolStripMenuItem,
            this.systemStatusToolStripMenuItem,
            this.testToolStripMenuItem});
            this.processingToolStripMenuItem.Name = "processingToolStripMenuItem";
            this.processingToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.processingToolStripMenuItem.Text = "&Processing";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.stopToolStripMenuItem.Text = "S&top";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createRMTTablesToolStripMenuItem,
            this.createTemplateDirectoryToolStripMenuItem,
            this.alertDBTablesToolStripMenuItem,
            this.openRootPathToolStripMenuItem,
            this.openProjectPathToolStripMenuItem,
            this.openTemplatePathToolStripMenuItem,
            this.openDebugLogToolStripMenuItem,
            this.openConfigFileToolStripMenuItem});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.databaseToolStripMenuItem.Text = "Database/File";
            // 
            // createRMTTablesToolStripMenuItem
            // 
            this.createRMTTablesToolStripMenuItem.Enabled = false;
            this.createRMTTablesToolStripMenuItem.Name = "createRMTTablesToolStripMenuItem";
            this.createRMTTablesToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.createRMTTablesToolStripMenuItem.Text = "Create RMT DB Tables";
            this.createRMTTablesToolStripMenuItem.Visible = false;
            this.createRMTTablesToolStripMenuItem.Click += new System.EventHandler(this.createRMTTablesToolStripMenuItem_Click);
            // 
            // alertDBTablesToolStripMenuItem
            // 
            this.alertDBTablesToolStripMenuItem.Enabled = false;
            this.alertDBTablesToolStripMenuItem.Name = "alertDBTablesToolStripMenuItem";
            this.alertDBTablesToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.alertDBTablesToolStripMenuItem.Text = "Alert DB Tables";
            this.alertDBTablesToolStripMenuItem.Visible = false;
            this.alertDBTablesToolStripMenuItem.Click += new System.EventHandler(this.alertDBTablesToolStripMenuItem_Click);
            // 
            // createTemplateDirectoryToolStripMenuItem
            // 
            this.createTemplateDirectoryToolStripMenuItem.Enabled = false;
            this.createTemplateDirectoryToolStripMenuItem.Name = "createTemplateDirectoryToolStripMenuItem";
            this.createTemplateDirectoryToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.createTemplateDirectoryToolStripMenuItem.Text = "Create Template Paths";
            this.createTemplateDirectoryToolStripMenuItem.Visible = false;
            this.createTemplateDirectoryToolStripMenuItem.Click += new System.EventHandler(this.createTemplateDirectoryToolStripMenuItem_Click);
            // 
            // openTemplatePathToolStripMenuItem
            // 
            this.openTemplatePathToolStripMenuItem.Name = "openTemplatePathToolStripMenuItem";
            this.openTemplatePathToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openTemplatePathToolStripMenuItem.Text = "Open Template Path";
            this.openTemplatePathToolStripMenuItem.Click += new System.EventHandler(this.openTemplatePathToolStripMenuItem_Click);
            // 
            // openRootPathToolStripMenuItem
            // 
            this.openRootPathToolStripMenuItem.Name = "openRootPathToolStripMenuItem";
            this.openRootPathToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openRootPathToolStripMenuItem.Text = "Open Root Path";
            this.openRootPathToolStripMenuItem.Click += new System.EventHandler(this.openRootPathToolStripMenuItem_Click);
            // 
            // openDebugLogToolStripMenuItem
            // 
            this.openDebugLogToolStripMenuItem.Name = "openDebugLogToolStripMenuItem";
            this.openDebugLogToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openDebugLogToolStripMenuItem.Text = "Open Debug Log";
            this.openDebugLogToolStripMenuItem.Click += new System.EventHandler(this.openDebugLogToolStripMenuItem_Click);
            // 
            // openProjectPathToolStripMenuItem
            // 
            this.openProjectPathToolStripMenuItem.Name = "openProjectPathToolStripMenuItem";
            this.openProjectPathToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openProjectPathToolStripMenuItem.Text = "Open Project Path";
            this.openProjectPathToolStripMenuItem.Click += new System.EventHandler(this.openProjectPathToolStripMenuItem_Click);
            // 
            // openConfigFileToolStripMenuItem
            // 
            this.openConfigFileToolStripMenuItem.Name = "openConfigFileToolStripMenuItem";
            this.openConfigFileToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openConfigFileToolStripMenuItem.Text = "Open Config File";
            this.openConfigFileToolStripMenuItem.Click += new System.EventHandler(this.openConfigFileToolStripMenuItem_Click);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runRmtTaskProcessingCurrentDataToolStripMenuItem,
            this.rmtRevertSourceDataToolStripMenuItem,
            this.rmtUpdateErrorDataToolStripMenuItem,
            this.renameSourceDataToolStripMenuItem,
            this.searchErrorCopyImageToolStripMenuItem});
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            this.taskToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.taskToolStripMenuItem.Text = "Run RMT Task";
            // 
            // runRmtTaskProcessingCurrentDataToolStripMenuItem
            // 
            this.runRmtTaskProcessingCurrentDataToolStripMenuItem.Name = "runRmtTaskProcessingCurrentDataToolStripMenuItem";
            this.runRmtTaskProcessingCurrentDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.runRmtTaskProcessingCurrentDataToolStripMenuItem.Text = "Processing Current Data";
            this.runRmtTaskProcessingCurrentDataToolStripMenuItem.Click += new System.EventHandler(this.processingCurrentDataToolStripMenuItem_Click);
            // 
            // rmtRevertSourceDataToolStripMenuItem
            // 
            this.rmtRevertSourceDataToolStripMenuItem.Name = "rmtRevertSourceDataToolStripMenuItem";
            this.rmtRevertSourceDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.rmtRevertSourceDataToolStripMenuItem.Text = "Revert Source Data";
            this.rmtRevertSourceDataToolStripMenuItem.Click += new System.EventHandler(this.revertSourceDataToolStripMenuItem_Click);
            // 
            // rmtUpdateErrorDataToolStripMenuItem
            // 
            this.rmtUpdateErrorDataToolStripMenuItem.Name = "rmtUpdateErrorDataToolStripMenuItem";
            this.rmtUpdateErrorDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.rmtUpdateErrorDataToolStripMenuItem.Text = "Update Error Data";
            this.rmtUpdateErrorDataToolStripMenuItem.Click += new System.EventHandler(this.updateErrorDataToolStripMenuItem_Click);
            // 
            // renameSourceDataToolStripMenuItem
            // 
            this.renameSourceDataToolStripMenuItem.Enabled = false;
            this.renameSourceDataToolStripMenuItem.Name = "renameSourceDataToolStripMenuItem";
            this.renameSourceDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.renameSourceDataToolStripMenuItem.Text = "Rename Source Data";
            this.renameSourceDataToolStripMenuItem.Click += new System.EventHandler(this.renameSourceDataToolStripMenuItem_Click);
            // 
            // searchErrorCopyImageToolStripMenuItem
            // 
            this.searchErrorCopyImageToolStripMenuItem.Name = "searchErrorCopyImageToolStripMenuItem";
            this.searchErrorCopyImageToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.searchErrorCopyImageToolStripMenuItem.Text = "Search/Copy Error Data";
            this.searchErrorCopyImageToolStripMenuItem.Click += new System.EventHandler(this.searchErrorCopyImageToolStripMenuItem_Click);
            // 
            // runPalertTaskToolStripMenuItem
            // 
            this.runPalertTaskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.palertRevertSourceDataToolStripMenuItem1,
            this.renamePalertSourceDataToolStripMenuItem1});
            this.runPalertTaskToolStripMenuItem.Name = "runPalertTaskToolStripMenuItem";
            this.runPalertTaskToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.runPalertTaskToolStripMenuItem.Text = "Run Palert Task";
            // 
            // palertRevertSourceDataToolStripMenuItem1
            // 
            this.palertRevertSourceDataToolStripMenuItem1.Name = "palertRevertSourceDataToolStripMenuItem1";
            this.palertRevertSourceDataToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.palertRevertSourceDataToolStripMenuItem1.Text = "Revert Source Data";
            this.palertRevertSourceDataToolStripMenuItem1.Click += new System.EventHandler(this.palertRevertSourceDataToolStripMenuItem_Click);
            // 
            // renamePalertSourceDataToolStripMenuItem1
            // 
            this.renamePalertSourceDataToolStripMenuItem1.Name = "renamePalertSourceDataToolStripMenuItem1";
            this.renamePalertSourceDataToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.renamePalertSourceDataToolStripMenuItem1.Text = "Rename Source Data";
            this.renamePalertSourceDataToolStripMenuItem1.Click += new System.EventHandler(this.renamePalertSourceDataToolStripMenuItem_Click);
            // 
            // systemStatusToolStripMenuItem
            // 
            this.systemStatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFontsToolStripMenuItem});
            this.systemStatusToolStripMenuItem.Name = "systemStatusToolStripMenuItem";
            this.systemStatusToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.systemStatusToolStripMenuItem.Text = "System Status";
            // 
            // showFontsToolStripMenuItem
            // 
            this.showFontsToolStripMenuItem.Name = "showFontsToolStripMenuItem";
            this.showFontsToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Enabled = false;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAutoStart});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.optionToolStripMenuItem.Text = "O&ption";
            // 
            // menuAutoStart
            // 
            this.menuAutoStart.Checked = true;
            this.menuAutoStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuAutoStart.Name = "menuAutoStart";
            this.menuAutoStart.Size = new System.Drawing.Size(130, 22);
            this.menuAutoStart.Text = "Auto Start";
            this.menuAutoStart.Click += new System.EventHandler(this.menuAutoStart_Click);
            // 
            // tbStat
            // 
            this.tbStat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbStat.Location = new System.Drawing.Point(24, 25);
            this.tbStat.Multiline = true;
            this.tbStat.Name = "tbStat";
            this.tbStat.Size = new System.Drawing.Size(199, 206);
            this.tbStat.TabIndex = 9;
            // 
            // chkRmtWarnSound
            // 
            this.chkRmtWarnSound.AutoSize = true;
            this.chkRmtWarnSound.Checked = true;
            this.chkRmtWarnSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRmtWarnSound.Location = new System.Drawing.Point(6, 21);
            this.chkRmtWarnSound.Name = "chkRmtWarnSound";
            this.chkRmtWarnSound.Size = new System.Drawing.Size(122, 19);
            this.chkRmtWarnSound.TabIndex = 10;
            this.chkRmtWarnSound.Text = "RMT Warn Sound";
            this.chkRmtWarnSound.UseVisualStyleBackColor = true;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.BackColor = System.Drawing.SystemColors.Window;
            this.groupBoxSettings.Controls.Add(this.chkSaveHimawari8SourceImages);
            this.groupBoxSettings.Controls.Add(this.tbMwLimit);
            this.groupBoxSettings.Controls.Add(this.label1);
            this.groupBoxSettings.Controls.Add(this.hScrollMwLimit);
            this.groupBoxSettings.Controls.Add(this.tbMrLimit);
            this.groupBoxSettings.Controls.Add(this.label4);
            this.groupBoxSettings.Controls.Add(this.hScrollMrLimit);
            this.groupBoxSettings.Controls.Add(this.chkPalertQuakeSound);
            this.groupBoxSettings.Controls.Add(this.chkRmtMrSound);
            this.groupBoxSettings.Controls.Add(this.chkPalertForceSync);
            this.groupBoxSettings.Controls.Add(this.chkPalertSyncSound);
            this.groupBoxSettings.Controls.Add(this.chkShowOcrAOI);
            this.groupBoxSettings.Controls.Add(this.chkSaveRmtCsv);
            this.groupBoxSettings.Controls.Add(this.btnDefaultSetting);
            this.groupBoxSettings.Controls.Add(this.chkSaveRmtNanImage);
            this.groupBoxSettings.Controls.Add(this.tbNan);
            this.groupBoxSettings.Controls.Add(this.label3);
            this.groupBoxSettings.Controls.Add(this.hScrollNan);
            this.groupBoxSettings.Controls.Add(this.chkSaveRmtWarnImage);
            this.groupBoxSettings.Controls.Add(this.tbWarn);
            this.groupBoxSettings.Controls.Add(this.label2);
            this.groupBoxSettings.Controls.Add(this.hScrollWarn);
            this.groupBoxSettings.Controls.Add(this.chkRmtWarnSound);
            this.groupBoxSettings.Location = new System.Drawing.Point(1156, 73);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(256, 352);
            this.groupBoxSettings.TabIndex = 11;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // chkSaveHimawari8SourceImages
            // 
            this.chkSaveHimawari8SourceImages.AutoSize = true;
            this.chkSaveHimawari8SourceImages.Checked = true;
            this.chkSaveHimawari8SourceImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveHimawari8SourceImages.Location = new System.Drawing.Point(6, 165);
            this.chkSaveHimawari8SourceImages.Name = "chkSaveHimawari8SourceImages";
            this.chkSaveHimawari8SourceImages.Size = new System.Drawing.Size(203, 19);
            this.chkSaveHimawari8SourceImages.TabIndex = 63;
            this.chkSaveHimawari8SourceImages.Text = "Save Himawari8 Source Images";
            this.chkSaveHimawari8SourceImages.UseVisualStyleBackColor = true;
            // 
            // tbMwLimit
            // 
            this.tbMwLimit.Location = new System.Drawing.Point(85, 277);
            this.tbMwLimit.Name = "tbMwLimit";
            this.tbMwLimit.ReadOnly = true;
            this.tbMwLimit.Size = new System.Drawing.Size(38, 21);
            this.tbMwLimit.TabIndex = 61;
            this.tbMwLimit.Text = "3.5";
            this.tbMwLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 60;
            this.label1.Text = "Mw limit :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hScrollMwLimit
            // 
            this.hScrollMwLimit.LargeChange = 1;
            this.hScrollMwLimit.Location = new System.Drawing.Point(132, 277);
            this.hScrollMwLimit.Maximum = 50;
            this.hScrollMwLimit.Minimum = 25;
            this.hScrollMwLimit.Name = "hScrollMwLimit";
            this.hScrollMwLimit.Size = new System.Drawing.Size(88, 21);
            this.hScrollMwLimit.TabIndex = 62;
            this.hScrollMwLimit.Value = 30;
            this.hScrollMwLimit.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollMwLimit_Scroll);
            // 
            // tbMrLimit
            // 
            this.tbMrLimit.Location = new System.Drawing.Point(85, 249);
            this.tbMrLimit.Name = "tbMrLimit";
            this.tbMrLimit.ReadOnly = true;
            this.tbMrLimit.Size = new System.Drawing.Size(38, 21);
            this.tbMrLimit.TabIndex = 58;
            this.tbMrLimit.Text = "35";
            this.tbMrLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 57;
            this.label4.Text = "MR limit :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hScrollMrLimit
            // 
            this.hScrollMrLimit.LargeChange = 1;
            this.hScrollMrLimit.Location = new System.Drawing.Point(132, 249);
            this.hScrollMrLimit.Maximum = 70;
            this.hScrollMrLimit.Minimum = 25;
            this.hScrollMrLimit.Name = "hScrollMrLimit";
            this.hScrollMrLimit.Size = new System.Drawing.Size(88, 21);
            this.hScrollMrLimit.TabIndex = 59;
            this.hScrollMrLimit.Value = 35;
            this.hScrollMrLimit.ValueChanged += new System.EventHandler(this.hScrollMrLimit_ValueChanged);
            // 
            // chkPalertQuakeSound
            // 
            this.chkPalertQuakeSound.AutoSize = true;
            this.chkPalertQuakeSound.Checked = true;
            this.chkPalertQuakeSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPalertQuakeSound.Location = new System.Drawing.Point(7, 105);
            this.chkPalertQuakeSound.Name = "chkPalertQuakeSound";
            this.chkPalertQuakeSound.Size = new System.Drawing.Size(136, 19);
            this.chkPalertQuakeSound.TabIndex = 56;
            this.chkPalertQuakeSound.Text = "Palert Quake Sound";
            this.chkPalertQuakeSound.UseVisualStyleBackColor = true;
            // 
            // chkRmtMrSound
            // 
            this.chkRmtMrSound.AutoSize = true;
            this.chkRmtMrSound.Checked = true;
            this.chkRmtMrSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRmtMrSound.Location = new System.Drawing.Point(130, 21);
            this.chkRmtMrSound.Name = "chkRmtMrSound";
            this.chkRmtMrSound.Size = new System.Drawing.Size(114, 19);
            this.chkRmtMrSound.TabIndex = 55;
            this.chkRmtMrSound.Text = "RMT MR  Sound";
            this.chkRmtMrSound.UseVisualStyleBackColor = true;
            // 
            // chkPalertForceSync
            // 
            this.chkPalertForceSync.AutoSize = true;
            this.chkPalertForceSync.Checked = true;
            this.chkPalertForceSync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPalertForceSync.Location = new System.Drawing.Point(7, 140);
            this.chkPalertForceSync.Name = "chkPalertForceSync";
            this.chkPalertForceSync.Size = new System.Drawing.Size(121, 19);
            this.chkPalertForceSync.TabIndex = 53;
            this.chkPalertForceSync.Text = "Palert Force Sync";
            this.chkPalertForceSync.UseVisualStyleBackColor = true;
            // 
            // chkPalertSyncSound
            // 
            this.chkPalertSyncSound.AutoSize = true;
            this.chkPalertSyncSound.Checked = true;
            this.chkPalertSyncSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPalertSyncSound.Location = new System.Drawing.Point(7, 122);
            this.chkPalertSyncSound.Name = "chkPalertSyncSound";
            this.chkPalertSyncSound.Size = new System.Drawing.Size(126, 19);
            this.chkPalertSyncSound.TabIndex = 52;
            this.chkPalertSyncSound.Text = "Palert Sync Sound";
            this.chkPalertSyncSound.UseVisualStyleBackColor = true;
            // 
            // chkShowOcrAOI
            // 
            this.chkShowOcrAOI.AutoSize = true;
            this.chkShowOcrAOI.Checked = true;
            this.chkShowOcrAOI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowOcrAOI.Location = new System.Drawing.Point(7, 41);
            this.chkShowOcrAOI.Name = "chkShowOcrAOI";
            this.chkShowOcrAOI.Size = new System.Drawing.Size(108, 19);
            this.chkShowOcrAOI.TabIndex = 50;
            this.chkShowOcrAOI.Text = "Show OCR AOI";
            this.chkShowOcrAOI.UseVisualStyleBackColor = true;
            this.chkShowOcrAOI.CheckedChanged += new System.EventHandler(this.chkShowOcrAOI_CheckedChanged);
            // 
            // chkSaveRmtCsv
            // 
            this.chkSaveRmtCsv.AutoSize = true;
            this.chkSaveRmtCsv.Checked = true;
            this.chkSaveRmtCsv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveRmtCsv.Location = new System.Drawing.Point(131, 41);
            this.chkSaveRmtCsv.Name = "chkSaveRmtCsv";
            this.chkSaveRmtCsv.Size = new System.Drawing.Size(108, 19);
            this.chkSaveRmtCsv.TabIndex = 47;
            this.chkSaveRmtCsv.Text = "Save RMT CSV";
            this.chkSaveRmtCsv.UseVisualStyleBackColor = true;
            // 
            // btnDefaultSetting
            // 
            this.btnDefaultSetting.Location = new System.Drawing.Point(77, 312);
            this.btnDefaultSetting.Name = "btnDefaultSetting";
            this.btnDefaultSetting.Size = new System.Drawing.Size(119, 23);
            this.btnDefaultSetting.TabIndex = 41;
            this.btnDefaultSetting.Text = "Default Setting";
            this.btnDefaultSetting.UseVisualStyleBackColor = true;
            this.btnDefaultSetting.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // chkSaveRmtNanImage
            // 
            this.chkSaveRmtNanImage.AutoSize = true;
            this.chkSaveRmtNanImage.Checked = true;
            this.chkSaveRmtNanImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveRmtNanImage.Location = new System.Drawing.Point(7, 80);
            this.chkSaveRmtNanImage.Name = "chkSaveRmtNanImage";
            this.chkSaveRmtNanImage.Size = new System.Drawing.Size(147, 19);
            this.chkSaveRmtNanImage.TabIndex = 42;
            this.chkSaveRmtNanImage.Text = "Save RMT NaN Image";
            this.chkSaveRmtNanImage.UseVisualStyleBackColor = true;
            // 
            // tbNan
            // 
            this.tbNan.Location = new System.Drawing.Point(85, 219);
            this.tbNan.Name = "tbNan";
            this.tbNan.ReadOnly = true;
            this.tbNan.Size = new System.Drawing.Size(38, 21);
            this.tbNan.TabIndex = 39;
            this.tbNan.Text = "18";
            this.tbNan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Max NaN :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hScrollNan
            // 
            this.hScrollNan.LargeChange = 1;
            this.hScrollNan.Location = new System.Drawing.Point(132, 219);
            this.hScrollNan.Maximum = 54;
            this.hScrollNan.Minimum = 1;
            this.hScrollNan.Name = "hScrollNan";
            this.hScrollNan.Size = new System.Drawing.Size(88, 21);
            this.hScrollNan.TabIndex = 40;
            this.hScrollNan.Value = 18;
            this.hScrollNan.ValueChanged += new System.EventHandler(this.hScrollNan_ValueChanged);
            // 
            // chkSaveRmtWarnImage
            // 
            this.chkSaveRmtWarnImage.AutoSize = true;
            this.chkSaveRmtWarnImage.Checked = true;
            this.chkSaveRmtWarnImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveRmtWarnImage.Location = new System.Drawing.Point(7, 61);
            this.chkSaveRmtWarnImage.Name = "chkSaveRmtWarnImage";
            this.chkSaveRmtWarnImage.Size = new System.Drawing.Size(168, 19);
            this.chkSaveRmtWarnImage.TabIndex = 37;
            this.chkSaveRmtWarnImage.Text = "Save RMT Warning Image";
            this.chkSaveRmtWarnImage.UseVisualStyleBackColor = true;
            // 
            // tbWarn
            // 
            this.tbWarn.Location = new System.Drawing.Point(85, 191);
            this.tbWarn.Name = "tbWarn";
            this.tbWarn.ReadOnly = true;
            this.tbWarn.Size = new System.Drawing.Size(38, 21);
            this.tbWarn.TabIndex = 16;
            this.tbWarn.Text = "36";
            this.tbWarn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Max Warn :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hScrollWarn
            // 
            this.hScrollWarn.LargeChange = 1;
            this.hScrollWarn.Location = new System.Drawing.Point(132, 191);
            this.hScrollWarn.Maximum = 54;
            this.hScrollWarn.Minimum = 1;
            this.hScrollWarn.Name = "hScrollWarn";
            this.hScrollWarn.Size = new System.Drawing.Size(88, 21);
            this.hScrollWarn.TabIndex = 36;
            this.hScrollWarn.Value = 36;
            this.hScrollWarn.ValueChanged += new System.EventHandler(this.hScrollWarn_ValueChanged);
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // toolStripBtnStart
            // 
            this.toolStripBtnStart.AutoSize = false;
            this.toolStripBtnStart.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripBtnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripBtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnStart.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripBtnStart.ForeColor = System.Drawing.Color.White;
            this.toolStripBtnStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnStart.Image")));
            this.toolStripBtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnStart.Name = "toolStripBtnStart";
            this.toolStripBtnStart.Size = new System.Drawing.Size(48, 28);
            this.toolStripBtnStart.Text = "Start";
            this.toolStripBtnStart.Click += new System.EventHandler(this.toolStripBtnStart_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripBtnCamera
            // 
            this.toolStripBtnCamera.AutoSize = false;
            this.toolStripBtnCamera.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripBtnCamera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripBtnCamera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnCamera.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripBtnCamera.ForeColor = System.Drawing.Color.White;
            this.toolStripBtnCamera.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnCamera.Image")));
            this.toolStripBtnCamera.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnCamera.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnCamera.Name = "toolStripBtnCamera";
            this.toolStripBtnCamera.Size = new System.Drawing.Size(48, 28);
            this.toolStripBtnCamera.Text = "Camera";
            this.toolStripBtnCamera.Click += new System.EventHandler(this.toolStripBtnCamera_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStriptbRootPath
            // 
            this.toolStriptbRootPath.AutoSize = false;
            this.toolStriptbRootPath.BackColor = System.Drawing.SystemColors.Control;
            this.toolStriptbRootPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStriptbRootPath.Name = "toolStriptbRootPath";
            this.toolStriptbRootPath.ReadOnly = true;
            this.toolStriptbRootPath.Size = new System.Drawing.Size(300, 23);
            // 
            // toolStripBtnRootPath
            // 
            this.toolStripBtnRootPath.AutoSize = false;
            this.toolStripBtnRootPath.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripBtnRootPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnRootPath.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripBtnRootPath.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnRootPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnRootPath.Name = "toolStripBtnRootPath";
            this.toolStripBtnRootPath.Size = new System.Drawing.Size(24, 28);
            this.toolStripBtnRootPath.Text = "...";
            this.toolStripBtnRootPath.Click += new System.EventHandler(this.toolStripBtnRootPath_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnStart,
            this.toolstripBtnStop,
            this.toolStripSeparator2,
            this.toolStripBtnCamera,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.toolStriptbRootPath,
            this.toolStripBtnRootPath,
            this.toolStripSeparator4,
            this.toolStripBtnHome,
            this.toolStripBtnBack,
            this.toolStripBtnForward,
            this.toolStripBtnRefresh,
            this.toolStripSeparatorCWB});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1420, 31);
            this.toolStrip.TabIndex = 13;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolstripBtnStop
            // 
            this.toolstripBtnStop.AutoSize = false;
            this.toolstripBtnStop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolstripBtnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolstripBtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolstripBtnStop.Enabled = false;
            this.toolstripBtnStop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolstripBtnStop.Image = ((System.Drawing.Image)(resources.GetObject("toolstripBtnStop.Image")));
            this.toolstripBtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolstripBtnStop.Name = "toolstripBtnStop";
            this.toolstripBtnStop.Size = new System.Drawing.Size(48, 28);
            this.toolstripBtnStop.Text = "Stop";
            this.toolstripBtnStop.Click += new System.EventHandler(this.toolstripBtnStop_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 28);
            this.toolStripLabel1.Text = "Root Path :";
            // 
            // toolStripBtnHome
            // 
            this.toolStripBtnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnHome.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnHome.Image")));
            this.toolStripBtnHome.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnHome.Name = "toolStripBtnHome";
            this.toolStripBtnHome.Size = new System.Drawing.Size(28, 28);
            this.toolStripBtnHome.Text = "Home";
            this.toolStripBtnHome.Visible = false;
            this.toolStripBtnHome.Click += new System.EventHandler(this.toolStripBtnHome_Click);
            // 
            // toolStripBtnBack
            // 
            this.toolStripBtnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnBack.Image")));
            this.toolStripBtnBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnBack.Name = "toolStripBtnBack";
            this.toolStripBtnBack.Size = new System.Drawing.Size(28, 28);
            this.toolStripBtnBack.Text = "Back";
            this.toolStripBtnBack.Visible = false;
            this.toolStripBtnBack.Click += new System.EventHandler(this.toolStripBtnBack_Click);
            // 
            // toolStripBtnForward
            // 
            this.toolStripBtnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnForward.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnForward.Image")));
            this.toolStripBtnForward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnForward.Name = "toolStripBtnForward";
            this.toolStripBtnForward.Size = new System.Drawing.Size(28, 28);
            this.toolStripBtnForward.Text = "Forward";
            this.toolStripBtnForward.Visible = false;
            this.toolStripBtnForward.Click += new System.EventHandler(this.toolStripBtnForward_Click);
            // 
            // toolStripBtnRefresh
            // 
            this.toolStripBtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnRefresh.Image")));
            this.toolStripBtnRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripBtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnRefresh.Name = "toolStripBtnRefresh";
            this.toolStripBtnRefresh.Size = new System.Drawing.Size(28, 28);
            this.toolStripBtnRefresh.Text = "Refresh";
            this.toolStripBtnRefresh.Visible = false;
            this.toolStripBtnRefresh.Click += new System.EventHandler(this.toolStripBtnRefresh_Click);
            // 
            // toolStripSeparatorCWB
            // 
            this.toolStripSeparatorCWB.Name = "toolStripSeparatorCWB";
            this.toolStripSeparatorCWB.Size = new System.Drawing.Size(6, 31);
            this.toolStripSeparatorCWB.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.btnResetStatistic);
            this.groupBox1.Controls.Add(this.tbStat);
            this.groupBox1.Location = new System.Drawing.Point(1153, 431);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 272);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // btnResetStatistic
            // 
            this.btnResetStatistic.Location = new System.Drawing.Point(80, 237);
            this.btnResetStatistic.Name = "btnResetStatistic";
            this.btnResetStatistic.Size = new System.Drawing.Size(84, 23);
            this.btnResetStatistic.TabIndex = 46;
            this.btnResetStatistic.Text = "Reset";
            this.btnResetStatistic.UseVisualStyleBackColor = true;
            this.btnResetStatistic.Click += new System.EventHandler(this.btnResetStatistic_Click);
            // 
            // tbDebug
            // 
            this.tbDebug.Location = new System.Drawing.Point(1152, 709);
            this.tbDebug.Multiline = true;
            this.tbDebug.Name = "tbDebug";
            this.tbDebug.Size = new System.Drawing.Size(259, 120);
            this.tbDebug.TabIndex = 15;
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(1153, 835);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(259, 75);
            this.tbLog.TabIndex = 16;
            // 
            // timerPalert
            // 
            this.timerPalert.Interval = 3000;
            this.timerPalert.Tick += new System.EventHandler(this.timerPalert_Tick);
            // 
            // RMTProcessingMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1420, 941);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.tbDebug);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "RMTProcessingMainForm";
            this.Text = "RMT Processing v1.10";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RMTProcessingMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RMTProcessingMainForm_FormClosed);
            this.Load += new System.EventHandler(this.RMTProcessingMainForm_Load);
            this.Shown += new System.EventHandler(this.RMTProcessingMainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RMTProcessingMainForm_KeyDown);
            this.Resize += new System.EventHandler(this.RMTProcessingMainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picSource)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabPages.ResumeLayout(false);
            this.tabPageSourceImage.ResumeLayout(false);
            this.tabPageOutputImage.ResumeLayout(false);
            this.tabPageOutputImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).EndInit();
            this.tabPageCwbReport.ResumeLayout(false);
            this.tabPageHimawari8.ResumeLayout(false);
            this.tabPageHimawari8.PerformLayout();
            this.groupHimawari8Histrogram.ResumeLayout(false);
            this.groupHimawari8Histrogram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHimawari8Histrogram)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHimawari8)).EndInit();
            this.tabPagePalert.ResumeLayout(false);
            this.tabPagePalert.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox picSource;
        public System.Windows.Forms.PictureBox picOutput;
        private System.Windows.Forms.Timer timerRMT;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;
        private System.Windows.Forms.ToolStripStatusLabel statusMouse;
        private System.Windows.Forms.ToolStripStatusLabel statusColor;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAutoStart;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRMTTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runRmtTaskProcessingCurrentDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTemplateDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTemplatePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDebugLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFontsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusRmtCore;
        private System.Windows.Forms.ToolStripButton toolStripBtnStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripBtnCamera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox toolStriptbRootPath;
        private System.Windows.Forms.ToolStripButton toolStripBtnRootPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage tabPageSourceImage;
        private System.Windows.Forms.TabPage tabPageOutputImage;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.TextBox tbStat;
        private System.Windows.Forms.CheckBox chkRmtWarnSound;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.TextBox tbWarn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar hScrollWarn;
        private System.Windows.Forms.CheckBox chkSaveRmtWarnImage;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.TextBox tbNan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar hScrollNan;
        private System.Windows.Forms.Button btnDefaultSetting;
        private System.Windows.Forms.CheckBox chkSaveRmtNanImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnResetStatistic;
        private System.Windows.Forms.ToolStripButton toolstripBtnStop;
        private System.Windows.Forms.CheckBox chkSaveRmtCsv;
        private System.Windows.Forms.TabPage tabPageHimawari8;
        private System.Windows.Forms.PictureBox picHimawari8;
        private System.Windows.Forms.Button btnHimawari8Rfresh;
        private System.Windows.Forms.TextBox tbSiteInfo4;
        private System.Windows.Forms.TextBox tbSiteInfo3;
        private System.Windows.Forms.TextBox tbSiteInfo1;
        private System.Windows.Forms.TextBox tbSiteInfo2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioSatImageMono;
        private System.Windows.Forms.RadioButton radioSatImageIrEnhence;
        private System.Windows.Forms.RadioButton radioSatImageVisible;
        private System.Windows.Forms.RadioButton radioSatImageIrColor;
        private System.Windows.Forms.Button btnHiimawari8Equalize;
        private System.Windows.Forms.TextBox tbHimarwari8Enhence;
        private System.Windows.Forms.HScrollBar hScrollHimarwari8Enhence;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioSatAreaTaiwan;
        private System.Windows.Forms.RadioButton radioSatAreaAsia;
        private System.Windows.Forms.RadioButton radioSatAreaGlobal;
        private System.Windows.Forms.ToolStripStatusLabel statusPalert;
        private System.Windows.Forms.TabPage tabPageCwbReport;
        private System.Windows.Forms.WebBrowser webCWB;
        private System.Windows.Forms.ToolStripButton toolStripBtnRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCWB;
        private System.Windows.Forms.ToolStripButton toolStripBtnBack;
        private System.Windows.Forms.ToolStripButton toolStripBtnHome;
        private System.Windows.Forms.ToolStripButton toolStripBtnForward;
        private System.Windows.Forms.TextBox tbSiteInfo5;
        private System.Windows.Forms.TextBox tbSiteInfo6;
        private System.Windows.Forms.TabPage tabPagePalert;
        private System.Windows.Forms.PictureBox picHimawari8Histrogram;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSatEnhenceHigh;
        private System.Windows.Forms.HScrollBar hScrollSatEnhenceHigh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSatEnhenceLow;
        private System.Windows.Forms.HScrollBar hScrollSatEnhenceLow;
        private System.Windows.Forms.GroupBox groupHimawari8Histrogram;
        private System.Windows.Forms.TabPage tabPageAnalysis;
        private System.Windows.Forms.RadioButton radioSatImageTrueColor;
        private System.Windows.Forms.TextBox tbDebug;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.CheckBox chkShowOcrAOI;
        private System.Windows.Forms.ToolStripMenuItem openConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRootPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rmtRevertSourceDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameSourceDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchErrorCopyImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusThread;
        private System.Windows.Forms.ToolStripMenuItem rmtUpdateErrorDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alertDBTablesToolStripMenuItem;
        private System.Windows.Forms.TextBox tbPalert;
        private System.Windows.Forms.Timer timerPalert;
        private System.Windows.Forms.ToolStripStatusLabel statusHimawari8;
        private System.Windows.Forms.CheckBox chkPalertSyncSound;
        private System.Windows.Forms.CheckBox chkPalertForceSync;
        private System.Windows.Forms.ToolStripMenuItem runPalertTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem palertRevertSourceDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renamePalertSourceDataToolStripMenuItem1;
        private System.Windows.Forms.CheckBox chkPalertQuakeSound;
        private System.Windows.Forms.CheckBox chkRmtMrSound;
        private System.Windows.Forms.TextBox tbMwLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar hScrollMwLimit;
        private System.Windows.Forms.TextBox tbMrLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HScrollBar hScrollMrLimit;
        private System.Windows.Forms.CheckBox chkSaveHimawari8SourceImages;
        private System.Windows.Forms.ToolStripMenuItem openProjectPathToolStripMenuItem;
    }
}

