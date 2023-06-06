using DeviceManagementDB.Models;
using DeviceManagementWeb.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementTests.ControllersTests
{
    public class CitiesControllersTests
    {
        private DeviceManagementContext _dbContext;
        public CitiesControllersTests()
        {
            _dbContext = new DeviceManagementContext();
        }

        [Fact]
        public void CitiesControllers_GetAll()
        {
            // Arange
            var controller = new CitiesController(_dbContext);

            // Act
            var result = (OkObjectResult)controller.GetAll().Result;
            var allCities = (List<City>)result.Value;

            // Assert
            Assert.NotNull(allCities);
            Assert.NotEmpty(allCities);
        }

        [Fact]
        public void CitiesControllers_GetById()
        {
            // Arange
            var controller = new CitiesController(_dbContext);

            // Act
            var result = (OkObjectResult)controller.GetById(1).Result;
            var city = (City)result.Value;

            // Assert
            Assert.NotNull(city);
            Assert.Equal("London", city.Name);
        }
    }
}
