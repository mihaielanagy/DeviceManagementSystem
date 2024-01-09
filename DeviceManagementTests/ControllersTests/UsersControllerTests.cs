using DeviceManagementDB.Models;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Controllers;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementTests.ControllersTests
{
    public class UsersControllerTests
    {
        private DeviceManagementContext _dbContext;
        private BaseRepository<User> _userRepository;
        private BaseRepository<City> _cityRepository;
        private BaseRepository<Country> _countryRepository;
        private BaseRepository<Location> _locationRepository;
        private BaseRepository<Role> _roleRepository;
        private CitiesService _cityService;
        private CountriesService _countryService;
        private LocationsService _locationService;
        private RolesService _roleService;
        private UsersService _userService;

        public UsersControllerTests()
        {
            _dbContext = new DeviceManagementContext();
            _cityRepository = new BaseRepository<City>(_dbContext);
            _countryRepository = new BaseRepository<Country>(_dbContext);
            _locationRepository = new BaseRepository<Location>(_dbContext);
            _countryService = new CountriesService(_countryRepository);
            _cityService = new CitiesService(_cityRepository, _countryService);
            _locationService = new LocationsService(_locationRepository, _cityService);
            _userRepository = new BaseRepository<User>(_dbContext);
            _roleRepository = new BaseRepository<Role>(_dbContext);
            _roleService = new RolesService(_roleRepository);
            _userService = new UsersService(_userRepository, _locationService, _roleService);
        }


        [Fact]
        public void UsersController_GetAll_ReturnsANonEmptyListOfUsers()
        {
            // Arange
            var controller = new UsersController(_userService);

            // Act
            var result = (OkObjectResult)controller.GetAll().Result;
            var allUsers = (List<UserDto>)result.Value;

            // Assert
            Assert.NotNull(allUsers);
            Assert.NotEmpty(allUsers);
        }

        [Fact]
        public void UsersController_GetById_ReturnsTheCorrectUserFromDb()
        {
            // Arange
            var controller = new UsersController(_userService);

            // Act
            var result = (OkObjectResult)controller.GetById(1).Result;
            var user = (UserDto)result.Value;

            // Assert
            Assert.NotNull(user);
            Assert.Equal("john.doe@darwinmail.com", user.Email);
        }
        [Fact]
        public void UsersController_Insert_InsertsUserInDB_UserIsInDb()
        {
            // Arange
            var controller = new UsersController(_userService);
            var userInsert = new UserInsertDto
            {
                FirstName = "New",
                LastName = "User",
                Email = "new.user@darwinemail.com",
                Password = "password123!",
                IdLocation = 1,
                IdRole = 1
            };

            // Act
            var resultInsert = (OkObjectResult)controller.Insert(userInsert).Result;
            var userInsertId = (int)resultInsert.Value;
            var userGet = _dbContext.Users.Find(userInsertId);

            // Assert
            Assert.False(userInsertId < 1);
            Assert.Equal(userInsert.Email, userGet.Email);
            Assert.Equal(userInsert.FirstName, userGet.FirstName);
            Assert.Equal(userInsert.LastName, userGet.LastName);
            Assert.Equal(userInsert.IdRole, userGet.IdRole);
            Assert.Equal(userInsert.IdLocation, userGet.IdLocation);

            //Cleanup
            _dbContext.Remove(userGet);
            _dbContext.SaveChanges();
        }

        [Fact]
        public void UsersController_Update_UpdatesLastName_FieldIsUpdatedInDb()
        {
            // Arange
            var controller = new UsersController(_userService);
            var dbUser = _dbContext.Users.Find(1);
            var initialLastName = (string)dbUser.LastName.Clone();
            var userInsertDto = new UserInsertDto
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = "Updated Name",
                Email = dbUser.Email,
                Password = dbUser.Password,
                IdLocation = dbUser.IdLocation,
                IdRole = dbUser.IdRole,
            };

            // Act
            controller.Update(userInsertDto);
            var userGet = _dbContext.Users.Find(1);


            // Assert
            Assert.Equal(dbUser.LastName, userGet.LastName);
            Assert.Equal(dbUser.Email, userGet.Email);
            Assert.Equal(dbUser.FirstName, userGet.FirstName);
            Assert.Equal(dbUser.IdRole, userGet.IdRole);
            Assert.Equal(dbUser.IdLocation, userGet.IdLocation);

            //Cleanup
            dbUser.LastName = initialLastName;
            _dbContext.SaveChanges();
        }

        [Fact]
        public void UsersController_Delete_DeletsUserAfterBeingCreated()
        {
            // Arange
            var controller = new UsersController(_userService);
            var newUser = new User
            {
                FirstName = "Delete",
                LastName = "ThisUser",
                Email = "delete.thisuser@darwinemail.com",
                Password = "password123!",
                IdLocation = 1,
                IdRole = 1
            };
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            // Act
            controller.Delete(newUser.Id);
            var dbUser = _dbContext.Users.Find(newUser.Id);

            // Assert
            Assert.Null(dbUser);
        }

        [Fact]
        public void UsersController_Insert_InvalidEmailAddress_ReturnsBadRequest()
        {
            // Arange
            var controller = new UsersController(_userService);
            var userInsert = new UserInsertDto
            {
                FirstName = "New",
                LastName = "User",
                Email = "invalidemail",
                Password = "password123!",
                IdLocation = 1,
                IdRole = 1
            };

            // Act
            var resultInsert = (BadRequestObjectResult)controller.Insert(userInsert).Result;

            // Assert
            Assert.Equal("Invalid email", resultInsert.Value);
        }

        [Fact]
        public void UsersController_Insert_InvalidPassword_ReturnsBadRequest()
        {
            // Arange
            var controller = new UsersController(_userService);
            var userInsert = new UserInsertDto
            {
                FirstName = "New",
                LastName = "User",
                Email = "new.user@darwinemail.com",
                Password = "short",
                IdLocation = 1,
                IdRole = 1
            };

            // Act
            var resultInsert = (BadRequestObjectResult)controller.Insert(userInsert).Result;

            // Assert
            Assert.Equal("Password too short. Must contain at least 8 characters", resultInsert.Value);
        }
    }
}


