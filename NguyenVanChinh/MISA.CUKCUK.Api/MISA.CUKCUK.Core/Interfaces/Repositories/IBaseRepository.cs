using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface cơ sở cho các interface repo
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: NVCHINH (07/08/2022)
    public interface IBaseRepository<T>
    {
        #region Interface
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: NVCHINH (07/08/2022)
        IEnumerable<T> Get();
        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>Số bản ghi đã thêm</returns>
        /// Created by: NVCHINH (07/08/2022)
        IEnumerable<T> Insert(T entity);
        /// <summary>
        /// Xóa bảng fhi
        /// </summary>
        /// <param name="entityID">ID bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: NVCHINH (07/08/2022)
        int Delete(T entity);
        IEnumerable<T> Fillter(int pageIndex, int pageSize, string where, string sort, ref int totalRecord);
        int CheckCode(string? id,string code,string colum);
        #endregion
    }
}
