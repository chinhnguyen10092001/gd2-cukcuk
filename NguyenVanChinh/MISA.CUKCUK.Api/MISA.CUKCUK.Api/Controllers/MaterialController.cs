using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaterialController : BaseController<Material>
    {
        IMaterialRepository _repository;
        IMaterialService _service;
        public MaterialController(IMaterialRepository repository, IMaterialService service) : base(repository, service)
        {
            this._repository = repository;
            this._service = service;
        }
    }
}
