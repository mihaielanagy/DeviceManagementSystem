using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeviceManagementWeb.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;


        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {

            var key = _configuration.GetSection("JwtAuth:Key").Get<string>();
            var iss = _configuration.GetSection("JwtAuth:Issuer").Get<string>();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: iss,
                audience: iss,
                claims: claims,
                expires: DateTime.Now.AddMinutes(100),
                signingCredentials: signingCredentials
            );

           return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
