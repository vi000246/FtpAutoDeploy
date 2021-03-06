﻿using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public int TotalFileCount = 0;
        public int fileUploadCompletedCount = 0;
        private ContextMenuStrip listboxContextMenu;//檔案清單的右鍵選單

        public Form1()
        {
            InitializeComponent();
            LoadServerCombobox();//Combobox要先init出來 ShowConfigData()才能自動依據DB裡的值選擇伺服器群組
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
            toolTip.SetToolTip(cbIsBackUp, "若FTP/目標資料夾已存在相同檔案，將FTP/目標資料夾的檔案備份到指定目錄");
            toolTip.SetToolTip(lbBackUp, "請選擇備份檔放置的目錄");
            toolTip.SetToolTip(tbLocalRoot, "請選擇要複製的本機目的資料夾");
            toolTip.SetToolTip(cbUpdateHighLight, "只上傳選取反白的路徑，預設是上傳全部路徑");
            toolTip.SetToolTip(cbDeployToLocal, @"將檔案複製到本機的目標路徑,勾選此選項後，將不受檔案清單根目錄的限制，在清單中的檔案都能直接複製到目標資料夾，
但無法將整個目錄結構都一併複製，只能複製檔案"
                                                );
            toolTip.SetToolTip(btnBrowseRoot, "瀏覽");
            toolTip.SetToolTip(btnBrowseBackUp, "瀏覽");
            toolTip.SetToolTip(btnBrowsLocal, "瀏覽");
            toolTip.SetToolTip(btnFastChooseFileRoot, "選擇常用路徑");
            toolTip.SetToolTip(btnFastChooseBackUpRoot, "選擇常用路徑");
            toolTip.SetToolTip(btnFastChooseLocal, "選擇常用路徑");
            toolTip.SetToolTip(btnOpenFileRoot, "開啟檔案清單根目錄");
            toolTip.SetToolTip(btnOpenBackUpRoot, "開啟檔案備份目錄");
            toolTip.SetToolTip(btnOpenLocal, "開啟本地目標目錄");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowGroupData();
            ShowDetailData();
            ShowConfigData();
            if (lbDeployGroup.SelectedItem != null)
                lastDeployGroupIdSelected = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
            else
                SetControlEnable(false);
            

            //指派右鍵選單給檔案清單的listbox
            listboxContextMenu = new ContextMenuStrip();
            var item = listboxContextMenu.Items.Add("開啟檔案位置");
            item.Click += OpenSelectedItemPath;
            lbFileList.ContextMenuStrip = listboxContextMenu;
        }

        // 啟動按鈕Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                SetControlEnable(false);
                if (lbFileList.Items.Count == 0)
                    throw new ArgumentException("上傳清單不得為空");

                if (!cbDeployToLocal.Checked && 
                    (cbServerList.SelectedItem == null|| (cbServerList.SelectedItem as model.FTP_M).Name== "-------- 請選擇 --------"))
                    throw new ArgumentException("請選擇一個伺服器群組");
                if(!cbDeployToLocal.Checked && string.IsNullOrEmpty(tbFileRoot.Text))
                    throw new ArgumentException("請選擇檔案清單根目錄");
                if (cbIsBackUp.Checked && string.IsNullOrEmpty(tbBackUpPath.Text))
                    throw new ArgumentException("請輸入備份檔存放路徑");
                if(cbDeployToLocal.Checked && string.IsNullOrEmpty(tbLocalRoot.Text))
                    throw new ArgumentException("請選擇本地目標目錄");

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
                    var FTP_M = (cbServerList.SelectedItem as model.FTP_M);
                    serverGroupID = FTP_M == null ? serverGroupID : FTP_M.ID;
                    files = ListBoxUtility.GetAllPath(lbFileList, cbUpdateHighLight.Checked);
                }));

                List<model.FTP_D> FTPList = new List<model.FTP_D>();
                //計算總檔案數
                if (!cbDeployToLocal.Checked)
                {
                    //取得combobx選中的FTP群組的FTP列表
                    FTPList = db.GetDataFromDBByCondition<model.FTP_D>(new {GroupID = serverGroupID});
                    if (FTPList.Count() == 0)
                        throw new ArgumentException("此FTP群組內沒有設置FTP Server");
                    TotalFileCount = FTPList.Count() * files.Count();
                }
                else
                {
                    TotalFileCount = files.Count;
                }

                fileUploadCompletedCount = 0;//重設progress bar
                Invoke(new Action(() => { lbRestFile.Text = string.Format("檔案剩餘：{0}", TotalFileCount.ToString()); }));

                //依據checkbox，選擇deploy至FTP或是本地
                if (!cbDeployToLocal.Checked)
                {
                    foreach (var FTPitem in FTPList)
                    {
                        using (ftp ftp = new ftp(this, FTPitem.ClientIP, FTPitem.UserName, FTPitem.Password,
                            Convert.ToInt32(FTPitem.Port)))
                        {
                            ftp.UploadFileToFtp(files, tbFileRoot.Text, tbFtpRoot.Text, tbBackUpPath.Text,
                                cbIsBackUp.Checked, cbVersionNum.Checked);
                        }
                    }
                }
                else
                {
                    new deployToLocal(this).UploadFileToLocal(files, tbFileRoot.Text, tbLocalRoot.Text, tbBackUpPath.Text,
                        cbIsBackUp.Checked, cbVersionNum.Checked);
                }


                LogToBox("===================    所有檔案部署完成    ===================");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                var data = db.GetDataFromDB<model.FTP_M>();
                data.Insert(0, new model.FTP_M() {ID=-9999999,Name= "-------- 請選擇 --------" });
                cbServerList.DataSource = data;
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
            try
            {
                // 判斷物件是否可以拖曳進入控件，EX：垃圾桶無法拖曳進入控件，
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.All;
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // 檔案拖放
        private void lbFileList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                CheckIsSelectDeployGroup();
                //在FTP deploy模式中，如果還沒選擇檔案清單 就跳出 Browse Dialog讓使用者選
                if (!cbDeployToLocal.Checked && string.IsNullOrEmpty(tbFileRoot.Text))
                {
                    //讓messageBox顯示在最上層
                    MessageBox.Show(new Form() { TopMost = true }, "請先選擇要Deploy的專案根路徑");
                    string path = dialog.SelectFolderPath();
                    tbFileRoot.Text = path;
                }
                //在FTP deploy模式中，如果沒選擇專案根目錄 就中止
                if (!cbDeployToLocal.Checked && string.IsNullOrEmpty(tbFileRoot.Text))
                    return;

                // GetData() 回傳 string[]，內容為物件路徑，允許使用者一次拖曳多個物件
                string[] entriesPath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string entryPath in entriesPath)
                {
                    int groupid = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;

                    //在FTP deploy模式中，判斷拖曳的檔案或資料夾 是否在根目錄內 (判斷是否為子目錄)
                    if (!cbDeployToLocal.Checked && !file.IsSubfolder(tbFileRoot.Text, entryPath))
                    {
                        MessageBox.Show(new Form { TopMost = true }, @"此路徑:" + Environment.NewLine + entryPath + "\n不屬於檔案清單根目錄的路徑");
                    }
                    //如果Path已存在 就不要新增
                    else if (db.GetDataFromDBByCondition<model.Deploy_D>(new { Path = entryPath, GroupID= groupid }).Count()>0) {
                        continue;
                    }
                    else
                    {
                        //存檔進去的路徑會replace掉檔案清單根目錄
                        db.AddDeployDetail(new model.Deploy_D { GroupID = groupid, Path = entryPath });
                    }
                }
                ShowDetailData();
            }
            catch (Exception ex) {
                MessageBox.Show(new Form { TopMost = true},ex.Message);
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

        //按下右鍵時可開啟檔案目錄
        private void lbFileList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            lbFileList.ClearSelected();
            lbFileList.SelectedIndex = lbFileList.IndexFromPoint(e.X, e.Y);
            if (lbFileList.SelectedIndex != -1)
            {
                listboxContextMenu.Show();
            }
        }
        //右鍵選單的選項點擊事件 開啟指定的目錄
        private void OpenSelectedItemPath(object sender, EventArgs e)
        {
            try
            {
                string path = (lbFileList.SelectedItem as model.Deploy_D).Path.ToString();
                //如果是檔案路徑 就只取Directory 
                FileAttributes attr = File.GetAttributes(path);
                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                    path = Path.GetDirectoryName(path);
                if (!Directory.Exists(path))
                    MessageBox.Show("目錄不存在");
                else
                    Process.Start("explorer.exe", path);
            }
            catch (Exception ex) {
                MessageBox.Show("無法開啟檔案位置");
            }
        }
        #endregion
        #region =======================config控制項相關================================
        //瀏覽按鈕點擊事件
        private void btnBrowseRoot_Click(object sender, EventArgs e)
        {
            string path = dialog.SelectFolderPath();
            //如果切換根目錄 則會清除所有的檔案清單
            if (!string.IsNullOrEmpty(path))
            {
                ClearCurrentFileList();
                tbFileRoot.Text = path;
            }
        }
        private void btnBrowseBackUp_Click(object sender, EventArgs e)
        {
            string path = dialog.SelectFolderPath();
            if(!string.IsNullOrEmpty(path))
                tbBackUpPath.Text = path;
        }
        private void btnBrowsLocal_Click(object sender, EventArgs e)
        {
            string path = dialog.SelectFolderPath();
            if (!string.IsNullOrEmpty(path))
                tbLocalRoot.Text = path;
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
            List<string> paths = ListBoxUtility.GetAllPath(lbFileList,cbUpdateHighLight.Checked);
            int TotalPath = paths.Count();
            if (TotalPath > 50000)
            {
                if (MessageBox.Show("檔案數過多 共"+ TotalPath + "筆 是否仍要預覽?", "關閉", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    isShow = false;
                }
            }
            if(isShow)
                dialog.ShowPreview(paths,tbFileRoot.Text,tbFtpRoot.Text,cbDeployToLocal.Checked);
        }

        //上傳檔案根目錄 開啟常用選單
        private void btnFastChooseFileRoot_Click(object sender, EventArgs e)
        {
            string selectedPath = dialog.ShowFastChoose((int)FastChooseType.Project);
            if (!string.IsNullOrEmpty(selectedPath))
            {
                ClearCurrentFileList();
                tbFileRoot.Text = selectedPath;
            }

        }

        //備份檔案目錄 開啟常用選單
        private void btnFastChooseBackUpRoot_Click(object sender, EventArgs e)
        {
            string selectedPath = dialog.ShowFastChoose((int)FastChooseType.Backup);
            if (!string.IsNullOrEmpty(selectedPath))
            {
                if(!cbDeployToLocal.Checked)
                    ClearCurrentFileList();
                tbBackUpPath.Text = selectedPath;
            }
        }

        private void btnFastChooseLocal_Click(object sender, EventArgs e)
        {
            string selectedPath = dialog.ShowFastChoose((int)FastChooseType.Local);
            if (!string.IsNullOrEmpty(selectedPath))
            {
                if(!cbDeployToLocal.Checked)
                    ClearCurrentFileList();
                tbLocalRoot.Text = selectedPath;
            }
        }

        private void cbDeployToLocal_MouseDown(object sender, MouseEventArgs e)
        {
            ClearCurrentFileList();
        }
        //因為在cs設定Checked屬性時也會觸發，因此另外寫一個mouseDown事件
        private void cbDeployToLocal_CheckedChanged(object sender, EventArgs e)
        {
            SetControlEnable(true);
        }
        //更新progressBar進度
        public void updateProgressBar()
        {
            fileUploadCompletedCount += 1;
            int percent = (int)Math.Round((double)(100 * fileUploadCompletedCount) / TotalFileCount);
            MethodInvoker updateProgressBar = delegate
            {
                progressBar1.Value = percent;
            };
            progressBar1.BeginInvoke(updateProgressBar);

            MethodInvoker updateRestFile = delegate
            {
                lbRestFile.Text = "檔案剩餘：" + (TotalFileCount - fileUploadCompletedCount).ToString();
            };
            lbRestFile.BeginInvoke(updateRestFile);
        }
        //開啟目錄
        private void btnOpenFileRoot_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFileRoot.Text))
            {
                if(!Directory.Exists(tbFileRoot.Text))
                    MessageBox.Show("目錄不存在");
                else
                    Process.Start("explorer.exe", tbFileRoot.Text);
            }
            else
                MessageBox.Show("請先選擇檔案清單根目錄");

        }
        //開啟目錄
        private void btnOpenBackUpRoot_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbBackUpPath.Text))
            {
                if (!Directory.Exists(tbBackUpPath.Text))
                    MessageBox.Show("目錄不存在");
                else
                    Process.Start("explorer.exe", tbBackUpPath.Text);
            }
            else
                MessageBox.Show("請先選擇檔案備份根路徑");
        }
        //開啟目錄
        private void btnOpenLocal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLocalRoot.Text))
            {
                if (!Directory.Exists(tbLocalRoot.Text))
                    MessageBox.Show("目錄不存在");
                else
                    Process.Start("explorer.exe", tbLocalRoot.Text);
            }
            else
                MessageBox.Show("請先選擇本地目標目錄");
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
                    //新增出來的Deploy群組 FTP群組預設為-9999999  Name:"---- 請選擇 ----"
                    int newID = db.AddDeployGroupByName(new model.Deploy_M { Name=name,FtpGroup = -9999999 });
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
                SetControlEnable(true);
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
                if (lbDeployGroup.CheckedItems.Count == 0)
                {
                    MessageBox.Show("請至少勾選一個項目");
                    return;
                }
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
        //複製按鈕
        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                CheckIsSelectDeployGroup();
                int groupId = (lbDeployGroup.SelectedItem as model.Deploy_M).ID;
                UpdateDeployConfig(groupId);//複製前先更新目前的config
                //取得被複製的群組及子群組的資料
                var OriginGroupM = db.GetDataFromDB<model.Deploy_M>(groupId);
                var OriginGroupD = db.GetDataFromDBByCondition<model.Deploy_D>(new { GroupID = groupId });
                //新增一筆名稱一樣的deploy group 並取得id
                int newID = db.AddDeployGroupByName(OriginGroupM);

                //將原始DeployGroup_D的groupID設為新的群組ID
                var GroupD = OriginGroupD.Select(s=> { s.GroupID = Convert.ToInt32(newID);return s; }).ToList<model.Deploy_D>();
                //複製原始DeployGroup_D到新的群組Detail
                db.AddDeployDetail(GroupD);
                

                ShowGroupData();
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
                //如果Deploy群組裡面沒東西 跳過此方法
                if (lbDeployGroup.Items.Count == 0)
                    return;

                CheckIsSelectDeployGroup();

                int serverGroupID = 0;
                if (cbServerList.SelectedItem != null)
                    serverGroupID = (cbServerList.SelectedItem as model.FTP_M).ID;

                form.ID = DeployGroupDd;
                form.FtpGroup = serverGroupID;
                form.FileRootPath = tbFileRoot.Text;
                form.FtpTargetPath = tbFtpRoot.Text;
                form.BackUpPath = tbBackUpPath.Text;
                form.LocalPath = tbLocalRoot.Text;
                form.Memo = tbMemo.Text;
                form.IsBackup = cbIsBackUp.Checked;
                form.IsChangeVersionNum = cbVersionNum.Checked;
                form.IsUpdateHighLight = cbUpdateHighLight.Checked;
                form.IsDeployToLocal = cbDeployToLocal.Checked;
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
                    tbLocalRoot.Text = form.LocalPath;
                    cbIsBackUp.Checked = form.IsBackup;
                    cbVersionNum.Checked = form.IsChangeVersionNum;
                    cbUpdateHighLight.Checked = form.IsUpdateHighLight;
                    cbDeployToLocal.Checked = form.IsDeployToLocal;
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

                var data = db.GetDataFromDB<model.Deploy_M>();
                //將最新的排序在最上面
                data = data
                    .Where(x=>string.IsNullOrEmpty(tbSearchList.Text)||
                              x.Name.IndexOf(tbSearchList.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    .OrderByDescending(x => x.ID).ToList<model.Deploy_M>();
                lbDeployGroup.DataSource = data;
                lbDeployGroup.DisplayMember = "Name";
                lbDeployGroup.ValueMember = "ID";

                //如果Deploy群組沒東西 就將control設為disable
                SetControlEnable(!(lbDeployGroup.Items.Count == 0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //滑鼠移到item時 顯示deploy群組的全名tooltip
        public int hoveredIndex = 0;
        private void lbDeployGroup_MouseMove(object sender, MouseEventArgs e)
        {
            int newHoveredIndex = lbDeployGroup.IndexFromPoint(e.Location);

            if (hoveredIndex != newHoveredIndex)
            {
                hoveredIndex = newHoveredIndex;

                if (hoveredIndex > -1)
                {
                    toolTip.Active = false;
                    toolTip.SetToolTip(lbDeployGroup, ((model.Deploy_M)lbDeployGroup.Items[hoveredIndex]).Name);
                    toolTip.Active = true;
                }
            }
        }

        //搜尋按鈕
        private void btnSearchList_Click(object sender, EventArgs e)
        {
            ShowGroupData();
        }
        //搜尋欄按enter
        private void tbSearchList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ShowGroupData();
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
                SetCenter(FormServer);
                FormServer.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //menu 說明 click
        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorInfo form = new AuthorInfo();
            SetCenter(form);
            form.Show();
        }
        //按下log時 開啟Log資料夾
        private void log查詢ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs";
            if (Directory.Exists(path))
                Process.Start("explorer.exe", path);
            else
                MessageBox.Show("找不到Logs資料夾");

        }
        //常用清單click
        private void 常用清單設置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFastChoose form = new FormFastChoose();
            SetCenter(form);
            form.Show(this);
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

        //取得目前選取的Deploy群組名稱 供log使用
        public string GetDeployGroupName() {
            string name = string.Empty;
            Invoke(new Action(() =>
            {
                if (lbDeployGroup.SelectedItem != null)
                    name = (lbDeployGroup.SelectedItem as model.Deploy_M).Name;
            }));
            return name;
        }

        //當上傳時 將控制項disable 結束時再enable
        private void SetControlEnable(bool enable) {
            //跨執行緒存取UI thread的control
            Invoke(new Action(() =>
            {
                btnStart.Enabled = enable;
                cbServerList.Enabled = enable;
                cbVersionNum.Enabled = enable;
                tbFtpRoot.Enabled = enable;
                btnBrowseRoot.Enabled = enable;
                btnBrowseBackUp.Enabled = enable;
                btnBrowsLocal.Enabled = enable;
                cbIsBackUp.Enabled = enable;
                tbMemo.Enabled = enable;
                btnClear.Enabled = enable;
                cbUpdateHighLight.Enabled = enable;
                btnFastChooseFileRoot.Enabled = enable;
                btnFastChooseBackUpRoot.Enabled = enable;
                btnFastChooseLocal.Enabled = enable;
                btnOpenFileRoot.Enabled = enable;
                btnOpenBackUpRoot.Enabled = enable;
                btnOpenLocal.Enabled = enable;

                //如果是deploy到本地目錄，就disable FTP相關控制項,反之disable本地目錄相關控制項
                if (enable)
                {
                    if (cbDeployToLocal.Checked)
                    {
                        cbServerList.Enabled = false;
                        cbServerList.SelectedIndex = -1;
                        tbFtpRoot.Enabled = false;
                        tbFtpRoot.Clear();
                        tbFileRoot.Clear();
                        lbFileRoot.Font = new Font(lbFileRoot.Font, FontStyle.Strikeout);
                        lbFileRoot.ForeColor = Color.DimGray;
                        lbChooseServerGroup.Font = new Font(lbChooseServerGroup.Font, FontStyle.Strikeout);
                        lbChooseServerGroup.ForeColor = Color.DimGray;
                        lbFTPDirectory.Font = new Font(lbFTPDirectory.Font, FontStyle.Strikeout);
                        lbFTPDirectory.ForeColor = Color.DimGray;
                        lbLocalDirectory.Font = new Font(lbLocalDirectory.Font, FontStyle.Regular);
                        lbLocalDirectory.ForeColor = Color.Black;
                        btnBrowseRoot.Enabled = false;
                        btnOpenFileRoot.Enabled = false;
                        btnFastChooseFileRoot.Enabled = false;
                    }
                    else
                    {
                        tbLocalRoot.Enabled = false;
                        tbLocalRoot.Clear();
                        btnOpenLocal.Enabled = false;
                        btnBrowsLocal.Enabled = false;
                        btnFastChooseLocal.Enabled = false;
                        lbLocalDirectory.Font = new Font(lbLocalDirectory.Font, FontStyle.Strikeout);
                        lbLocalDirectory.ForeColor = Color.DimGray;
                        lbFileRoot.Font = new Font(lbFileRoot.Font,FontStyle.Regular);
                        lbFileRoot.ForeColor = Color.Black;
                        lbChooseServerGroup.Font = new Font(lbChooseServerGroup.Font, FontStyle.Regular);
                        lbChooseServerGroup.ForeColor = Color.Black;
                        lbFTPDirectory.Font = new Font(lbFTPDirectory.Font, FontStyle.Regular);
                        lbFTPDirectory.ForeColor = Color.Black;
                    }
                }

            }));

        }

        //將視窗設為置中
        private void SetCenter(Form form) {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(this.Location.X + (this.Width - form.Width) / 2, this.Location.Y + (this.Height - form.Height) / 2);
        }

    }
}
