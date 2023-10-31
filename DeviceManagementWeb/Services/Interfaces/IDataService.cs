namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IDataService<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        int Insert(T request);
        int Update(T request);
        int Delete(int id);
    }
}
