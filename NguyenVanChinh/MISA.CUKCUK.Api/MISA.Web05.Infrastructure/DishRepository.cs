using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Exceptions;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        IDishMaterialRepository materialRepository;
        public DishRepository(IDishMaterialRepository materialRepository,IConfiguration configuration) : base(configuration)
        {
            this.materialRepository = materialRepository;
        }
        /// <summary>
        /// Thêm món ăn đầy đủ
        /// CreatedBy:NVCHINH (15/08/2022)
        /// </summary>
        /// <param name="dish">món ăn được thêm</param>
        /// <param name="list">list NVL thêm với món ăn</param>
        /// <returns>số lượng món ăn dc thêm</returns>
        public int InsertAll(Dish dish, List<DishMaterial> list)
        {
            try
            {
                using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    // chưa mở kết nối thì open
                    if(SqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }
                    using (var tran = SqlConnection.BeginTransaction())
                    {
                        try
                        {
                            // nếu có nvl tring list thì set Quantify=2 ( đã thiết lập)
                            if (list.Count > 0)
                            {
                                dish.Quantify = 2;
                            }
                            // Quantity=1( chwua thiết lập)
                            else
                            {
                                dish.Quantify=1;
                            }
                            // thực hiện thêm món ăn vào bảng món ăn
                            var sqlQuery = $"Proc_insertDish";
                            bool isSucess = SqlConnection.Execute(sql: sqlQuery, param: dish, tran, commandType: System.Data.CommandType.StoredProcedure) > 0;
                            // nếu thêm món ăn thành công thì mới thực hiện tiếp
                            if (isSucess)
                            {
                                if (list.Count > 0)
                                {
                                    // thực hiện thêm định lượng NVL
                                    foreach (var item in list)
                                    {
                                        item.DishID = dish.DishID;
                                        var param = new DynamicParameters(item);
                                        param.Add("@NewID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                                        sqlQuery = $"Proc_insertDishMaterial";
                                        isSucess = SqlConnection.Execute(sqlQuery, param, tran, commandType: System.Data.CommandType.StoredProcedure) > 0;
                                        var id = param.Get<int>("@NewID");
                                        // Nếu thêm 1 bản ghi khoogn thành công thì rollback
                                        if (!isSucess)
                                        {
                                            tran.Rollback();
                                            return 0;
                                        }
                                    }
                                }
                                
                            }
                            if (isSucess)
                            {
                                tran.Commit();
                            }
                            else
                            {
                                tran.Rollback();
                            }
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
                            throw ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if(SqlConnection != null && SqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    SqlConnection.Close();
                }
            }
            return 1;
        }
        /// <summary>
        /// cập nhật món ăn đầy đủ
        /// CreatedBy:NVCHINH (15/08/2022)
        /// </summary>
        /// <param name="dish">món ăn được thêm</param>
        /// <param name="list">list NVL thêm với món ăn</param>
        /// <returns>số lượng món ăn dc thêm</returns>
        public int UpdateAll(Dish dish, List<DishMaterial> list)
        {
            try
            {
                using (SqlConnection = new MySqlConnection(ConnectionString))
                {
                    // kiểm tra xem kết nối mở chưa, nếu chưa thì mở
                    if (SqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }
                    using (var tran = SqlConnection.BeginTransaction())
                    {
                        try
                        {
                            // kiểm tra xem xem có định lượng NVL không ( nếu có thì Quantify=2 - đã thiết lâp)
                            if (list.Count > 0)
                            {
                                dish.Quantify = 2;
                            }
                            // ngược lại thì ==1
                            else
                            {
                                dish.Quantify = 1;
                            }
                            // cập nhật món ăn
                            var sqlQuery = $"Proc_updateDish";
                            bool isSucess = SqlConnection.Execute(sql: sqlQuery, param: dish, tran, commandType: System.Data.CommandType.StoredProcedure) > 0;
                            // nếu cập nhật món ăn thành cong thì xóa các định lượng cũ
                            if (isSucess)
                            {
                                var listId = "";
                                //sqlQuery = $"Proc_deleteDishMaterialByDishID";
                                //var param = new DynamicParameters();
                                //param.Add("@DishID", dish.DishID);
                                //param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                                //isSucess = SqlConnection.Execute(sql: sqlQuery, param: param, tran, commandType: System.Data.CommandType.StoredProcedure) == param.Get<int>("@Total");
                                if (list.Count > 0)
                                {
                                    if (isSucess)
                                    {
                                        
                                        foreach (var item in list)
                                        {
                                            if (item.DishID.Equals(dish.DishID))
                                            {
                                                sqlQuery = $"Proc_updateDishMaterial";
                                                isSucess = SqlConnection.Execute(sqlQuery, item, tran, commandType: System.Data.CommandType.StoredProcedure) > 0;
                                                listId = listId + item.ID + ",";
                                            }
                                            else
                                            {
                                                item.DishID = dish.DishID;
                                                var param = new DynamicParameters(item);
                                                param.Add("@NewID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                                                sqlQuery = $"Proc_insertDishMaterial";
                                                isSucess = SqlConnection.Execute(sqlQuery, param, tran, commandType: System.Data.CommandType.StoredProcedure) > 0;
                                                var id = param.Get<int>("@NewID");
                                                listId = listId + id + ",";
                                            }
                                            if (!isSucess)
                                            {
                                                tran.Rollback();
                                                return 0;
                                            }
                                            
                                        }
                                        
                                    }
                                }
                                if (isSucess)
                                {
                                    sqlQuery = $"Proc_deleteDishMaterial";
                                    var param = new DynamicParameters();
                                    param.Add("@list", listId.TrimEnd(','));
                                    param.Add("@DishId", dish.DishID);
                                    param.Add("@Total", dbType: DbType.Int32, direction: ParameterDirection.Output);
                                    isSucess = SqlConnection.Execute(sqlQuery, param, tran, commandType: System.Data.CommandType.StoredProcedure) == param.Get<int>("@Total");
                                }

                            }
                            if (isSucess)
                            {
                                tran.Commit();
                            }
                            else
                            {
                                tran.Rollback();
                                return 0;
                            }
                        }
                        catch (Exception)
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (SqlConnection != null && SqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    SqlConnection.Close();
                }
            }
            return 1;
        }
    }

}
