using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IUsersService
    {
        ServiceResponse<List<UserDto>> GetAll();
        ServiceResponse<UserDto> GetById(int id);
        // UserDto GetByEmail(string email);
        ServiceResponse<int> Insert(UserInsertDto userInsertDto);
        ServiceResponse<int> Update(UserInsertDto request);
        ServiceResponse<int> Delete(int id);
    }
}
