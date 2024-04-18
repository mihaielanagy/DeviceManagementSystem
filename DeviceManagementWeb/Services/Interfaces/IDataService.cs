using DeviceManagementWeb.DTOs;

namespace DeviceManagementWeb.Services.Interfaces
{
    public interface IDataService<T> where T : class
    {
        ServiceResponse<List<T>> GetAll();
        ServiceResponse<T> GetById(int id);
        ServiceResponse<int> Insert(T request);
        ServiceResponse<int> Update(T request);
        ServiceResponse<int> Delete(int id);
    }
}
