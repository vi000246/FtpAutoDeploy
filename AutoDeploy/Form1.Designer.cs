namespace AutoDeploy
{
    partial class Form1
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
            this.gbUploadList = new System.Windows.Forms.GroupBox();
            this.lbFileList = new System.Windows.Forms.ListBox();
            this.cbServerList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbIsBackUp = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.lbProgress = new System.Windows.Forms.Label();
            this.gbUploadList.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUploadList
            // 
            this.gbUploadList.Controls.Add(this.lbFileList);
            this.gbUploadList.Location = new System.Drawing.Point(224, 12);
            this.gbUploadList.Name = "gbUploadList";
            this.gbUploadList.Size = new System.Drawing.Size(469, 264);
            this.gbUploadList.TabIndex = 1;
            this.gbUploadList.TabStop = false;
            this.gbUploadList.Text = "上傳清單";
            // 
            // lbFileList
            // 
            this.lbFileList.FormattingEnabled = true;
            this.lbFileList.ItemHeight = 12;
            this.lbFileList.Location = new System.Drawing.Point(6, 21);
            this.lbFileList.Name = "lbFileList";
            this.lbFileList.Size = new System.Drawing.Size(457, 232);
            this.lbFileList.TabIndex = 0;
            // 
            // cbServerList
            // 
            this.cbServerList.FormattingEnabled = true;
            this.cbServerList.Location = new System.Drawing.Point(244, 308);
            this.cbServerList.Name = "cbServerList";
            this.cbServerList.Size = new System.Drawing.Size(132, 20);
            this.cbServerList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 293);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "選擇伺服器";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 446);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上傳群組";
            // 
            // cbIsBackUp
            // 
            this.cbIsBackUp.AutoSize = true;
            this.cbIsBackUp.Location = new System.Drawing.Point(244, 334);
            this.cbIsBackUp.Name = "cbIsBackUp";
            this.cbIsBackUp.Size = new System.Drawing.Size(132, 16);
            this.cbIsBackUp.TabIndex = 5;
            this.cbIsBackUp.Text = "是否備份遠端原始檔";
            this.cbIsBackUp.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(544, 293);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 57);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "開始上傳";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(230, 407);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(457, 52);
            this.lbLog.TabIndex = 7;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(230, 389);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(56, 12);
            this.lbProgress.TabIndex = 8;
            this.lbProgress.Text = "目前進度:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 465);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cbIsBackUp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbServerList);
            this.Controls.Add(this.gbUploadList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbUploadList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUploadList;
        private System.Windows.Forms.ListBox lbFileList;
        private System.Windows.Forms.ComboBox cbServerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbIsBackUp;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Label lbProgress;
    }
}

