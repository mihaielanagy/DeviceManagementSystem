using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class UsersService : IUsersService
    {
        private readonly DeviceManagementContext _context;

        public UsersService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<UserDto> GetAll()
        {
            var users = _context.Users.ToList();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = MapUser(user);
                usersDto.Add(userDto);
            }

            return usersDto;
        }

        public UserDto GetById(int id)
        {
            if (id < 1)
               return null;

            var user = _context.Users.Find(id);
            if (user == null)
                return null;

            var userDto = MapUser(user);

            return userDto;
        }

        public UserDto GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            var userDto = MapUser(user);

            return userDto;
        }
        public int Insert(UserInsertDto userInsertDto)
        {
            if (!EmailIsValid(userInsertDto.Email))
                return 0;

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == userInsertDto.Email);
            if (existingUser != null)
                return 0;
            //throw exceptions instead of returning 0?

            if (!PasswordIsValid(userInsertDto.Password))
                return 0;

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

            return user.Id;
        }

        public int Update(UserInsertDto request)
        {
            var user = _context.Users.Find(request.Id);
            if (user == null)
                return 0;

            if (!EmailIsValid(user.Email))
                return 0;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdRole = request.IdRole;
            user.IdLocation = request.IdLocation;
            

            return _context.SaveChanges(); ;
        }

        public int Delete(int id)
        {
            if (id < 1)
                return 0;

            var user = _context.Users.Find(id);
            if (user == null)
                return 0;

            _context.Users.Remove(user);
            

            return _context.SaveChanges();
        }

        private bool EmailIsValid(string email) => email.Contains("@") && email.Contains(".");
        private bool PasswordIsValid(string password) => password.Length >= 8;

        private UserDto MapUser(User request)
        {
            Location loc = _context.Locations.Find(request.IdLocation);
            City city = _context.Cities.Find(loc.IdCity);
            Country country = _context.Countries.Find(city.IdCountry);

            var userDto = new UserDto
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = _context.Roles.Find(request.IdRole),
                Location = new LocationDto { Id = loc.Id, Address = loc.Address, City = new CityDto { Id = loc.IdCity, Name = city.Name, Country = country } }
            };

            return userDto;
        }
    }
}
