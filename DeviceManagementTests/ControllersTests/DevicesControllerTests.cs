using DeviceManagementDB.Models;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Controllers;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using OperatingSystem = DeviceManagementDB.Models.OperatingSystem;

namespace DeviceManagementTests.ControllersTests
{
    public class DevicesControllerTests

    {
        private DeviceManagementContext _dbContext;
        private BaseRepository<User> _userRepository;
        private BaseRepository<City> _cityRepository;
        private BaseRepository<Country> _countryRepository;
        private BaseRepository<Location> _locationRepository;
        private BaseRepository<Role> _roleRepository;
        private BaseRepository<Device> _deviceRepository;
        private BaseRepository<DeviceType> _deviceTypeRepository;
        private BaseRepository<Manufacturer> _manufacturerRepository;
        private BaseRepository<OperatingSystem> _osRepository;
        private BaseRepository<OperatingSystemVersion> _osVersionRepository;
        private BaseRepository<Processor> _processorRepository;
        private BaseRepository<Ramamount> _ramRepository;

        private CitiesService _cityService;
        private CountriesService _countryService;
        private LocationsService _locationService;
        private RolesService _roleService;
        private DevicesService _deviceService;
        private DeviceTypesService _deviceTypeService;
        private ManufacturersService _manufacturerService;
        private OperatingSystemsService _osService;
        private OSVersionsService _osVersionService;
        private ProcessorsService _processorService;
        private RamAmountsService _ramService;
        private UsersService _userService;

        public DevicesControllerTests()
        {
            _dbContext = new DeviceManagementContext();

            _deviceTypeRepository = new BaseRepository<DeviceType>(_dbContext);
            _manufacturerRepository = new BaseRepository<Manufacturer>(_dbContext);
            _osRepository = new BaseRepository<OperatingSystem>(_dbContext);
            _osVersionRepository = new BaseRepository<OperatingSystemVersion>(_dbContext);
            _processorRepository = new BaseRepository<Processor>(_dbContext);
            _ramRepository = new BaseRepository<Ramamount>(_dbContext);
            _cityRepository = new BaseRepository<City>(_dbContext);
            _countryRepository = new BaseRepository<Country>(_dbContext);
            _locationRepository = new BaseRepository<Location>(_dbContext);
            _userRepository = new BaseRepository<User>(_dbContext);
            _roleRepository = new BaseRepository<Role>(_dbContext);
            _deviceRepository = new BaseRepository<Device>(_dbContext);

            _deviceTypeService = new DeviceTypesService(_deviceTypeRepository);
            _manufacturerService = new ManufacturersService(_manufacturerRepository);
            _osService = new OperatingSystemsService(_osRepository);
            _osVersionService = new OSVersionsService(_osVersionRepository, _osService);
            _processorService = new ProcessorsService(_processorRepository);
            _ramService = new RamAmountsService(_ramRepository);
            _countryService = new CountriesService(_countryRepository);
            _cityService = new CitiesService(_cityRepository, _countryService);
            _locationService = new LocationsService(_locationRepository, _cityService);
            _roleService = new RolesService(_roleRepository);
            //_userService = new UsersService(_userRepository, _locationService, _roleService);
            _deviceService = new DevicesService(_deviceRepository, _userService, _processorService, _ramService,
                _osVersionService, _manufacturerService, _deviceTypeService);


        }
        [Fact]
        public void DevicesController_GetAll_ReturnsANonEmptyListOfDevices()
        {
            // Arange
            var controller = new DevicesController(_deviceService, _userService);

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
            var controller = new DevicesController(_deviceService, _userService);
            var dbDevice = _dbContext.Devices.FirstOrDefault();

            // Act
            var result = (OkObjectResult)controller.GetById(dbDevice.Id).Result;
            var expectedDevice = _dbContext.Devices.Find(dbDevice.Id);
            var actualDevice = (DeviceDto)result.Value;

            // Assert
            Assert.NotNull(actualDevice);
            Assert.Equal(expectedDevice.Name, actualDevice.Name);
        }

        [Fact]
        public void DevicesController_Insert_InsertsDeviceInDB_DeviceIsInDb()
        {
            // Arange
            var controller = new DevicesController(_deviceService, _userService);
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
            var controller = new DevicesController(_deviceService, _userService);
            var dbDevice = _dbContext.Devices.FirstOrDefault();
            var idDevice = dbDevice.Id;
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
            var deviceGet = _dbContext.Devices.Find(idDevice);

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
            var controller = new DevicesController(_deviceService, _userService);
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
            var controller = new DevicesController(_deviceService, _userService);
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
            var controller = new DevicesController(_deviceService, _userService);
            int inexistentId = _dbContext.Devices.Max(d => d.Id) + 1;

            // Act
            var resultInsert = (NotFoundObjectResult)controller.GetById(inexistentId).Result;

            // Assert
            Assert.Equal("Device not found", resultInsert.Value);
        }
    }
}

