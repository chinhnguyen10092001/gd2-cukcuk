using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng kho
    /// </summary>
    /// Created by: NVCHINH (07/08/2022)
    public class Repository : BaseEntity
    {
        #region Contructor
        public Repository()
        {
            RepositoryID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid RepositoryID { get; set; }
        /// <summary>
        /// Mã kho
        /// </summary>
        public string RepositoryCode { get; set; }
        /// <summary>
        /// Tên kho
        /// </summary>
        public string RepositoryName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        #endregion
    }
}
