using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDeploy
{
    public class deployToLocal
    {
        public readonly Form1 form;

        public deployToLocal(Form1 form)
        {
            this.form = form;//這樣才能access Form的控制項
            form.LogToBox("===================  開始複製檔案  ===================");
        }

        public void UploadFileToLocal(List<string> filePaths, string fileRootPath, string LocalTargetPath, string BackUpPath = "", bool IsBackUp = false, bool IsChangeVersionNum = false)
        {
            bool valid = true;
            //驗證目錄存不存在
            if (!Directory.Exists(LocalTargetPath))
            {
                if (MessageBox.Show("本機查無此目標目錄\n" + LocalTargetPath + "\n 是否創建此目錄並上傳檔案 ?", "關閉", MessageBoxButtons.YesNo)
                    == DialogResult.No)
                {
                    valid = false;
                }
                
            }

            if (valid)
            {
                foreach (var path in filePaths)
                {
                    //如果要變更版本號
                    if (IsChangeVersionNum)
                    {
                        new ChangeVersionNum().changeFileVersionNum(path);
                    }

                    //如果有勾選備份 就將檔案下載下來
                    if (IsBackUp && !string.IsNullOrEmpty(BackUpPath))
                    {
                        string deployGroupName = form.GetDeployGroupName();
                        if (string.IsNullOrEmpty(deployGroupName))
                            throw new ArgumentException("程式錯誤: 無法取得Deploy群組的名稱");
                        string backupPath = BackUpPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + deployGroupName + "\\";
                        bool exists = Directory.Exists(backupPath);

                        if (!exists)
                            Directory.CreateDirectory(backupPath);
                        File.Copy(path, backupPath +"\\" + Path.GetFileName(path),true);
                    }

                    //開始複製檔案到目標目錄
                    string filename = Path.GetFileName(path);
                    string destPath = LocalTargetPath + "\\" + Path.GetFileName(path);
                    File.Copy(path, destPath, true);
                    form.LogToBox("複製成功" + " 檔案: " + filename);
                    form.updateProgressBar();
                }
            }
        }
    }
}
