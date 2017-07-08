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
    public partial class FormServer : Form
    {
        public db db = new db();

        public FormServer()
        {
            InitializeComponent();
            ShowGroupData();
            ShowDetailData();
        }
        #region FTP群組相關操作

        //新增群組按鈕點擊
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string name = dialog.ShowInputGroupNameForm();
            if (!string.IsNullOrEmpty(name)) {
                int? newID = db.AddFtpGroup(name);
            }
            ShowGroupData();
        }


        //按下delete鍵 刪除群組
        private void lbGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("確定刪除此FTP群組及底下的FTP列表?", "關閉", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    new ListBox().deleteListBoxItem<model.FTP_M>(lbGroup, db.DeleteDataFromDB<model.FTP_M>);
                    ShowGroupData();
                }

            }
        }
        //點擊兩下伺服器群組item 修改名稱
        private void lbGroup_DoubleClick(object sender, EventArgs e)
        {
            if (lbGroup.SelectedItem != null)
            {
                //取得修改後的名稱
                string name = dialog.ShowInputGroupNameForm("重新命名");
                if (!string.IsNullOrEmpty(name))
                {
                    model.FTP_M item = (lbGroup.SelectedItem as model.FTP_M);
                    item.Name = name;
                    db.UpdateDataToDB(item);
                }
            }
            ShowGroupData();
        }
        //當選擇伺服器群組時 refresh detail view
        private void lbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDetailData();
        }

        //取得伺服器群組資料  用來Refresh或一開始的init
        private void ShowGroupData()
        {
            lbGroup.DataSource = db.GetDataFromDB<model.FTP_M>();
            lbGroup.DisplayMember = "Name";
            lbGroup.ValueMember = "ID";
        }



        #endregion

        #region FTP列表相關
        //新增按鈕點擊
        private void btnAddList_Click(object sender, EventArgs e)
        {
            if (lbGroup.SelectedItem == null)
            {
                MessageBox.Show("請選擇一個伺服器群組!");
                return;
            }
            else
            {
                model.FTP_D form = dialog.ShowFtpDetailDialog();

                if (form != null)
                {
                    form.GroupID = (lbGroup.SelectedItem as model.FTP_M).ID;
                    db.AddFtpDetail(form);
                    ShowDetailData();
                }
            }
        }

        //重整detail view的資料 依據選擇的FTP群組
        private void ShowDetailData() {
            if (lbGroup.SelectedItem != null)
            {
                gridList.DataSource = db.
                    GetDataFromDBByCondition<model.FTP_D>(new { GroupID = (lbGroup.SelectedItem as model.FTP_M).ID });
            }
            else {
                gridList.DataSource = null;
            }
        }
        #endregion


    }
}
