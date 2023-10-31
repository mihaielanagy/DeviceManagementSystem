using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

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
        public ActionResult<List<OperatingSystem>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<OperatingSystem> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var operatingSystem = _service.GetById(id);

            if (operatingSystem == null)
                return BadRequest("Operating system not found");

            return Ok(operatingSystem);
        }

        [HttpPost]
        public ActionResult<int> Insert(OperatingSystem request)
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
        public ActionResult<int> Update(OperatingSystem request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            int rowsAffected = _service.Update(request);

            if (rowsAffected == 0)
            {
                return NotFound("Operating System not found.");
            }

            return Ok(rowsAffected);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            int rowsAffected = _service.Delete(id);

            if (rowsAffected == 0)
            {
                return NotFound("Operating System not found.");
            };

            return Ok(rowsAffected);
        }
    }
}
