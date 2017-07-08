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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddList = new System.Windows.Forms.Button();
            this.fTP_MTableAdapter = new AutoDeploy.dbDataSetTableAdapters.FTP_MTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fTPMBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddGroup);
            this.groupBox1.Controls.Add(this.lbGroup);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 189);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FTP群組:";
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(273, 157);
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
            this.lbGroup.Size = new System.Drawing.Size(334, 124);
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
            this.gridList.Location = new System.Drawing.Point(23, 21);
            this.gridList.Name = "gridList";
            this.gridList.RowTemplate.Height = 24;
            this.gridList.Size = new System.Drawing.Size(343, 135);
            this.gridList.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddList);
            this.groupBox2.Controls.Add(this.gridList);
            this.groupBox2.Location = new System.Drawing.Point(3, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 188);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FTP列表";
            // 
            // btnAddList
            // 
            this.btnAddList.Location = new System.Drawing.Point(282, 159);
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
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 397);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormServer";
            this.Text = "FTP列表";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fTPMBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}