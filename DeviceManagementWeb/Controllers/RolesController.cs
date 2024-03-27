using Microsoft.AspNetCore.Mvc;
using DeviceManagementWeb.Services.Interfaces;
using DeviceManagementWeb.Services;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IDataService<Role> _rolesService;
        private readonly ILoggingService _loggingService;

        public RolesController(IDataService<Role> rolesService, ILoggingService loggingService)
        {
            _rolesService = rolesService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public ActionResult<List<Role>> GetAll()
        {
            var serviceResp = _rolesService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<Role> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _rolesService.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(Role request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Role name cannot be empty");

            var serviceResp = _rolesService.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(Role request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Role name cannot be empty.");

            var serviceResp = _rolesService.Update(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            var serviceResp = _rolesService.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }
    }
}
