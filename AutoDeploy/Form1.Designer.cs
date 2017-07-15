using System;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbProgress = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deploy伺服器設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.log查詢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnCancelSelect = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lbDeployGroup = new System.Windows.Forms.CheckedListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.gbUploadList = new System.Windows.Forms.GroupBox();
            this.tbMemo = new System.Windows.Forms.TextBox();
            this.lbmemo = new System.Windows.Forms.Label();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.btnPreView = new System.Windows.Forms.Button();
            this.btnBrowseRoot = new System.Windows.Forms.Button();
            this.lbFileRoot = new System.Windows.Forms.Label();
            this.tbFileRoot = new System.Windows.Forms.TextBox();
            this.tbFtpRoot = new System.Windows.Forms.TextBox();
            this.lbFTPDirectory = new System.Windows.Forms.Label();
            this.btnBrowseBackUp = new System.Windows.Forms.Button();
            this.tbBackUpPath = new System.Windows.Forms.TextBox();
            this.lbBackUp = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lbFileList = new System.Windows.Forms.ListBox();
            this.lbChooseServerGroup = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cbServerList = new System.Windows.Forms.ComboBox();
            this.cbIsBackUp = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbUploadList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbProgress.Location = new System.Drawing.Point(10, 428);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(73, 19);
            this.lbProgress.TabIndex = 8;
            this.lbProgress.Text = "目前進度:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(742, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deploy伺服器設定ToolStripMenuItem,
            this.log查詢ToolStripMenuItem,
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
            // log查詢ToolStripMenuItem
            // 
            this.log查詢ToolStripMenuItem.Name = "log查詢ToolStripMenuItem";
            this.log查詢ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.log查詢ToolStripMenuItem.Text = "Log查詢";
            this.log查詢ToolStripMenuItem.Click += new System.EventHandler(this.log查詢ToolStripMenuItem_Click);
            // 
            // test2ToolStripMenuItem
            // 
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            this.test2ToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.test2ToolStripMenuItem.Text = "說明";
            this.test2ToolStripMenuItem.Click += new System.EventHandler(this.test2ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnRename);
            this.groupBox1.Controls.Add(this.btnCancelSelect);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.lbDeployGroup);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 515);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deploy群組";
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(10, 471);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 15;
            this.btnRename.Text = "重命名勾選";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnCancelSelect
            // 
            this.btnCancelSelect.Location = new System.Drawing.Point(112, 429);
            this.btnCancelSelect.Name = "btnCancelSelect";
            this.btnCancelSelect.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSelect.TabIndex = 14;
            this.btnCancelSelect.Text = "取消全選";
            this.btnCancelSelect.UseVisualStyleBackColor = true;
            this.btnCancelSelect.Click += new System.EventHandler(this.btnCancelSelect_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(112, 471);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "刪除勾選";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(10, 429);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 12;
            this.btnSelectAll.Text = "全選";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lbDeployGroup
            // 
            this.lbDeployGroup.FormattingEnabled = true;
            this.lbDeployGroup.Location = new System.Drawing.Point(6, 21);
            this.lbDeployGroup.Name = "lbDeployGroup";
            this.lbDeployGroup.Size = new System.Drawing.Size(188, 361);
            this.lbDeployGroup.TabIndex = 11;
            this.lbDeployGroup.SelectedIndexChanged += new System.EventHandler(this.lbDeployGroup_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 391);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(112, 391);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 10;
            this.btnCopy.Text = "複製";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // gbUploadList
            // 
            this.gbUploadList.AutoSize = true;
            this.gbUploadList.Controls.Add(this.progressBar1);
            this.gbUploadList.Controls.Add(this.tbMemo);
            this.gbUploadList.Controls.Add(this.lbmemo);
            this.gbUploadList.Controls.Add(this.lbLog);
            this.gbUploadList.Controls.Add(this.btnPreView);
            this.gbUploadList.Controls.Add(this.btnBrowseRoot);
            this.gbUploadList.Controls.Add(this.lbFileRoot);
            this.gbUploadList.Controls.Add(this.tbFileRoot);
            this.gbUploadList.Controls.Add(this.tbFtpRoot);
            this.gbUploadList.Controls.Add(this.lbFTPDirectory);
            this.gbUploadList.Controls.Add(this.btnBrowseBackUp);
            this.gbUploadList.Controls.Add(this.tbBackUpPath);
            this.gbUploadList.Controls.Add(this.lbBackUp);
            this.gbUploadList.Controls.Add(this.lbProgress);
            this.gbUploadList.Controls.Add(this.btnClear);
            this.gbUploadList.Controls.Add(this.lbFileList);
            this.gbUploadList.Controls.Add(this.lbChooseServerGroup);
            this.gbUploadList.Controls.Add(this.btnStart);
            this.gbUploadList.Controls.Add(this.cbServerList);
            this.gbUploadList.Controls.Add(this.cbIsBackUp);
            this.gbUploadList.Location = new System.Drawing.Point(216, 24);
            this.gbUploadList.Name = "gbUploadList";
            this.gbUploadList.Size = new System.Drawing.Size(526, 571);
            this.gbUploadList.TabIndex = 1;
            this.gbUploadList.TabStop = false;
            this.gbUploadList.Text = "上傳清單";
            // 
            // tbMemo
            // 
            this.tbMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.tbMemo.Location = new System.Drawing.Point(112, 352);
            this.tbMemo.Multiline = true;
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.Size = new System.Drawing.Size(266, 43);
            this.tbMemo.TabIndex = 19;
            // 
            // lbmemo
            // 
            this.lbmemo.AutoSize = true;
            this.lbmemo.Location = new System.Drawing.Point(17, 352);
            this.lbmemo.Name = "lbmemo";
            this.lbmemo.Size = new System.Drawing.Size(32, 12);
            this.lbmemo.TabIndex = 18;
            this.lbmemo.Text = "備註:";
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(14, 450);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(500, 100);
            this.lbLog.TabIndex = 7;
            // 
            // btnPreView
            // 
            this.btnPreView.Location = new System.Drawing.Point(277, 237);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(75, 39);
            this.btnPreView.TabIndex = 17;
            this.btnPreView.Text = "預覽";
            this.btnPreView.UseVisualStyleBackColor = true;
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // btnBrowseRoot
            // 
            this.btnBrowseRoot.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBrowseRoot.Location = new System.Drawing.Point(387, 292);
            this.btnBrowseRoot.Name = "btnBrowseRoot";
            this.btnBrowseRoot.Size = new System.Drawing.Size(46, 19);
            this.btnBrowseRoot.TabIndex = 16;
            this.btnBrowseRoot.Text = "瀏覽";
            this.btnBrowseRoot.UseVisualStyleBackColor = false;
            this.btnBrowseRoot.Click += new System.EventHandler(this.btnBrowseRoot_Click);
            // 
            // lbFileRoot
            // 
            this.lbFileRoot.AutoSize = true;
            this.lbFileRoot.Cursor = System.Windows.Forms.Cursors.Help;
            this.lbFileRoot.Location = new System.Drawing.Point(20, 296);
            this.lbFileRoot.Name = "lbFileRoot";
            this.lbFileRoot.Size = new System.Drawing.Size(89, 12);
            this.lbFileRoot.TabIndex = 15;
            this.lbFileRoot.Text = "檔案清單根目錄";
            // 
            // tbFileRoot
            // 
            this.tbFileRoot.Enabled = false;
            this.tbFileRoot.Location = new System.Drawing.Point(112, 292);
            this.tbFileRoot.Name = "tbFileRoot";
            this.tbFileRoot.Size = new System.Drawing.Size(266, 22);
            this.tbFileRoot.TabIndex = 14;
            // 
            // tbFtpRoot
            // 
            this.tbFtpRoot.Location = new System.Drawing.Point(113, 258);
            this.tbFtpRoot.Name = "tbFtpRoot";
            this.tbFtpRoot.Size = new System.Drawing.Size(132, 22);
            this.tbFtpRoot.TabIndex = 13;
            this.tbFtpRoot.Validating += new System.ComponentModel.CancelEventHandler(this.tbFtpRoot_Validating);
            // 
            // lbFTPDirectory
            // 
            this.lbFTPDirectory.AutoSize = true;
            this.lbFTPDirectory.Cursor = System.Windows.Forms.Cursors.Help;
            this.lbFTPDirectory.Location = new System.Drawing.Point(18, 264);
            this.lbFTPDirectory.Name = "lbFTPDirectory";
            this.lbFTPDirectory.Size = new System.Drawing.Size(75, 12);
            this.lbFTPDirectory.TabIndex = 12;
            this.lbFTPDirectory.Text = "FTP目標目錄:";
            // 
            // btnBrowseBackUp
            // 
            this.btnBrowseBackUp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBrowseBackUp.Location = new System.Drawing.Point(387, 320);
            this.btnBrowseBackUp.Name = "btnBrowseBackUp";
            this.btnBrowseBackUp.Size = new System.Drawing.Size(46, 19);
            this.btnBrowseBackUp.TabIndex = 11;
            this.btnBrowseBackUp.Text = "瀏覽";
            this.btnBrowseBackUp.UseVisualStyleBackColor = false;
            this.btnBrowseBackUp.Click += new System.EventHandler(this.btnBrowseBackUp_Click);
            // 
            // tbBackUpPath
            // 
            this.tbBackUpPath.Enabled = false;
            this.tbBackUpPath.Location = new System.Drawing.Point(112, 320);
            this.tbBackUpPath.Name = "tbBackUpPath";
            this.tbBackUpPath.Size = new System.Drawing.Size(267, 22);
            this.tbBackUpPath.TabIndex = 10;
            // 
            // lbBackUp
            // 
            this.lbBackUp.AutoSize = true;
            this.lbBackUp.Cursor = System.Windows.Forms.Cursors.Help;
            this.lbBackUp.Location = new System.Drawing.Point(17, 330);
            this.lbBackUp.Name = "lbBackUp";
            this.lbBackUp.Size = new System.Drawing.Size(92, 12);
            this.lbBackUp.TabIndex = 9;
            this.lbBackUp.Text = "備份原始檔路徑:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(358, 237);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 39);
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
            this.lbFileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFileList.Size = new System.Drawing.Size(508, 208);
            this.lbFileList.TabIndex = 0;
            this.lbFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbFileList_DragDrop);
            this.lbFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbFileList_DragEnter);
            this.lbFileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbFileList_KeyDown);
            // 
            // lbChooseServerGroup
            // 
            this.lbChooseServerGroup.AutoSize = true;
            this.lbChooseServerGroup.Cursor = System.Windows.Forms.Cursors.Help;
            this.lbChooseServerGroup.Location = new System.Drawing.Point(18, 235);
            this.lbChooseServerGroup.Name = "lbChooseServerGroup";
            this.lbChooseServerGroup.Size = new System.Drawing.Size(92, 12);
            this.lbChooseServerGroup.TabIndex = 3;
            this.lbChooseServerGroup.Text = "選擇伺服器群組:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(439, 237);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 73);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "開始上傳";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cbServerList
            // 
            this.cbServerList.FormattingEnabled = true;
            this.cbServerList.Location = new System.Drawing.Point(113, 232);
            this.cbServerList.Name = "cbServerList";
            this.cbServerList.Size = new System.Drawing.Size(132, 20);
            this.cbServerList.TabIndex = 2;
            // 
            // cbIsBackUp
            // 
            this.cbIsBackUp.AutoSize = true;
            this.cbIsBackUp.Cursor = System.Windows.Forms.Cursors.Help;
            this.cbIsBackUp.Location = new System.Drawing.Point(387, 354);
            this.cbIsBackUp.Name = "cbIsBackUp";
            this.cbIsBackUp.Size = new System.Drawing.Size(132, 16);
            this.cbIsBackUp.TabIndex = 5;
            this.cbIsBackUp.Text = "是否備份遠端原始檔";
            this.cbIsBackUp.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 30000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 402);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(500, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 582);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbUploadList);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "FTP AutoDeploy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem test2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deploy伺服器設定ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.GroupBox gbUploadList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lbFileList;
        private System.Windows.Forms.Label lbChooseServerGroup;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cbServerList;
        private System.Windows.Forms.CheckBox cbIsBackUp;
        private System.Windows.Forms.CheckedListBox lbDeployGroup;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnCancelSelect;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnBrowseBackUp;
        private System.Windows.Forms.TextBox tbBackUpPath;
        private System.Windows.Forms.Label lbBackUp;
        private System.Windows.Forms.Button btnBrowseRoot;
        private System.Windows.Forms.Label lbFileRoot;
        private System.Windows.Forms.TextBox tbFileRoot;
        private System.Windows.Forms.TextBox tbFtpRoot;
        private System.Windows.Forms.Label lbFTPDirectory;
        private System.Windows.Forms.Button btnPreView;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox tbMemo;
        private System.Windows.Forms.Label lbmemo;
        private System.Windows.Forms.ToolStripMenuItem log查詢ToolStripMenuItem;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

