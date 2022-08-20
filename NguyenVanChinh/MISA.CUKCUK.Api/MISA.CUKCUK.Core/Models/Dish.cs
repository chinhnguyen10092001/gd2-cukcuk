using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng món ăn
    /// </summary>
    /// Created by: NVCHINH (07/08/2022)
    public class Dish : BaseEntity
    {
        #region Contructor
        public Dish()
        {
            DishID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DishID { get; set; }
        /// <summary>
        /// Tên món ăn
        /// </summary>
        public string? DishName { get; set; }
        /// <summary>
        /// Mã món ăn
        /// </summary>
        public string? DishCode { get; set; }
        /// <summary>
        /// ID nhóm thực đơn
        /// </summary>
        public int? DishGroupID { get; set; }
        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        public string? DishGroupName { get; set; }
        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        public int? UnitID { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string? UnitName { get; set; }
        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal? PriceIn { get; set; }
        /// <summary>
        /// Giá gốc
        /// </summary>
        public decimal? ControlPrice { get; set; }
        /// <summary>
        /// Cong thuc che bien
        /// </summary>
        public string? Formula { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// ID bếp
        /// </summary>
        public int? KitchenID { get; set; }
        public int? Dishfeatured { get; set; }
        public string? FormulaPlace { get; set; }
        /// <summary>
        /// Tên bếp
        /// </summary>
        public string? KitchenName { get; set; }
        /// <summary>
        /// Thứ tự món
        /// </summary>
        public int? DishOrder { get; set; }
        /// <summary>
        /// Hiển thị trên menu
        /// </summary>
        public int? MenuShow { get; set; }
        /// <summary>
        /// Là bán thành phẩm
        /// </summary>
        public int? SemiProduct { get; set; }
        /// <summary>
        /// Anh dai dien
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// Anh dai dien
        /// </summary>
        public int? Sale { get; set; }
        public int? Quantify { get; set; }
        public List<DishMaterial>? list { get; set; }
        #endregion
    }
}
