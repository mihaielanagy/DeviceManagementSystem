using DeviceManagementDB.Models;
using DeviceManagementWeb.Controllers;
using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementTests.ControllersTests
{
    public class DevicesControllerTests

    {
        private DeviceManagementContext _dbContext;
        public DevicesControllerTests()
        {
            _dbContext = new DeviceManagementContext();
        }
        [Fact]
        public void DevicesController_GetAll_ReturnsANonEmptyListOfDevices()
        {
            // Arange
            var controller = new DevicesController(_dbContext);

            // Act
            var result = (OkObjectResult)controller.GetAll().Result;
            var allDevices = (List<DeviceDto>)result.Value;

            // Assert
            Assert.NotNull(allDevices);
            Assert.NotEmpty(allDevices);
        }

        [Fact]
        public void DevicesController_GetById_ReturnsTheCorrectDeviceFromDb()
        {
            // Arange
            var controller = new DevicesController(_dbContext);

            // Act
            var result = (OkObjectResult)controller.GetById(1).Result;
            var device = (DeviceDto)result.Value;

            // Assert
            Assert.NotNull(device);
            Assert.Equal("Samsung Galaxy S22", device.Name);
        }
        [Fact]
        public void DevicesController_Insert_InsertsDeviceInDB_DeviceIsInDb()
        {
            // Arange
            var controller = new DevicesController(_dbContext);
            var deviceInsertDto = new DeviceInsertDto
            {
                Name = "New Device",
                IdManufacturer = 1,
                IdDeviceType = 1,
                IdOsVersion = 1,
                IdProcessor = 1,
                IdRamAmount = 1,
                IdUser = 1
            };

            // Act
            var resultInsert = (OkObjectResult)controller.Insert(deviceInsertDto).Result;
            var deviceInsertId = (int)resultInsert.Value;
            var deviceGet = _dbContext.Devices.Find(deviceInsertId);

            // Assert
            Assert.False(deviceInsertId < 1);
            Assert.Equal(deviceInsertDto.Name, deviceGet.Name);
            Assert.Equal(deviceInsertDto.IdManufacturer, deviceGet.IdManufacturer);
            Assert.Equal(deviceInsertDto.IdDeviceType, deviceGet.IdDeviceType);
            Assert.Equal(deviceInsertDto.IdOsVersion, deviceGet.IdOsversion);
            Assert.Equal(deviceInsertDto.IdProcessor, deviceGet.IdProcessor);
            Assert.Equal(deviceInsertDto.IdRamAmount, deviceGet.IdRamamount);

            //Cleanup
            _dbContext.Remove(deviceGet);
            _dbContext.SaveChanges();
        }

        [Fact]
        public void DevicesController_Update_UpdatesName_FieldIsUpdatedInDb()
        {
            // Arange
            var controller = new DevicesController(_dbContext);
            var dbDevice = _dbContext.Devices.Find(1);
            var initialName = (string)dbDevice.Name.Clone();
            dbDevice.Name = "Updated Name";
            var deviceInsertDto = new DeviceInsertDto
            {
                Name = dbDevice.Name,
                IdDeviceType = dbDevice.IdDeviceType,
                IdManufacturer = dbDevice.IdManufacturer,
                IdUser = dbDevice.IdCurrentUser,
                IdOsVersion = dbDevice.IdOsversion,
                IdProcessor = dbDevice.IdProcessor,
                IdRamAmount = dbDevice.IdRamamount
            };

            // Act
            controller.Update(deviceInsertDto);
            var deviceGet = _dbContext.Devices.Find(1);

            // Assert
            Assert.Equal(dbDevice.Name, deviceGet.Name);
            Assert.Equal(dbDevice.IdManufacturer, deviceGet.IdManufacturer);
            Assert.Equal(dbDevice.IdDeviceType, deviceGet.IdDeviceType);
            Assert.Equal(dbDevice.IdOsversion, deviceGet.IdOsversion);
            Assert.Equal(dbDevice.IdProcessor, deviceGet.IdProcessor);
            Assert.Equal(dbDevice.IdRamamount, deviceGet.IdRamamount);

            //Cleanup
            dbDevice.Name = initialName;
            _dbContext.SaveChanges();
        }

        [Fact]
        public void DevicesController_Delete_DeletsDeviceAfterBeingCreated()
        {
            // Arange
            var controller = new DevicesController(_dbContext);
            var newDevice = new Device
            {
                Name = "Delete This Device",
                IdManufacturer = 1,
                IdDeviceType = 1,
                IdOsversion = 1,
                IdProcessor = 1,
                IdRamamount = 1,
                IdCurrentUser = 1
            };
            _dbContext.Devices.Add(newDevice);
            _dbContext.SaveChanges();

            // Act
            controller.Delete(newDevice.Id);
            var dbDevice = _dbContext.Devices.Find(newDevice.Id);

            // Assert
            Assert.Null(dbDevice);
        }

        [Fact]
        public void DevicesController_Insert_NullDevice_ReturnsBadRequest()
        {
            // Arange
            var controller = new DevicesController(_dbContext);
            DeviceInsertDto deviceInsert = null;

            // Act
            var resultInsert = (BadRequestObjectResult)controller.Insert(deviceInsert).Result;

            // Assert
            Assert.Equal("Input data is invalid. Device cannot be null.", resultInsert.Value);
        }

        [Fact]
        public void DevicesController_GetById_InexistentId_ReturnsBadRequest()
        {
            // Arange
            var controller = new DevicesController(_dbContext);
            int inexistentId = _dbContext.Devices.Max(d => d.Id) + 1;

            // Act
            var resultInsert = (BadRequestObjectResult)controller.GetById(inexistentId).Result;

            // Assert
            Assert.Equal("Device not found", resultInsert.Value);
        }
    }
}

