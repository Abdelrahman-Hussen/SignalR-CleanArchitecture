namespace SignalR.Application.Features.System
{
    public class ConfirmMailOTPDto
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string OTP { get; set; }
    }
}
