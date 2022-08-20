using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Models;
using MySqlConnector;
using System.Data;

namespace MISA.Web05.Infrastructure
{
    /// <summary>
    /// Lớp repository cha
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: NVCHINH (08/08/2022)
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Variable
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        protected string TableName;
        #endregion

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            // Khai báo kết nối 
            ConnectionString = configuration.GetConnectionString("ConnectionStrings");
            // Lấy tên bảng
            TableName = typeof(T).Name;
        }
        #endregion
        /// <summary>
        /// Xóa bản ghi
        /// CreatedBy:NVCHINH(06/08/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        #region Repository
        public int Delete(T entity)
        {
            // mở kết nối
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery = $"Proc_delete{TableName}";
                // thực hiện truy vấn
                var res = SqlConnection.Execute(sql: sqlQuery, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }
        /// <summary>
        /// lấy toàn bộ bản ghi
        /// CreatedBy:NVCHINH(06/08/2022)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<T> Get()
        {
           
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery = $"Proc_get{TableName}";
                var res = SqlConnection.Query<T>(sql: sqlQuery, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            // Trả lại kết quả
        }
        /// <summary>
        /// tìm kiếm,phân trang
        /// CreatedBy:NVCHINH(06/08/2022)
        /// </summary>
        /// <param name="pageIndex">số thứ tự trang </param>
        /// <param name="pageSize">số bản ghi /trang</param>
        /// <param name="where">Câu lệnh truy vấn</param>
        /// <param name="sort">Câu lệnh sort</param>
        /// <param name="totalRecord">Kết quả query trả về</param>
        /// <returns>totalRecord</returns>
        public IEnumerable<T> Fillter(int pageIndex, int pageSize, string where, string sort, ref int totalRecord)
        {

            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery = $"Proc_fillter{TableName}";
                //tạo param
                DynamicParameters param = new DynamicParameters();
                // gán giá trị cho param
                param.Add("$PageIndex", pageIndex);
                param.Add("$PageSize", pageSize);
                param.Add("$Where", where);
                param.Add("$Sort", sort);
                param.Add("$Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //thực hiện truy vấn
                var res = SqlConnection.Query<T>(sql: sqlQuery, param: param, commandType: System.Data.CommandType.StoredProcedure);
                totalRecord = param.Get<int>("$Total");
                return res;
            }
            // Trả lại kết quả
        }
        /// <summary>
        /// Thêm bane ghi
        /// createdBy:NVCHINH(06/08/2022)
        /// </summary>
        /// <param name="entity">Đối tượng đưuọc thêm</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Insert(T entity)
        {
               using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    SqlConnection.Open();
                    return BaseInsert(entity);
                    
                }
                    
        }
        public IEnumerable<T> BaseInsert(T entity)
        {
            var sqlQuery = $"Proc_insert{TableName}";
            var res = SqlConnection.Execute(sql: sqlQuery, param: entity, commandType: System.Data.CommandType.StoredProcedure);
            if (res == 1)
            {
                sqlQuery = $"Select * from {TableName} ORDER BY CreatedDate DESC  limit 1";
                var data = SqlConnection.Query<T>(sqlQuery);
                return data;
            }
            else
            {
                return null; ;
            }
           
        }
        
        /// <summary>
        /// Check trùng thuộc tính
        /// createdBy:NVCHINH(06/08/2022)
        /// </summary>
        /// <param name="id">id càn check</param>
        /// <param name="code">mã cần check</param>
        /// <param name="colum">cột thuộc tính cần check</param>
        /// <returns>0: Code khong tồn tại,1:code tồn tại ID khong tồn tại,2:Code và ID tồn tại</returns>
        public int CheckCode(string? id,string code,string colum)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery =$"Select * from {TableName} where {colum}='{code}'";
                var res = SqlConnection.QueryFirstOrDefault<T>(sql: sqlQuery);
                if(res == null)
                {
                    return 0;// mã chưa tồn tại
                }
                else
                {
                    int x = 0;
                    var sql="";
                    if (Int32.TryParse(id,out x))
                    {
                        sql = $"Select * from {TableName} where {colum}='{code}' and {TableName}ID={id}";
                    }
                    else
                    {
                        sql = $"Select * from {TableName} where {colum}='{code}' and {TableName}ID='{id}'";
                    }
                    var ress = SqlConnection.QueryFirstOrDefault<T>(sql: sql);
                    if(ress == null)
                    {
                        return 1;// Tồn tại mã nhưng không có ID
                    }
                    else
                    {
                        return 2;// Tồn tại mã và ID
                    }
                }
            }
        }
        #endregion
    }
}