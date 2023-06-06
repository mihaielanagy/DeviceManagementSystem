using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemsController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public OperatingSystemsController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<DeviceManagementDB.Models.OperatingSystem>> GetAll()
        {
            return Ok(_context.OperatingSystems.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<DeviceManagementDB.Models.OperatingSystem> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var operatingSystem = _context.OperatingSystems.FirstOrDefault(i => i.Id == id);

            if (operatingSystem == null)
                return BadRequest("Operating system not found");

            return Ok(operatingSystem);
        }
    }
}
