using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Mapping
{
    public class OperatingSystemResolver : IValueResolver<OperatingSystemVersion, OsVersionDto, OperatingSystem>
    {
        private readonly IDataService<OperatingSystem> _service;

        public OperatingSystemResolver(IDataService<OperatingSystem> service)
        {
            _service = service;
        }

        public OperatingSystem Resolve(OperatingSystemVersion source, OsVersionDto destination, OperatingSystem destMember, ResolutionContext context)
        {
            return _service.GetById(source.IdOs);
        }
    }
}