using DeviceManagementWeb.Services.Interfaces;
using System.Data;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly DeviceManagementContext _context;

        public CountriesService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<Country> GetAll()
        {
            return _context.Countries.ToList();
        }

        public Country GetById(int id)
        {
            if (id <= 0)
                return null;
            
            return _context.Countries.FirstOrDefault(i => i.Id == id);
        }

        public int Insert(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
               throw new ArgumentException("Country name cannot be empty");
            }

            var country = new Country
            {
                Name = name
            };

            _context.Countries.Add(country);
            _context.SaveChanges();

            return country.Id;
        }

       
        public void Update(string name, int countryId) 
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Country name cannot be null", nameof(name));
            }

            if (countryId < 1)
            {
                throw new ArgumentException("Country id is invalid", nameof(countryId));
            }

            var country = _context.Countries.Find(countryId);
            if (country == null)
            {
                throw new ObjectNotFoundException("Country not found in the database.");
            }

            country.Name = name;
            _context.Countries.Update(country);
            _context.SaveChanges();
        }

        public void Delete(int id) 
        {
            if (id < 1)
            {
                throw new ArgumentException("Country id is invalid", nameof(id));
            }

            var country = _context.Countries.Find(id);
            if (country == null)
                throw new ObjectNotFoundException("Country id not found in the database.");

            _context.Countries.Remove(country);
            _context.SaveChanges();
        }

    }
}
