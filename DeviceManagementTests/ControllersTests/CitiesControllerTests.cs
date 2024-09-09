using DeviceManagementDB.Models;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Controllers;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeviceManagementTests.ControllersTests
{
    public class CitiesControllersTests
    {
        private readonly Mock<IDataService<CityDto>> _mockDataService;
        private readonly CitiesController _controller;

        public CitiesControllersTests()
        {
            _mockDataService = new Mock<IDataService<CityDto>>();
            _controller = new CitiesController(_mockDataService.Object);

        }

        [Fact]
        public void CitiesControllers_GetAll()
        {
            // Arange

            // Act
            var result = (OkObjectResult)_controller.GetAll().Result;
            var allCities = (List<CityDto>)result.Value;

            // Assert
            Assert.NotNull(allCities);
            Assert.NotEmpty(allCities);
        }

        [Fact]
        public void CitiesControllers_GetById()
        {
            // Arange
           

            // Act
            var result = (OkObjectResult)_controller.GetById(1).Result;
            var city = (CityDto)result.Value;

            // Assert
            Assert.NotNull(city);
            Assert.Equal("London", city.Name);
        }
    }
}
