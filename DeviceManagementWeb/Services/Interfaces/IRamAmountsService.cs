namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IRamAmountsService
    {
        List<Ramamount> GetAll();
        Ramamount GetById(int id);
    }
}
