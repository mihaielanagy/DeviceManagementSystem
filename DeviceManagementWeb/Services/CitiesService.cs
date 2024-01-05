using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class CitiesService :IDataService<CityDto>
    {
        private readonly IBaseRepository<City> _repository;
        private readonly IDataService<Country> _countryService;

        public CitiesService(IBaseRepository<City> repository, IDataService<Country> countryService)
        {
            _repository = repository;
            _countryService = countryService;
        }

        public List<CityDto> GetAll()
        {
            var cities = _repository.GetAll();
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

            var city = _repository.GetById(id);

            CityDto cityDto = MapCity(city);

            return cityDto;
        }

        public int Insert(CityDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return 0;
            }

            if (request.Country.Id < 1)
            {
                return 0;
            }

            if (request.Country == null)
            {
                return 0;
            }

            var city = new City
            {
                Name = request.Name,
                IdCountry = request.Country.Id,
            };

            _repository.Insert(city);
            return city.Id;
        }


        public int Update(CityDto request)
        {
            if (request == null)
            {
                throw new ObjectNotFoundException("City cannot be null");
            }

            if (request.Id <= 0)
            {
                throw new ArgumentException("Id is invalid");
            }

            var city = _repository.GetById(request.Id);
            if (city == null)
                throw new ObjectNotFoundException("City not found in the database");

            city.Name = request.Name;
            city.IdCountry = request.Country.Id;

            return _repository.Update(city);
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("City id is invalid", nameof(id));
            }

            var city = _repository.GetById(id);
            if (city == null)
                throw new ObjectNotFoundException("City id not found in the database.");
                        
            return _repository.Delete(city.Id);
        }

        public CityDto MapCity(City city)
        {
            var cityDto = new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Country = _countryService.GetById(city.IdCountry)
            };

            return cityDto;
        }
    }
}
