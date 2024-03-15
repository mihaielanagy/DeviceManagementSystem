using AutoMapper;
using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom<CountryResolver>());

            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom<CityResolver>()); 

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom<RoleResolver>())
                .ForMember(dest => dest.Location, opt => opt.MapFrom<LocationResolver>());

            CreateMap<Device, DeviceDto>();

        }
    }
}
