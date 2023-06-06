using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorsController : ControllerBase
    {
        private readonly DeviceManagementContext _context;

        public ProcessorsController(DeviceManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Processor>> GetAll()
        {
            return Ok(_context.Processors.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Processor> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var processor = _context.Processors.FirstOrDefault(i => i.Id == id);

            if (processor == null)
                return BadRequest("Processor not found");

            return Ok(processor);
        }
    }
}