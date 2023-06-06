using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamAmountsController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public RamAmountsController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Ramamount>> GetAll()
        {
            return Ok(_context.Ramamounts.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Ramamount> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var ramAmount = _context.Ramamounts.FirstOrDefault(i => i.Id == id);

            if (ramAmount == null)
                return BadRequest("ramAmount not found");

            return Ok(ramAmount);
        }
    }
}