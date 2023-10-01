using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypesController : ControllerBase
    {
        private readonly IDeviceTypesService _deviceTypesService;

        public DeviceTypesController(IDeviceTypesService deviceTypesService)
        {
            _deviceTypesService = deviceTypesService;
        }

        [HttpGet]
        public ActionResult<List<DeviceType>> GetAll()
        {
            return Ok(_deviceTypesService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<DeviceType> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var deviceType = _deviceTypesService.GetById(id);

            if (deviceType == null)
                return BadRequest("Device type not found");

            return Ok(deviceType);
        }
    }
}
