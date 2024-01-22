using System.IdentityModel.Tokens.Jwt;
using SignalR.Application.Features.User;
using SignalR.Domain.Models;

namespace SignalR.Application.Features.Auth
{
    public class AuthDto
    {
        public AuthDto(JwtSecurityToken jwtToken, RefreshToken refreshToken, ApplicationUserDto user)
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            ExpiresOn = jwtToken.ValidTo;
            User = user;
            RefreshToken = refreshToken.Token;
            RefreshTokenExpiration = refreshToken.ExpiresOn;
        }

        public ApplicationUserDto User { get; init; }
        public string? Token { get; init; }
        public DateTime? ExpiresOn { get; init; }
        public string? RefreshToken { get; init; }
        public DateTime RefreshTokenExpiration { get; init; }
    }
}
