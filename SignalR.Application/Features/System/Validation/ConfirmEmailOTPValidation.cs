using SignalR.Domain.Models;
using SignalR.Infrastructure.Reposatory;

namespace SignalR.Application.Features.System.Validation
{
    internal class ConfirmEmailOTPValidation : AbstractValidator<ConfirmMailOTPDto>
    {
        private readonly IGenericRepository<OTP> _otpRepo;

        private OTP _otp;

        public ConfirmEmailOTPValidation(IGenericRepository<OTP> otpRepo)
        {
            _otpRepo = otpRepo;

            When(t => isExist(t.Email, new CancellationToken()).Result, () =>
            {
                RuleFor(u => u.Email)
                    .NotEmpty()
                    .Must(isOtpNotExpired)
                    .WithMessage(Message.Error_OTPExpired);

                RuleFor(u => u.OTP)
                    .NotEmpty()
                    .Must(isOtpCorrect)
                    .WithMessage(Message.Error_OTPWrong);

            }).Otherwise(() =>
            {
                RuleFor(u => u.Email)
                    .Must(x => false)
                    .WithMessage(Message.Error_UserEmailNotExist);
            });

            // onther way to use fluant validation in cusotm cases
            //RuleFor(x => x).Custom((request, context) => ConfirmOTP(request, context));
        }
        private void ConfirmOTP(ConfirmMailOTPDto request, ValidationContext<ConfirmMailOTPDto> context)
        {
            var otp = _otpRepo.GetEntityWithSpec(OTPSpecification.GetByEmail(request.Email));

            if (otp == null)
            {
                context.AddFailure(Message.Error_UserEmailNotExist);
                return;
            }

            if (DateTime.Now > otp.OTPExpirationDate)
                context.AddFailure(Message.Error_OTPExpired);

            if (otp.OTPCode != request.OTP)
                context.AddFailure(Message.Error_OTPWrong);
        }

        private async Task<bool> isExist(string email, CancellationToken cancellationToken)
        {
            _otp = _otpRepo.GetEntityWithSpec(OTPSpecification.GetByEmail(email));
            return _otp != null;
        }
        private bool isOtpNotExpired(string email)
            => !(DateTime.Now > _otp.OTPExpirationDate);

        private bool isOtpCorrect(string otp)
            => (_otp.OTPCode == otp);
    }
}
