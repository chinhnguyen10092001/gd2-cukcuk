using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishGroupController : BaseController<DishGroup>
    {
        public DishGroupController(IDishGroupRepository repository, IDishGroupService service) : base(repository, service)
        {
        }
    }
}
