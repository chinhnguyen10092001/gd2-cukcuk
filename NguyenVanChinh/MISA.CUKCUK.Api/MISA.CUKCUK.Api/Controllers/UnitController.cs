using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitCOntroller : BaseController<Unit>
    {
        IUnitRepository _repository;
        IUnitService _service;
        public UnitCOntroller(IUnitRepository repository, IUnitService service) : base(repository, service)
        {
            this._repository=repository;
            this._service=service;
        }
    }
}
