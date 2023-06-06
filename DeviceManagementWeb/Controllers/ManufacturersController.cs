using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public ManufacturersController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Manufacturer>> GetAll()
        {
            return Ok(_context.Manufacturers.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Manufacturer> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var manufacturer = _context.Manufacturers.FirstOrDefault(i => i.Id == id);

            if (manufacturer == null)
                return BadRequest("Manufacturer not found");

            return Ok(manufacturer);
        }
    }
}
