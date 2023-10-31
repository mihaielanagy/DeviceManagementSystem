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

        [HttpPost]
        public ActionResult<int> Insert(Role request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Role name cannot be empty");
            }

            var id = _rolesService.Insert(request);

            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<int> Update(Role request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Role name cannot be empty.");
            }

            int rowsAffected = _rolesService.Update(request);

            if (rowsAffected == 0)
            {
                return NotFound("Role not found.");
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

            int rowsAffected = _rolesService.Delete(id);

            if (rowsAffected == 0)
            {
                return NotFound("Role not found.");
            };

            return Ok(rowsAffected);
        }

    }
}
