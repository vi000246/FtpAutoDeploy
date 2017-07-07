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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // 啟動按鈕Click
        private void btnStart_Click(object sender, EventArgs e)
        {

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
                new ListBox().deleteListBoxItem(lbFileList);
            }
        }
        //清除所有檔案清單的路徑
        private void btnClear_Click(object sender, EventArgs e)
        {
            lbFileList.Items.Clear();
        }
        #endregion




    }
}
