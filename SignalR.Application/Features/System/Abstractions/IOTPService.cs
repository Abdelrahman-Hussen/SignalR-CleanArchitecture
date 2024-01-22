using LurkingUnits.Application;

namespace SignalR.Application.Features.System
{
    public interface IOTPService
    {
        Task<ResponseModel<string>> ConfirmMailOTP(ConfirmMailOTPDto confirmMailOTPDto);
        Task<ResponseModel<string>> SendOTPViaMail(SendMailOTPDto sendOTPDto);
    }
}