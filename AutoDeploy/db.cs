using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 放置DB操作程式碼
/// </summary>
namespace AutoDeploy
{
    public class db
    {
        //新增FTP群組
        public int? AddFtpGroup(string name) {
            int? newId = 0;
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into FTP_M (Name) values(@name)", new{ name });
            }
            return newId;
        }
    }



    public class SqLiteBaseRepository
    {
        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + @".\db.db3");
        }
    }
}
