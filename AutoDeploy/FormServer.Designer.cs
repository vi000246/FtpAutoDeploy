using System.Drawing;

namespace AutoDeploy
{
    partial class FormServer
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.lbGroup = new System.Windows.Forms.ListBox();
            this.fTPMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet = new AutoDeploy.dbDataSet();
            this.gridList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddList = new System.Windows.Forms.Button();
            this.fTP_MTableAdapter = new AutoDeploy.dbDataSetTableAdapters.FTP_MTableAdapter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.說明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fTPMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddGroup);
            this.groupBox1.Controls.Add(this.lbGroup);
            this.groupBox1.Location = new System.Drawing.Point(7, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 189);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FTP群組:";
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(282, 157);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroup.TabIndex = 2;
            this.btnAddGroup.Text = "新增";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // lbGroup
            // 
            this.lbGroup.DataBindings.Add(new System.Windows.Forms.Binding("SelectedIndex", this.fTPMBindingSource, "ID", true));
            this.lbGroup.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.fTPMBindingSource, "ID", true));
            this.lbGroup.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.fTPMBindingSource, "Name", true));
            this.lbGroup.DataSource = this.fTPMBindingSource;
            this.lbGroup.DisplayMember = "Name";
            this.lbGroup.FormattingEnabled = true;
            this.lbGroup.ItemHeight = 12;
            this.lbGroup.Location = new System.Drawing.Point(14, 27);
            this.lbGroup.Name = "lbGroup";
            this.lbGroup.Size = new System.Drawing.Size(343, 124);
            this.lbGroup.TabIndex = 1;
            this.lbGroup.SelectedIndexChanged += new System.EventHandler(this.lbGroup_SelectedIndexChanged);
            this.lbGroup.DoubleClick += new System.EventHandler(this.lbGroup_DoubleClick);
            this.lbGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbGroup_KeyDown);
            // 
            // fTPMBindingSource
            // 
            this.fTPMBindingSource.DataMember = "FTP_M";
            this.fTPMBindingSource.DataSource = this.dbDataSet;
            // 
            // dbDataSet
            // 
            this.dbDataSet.DataSetName = "dbDataSet";
            this.dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridList
            // 
            this.gridList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ClientIP,
            this.Port,
            this.UserName,
            this.Password});
            this.gridList.Location = new System.Drawing.Point(6, 21);
            this.gridList.MultiSelect = false;
            this.gridList.Name = "gridList";
            this.gridList.RowTemplate.Height = 24;
            this.gridList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridList.Size = new System.Drawing.Size(440, 135);
            this.gridList.TabIndex = 3;
            this.gridList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridList_CellEndEdit);
            this.gridList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridList_KeyDown);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // ClientIP
            // 
            this.ClientIP.DataPropertyName = "ClientIP";
            this.ClientIP.HeaderText = "IP";
            this.ClientIP.Name = "ClientIP";
            // 
            // Port
            // 
            this.Port.DataPropertyName = "Port";
            this.Port.HeaderText = "Port";
            this.Port.Name = "Port";
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "使用者名稱";
            this.UserName.Name = "UserName";
            // 
            // Password
            // 
            this.Password.DataPropertyName = "Password";
            this.Password.HeaderText = "密碼";
            this.Password.Name = "Password";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddList);
            this.groupBox2.Controls.Add(this.gridList);
            this.groupBox2.Location = new System.Drawing.Point(7, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 188);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FTP站台列表";
            // 
            // btnAddList
            // 
            this.btnAddList.Location = new System.Drawing.Point(371, 159);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new System.Drawing.Size(75, 23);
            this.btnAddList.TabIndex = 4;
            this.btnAddList.Text = "新增";
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
            // 
            // fTP_MTableAdapter
            // 
            this.fTP_MTableAdapter.ClearBeforeFill = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.說明ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(466, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 說明ToolStripMenuItem
            // 
            this.說明ToolStripMenuItem.BackColor = System.Drawing.Color.LightSteelBlue;
            this.說明ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.說明ToolStripMenuItem.Name = "說明ToolStripMenuItem";
            this.說明ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.說明ToolStripMenuItem.Text = "說明";
            this.說明ToolStripMenuItem.Click += new System.EventHandler(this.說明ToolStripMenuItem_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 425);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FTP列表";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fTPMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.ListBox lbGroup;
        private System.Windows.Forms.DataGridView gridList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddList;
        private dbDataSet dbDataSet;
        private System.Windows.Forms.BindingSource fTPMBindingSource;
        private dbDataSetTableAdapters.FTP_MTableAdapter fTP_MTableAdapter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 說明ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
    }
}