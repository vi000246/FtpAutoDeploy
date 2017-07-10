using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 彈出視窗相關
/// </summary>
namespace AutoDeploy
{
    public class dialog
    {
        /// <summary>
        /// 顯示單個input box的dialog
        /// </summary>
        /// <param name="title"></param>
        /// <returns>回傳input的名稱</returns>
        public static string ShowInputGroupNameForm(string title = "新增群組",string message = "請輸入群組名稱")
        {
            try
            {
                Form prompt = new Form()
                {
                    Width = 245,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = title,
                    StartPosition = FormStartPosition.CenterParent
                };
                Label textLabel = new Label() { Left = 30, Top = 20, Width = 150, Text = message, AutoSize = true };
                TextBox textBox = new TextBox() { Left = 30, Top = 50, Width = 150 };
                Button confirmation = new Button() { Text = "確定", Left = 70, Width = 50, Top = 80, DialogResult = DialogResult.OK };
                Button cancelation = new Button() { Text = "取消", Left = 130, Width = 50, Top = 80, DialogResult = DialogResult.Cancel };
                confirmation.Click += (sender, e) =>
                {
                    prompt.Close();
                };
                cancelation.Click += (sender, e) =>
                {
                    prompt.Close();
                };
                prompt.FormClosing += (sender, e) =>
                {
                    if (prompt.DialogResult == DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(textBox.Text))
                        {
                            MessageBox.Show("名稱不得為空白");
                            e.Cancel = true;
                        }
                        else if (System.Text.ASCIIEncoding.GetEncoding("big5").GetByteCount(textBox.Text) > 24)
                        {
                            MessageBox.Show("名稱過長 英文限24字元 中文限12字元");
                            e.Cancel = true;
                        }
                    }
                };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(cancelation);
                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancelation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        /// <summary>
        /// FTP Detail的彈出視窗
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static model.FTP_D ShowFtpDetailDialog(string title="新增FTP伺服器") {
            try
            {
                model.FTP_D item = new model.FTP_D();

                Form prompt = new Form()
                {
                    Width = 270,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = title,
                    StartPosition = FormStartPosition.CenterParent
                };
                Label lbIP = new Label() { Left = 10, Top = 20, Width = 70, Text = "FTP IP" };
                TextBox tbIP = new TextBox() { Left = 80, Top = 20, Width = 150 };
                Label lbName = new Label() { Left = 10, Top = 50, Width = 70, Text = "使用者名稱" };
                TextBox tbName = new TextBox() { Left = 80, Top = 50, Width = 150 };
                Label lbPassword = new Label() { Left = 10, Top = 80, Width = 70, Text = "密碼" };
                TextBox tbPassword = new TextBox() { Left = 80, Top = 80, Width = 150 };
                Button confirmation = new Button() { Text = "確定", Left = 100, Width = 50, Top = 120, DialogResult = DialogResult.OK };
                Button cancelation = new Button() { Text = "取消", Left = 160, Width = 50, Top = 120, DialogResult = DialogResult.Cancel };
                confirmation.Click += (sender, e) =>
                {
                    prompt.Close();
                };
                cancelation.Click += (sender, e) =>
                {
                    prompt.Close();
                };
                prompt.FormClosing += (sender, e) =>
                {
                    if (prompt.DialogResult == DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(tbIP.Text) || string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbPassword.Text))
                        {
                            MessageBox.Show("欄位不得為空");
                            e.Cancel = true;
                        }
                        item.ClientIP = tbIP.Text;
                        item.UserName = tbName.Text;
                        item.Password = tbPassword.Text;
                    }
                };

                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancelation);
                prompt.Controls.Add(lbIP);
                prompt.Controls.Add(tbIP);
                prompt.Controls.Add(lbName);
                prompt.Controls.Add(tbName);
                prompt.Controls.Add(lbPassword);
                prompt.Controls.Add(tbPassword);
                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancelation;



                return prompt.ShowDialog() == DialogResult.OK ? item : null;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //開啟資料夾瀏覽視窗 回傳所選擇的資料夾路徑
        public static string BrowseFolder()
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }
            return folderPath;
        }
    }
}
