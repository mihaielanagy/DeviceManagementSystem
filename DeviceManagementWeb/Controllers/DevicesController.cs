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
            var serviceResp = _devicesService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DeviceDto> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Id is invalid.");

            var serviceResp = _devicesService.GetById(id);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }


        [HttpPost]
        [Authorize]
        public ActionResult<int> Insert(DeviceInsertDto request)
        {
            if (request == null)
                return BadRequest("Input data is invalid. Device cannot be null.");

            var serviceResp = _devicesService.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        [Authorize]
        public ActionResult<int> Update(DeviceInsertDto request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid");

            if (request == null)
                return BadRequest("Device cannot be null");


            var serviceResp = _devicesService.Update(request);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Id is invalid");


            var serviceResp = _devicesService.Delete(id);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut("assign/{id}")]
        [Authorize]
        public ActionResult<int> AssignDeviceToCurrentUser(int id)
        {
            if (id < 1)
                return BadRequest("Device id is invalid");


            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return BadRequest("User id not found");


            var userId = int.Parse(userIdClaim.Value);

            if (userId < 1)
                return BadRequest("User id is invalid");

            var deviceResp = _devicesService.GetById(id);
            var userResp = _usersService.GetById(userId);

            if (userResp.Data == null)
                return BadRequest("User not found");

            if (deviceResp.Data == null)
                return NotFound("Device not found");

            var assignResp = _devicesService.UpdateDeviceUser(deviceResp.Data.Id, userResp.Data.Id);
            if (assignResp.IsSuccess == false)
                return BadRequest($"An error has occured: {assignResp.ErrorMessage}");
            else
                return Ok(assignResp.Data);
        }

        [HttpPut("unassign/{id}")]
        [Authorize]
        public ActionResult<int> UnassignDevice(int id)
        {
            if (id < 1)
                return BadRequest("Device id is invalid");

            var device = _devicesService.GetById(id).Data;

            if (device == null)
                return BadRequest("Device not found");

            var serviceResp = _devicesService.UpdateDeviceUser(device.Id, null);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }
    }
}
