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
            public int ID { get; }
            public string Name { get; set; }
        }

        public class FTP_D {
            public int ID { get; }
            public string ClientIP { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int GroudID { get; set; }


        }
    }
}
