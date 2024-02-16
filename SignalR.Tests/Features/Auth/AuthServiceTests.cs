using FluentAssertions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Moq;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalR.Tests.Features.Auth
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, Array.Empty<object>());
        private readonly Mock<IValidator<LoginDto>> _loginValidationMock = new Mock<IValidator<LoginDto>>();
        private readonly Mock<IValidator<RegisterDto>> _registerValidationMock = new Mock<IValidator<RegisterDto>>();
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        private readonly Mock<IRefreshTokenService> _refreshTokenServiceMock = new Mock<IRefreshTokenService>();
        private readonly IMapper _mapper;
        private readonly AuthService _authService;
        public AuthServiceTests()
        {
            _mapper = GetMapper();
            _authService = new AuthService(_userManagerMock.Object, _loginValidationMock.Object, _registerValidationMock.Object,
                                           _jwtTokenServiceMock.Object, _refreshTokenServiceMock.Object, _mapper);
        }

        [Fact]
        public async Task Login_ReturnSuccess()
        {
            #region Arrange
            var dummyDate = new LoginDto()
            {
                Email = "test@test.com",
                Password = "P@ssw0rd",
            };

            var userDummyDate = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = "Test",
                Email = "test@test.com",
                PhoneNumber = "1234567890",
            };

            var jwtSecurityTokenDummyDate = new JwtSecurityToken(
                                        issuer: "dummy_issuer",
                                        audience: "dummy_audience",
                                        claims: new List<Claim>
                                        {
                                            new Claim("claim_type1", "claim_value1"),
                                            new Claim("claim_type2", "claim_value2"),
                                        },
                                        expires: DateTime.UtcNow.AddDays(1),
                                        signingCredentials: new SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dummy_key")),
                                            SecurityAlgorithms.HmacSha256)
                                                );

            var refreshTokenDummyDate = new RefreshToken()
            {
                Token = "dummy_token",
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                RevokedOn = null,
                UserId = "dummy_user_id"
            };

            _loginValidationMock.Setup(val => val.ValidateAsync(It.IsAny<LoginDto>(), default));

            _userManagerMock.Setup(repo => repo.FindByEmailAsync(dummyDate.Email))
                            .ReturnsAsync(userDummyDate);

            _jwtTokenServiceMock.Setup(repo => repo.CreateJwtToken(userDummyDate))
                            .Returns(jwtSecurityTokenDummyDate);

            _refreshTokenServiceMock.Setup(repo => repo.GenerateRefreshToken(userDummyDate.Id))
                            .ReturnsAsync(refreshTokenDummyDate);
            #endregion

            #region Act
            var result = await _authService.Login(dummyDate);
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Ok.Should().BeTrue();
            #endregion
        }

        private IMapper GetMapper()
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(IJwtTokenService).Assembly);
            return new Mapper(config);
        }
    }
}
