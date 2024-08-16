using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StarWall.Core.Interfaces;
using StarWall.Domain.UserEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StarWall.Persistence.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration _configuration;

        public AuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWTToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("roles",user.Role.Title)
             }),
                Expires = DateTime.UtcNow.AddMonths(1),
                Issuer = issuer,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256),    
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
