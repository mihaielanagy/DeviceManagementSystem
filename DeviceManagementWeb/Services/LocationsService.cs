using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Services
{
    public class LocationsService : ILocationService
    {
        private readonly DeviceManagementContext _context;

        public LocationsService(DeviceManagementContext context)
        {
            _context = context;
        }

        public List<LocationDto> GetAll()
        {
            var locations = _context.Locations.ToList();
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

            var location = _context.Locations.FirstOrDefault(i => i.Id == id);

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

            _context.Locations.Add(location);
            _context.SaveChanges();

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

            var location = _context.Locations.Find(request.Id);
            location.Address = request.Address;
            location.IdCity = request.City.Id;

            _context.Locations.Update(location);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Location id is invalid", nameof(id));
            }

            var location = _context.Locations.Find(id);
            if (location == null)
                throw new ObjectNotFoundException("Location not found in the database.");

            _context.Locations.Remove(location);

            return _context.SaveChanges();
        }

        private LocationDto MapLocation(Location request)
        {
            City city = _context.Cities.Find(request.IdCity);
            Country country = _context.Countries.Find(city.IdCountry);

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
