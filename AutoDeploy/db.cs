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
        public string dbPath = @".\db.db3";
        //測試用路徑 
        //public string dbPath = @"E:\MyProjects\AutoDeploy\AutoDeploy\db.db3";
        //public string dbPath = @"C:\Users\user\Documents\FtpAutoDeploy\AutoDeploy\db.db3";
        
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
                var result = cnn.Execute(@"Insert into FTP_D (ClientIP,UserName,Port,Password,GroupID) 
                                            values(@ip,@name,@port,@password,@groupid)",
                    new { ip= item.ClientIP,name=item.UserName,port = item.Port, password=item.Password,groupid=item.GroupID});
            }
            return newId;
        }

        //新增Deploy 路徑清單
        public int? AddDeployDetail(model.Deploy_D item)
        {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into Deploy_D (GroupID,Path) 
                                            values(@groupid,@path)",
                    new { groupid = item.GroupID, path = item.Path});
            }
            return newId;
        }
        //新增Deploy 路徑清單 新增多筆
        public int? AddDeployDetail(List<model.Deploy_D> item)
        {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into Deploy_D (GroupID,Path) 
                                            values(@groupid,@path)",
                    item);
            }
            return newId;
        }
        //新增常用路徑
        public int? AddFastChoosePath(model.FastChoose item) {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"Insert into FastChoose (Path,Type) 
                                            values(@Path,@Type)",
                    item);
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
        public int AddDeployGroupByName(model.Deploy_M item)
        {
            if (string.IsNullOrEmpty(item.Name))
                throw new ArgumentException("名稱不得為空");
            int newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                newId = cnn.Query<int>(@"Insert into Deploy_M (Name,FtpGroup,FtpTargetPath,FileRootPath,BackUpPath,Memo,IsBackup,IsChangeVersionNum,IsUpdateHighLight,IsDeployToLocal,LocalPath) 
                                            values(@name,@FtpGroup,@FtpTargetPath,@FileRootPath,@BackUpPath,@Memo,@IsBackup,@IsChangeVersionNum,@IsUpdateHighLight,@IsDeployToLocal,@LocalPath);
                                            SELECT CAST(last_insert_rowid() as int)", 
                                            new { item.Name,item.FtpGroup,item.FtpTargetPath,item.FileRootPath,item.BackUpPath,item.Memo,item.IsBackup,item.IsChangeVersionNum,item.IsUpdateHighLight, item.IsDeployToLocal,item.LocalPath }).Single();
            }
            return newId;
        }
        //更新Deploy群組的config相關欄位
        public int? UpdateDeployConfig(model.Deploy_M form) {
            int? newId = 0;
            using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
            {
                cnn.Open();
                var result = cnn.Execute(@"update Deploy_M set 
                    FtpGroup = @FtpGroup,
                    FtpTargetPath = @FtpTargetPath,
                    FileRootPath = @FileRootPath,
                    BackUpPath = @BackUpPath,
                    LocalPath = @LocalPath,
                    Memo = @Memo,
                    IsBackup = @IsBackup,
                    IsChangeVersionNum = @IsChangeVersionNum,
                    IsUpdateHighLight = @IsUpdateHighLight,
                    IsDeployToLocal = @IsDeployToLocal
                    where Id = @id", 
                    new { id = form.ID,
                        FtpGroup = form.FtpGroup,
                        FtpTargetPath = form.FtpTargetPath,
                        FileRootPath = form.FileRootPath,
                        BackUpPath = form.BackUpPath,
                        LocalPath = form.LocalPath,
                        Memo = form.Memo,
                        IsBackup = form.IsBackup,
                        IsChangeVersionNum = form.IsChangeVersionNum,
                        IsUpdateHighLight = form.IsUpdateHighLight,
                        IsDeployToLocal = form.IsDeployToLocal
                    });
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
        /// 多載 依據id取得單一筆資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetDataFromDB<T>(int id)where T:new()
        {
            try { 
                T result = new T();
                using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
                {
                    cnn.Open();
                    result = cnn.Get<T>(id);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// 依據where條件 從資料庫取得此class的資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereConditions">where條件 ex. new{Age=12}</param>
        /// <returns></returns>
        public List<T> GetDataFromDBByCondition<T>(object whereConditions)
        {
            try
            {
                List<T> list = new List<T>();
                using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
                {
                    cnn.Open();
                    list = cnn.GetList<T>(whereConditions).ToList<T>();
                }
                return list;
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message);
            }
        }



        /// <summary>
        /// 從DB刪除資料 傳進要刪的資料的class instance(單筆) 使用dapper simpleCRUD刪掉
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void DeleteDataFromDB<T>(T item) {
            try
            {
                using (var cnn = new SQLiteConnection("Data Source=" + dbPath))
                {
                    cnn.Open();
                    cnn.Delete(item);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //將DateTime改成Sqlite能儲存的時間
        public string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }
    }
}
