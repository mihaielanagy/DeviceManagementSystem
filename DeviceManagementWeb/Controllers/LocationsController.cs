using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public LocationsController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Location>> GetAll()
        {
            var locations = _context.Locations.ToList();
            var locationsDto = new List<LocationDto>();

            foreach (var location in locations)
            {
                City city = _context.Cities.Find(location.IdCity);
                Country country = _context.Countries.Find(city.IdCountry);

                var locationDto = new LocationDto
                {
                    Id = location.Id,
                    Address = location.Address,
                    City = new CityDto { Id = city.Id, Name = city.Name, Country = country }
                };
                locationsDto.Add(locationDto);
            }

            return Ok(locationsDto);
        }

        [HttpGet("{id}")]
        public ActionResult<Location> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var location = _context.Locations.Include(c => c.IdCityNavigation).ThenInclude(c => c.IdCountryNavigation).FirstOrDefault(i => i.Id == id);

            if (location == null)
                return BadRequest("Location not found");

            return Ok(location);
        }
    }
}
