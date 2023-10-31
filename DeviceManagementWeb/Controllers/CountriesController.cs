global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using DeviceManagementWeb.Services.Interfaces;
using System.Data.Entity.Core;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IDataService<Country> _countriesService;

        public CountriesController(IDataService<Country> countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public ActionResult<List<Country>> GetAll()
        {
            return Ok(_countriesService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Country> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var country = _countriesService.GetById(id);

            if (country == null)
                return BadRequest("Country not found");

            return Ok(country);
        }

        [HttpPost]
        public ActionResult<int> Insert(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return BadRequest("Country name cannot be empty");
            }
                        
            try
            {
                var countryId = _countriesService.Insert(country);
                return Ok(countryId);
            }
            catch (Exception)
            {
                return BadRequest("Country could not be added.");
            }                     
            
        }

        [HttpPut]
        public ActionResult<int> Update(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
            {
                return BadRequest("Country name cannot be empty");
            }

            if (country.Id < 1)
            {
                return BadRequest("Country id is invalid");
            }

            try
            {
                return Ok(_countriesService.Update(country));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            try
            {
                return Ok(_countriesService.Delete(id));
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
