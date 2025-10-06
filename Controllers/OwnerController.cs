using FuerzaG.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuerzaG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerService _service;

        public OwnerController(OwnerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            var owners = _service.GetAllOwners();
            return Ok(owners);
        }
    }
}
