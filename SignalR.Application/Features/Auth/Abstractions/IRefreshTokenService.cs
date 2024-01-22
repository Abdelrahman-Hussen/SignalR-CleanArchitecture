using LurkingUnits.Application;
using SignalR.Domain.Models;

namespace SignalR.Application.Features.Auth
{
    public interface IRefreshTokenService
    {
        Task<ResponseModel<string>> DeleteUserRefreshTokens(string userId);
        Task<RefreshToken> GenerateRefreshToken(string userId);
        Task<ResponseModel<AuthDto>> RefreshToken(string token);
        Task<ResponseModel<string>> RevokeToken(string token);
    }
}