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
        //新增Deploy群組的dialog
        public static string ShowAddGroup()
        {
            Form prompt = new Form()
            {
                Width = 220,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "新增群組",
                StartPosition = FormStartPosition.CenterParent
            };
            Label textLabel = new Label() { Left = 30, Top = 20, Text = "請輸入群組名稱" };
            TextBox textBox = new TextBox() { Left = 30, Top = 50, Width = 150 };
            Button confirmation = new Button() { Text = "確定", Left = 70, Width = 50, Top = 80, DialogResult = DialogResult.OK };
            Button cancelation = new Button() { Text = "取消", Left = 130, Width = 50, Top = 80, DialogResult = DialogResult.Cancel };
            confirmation.Click += (sender, e) => {
                    prompt.Close();
            };
            cancelation.Click += (sender, e) => {
                prompt.Close();
            };
            prompt.FormClosing += (sender, e) => {
                if (prompt.DialogResult == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        MessageBox.Show("名稱不得為空白");
                        e.Cancel = true;
                    } else if (System.Text.ASCIIEncoding.GetEncoding("big5").GetByteCount(textBox.Text)>24) {
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
    }
}
