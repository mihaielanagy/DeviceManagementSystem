using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorsController : ControllerBase
    {
        private readonly IDataService<Processor> _processorsService;

        public ProcessorsController(IDataService<Processor> processorsService)
        {
            _processorsService = processorsService;
        }


        [HttpGet]
        public ActionResult<List<Processor>> GetAll()
        {
            var serviceResp = _processorsService.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<Processor> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var serviceResp = _processorsService.GetById(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(Processor request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Processor name cannot be empty");

            var serviceResp = _processorsService.Insert(request);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(Processor request)
        {
            if (request.Id < 1)
                return BadRequest("Id is invalid.");

            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name cannot be empty.");

            var serviceResp = _processorsService.Update(request);

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

            var serviceResp = _processorsService.Delete(id);

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }
    }
}