﻿namespace SignalR.Domain.Enums
{
    public enum Messages
    {
        Success_General,
        Error_General,
        Error_NotFound,
        Error_InvalidRefreshToken,
        Error_InactiveRefreshToken,
        Error_Login,
        Error_UserEmailExist,
        Error_UserPhoneExist,
        Error_UserNameExist,
        Error_OTPExpired,
        Error_OTPWrong,
        Error_UserEmailNotExist,
        IdentityError_DuplicateEmail,
        IdentityError_DuplicateUserName,
        IdentityError_InvalidEmail,
        IdentityError_DuplicateRoleName,
        IdentityError_InvalidRoleName,
        IdentityError_InvalidUserName,
        IdentityError_PasswordMismatch,
        IdentityError_PasswordRequiresDigit,
        IdentityError_PasswordRequiresLower,
        IdentityError_PasswordRequiresNonAlphanumeric,
        IdentityError_PasswordRequiresUniqueChars,
        IdentityError_PasswordRequiresUpper,
        IdentityError_PasswordTooShort,
        IdentityError_UserAlreadyInRole,
        IdentityError_UserNotinRole,
        IdentityError_DefaultIdentityError
    }
}
