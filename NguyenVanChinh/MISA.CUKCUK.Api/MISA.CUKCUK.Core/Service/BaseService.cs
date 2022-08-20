using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    /// <summary>
    /// Lớp service cha
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: NVCHINH (08/08/2022)

    
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// Thêm bản ghi
        /// createdBy:NVCHINH
        /// </summary>
        /// <param name="entity">Dối tượng dc thêm</param>
        /// <returns>số bản ghi đc thêm</returns>
        public virtual IEnumerable<T> InsertService(T entity)
        {
            var res = _repository.Insert(entity);
            return res;
        }
        
        /// <summary>
        /// CHeck rỗng
        /// CreatedBy:NVCHINH (10/08/2022)
        /// </summary>
        /// <param name="text">text cần chẹck</param>
        /// <returns>true:rỗng,false:khoogn rỗng</returns>
        public bool ChechNull(string text)
        {
            return string.IsNullOrEmpty(text) ? true : false;
        }
        
    }
}
