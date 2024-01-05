using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Security.Claims;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Services
{
    public class DevicesService : IDevicesService
    {
        private readonly IBaseRepository<Device> _deviceRepository;
        private readonly IUsersService _userService;
        private readonly IDataService<DeviceType> _deviceTypeService;
        private readonly IDataService<Manufacturer> _manufacturerService;
        private readonly IDataService<OsVersionDto> _osVersionService;
        private readonly IDataService<Ramamount> _ramService;
        private readonly IDataService<Processor> _processorService;


        public DevicesService(IBaseRepository<Device> deviceRepository, IUsersService userService,
            IDataService<Processor> processorService, IDataService<Ramamount> ramService,
            IDataService<OsVersionDto> osVersionService, IDataService<Manufacturer> manufacturerService,
            IDataService<DeviceType> deviceTypeService)
        {
            _deviceRepository = deviceRepository;
            _userService = userService;
            _processorService = processorService;
            _ramService = ramService;
            _osVersionService = osVersionService;
            _manufacturerService = manufacturerService;
            _deviceTypeService = deviceTypeService;
        }

        public List<DeviceDto> GetAll()
        {
            var devices = _deviceRepository.GetAll();
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
            var device = _deviceRepository.GetById(id);
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

            _deviceRepository.Insert(device);

            return device.Id;
        }

        public int Update(DeviceInsertDto request)
        {
            if (request == null || request.Id < 1)
                return 0;

            var device = _deviceRepository.GetById(request.Id);
            if (device == null)
                return 0;

            device.Name = request.Name;
            device.IdManufacturer = request.IdManufacturer;
            device.IdProcessor = request.IdProcessor;
            device.IdDeviceType = request.IdDeviceType;
            device.IdOsversion = request.IdOsVersion;
            device.IdRamamount = request.IdRamAmount;
            device.IdCurrentUser = request.IdUser;

            int affectedRows = _deviceRepository.Update(device);

            return affectedRows;
        }
        public int Delete(int id)
        {
            if (id < 1)
                return 0;

            var device = _deviceRepository.GetById(id);
            if (device == null)
                return 0;

            int affectedRows = _deviceRepository.Delete(id);
            return affectedRows;
        }

        public int UpdateDeviceUser(int deviceId, int? userId)
        {
            var device = _deviceRepository.GetById(deviceId);
            device.IdCurrentUser = userId;
            int affectedRows = _deviceRepository.Update(device);

            return affectedRows;
        }

        private DeviceDto MapDevice(Device device)
        {
            UserDto user = null;

            if (device.IdCurrentUser != null)
            {
                user = _userService.GetById(device.IdCurrentUser ?? 0);
            }

            var deviceDto = new DeviceDto
            {
                Id = device.Id,
                Name = device.Name,
                DeviceType = _deviceTypeService.GetById(device.IdDeviceType),
                Manufacturer = _manufacturerService.GetById(device.IdManufacturer),
                OsVersion = _osVersionService.GetById(device.IdOsversion),
                Processor = _processorService.GetById(device.IdProcessor),
                RamAmount = _ramService.GetById(device.IdRamamount),
                User = user
            };
            return deviceDto;
        }
    }
}

