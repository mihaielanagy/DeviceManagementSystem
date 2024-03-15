using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class CityResolver : IValueResolver<Location, LocationDto, CityDto>
    {
        private readonly IDataService<CityDto> _cityService;
        private readonly IMapper _mapper;

        public CityResolver(IDataService<CityDto> cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        public CityDto Resolve(Location source, LocationDto destination, CityDto destMember, ResolutionContext context)
        {
            var city =  _cityService.GetById(source.IdCity);

            return _mapper.Map<CityDto>(city);
        }
    }
}
