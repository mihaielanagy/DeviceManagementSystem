using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemVersionsController : ControllerBase
    {
        private readonly IDataService<OsVersionDto> _service;

        public OperatingSystemVersionsController(IDataService<OsVersionDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<OperatingSystemVersion>> GetAll()
        {
            var serviceResp = _service.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<OsVersionDto> GetById(int id)
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
        public ActionResult<int> Insert(OsVersionDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("OS Version name cannot be empty");

            var serviceResp = _service.Insert(request);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(OsVersionDto request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name cannot be empty.");

            if(request.OS == null)
                return BadRequest("OS Version cannot be null.");

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
