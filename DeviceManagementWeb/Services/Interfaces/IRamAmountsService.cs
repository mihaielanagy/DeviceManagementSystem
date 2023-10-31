namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IRamAmountsService
    {
        List<Ramamount> GetAll();
        Ramamount GetById(int id);
        int Insert(Ramamount request);
        int Update(Ramamount request);
        int Delete(int id);
    }
}
