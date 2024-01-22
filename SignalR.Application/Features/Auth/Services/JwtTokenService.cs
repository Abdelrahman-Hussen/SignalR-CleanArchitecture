using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalR.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalR.Application.Features.Auth
{
    internal class JwtTokenService(IConfiguration _configuration) : IJwtTokenService
    {
        public JwtSecurityToken CreateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]!));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Token:ValidIssuer"],
                audience: _configuration["Token:ValidAudiance"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Token:JWTDurationInMinutes"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
