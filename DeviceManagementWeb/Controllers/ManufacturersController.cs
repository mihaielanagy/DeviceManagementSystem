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

        [HttpPost]
        public ActionResult<int> Insert(Manufacturer request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Manufacturer name cannot be empty");
            }

            var id = _manufacturersService.Insert(request);

            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<int> Update(Manufacturer request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            int rowsAffected = _manufacturersService.Update(request);

            if (rowsAffected == 0)
            {
                return NotFound("Manufacturer not found.");
            }

            return Ok(rowsAffected);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id is invalid");
            }

            int rowsAffected = _manufacturersService.Delete(id);

            if (rowsAffected == 0)
            {
                return NotFound("Manufacturer not found.");
            };

            return Ok(rowsAffected);
        }


    }
}
