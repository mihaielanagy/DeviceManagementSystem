namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IProcessorsService
    {
        List<Processor> GetAll();
        Processor GetById(int id);
    }
}
