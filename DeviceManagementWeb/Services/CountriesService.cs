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

        public List<Country> GetAll()
        {
            return _repository.GetAll();
        }

        public Country GetById(int id)
        {
            if (id <= 0)
                return null;

            return _repository.GetById(id);
        }

        public int Insert(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                throw new ArgumentException("Country name cannot be empty");
            }

            _repository.Insert(country);

            return country.Id;
        }


        public int Update(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                throw new ArgumentException("Country name cannot be null", nameof(country.Name));
            }

            if (country.Id < 1)
            {
                throw new ArgumentException("Country id is invalid", nameof(country.Id));
            }

            if (country == null)
            {
                throw new ArgumentException("Country cannot be null");
            }

            var dbCountry = _repository.GetById(country.Id);
            if(dbCountry == null)
            {
                throw new ObjectNotFoundException("Country not found in the database.");
            }

            dbCountry.Name = country.Name;
                        
            return _repository.Update(dbCountry);
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Country id is invalid", nameof(id));
            }

            var country = _repository.GetById(id);
            if (country == null)
                throw new ObjectNotFoundException("Country id not found in the database.");

            
            return _repository.Delete(country.Id);
        }

    }
}
