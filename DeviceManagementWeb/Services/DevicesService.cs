using AutoMapper;
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
        private readonly IMapper _mapper;


        public DevicesService(IBaseRepository<Device> deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public ServiceResponse<List<DeviceDto>> GetAll()
        {
            var devices = _deviceRepository.GetAll();
            var devicesDto = new List<DeviceDto>();

            foreach (var device in devices)
            {
                DeviceDto deviceDto = _mapper.Map<DeviceDto>(device);
                devicesDto.Add(deviceDto);
            }

            return new ServiceResponse<List<DeviceDto>>(devicesDto, true);
        }

        public ServiceResponse<DeviceDto> GetById(int id)
        {
            var device = _deviceRepository.GetById(id);
            if (device == null)
                return new ServiceResponse<DeviceDto>(null, false,"Device not found");

            DeviceDto deviceDto = _mapper.Map<DeviceDto>(device);
            return new ServiceResponse<DeviceDto>(deviceDto, true);
        }

        public ServiceResponse<int> Insert(DeviceInsertDto request)
        {
            if (request == null)
                return new ServiceResponse<int>(0, false, "Device cannot be null");

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
            return new ServiceResponse<int>(device.Id, true);
        }

        public ServiceResponse<int> Update(DeviceInsertDto request)
        {
            if (request == null)
                return new ServiceResponse<int>(0, false, "Device cannot be null");

            var device = _deviceRepository.GetById(request.Id);
            if (device == null)
                return new ServiceResponse<int>(0, false, "Device not found");

            device.Name = request.Name;
            device.IdManufacturer = request.IdManufacturer;
            device.IdProcessor = request.IdProcessor;
            device.IdDeviceType = request.IdDeviceType;
            device.IdOsversion = request.IdOsVersion;
            device.IdRamamount = request.IdRamAmount;
            device.IdCurrentUser = request.IdUser;

            int affectedRows = _deviceRepository.Update(device);
            return new ServiceResponse<int>(affectedRows, true);
        }
        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var device = _deviceRepository.GetById(id);
            if (device == null)
                return new ServiceResponse<int>(0, false, "Device not found");

            int affectedRows = _deviceRepository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> UpdateDeviceUser(int deviceId, int? userId)
        {
            var device = _deviceRepository.GetById(deviceId);
            device.IdCurrentUser = userId;
            int affectedRows = _deviceRepository.Update(device);

            return new ServiceResponse<int>(affectedRows, true);
        }

        //private DeviceDto MapDevice(Device device)
        //{
        //    UserDto user = null;

        //    if (device.IdCurrentUser != null)
        //    {
        //        user = _userService.GetById(device.IdCurrentUser ?? 0);
        //    }

        //    var deviceDto = new DeviceDto
        //    {
        //        Id = device.Id,
        //        Name = device.Name,
        //        DeviceType = _deviceTypeService.GetById(device.IdDeviceType),
        //        Manufacturer = _manufacturerService.GetById(device.IdManufacturer),
        //        OsVersion = _osVersionService.GetById(device.IdOsversion),
        //        Processor = _processorService.GetById(device.IdProcessor),
        //        RamAmount = _ramService.GetById(device.IdRamamount),
        //        User = user
        //    };
        //    return deviceDto;
        //}
    }
}

