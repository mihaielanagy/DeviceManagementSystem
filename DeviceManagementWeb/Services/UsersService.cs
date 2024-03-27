using AutoMapper;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Services
{
    public class UsersService : IUsersService
    {
        private readonly IBaseRepository<User> _userRepository;
        public readonly IMapper _mapper;

        public UsersService(IBaseRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ServiceResponse<List<UserDto>> GetAll()
        {
            var users = _userRepository.GetAll();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                //var userDto = MapUser(user);
                var userDto = _mapper.Map<UserDto>(user);
                usersDto.Add(userDto);
            }

            return new ServiceResponse<List<UserDto>>(usersDto, true);
        }

        public ServiceResponse<UserDto> GetById(int id)
        {
            if (id < 1)
                return new ServiceResponse<UserDto>(null, false, "Invalid id");

            var user = _userRepository.GetById(id);
            if (user == null)
                return new ServiceResponse<UserDto>(null, false, "User not found");

            var userDto = _mapper.Map<UserDto>(user);
            return new ServiceResponse<UserDto>(userDto, true);
        }

        //public UserDto GetByEmail(string email)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);

        //    if (user == null)
        //        return null;

        //    var userDto = MapUser(user);

        //    return userDto;
        //}
        public ServiceResponse<int> Insert(UserInsertDto userInsertDto)
        {
            if (!EmailIsValid(userInsertDto.Email))
                return new ServiceResponse<int>(0, false, "Email is invalid");

            //var existingUser = _context.Users.FirstOrDefault(u => u.Email == userInsertDto.Email);
            // if (existingUser != null)
            //return 0;


            if (!PasswordIsValid(userInsertDto.Password))
                return new ServiceResponse<int>(0, false, "Password is invalid");

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
            return new ServiceResponse<int>(user.Id, true);
        }

        public ServiceResponse<int> Update(UserInsertDto request)
        {
            var user = _userRepository.GetById(request.Id);
            if (user == null)
                return new ServiceResponse<int>(0, false, "User not found");

            if (!EmailIsValid(user.Email))
                return new ServiceResponse<int>(0, false, "Email is invalid");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdRole = request.IdRole;
            user.IdLocation = request.IdLocation;

            int affectedRows = _userRepository.Update(user);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
                return new ServiceResponse<int>(0, false, "Invalid id");

            var user = _userRepository.GetById(id);
            if (user == null)
                return new ServiceResponse<int>(0, false, "User not found");

            int affectedRows = _userRepository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
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
