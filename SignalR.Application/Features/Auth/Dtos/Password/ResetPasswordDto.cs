namespace SignalR.Application.Features.Auth
{
    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
