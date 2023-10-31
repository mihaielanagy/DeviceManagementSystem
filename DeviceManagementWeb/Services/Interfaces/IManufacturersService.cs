namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IManufacturersService
    {
        List<Manufacturer> GetAll();
        Manufacturer GetById(int id);
        int Insert(Manufacturer request);
        int Update(Manufacturer request);
        int Delete(int id);
    }
}
