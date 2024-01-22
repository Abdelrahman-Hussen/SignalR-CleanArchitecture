using LurkingUnits.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR.Application.Features.Auth;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RefreshTokenController(IRefreshTokenService _refreshTokenService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel<AuthDto>>> RefreshToken(string token)
            => Ok(await _refreshTokenService.RefreshToken(token));

        [Authorize]
        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel<string>>> RevokeToken(string token)
            => Ok(await _refreshTokenService.RevokeToken(token));
    }
}
