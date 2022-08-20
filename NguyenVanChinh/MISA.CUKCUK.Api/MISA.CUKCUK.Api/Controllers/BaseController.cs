using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Exceptions;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using MISA.CUKCUK.Core.Resources;

namespace MISA.CUKCUK.Api.Controllers
{
    /// <summary>
    /// Lớp controller cơ sở
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: NVCHINH (07/08/2022)
    [Route("api/v1/'[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Variable
        IBaseRepository<T> _repository;
        IBaseService<T> _service;
        #endregion

        #region Contructor
        public BaseController(IBaseRepository<T> repository, IBaseService<T> service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion
        #region Controller
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Mảng dữ liệu</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Lấy data
                var data = _repository.Get();

                // Trả về data
                return Ok(new Response(data,true,""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Check trùng thuộc tính
        /// </summary>
        /// <returns>0:Code khoogn tồn tại,1:code tồn tại,ID không tt, 2: Tồn tại Code và ID</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpGet("CheckCode")]
        public IActionResult CheckCode(string? id,string code,string colum)
        {
            try
            {
                // Lấy data
                var data = _repository.CheckCode(id, code, colum);
                return StatusCode(200,data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi được thêm</param>
        /// <returns>sô bản ghi được thêm</returns>
        /// CreatedBy:NVCHINH(10/08/2022)

        [HttpPost]
        public virtual  IActionResult Insert(T entity)
        {
            try
            {
                // Lấy data
                var data = _service.InsertService(entity);

                // Trả về data
                return StatusCode(201,data);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion
        /// <summary>
        /// Hàm refull
        /// </summary>
        /// <param name="ex">mã lỗi</param>
        /// <returns>lỗi</returns>
        protected IActionResult HandleException(Exception ex)
        {
            // nếu ex validate dữ liệu thì trả về res 400
            if (ex is CukcukException)
            {
                var res = new
                {
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
            // nếu khoogn trả về 500
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.NV_Show_Error
                };
                return StatusCode(500, res);
            }
        }
        
    }
}
