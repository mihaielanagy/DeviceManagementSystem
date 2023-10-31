namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IProcessorsService
    {
        List<Processor> GetAll();
        Processor GetById(int id);
        int Insert(Processor request);
        int Update(Processor request);
        int Delete(int id);
    }
}
