using MISA.CUKCUK.Core.Exceptions;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    public class MaterialService : BaseService<Material>, IMaterialService
    {
        IMaterialRepository _reposotory;
        public MaterialService(IMaterialRepository _reposotory) : base(_reposotory)
        {
            this._reposotory = _reposotory;
        }
        /// <summary>
        /// Thêm món ăn
        /// CreatedBy: NVCHINH (11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>số lượng món ăn được thêm</returns>
        public IEnumerable<Material> InsertService(Material dish)
        {
            if (Validate(dish))
            {
                return (_reposotory.Insert(dish));
            }
            else
            {
                return null;
            }
        }
     
        /// <summary>
        /// Validate dữ liệu
        /// createdby:NVCHINH(11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        private bool Validate(Material dish)
        {
            bool check = true;
            if (ChechNull(dish.MaterialCode))
            {
                check = false;
                throw new CukcukException(Resources.Material.VN_Null_Name);
            }
            if (ChechNull(dish.MaterialName))
            {
                check = false;
                throw new CukcukException(Resources.Material.VN_Null_Name);
            }
            if(_reposotory.CheckCode("",dish.MaterialCode, "MaterialCode") != 0)
            {
                throw new CukcukException(Resources.Material.VN_Dup_Code);
                return false;
            }
            return check;
        }
    }
}
