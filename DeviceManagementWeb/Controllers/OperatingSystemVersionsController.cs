using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatingSystemVersionsController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public OperatingSystemVersionsController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<OperatingSystemVersion>> GetAll()
        {
            return Ok(_context.OperatingSystemVersions.Include(o => o.IdOsNavigation).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<OperatingSystemVersion> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var operatingSystemVersion = _context.OperatingSystemVersions.Include(o => o.IdOsNavigation).FirstOrDefault(i => i.Id == id);

            if (operatingSystemVersion == null)
                return BadRequest("Operating system version not found");

            return Ok(operatingSystemVersion);
        }
    }
}
