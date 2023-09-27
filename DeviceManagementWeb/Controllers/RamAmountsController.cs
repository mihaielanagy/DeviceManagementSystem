using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamAmountsController : ControllerBase
    {
        private readonly IRamAmountsService _ramAmountsService;

        public RamAmountsController(IRamAmountsService ramAmountsService)
        {
            _ramAmountsService = ramAmountsService;
        }

        [HttpGet]
        public ActionResult<List<Ramamount>> GetAll()
        {
            return Ok(_ramAmountsService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Ramamount> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var ramAmount = _ramAmountsService.GetById(id);

            if (ramAmount == null)
                return BadRequest("ramAmount not found");

            return Ok(ramAmount);
        }
    }
}