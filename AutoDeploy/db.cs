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
        //public string dbPath = @".\db.db3";
        //測試用路徑 
        public string dbPath = @"E:\MyProjects\AutoDeploy\AutoDeploy\db.db3";

        //新增FTP群組
        public int? AddFtpGroup(string name) {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into FTP_M (Name) values(@name)", new{ name });
            }
            return newId;
        }

        /// <summary>
        /// 從資料庫取得此class的所有資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetDataFromDB<T>() {
            List<T> list = new List<T>();
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                list = cnn.GetList<T>().ToList();
            }
            return list;
        }


        /// <summary>
        /// 從DB刪除資料 傳進要刪的資料的class instance(單筆) 使用dapper simpleCRUD刪掉
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void DeleteDataFromDB<T>(T item) {
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                cnn.Delete(item);
            }
        }
    }
}
