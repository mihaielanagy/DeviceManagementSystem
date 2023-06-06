using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public RolesController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Role>> GetAll()
        {
            return Ok(_context.Roles.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var role = _context.Roles.FirstOrDefault(i => i.Id == id);

            if (role == null)
                return BadRequest("Role not found");

            return Ok(role);
        }
    }
}
