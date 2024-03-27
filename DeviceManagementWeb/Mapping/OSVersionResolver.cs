using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class OSVersionResolver : IValueResolver<Device, DeviceDto, OsVersionDto>
    {
        private readonly IDataService<OsVersionDto> _service;
        private readonly IMapper _mapper;

        public OSVersionResolver(IDataService<OsVersionDto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public OsVersionDto Resolve(Device source, DeviceDto destination, OsVersionDto destMember, ResolutionContext context)
        {

            var osv = _service.GetById(source.IdOsversion).Data;
            return _mapper.Map<OsVersionDto>(osv);
        }
    }

}
