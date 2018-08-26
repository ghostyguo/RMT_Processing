namespace VlfMagneticManager
{
    partial class VlfMagneticManagerForm
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
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.stopDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSaveFileDir = new System.Windows.Forms.TextBox();
            this.btnSaveDir = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSourceUrl = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(128, 67);
            this.startDate.Margin = new System.Windows.Forms.Padding(4);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(161, 27);
            this.startDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "開始日期 : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "結束日期 : ";
            // 
            // stopDate
            // 
            this.stopDate.Location = new System.Drawing.Point(430, 67);
            this.stopDate.Margin = new System.Windows.Forms.Padding(4);
            this.stopDate.Name = "stopDate";
            this.stopDate.Size = new System.Drawing.Size(161, 27);
            this.stopDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "下載項目 : ";
            // 
            // tbSaveFileDir
            // 
            this.tbSaveFileDir.Enabled = false;
            this.tbSaveFileDir.Location = new System.Drawing.Point(128, 168);
            this.tbSaveFileDir.Name = "tbSaveFileDir";
            this.tbSaveFileDir.Size = new System.Drawing.Size(463, 27);
            this.tbSaveFileDir.TabIndex = 5;
            this.tbSaveFileDir.Text = "E:\\RMT Processing\\ULF Magnetic";
            // 
            // btnSaveDir
            // 
            this.btnSaveDir.Location = new System.Drawing.Point(602, 168);
            this.btnSaveDir.Name = "btnSaveDir";
            this.btnSaveDir.Size = new System.Drawing.Size(35, 27);
            this.btnSaveDir.TabIndex = 6;
            this.btnSaveDir.Text = "...";
            this.btnSaveDir.UseVisualStyleBackColor = true;
            this.btnSaveDir.Click += new System.EventHandler(this.btnSaveDir_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(259, 222);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(135, 55);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "開始下載";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 306);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(670, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip";
            // 
            // statusMessage
            // 
            this.statusMessage.AutoSize = false;
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(875, 17);
            this.statusMessage.Spring = true;
            this.statusMessage.Text = "Start";
            this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "下載目錄 : ";
            // 
            // cbSourceUrl
            // 
            this.cbSourceUrl.FormattingEnabled = true;
            this.cbSourceUrl.Location = new System.Drawing.Point(128, 122);
            this.cbSourceUrl.Name = "cbSourceUrl";
            this.cbSourceUrl.Size = new System.Drawing.Size(509, 24);
            this.cbSourceUrl.TabIndex = 10;
            this.cbSourceUrl.SelectedIndexChanged += new System.EventHandler(this.cbSourceUrl_SelectedIndexChanged);
            // 
            // VlfMagneticManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 328);
            this.Controls.Add(this.cbSourceUrl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnSaveDir);
            this.Controls.Add(this.tbSaveFileDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stopDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startDate);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VlfMagneticManagerForm";
            this.Text = "中央氣象局超低頻地磁相關係數等值圖管理員";
            this.Load += new System.EventHandler(this.VlfMagneticManagerForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker stopDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSaveFileDir;
        private System.Windows.Forms.Button btnSaveDir;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSourceUrl;
    }
}

