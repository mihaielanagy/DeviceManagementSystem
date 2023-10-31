using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorsController : ControllerBase
    {
        private readonly IProcessorsService _processorsService;

        public ProcessorsController(IProcessorsService processorsService)
        {
            _processorsService = processorsService;
        }

       
        [HttpGet]
        public ActionResult<List<Processor>> GetAll()
        {
            return Ok(_processorsService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Processor> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var processor = _processorsService.GetById(id);

            if (processor == null)
                return BadRequest("Processor not found");

            return Ok(processor);
        }

        [HttpPost]
        public ActionResult<int> Insert(Processor request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Processor name cannot be empty");
            }

            var id = _processorsService.Insert(request);

            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<int> Update(Processor request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Name cannot be empty.");
            }

            int rowsAffected = _processorsService.Update(request);

            if (rowsAffected == 0)
            {
                return NotFound("Processor not found.");
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

            int rowsAffected = _processorsService.Delete(id);

            if (rowsAffected == 0)
            {
                return NotFound("Processor not found.");
            };

            return Ok(rowsAffected);
        }
    }
}