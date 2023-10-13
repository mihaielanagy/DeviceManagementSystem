using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface ICitiesService
    {
        List<CityDto> GetAll();
        CityDto GetById(int id);
        int Insert(CityDto request);
        int Update(CityDto request);
        int Delete(int id);
    }
}
