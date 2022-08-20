using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng đồ ăn - nguyên vật liệu
    /// </summary>
    /// Created by: NVCHINH (08/08/2022)
    public class DishMaterial : BaseEntity
    {
        #region Contructor
        
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ID món ăn
        /// </summary>
        public Guid DishID { get; set; }
        /// <summary>
        /// ID nguyên vật liệu
        /// </summary>
        public int MaterialID { get; set; }
        /// <summary>
        /// Tên nguyên vật liệu
        /// </summary>
        public int? UnitID { get; set; }
        /// <summary>
        /// ID nguyên vật liệu
        /// </summary>

        public int Amount { get; set; }
        public int PriceIn { get; set; }

        #endregion
    }
}
