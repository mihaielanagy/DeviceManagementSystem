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

        [HttpPost]
        public ActionResult<int> Insert(DeviceType request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Manufacturer name cannot be empty");
            }

            var id = _deviceTypesService.Insert(request);

            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<int> Update(DeviceType request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            int rowsAffected = _deviceTypesService.Update(request);

            if (rowsAffected == 0)
            {
                return NotFound("Device Type not found.");
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

            int rowsAffected = _deviceTypesService.Delete(id);

            if (rowsAffected == 0)
            {
                return NotFound("Device Type not found.");
            };

            return Ok(rowsAffected);
        }

    }
}
