using DeviceManagementDB.Repositories;
using DeviceManagementWeb.Services.Interfaces;
using System.Data;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class CountriesService : IDataService<Country>
    {
        private readonly IBaseRepository<Country> _repository;

        public CountriesService(IBaseRepository<Country> repository)
        {
            _repository = repository;
        }

        public ServiceResponse<List<Country>> GetAll()
        {
            var countries = _repository.GetAll();

            return new ServiceResponse<List<Country>>(countries, true);
        }

        public ServiceResponse<Country> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<Country>(null, false, "Invalid Id");

            var country = _repository.GetById(id);
            return new ServiceResponse<Country>(country, true);
        }

        public ServiceResponse<int> Insert(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return new ServiceResponse<int>(0, false, "Country name cannot be empty");
            }

            _repository.Insert(country);

            return new ServiceResponse<int>(country.Id, true);
        }


        public ServiceResponse<int> Update(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return new ServiceResponse<int>(0, false, "Country name cannot be empty");
            }

            if (country.Id < 1)
            {
                return new ServiceResponse<int>(0, false, "Invalid Country Id");
            }

            if (country == null)
            {
                return new ServiceResponse<int>(0, false, "Country cannot be null");
            }

            var dbCountry = _repository.GetById(country.Id);
            if (dbCountry == null)
            {
                return new ServiceResponse<int>(0, false, "Country not found in the database");
            }

            dbCountry.Name = country.Name;
            var affectedRows = _repository.Update(dbCountry);

            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
            {
                return new ServiceResponse<int>(0, false, "Invalid Country Id");
            }

            var country = _repository.GetById(id);
            if (country == null)
                return new ServiceResponse<int>(0, false, "Country not found in the database");

            var affectedRows = _repository.Delete(country.Id);

            return new ServiceResponse<int>(affectedRows, true);
        }
    }
}
