using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;

namespace SignalR.Application.Validation
{
    internal class ChangePasswordValidation : AbstractValidator<ChangePasswordDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(isUserExist)
                .WithMessage(Message.Error_UserEmailNotExist);
        }

        private bool isUserExist(string email)
            => _userManager.Users.Any(x => x.Email == email);
    }
}
