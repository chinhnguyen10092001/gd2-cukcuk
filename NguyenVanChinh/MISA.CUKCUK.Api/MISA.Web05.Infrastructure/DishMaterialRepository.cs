using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure
{
    public class DishMaterialRepository: BaseRepository<DishMaterial>, IDishMaterialRepository
    {
        public DishMaterialRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Thêm nhiều NVL
        /// CreatedBy:NVCHINH(10/08/2022)
        /// </summary>
        /// <param name="material">List NV đc thêm </param>
        /// <returns>số bản ghi được thêm thành công</returns>
        public int InsertAll(List<DishMaterial> material)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                SqlConnection.Open();
                using (var tran = SqlConnection.BeginTransaction())
                {
                    int dem = 0;
                    try
                    {
                        foreach (var item in material)
                        {
                            var sqlQuery = $"Proc_insert{TableName}";
                            var res = SqlConnection.Execute(sqlQuery,item,tran, commandType: System.Data.CommandType.StoredProcedure);
                            dem++;
                        }
                        // đúng thì comit dữ liệu
                        tran.Commit();
                        return dem;
                    }
                    catch (Exception)
                    {
                        // lỗi thì rollback
                        tran.Rollback();
                        throw;
                    }
                }
            }
               
        }
        /// <summary>
        /// Timf kiếm
        /// CreatedBy:NVCHINH
        /// </summary>
        /// <param name="dishId">ID dish cần truyền</param>
        /// <returns>Mảng material bản ghi thỏa mãn</returns>
        public IEnumerable<DishMaterial> fillter(string dishId)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var param = new DynamicParameters();
                param.Add("@DishID", dishId);
                var sqlQuery = $"Proc_fillterDishMaterial";
                var res = SqlConnection.Query<DishMaterial>(sql: sqlQuery, param: param, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }
    }
}
