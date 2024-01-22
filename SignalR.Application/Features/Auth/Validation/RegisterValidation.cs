using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;

namespace SignalR.Application.Validation
{
    internal class RegisterValidation : AbstractValidator<RegisterDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILocalizationService _localizationService;

        public RegisterValidation(UserManager<ApplicationUser> userManager, ILocalizationService localizationService)
        {
            _userManager = userManager;
            _localizationService = localizationService;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(isEmailUsed)
                .WithMessage(_localizationService.GetMessage(Messages.Error_UserEmailExist));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Must(isPhoneNumberUsed)
                .WithMessage(_localizationService.GetMessage(Messages.Error_UserPhoneExist));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .Must(isUserNameUsed)
                .WithMessage(_localizationService.GetMessage(Messages.Error_UserNameExist));
        }

        private bool isEmailUsed(string email)
            => !_userManager.Users.Any(x => x.Email == email);

        private bool isPhoneNumberUsed(string PhoneNumber)
            => !_userManager.Users.Any(x => x.PhoneNumber == PhoneNumber);

        private bool isUserNameUsed(string userNam)
            => !_userManager.Users.Any(x => x.UserName == userNam);
    }
}
