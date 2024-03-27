using AutoMapper;
using DeviceManagementDB.Repositories;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class LocationsService : IDataService<LocationDto>
    {
        private readonly IBaseRepository<Location> _locationRepository;
        private readonly IMapper _mapper;


        public LocationsService(IBaseRepository<Location> locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public ServiceResponse<List<LocationDto>> GetAll()
        {
            var locations = _locationRepository.GetAll();
            var locationsDto = new List<LocationDto>();

            foreach (var location in locations)
            {
                locationsDto.Add(_mapper.Map<LocationDto>(location));
            }

            return new ServiceResponse<List<LocationDto>>(locationsDto, true);
        }

        public ServiceResponse<LocationDto> GetById(int id)
        {
            if (id <= 0)
                return new ServiceResponse<LocationDto>(null, false, "Invalid id");

            var location = _locationRepository.GetById(id);
            if (location == null)
                return new ServiceResponse<LocationDto>(null, false, "Location not found");

            var dto = _mapper.Map<LocationDto>(location);
            return new ServiceResponse<LocationDto>(dto, true);

        }

        public ServiceResponse<int> Insert(LocationDto request)
        {
            if (string.IsNullOrEmpty(request.Address))
                return new ServiceResponse<int>(0, false, "Address cannot be null");

            if (request.City == null)
                return new ServiceResponse<int>(0, false, "City cannot be null");

            var location = new Location
            {
                Address = request.Address,
                IdCity = request.City.Id,
            };

            _locationRepository.Insert(location);

            return new ServiceResponse<int>(location.Id, true);
        }

        public ServiceResponse<int> Update(LocationDto request)
        {
            if (request == null)
                return new ServiceResponse<int>(0, false, "Location cannot be null");

            if (request.Id <= 0)
                return new ServiceResponse<int>(0, false, "Invalid Id");

            var location = _locationRepository.GetById(request.Id);
            location.Address = request.Address;
            location.IdCity = request.City.Id;

            int affectedRows = _locationRepository.Update(location);
            return new ServiceResponse<int>(affectedRows, true);
        }

        public ServiceResponse<int> Delete(int id)
        {
            if (id < 1)
                return new ServiceResponse<int>(0, false, "Location id is invalid");

            var location = _locationRepository.GetById(id);
            if (location == null)
                return new ServiceResponse<int>(0, false, "Location not found in the database");

            int affectedRows = _locationRepository.Delete(id);
            return new ServiceResponse<int>(affectedRows, true);
        }

        //private LocationDto MapLocation(Location request)
        //{

        //    var locationDto = new LocationDto
        //    {
        //        Id = request.Id,
        //        Address = request.Address,
        //        City = _cityService.GetById(request.IdCity)
        //    };

        //    return locationDto;
        //}
    }
}
