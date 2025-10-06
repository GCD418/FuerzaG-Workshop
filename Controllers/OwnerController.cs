using FuerzaG.Models;
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

        [HttpGet("{id}")]
        public IActionResult GetOwnerById(short id)
        {
            var owner = _service.GetOwnerById(id);
            if (owner == null)
                return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        public IActionResult AddOwner([FromBody] Owner owner)
        {
            _service.AddOwner(owner);
            return CreatedAtAction(nameof(GetOwnerById), new { id = owner.Id }, owner);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(short id, [FromBody] Owner owner)
        {
            var existing = _service.GetOwnerById(id);
            if (existing == null)
                return NotFound();

            owner.Id = id;
            _service.UpdateOwner(owner);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(short id)
        {
            var existing = _service.GetOwnerById(id);
            if (existing == null)
                return NotFound();

            _service.DeleteOwner(id);
            return NoContent();
        }
    }
}
