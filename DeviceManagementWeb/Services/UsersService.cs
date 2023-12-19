using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class UsersService : IUsersService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Location> _locationRepository;
        private readonly IBaseRepository<City> _cityRepository;
        private readonly IBaseRepository<Country> _countryRepository;
        private readonly IBaseRepository<Role> _roleRepository;

        public UsersService(IBaseRepository<User> userRepository, IBaseRepository<Country> countryRepository, 
            IBaseRepository<City> cityRepository, IBaseRepository<Location> locationRepository, IBaseRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _locationRepository = locationRepository;
            _roleRepository = roleRepository;
        }

        public List<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
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

            var user = _userRepository.GetById(id);
            if (user == null)
                return null;

            var userDto = MapUser(user);

            return userDto;
        }

        //public UserDto GetByEmail(string email)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);

        //    if (user == null)
        //        return null;

        //    var userDto = MapUser(user);

        //    return userDto;
        //}
        public int Insert(UserInsertDto userInsertDto)
        {
            if (!EmailIsValid(userInsertDto.Email))
                return 0;

            //var existingUser = _context.Users.FirstOrDefault(u => u.Email == userInsertDto.Email);
           // if (existingUser != null)
                //return 0;
            

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

            _userRepository.Insert(user);

            return user.Id;
        }

        public int Update(UserInsertDto request)
        {
            var user = _userRepository.GetById(request.Id);
            if (user == null)
                return 0;

            if (!EmailIsValid(user.Email))
                return 0;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdRole = request.IdRole;
            user.IdLocation = request.IdLocation;
            
            int affectedRows = _userRepository.Update(user);
            return affectedRows;
        }

        public int Delete(int id)
        {
            if (id < 1)
                return 0;

            var user = _userRepository.GetById(id);
            if (user == null)
                return 0;

            int affectedRows = _userRepository.Delete(id);            

            return affectedRows;
        }

        private bool EmailIsValid(string email) => email.Contains("@") && email.Contains(".");
        private bool PasswordIsValid(string password) => password.Length >= 8;

        private UserDto MapUser(User request)
        {
            Location loc = _locationRepository.GetById(request.IdLocation);
            City city = _cityRepository.GetById(loc.IdCity);
            Country country = _countryRepository.GetById(city.IdCountry);

            var userDto = new UserDto
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = _roleRepository.GetById(request.IdRole),
                Location = new LocationDto { Id = loc.Id, Address = loc.Address, City = new CityDto { Id = loc.IdCity, Name = city.Name, Country = country } }
            };

            return userDto;
        }
    }
}
