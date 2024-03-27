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
            var serviceResp = _service.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<LocationDto> GetById(int id)
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
        public ActionResult<int> Insert(LocationDto request)
        {
            if (string.IsNullOrEmpty(request.Address))
                return BadRequest("Address cannot be empty");


            if (request.City == null)
                return BadRequest("City cannot be null.");

            var serviceResp = _service.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(LocationDto request)
        {
            if (request.Id < 1)
                            return BadRequest("Location id is invalid.");
            
            if (string.IsNullOrEmpty(request.Address))
                            return BadRequest("Address cannot be empty.");
           
            if (request.City == null)
                            return BadRequest("City cannot be null.");
           
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
            else
                return Ok(serviceResp.Data);
        }
    }
}
