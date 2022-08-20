using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    /// <summary>
    /// Controller xử lý liên quan đến món ăn
    /// </summary>
    /// Created by: NVCHINH (08/08/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : BaseController<Dish>
    {
        IDishRepository _repository;
        IDishService _service;
        #region Contructor
        public DishController(IDishRepository repository, IDishService service) : base(repository, service)
        {
            this._repository = repository;
            this._service = service;
        }
        /// <summary>
        /// Thêm món ăn đầy đủ
        /// </summary>
        /// <param name="data"></param>
        /// <returns>số món ăn đc thêm</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpPost]
        public override  IActionResult Insert(Dish data)
        {
            try
            {
                List<DishMaterial> list = data.list;
                data.list = null;
                var res = _service.InsertService(data,list);
                if (res.Success)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(400, res);
                }
                
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageIndex">Số thứ tự trang</param>
        /// <param name="sort">Câu lênh sort </param>
        /// <param name="where">Câu lênh tìm kiếm</param>
        /// <returns>Mảng dữ liệu đã được phân trang,tìm kiếm</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpGet("Fillter")]

        public IActionResult Fillter(int pageIndex, int pageSize, string? where, string? sort)
        {
            try
            {
                // Lấy data
                int totalRecord = 0;
                var data = _repository.Fillter(pageIndex, pageSize, where, sort, ref totalRecord);
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                if (pageSize == 0)
                {
                    pageSize = 50;
                }
                var res = new
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize,
                    totalRecord = totalRecord,
                    data = data
                };
                // Trả về data
                return Ok(new Response(res,true,""));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Xóa món ăn 
        /// </summary>
        /// <param name="dish"></param>
        /// <returns>Số lượng món ăn được xóa</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpDelete]
        public IActionResult Delete(Dish dish)
        {
            try
            {
               
                // Lấy data
                var data = _repository.Delete(dish);

                // Trả về data
                return StatusCode(200, new Response(data, true, ""));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa món ăn 
        /// </summary>
        /// <param name="dish">món ăn được sửa</param>
        /// <returns>số lượng món ăn đc sửa/returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpPut]
        public  IActionResult Update(Dish dish)
        {
            try
            {
                List<DishMaterial> list = dish.list;
                dish.list = null;
                // Lấy data
                var res = _service.UpdateService(dish,list);
                // Trả về data
                if (res.Success)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(400, res);
                }
                
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Thêm ảnh
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            string fileName;
            try
            {
                // tạo đuôi file ảnh
                var extention = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extention;
                // tạo thư mục
                var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Images");
                // nếu thư mục đã tòn tại thì bỏ qua
                if (!Directory.Exists(pathBuild))
                {
                    Directory.CreateDirectory(pathBuild);
                }
                // lấy path
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Images", fileName);
                // đẩy ảnh vào theo path
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                return Ok(fileName);
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion
    }
}
