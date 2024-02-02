using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features.Auth;
using SignalR.Domain.Models;

namespace SignalR.Application.Validation
{
    internal class RegisterValidation : AbstractValidator<RegisterDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterValidation(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(isEmailUsed)
                .WithMessage(Message.Error_UserEmailExist);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Must(isPhoneNumberUsed)
                .WithMessage(Message.Error_UserPhoneExist);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .Must(isUserNameUsed)
                .WithMessage(Message.Error_UserNameExist);
        }

        private bool isEmailUsed(string email)
            => !_userManager.Users.Any(x => x.Email == email);

        private bool isPhoneNumberUsed(string PhoneNumber)
            => !_userManager.Users.Any(x => x.PhoneNumber == PhoneNumber);

        private bool isUserNameUsed(string userNam)
            => !_userManager.Users.Any(x => x.UserName == userNam);
    }
}
