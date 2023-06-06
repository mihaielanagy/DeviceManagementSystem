global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public CountriesController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Country>> GetAll()
        {
            return Ok(_context.Countries.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Country> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var country = _context.Countries.FirstOrDefault(i => i.Id == id);

            if (country == null)
                return BadRequest("Country not found");

            return Ok(country);
        }

        [HttpPost]
        public ActionResult Insert(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Country name cannot be empty");
            }

            var country = new Country
            {
                Name = name
            };

            _context.Countries.Add(country);
            _context.SaveChanges();

            return Ok(country.Id);
        }

        [HttpPut]
        public ActionResult Update(string name, int cuntryId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Country name cannot be empty");
            }

            if (cuntryId < 1)
            {
                return BadRequest("Country id is invalid");
            }

            var country = _context.Countries.Find(cuntryId);
            if (country == null)
                return BadRequest("Country not found");

            country.Name = name;
            country.Id = cuntryId;
            _context.Countries.Update(country);
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

            var country = _context.Countries.Find(id);
            if (country == null)
                return BadRequest("Data not found");

            _context.Countries.Remove(country);
            _context.SaveChanges();

            return Ok();
        }
    }
}
