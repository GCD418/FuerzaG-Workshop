using FuerzaG.Models;
using FuerzaG.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuerzaG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnicianController : ControllerBase
    {
        private readonly TechnicianService _service;

        public TechnicianController(TechnicianService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllTechnicians()
        {
            var technicians = _service.GetAllTechnicians();
            return Ok(technicians);
        }

        [HttpGet("{id}")]
        public IActionResult GetTechnicianById(short id)
        {
            var tech = _service.GetTechnicianById(id);
            if (tech == null)
                return NotFound();
            return Ok(tech);
        }

        [HttpPost]
        public IActionResult AddTechnician([FromBody] Technician tech)
        {
            _service.AddTechnician(tech);
            return CreatedAtAction(nameof(GetTechnicianById), new { id = tech.Id }, tech);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTechnician(short id, [FromBody] Technician tech)
        {
            var existing = _service.GetTechnicianById(id);
            if (existing == null)
                return NotFound();

            tech.Id = id;
            _service.UpdateTechnician(tech);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTechnician(short id)
        {
            var existing = _service.GetTechnicianById(id);
            if (existing == null)
                return NotFound();

            _service.DeleteTechnician(id);
            return NoContent();
        }
    }
}
