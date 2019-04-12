using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 用來儲存資料庫的類別
/// </summary>
namespace AutoDeploy
{
    public class model
    {
        //FTP群組
        [Table("FTP_M")]
        public class FTP_M {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
        }
        //FTP列表
        [Table("FTP_D")]
        public class FTP_D {
            [Key]
            public int ID { get; set; }
            public string ClientIP { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Port { get; set; }
            [Editable(false)]
            public int GroupID { get; set; }


        }
        //Deploy群組
        [Table("Deploy_M")]
        public class Deploy_M
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public int FtpGroup { get; set; }
            public string FtpTargetPath { get; set; }
            public string FileRootPath { get; set; }
            public string BackUpPath { get; set; }
            public string LocalPath { get; set; }
            public string Memo { get; set; }
            public bool IsBackup { get; set; }
            public bool IsChangeVersionNum { get; set; }
            public bool IsUpdateHighLight { get; set; }
            public bool IsDeployToLocal { get; set; }
        }
        //檔案清單
        [Table("Deploy_D")]
        public class Deploy_D
        {
            [Key]
            public int ID { get; set; }
            [Editable(false)]
            public int GroupID { get; set; }
            public string Path { get; set; }
        }
        //常用路徑設置
        [Table("FastChoose")]
        public class FastChoose {
            [Key]
            public int ID { get; set; }
            public string Path { get; set; }
            public int Type { get; set; }
        }
    }
    /// <summary>
    /// 用來設置GridView的Clumn Index 這樣改變排序時就不用改程式
    /// </summary>
    public class columnIndex {
        public enum FTP_D {
            ID,
            ClientIP,
            Port,
            UserName,
            Password
        }
    }

    /// <summary>
    /// 用來儲存常用路徑的類型 
    /// Project:專案根目錄
    /// Backup:備份目錄
    /// </summary>
    public enum FastChooseType {
        Project,
        Backup,
        Local
    }
}
