using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public CitiesController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<City>> GetAll()
        {
            return Ok(_context.Cities.Include(c => c.IdCountryNavigation).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<City> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var city = _context.Cities.Include(c => c.IdCountryNavigation).FirstOrDefault(i => i.Id == id);

            if (city == null)
                return BadRequest("City not found");

            return Ok(city);
        }

        [HttpPost]
        public ActionResult<int> Insert(string cityName, int countryId)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                return BadRequest("City name cannot be empty");
            }

            if (countryId < 1)
            {
                return BadRequest("Country id is invalid");
            }

            var city = new City
            {
                IdCountry = countryId,
                Name = cityName
            };

            _context.Cities.Add(city);
            _context.SaveChanges();

            return Ok(city.Id);
        }

        [HttpPut]
        public ActionResult Update(int cityId, string cityName, int countryId)
        {
            if (cityId < 1)
            {
                return BadRequest("City id is invalid");
            }

            if (string.IsNullOrEmpty(cityName))
            {
                return BadRequest("City name cannot be empty");
            }

            if (countryId < 1)
            {
                return BadRequest("Country id is invalid");
            }

            var city = _context.Cities.Find(cityId);
            if (city == null)
                return BadRequest("City not found");

            city.Name = cityName;
            city.IdCountry = countryId;
            _context.Cities.Update(city);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            var city = _context.Cities.Find(id);
            if (city == null)
                return BadRequest("Data not found");

            _context.Cities.Remove(city);
            _context.SaveChanges();

            return Ok();
        }
    }
}
