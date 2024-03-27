using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypesController : ControllerBase
    {
        private readonly IDataService<DeviceType> _deviceTypesService;

        public DeviceTypesController(IDataService<DeviceType> deviceTypesService)
        {
            _deviceTypesService = deviceTypesService;
        }

        [HttpGet]
        public ActionResult<List<DeviceType>> GetAll()
        {
            var serviceResp = _deviceTypesService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<DeviceType> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _deviceTypesService.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(DeviceType request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Manufacturer name cannot be empty");

            var serviceResp = _deviceTypesService.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(DeviceType request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");


            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name cannot be empty.");

            var serviceResp = _deviceTypesService.Update(request);

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

            var serviceResp = _deviceTypesService.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }
    }
}
