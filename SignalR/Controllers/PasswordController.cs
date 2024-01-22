using LurkingUnits.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR.Application.Features.Auth;
using SignalR.Application.Features.System;

namespace SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController(IPasswordService _passwordService,
                                    IOTPService _otpService) : ControllerBase
    {
        [Authorize]
        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel<string>>> ChangePassword([FromBody] ChangePasswordDto Dto)
            => Ok(await _passwordService.ChangePassword(Dto));
        
        [AllowAnonymous]
        [HttpPut("[action]")]
        public async Task<ActionResult<ResponseModel<string>>> ResetPassword([FromBody] ResetPasswordDto Dto)
            => Ok(await _passwordService.ResetPassword(Dto));

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseModel<string>>> SendOtp([FromBody] SendMailOTPDto Dto)
            => Ok(await _otpService.SendOTPViaMail(Dto));
    }
}
