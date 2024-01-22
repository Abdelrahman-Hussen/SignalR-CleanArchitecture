using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;

namespace SignalR.Application.Validation
{
    internal class LoginValidation : AbstractValidator<LoginDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILocalizationService _localizationService;

        public LoginValidation(UserManager<ApplicationUser> userManager, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _userManager = userManager;

            RuleFor(u => new { u.Email, u.Password })
               .NotEmpty()
               .MustAsync((a, cancellationToken) => isEmailExist(a.Email, a.Password, cancellationToken))
               .WithMessage(_localizationService.GetMessage(Messages.Error_Login));
        }
        private async Task<bool> isEmailExist(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            return !(user == null || (!await _userManager.CheckPasswordAsync(user, password)));
        }
    }
}
