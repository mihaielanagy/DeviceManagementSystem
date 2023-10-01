using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly  IManufacturersService _manufacturersService;

        public ManufacturersController(IManufacturersService manufacturersService)
        {
            _manufacturersService = manufacturersService;
        }

        [HttpGet]
        public ActionResult<List<Manufacturer>> GetAll()
        {
            return Ok(_manufacturersService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Manufacturer> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var manufacturer = _manufacturersService.GetById(id);

            if (manufacturer == null)
                return BadRequest("Manufacturer not found");

            return Ok(manufacturer);
        }
    }
}
