using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class LocationResolver : IValueResolver<User, UserDto, LocationDto>
    {
        private readonly IDataService<LocationDto> _locationService;
        private readonly IMapper _mapper;

        public LocationResolver(IDataService<LocationDto> locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        public LocationDto Resolve(User source, UserDto destination, LocationDto destMember, ResolutionContext context)
        {
            
            var loc =  _locationService.GetById(source.IdLocation).Data;
            return _mapper.Map<LocationDto>(loc);
        }
    }

}
