global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesService _devicesService;
        private readonly IUsersService _usersService;

        public DevicesController(IDevicesService devicesService, IUsersService usersService)
        {
            _devicesService = devicesService;
            _usersService = usersService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<DeviceDto>> GetAll()
        {
            return Ok(_devicesService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DeviceDto> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id is invalid.");
            var device = _devicesService.GetById(id);
            if(device == null)
                return NotFound("Device not found");

            return Ok(device);
        }


        [HttpPost]
        [Authorize]
        public ActionResult<int> Insert(DeviceInsertDto request)
        {
            if (request == null)
            {
                return BadRequest("Input data is invalid. Device cannot be null.");
            }

            int id = _devicesService.Insert(request);
            if (id == 0)
                return BadRequest("An error has occured.");

            return Ok(id);
        }

        [HttpPut]
        [Authorize]
        public ActionResult<int> Update(DeviceInsertDto request)
        {
            if (request == null || request.Id < 1)
            {
                return BadRequest("Id is invalid");
            }

            int affectedRows = _devicesService.Update(request);
            if (affectedRows == 0)
                return BadRequest("Device not found");

            return Ok(affectedRows);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            int affectedRows = _devicesService.Delete(id);
            if (affectedRows == 0)
                return BadRequest("Device not found");

            return Ok(affectedRows);
        }

        [HttpPut("assign/{id}")]
        [Authorize]
        public ActionResult<int> AssignDeviceToCurrentUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("Device id is invalid");
            }

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return BadRequest("User id not found");
            }

            var userId = int.Parse(userIdClaim.Value);

            if (userId < 1)
            {
                return BadRequest("User id is invalid");
            }

            var device = _devicesService.GetById(id);
            var user = _usersService.GetById(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (device == null)
            {
                return BadRequest("Device not found");
            }

            int affectedRows = _devicesService.UpdateDeviceUser(device.Id, user.Id);
            if (affectedRows == 0)
                return BadRequest("The current user couldn't be assigned.");


            return Ok(affectedRows);
        }

        [HttpPut("unassign/{id}")]
        [Authorize]
        public ActionResult<int> UnassignDevice(int id)
        {
            if (id < 1)
            {
                return BadRequest("Device id is invalid");
            }

            var device = _devicesService.GetById(id);

            if (device == null)
            {
                return BadRequest("Device not found");
            }

            int affectedRows = _devicesService.UpdateDeviceUser(device.Id, null);
            if (affectedRows == 0)
                return BadRequest("An error has occured.");

            return Ok(affectedRows);
        }
    }
}
