using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
        //新增FTP列表
        public int? AddFtpDetail(model.FTP_D item)
        {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into FTP_D (ClientIP,UserName,Password,GroupID) 
                                            values(@ip,@name,@password,@groupid)",
                    new { ip= item.ClientIP,name=item.UserName,password=item.Password,groupid=item.GroupID});
            }
            return newId;
        }

        //刪除FTP的主表和明細表
        public void DeleteFtpMasterAndDetail(model.FTP_M item) {
            //刪除主表的資料
            DeleteDataFromDB(item);
            //刪除明細表的資料  where GroupID=主表ID的資料
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                cnn.DeleteList<model.FTP_D>(new { GroupID = item.ID });
            }

        }
        //刪除Deploy的主表和明細表
        public void DeleteDeployMasterAndDetail(model.Deploy_M item)
        {
            //刪除主表的資料
            DeleteDataFromDB(item);
            //刪除明細表的資料  where GroupID=主表ID的資料
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                cnn.DeleteList<model.Deploy_D>(new { GroupID = item.ID });
            }

        }

        //新增Deploy群組
        public int? AddDeployGroup(string name)
        {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into Deploy_M (Name) values(@name)", new { name });
            }
            return newId;
        }

        /// <summary>
        /// 更新資料到DB 傳進資料的class instance 用simpleCRUD更新資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void UpdateDataToDB<T>(T item)
        {
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                (cnn as IDbConnection).Update(item);
            }
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
        /// 依據where條件 從資料庫取得此class的資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions">where條件 ex. new{Age=12}</param>
        /// <returns></returns>
        public List<T> GetDataFromDBByCondition<T>(object whereConditions)
        {
            List<T> list = new List<T>();
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                list = cnn.GetList<T>(whereConditions).ToList<T>();
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
