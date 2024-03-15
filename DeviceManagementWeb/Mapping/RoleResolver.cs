using AutoMapper;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;

namespace DeviceManagementWeb.Mapping
{    public class RoleResolver : IValueResolver<User, UserDto, Role>
    {
        private readonly IDataService<Role> _roleService;

        public RoleResolver(IDataService<Role> roleService)
        {
            _roleService = roleService;
        }

        public Role Resolve(User source, UserDto destination, Role destMember, ResolutionContext context)
        {
            // Fetch the Role from the database using _roleService.GetById
            return _roleService.GetById(source.IdRole);
        }
    }
}
