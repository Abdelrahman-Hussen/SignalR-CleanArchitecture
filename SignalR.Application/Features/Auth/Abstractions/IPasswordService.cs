using LurkingUnits.Application;

namespace SignalR.Application.Features.Auth
{
    public interface IPasswordService
    {
        Task<ResponseModel<string>> ChangePassword(ChangePasswordDto model);
        Task<ResponseModel<string>> ResetPassword(ResetPasswordDto model);
    }
}