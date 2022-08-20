using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng nhóm thực đơn
    /// </summary>
    /// Created by: NVCHINH (07/08/2022)
    public class DishGroup : BaseEntity
    {
        #region Contructor
        
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public int DishGroupID { get; set; }
        /// <summary>
        /// Mã nhóm thực đơn
        /// </summary>
        public string DishGroupCode { get; set; }
        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        public string DishGroupName { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// ID bếp
        /// </summary>
        public int KitchenID { get; set; }
        /// <summary>
        /// Tên bếp
        /// </summary>
        public string? KitchenName { get; set; }
        #endregion
    }
}
