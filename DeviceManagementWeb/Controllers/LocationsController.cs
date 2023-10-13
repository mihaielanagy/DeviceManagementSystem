using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using DeviceManagementWeb.Services.Interfaces;



namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationsController(ILocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LocationDto>> GetAll()
        {
            var locations = _service.GetAll();

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public ActionResult<LocationDto> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var location = _service.GetById(id);

            if (location == null)
                return BadRequest("Location not found");

            return Ok(location);
        }

        [HttpPost]
        public ActionResult<int> Insert(LocationDto request)
        {
            if (string.IsNullOrEmpty(request.Address))
            {
                return BadRequest("Address cannot be empty");
            }

            if (request.City == null)
            {
                return BadRequest("City cannot be null.");
            }

            var locId = _service.Insert(request);
            if (locId == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(locId);
        }

        [HttpPut]
        public ActionResult Update(LocationDto request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Location id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Address))
            {
                return BadRequest("Address cannot be empty.");
            }

            if (request.City == null)
            {
                return BadRequest("City cannot be null.");
            }

            if (_service.Update(request) == 0)
            {
                return NotFound("Location not found.");
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
                return NotFound("Location not found.");
            };

            return Ok();
        }
    }
}
