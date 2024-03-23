using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class ManufacturerResolver : IValueResolver<Device, DeviceDto, Manufacturer>
    {
        private readonly IDataService<Manufacturer> _service;

        public ManufacturerResolver(IDataService<Manufacturer> service)
        {
            _service = service;
        }

        public Manufacturer Resolve(Device source, DeviceDto destination, Manufacturer destMember, ResolutionContext context)
        {
            return _service.GetById(source.IdDeviceType);
        }
    }
}