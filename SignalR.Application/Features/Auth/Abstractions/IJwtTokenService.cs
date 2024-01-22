using SignalR.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SignalR.Application.Features.Auth
{
    public interface IJwtTokenService
    {
        JwtSecurityToken CreateJwtToken(ApplicationUser user);
    }
}