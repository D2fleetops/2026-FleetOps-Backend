using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using fleetops_backend.Models;

namespace fleetops_backend.Application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var jwt = _configuration.GetSection("Jwt");

            var keyString = jwt["Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            if (keyString.Length < 32)
            {
                throw new InvalidOperationException("JWT Key must be at least 32 characters long for security reasons.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var expireMinutesString = jwt["ExpireMinutes"] ?? throw new InvalidOperationException("JWT ExpireMinutes is not configured.");
            if (!double.TryParse(expireMinutesString, out double expireMinutes))
            {
                throw new InvalidOperationException("JWT ExpireMinutes is not a valid number.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            if (user.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = jwt["Issuer"],
                Audience = jwt["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}