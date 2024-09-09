//using DeviceManagementDB.Models;
//using DeviceManagementDB.Repositories;
//using DeviceManagementWeb.Controllers;
//using DeviceManagementWeb.DTOs;
//using DeviceManagementWeb.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace DeviceManagementTests.ControllersTests
//{
//    public class LocationsControllerTests
//    {
//        private DeviceManagementContext _dbContext;
//        private CitiesService _cityService;
//        private CountriesService _countryService;
//        private LocationsService _locationService;
//        private BaseRepository<City> _cityRepository;
//        private BaseRepository<Country> _countryRepository;
//        private BaseRepository<Location> _locationRepository;
           
//        public LocationsControllerTests()
//        {
//            _dbContext = new DeviceManagementContext();
//            _cityRepository = new BaseRepository<City>(_dbContext);
//            _countryRepository = new BaseRepository<Country>(_dbContext);
//            _locationRepository = new BaseRepository<Location>(_dbContext);
//            _countryService = new CountriesService(_countryRepository);
//            _cityService = new CitiesService(_cityRepository, _countryService);
//            _locationService = new LocationsService(_locationRepository, _cityService);
//        }

//        [Fact]
//        public void LocationsController_GetAll()
//        {
//            // Arange
//            var controller = new LocationsController(_locationService);

//            // Act
//            var result = (OkObjectResult)controller.GetAll().Result;
//            var allLocations = (List<LocationDto>)result.Value;

//            // Assert
//            Assert.NotNull(allLocations);
//            Assert.NotEmpty(allLocations);
//        }

//        [Fact]
//        public void LocationController_GetById()
//        {
//            // Arange
//            var controller = new LocationsController(_locationService);

//            // Act
//            var result = (OkObjectResult)controller.GetById(1).Result;
//            var location = (LocationDto)result.Value;

//            // Assert
//            Assert.NotNull(location);
//            Assert.Equal("1 Tower Place West, Tower Place", location.Address);
//        }
//    }
//}
