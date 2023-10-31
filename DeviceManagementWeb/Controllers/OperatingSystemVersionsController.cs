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
                return BadRequest("OS Version not found");

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
        public ActionResult<int> Update(OsVersionDto request)
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
                return BadRequest("OS Version cannot be null.");
            }

            int rowsAffected = _service.Update(request);
            if (rowsAffected == 0)
            {
                return NotFound("OS Version not found.");
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
                return NotFound("OS Version not found.");
            };

            return Ok(rowsAffected);
        }
    }
}
