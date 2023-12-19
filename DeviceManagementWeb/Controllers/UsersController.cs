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
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Id");

            var user = _service.GetById(id);
            if (user == null)
                return BadRequest("User not found");

            return Ok(user);
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
            var userId = _service.Insert(userInsertDto);

            if (userId == 0)
                return BadRequest("An error has occured");

            return Ok(userId);
        }

        [HttpPut]
        public ActionResult<int> Update(UserInsertDto request)
        {
            var user = _service.GetById(request.Id);
            if (user == null)
                return BadRequest("User not found");

            if (!EmailIsValid(request.Email))
                return BadRequest("Invalid email");

            int affectedRows = _service.Update(request);
            if(affectedRows == 0)
                return BadRequest("An error has occured");

            return Ok(affectedRows);
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Invalid Id");

            var user = _service.GetById(id);
            if (user == null)
                return BadRequest("User not found");

            int affectedRows = _service.Delete(id);
            if (affectedRows == 0)
                return BadRequest("An error has occured");

            return Ok(affectedRows);
        }

        private bool EmailIsValid(string email) => email.Contains("@") && email.Contains(".");
        private bool PasswordIsValid(string password) => password.Length >= 8;

    }
}
