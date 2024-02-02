using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;

namespace SignalR.Application.Validation
{
    internal class LoginValidation : AbstractValidator<LoginDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(u => new { u.Email, u.Password })
               .NotEmpty()
               .MustAsync((a, cancellationToken) => isEmailExist(a.Email, a.Password, cancellationToken))
               .WithMessage(Message.Error_Login);
        }
        private async Task<bool> isEmailExist(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return !(user == null || (!await _userManager.CheckPasswordAsync(user, password)));
        }
    }
}
