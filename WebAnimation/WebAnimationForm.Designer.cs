namespace WebAnimation
{
    partial class WebAnimationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebAnimationForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRootPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStriptbRootPath = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripBtnRootPath = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnCamera = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.cbAnalysisItem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.tbMonth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDay2 = new System.Windows.Forms.TextBox();
            this.tbHour2 = new System.Windows.Forms.TextBox();
            this.tbMinute2 = new System.Windows.Forms.TextBox();
            this.btnAnalysis = new System.Windows.Forms.Button();
            this.btnSavePicture = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbMr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbMw = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.tbTimeTag = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbWarning = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDay1 = new System.Windows.Forms.TextBox();
            this.tbHour1 = new System.Windows.Forms.TextBox();
            this.tbMinute1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbTimeInterval = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCloudTyoe = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbOutputType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.picCloud = new System.Windows.Forms.PictureBox();
            this.timerSnap = new System.Windows.Forms.Timer(this.components);
            this.btnTest = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCloud)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStriptbRootPath,
            this.toolStripBtnRootPath,
            this.toolStripSeparator1,
            this.toolStripBtnCamera});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1884, 35);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRootPathToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 31);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openRootPathToolStripMenuItem
            // 
            this.openRootPathToolStripMenuItem.Name = "openRootPathToolStripMenuItem";
            this.openRootPathToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openRootPathToolStripMenuItem.Text = "Open Root Path";
            this.openRootPathToolStripMenuItem.Click += new System.EventHandler(this.openRootPathToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 28);
            this.toolStripLabel1.Text = "Root Path :";
            // 
            // toolStriptbRootPath
            // 
            this.toolStriptbRootPath.AutoSize = false;
            this.toolStriptbRootPath.BackColor = System.Drawing.SystemColors.Control;
            this.toolStriptbRootPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStriptbRootPath.Name = "toolStriptbRootPath";
            this.toolStriptbRootPath.ReadOnly = true;
            this.toolStriptbRootPath.Size = new System.Drawing.Size(400, 23);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip.Location = new System.Drawing.Point(0, 913);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1884, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(1869, 17);
            this.statusMessage.Spring = true;
            this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 49);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(787, 790);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // cbAnalysisItem
            // 
            this.cbAnalysisItem.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbAnalysisItem.FormattingEnabled = true;
            this.cbAnalysisItem.Items.AddRange(new object[] {
            "Mr最大強度圖",
            "Mw最大強度圖",
            "林志勇猜想分布圖",
            "樣本熱區圖",
            "Mw累積強度圖",
            "Mw*Mr累積強度圖"});
            this.cbAnalysisItem.Location = new System.Drawing.Point(110, 26);
            this.cbAnalysisItem.Name = "cbAnalysisItem";
            this.cbAnalysisItem.Size = new System.Drawing.Size(170, 24);
            this.cbAnalysisItem.TabIndex = 3;
            this.cbAnalysisItem.Text = "Mr最大強度圖";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(24, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "分析項目 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(171, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "年";
            // 
            // tbYear
            // 
            this.tbYear.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbYear.Location = new System.Drawing.Point(110, 30);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(55, 27);
            this.tbYear.TabIndex = 8;
            this.tbYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbMonth
            // 
            this.tbMonth.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbMonth.Location = new System.Drawing.Point(201, 30);
            this.tbMonth.Name = "tbMonth";
            this.tbMonth.Size = new System.Drawing.Size(41, 27);
            this.tbMonth.TabIndex = 10;
            this.tbMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(248, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "月";
            // 
            // tbDay2
            // 
            this.tbDay2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbDay2.Location = new System.Drawing.Point(110, 103);
            this.tbDay2.Name = "tbDay2";
            this.tbDay2.Size = new System.Drawing.Size(33, 27);
            this.tbDay2.TabIndex = 18;
            this.tbDay2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbHour2
            // 
            this.tbHour2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbHour2.Location = new System.Drawing.Point(171, 103);
            this.tbHour2.Name = "tbHour2";
            this.tbHour2.Size = new System.Drawing.Size(33, 27);
            this.tbHour2.TabIndex = 20;
            this.tbHour2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbMinute2
            // 
            this.tbMinute2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbMinute2.Location = new System.Drawing.Point(216, 103);
            this.tbMinute2.Name = "tbMinute2";
            this.tbMinute2.Size = new System.Drawing.Size(33, 27);
            this.tbMinute2.TabIndex = 22;
            this.tbMinute2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAnalysis
            // 
            this.btnAnalysis.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAnalysis.Location = new System.Drawing.Point(1521, 640);
            this.btnAnalysis.Name = "btnAnalysis";
            this.btnAnalysis.Size = new System.Drawing.Size(81, 41);
            this.btnAnalysis.TabIndex = 26;
            this.btnAnalysis.Text = "分析";
            this.btnAnalysis.UseVisualStyleBackColor = true;
            this.btnAnalysis.Click += new System.EventHandler(this.btnAnalysis_Click);
            // 
            // btnSavePicture
            // 
            this.btnSavePicture.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSavePicture.Location = new System.Drawing.Point(1608, 640);
            this.btnSavePicture.Name = "btnSavePicture";
            this.btnSavePicture.Size = new System.Drawing.Size(81, 41);
            this.btnSavePicture.TabIndex = 27;
            this.btnSavePicture.Text = "存檔";
            this.btnSavePicture.UseVisualStyleBackColor = true;
            this.btnSavePicture.Click += new System.EventHandler(this.btnSavePicture_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(70, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 16);
            this.label10.TabIndex = 29;
            this.label10.Text = "Mr :";
            // 
            // tbMr
            // 
            this.tbMr.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbMr.Location = new System.Drawing.Point(110, 98);
            this.tbMr.Name = "tbMr";
            this.tbMr.Size = new System.Drawing.Size(81, 27);
            this.tbMr.TabIndex = 28;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(64, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 31;
            this.label11.Text = "Mw :";
            // 
            // tbMw
            // 
            this.tbMw.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbMw.Location = new System.Drawing.Point(110, 131);
            this.tbMw.Name = "tbMw";
            this.tbMw.Size = new System.Drawing.Size(81, 27);
            this.tbMw.TabIndex = 30;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnNext.Location = new System.Drawing.Point(1695, 640);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(81, 41);
            this.btnNext.TabIndex = 34;
            this.btnNext.Text = "下一張";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAuto.Location = new System.Drawing.Point(1523, 748);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(253, 41);
            this.btnAuto.TabIndex = 36;
            this.btnAuto.Text = "自動處理";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(28, 32);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 16);
            this.label13.TabIndex = 38;
            this.label13.Text = "Time Tag :";
            // 
            // tbTimeTag
            // 
            this.tbTimeTag.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbTimeTag.Location = new System.Drawing.Point(110, 26);
            this.tbTimeTag.Name = "tbTimeTag";
            this.tbTimeTag.Size = new System.Drawing.Size(170, 27);
            this.tbTimeTag.TabIndex = 37;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(34, 164);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 16);
            this.label14.TabIndex = 40;
            this.label14.Text = "Warning :";
            // 
            // tbWarning
            // 
            this.tbWarning.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbWarning.Location = new System.Drawing.Point(110, 164);
            this.tbWarning.Name = "tbWarning";
            this.tbWarning.Size = new System.Drawing.Size(81, 27);
            this.tbWarning.TabIndex = 39;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.Location = new System.Drawing.Point(1608, 593);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(81, 41);
            this.btnStart.TabIndex = 41;
            this.btnStart.Text = "準備開始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(202, 106);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 16);
            this.label15.TabIndex = 42;
            this.label15.Text = " :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(144, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "日";
            // 
            // tbDay1
            // 
            this.tbDay1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbDay1.Location = new System.Drawing.Point(110, 70);
            this.tbDay1.Name = "tbDay1";
            this.tbDay1.Size = new System.Drawing.Size(33, 27);
            this.tbDay1.TabIndex = 12;
            this.tbDay1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbHour1
            // 
            this.tbHour1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbHour1.Location = new System.Drawing.Point(171, 70);
            this.tbHour1.Name = "tbHour1";
            this.tbHour1.Size = new System.Drawing.Size(33, 27);
            this.tbHour1.TabIndex = 14;
            this.tbHour1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbMinute1
            // 
            this.tbMinute1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbMinute1.Location = new System.Drawing.Point(216, 70);
            this.tbMinute1.Name = "tbMinute1";
            this.tbMinute1.Size = new System.Drawing.Size(33, 27);
            this.tbMinute1.TabIndex = 16;
            this.tbMinute1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(202, 73);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 16);
            this.label16.TabIndex = 43;
            this.label16.Text = " :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(145, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 44;
            this.label5.Text = "日";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(24, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 46;
            this.label7.Text = "開始時間 :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(24, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 47;
            this.label6.Text = "結束時間 :";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnReset.Location = new System.Drawing.Point(1578, 805);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(151, 41);
            this.btnReset.TabIndex = 35;
            this.btnReset.Text = "自動處理重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(24, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 49;
            this.label8.Text = "時間間隔 :";
            // 
            // cbTimeInterval
            // 
            this.cbTimeInterval.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbTimeInterval.FormattingEnabled = true;
            this.cbTimeInterval.Items.AddRange(new object[] {
            "10 Minutes",
            "15 Minutes",
            "20 Minutes",
            "30 Minutes",
            "1 Hour",
            "2 Hours"});
            this.cbTimeInterval.Location = new System.Drawing.Point(110, 136);
            this.cbTimeInterval.Name = "cbTimeInterval";
            this.cbTimeInterval.Size = new System.Drawing.Size(108, 24);
            this.cbTimeInterval.TabIndex = 48;
            this.cbTimeInterval.Text = "1 Hour";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(40, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 50;
            this.label9.Text = "資料庫 :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SeaShell;
            this.groupBox1.Controls.Add(this.cbTimeInterval);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbYear);
            this.groupBox1.Controls.Add(this.tbMonth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbDay1);
            this.groupBox1.Controls.Add(this.tbHour1);
            this.groupBox1.Controls.Add(this.tbMinute1);
            this.groupBox1.Controls.Add(this.tbDay2);
            this.groupBox1.Controls.Add(this.tbHour2);
            this.groupBox1.Controls.Add(this.tbMinute2);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(1496, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 172);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "時間設定";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.SeaShell;
            this.groupBox2.Controls.Add(this.cbCloudTyoe);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.tbWarning);
            this.groupBox2.Controls.Add(this.tbMr);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.tbMw);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbAnalysisItem);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(1496, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 210);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "參數設定";
            // 
            // cbCloudTyoe
            // 
            this.cbCloudTyoe.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbCloudTyoe.FormattingEnabled = true;
            this.cbCloudTyoe.Items.AddRange(new object[] {
            "可見光",
            "紅外線彩色",
            "紅外線黑白",
            "色調強化",
            "真實色彩",
            "雷達回波"});
            this.cbCloudTyoe.Location = new System.Drawing.Point(110, 56);
            this.cbCloudTyoe.Name = "cbCloudTyoe";
            this.cbCloudTyoe.Size = new System.Drawing.Size(170, 24);
            this.cbCloudTyoe.TabIndex = 41;
            this.cbCloudTyoe.Text = "紅外線彩色";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label18.Location = new System.Drawing.Point(24, 59);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 16);
            this.label18.TabIndex = 42;
            this.label18.Text = "雲圖種類 :";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.SeaShell;
            this.groupBox3.Controls.Add(this.cbOutputType);
            this.groupBox3.Controls.Add(this.tbTimeTag);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(1496, 460);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 92);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "輸出設定";
            // 
            // cbOutputType
            // 
            this.cbOutputType.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbOutputType.FormattingEnabled = true;
            this.cbOutputType.Items.AddRange(new object[] {
            "Web",
            "Web+Sat"});
            this.cbOutputType.Location = new System.Drawing.Point(110, 59);
            this.cbOutputType.Name = "cbOutputType";
            this.cbOutputType.Size = new System.Drawing.Size(108, 24);
            this.cbOutputType.TabIndex = 51;
            this.cbOutputType.Text = "Web+Sat";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(24, 62);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 16);
            this.label17.TabIndex = 52;
            this.label17.Text = "時間間隔 :";
            // 
            // picCloud
            // 
            this.picCloud.BackColor = System.Drawing.Color.Black;
            this.picCloud.Location = new System.Drawing.Point(678, 120);
            this.picCloud.Name = "picCloud";
            this.picCloud.Size = new System.Drawing.Size(760, 675);
            this.picCloud.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCloud.TabIndex = 54;
            this.picCloud.TabStop = false;
            // 
            // timerSnap
            // 
            this.timerSnap.Interval = 20000;
            this.timerSnap.Tick += new System.EventHandler(this.timerSnap_Tick);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTest.Location = new System.Drawing.Point(1608, 687);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(81, 41);
            this.btnTest.TabIndex = 55;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // WebAnimationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1884, 935);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.picCloud);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSavePicture);
            this.Controls.Add(this.btnAnalysis);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "WebAnimationForm";
            this.Text = "RMT Web Animation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WebAnimationForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCloud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ComboBox cbAnalysisItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.TextBox tbMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDay2;
        private System.Windows.Forms.TextBox tbHour2;
        private System.Windows.Forms.TextBox tbMinute2;
        private System.Windows.Forms.Button btnAnalysis;
        private System.Windows.Forms.Button btnSavePicture;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStriptbRootPath;
        private System.Windows.Forms.ToolStripButton toolStripBtnRootPath;
        private System.Windows.Forms.ToolStripButton toolStripBtnCamera;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbMr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbMw;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbTimeTag;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbWarning;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDay1;
        private System.Windows.Forms.TextBox tbHour1;
        private System.Windows.Forms.TextBox tbMinute1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbTimeInterval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRootPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox picCloud;
        private System.Windows.Forms.ComboBox cbOutputType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbCloudTyoe;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Timer timerSnap;
        private System.Windows.Forms.Button btnTest;
    }
}

