    using Employee_Management_System.Data;
using Employee_Management_System.DTOs.AccessTokenDTOs;
using Employee_Management_System.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_Management_System.Services.Classes
{
    public class JwtServices
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public JwtServices(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task <string> GenerateAccessToken(User user)
        {
            await Task.Yield();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("IshikaSRaiyaniIshikaSRaiyaniIshikaSRaiyaniIshikaSRaiyani");

            var ExpiryMinutes = Convert.ToDouble(_configuration.GetSection("Jwt:ExpiryMinutes").Value);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("role", user.RoleId == 1 ? "Admin" :"Employee")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration.GetSection("Jwt:Audience").Value,
                Issuer = _configuration.GetSection("Jwt:Issuer").Value,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Console.WriteLine($"Generated Token: {tokenHandler.WriteToken(token)}");
            //return Task.FromResult(tokenHandler.WriteToken(token));
            var tokenString = tokenHandler.WriteToken(token);

            Console.WriteLine($"Generated Token: {tokenString}");

            return tokenString;
        }

    }
}
