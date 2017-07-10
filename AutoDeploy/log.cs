using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// log相關
/// </summary>
namespace AutoDeploy
{
    public class log
    {
        /// <summary>
        /// 用來紀錄Deploy的歷史紀錄
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static string BuildDeployHistoryMessage(int DeployGroupId)
        {
            model.Deploy_M master = new db().GetDataFromDB<model.Deploy_M>(DeployGroupId);
            List<model.Deploy_D> detail = new db().GetDataFromDBByCondition<model.Deploy_D>(new {GroupID = DeployGroupId });
            //依據FTP群組ID 取得FTP群組名稱
            model.FTP_M ftpM = new db().GetDataFromDB<model.FTP_M>(master.FtpGroup);
            string FTPName = ftpM == null ? "查無此FTP群組名稱" : ftpM.Name;
            StringBuilder message = new StringBuilder();
            message.AppendLine();
            message.AppendLine("________________________________________");
            message.AppendLine("Deploy群組名稱: " + master.Name);
            message.AppendLine("Deploy執行時間: " + DateTime.Now);
            message.AppendLine("上傳FTP群組名稱: " + FTPName);
            message.AppendLine("FTP目標目錄: " + master.FtpTargetPath);
            message.AppendLine("本地上傳清單根目錄: " + master.FileRootPath);
            message.AppendLine("本地備份檔目錄: " + master.BackUpPath);
            message.AppendLine("備註: ");
            message.AppendLine(master.Memo);
            message.AppendLine("Deploy的檔案清單:");
            foreach (var filePath in detail)
            {
                message.AppendLine(filePath.Path);
            }
            message.AppendLine("________________________________________");
            //// Get the QueryString along with the Virtual Path
            //message.AppendLine("Raw Url : " + System.Web.HttpContext.Current.Request.RawUrl);
            //// Type of Exception
            //message.AppendLine("Type of Exception : " + logException.GetType().Name);
            //// Get the error message
            //message.AppendLine("Message : " + logException.Message);
            //// Source of the message
            //message.AppendLine("Source : " + logException.Source);

            //// Stack Trace of the error
            //message.AppendLine("Stack Trace : " + logException.StackTrace);
            //// Method where the error occurred
            //message.AppendLine("TargetSite : " + logException.TargetSite);
            return message.ToString();
        }
    }
}

