﻿using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class LocationsService : IDataService<LocationDto>
    {
        private readonly IBaseRepository<Location> _locationRepository;
        private readonly IBaseRepository<City> _cityRepository;
        private readonly IBaseRepository<Country> _countryRepository;

        public LocationsService(IBaseRepository<Location> locationRepository, IBaseRepository<City> cityRepository, IBaseRepository<Country> countryRepository)
        {
            _locationRepository = locationRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public List<LocationDto> GetAll()
        {
            var locations = _locationRepository.GetAll();
            var locationsDto = new List<LocationDto>();

            foreach (var location in locations)
            {
                locationsDto.Add(MapLocation(location));
            }

            return locationsDto;
        }

        public LocationDto GetById(int id)
        {
            if (id <= 0)
                return null;

            var location = _locationRepository.GetById(id);

            if (location == null)
                return null;

            return MapLocation(location);
        }

        public int Insert(LocationDto request)
        {
            if (string.IsNullOrEmpty(request.Address))
            {
                return 0;
            }

            if (request.City == null)
            {
                return 0;
            }                 

            var location = new Location
            {
                Address = request.Address,
                IdCity = request.City.Id,
            };

            _locationRepository.Insert(location);

            return location.Id;
        }

        public int Update(LocationDto request)
        {
            if (request == null)
            {
                throw new ObjectNotFoundException("Location cannot be null");
            }

            if (request.Id <= 0)
            {
                throw new ArgumentException("Id is invalid");
            }

            var location = _locationRepository.GetById(request.Id);
            location.Address = request.Address;
            location.IdCity = request.City.Id;

            int affectedRows = _locationRepository.Update(location);
            return affectedRows;
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Location id is invalid", nameof(id));
            }

            var location = _locationRepository.GetById(id);
            if (location == null)
                throw new ObjectNotFoundException("Location not found in the database.");

            int affectedRows = _locationRepository.Delete(id);

            return affectedRows;
        }

        private LocationDto MapLocation(Location request)
        {
            City city = _cityRepository.GetById(request.IdCity);
            Country country = _countryRepository.GetById(city.IdCountry);

            var locationDto = new LocationDto
            {
                Id = request.Id,
                Address = request.Address,
                City = new CityDto { Id = city.Id, Name = city.Name, Country = country }
            };

            return locationDto;
        }
    }
}
