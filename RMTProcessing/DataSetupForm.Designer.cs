namespace RMTProcessing
{
    partial class DataSetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileRoot = new System.Windows.Forms.TextBox();
            this.btnBtnRootPath = new System.Windows.Forms.Button();
            this.chkSameAsStartDate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAbort
            // 
            this.btnAbort.BackColor = System.Drawing.Color.Lime;
            this.btnAbort.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAbort.Location = new System.Drawing.Point(328, 234);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(102, 46);
            this.btnAbort.TabIndex = 68;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = false;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.Lime;
            this.btnConvert.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConvert.Location = new System.Drawing.Point(165, 234);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(102, 46);
            this.btnConvert.TabIndex = 67;
            this.btnConvert.Text = "Run";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(47, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "End Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(43, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 65;
            this.label2.Text = "Start Date :";
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.datePickerEnd.Location = new System.Drawing.Point(126, 167);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Size = new System.Drawing.Size(161, 27);
            this.datePickerEnd.TabIndex = 64;
            // 
            // datePickerStart
            // 
            this.datePickerStart.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.datePickerStart.Location = new System.Drawing.Point(126, 115);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.Size = new System.Drawing.Size(161, 27);
            this.datePickerStart.TabIndex = 63;
            this.datePickerStart.ValueChanged += new System.EventHandler(this.datePickerStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(78, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 69;
            this.label1.Text = "Path :";
            // 
            // tbFileRoot
            // 
            this.tbFileRoot.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbFileRoot.Location = new System.Drawing.Point(126, 72);
            this.tbFileRoot.Name = "tbFileRoot";
            this.tbFileRoot.ReadOnly = true;
            this.tbFileRoot.Size = new System.Drawing.Size(380, 27);
            this.tbFileRoot.TabIndex = 70;
            // 
            // btnBtnRootPath
            // 
            this.btnBtnRootPath.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnBtnRootPath.Location = new System.Drawing.Point(512, 75);
            this.btnBtnRootPath.Name = "btnBtnRootPath";
            this.btnBtnRootPath.Size = new System.Drawing.Size(33, 23);
            this.btnBtnRootPath.TabIndex = 71;
            this.btnBtnRootPath.Text = "...";
            this.btnBtnRootPath.UseVisualStyleBackColor = true;
            this.btnBtnRootPath.Click += new System.EventHandler(this.btnBtnRootPath_Click);
            // 
            // chkSameAsStartDate
            // 
            this.chkSameAsStartDate.AutoSize = true;
            this.chkSameAsStartDate.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkSameAsStartDate.Location = new System.Drawing.Point(312, 173);
            this.chkSameAsStartDate.Name = "chkSameAsStartDate";
            this.chkSameAsStartDate.Size = new System.Drawing.Size(147, 20);
            this.chkSameAsStartDate.TabIndex = 72;
            this.chkSameAsStartDate.Text = "Same As Start Date";
            this.chkSameAsStartDate.UseVisualStyleBackColor = true;
            this.chkSameAsStartDate.CheckedChanged += new System.EventHandler(this.chkSameAsStartDate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(69, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 76;
            this.label4.Text = "Mode :";
            // 
            // tbMode
            // 
            this.tbMode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMode.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbMode.Location = new System.Drawing.Point(131, 34);
            this.tbMode.Name = "tbMode";
            this.tbMode.ReadOnly = true;
            this.tbMode.Size = new System.Drawing.Size(347, 20);
            this.tbMode.TabIndex = 77;
            // 
            // DataSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 325);
            this.Controls.Add(this.tbMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkSameAsStartDate);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnBtnRootPath);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.tbFileRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.datePickerStart);
            this.Name = "DataSetupForm";
            this.Text = "Setup Data";
            this.Load += new System.EventHandler(this.RevertHistoryDataForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileRoot;
        private System.Windows.Forms.Button btnBtnRootPath;
        private System.Windows.Forms.CheckBox chkSameAsStartDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMode;
    }
}