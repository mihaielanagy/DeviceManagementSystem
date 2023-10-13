using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemVersionsController : ControllerBase
    {
        private readonly IOsVersionService _service;

        public OperatingSystemVersionsController(IOsVersionService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<OperatingSystemVersion>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<OsVersionDto> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var operatingSystemVersion = _service.GetById(id);

            if (operatingSystemVersion == null)
                return BadRequest("Operating system version not found");

            return Ok(operatingSystemVersion);
        }

        [HttpPost]
        public ActionResult<int> Insert(OsVersionDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("OS Version name cannot be empty");
            }

            var id = _service.Insert(request);
            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult Update(OsVersionDto request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            if(request.OS == null)
            {
                return BadRequest("Operating System cannot be null.");
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
                return NotFound("Operating System Version not found.");
            };

            return Ok();
        }
    }
}
