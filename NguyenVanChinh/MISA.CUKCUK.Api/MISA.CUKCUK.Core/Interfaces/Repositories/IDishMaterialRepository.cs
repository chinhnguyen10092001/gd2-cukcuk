using MISA.CUKCUK.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Repositories
{
    public interface IDishMaterialRepository : IBaseRepository<DishMaterial>
    {
        int InsertAll(List<DishMaterial> material);
        IEnumerable<DishMaterial> fillter(string dishId);
    }
}
