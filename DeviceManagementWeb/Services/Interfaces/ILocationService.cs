using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface ILocationService
    {
        List<LocationDto> GetAll();
        LocationDto GetById(int id);

        int Insert(LocationDto request);
        int Update(LocationDto request);
        int Delete(int id);
    }
}
