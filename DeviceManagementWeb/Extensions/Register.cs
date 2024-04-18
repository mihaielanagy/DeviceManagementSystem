using AutoMapper;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using DeviceManagementWeb.Services.Interfaces;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementWeb.Extensions
{
    public static class Register
    {
        public static void RegisterServices(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddSingleton<ILoggingService, LoggingService>();

            serviceProvider.AddTransient<IUsersService, UsersService>();
            serviceProvider.AddTransient<IDevicesService, DevicesService>();

            serviceProvider.AddTransient<IDataService<CityDto>, CitiesService>();
            serviceProvider.AddTransient<IDataService<Country>, CountriesService>();
            serviceProvider.AddTransient<IDataService<DeviceType>, DeviceTypesService>();
            serviceProvider.AddTransient<IDataService<Manufacturer>, ManufacturersService>();
            serviceProvider.AddTransient<IDataService<OperatingSystem>, OperatingSystemsService>();
            serviceProvider.AddTransient<IDataService<Processor>, ProcessorsService>();
            serviceProvider.AddTransient<IDataService<Ramamount>, RamAmountsService>();
            serviceProvider.AddTransient<IDataService<Role>, RolesService>();
            serviceProvider.AddTransient<IDataService<OsVersionDto>, OSVersionsService>();
            serviceProvider.AddTransient<IDataService<LocationDto>, LocationsService>();

            serviceProvider.AddTransient<ITokenService, TokenService>();
        }

        public static void RegisterRepositories(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<IBaseRepository<City>, BaseRepository<City>>();
            serviceProvider.AddTransient<IBaseRepository<Country>, BaseRepository<Country>>();
            serviceProvider.AddTransient<IBaseRepository<DeviceType>, BaseRepository<DeviceType>>();
            serviceProvider.AddTransient<IBaseRepository<Manufacturer>, BaseRepository<Manufacturer>>();
            serviceProvider.AddTransient<IBaseRepository<OperatingSystem>, BaseRepository<OperatingSystem>>();
            serviceProvider.AddTransient<IBaseRepository<Processor>, BaseRepository<Processor>>();
            serviceProvider.AddTransient<IBaseRepository<Ramamount>, BaseRepository<Ramamount>>();
            serviceProvider.AddTransient<IBaseRepository<Role>, BaseRepository<Role>>();
            serviceProvider.AddTransient<IBaseRepository<OperatingSystemVersion>, BaseRepository<OperatingSystemVersion>>();
            serviceProvider.AddTransient<IBaseRepository<Location>, BaseRepository<Location>>();
            serviceProvider.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            serviceProvider.AddTransient<IBaseRepository<Device>, BaseRepository<Device>>();
        }
    }
}
