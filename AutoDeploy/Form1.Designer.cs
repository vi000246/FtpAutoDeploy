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
            this.lbProgress = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deploy伺服器設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.gbUploadList = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbFileList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbServerList = new System.Windows.Forms.ComboBox();
            this.cbIsBackUp = new System.Windows.Forms.CheckBox();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.lbDeployGroup = new System.Windows.Forms.CheckedListBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbUploadList.SuspendLayout();
            this.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(685, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deploy伺服器設定ToolStripMenuItem,
            this.testToolStripMenuItem1,
            this.test2ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem1.Text = "選單";
            // 
            // deploy伺服器設定ToolStripMenuItem
            // 
            this.deploy伺服器設定ToolStripMenuItem.Name = "deploy伺服器設定ToolStripMenuItem";
            this.deploy伺服器設定ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.deploy伺服器設定ToolStripMenuItem.Text = "Deploy伺服器設定";
            this.deploy伺服器設定ToolStripMenuItem.Click += new System.EventHandler(this.deploy伺服器設定ToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Enabled = false;
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.testToolStripMenuItem1.Text = "Deploy歷史紀錄";
            // 
            // test2ToolStripMenuItem
            // 
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            this.test2ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.test2ToolStripMenuItem.Text = "說明";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lbDeployGroup);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 458);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deploy群組";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 390);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(108, 390);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 10;
            this.btnCopy.Text = "複製";
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // gbUploadList
            // 
            this.gbUploadList.AutoSize = true;
            this.gbUploadList.Controls.Add(this.btnClear);
            this.gbUploadList.Controls.Add(this.lbFileList);
            this.gbUploadList.Controls.Add(this.label1);
            this.gbUploadList.Controls.Add(this.btnStart);
            this.gbUploadList.Controls.Add(this.cbServerList);
            this.gbUploadList.Controls.Add(this.cbIsBackUp);
            this.gbUploadList.Location = new System.Drawing.Point(216, 24);
            this.gbUploadList.Name = "gbUploadList";
            this.gbUploadList.Size = new System.Drawing.Size(469, 458);
            this.gbUploadList.TabIndex = 1;
            this.gbUploadList.TabStop = false;
            this.gbUploadList.Text = "上傳清單";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(295, 260);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清除所有";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lbFileList
            // 
            this.lbFileList.AllowDrop = true;
            this.lbFileList.FormattingEnabled = true;
            this.lbFileList.ItemHeight = 12;
            this.lbFileList.Location = new System.Drawing.Point(6, 21);
            this.lbFileList.Name = "lbFileList";
            this.lbFileList.Size = new System.Drawing.Size(457, 232);
            this.lbFileList.TabIndex = 0;
            this.lbFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbFileList_DragDrop);
            this.lbFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbFileList_DragEnter);
            this.lbFileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbFileList_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "選擇伺服器";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(388, 260);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 57);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "開始上傳";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbServerList
            // 
            this.cbServerList.FormattingEnabled = true;
            this.cbServerList.Location = new System.Drawing.Point(20, 275);
            this.cbServerList.Name = "cbServerList";
            this.cbServerList.Size = new System.Drawing.Size(132, 20);
            this.cbServerList.TabIndex = 2;
            // 
            // cbIsBackUp
            // 
            this.cbIsBackUp.AutoSize = true;
            this.cbIsBackUp.Enabled = false;
            this.cbIsBackUp.Location = new System.Drawing.Point(20, 301);
            this.cbIsBackUp.Name = "cbIsBackUp";
            this.cbIsBackUp.Size = new System.Drawing.Size(132, 16);
            this.cbIsBackUp.TabIndex = 5;
            this.cbIsBackUp.Text = "是否備份遠端原始檔";
            this.cbIsBackUp.UseVisualStyleBackColor = true;
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(230, 407);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(449, 52);
            this.lbLog.TabIndex = 7;
            // 
            // lbDeployGroup
            // 
            this.lbDeployGroup.FormattingEnabled = true;
            this.lbDeployGroup.Location = new System.Drawing.Point(6, 21);
            this.lbDeployGroup.Name = "lbDeployGroup";
            this.lbDeployGroup.Size = new System.Drawing.Size(188, 361);
            this.lbDeployGroup.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 482);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbUploadList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "FTP AutoDeploy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbUploadList.ResumeLayout(false);
            this.gbUploadList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem test2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deploy伺服器設定ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.GroupBox gbUploadList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lbFileList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cbServerList;
        private System.Windows.Forms.CheckBox cbIsBackUp;
        private System.Windows.Forms.CheckedListBox lbDeployGroup;
    }
}

