using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class RamAmountResolver : IValueResolver<Device, DeviceDto, Ramamount>
    {
        private readonly IDataService<Ramamount> _service;

        public RamAmountResolver(IDataService<Ramamount> service)
        {
            _service = service;
        }

        public Ramamount Resolve(Device source, DeviceDto destination, Ramamount destMember, ResolutionContext context)
        {
            return _service.GetById(source.IdRamamount);
        }
    }
}