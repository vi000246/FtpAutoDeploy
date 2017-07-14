using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using System.Net;
using System.IO;
using System.Windows.Forms;
/// <summary>
/// 用來處理ftp相關
/// </summary>
namespace AutoDeploy
{


    public class ftp: IDisposable
    {
        public readonly Form1 form;

        public FtpClient client;
        public ftp(Form1 form,string ClientIP,string User,string Password,int port) {
            try
            {
                this.form = form;//這樣才能access Form的控制項

                client = new FtpClient(ClientIP);
                //client.Encoding = Encoding.UTF8;
                //如果帳密不為空 就登入 否則採用匿名登入
                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                    client.Credentials = new NetworkCredential(User, Password);
                client.Port = port;
                client.Encoding = Encoding.Default;//防止上傳的中文變亂碼
                client.Connect();
                form.LogToBox("=====連線至FTP :"+ClientIP+":"+port+"======");
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }
        //上傳檔案至FTP
        public void UploadFileToFtp(List<string> filePaths,string fileRootPath,string FtpTargetPath, string BackUpPath = "",bool IsBackUp = false) {
            try
            {
                client.RetryAttempts = 3;
                bool valid = true;
                var IsDirectoryExist = client.DirectoryExists(FtpTargetPath);
                if (!IsDirectoryExist)
                    if (MessageBox.Show("FTP查無此目標目錄\n" + FtpTargetPath + "\n 是否創建此目錄並上傳檔案 ?", "關閉", MessageBoxButtons.YesNo)
                        == DialogResult.No)
                    {
                        valid = false;
                    }

                if (valid)
                {
                    foreach (var path in filePaths)
                    {
                        int index = filePaths.IndexOf(path);
                        int rest = filePaths.Count - (index+1);

                        //建立出FTP要上傳的目錄
                        string remotePath = file.BuildFtpRemotePath(path, fileRootPath, FtpTargetPath);
                        if (client.FileExists(remotePath))
                        {
                            if (IsBackUp && !string.IsNullOrEmpty(BackUpPath)) {
                                string deployGroupName = form.GetDeployGroupName();
                                if (string.IsNullOrEmpty(deployGroupName))
                                    throw new ArgumentException("程式錯誤: 無法取得Deploy群組的名稱");
                                client.DownloadFile(
                                    BackUpPath+"\\"+ DateTime.Now.ToString("yyyy-MM-dd") +"\\"+deployGroupName+"\\" +remotePath,
                                    remotePath);
                            }

                        }

                        //取得目錄名稱 ex.c:/Desktop/test.txt  => c:/Desktop
                        remotePath = Path.GetDirectoryName(remotePath);
                        //因為FluentFTP UploadFile()有bug 所以用多檔上傳的api來傳檔案
                        int result = client.UploadFiles(new string[] { path }, remotePath, FtpExists.Overwrite, true);
                        form.LogToBox("剩餘:"+ rest + " 上傳"+((result==1)?"成功":"失敗")+" 檔案: "+path.Replace(fileRootPath,""));
                    }
                    form.LogToBox("=========所有檔案部署完成=======");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void testUpload() {
            //正確的
            client.UploadFiles(new string[] { @"C:\Users\vi000\Desktop\testUpload\111.txt" }, @"Users\vi000\Desktop\testUpload\", FtpExists.Overwrite, true);
            ///錯誤的remotePath   (看來是FTP server的問題)

        }


        public void Dispose()
        {
            client.Disconnect();
        }
    }
}
