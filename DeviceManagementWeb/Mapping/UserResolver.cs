using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{
    public class UserResolver : IValueResolver<Device, DeviceDto, UserDto>
    {
        private readonly IUsersService _service;
        private readonly IMapper _mapper;

        public UserResolver(IUsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public UserDto Resolve(Device source, DeviceDto destination, UserDto destMember, ResolutionContext context)
        {
            var user = _service.GetById(source.IdCurrentUser ?? 0);
            return _mapper.Map<UserDto>(user);
        }
    }

}
