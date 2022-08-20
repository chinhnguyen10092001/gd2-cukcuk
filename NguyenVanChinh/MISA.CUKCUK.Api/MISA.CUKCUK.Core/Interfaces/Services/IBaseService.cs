using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Services
{
    /// <summary>
    /// Interface cơ sở cho các interface service
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: NVCHINH (07/08/2022)
    public interface IBaseService<T>
    {
        #region Interface
        /// <summary>
        /// Interface thêm bản ghi mới
        /// </summary>
        /// <param name="entity">bản ghi mới</param>
        /// <returns>Số bản ghi đã đc thêm</returns>
        /// Created by: NVCHINH (07/08/2022)
        IEnumerable<T> InsertService(T entity);
        /// <summary>
        /// Interface cập nhật bản ghi
        /// </summary>
        /// <param name="entity">bản ghi cần cập nhật</param>
        /// <returns>Số bản ghi đã cập nhật</returns>
        /// Created by: NVCHINH (07/08/2022)
        #endregion
    }
}
