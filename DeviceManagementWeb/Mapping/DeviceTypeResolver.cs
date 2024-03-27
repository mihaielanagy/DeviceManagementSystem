using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class DeviceTypeResolver : IValueResolver<Device, DeviceDto, DeviceType>
    {
        private readonly IDataService<DeviceType> _service;

        public DeviceTypeResolver(IDataService<DeviceType> service)
        {
            _service = service;
        }

        public DeviceType Resolve(Device source, DeviceDto destination, DeviceType destMember, ResolutionContext context)
        {
            return _service.GetById(source.IdDeviceType).Data;
        }
    }
}
