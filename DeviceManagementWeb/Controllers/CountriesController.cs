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
            var serviceResp = _countriesService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<Country> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _countriesService.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
                return BadRequest("Country name cannot be empty");

            var serviceResp = _countriesService.Insert(country);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);

        }

        [HttpPut]
        public ActionResult<int> Update(Country country)
        {
            if (string.IsNullOrEmpty(country.Name))
                return BadRequest("Country name cannot be empty");


            if (country.Id < 1)
                return BadRequest("Country id is invalid");

            var serviceResp = _countriesService.Update(country);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Id is invalid");

            var serviceResp = _countriesService.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);
        }
    }
}
