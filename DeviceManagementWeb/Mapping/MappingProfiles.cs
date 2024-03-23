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

            CreateMap<OperatingSystemVersion, OsVersionDto>()
                .ForMember(dest => dest.OS, opt => opt.MapFrom<OperatingSystemResolver>());

            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.DeviceType, opt => opt.MapFrom<DeviceTypeResolver>())
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom<ManufacturerResolver>())
                .ForMember(dest => dest.OsVersion, opt => opt.MapFrom<OSVersionResolver>())
                .ForMember(dest => dest.Processor, opt => opt.MapFrom<ProcessorResolver>())
                .ForMember(dest => dest.RamAmount, opt => opt.MapFrom<RamAmountResolver>())
                .ForMember(dest => dest.User, opt => opt.MapFrom<UserResolver>());
        }
    }
}
