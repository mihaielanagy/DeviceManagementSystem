using DeviceManagementDB.Models;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Controllers;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementTests.ControllersTests
{
    public class CitiesControllersTests
    {
        private DeviceManagementContext _dbContext;

        private CitiesService _cityService;
        private CountriesService _countryService;
        private BaseRepository<City> _cityRepository;
        private BaseRepository<Country> _countryRepository;
        public CitiesControllersTests()
        {
            _dbContext = new DeviceManagementContext();
            _cityRepository = new BaseRepository<City>(_dbContext);
            _countryRepository = new BaseRepository<Country>(_dbContext);
            _countryService = new CountriesService(_countryRepository);
            _cityService = new CitiesService(_cityRepository, _countryService);
        }

        [Fact]
        public void CitiesControllers_GetAll()
        {
            // Arange
            var controller = new CitiesController(_cityService);

            // Act
            var result = (OkObjectResult)controller.GetAll().Result;
            var allCities = (List<CityDto>)result.Value;

            // Assert
            Assert.NotNull(allCities);
            Assert.NotEmpty(allCities);
        }

        [Fact]
        public void CitiesControllers_GetById()
        {
            // Arange
            var controller = new CitiesController(_cityService);

            // Act
            var result = (OkObjectResult)controller.GetById(1).Result;
            var city = (CityDto)result.Value;

            // Assert
            Assert.NotNull(city);
            Assert.Equal("London", city.Name);
        }
    }
}
