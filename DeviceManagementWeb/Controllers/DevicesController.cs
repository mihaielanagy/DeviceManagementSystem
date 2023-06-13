global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
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
        private readonly DeviceManagementContext _context;

        public DevicesController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<DeviceDto>> GetAll()
        {
            var devices = _context.Devices.ToList();
            var devicesDto = new List<DeviceDto>();

            foreach (var device in devices)
            {
                DeviceDto deviceDto = MapDevice(device);

                devicesDto.Add(deviceDto);
            }

            return Ok(devicesDto);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<DeviceDto> GetById(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return BadRequest("Device not found");

            DeviceDto deviceDto = MapDevice(device);

            return Ok(deviceDto);
        }


        [HttpPost]
        [Authorize]
        public ActionResult<int> Insert(DeviceInsertDto request)
        {
            if (request == null)
            {
                return BadRequest("Input data is invalid. Device cannot be null.");
            }

            var device = new Device
            {
                Name = request.Name,
                IdDeviceType = request.IdDeviceType,
                IdManufacturer = request.IdManufacturer,
                IdOsversion = request.IdOsVersion,
                IdProcessor = request.IdProcessor,
                IdRamamount = request.IdRamAmount,
            };

            _context.Devices.Add(device);
            _context.SaveChanges();

            return Ok(device.Id);
        }

        [HttpPut]
        [Authorize]
        public ActionResult<int> Update(DeviceInsertDto request)
        {
            if (request == null || request.Id < 1)
            {
                return BadRequest("Id is invalid");
            }

            var device = _context.Devices.Find(request.Id);
            if (device == null)
                return BadRequest("Device not found");

            device.Name = request.Name;
            device.IdManufacturer = request.IdManufacturer;
            device.IdProcessor = request.IdProcessor;
            device.IdDeviceType = request.IdDeviceType;
            device.IdOsversion = request.IdOsVersion;
            device.IdRamamount = request.IdRamAmount;
            device.IdCurrentUser = request.IdUser;

            _context.Devices.Update(device);
            _context.SaveChanges();

            return Ok(device.Id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<List<DeviceDto>> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            var device = _context.Devices.Find(id);
            if (device == null)
                return BadRequest("Device not found");

            _context.Devices.Remove(device);
            _context.SaveChanges();

            return Ok(GetAll().Result);
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

            var device = _context.Devices.Find(id);
            var user = _context.Users.Find(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (device == null)
            {
                return BadRequest("Device not found");
            }

           
            device.IdCurrentUser = user.Id;

            _context.Devices.Update(device);
            _context.SaveChanges();

            return Ok(device.Id);
        }

        [HttpPut("unassign/{id}")]
        [Authorize]
        public ActionResult<int> UnassignDevice(int id)
        {
            if (id < 1)
            {
                return BadRequest("Device id is invalid");
            }

            var device = _context.Devices.Find(id);

            if (device == null)
            {
                return BadRequest("Device not found");
            }

            device.IdCurrentUser = null;

            _context.Devices.Update(device);
            _context.SaveChanges();

            return Ok(device.Id);
        }

        private DeviceDto MapDevice(Device device)
        {
            User user = null;
            Location loc = null;
            City city = null;
            Country country = null;
            if (device.IdCurrentUser != null)
            {
                user = _context.Users.Find(device.IdCurrentUser);
                loc = _context.Locations.Find(user.IdLocation);
                city = _context.Cities.Find(loc.IdCity);
                country = _context.Countries.Find(city.IdCountry);
            }
            OperatingSystemVersion osv = _context.OperatingSystemVersions.Find(device.IdOsversion);
            OperatingSystem os = _context.OperatingSystems.Find(osv.IdOs);


            var deviceDto = new DeviceDto
            {
                Id = device.Id,
                Name = device.Name,
                DeviceType = _context.DeviceTypes.Find(device.IdDeviceType),
                Manufacturer = _context.Manufacturers.Find(device.IdManufacturer),
                OsVersion = new OsVersionDto { Id = osv.Id, Name = osv.Name, OS = os },
                Processor = _context.Processors.Find(device.IdProcessor),
                RamAmount = _context.Ramamounts.Find(device.IdRamamount),
                User = user != null ? new UserDto
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = _context.Roles.Find(user.IdRole),
                    Location = new LocationDto { Id = loc.Id, Address = loc.Address, City = new CityDto { Id = city.Id, Country = country } },
                } : null
            };
            return deviceDto;
        }
    }
}
