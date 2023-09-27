using Microsoft.AspNetCore.Mvc;
using DeviceManagementWeb.Services.Interfaces;
using DeviceManagementWeb.Services;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        private readonly ILoggingService _loggingService;

        public RolesController(IRolesService rolesService, ILoggingService loggingService)
        {
            _rolesService = rolesService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult<List<Role>> GetAll()
        {
            return Ok(_rolesService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetById(int id)
        {
                _loggingService.LogInformation("User logged successfuly.");
            if (id <= 0)
                return BadRequest("Invalid Id");

            var role = _rolesService.GetById(id);

            if (role == null)
                return BadRequest("Role not found");

            return Ok(role);
        }
    }
}
