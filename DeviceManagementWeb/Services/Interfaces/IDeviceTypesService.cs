namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IDeviceTypesService
    {
        List<DeviceType> GetAll();
        DeviceType GetById(int id);
        int Insert(DeviceType request);
        int Update(DeviceType request);
        int Delete(int id);
    }
}
