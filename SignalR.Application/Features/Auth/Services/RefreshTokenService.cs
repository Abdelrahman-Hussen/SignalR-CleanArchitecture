using LurkingUnits.Application;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SignalR.Application.Features.System.Resources;
using SignalR.Application.Features.User;
using SignalR.Domain.Models;
using SignalR.Infrastructure.Reposatory;
using System.Security.Cryptography;

namespace SignalR.Application.Features.Auth
{
    internal class RefreshTokenService(IGenericRepository<RefreshToken> _refreshTokenRepo,
                                       IJwtTokenService _jwtTokenService,
                                       IConfiguration _configuration,
                                       ILogger<RefreshToken> _logger,
                                       IMapper _mapper) : IRefreshTokenService
    {
        public async Task<ResponseModel<AuthDto>> RefreshToken(string token)
        {
            var refreshToken = GetRefreshToken(token);

            if (refreshToken.RevokedOn.HasValue)
                throw new BadRequestException(Message.Error_InactiveRefreshToken);

            var jwtToken = _jwtTokenService.CreateJwtToken(refreshToken.User);

            AuthDto authDto;

            if (!refreshToken.IsExpired)
            {
                authDto = new AuthDto(jwtToken, refreshToken,
                    _mapper.Map<ApplicationUserDto>(refreshToken.User));
            }
            else
            {
                refreshToken.Revoke();

                await _refreshTokenRepo.Save();

                var newRefreshToken = await GenerateRefreshToken(refreshToken.UserId);

                authDto = new AuthDto(jwtToken, newRefreshToken,
                    _mapper.Map<ApplicationUserDto>(refreshToken.User));
            }

            return ResponseModel<AuthDto>.Success(authDto, Message.Success_General);
        }

        public async Task<RefreshToken> GenerateRefreshToken(string userId)
        {
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.Now.AddDays(double.Parse(_configuration["Token:RefreshDurationInDays"]!)),
            };

            await _refreshTokenRepo.Add(refreshToken);

            await _refreshTokenRepo.Save();

            return refreshToken;
        }

        public async Task<ResponseModel<string>> RevokeToken(string token)
        {

            var refreshToken = GetRefreshToken(token);

            refreshToken.Revoke();

            //_refreshTokenRepo.Update(refreshToken); // need test

            await _refreshTokenRepo.Save();

            return ResponseModel<string>.Success();

        }

        public async Task<ResponseModel<string>> DeleteUserRefreshTokens(string userId)
        {

            var result = _refreshTokenRepo.GetWithSpec(RefreshTokenSpecification.GetByUserId(userId));

            _refreshTokenRepo.DeleteRange(result.data);

            await _refreshTokenRepo.Save();

            return ResponseModel<string>.Success();

        }

        private RefreshToken GetRefreshToken(string token)
        {
            var result = _refreshTokenRepo
                .GetEntityWithSpec(RefreshTokenSpecification.GetByToken(token));

            if (result is null)
                throw new BadRequestException(Message.Error_InvalidRefreshToken);

            return result;
        }
    }
}