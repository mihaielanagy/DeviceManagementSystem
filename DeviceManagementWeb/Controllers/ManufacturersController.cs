using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IDataService<Manufacturer> _manufacturersService;

        public ManufacturersController(IDataService<Manufacturer> manufacturersService)
        {
            _manufacturersService = manufacturersService;
        }

        [HttpGet]
        public ActionResult<List<Manufacturer>> GetAll()
        {
            var serviceResp = _manufacturersService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<Manufacturer> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _manufacturersService.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(Manufacturer request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Manufacturer name cannot be empty");

            var serviceResp = _manufacturersService.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(Manufacturer request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name cannot be empty.");

            var serviceResp = _manufacturersService.Update(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Id is invalid");

            var serviceResp = _manufacturersService.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }
    }
}
