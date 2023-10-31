using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IDataService<CityDto> _service;

        public CitiesController(IDataService<CityDto> service)
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
        public ActionResult<int> Update(CityDto request)
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

            int rowsAffected = _service.Update(request);
            if (rowsAffected == 0)
            {
                return NotFound("City not found.");
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
                return NotFound("City not found");
            };

            return Ok(rowsAffected);
        }
    }
}
