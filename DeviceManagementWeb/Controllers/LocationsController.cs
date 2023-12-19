using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using DeviceManagementWeb.Services.Interfaces;



namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IDataService<LocationDto> _service;

        public LocationsController(IDataService<LocationDto> service)
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
        public ActionResult<int> Update(LocationDto request)
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

            int affectedRows = _service.Update(request);

            if (affectedRows == 0)
            {
                return NotFound("Location not found.");
            }

            return Ok(affectedRows);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            int affectedRows = _service.Delete(id);


            if (affectedRows == 0)
            {
                return NotFound("Location not found.");
            };

            return Ok(affectedRows);
        }
    }
}
