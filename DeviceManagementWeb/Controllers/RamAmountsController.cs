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
                return BadRequest("RAM Amount not found");

            return Ok(ramAmount);
        }

        [HttpPost]
        public ActionResult<int> Insert(Ramamount request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest("RAM Amount cannot be less than or equal to 0.");
            }

            var id = _ramAmountsService.Insert(request);

            if (id == 0)
            {
                return BadRequest("An error has occured");
            }

            return Ok(id);
        }

        [HttpPut]
        public ActionResult<int> Update(Ramamount request)
        {
            if (request.Id < 1)
            {
                return BadRequest("Id is invalid.");
            }

            if (request.Amount <= 0)
            {
                return BadRequest("RAM Amount cannot be less than or equal to 0.");
            }

            int rowsAffected = _ramAmountsService.Update(request);

            if (rowsAffected == 0)
            {
                return NotFound("RAM Amount not found.");
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

            int rowsAffected = _ramAmountsService.Delete(id);

            if (rowsAffected == 0)
            {
                return NotFound("RAM Amount not found.");
            };

            return Ok(rowsAffected);
        }

    }
}