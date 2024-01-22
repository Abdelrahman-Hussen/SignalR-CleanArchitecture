using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;

namespace SignalR.Application.Validation
{
    internal class ChangePasswordValidation : AbstractValidator<ChangePasswordDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILocalizationService _localizationService;

        public ChangePasswordValidation(UserManager<ApplicationUser> userManager,
                                        ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(isUserExist)
                .WithMessage(_localizationService.GetMessage(Messages.Error_UserEmailNotExist));
        }

        private bool isUserExist(string email)
            => _userManager.Users.Any(x => x.Email == email);
    }
}
