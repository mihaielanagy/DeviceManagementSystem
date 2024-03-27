using AutoMapper;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class CitiesService : IDataService<CityDto>
    {
        private readonly IBaseRepository<City> _repository;
        private readonly IMapper _mapper;

        public CitiesService(IBaseRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<List<CityDto>> GetAll()
        {
            var cities = _repository.GetAll();
            var citiesDto = new List<CityDto>();
            foreach (var city in cities)
            {
                CityDto cityDto = _mapper.Map<CityDto>(city);
                citiesDto.Add(cityDto);
            }

            return new ServiceResponse<List<CityDto>>(citiesDto, true); ;
        }

        public ServiceResponse<CityDto> GetById(int id)
        {

            if (id <= 0)
            {
                return new ServiceResponse<CityDto>(null, false, "Invalid Id. Id cannot be a number smaller than 0");
            }

            var city = _repository.GetById(id);
            CityDto cityDto = _mapper.Map<CityDto>(city);
            
            return new ServiceResponse<CityDto>(cityDto, true);
        }

        public ServiceResponse<int> Insert(CityDto request)
        {

            if (string.IsNullOrEmpty(request.Name))
            {
                return new ServiceResponse<int>(0, false, "The name cannot be empty");
            }

            if (request.Country.Id < 1)
            {
                return new ServiceResponse<int>(0, false, "Invalid Id");
            }

            if (request.Country == null)
            {
                return new ServiceResponse<int>(0, false, "Country cannot be null");
            }

            var city = new City
            {
                Name = request.Name,
                IdCountry = request.Country.Id,
            };

            _repository.Insert(city);

            return new ServiceResponse<int>(city.Id, true);
        }


        public ServiceResponse<int> Update(CityDto request)
        {

            if (request == null)
            {
                return new ServiceResponse<int>(0, false, "City cannot be null");
            }

            if (request.Id <= 0)
            {
                return new ServiceResponse<int>(0, false, "Invalid id");
            }

            var city = _repository.GetById(request.Id);
            if (city == null)
                return new ServiceResponse<int>(0, false, "Invalid id");

            city.Name = request.Name;
            city.IdCountry = request.Country.Id;

            return new ServiceResponse<int>(_repository.Update(city), true);

        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
            {
                return new ServiceResponse<int>(0, false, "City id is invalid");
            }

            var city = _repository.GetById(id);
            if (city == null)
                return new ServiceResponse<int>(0, false, "City id not found in the database.");

            var deletedRows = _repository.Delete(city.Id);

            return new ServiceResponse<int>(deletedRows, false, "City id is invalid");
        }

        //public CityDto MapCity(City city)
        //{
        //    var cityDto = new CityDto
        //    {
        //        Id = city.Id,
        //        Name = city.Name,
        //        Country = _countryService.GetById(city.IdCountry)
        //    };

        //    return cityDto;
        //}
    }
}
