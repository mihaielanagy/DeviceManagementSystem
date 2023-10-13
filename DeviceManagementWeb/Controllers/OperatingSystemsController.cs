using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemsController : ControllerBase
    {
        private readonly IOperatingSystemsService _service;

        public OperatingSystemsController(IOperatingSystemsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<DeviceManagementDB.Models.OperatingSystem>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<DeviceManagementDB.Models.OperatingSystem> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var operatingSystem = _service.GetById(id);

            if (operatingSystem == null)
                return BadRequest("Operating system not found");

            return Ok(operatingSystem);
        }

        [HttpPost]
        public ActionResult<int> Insert(DeviceManagementDB.Models.OperatingSystem request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("OS name cannot be empty");
            }

            var id = _service.Insert(request);
            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult Update(DeviceManagementDB.Models.OperatingSystem request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            if (_service.Update(request) == 0)
            {
                return NotFound("Operating System not found.");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            if (_service.Delete(id) == 0)
            {
                return NotFound("Operating System not found.");
            };

            return Ok();
        }
    }
}
