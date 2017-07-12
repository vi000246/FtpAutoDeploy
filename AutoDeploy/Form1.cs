using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDeploy
{
    public partial class Form1 : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public db db = new db();
        //用來儲存目前選擇的Deploy群組 在selected index change事件會變更此值
        public int lastDeployGroupIdSelected = 0;
        public file file = new file();

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
            if (lbDeployGroup.SelectedItem != null)
                lastDeployGroupIdSelected = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
            ShowConfigData();
        }

        // 啟動按鈕Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                SetControlEnable(false);
                if (lbFileList.Items.Count == 0)
                    throw new ArgumentException("上傳清單不得為空");

                if (cbServerList.SelectedItem == null)
                    throw new ArgumentException("請選擇一個伺服器群組");
                if(string.IsNullOrEmpty(tbFileRoot.Text))
                    throw new ArgumentException("請選擇檔案清單根目錄");
                //儲存目前的設置
                UpdateDeployConfig(lastDeployGroupIdSelected);

                ThreadPool.QueueUserWorkItem(state => UpdateProcess());

                //將紀錄存檔到txt
                logger.Trace(log.BuildDeployHistoryMessage(lastDeployGroupIdSelected));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetControlEnable(true);
            }
        }
        //用Thread呼叫更新邏輯
        public void UpdateProcess() {
            try
            {
                int serverGroupID = 0;
                List<string> files = new List<string>();
                //跨執行緒存取UI thread的control
                Invoke(new Action(() =>
                {
                    serverGroupID = (cbServerList.SelectedItem as model.FTP_M).ID;
                    files = ListBoxUtility.GetAllPath(lbFileList);
                }));
                
                //取得combobx選中的FTP群組的FTP列表
                List<model.FTP_D> FTPList = db.GetDataFromDBByCondition<model.FTP_D>(new { GroupID = serverGroupID });
                foreach (var FTPitem in FTPList)
                {
                    using (ftp ftp = new ftp(this, FTPitem.ClientIP, FTPitem.UserName, FTPitem.Password, Convert.ToInt32(FTPitem.Port)))
                    {
                        ftp.UploadFileToFtp(files, tbFileRoot.Text, tbFtpRoot.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally {
                //將UI控制項設為Enable
                SetControlEnable(true);
            }

        }
        //載入伺服器選單的combobox
        public void LoadServerCombobox() {
            try
            {
                cbServerList.DataSource = db.GetDataFromDB<model.FTP_M>();
                cbServerList.ValueMember = "ID";
                cbServerList.DisplayMember = "Name";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
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
                //如果還沒選擇檔案清單 就跳出 Browse Dialog讓使用者選
                if (string.IsNullOrEmpty(tbFileRoot.Text))
                {
                    //讓messageBox顯示在最上層
                    MessageBox.Show(new Form() { TopMost = true }, "請先選擇要Deploy的專案根路徑");
                    string path = dialog.BrowseFolder();
                    tbFileRoot.Text = path;
                }
                //如果沒選擇專案根目錄 就中止
                if (string.IsNullOrEmpty(tbFileRoot.Text))
                    return;

                // GetData() 回傳 string[]，內容為物件路徑，允許使用者一次拖曳多個物件
                string[] entriesPath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string entryPath in entriesPath)
                {
                    //判斷拖曳的檔案或資料夾 是否在根目錄內 (判斷是否為子目錄)
                    if (!file.IsSubfolder(tbFileRoot.Text, entryPath))
                    {
                        MessageBox.Show(new Form { TopMost = true }, @"此路徑:" + Environment.NewLine + entryPath + "\n不屬於檔案清單根目錄的路徑");
                    }
                    //如果Path已存在 就不要新增
                    else if (db.GetDataFromDBByCondition<model.Deploy_D>(new { Path = entryPath }).Count()>0) {
                        continue;
                    }
                    else
                    {
                        int groupid = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
                        //存檔進去的路徑會replace掉檔案清單根目錄
                        db.AddDeployDetail(new model.Deploy_D { GroupID = groupid, Path = entryPath });
                    }
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
                ClearCurrentFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearCurrentFileList() {
            CheckIsSelectDeployGroup();
            for (int i = lbFileList.Items.Count - 1; i >= 0; i--)
            {
                // do with listBox1.Items[i]
                db.DeleteDataFromDB<model.Deploy_D>((model.Deploy_D)lbFileList.Items[i]);
            }
            ShowDetailData();
        }

        //重整detail view的資料 依據選擇的Deploy群組
        private void ShowDetailData()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckIsSelectDeployGroup() {
            if (lbDeployGroup.SelectedItem == null)
            {
                throw new ArgumentException("請先選擇Deploy群組");
            }
        }

        #endregion
        #region =======================config控制項相關================================
        //瀏覽按鈕點擊事件
        private void btnBrowseRoot_Click(object sender, EventArgs e)
        {
            string path = dialog.BrowseFolder();
            //如果切換根目錄 則會清除所有的檔案清單
            if (!string.IsNullOrEmpty(path))
                ClearCurrentFileList();
            tbFileRoot.Text = path;
        }
        private void btnBrowseBackUp_Click(object sender, EventArgs e)
        {
            string path = dialog.BrowseFolder();
            tbBackUpPath.Text = path;
        }
        //伺服器目標目徑的驗證
        private void tbFtpRoot_Validating(object sender, CancelEventArgs e)
        {
            if (!Regex.IsMatch(tbFtpRoot.Text, @"^([\\/]?\w*[\\/]?)+$"))
            {
                MessageBox.Show(@"目標目徑的格式錯誤!
範例:
aaa、\aaa 、\aaa\ 、aaa\ 、\aaa\bbb、 aaa\bbb 、 aaa\bbb\ 、 \aaa\bbb\
/aaa 、 /aaa/ 、 aaa/ 、 /aaa/bbb 、 aaa/bbb 、 aaa/bbb/ 、 /aaa/bbb/
");
                tbFtpRoot.Text = "";
            }
        }

        //預覽按鈕點擊事件
        private void btnPreView_Click(object sender, EventArgs e)
        {
            bool isShow = true;
            List<string> paths = ListBoxUtility.GetAllPath(lbFileList);
            int TotalPath = paths.Count();
            if (TotalPath > 50000)
            {
                if (MessageBox.Show("檔案數過多 共"+ TotalPath + "筆 是否仍要預覽?", "關閉", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    isShow = false;
                }
            }
            if(isShow)
                dialog.ShowPreview(paths,tbFileRoot.Text,tbFtpRoot.Text);
        }

        #endregion
        #region ========================Deploy 群組相關===============================
        //新增Deploy群組
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = dialog.ShowInputGroupNameForm();
                if (!string.IsNullOrEmpty(name))
                {
                    int? newID = db.AddDeployGroup(name);
                }
                ShowGroupData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //依據group 自動切換Detail view
        private void lbDeployGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateDeployConfig(lastDeployGroupIdSelected);
                if (lbDeployGroup.SelectedItem != null)
                    lastDeployGroupIdSelected = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
                ShowDetailData();
                ShowConfigData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //修改名稱 loop checked的項目
        private void btnRename_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 只更新Deploy_M裡 和設置有關的欄位
        /// </summary>
        public void UpdateDeployConfig(int DeployGroupDd) {
            try
            {
                model.Deploy_M form = new model.Deploy_M();
                CheckIsSelectDeployGroup();
                int serverGroupID = 0;
                if (cbServerList.SelectedItem != null)
                    serverGroupID = (cbServerList.SelectedItem as model.FTP_M).ID;

                form.ID = DeployGroupDd;
                form.FtpGroup = serverGroupID;
                form.FileRootPath = tbFileRoot.Text;
                form.FtpTargetPath = tbFtpRoot.Text;
                form.BackUpPath = tbBackUpPath.Text;
                form.Memo = tbMemo.Text;
                db.UpdateDeployConfig(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //當切換群組時 讀取Deploy_M裡的config相關欄位並顯示到控制項中
        public void ShowConfigData() {
            try
            {
                if (lbDeployGroup.SelectedItem != null)
                {
                    int groupid = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
                    model.Deploy_M form = db.GetDataFromDB<model.Deploy_M>(groupid);

                    //依據config欄位 勾選Ftp Combobox的值
                    for (int i = 0; i < cbServerList.Items.Count; i++)
                    {

                        if ((cbServerList.Items[i] as model.FTP_M).ID == form.FtpGroup)
                        {
                            cbServerList.SelectedIndex = i;
                            break;
                        }
                    }
                    tbFileRoot.Text = form.FileRootPath;
                    tbFtpRoot.Text = form.FtpTargetPath;
                    tbBackUpPath.Text = form.BackUpPath;
                    tbMemo.Text = form.Memo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //取得Deploy群組資料  用來Refresh或一開始的init
        private void ShowGroupData()
        {
            try
            {
                lbDeployGroup.DataSource = db.GetDataFromDB<model.Deploy_M>();
                lbDeployGroup.DisplayMember = "Name";
                lbDeployGroup.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ==================Menu相關==========================
        //點擊Menu的伺服器設定時
        private void deploy伺服器設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form FormServer = new FormServer();
                //關閉Server設定時 重整combobox
                FormServer.FormClosing += (formSender, Forme) =>
                {
                    LoadServerCombobox();
                };
                FormServer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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


        //當視窗關閉時 將目前的設置存檔至Deploy_M
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                UpdateDeployConfig(lastDeployGroupIdSelected);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //用來將字串寫到log 的listBox
        public void LogToBox(string msg)
        {
            MethodInvoker updateLog = delegate
            {
                lbLog.Items.Add(msg);
                int visibleItems = lbLog.ClientSize.Height / lbLog.ItemHeight;
                lbLog.TopIndex = Math.Max(lbLog.Items.Count - visibleItems + 1, 0);
            };
            lbLog.BeginInvoke(updateLog);
        }

        //當上傳時 將控制項disable 結束時再enable
        private void SetControlEnable(bool enable) {
            //跨執行緒存取UI thread的control
            Invoke(new Action(() =>
            {
                btnStart.Enabled = enable;
                cbServerList.Enabled = enable;
                tbFtpRoot.Enabled = enable;
                btnBrowseRoot.Enabled = enable;
                btnBrowseBackUp.Enabled = enable;
                cbIsBackUp.Enabled = enable;
                tbMemo.Enabled = enable;
                btnClear.Enabled = enable;
            }));

        }


    }
}
