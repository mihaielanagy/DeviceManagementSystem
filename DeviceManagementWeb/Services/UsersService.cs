using AutoMapper;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class UsersService : IUsersService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IDataService<LocationDto> _locationService;
        private readonly IDataService<Role> _roleService;
        public readonly IMapper _mapper;

        public UsersService(IBaseRepository<User> userRepository, IDataService<LocationDto> locationService,
            IDataService<Role> roleService, IMapper mapper)
        {
            _userRepository = userRepository;
            _locationService = locationService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public List<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                //var userDto = MapUser(user);
                var userDto = _mapper.Map<UserDto>(user);
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

            var userDto = _mapper.Map<UserDto>(user);

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

        //private UserDto MapUser(User request)
        //{
        //    var userDto = new UserDto
        //    {
        //        Id = request.Id,
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        Email = request.Email,
        //        Role = _roleService.GetById(request.IdRole),
        //        Location = _locationService.GetById(request.IdLocation)
        //    };

        //    return userDto;
        //}
    }
}
