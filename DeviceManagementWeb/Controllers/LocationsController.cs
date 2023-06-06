using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public LocationsController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Location>> GetAll()
        {
            return Ok(_context.Locations.Include(c=>c.IdCityNavigation).ThenInclude(c=>c.IdCountryNavigation).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var location = _context.Locations.Include(c => c.IdCityNavigation).ThenInclude(c => c.IdCountryNavigation).FirstOrDefault(i => i.Id == id);

            if (location == null)
                return BadRequest("Location not found");

            return Ok(location);
        }
    }
}