global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public UsersController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            var users = _context.Users.ToList();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                Location loc = _context.Locations.Find(user.IdLocation);
                City city = _context.Cities.Find(loc.IdCity);
                Country country = _context.Countries.Find(city.IdCountry);

                var userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = _context.Roles.Find(user.IdRole),
                    Location = new LocationDto { Id = loc.Id, Address = loc.Address, City = new CityDto { Id = city.Id, Country = country } }
                };

                usersDto.Add(userDto);
            }

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Id");

            var user = _context.Users.Find(id);
            if (user == null)
                return BadRequest("User not found");
            Location loc = _context.Locations.Find(user.IdLocation);
            City city = _context.Cities.Find(loc.IdCity);
            Country country = _context.Countries.Find(city.IdCountry);

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = _context.Roles.Find(user.IdRole),
                Location = new LocationDto { Id = loc.Id, Address = loc.Address, City = new CityDto { Id = city.Id, Country = country } }
            };

            return Ok(userDto);
        }

        [HttpPost]
        public ActionResult<int> Insert(UserInsertDto userInsertDto)
        {
            if (!EmailIsValid(userInsertDto.Email))
                return BadRequest("Invalid email");

            if (!PasswordIsValid(userInsertDto.Password))
                return BadRequest("Password too short. Must contain at least 8 characters");

            User user = new User
            {
                FirstName = userInsertDto.FirstName,
                LastName = userInsertDto.LastName,
                Email = userInsertDto.Email,
                Password = userInsertDto.Password,
                IdLocation = userInsertDto.IdLocation,
                IdRole = userInsertDto.IdRole
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user.Id);
        }

        [HttpPut]
        public ActionResult<User> Update(UserInsertDto request)
        {
            var user = _context.Users.Find(request.Id);
            if (user == null)
                return BadRequest("User not found");

            if (!EmailIsValid(user.Email))
                return BadRequest("Invalid email");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdRole = request.IdRole;
            user.IdLocation = request.IdLocation;
            _context.SaveChanges();

            return Ok(_context.Users.Find(user.Id));
        }

        [HttpDelete("{id}")]
        public ActionResult<List<User>> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Id");

            var user = _context.Users.Find(id);
            if (user == null)
                return BadRequest("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(_context.Users.ToList());
        }

        private bool EmailIsValid(string email) => email.Contains("@") && email.Contains(".");
        private bool PasswordIsValid(string password) => password.Length >= 8;

    }
}
