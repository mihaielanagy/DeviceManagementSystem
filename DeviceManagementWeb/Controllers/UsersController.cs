global using Microsoft.EntityFrameworkCore;
using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DeviceManagementWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpGet]

        public ActionResult<List<UserDto>> GetAll()
        {
            var serviceResp = _service.GetAll();

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");
            else
                return Ok(serviceResp.Data);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Id");

            var serviceResp = _service.GetById(id);            
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);
        }

        [HttpPost]
        public ActionResult<int> Insert(UserInsertDto userInsertDto)
        {
            if (!EmailIsValid(userInsertDto.Email))
                return BadRequest("Invalid email");

            //var existingUser = _service.GetByEmail(userInsertDto.Email);
            //if (existingUser != null)
            //    return BadRequest("User already exists");

            if (!PasswordIsValid(userInsertDto.Password))
                return BadRequest("Password too short. Must contain at least 8 characters");

            var serviceResp = _service.Insert(userInsertDto);
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(serviceResp.Data);
        }

        [HttpPut]
        public ActionResult<int> Update(UserInsertDto request)
        {
            var user = _service.GetById(request.Id).Data;
            if (user == null)
                return NotFound("User not found");

            if (!EmailIsValid(request.Email))
                return BadRequest("Invalid email");

            var serviceResp = _service.Update(request);
            int affectedRows = serviceResp.Data;

            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(affectedRows);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Id");

            var serviceResp = _service.Delete(id);
            int affectedRows = serviceResp.Data;
            if (serviceResp.IsSuccess == false)
                return BadRequest($"An error has occured: {serviceResp.ErrorMessage}");

            return Ok(affectedRows);
        }

        private bool EmailIsValid(string email) => email.Contains("@") && email.Contains(".");
        private bool PasswordIsValid(string password) => password.Length >= 8;

    }
}
