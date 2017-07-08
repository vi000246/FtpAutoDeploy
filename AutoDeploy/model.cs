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
        [Table("FTP_M")]
        public class FTP_M {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
        }
        [Table("FTP_D")]
        public class FTP_D {
            [Key]
            public int ID { get; set; }
            public string ClientIP { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            [Editable(false)]
            public int GroupID { get; set; }


        }
    }
}
