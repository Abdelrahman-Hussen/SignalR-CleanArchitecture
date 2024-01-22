using SignalR.Domain.Models;
using SignalR.Infrastructure.Reposatory;

namespace SignalR.Application.Features.System.Validation
{
    internal class ConfirmEmailOTPValidation : AbstractValidator<ConfirmMailOTPDto>
    {
        private readonly IGenericRepository<OTP> _otpRepo;
        private readonly ILocalizationService _localizationService;

        private OTP _otp;

        public ConfirmEmailOTPValidation(IGenericRepository<OTP> otpRepo, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _otpRepo = otpRepo;

            When(t => isExist(t.Email, new CancellationToken()).Result, () =>
            {
                RuleFor(u => u.Email)
                    .NotEmpty()
                    .Must(isOtpExpired)
                    .WithMessage(_localizationService.GetMessage(Messages.Error_OTPExpired));

                RuleFor(u => u.OTP)
                    .NotEmpty()
                    .Must(isOtpCorrect)
                    .WithMessage(_localizationService.GetMessage(Messages.Error_OTPWrong));

            }).Otherwise(() =>
            {
                RuleFor(u => u.Email)
                    .Must(x => false)
                    .WithMessage(_localizationService.GetMessage(Messages.Error_UserEmailNotExist));
            });
        }

        private async Task<bool> isExist(string email, CancellationToken cancellationToken)
        {
            _otp = _otpRepo.GetEntityWithSpec(OTPSpecification.GetByEmail(email));
            return _otp != null;
        }
        private bool isOtpExpired(string email)
            => (DateTime.Now > _otp.OTPExpirationDate);

        private bool isOtpCorrect(string otp)
            => (_otp.OTPCode == otp);
    }
}
