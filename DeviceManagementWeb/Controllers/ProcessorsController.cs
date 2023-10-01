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
    }
}