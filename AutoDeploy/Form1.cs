using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDeploy
{
    public partial class Form1 : Form
    {
        public db db = new db();

        public Form1()
        {
            InitializeComponent();
            ShowGroupData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 啟動按鈕Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            //取得檔案listBox中的所有檔案
            foreach (string path in lbFileList.Items)
            {
                var files = new file().getAllFiles(path);
            }
            
            
        }

        #region ========================檔案清單相關===============================
        // 檔案拖放
        private void lbFileList_DragDrop(object sender, DragEventArgs e)
        {
            // GetData() 回傳 string[]，內容為物件路徑，允許使用者一次拖曳多個物件
            string[] entriesPath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string entryPath in entriesPath)
            {
                lbFileList.Items.Add(entryPath);
            }
        }
        //拖曳進入事件
        private void lbFileList_DragEnter(object sender, DragEventArgs e)
        {
            // 判斷物件是否可以拖曳進入控件，EX：垃圾桶無法拖曳進入控件，
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        //按下delete鍵 刪掉反白的路徑
        private void lbFileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //從DB刪除資料
                //new ListBox().deleteListBoxItem(lbFileList);
            }
        }
        //清除所有檔案清單的路徑
        private void btnClear_Click(object sender, EventArgs e)
        {
            lbFileList.Items.Clear();
        }



        #endregion
        #region ========================Deploy 群組相關===============================
        //新增Deploy群組
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = dialog.ShowInputGroupNameForm();
            if (!string.IsNullOrEmpty(name))
            {
                int? newID = db.AddDeployGroup(name);
            }
            ShowGroupData();
        }


        //全選按鈕
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < lbDeployGroup.Items.Count; i++)
                lbDeployGroup.SetItemChecked(i, true);
        }
        //取消全選按鈕
        private void btnCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbDeployGroup.Items.Count; i++)
                lbDeployGroup.SetItemChecked(i, false);
        }
        //刪除勾選項目
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定刪除選中的Deploy群組及底下的上傳清單?", "關閉", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new ListBoxUtility().LoopListBoxItem<model.Deploy_M>(lbDeployGroup, db.DeleteDeployMasterAndDetail);
                //重新整理master和detail
                ShowGroupData();
                //將所有項目unchecked
                ListBoxUtility.unCheckAll(lbDeployGroup);
            }
        }
        //修改名稱 loop checked的項目
        private void btnRename_Click(object sender, EventArgs e)
        {
            new ListBoxUtility().LoopListBoxItem<model.Deploy_M>(lbDeployGroup, ListBoxUtility.RenameDeployGroupItem);
            ShowGroupData();
            //將所有項目unchecked
            ListBoxUtility.unCheckAll(lbDeployGroup);
        }
        //取得Deploy群組資料  用來Refresh或一開始的init
        private void ShowGroupData()
        {
            lbDeployGroup.DataSource = db.GetDataFromDB<model.Deploy_M>();
            lbDeployGroup.DisplayMember = "Name";
            lbDeployGroup.ValueMember = "ID";
        }
        #endregion

        //點擊Menu的伺服器設定時
        private void deploy伺服器設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormServer = new FormServer();
            FormServer.Show();
        }


    }
}
