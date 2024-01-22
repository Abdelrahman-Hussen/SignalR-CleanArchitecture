using LurkingUnits.Application;
using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.System;
using SignalR.Domain.Models;
using SingalR.Application.Utils;

namespace SignalR.Application.Features.Auth
{
    internal class PasswordService(UserManager<ApplicationUser> _userManager,
                                   IValidator<ChangePasswordDto> _changePasswordValidation,
                                   IOTPService _oTPService) : IPasswordService
    {
        public async Task<ResponseModel<string>> ChangePassword(ChangePasswordDto model)
        {
            var validationResult = await _changePasswordValidation.ValidateAsync(model);

            if (!validationResult.IsValid)
                throw new BadRequestException(Helpers.ArrangeValidationErrors(validationResult.Errors));

            var user = await _userManager.FindByEmailAsync(model.Email);

            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            return ResponseModel<string>.Success();
        }

        public async Task<ResponseModel<string>> ResetPassword(ResetPasswordDto model)
        {
            var confirmOTPDto = new ConfirmMailOTPDto()
            {
                Email = model.Email,
                OTP = model.OTP,
            };

            var confirmationResult = await _oTPService.ConfirmMailOTP(confirmOTPDto);

            if (!confirmationResult.Ok)
                throw new BadRequestException(confirmationResult.Message);

            var user = await _userManager.FindByEmailAsync(model.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!resetPasswordResult.Succeeded)
                throw new BadRequestException(Helpers.ArrangeIdentityErrors(resetPasswordResult.Errors));

            return ResponseModel<string>.Success();
        }
    }
}
