using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng nhóm nguyên vật liệu
    /// </summary>
    /// Created by: NVCHINH (07/08/2022)
    public class MaterialGroup : BaseEntity
    {
        #region Contructor
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public int MaterialGroupID { get; set; }
        /// <summary>
        /// Mã nhóm NVL
        /// </summary>
        public string MaterialGroupCode { get; set; }
        /// <summary>
        /// Tên nhóm NVL
        /// </summary>
        public string MaterialGroupName { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        #endregion
    }
}
