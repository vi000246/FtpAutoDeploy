using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDeploy
{
    public partial class FormFastChoose : Form
    {
        public db db = new db();
        public FormFastChoose()
        {
            InitializeComponent();
        }
        private void FormFastChoose_Load(object sender, EventArgs e)
        {
            RefreshProjectListBox();
            RefreshBackupListBox();
        }
        //常用檔案清單根目錄 新增按鈕點擊
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = dialog.BrowseFolder();
                if (!string.IsNullOrEmpty(path))
                {
                    if (db.GetDataFromDBByCondition<model.FastChoose>(new { Path = path, Type = (int)FastChooseType.Project }).Count() > 0)
                        throw new ArgumentException("此路徑己存在");

                    model.FastChoose item = new model.FastChoose() { Path = path, Type = (int)FastChooseType.Project };
                    db.AddFastChoosePath(item);
                    RefreshProjectListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //常用檔案清單根目錄 delete點擊
        private void lbProjectPath_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //從DB刪除資料
                    new ListBoxUtility().LoopListBoxItem<model.FastChoose>(lbProjectPath, db.DeleteDataFromDB);
                    RefreshProjectListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //常用備份檔根目錄 新增按鈕點擊 
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string path = dialog.BrowseFolder();
                if (!string.IsNullOrEmpty(path))
                {
                    if (db.GetDataFromDBByCondition<model.FastChoose>(new { Path = path, Type = (int)FastChooseType.Backup }).Count() > 0)
                        throw new ArgumentException("此路徑己存在");

                    model.FastChoose item = new model.FastChoose() { Path = path, Type = (int)FastChooseType.Backup };
                    db.AddFastChoosePath(item);
                    RefreshBackupListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //常用備份檔根目錄 delete點擊
        private void lbBackupPath_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //從DB刪除資料
                    new ListBoxUtility().LoopListBoxItem<model.FastChoose>(lbBackupPath, db.DeleteDataFromDB);
                    RefreshBackupListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //本地目標目錄 新增按鈕點擊 
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string path = dialog.BrowseFolder();
                if (!string.IsNullOrEmpty(path))
                {
                    if (db.GetDataFromDBByCondition<model.FastChoose>(new { Path = path, Type = (int)FastChooseType.Local }).Count() > 0)
                        throw new ArgumentException("此路徑己存在");

                    model.FastChoose item = new model.FastChoose() { Path = path, Type = (int)FastChooseType.Local };
                    db.AddFastChoosePath(item);
                    RefreshBackupListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //本地目標目錄 delete點擊
        private void lbLocalPath_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //從DB刪除資料
                    new ListBoxUtility().LoopListBoxItem<model.FastChoose>(lbLocalPath, db.DeleteDataFromDB);
                    RefreshBackupListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void List_DragEnter(object sender, DragEventArgs e)
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
        //拖曳folder 進入listBox事件
        private void List_DragDrop(object sender, DragEventArgs e)
        {
            int type = 0;
            switch ((sender as ListBox).Name)
            {
                case "lbProjectPath":
                    type = (int) FastChooseType.Project;
                    break;
                case "lbLocalPath":
                    type = (int)FastChooseType.Local;
                    break;
                case "lbBackupPath":
                    type = (int)FastChooseType.Backup;
                    break;
            }

            string[] entriesPath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string entryPath in entriesPath)
            {
                //如果path已存在 就跳過
                if (db.GetDataFromDBByCondition<model.FastChoose>(new { Path = entryPath, Type = type }).Count() > 0)
                    continue;
                model.FastChoose item = new model.FastChoose() { Path = entryPath, Type = type };
                db.AddFastChoosePath(item);
            }
            //重整listBox
            if (type == (int)FastChooseType.Project)
                RefreshProjectListBox();
            else if(type == (int)FastChooseType.Backup)
                RefreshBackupListBox();
            else
                RefreshLocalListBox();

        }

        //重新整理常用專案根目錄路徑
        private void RefreshProjectListBox() {
            lbProjectPath.DataSource = db.
                GetDataFromDBByCondition<model.FastChoose>(new { Type = (int)FastChooseType.Project });
            lbProjectPath.DisplayMember = "Path";
            lbProjectPath.ValueMember = "Path";
        }

        //重新整理常用備份目錄路徑
        private void RefreshBackupListBox() {
            lbBackupPath.DataSource = db.
                GetDataFromDBByCondition<model.FastChoose>(new { Type = (int)FastChooseType.Backup });
            lbBackupPath.DisplayMember = "Path";
            lbBackupPath.ValueMember = "Path";
        }
        //重新整理常用本地路徑
        private void RefreshLocalListBox()
        {
            lbLocalPath.DataSource = db.
                GetDataFromDBByCondition<model.FastChoose>(new { Type = (int)FastChooseType.Local });
            lbLocalPath.DisplayMember = "Path";
            lbLocalPath.ValueMember = "Path";
        }

    }
}
