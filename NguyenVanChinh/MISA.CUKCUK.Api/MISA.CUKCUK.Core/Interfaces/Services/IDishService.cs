using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Services
{
    /// <summary>
    /// Interface c/ho các service của bảng món ăn
    /// </summary>/
    /// Created by: NVCHINH (08/08/2022)
    public interface IDishService : IBaseService<Dish>
    {
        Response InsertService(Dish dish, List<DishMaterial> list);
        Response UpdateService(Dish dish, List<DishMaterial> list);
    }
}
