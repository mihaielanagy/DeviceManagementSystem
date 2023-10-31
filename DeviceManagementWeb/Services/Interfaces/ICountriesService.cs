namespace DeviceManagementWeb.Services.Interfaces
{
    public interface ICountriesService
    {
        List<Country> GetAll();
        Country GetById(int id);
        int Insert(string name);

        int Update(string name, int countryId);
        int Delete(int id);
    }
}
