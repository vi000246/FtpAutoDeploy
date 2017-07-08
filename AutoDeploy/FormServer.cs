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
        }
        #region FTP群組相關操作

        //新增群組按鈕點擊
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string name = dialog.ShowAddGroup();
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
                new ListBox().deleteListBoxItem<model.FTP_M>(lbGroup, db.DeleteDataFromDB<model.FTP_M>);
                ShowGroupData();
            }
        }
        //取得伺服器群組資料  用來Refresh
        private void ShowGroupData()
        {
            lbGroup.DataSource = db.GetDataFromDB<model.FTP_M>();
            lbGroup.DisplayMember = "Name";
            lbGroup.ValueMember = "ID";
        }
        #endregion



    }
}
