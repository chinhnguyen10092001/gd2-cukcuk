using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishMaterialController : BaseController<DishMaterial>
    {
        IDishMaterialRepository _repository;
        IDishMaterialService _service;
        public DishMaterialController(IDishMaterialRepository repository, IDishMaterialService service) : base(repository, service)
        {
            this._repository = repository;
            this._service = service;
        }
        /// <summary>
        /// INsert nhiều bản ghi
        /// </summary>
        /// <param name="material">List NVL được thêm</param>
        /// <returns>số bản ghi được thêm</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpPost("InsertAll")]
        public IActionResult Insert(List<DishMaterial> material)
        {
            return StatusCode(201, _repository.InsertAll(material));
        }
        /// <summary>
        /// Tìm kiếm material theo Dish ID
        /// </summary>
        /// <param name="DishId"></param>
        /// <returns>Mảng Materia;</returns>
        /// CreatedBy:NVCHINH(10/08/2022)
        [HttpGet("Fillter")]
        public IActionResult Get(string DishId)
        {
            return Ok(_repository.fillter(DishId));
        }
    }
}
