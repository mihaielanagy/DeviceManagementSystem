global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;


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
        public ActionResult<DeviceDto> GetById(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return BadRequest("Device not found");

            DeviceDto deviceDto = MapDevice(device);

            return Ok(deviceDto);
        }


        [HttpPost]
        public ActionResult<Device> Insert(DeviceInsertDto request)
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
                IdCurrentUser = request.IdUser,
                IdOsversion = request.IdOsVersion,
                IdProcessor = request.IdProcessor,
                IdRamamount = request.IdRamAmount
            };

            _context.Devices.Add(device);
            _context.SaveChanges();

            return Ok(device.Id);
        }

        [HttpPut]
        public ActionResult<Device> Update(DeviceInsertDto request)
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

            return Ok(_context.Devices.Find(device.Id));
        }

        [HttpDelete("{id}")]
        public ActionResult<List<Device>> Delete(int id)
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

            return Ok(_context.Devices.ToList());
        }

        private DeviceDto MapDevice(Device device)
        {
            User user = null;
            if (device.IdCurrentUser != null)
            {
                user = _context.Users.Find(device.IdCurrentUser);
            }

            var deviceDto = new DeviceDto
            {
                Id = device.Id,
                Name = device.Name,
                DeviceType = _context.DeviceTypes.Find(device.IdDeviceType),
                Manufacturer = _context.Manufacturers.Find(device.IdManufacturer),
                OsVersion = _context.OperatingSystemVersions.Find(device.IdOsversion),
                Processor = _context.Processors.Find(device.IdProcessor),
                RamAmount = _context.Ramamounts.Find(device.IdRamamount),
                User = user != null ? new UserDto
                {
                    Email = user.Email,
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = _context.Roles.Find(user.IdRole),
                    Location = _context.Locations.Find(user.IdLocation)
                } : null
            };
            return deviceDto;
        }
    }
}
