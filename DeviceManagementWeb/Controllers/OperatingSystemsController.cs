using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemsController : ControllerBase
    {
        private readonly IDataService<OperatingSystem> _service;

        public OperatingSystemsController(IDataService<OperatingSystem> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<OperatingSystem>> GetAll()
        {
            var serviceResp = _service.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<OperatingSystem> GetById(int id)
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
        public ActionResult<int> Insert(OperatingSystem request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("OS name cannot be empty");

            var serviceResp = _service.Insert(request);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(OperatingSystem request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name cannot be empty.");

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
