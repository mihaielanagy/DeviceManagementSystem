using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamAmountsController : ControllerBase
    {
        private readonly IDataService<Ramamount> _ramAmountsService;

        public RamAmountsController(IDataService<Ramamount> ramAmountsService)
        {
            _ramAmountsService = ramAmountsService;
        }

        [HttpGet]
        public ActionResult<List<Ramamount>> GetAll()
        {
            var serviceResp = _ramAmountsService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<Ramamount> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _ramAmountsService.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(Ramamount request)
        {
            if (request.Amount <= 0)
                return BadRequest("RAM Amount cannot be less than or equal to 0.");

            var serviceResp = _ramAmountsService.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(Ramamount request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");

            if (request.Amount <= 0)
                return BadRequest("RAM Amount cannot be less than or equal to 0.");

            var serviceResp = _ramAmountsService.Update(request);

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

            var serviceResp = _ramAmountsService.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }
    }
}