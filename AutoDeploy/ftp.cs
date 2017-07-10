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
        public ftp(string ClientIP,string User,string Password) {
            try
            {
                client = new FtpClient(ClientIP);
                //如果帳密不為空 就登入 否則採用匿名登入
                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                    client.Credentials = new NetworkCredential(User, Password);

                client.Connect();
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }
        //上傳檔案至FTP
        public void UploadFileToFtp(List<string> filePaths,bool IsBackUp = false,string BackUpPath = "") {
            try
            {
                client.RetryAttempts = 3;
                foreach (var path in filePaths)
                {
                    //移除路徑的根目錄 ex.  C:/destop/user/test/111.txt  =>destop/user/test/111.txt
                    string remotePath = path.Substring(Path.GetPathRoot(path).Length);
                    //取得目錄名稱 ex.c:/Desktop/test.txt  => c:/Desktop
                    string directory = Path.GetDirectoryName(remotePath);
                    //這是用來多檔案上傳的api 因為單檔上傳的有bug
                    client.UploadFiles(new string[] { path }, directory, FtpExists.Overwrite, true);
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
        }


        public void Dispose()
        {
            client.Disconnect();
        }
    }
}
