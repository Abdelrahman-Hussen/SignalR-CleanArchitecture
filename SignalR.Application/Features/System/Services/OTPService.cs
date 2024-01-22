using LurkingUnits.Application;
using SignalR.Domain.Helper;
using SignalR.Domain.Models;
using SignalR.Infrastructure.Reposatory;
using SingalR.Application.Utils;

namespace SignalR.Application.Features.System
{
    internal class OTPService(IGenericRepository<OTP> _otpRepo,
                              IValidator<ConfirmMailOTPDto> _confirmOTPValidation) : IOTPService
    {
        public async Task<ResponseModel<string>> SendOTPViaMail(SendMailOTPDto sendOTPDto)
        {

            var emailOtp = _otpRepo.GetEntityWithSpec(OTPSpecification.GetByEmail(sendOTPDto.Email));

            string otp = new Random().Next(1000, 9999).ToString();

            if (emailOtp == null)
            {
                OTP emailOTP = new()
                {
                    Email = sendOTPDto.Email,
                    OTPCode = otp,
                    OTPExpirationDate = DateTime.Now.AddMinutes(5)
                };

                await _otpRepo.Add(emailOTP);
                await _otpRepo.Save();

                await MailHelper.Send(emailOTP.Email, "Mail Verification",
                    $"Verification OTP {emailOTP.OTPCode}\nValid until {emailOTP.OTPExpirationDate}");

                return ResponseModel<string>.Success();
            }
            else
            {
                emailOtp.OTPCode = otp;
                emailOtp.OTPExpirationDate = DateTime.Now.AddMinutes(5);

                //_otpRepo.Update(emailOtp);        // need to test
                await _otpRepo.Save();

                await MailHelper.Send(emailOtp.Email, "Mail Verification",
                $"Verification OTP {emailOtp.OTPCode}\nValid until {emailOtp.OTPExpirationDate}");

                return ResponseModel<string>.Success();
            }
        }

        public async Task<ResponseModel<string>> ConfirmMailOTP(ConfirmMailOTPDto confirmMailOTPDto)
        {
            var validationResult = await _confirmOTPValidation.ValidateAsync(confirmMailOTPDto);

            if (!validationResult.IsValid)
                throw new BadRequestException(Helpers.ArrangeValidationErrors(validationResult.Errors));

            return ResponseModel<string>.Success();
        }
    }
}
