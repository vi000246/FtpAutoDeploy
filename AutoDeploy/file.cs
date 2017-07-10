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

    }
}
