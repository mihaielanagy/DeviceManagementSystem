using DeviceManagementDB.Models;
using DeviceManagementWeb.Controllers;
using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementTests.ControllersTests
{
    public class LocationsControllerTests
    {
        private DeviceManagementContext _dbContext;
        public LocationsControllerTests()
        {
            _dbContext = new DeviceManagementContext();
        }

        [Fact]
        public void LocationsController_GetAll()
        {
            // Arange
            var controller = new LocationsController(_dbContext);

            // Act
            var result = (OkObjectResult)controller.GetAll().Result;
            var allLocations = (List<LocationDto>)result.Value;

            // Assert
            Assert.NotNull(allLocations);
            Assert.NotEmpty(allLocations);
        }

        [Fact]
        public void LocationController_GetById()
        {
            // Arange
            var controller = new LocationsController(_dbContext);

            // Act
            var result = (OkObjectResult)controller.GetById(1).Result;
            var location = (Location)result.Value;

            // Assert
            Assert.NotNull(location);
            Assert.Equal("1 Tower Place West, Tower Place", location.Address);
        }
    }
}
