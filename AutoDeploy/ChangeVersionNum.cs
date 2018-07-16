using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoDeploy
{
    //變更js、css版本號的相關邏輯
    public class ChangeVersionNum
    {
        //加上版本號 (js,css,圖片,)
        public void changeFileVersionNum(string path)
        {
            //讀取檔案的內容
            string html = File.ReadAllText(path);
            //將version number取代掉
            int unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //匹配 (.css)為一個group 忽略後面的?12312版本號數字 並且後面需要有單引號或雙引號
            string newHtml = Regex.Replace(html, @"(\.(css|js|png|jpg|gif|jpeg|bmp|mp3))\??\d*(?=(\""|\'))",
                m =>string.Format("{0}?{1}",m.Groups[1], unixTimestamp.ToString()) );
            //寫回檔案
            File.WriteAllText(path, newHtml);
        }
    }
}
