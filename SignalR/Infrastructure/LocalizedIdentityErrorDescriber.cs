using Microsoft.AspNetCore.Identity;
using SignalR.Application.Features;
using SignalR.Domain.Enums;

namespace SignalR.Infrastructure
{
    public class LocalizedIdentityErrorDescriber(ILocalizationService _localizationService) : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_DuplicateEmail), email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_DuplicateUserName), userName)
            };
        }

        public override IdentityError InvalidEmail(string? email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_InvalidEmail), email)
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_DuplicateRoleName), role)
            };
        }

        public override IdentityError InvalidRoleName(string? role)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_InvalidRoleName), role)
            };
        }

        public override IdentityError InvalidUserName(string? userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_InvalidUserName), userName)
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = _localizationService.GetMessage(Messages.IdentityError_PasswordMismatch)
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _localizationService.GetMessage(Messages.IdentityError_PasswordRequiresDigit)
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = _localizationService.GetMessage(Messages.IdentityError_PasswordRequiresLower)
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = _localizationService.GetMessage(Messages.IdentityError_PasswordRequiresNonAlphanumeric)
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_PasswordRequiresUniqueChars), uniqueChars)
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = _localizationService.GetMessage(Messages.IdentityError_PasswordRequiresUpper)
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_PasswordTooShort), length)
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_UserAlreadyInRole), role)
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                Description = string.Format(_localizationService.GetMessage(Messages.IdentityError_UserNotinRole), role)
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = _localizationService.GetMessage(Messages.IdentityError_DefaultIdentityError)
            };
        }
    }
}
