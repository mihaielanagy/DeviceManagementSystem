using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly DeviceManagementContext _context;

        public CitiesService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<CityDto> GetAll()
        {
            var cities = _context.Cities.ToList();
            var citiesDto = new List<CityDto>();
            foreach (var city in cities)
            {
                CityDto cityDto = MapCity(city);
                citiesDto.Add(cityDto);
            }
            return citiesDto;
        }

        public CityDto GetById(int id)
        {
            if (id <= 0)
                return null;

            var city = _context.Cities.FirstOrDefault(i => i.Id == id);

            CityDto cityDto = new CityDto
            {
                Id = id,
                Name = city.Name,
                Country = _context.Countries.Find(city.IdCountry)
            };

            return cityDto;
        }

        public int Insert(CityDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            if(request.Country.Id < 1)
            {
                return 0;
            }

            if(request.Country == null)
            {
                return 0;
            }
            
            var city = new City
            {
                Name = request.Name,
                IdCountry = request.Country.Id,
            };

            _context.Cities.Add(city);
            _context.SaveChanges();

            return city.Id;
        }


        public int Update(CityDto request)
        {
            if (request == null)
            {
                throw new ObjectNotFoundException("City cannot be null");
            }

            if(request.Id <= 0)
            {
                throw new ArgumentException("Id is invalid");
            }

            var city = _context.Cities.Find(request.Id);
            city.Name = request.Name;
            city.IdCountry = request.Country.Id;
                        
            _context.Cities.Update(city);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("City id is invalid", nameof(id));
            }

            var city = _context.Cities.Find(id);
            if (city == null)
                throw new ObjectNotFoundException("City id not found in the database.");

            _context.Cities.Remove(city);
            
            return _context.SaveChanges();
        }

        public CityDto MapCity(City city)
        {
            var cityDto = new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = _context.Countries.Find(city.IdCountry)
            };

            return cityDto;
        }
    }
}
