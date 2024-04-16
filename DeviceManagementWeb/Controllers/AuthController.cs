using DeviceManagementWeb.DTOs;
using DeviceManagementWeb.Services;
using DeviceManagementWeb.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace DeviceManagementWeb.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DeviceManagementContext _context;
        private readonly ILoggingService _loggingService;
        private readonly ITokenService _tokenService;

        public AuthController(DeviceManagementContext context, ILoggingService loggingService, ITokenService tokenService)
        {
            _context = context;
            _loggingService = loggingService;
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserLoginDto user)
        {
            
            if (user == null || string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password))
                return Unauthorized("Invalid client request");

            var foundUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (foundUser == null)
                return Unauthorized("User not found");

            var passwordValid = foundUser.Password.Equals(user.Password);
            if (!passwordValid)
                return Unauthorized("Invalid credentials");
            else
            {
                var token = _tokenService.GenerateToken(foundUser);
                _loggingService.LogInformation("User logged successfully.");
                return Ok(new { Token = token });

                //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@123"));
                //var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Email, user.Email),
                //    new Claim(ClaimTypes.Name, $"{foundUser.FirstName} {foundUser.LastName}"),
                //    new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString())
                //};

                //var tokenOptions = new JwtSecurityToken(
                //    issuer: "https://localhost:7250",
                //    audience: "https://localhost:7250/",
                //    claims: claims,
                //    expires: DateTime.Now.AddMinutes(100),
                //    signingCredentials: signingCredentials
                //);

                //var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                //_loggingService.LogInformation("User logged successfuly.");

                //return Ok(new { Token = tokenString });
            }
        }
    }
}
