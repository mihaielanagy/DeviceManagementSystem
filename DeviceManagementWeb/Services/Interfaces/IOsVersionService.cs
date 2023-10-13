using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IOsVersionService
    {
        List<OsVersionDto> GetAll();
        OsVersionDto GetById(int id);

        int Insert(OsVersionDto request);
        int Update(OsVersionDto request);
        int Delete(int id);
    }
}
