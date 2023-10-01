namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IDeviceTypesService
    {
        List<DeviceType> GetAll();
        DeviceType GetById(int id);
    }
}
