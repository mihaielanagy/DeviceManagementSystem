using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypesController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public DeviceTypesController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<DeviceType>> GetAll()
        {
            return Ok(_context.DeviceTypes.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DeviceType> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var deviceType = _context.DeviceTypes.FirstOrDefault(i => i.Id == id);

            if (deviceType == null)
                return BadRequest("Device type not found");

            return Ok(deviceType);
        }
    }
}
