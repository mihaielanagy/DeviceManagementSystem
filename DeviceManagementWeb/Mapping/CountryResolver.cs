using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class CountryResolver : IValueResolver<City, CityDto, Country>
    {
        private readonly IDataService<Country> _countryService;

        public CountryResolver(IDataService<Country> countryService)
        {
            _countryService = countryService;
        }

        public Country Resolve(City source, CityDto destination, Country destMember, ResolutionContext context)
        {
            return _countryService.GetById(source.IdCountry);
        }
    }
}
