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
    public class DishService : BaseService<Dish>, IDishService
    {
        IDishRepository _reposotory;
        string msg;
        public DishService(IDishRepository _reposotory):base(_reposotory)
        {
            this._reposotory = _reposotory;
        }
        /// <summary>
        /// Thêm món ăn
        /// CreatedBy: NVCHINH (11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>số lượng món ăn được thêm</returns>
        public Response InsertService(Dish dish, List<DishMaterial> list)
        {
            if (Validate(dish)&&checkCodeInsert(dish))
            {
                try
                {
                    var res = _reposotory.InsertAll(dish, list);
                    return new Response(res, true, "");
                }
                catch (Exception ex)
                {

                    return new Response(null, false,ex.Message);
                }
            }
            else
            {
                return new Response(null, false, msg);
            }
        }
        /// <summary>
        /// Sửa món ăn
        /// CreatedBy: NVCHINH (11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>số lượng món ăn được sửa</returns>
        public Response UpdateService(Dish dish, List<DishMaterial> list)
        {
            if (Validate(dish)&&checkCodeUpdate(dish))
            {
                var res = _reposotory.UpdateAll(dish,list);
                return new Response(res, true, "");
            }
            else
            {
                return new Response(null, false, msg);
            }
        }
        
        /// <summary>
        /// Validate dữ liệu
        /// createdby:NVCHINH(11/08/2022)
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        private bool Validate(Dish dish)
        {
            // nếu mã trống
            if (ChechNull(dish.DishCode))
            {
                msg=msg+(Resources.Dish.VN_Null_Code)+",";
            }
            // nếu tên trống
            if (ChechNull(dish.DishName))
            {
                msg=msg+(Resources.Dish.VN_Null_Name)+",";
            }
            //Nếu đơn vị trống
            if (dish.UnitID==null)
            {
                msg=msg+(Resources.Dish.VN_Null_Unit)+",";
            }
            
            if (ChechNull(msg) == false)
            {
                msg = msg.TrimEnd(',');
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool checkCodeInsert(Dish dish)
        {

            if (_reposotory.CheckCode("", dish.DishCode, "dishCode") != 0)
            {
                msg = msg + Resources.Dish.VN_Dup_Code + ",";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool checkCodeUpdate(Dish dish)
        {

            if (_reposotory.CheckCode(dish.DishID.ToString(), dish.DishCode, "dishCode") != 1)
            {
                return true;
            }
            else
            {
                msg = msg + Resources.Dish.VN_Dup_Code + ",";
                return false;
            }
        }
    }
}
