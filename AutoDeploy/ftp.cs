using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using System.Net;
using System.IO;
/// <summary>
/// 用來處理ftp相關
/// </summary>
namespace AutoDeploy
{
    public class ftp: IDisposable
    {
        public FtpClient client;
        public ftp(string ClientIP,string User,string Password,int port) {
            try
            {
                client = new FtpClient(ClientIP);
                //client.Encoding = Encoding.UTF8;
                //如果帳密不為空 就登入 否則採用匿名登入
                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                    client.Credentials = new NetworkCredential(User, Password);
                client.Port = port;

                client.Connect();
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
                foreach (var path in filePaths)
                {
                    //建立出FTP要上傳的目錄
                    string remotePath = file.BuildFtpRemotePath(path, fileRootPath, FtpTargetPath);
                    //因為FluentFTP UploadFile()有bug 所以用多檔上傳的api來傳檔案
                    //client.UploadFiles(new string[] { path }, remotePath, FtpExists.Overwrite, true);
                    client.UploadFiles(new string[] { @"C:\Users\user\Desktop\測試用專案\root.txt" }, @"\test\", FtpExists.Overwrite, true);
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
            ///  會create test資料夾 但can't find file
            ///  "test\"
            ///  "\test"
            ///  "\test\"
        }


        public void Dispose()
        {
            client.Disconnect();
        }
    }
}
