namespace DeviceManagementWeb.Services.Interfaces
{
    public interface ICountriesService
    {
        List<Country> GetAll();
        Country GetById(int id);
        int Insert(string name);

        void Update(string name, int countryId);
        void Delete(int id);
    }
}
