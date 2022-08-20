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
    public class DishMaterialService : BaseService<DishMaterial>, IDishMaterialService
    {
        public DishMaterialService(IBaseRepository<DishMaterial> repository) : base(repository)
        {
        }
    }
}
