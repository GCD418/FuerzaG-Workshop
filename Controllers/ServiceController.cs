using FuerzaG.Models;
using FuerzaG.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuerzaG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceServices _service;

        public ServiceController(ServiceServices service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            var services = _service.GetAllServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceById(short id)
        {
            var service = _service.GetServiceById(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [HttpPost]
        public IActionResult AddService([FromBody] Service service)
        {
            _service.AddService(service);
            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(short id, [FromBody] Service service)
        {
            var existing = _service.GetServiceById(id);
            if (existing == null)
                return NotFound();

            service.Id = id;
            _service.UpdateService(service);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(short id)
        {
            var existing = _service.GetServiceById(id);
            if (existing == null)
                return NotFound();

            _service.DeleteService(id);
            return NoContent();
        }
    }
}
