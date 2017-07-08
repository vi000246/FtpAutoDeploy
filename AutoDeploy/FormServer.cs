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
        public FormServer()
        {
            InitializeComponent();
        }
        //新增群組按鈕點擊
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            string name = dialog.ShowAddGroup();
            if (!string.IsNullOrEmpty(name)) {
                int? newID = new db().AddFtpGroup(name);
            }
            lbGroup.Refresh();
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'dbDataSet.FTP_M' 資料表。您可以視需要進行移動或移除。
            this.fTP_MTableAdapter.Fill(this.dbDataSet.FTP_M);

        }
    }
}
