using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 放置和檔案處理相關的function
/// </summary>
namespace AutoDeploy
{
    public class file
    {
        //輸入路徑 如果是目錄就取出所有檔案path 是檔案就回傳檔案path
        public List<string> getAllFiles(string path) {
            try
            {
                List<string> filePaths = new List<string>();
                FileAttributes attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    filePaths = Directory.GetFiles(path, "*",
                                     SearchOption.AllDirectories).ToList();
                }
                else
                {
                    filePaths.Add(path);
                }
                return filePaths;
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// 判斷一個路徑是否是另一個路徑的子目錄 是子目錄就回傳true 如果兩個路徑相等也回傳true
        /// </summary>
        /// <param name="parentPath"></param>
        /// <param name="childPath"></param>
        /// <returns></returns>
        public static bool IsSubfolder(string parentPath, string childPath)
        {
            var parentUri = new Uri(parentPath);
            var childUri = new DirectoryInfo(childPath).Parent;
            while (childUri != null)
            {
                if (new Uri(childUri.FullName) == parentUri)
                {
                    return true;
                }
                childUri = childUri.Parent;
            }
            if (parentPath == childPath)
                return true;
            return false;
        }

        /// <summary>
        /// 依據本地檔案清單根目錄跟FTP上傳目標目錄 組合出FTP的上傳路徑 (含檔名)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string BuildFtpRemotePath(string path, string fileRootPath, string FtpTargetPath) {
            //移除路徑的根目錄 
            string remotePath = path.Replace(fileRootPath,"");
            remotePath = FtpTargetPath + remotePath;


            return remotePath;
        }

    }
}
