using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class ProcessorResolver : IValueResolver<Device, DeviceDto, Processor>
    {
        private readonly IDataService<Processor> _service;

        public ProcessorResolver(IDataService<Processor> service)
        {
            _service = service;
        }

        public Processor Resolve(Device source, DeviceDto destination, Processor destMember, ResolutionContext context)
        {
            return _service.GetById(source.IdProcessor).Data;
        }
    }
}