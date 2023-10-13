namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IOperatingSystemsService
    {
        List<DeviceManagementDB.Models.OperatingSystem> GetAll();
        DeviceManagementDB.Models.OperatingSystem GetById(int id);

        int Insert(DeviceManagementDB.Models.OperatingSystem request);
        int Update(DeviceManagementDB.Models.OperatingSystem request);
        int Delete(int id);
    }
}
