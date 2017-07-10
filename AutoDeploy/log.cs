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
        public static string BuildDeployHistoryMessage(model.Deploy_D form)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine();
            //message.AppendLine("Error in Path : " + System.Web.HttpContext.Current.Request.Path);
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

