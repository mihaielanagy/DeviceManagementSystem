using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IUsersService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        // UserDto GetByEmail(string email);
        int Insert(UserInsertDto userInsertDto);
        int Update(UserInsertDto request);
        int Delete(int id);
    }
}
