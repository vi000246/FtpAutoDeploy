﻿using NLog;
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
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public db db = new db();

        public Form1()
        {
            InitializeComponent();
            ShowGroupData();
            ShowDetailData();
            LoadServerCombobox();
            toolTip.SetToolTip(lbChooseServerGroup,"選擇FTP伺服器群組，會Deploy至此群組底下的FTP列表");
            toolTip.SetToolTip(lbFTPDirectory, @"輸入要上傳至FTP的目標目錄，有子目錄就用斜線分隔，開頭跟後面不要有斜線 
如果為空代表是FTP的根目錄
格式: someDirectory/childDirectory");
            toolTip.SetToolTip(lbFileRoot, @"請選擇本機要Deploy的專案的根目錄 
此根目錄後的路徑都會複製到FTP
Example: 
要傳的檔:C:/Projects/Build/DemoWebSite/Script/jquery.js
Deploy專案根目錄:C:/Projects/Build/DemoWebSite
則會將Script/jquery.js傳到FTP目標目錄");
            toolTip.SetToolTip(lbBackUp, "請選擇備份檔放置的目錄");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 啟動按鈕Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnStart.Enabled = false;
                if (lbFileList.Items.Count == 0)
                    throw new ArgumentException("上傳清單不得為空");

                if (cbServerList.SelectedItem != null)
                    throw new ArgumentException("請選擇一個伺服器群組");

                int serverGroupID = (cbServerList.SelectedItem as model.FTP_M).ID;
                //取得combobx選中的FTP群組的FTP列表
                List<model.FTP_D> FTPList = db.GetDataFromDBByCondition<model.FTP_D>(new { GroupID = serverGroupID });

                foreach (var FTPitem in FTPList)
                {
                    using (ftp ftp = new ftp(FTPitem.ClientIP, FTPitem.UserName, FTPitem.Password))
                    {
                        //取得檔案listBox中的所有檔案
                        foreach (model.Deploy_D item in lbFileList.Items)
                        {
                            List<string> files = new file().getAllFiles(item.Path);
                            ftp.UploadFileToFtp(files);
                        }
                        //ftp.testUpload();
                    }
                }
                btnStart.Enabled = true;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        //載入伺服器選單的combobox
        public void LoadServerCombobox() {
            cbServerList.DataSource = db.GetDataFromDB<model.FTP_M>();
            cbServerList.ValueMember = "ID";
            cbServerList.DisplayMember = "Name";
        }

        #region ========================檔案清單相關===============================
        //拖曳進入事件
        private void lbFileList_DragEnter(object sender, DragEventArgs e)
        {
            // 判斷物件是否可以拖曳進入控件，EX：垃圾桶無法拖曳進入控件，
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        // 檔案拖放
        private void lbFileList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                CheckIsSelectDeployGroup();
                // GetData() 回傳 string[]，內容為物件路徑，允許使用者一次拖曳多個物件
                string[] entriesPath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string entryPath in entriesPath)
                {
                    int groupid = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
                    db.AddDeployDetail(new model.Deploy_D {GroupID = groupid ,Path = entryPath});
                }
                ShowDetailData();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        //按下delete鍵 刪掉反白的路徑
        private void lbFileList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CheckIsSelectDeployGroup();
                if (e.KeyCode == Keys.Delete)
                {
                    //從DB刪除資料
                    new ListBoxUtility().LoopListBoxItem<model.Deploy_D>(lbFileList, db.DeleteDataFromDB);
                    ShowDetailData();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
}
        //清除所有檔案清單的路徑
        private void btnClear_Click(object sender, EventArgs e)
        {
            try { 
                CheckIsSelectDeployGroup();
                for (int i = lbFileList.Items.Count - 1; i >= 0; i--)
                {
                    // do with listBox1.Items[i]
                    db.DeleteDataFromDB<model.Deploy_D>((model.Deploy_D)lbFileList.Items[i]);
                }
                ShowDetailData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //重整detail view的資料 依據選擇的Deploy群組
        private void ShowDetailData()
        {
            if (lbDeployGroup.SelectedItem != null)
            {
                lbFileList.DataSource = db.
                    GetDataFromDBByCondition<model.Deploy_D>(new { GroupID = (lbDeployGroup.SelectedItem as model.Deploy_M).ID });
                lbFileList.DisplayMember = "Path";
                lbFileList.ValueMember = "Path";
            }
            else
            {
                lbFileList.DataSource = null;
            }
        }

        private void CheckIsSelectDeployGroup() {
            if (lbDeployGroup.SelectedItem == null)
            {
                throw new ArgumentException("請先選擇Deploy群組");
            }
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
        //依據group 自動切換Detail view
        private void lbDeployGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDetailData();
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
            //將所有項目unchecked
            ListBoxUtility.unCheckAll(lbDeployGroup);
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
            if (lbDeployGroup.CheckedItems.Count == 0)
            {
                MessageBox.Show("請至少勾選一個項目");
                return;
            }
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

        #region ==================Menu相關==========================
        //點擊Menu的伺服器設定時
        private void deploy伺服器設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormServer = new FormServer();
            //關閉Server設定時 重整combobox
            FormServer.FormClosing += (formSender, Forme) =>
            {
                LoadServerCombobox();
            };
            FormServer.Show();
        }

        //menu 說明 click
        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"
這是用來幫助快速部署網站的FTP上傳程式

作者   ：Yich Lin
Github：vi000246
信箱   ：vi000246@hotmail.com

","程式資訊");
        }
        #endregion


    }
}
