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
    public class KitchenService : BaseService<Kitchen>, IKitchenService
    {
        IKitchenRepository _reposotory;
        public KitchenService(IKitchenRepository _reposotory):base(_reposotory)
        {
            this._reposotory = _reposotory;
        }
        /// <summary>
        /// Thêm món ăn
        /// CreatedBy: NVCHINH (11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>số lượng món ăn được thêm</returns>
        public override IEnumerable<Kitchen>  InsertService(Kitchen dish)
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
        private bool Validate(Kitchen dish)
        {
            bool check = true;
            if (ChechNull(dish.KitchenName))
            {
                check = false;
                throw new CukcukException(Resources.Kitchen.VN_Null_Name);
            }
            return check;
        }
    }
}
