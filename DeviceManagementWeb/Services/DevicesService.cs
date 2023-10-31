using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Security.Claims;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly DeviceManagementContext _context;

        public DevicesService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<DeviceDto> GetAll()
        {
            var devices = _context.Devices.ToList();
            var devicesDto = new List<DeviceDto>();

            foreach (var device in devices)
            {
                DeviceDto deviceDto = MapDevice(device);
                devicesDto.Add(deviceDto);
            }

            return devicesDto;
        }

        public DeviceDto GetById(int id)
        {
            var device = _context.Devices.Find(id);
            if (device == null)
                return null;

            DeviceDto deviceDto = MapDevice(device);

            return deviceDto;
        }

        public int Insert(DeviceInsertDto request)
        {
            if (request == null)
            {
                return 0;
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

            return device.Id;
        }

        public int Update(DeviceInsertDto request)
        {
            if (request == null || request.Id < 1)
                return 0;

            var device = _context.Devices.Find(request.Id);
            if (device == null)
                return 0;

            device.Name = request.Name;
            device.IdManufacturer = request.IdManufacturer;
            device.IdProcessor = request.IdProcessor;
            device.IdDeviceType = request.IdDeviceType;
            device.IdOsversion = request.IdOsVersion;
            device.IdRamamount = request.IdRamAmount;
            device.IdCurrentUser = request.IdUser;

            _context.Devices.Update(device);

            return _context.SaveChanges(); ;
        }
        public int Delete(int id)
        {
            if (id < 1)
                return 0;

            var device = _context.Devices.Find(id);
            if (device == null)
                return 0;

            _context.Devices.Remove(device);   
            

            return _context.SaveChanges();
        }

        public int UpdateDeviceUser(int deviceId, int? userId)
        {
            var device = _context.Devices.Find(deviceId);
            device.IdCurrentUser = userId;
            _context.Devices.Update(device);

            return _context.SaveChanges();
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

