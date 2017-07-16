using System.Drawing;

namespace AutoDeploy
{
    partial class FormFastChoose
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbProjectPath = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbBackupPath = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbProjectPath);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 203);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "常用上傳檔案清單根目錄";
            // 
            // lbProjectPath
            // 
            this.lbProjectPath.AllowDrop = true;
            this.lbProjectPath.FormattingEnabled = true;
            this.lbProjectPath.ItemHeight = 12;
            this.lbProjectPath.Location = new System.Drawing.Point(7, 22);
            this.lbProjectPath.Name = "lbProjectPath";
            this.lbProjectPath.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbProjectPath.Size = new System.Drawing.Size(506, 148);
            this.lbProjectPath.TabIndex = 1;
            this.lbProjectPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.List_DragDrop);
            this.lbProjectPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.List_DragEnter);
            this.lbProjectPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbProjectPath_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbBackupPath);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(12, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 203);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "常用備份檔儲存路徑";
            // 
            // lbBackupPath
            // 
            this.lbBackupPath.AllowDrop = true;
            this.lbBackupPath.FormattingEnabled = true;
            this.lbBackupPath.ItemHeight = 12;
            this.lbBackupPath.Location = new System.Drawing.Point(7, 20);
            this.lbBackupPath.Name = "lbBackupPath";
            this.lbBackupPath.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbBackupPath.Size = new System.Drawing.Size(506, 148);
            this.lbBackupPath.TabIndex = 2;
            this.lbBackupPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.List_DragDrop);
            this.lbBackupPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.List_DragEnter);
            this.lbBackupPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbBackupPath_KeyDown);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(437, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "新增";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormFastChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 458);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormFastChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "常用路徑設置";
            this.Load += new System.EventHandler(this.FormFastChoose_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lbProjectPath;
        private System.Windows.Forms.ListBox lbBackupPath;
    }
}