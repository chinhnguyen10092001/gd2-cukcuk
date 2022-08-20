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
    public class DishGroupService : BaseService<DishGroup>, IDishGroupService
    {
        IDishGroupRepository _reposotory;
        public DishGroupService(IDishGroupRepository _reposotory) : base(_reposotory)
        {
            this._reposotory = _reposotory;
        }
        /// <summary>
        /// Thêm món ăn
        /// CreatedBy: NVCHINH (11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>số lượng món ăn được thêm</returns>
        public override IEnumerable<DishGroup> InsertService(DishGroup dish)
        {
            if (Validate(dish))
            {
                return _reposotory.Insert(dish);
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
        private bool Validate(DishGroup dish)
        {
            if (ChechNull(dish.DishGroupCode))
            {
                throw new CukcukException(Resources.DishGroup.VN_Null_Code);
                return false;
            }
            if (ChechNull(dish.DishGroupName))
            {
                throw new CukcukException(Resources.DishGroup.VN_Null_Name);
                return false;
            }
            if(_reposotory.CheckCode("",dish.DishGroupCode, "DishGroupCode") != 0)
            {
                throw new CukcukException(Resources.DishGroup.VN_Dup_Code);
                return false;
            }
            return true;
        }
    }
}
