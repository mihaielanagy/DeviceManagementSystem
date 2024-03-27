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

        /// <summary>
        ///  Get a list of all cities in the database
        /// </summary>


        [HttpGet]
        public ActionResult<List<CityDto>> GetAll()
        {
            var serviceResp = _service.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);


        }
        /// <summary>
        ///  Get one city from the database
        /// </summary>

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _service.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            else
                return Ok(serviceResp.Data);
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

            var serviceResp = _service.Insert(request);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(CityDto request)
        {
            if (request.Id < 1)
                return BadRequest("City id is invalid.");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("City name cannot be empty.");

            if (request.Country == null)
                return BadRequest("Country cannot be null.");


            var serviceResp = _service.Update(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }


        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Id is invalid");

            var serviceResp = _service.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);
        }
    }
}
