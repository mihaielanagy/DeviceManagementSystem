namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IManufacturersService
    {
        List<Manufacturer> GetAll();
        Manufacturer GetById(int id);
    }
}
