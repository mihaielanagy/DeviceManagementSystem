using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _service;

        public CitiesController(ICitiesService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<CityDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var city = _service.GetById(id);

            if (city == null)
                return NotFound("City not found");

            return Ok(city);
        }

        [HttpPost]
        public ActionResult<int> Insert(CityDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("City name cannot be empty");
            }

            if (request.Country == null)
            {
                return BadRequest("Country cannot be null.");
            }

            var cityId = _service.Insert(request);
            if (cityId == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(cityId);
        }

        [HttpPut]
        public ActionResult Update(CityDto request)
        {
            if (request.Id < 1)
            {
                return BadRequest("City id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("City name cannot be empty.");
            }

            if (request.Country == null)
            {
                return BadRequest("Country cannot be null.");
            }

            if (_service.Update(request) == 0)
            {
                return NotFound("City not found.");
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
                return NotFound("City not found");
            };

            return Ok();
        }
    }
}
